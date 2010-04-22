'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 4/22/2010
' Heure: 2:12 PM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Scenarios
	Public Class Scenario
		Friend sim As Sim
		Friend Elements As new Collection
		
		
		Sub New (s As sim)
			sim = s
		End Sub
		
		Sub SoftReset
			Elements.Clear
			Dim xmlScenario As new Xml.XmlDocument
			
 			xmlScenario.Load(Application.StartupPath & "\scenario\Scenario.xml")
			
			Dim e As Element
			
			Dim xNode As Xml.XmlNode
			dim id as Integer
			For Each xNode In xmlScenario.SelectNodes("/Scenario/Element")
				id = xNode.Attributes.GetNamedItem("id").InnerText
				e = new Element(Me)
				e.CanTakeDiseaseDamage = xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/CanTakeDiseaseDamage").InnerText
				e.CanTakePetDamage = xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/CanTakePetDamage").InnerText
				e.CanTakePlayerStrike = xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/CanTakePlayerStrike").InnerText
				e.Start = sim.TimeStamp + xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/Start").InnerText
				e.length = xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/length").InnerText
				sim.FutureEventManager.Add(e.Start,"Scenario")
			Next
		End Sub
		
		
		
		
		
	End Class
End Namespace
