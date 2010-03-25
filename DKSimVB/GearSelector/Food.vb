'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 3/25/2010
' Heure: 3:55 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Food
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
	Protected foodDB As xml.XmlDocument
	Protected MainFrame as GearSelectorMainForm
	
	Sub New (MainFrm  As GearSelectorMainForm)
		MainFrame = MainFrm
		foodDB = MainFrame.foodDB
	End Sub
	Sub Attach(FoodId As Integer)
		If FoodId = 0 Then
			Detach
			Exit sub
		End If
		id = FoodId
		name = foodDB.SelectSingleNode("/food/item[id=" & FoodId & "]/name").InnerText
		Strength = foodDB.SelectSingleNode("/food/item[id=" & FoodId & "]/Strength").InnerText
		Agility = foodDB.SelectSingleNode("/food/item[id=" & FoodId & "]/Agility").InnerText
		HasteRating = foodDB.SelectSingleNode("/food/item[id=" & FoodId & "]/HasteRating").InnerText
		ExpertiseRating = foodDB.SelectSingleNode("/food/item[id=" & FoodId & "]/ExpertiseRating").InnerText
		HitRating = foodDB.SelectSingleNode("/food/item[id=" & FoodId & "]/HitRating").InnerText
		AttackPower = foodDB.SelectSingleNode("/food/item[id=" & FoodId & "]/AttackPower").InnerText
		CritRating = foodDB.SelectSingleNode("/food/item[id=" & FoodId & "]/CritRating").InnerText
		ArmorPenetrationRating = foodDB.SelectSingleNode("/food/item[id=" & FoodId & "]/ArmorPenetrationRating").InnerText
		Desc = foodDB.SelectSingleNode("/food/item[id=" & FoodId & "]/Desc").InnerText
	End Sub
	
	Sub Attach(FoodName As String)
		If FoodName = "" Then
			Detach
			Exit sub
		End If
		Dim XPathQ As String
		XPathQ = "/food/item[name=" & chr(34) & FoodName & chr(34) &  "]"
			
		id = foodDB.SelectSingleNode(XPathQ & "/id").InnerText
		name = foodDB.SelectSingleNode(XPathQ & "/name").InnerText
		Strength = foodDB.SelectSingleNode(XPathQ & "/Strength").InnerText
		Agility = foodDB.SelectSingleNode(XPathQ & "/Agility").InnerText
		HasteRating = foodDB.SelectSingleNode(XPathQ & "/HasteRating").InnerText
		ExpertiseRating = foodDB.SelectSingleNode(XPathQ & "/ExpertiseRating").InnerText
		HitRating = foodDB.SelectSingleNode(XPathQ & "/HitRating").InnerText
		AttackPower = foodDB.SelectSingleNode(XPathQ & "/AttackPower").InnerText
		CritRating = foodDB.SelectSingleNode(XPathQ & "/CritRating").InnerText
		ArmorPenetrationRating = foodDB.SelectSingleNode(XPathQ & "/ArmorPenetrationRating").InnerText
		Desc = foodDB.SelectSingleNode(XPathQ & "/Desc").InnerText
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
		Desc = ""
	End Sub
End Class
