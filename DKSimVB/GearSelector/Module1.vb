'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 2/23/2010
' Heure: 1:25 PM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Module Module1
	Function GemColor(id As String) As Color
		Select Case id
			Case 0
				return color.Red
			Case 1
				return color.Blue
			Case 2
				return color.Yellow
			Case 3
				return color.Violet
			Case 4
				return color.Green
			Case 5
				return color.Orange
			Case 6
				Return color.Aqua
			Case 7,8
				Return color.gray
			Case Else
				Return color.black
		End Select
	End Function
	
	
	
	
	
	Function GemColorName(id As String) As String
		Select Case id
			Case 0
				return "Red"
			Case 1
				return "Blue"
			Case 2
				return "Yell"
			Case 3
				return "Viol"
			Case 4
				return "Gree"
			Case 5
				return "Oran"
			Case 6
				Return "Meta"
			Case 7
				Return "Prism"
			Case Else
				return ""
		End Select
	End Function
	
	
	Function GemSlotColorName(id As String) As String
		Select Case id
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
	
	Function CheckForInt(s as String) As String
		If s <> "" Then
			Return s
		Else
			return 0
		End If
	End Function
	
	Function CheckForDouble(s as String) As String
		If s <> "" Then
			Return s
		Else
			return 0
		End If
	End Function
	
	
	Function GetSkillID(skill As String) As integer
		Select Case skill
			Case "Alchemy"
				return 0
			Case"Blacksmithing"
				return 164
			Case"Enchanting"
				return 333
			Case"Engineering"
				return 202
			Case"Inscription"
				return 773
			Case"Jewelcrafting"
				return 755
			Case"Leatherworking"
				return 165
			Case"Herb Gathering"
				return 0
			Case"Mining"
				return 0
			Case"Skinning"
				return 0
			case"Tailoring"
				return 197
			Case Else
				return 0
		End Select
	End Function
	
End Module
