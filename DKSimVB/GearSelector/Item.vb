'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 18/03/2010
' Heure: 16:45
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Item
	
	Friend Id As Integer
	Friend name As String
	Friend ilvl As Integer
	Friend slot as Integer
	Friend classs As Integer
	Friend subclass As Integer
	Friend heroic As Integer
	
	friend Strength As Integer
	friend Intel as Integer
	friend Agility as Integer
	friend BonusArmor as Integer
	friend Armor As Integer
	Friend HasteRating As Integer
	Friend ExpertiseRating As Integer
	
	
	friend HitRating as Integer
	friend AttackPower As Integer
	friend CritRating As Integer
	friend ArmorPenetrationRating As Integer
	friend Speed As string = "0"
	Friend DPS As String = "0"
	
	Friend setid As Integer
	
	Friend gem1 As Gem
	Friend gem2 As Gem
	Friend gem3 As Gem
	Friend Enchant As Enchant
	
	
	friend gembonus as Integer
	Friend keywords As String
	
	protected MainFrame as GearSelectorMainForm
	
	Protected ItemDB As Xml.XmlDocument
	
	Protected AdditionalGemNotSet As Boolean
	
	
	
	
	
	Sub New(MainFrm As GearSelectorMainForm, Optional ItemId As Integer = 0)
		MainFrame = MainFrm
		ItemDB = MainFrame.ItemDB
		
		gem1 = New Gem(Me.MainFrame,0)
		gem2 = New Gem(Me.MainFrame,0)
		gem3 = New Gem(Me.MainFrame,0)
		Enchant = new Enchant(Me.MainFrame)
		if ItemId <> 0 then LoadItem(ItemId)
	End Sub
	
	
	
	Sub LoadItem(ItemId As Integer)
		AdditionalGemNotSet = True
		id = ItemId
		If id = 0 Then
			Unload
			exit sub
		End If
		name  =ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/name").InnerText
		ilvl =ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/ilvl").InnerText
		slot =ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/slot").InnerText
		classs =ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/classs").InnerText
		subclass=ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/subclass").InnerText
		heroic =ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/heroic").InnerText
		
		
		
		Strength = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/Strength").InnerText
		Agility = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/Agility").InnerText
		BonusArmor = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/BonusArmor").InnerText
		Armor = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/Armor").InnerText
		HasteRating = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/HasteRating").InnerText
		ExpertiseRating = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/ExpertiseRating").InnerText
		HitRating = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/HitRating").InnerText
		AttackPower = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/AttackPower").InnerText
		CritRating = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/CritRating").InnerText
		ArmorPenetrationRating = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/ArmorPenetrationRating").InnerText
		
		
		Speed = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/speed").InnerText
		DPS =  ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/dps").InnerText
		
		setid = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/setid").InnerText
		
		Dim gem1Col As Integer = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/gem1").InnerText
		Dim gem2Col As Integer = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/gem2").InnerText
		Dim gem3Col as Integer = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/gem3").InnerText
		
		If gem1Col <> 0 Then
			gem1 = new Gem(me.MainFrame,gem1Col)
		Else
			If AdditionalGem And AdditionalGemNotSet Then
				gem1 = New Gem(Me.MainFrame,16)
				AdditionalGemNotSet=false
			Else
				gem1 = New Gem(Me.MainFrame,0)
			End If
		End If
		
		If gem2Col <> 0 Then
			gem2 = new Gem(me.MainFrame,gem2Col)
		Else
			If AdditionalGem And AdditionalGemNotSet Then
				gem2 = New Gem(Me.MainFrame,16)
				AdditionalGemNotSet=false
			Else
				gem2 = New Gem(Me.MainFrame,0)
			End If
			
		End If
		If gem3Col <> 0 Then
			gem3 = new Gem(me.MainFrame,gem3Col)
		Else
			If AdditionalGem And AdditionalGemNotSet Then
				gem3 = New Gem(Me.MainFrame,16)
				AdditionalGemNotSet=false
			Else
				gem3 = new Gem(me.MainFrame,0)
			End If
		End If
		gembonus = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/gembonus").InnerText
		keywords = ItemDB.SelectSingleNode("/items/item[id=" & ItemId & "]/keywords").InnerText
	End Sub
	
	
	Sub Unload()
		Id = 0
		name  =""
		ilvl =0
		slot =0
		classs =0
		subclass=0
		heroic =0
		Strength = 0
		Agility = 0
		BonusArmor = 0
		Armor = 0
		HasteRating = 0
		ExpertiseRating = 0
		HitRating = 0
		AttackPower = 0
		CritRating = 0
		ArmorPenetrationRating = 0
		Speed = 0
		DPS =  0
		setid = 0
		gem1.Detach
		gem2.Detach
		gem3.Detach
		Enchant.Detach
		
		gembonus = 0
		keywords = ""
	End Sub
	
	Function AdditionalGem As Boolean
		Select Case slot
			Case 9,10
				If GetSkillID(Me.MainFrame.cmbSkill1.SelectedItem) =  164 Or GetSkillID(Me.MainFrame.cmbSkill2.SelectedItem) = 164 Then
					Return True
				Else
					return false
				End If
			Case 6
				return true
			Case Else
				return false
		End Select
		
	End Function
	
	Function IsGembonusActif As Boolean
		
		If gem1.IsGemrightColor or gem1.ColorId=0 or gem1.ColorId=16 Then
			If gem2.IsGemrightColor or gem2.ColorId=0 or gem2.ColorId=16 Then
				If gem3.IsGemrightColor or gem3.ColorId=0 or gem3.ColorId=16 Then
					return true
				End If
			End If
		End If
		return false
	End Function
	
	
	
End Class
