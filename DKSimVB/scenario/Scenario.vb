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
			on error resume next
			Elements.Clear
			Dim xmlScenario As new Xml.XmlDocument
 			xmlScenario.Load(sim.ScenarioPath)
			Dim e As Element
			Dim xNode As Xml.XmlNode
			dim id as Integer
			For Each xNode In xmlScenario.SelectNodes("/Scenario/Element")
				id = xNode.Attributes.GetNamedItem("id").InnerText
				e = new Element(Me)
				e.CanTakeDiseaseDamage = xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/CanTakeDiseaseDamage").InnerText
				e.CanTakePetDamage = xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/CanTakePetDamage").InnerText
				e.CanTakePlayerStrike = xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/CanTakePlayerStrike").InnerText
				e.AddPop = xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/AddPop").InnerText
				e.DamageBonus = xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/DamageBonus").InnerText
				e.Start = sim.TimeStamp + ( xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/Start").InnerText * xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/Start").Attributes.GetNamedItem("multi").InnerText)
				e.length = xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/length").InnerText * xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/length").Attributes.GetNamedItem("multi").InnerText
				e.SpreadDisease = xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/SpreadDisease").InnerText 
				e.FightStop = sim.TimeStamp + xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/FightStop").InnerText * xNode.SelectSingleNode("/Scenario/Element[@id=" & id &"]/FightStop").Attributes.GetNamedItem("multi").InnerText
				If e.CanTakeDiseaseDamage =False Or e.CanTakePetDamage = False Or e.CanTakePlayerStrike = false Then
					sim.FutureEventManager.Add(e.Start,"Scenario")
				End If
				If e.DamageBonus <> 0 Then
					sim.FutureEventManager.Add(e.Start,"SuperBuff")
				End If
				If e.AddPop <> 0 Then
					sim.FutureEventManager.Add(e.Start,"AddPop")
					sim.FutureEventManager.Add(e.Ending,"AddDepop")
				End If
				If e.FightStop <> 0 Then
					If sim.FightLength = 0 Then
						sim.FightLength = e.FightStop / 100
					End If
					sim.FutureEventManager.Add(e.FightStop,"FightStop")
					sim.NextReset = e.FightStop
					If sim.NextReset > sim.MaxTime Then sim.NextReset = sim.MaxTime
				End If
				
			Next
		End Sub
		
		
	End Class
End Namespace
