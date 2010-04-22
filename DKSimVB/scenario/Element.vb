'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 4/22/2010
' Heure: 2:13 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Scenarios
Public Class Element
	Friend CanTakePetDamage As Boolean
	Friend CanTakeDiseaseDamage As Boolean
	Friend CanTakePlayerStrike As Boolean
	Friend Start As Long
	Friend length as long	
	Protected sim As Sim
	Protected Scenario As Scenario
	
	
	Sub New(Scenar As Scenario)
		Scenario = Scenar
		sim = Scenario.sim
		Scenario.Elements.Add(Me)
	End Sub
	
	Function Ending As Long
		return (Start + length)
	End Function
	
	
	
	
End Class
End Namespace
