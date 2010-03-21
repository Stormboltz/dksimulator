'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 18/03/2010
' Heure: 16:54
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Gem
	Friend ColorId as Integer
	Friend Id As Integer
	Friend name As String
	Friend ilvl As Integer
	Friend classs As Integer
	Friend subclass As Integer = -1

	friend Strength As Integer
	friend Intel as Integer
	friend Agility as Integer
	Friend HasteRating As Integer
	Friend ExpertiseRating As Integer
	friend HitRating as Integer
	friend AttackPower As Integer
	friend CritRating As Integer
	Friend ArmorPenetrationRating As Integer
	Friend keywords As String
	
	
	Protected GemDB As xml.XmlDocument
	Protected MainFrame as GearSelectorMainForm
	
	Sub New (MainFrm  As GearSelectorMainForm,Color As Integer)
		MainFrame = MainFrm
		GemDB = MainFrame.GemDB
		ColorId = Color
	End Sub
	Sub Attach(GemId As Integer)
		If GemId = 0 or ColorId = 0 Then
			Detach
			Exit sub
		End If
		id = GemId
		name  =GemDB.SelectSingleNode("/gems/item[id=" & GemId & "]/name").InnerText
		ilvl =GemDB.SelectSingleNode("/gems/item[id=" & GemId & "]/ilvl").InnerText
		classs =GemDB.SelectSingleNode("/gems/item[id=" & GemId & "]/class").InnerText
		subclass=GemDB.SelectSingleNode("/gems/item[id=" & GemId & "]/subclass").InnerText
		Strength = GemDB.SelectSingleNode("/gems/item[id=" & GemId & "]/Strength").InnerText
		Agility = GemDB.SelectSingleNode("/gems/item[id=" & GemId & "]/Agility").InnerText
		HasteRating = GemDB.SelectSingleNode("/gems/item[id=" & GemId & "]/HasteRating").InnerText
		ExpertiseRating = GemDB.SelectSingleNode("/gems/item[id=" & GemId & "]/ExpertiseRating").InnerText
		HitRating = GemDB.SelectSingleNode("/gems/item[id=" & GemId & "]/HitRating").InnerText
		AttackPower = GemDB.SelectSingleNode("/gems/item[id=" & GemId & "]/AttackPower").InnerText
		CritRating = GemDB.SelectSingleNode("/gems/item[id=" & GemId & "]/CritRating").InnerText
		ArmorPenetrationRating = GemDB.SelectSingleNode("/gems/item[id=" & GemId & "]/ArmorPenetrationRating").InnerText
		keywords = GemDB.SelectSingleNode("/gems/item[id=" & GemId & "]/keywords").InnerText
	End Sub
	
	Sub Detach()
		Id = 0
		'ColorId = 0
		name  =""
		ilvl =0
		classs =0
		subclass=-1
		Strength = 0
		Agility = 0
		HasteRating = 0
		ExpertiseRating = 0
		HitRating = 0
		AttackPower = 0
		CritRating = 0
		ArmorPenetrationRating = 0
		keywords = ""
	End Sub
	
	Function GemSlotColorName() As String
		Select Case ColorId
			Case 0
				return ""
			Case 1
				return "Meta"
			Case 2
				Return "Red"
			Case 4
				Return "Yellow"
			Case 8
				Return "Blue"
			Case 16
				Return "Prism"
			Case Else
				debug.Print ("GemSlotColorName: " & id & " ??")
				return ""
		End Select
	End Function
	
	Function GemSlotColor() As Color
		Select Case ColorId
			Case 0
				return color.Transparent
			Case 1
				return color.Aqua
			Case 2
				Return color.Red
			Case 4
				Return color.Yellow
			Case 8
				Return color.Blue
			Case 16
				Return color.Gray
				
			Case Else
				debug.Print ("GemSlotColorName: " & id & " ??")
		end select
	End Function
	
	Function IsGemrightColor() As Boolean
		
		if subclass = 8 then return true
		Select Case ColorId
			Case 0 '??
				return false
			Case 1 ' Meta
				if subclass = 6 then return true
			Case 2 ' Red
				if subclass = 0 or subclass = 3  or subclass = 5 then return true
				
			Case 4 'Yellow
				if subclass = 2 or subclass = 4  or subclass = 5 then return true
			Case 8 ' Blue
				if subclass = 1 or subclass = 3  or subclass = 4 then return true
			Case 16 'Prim
				return true
			Case Else
				debug.Print ("GemSlotColorName: " & ColorId & " ??")
				return false
		End Select
		return false
		
	End Function
	
	
End Class
