'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 18/03/2010
' Heure: 18:21
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Enchant
	Friend Id As Integer
	Friend name As String
	Friend slot As Integer
	
	
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
	
	
	Protected EnchantDB As xml.XmlDocument
	Protected MainFrame as GearSelectorMainForm
	
	Sub New (MainFrm  As GearSelectorMainForm)
		MainFrame = MainFrm
		EnchantDB = MainFrame.EnchantDB
	End Sub
	Sub Attach(EnchantId As Integer)
		If EnchantId = 0 Then
			Detach
			Exit sub
		End If
		id = EnchantId
		name = EnchantDB.SelectSingleNode("/enchant/item[id=" & EnchantId & "]/name").InnerText
		slot = EnchantDB.SelectSingleNode("/enchant/item[id=" & EnchantId & "]/slot").InnerText
		Strength = EnchantDB.SelectSingleNode("/enchant/item[id=" & EnchantId & "]/Strength").InnerText
		Agility = EnchantDB.SelectSingleNode("/enchant/item[id=" & EnchantId & "]/Agility").InnerText
		HasteRating = EnchantDB.SelectSingleNode("/enchant/item[id=" & EnchantId & "]/HasteRating").InnerText
		ExpertiseRating = EnchantDB.SelectSingleNode("/enchant/item[id=" & EnchantId & "]/ExpertiseRating").InnerText
		HitRating = EnchantDB.SelectSingleNode("/enchant/item[id=" & EnchantId & "]/HitRating").InnerText
		AttackPower = EnchantDB.SelectSingleNode("/enchant/item[id=" & EnchantId & "]/AttackPower").InnerText
		CritRating = EnchantDB.SelectSingleNode("/enchant/item[id=" & EnchantId & "]/CritRating").InnerText
		ArmorPenetrationRating = EnchantDB.SelectSingleNode("/enchant/item[id=" & EnchantId & "]/ArmorPenetrationRating").InnerText
		Desc = EnchantDB.SelectSingleNode("/enchant/item[id=" & EnchantId & "]/Desc").InnerText
	End Sub
	
	Sub Detach()
		Id = 0
		slot = 0
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
