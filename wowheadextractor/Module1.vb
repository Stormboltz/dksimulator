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
			Case else
				return color.white
		End Select
	End Function
	
	Function GemSlotColor(id As String) As Color
		Select Case id
			Case 0
				return color.White
			Case 1
				return color.Aqua
			Case 2
				Return color.Red
			Case 4
				Return color.Yellow
			Case 8
				Return color.Blue
			Case Else
				debug.Print ("GemSlotColorName: " & id & " ??")
		end select
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
			Case else
				Return "Meta"
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
			Case Else
				debug.Print ("GemSlotColorName: " & id & " ??")
		End Select
	End Function
	
End Module
