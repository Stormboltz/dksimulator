'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 2/22/2010
' Heure: 2:54 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class GearLoader
	Friend ItemDB As new Xml.XmlDocument
	
	Sub Init
		ItemDB.Load(Application.StartupPath & "\GearSelector\" & "itemDB.xml")
	End Sub
	
	Sub FillThis(cmb As ComboBox,slot As String)
		Dim xList As Xml.XmlNodeList
		dim xNode as Xml.XmlNode
		xList = ItemDB.SelectNodes("/items/item[slot="& slot &"]/name")
		
		For Each xNode In xList
			cmb.Items.Insert(0,xNode.InnerText)
		Next
		
	End Sub
	
End Class
