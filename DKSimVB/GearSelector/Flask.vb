'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 3/25/2010
' Heure: 4:04 PM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Flask
	Friend Id As Integer
	Friend name As String
	friend Strength As Integer
	friend Intel as Integer
	friend Agility as Integer
	Friend HasteRating As Integer
	Friend ExpertiseRating As Integer
	friend HitRating as Integer
	friend AttackPower As Integer
	friend CritRating As Integer
	Friend ArmorPenetrationRating As Integer
	Friend Desc As String
	Friend Armor as Integer
	Protected FlaskDB As xml.XmlDocument
	Protected MainFrame as GearSelectorMainForm
	
	Sub New (MainFrm  As GearSelectorMainForm)
		MainFrame = MainFrm
		FlaskDB = MainFrame.FlaskDB
	End Sub
	Sub Attach(FlaskId As Integer)
		If FlaskId = 0 Then
			Detach
			Exit sub
		End If
		id = FlaskId
		name = FlaskDB.SelectSingleNode("/flask/item[id=" & FlaskId & "]/name").InnerText
		Strength = FlaskDB.SelectSingleNode("/flask/item[id=" & FlaskId & "]/Strength").InnerText
		Agility = FlaskDB.SelectSingleNode("/flask/item[id=" & FlaskId & "]/Agility").InnerText
		HasteRating = FlaskDB.SelectSingleNode("/flask/item[id=" & FlaskId & "]/HasteRating").InnerText
		ExpertiseRating = FlaskDB.SelectSingleNode("/flask/item[id=" & FlaskId & "]/ExpertiseRating").InnerText
		HitRating = FlaskDB.SelectSingleNode("/flask/item[id=" & FlaskId & "]/HitRating").InnerText
		AttackPower = FlaskDB.SelectSingleNode("/flask/item[id=" & FlaskId & "]/AttackPower").InnerText
		CritRating = FlaskDB.SelectSingleNode("/flask/item[id=" & FlaskId & "]/CritRating").InnerText
		ArmorPenetrationRating = FlaskDB.SelectSingleNode("/flask/item[id=" & FlaskId & "]/ArmorPenetrationRating").InnerText
		Armor = FlaskDB.SelectSingleNode("/flask/item[id=" & FlaskId & "]/Armor").InnerText
		Desc = FlaskDB.SelectSingleNode("/flask/item[id=" & FlaskId & "]/Desc").InnerText
	End Sub
	
	Sub Attach(FlaskName As String)
		If FlaskName = "" Then
			Detach
			Exit sub
		End If
		Try
			Dim XPathQ As String
			
			XPathQ = "/flask/item[name=" & chr(34) & FlaskName & chr(34) &  "]"
			
			id = FlaskDB.SelectSingleNode(XPathQ & "/id").InnerText
			name = FlaskDB.SelectSingleNode(XPathQ & "/name").InnerText
			Strength = FlaskDB.SelectSingleNode(XPathQ & "/Strength").InnerText
			Agility = FlaskDB.SelectSingleNode(XPathQ & "/Agility").InnerText
			HasteRating = FlaskDB.SelectSingleNode(XPathQ & "/HasteRating").InnerText
			ExpertiseRating = FlaskDB.SelectSingleNode(XPathQ & "/ExpertiseRating").InnerText
			HitRating = FlaskDB.SelectSingleNode(XPathQ & "/HitRating").InnerText
			AttackPower = FlaskDB.SelectSingleNode(XPathQ & "/AttackPower").InnerText
			CritRating = FlaskDB.SelectSingleNode(XPathQ & "/CritRating").InnerText
			ArmorPenetrationRating = FlaskDB.SelectSingleNode(XPathQ & "/ArmorPenetrationRating").InnerText
			Armor = FlaskDB.SelectSingleNode(XPathQ & "/Armor").InnerText
			Desc = FlaskDB.SelectSingleNode(XPathQ & "/Desc").InnerText
		Catch Err as Exception
			debug.Print(Err.ToString)
			
		End Try
			
	End Sub
	
	
	
	Sub Detach()
		Id = 0
		name = ""
		Strength = 0
		Agility = 0
		HasteRating = 0
		ExpertiseRating = 0
		HitRating = 0
		AttackPower = 0
		CritRating = 0
		ArmorPenetrationRating = 0
		Armor = 0
		Desc = ""
	End Sub
End Class
