'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 4/23/2010
' Heure: 12:48 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Targets
Public Class TargetsManager
	Friend AllTargets As new Collections.ArrayList
	Friend MainTarget As Target
	Friend CurrentTarget As Target
	
	Function Count As Integer
		return AllTargets.count
	End function
	
	Sub New(s As sim)
		
	End Sub
	Sub KillEveryoneExceptMainTarget()
		dim Tar as Target
		Do Until AllTargets.Count = 1
			For Each Tar In AllTargets
				If Tar.Equals(Maintarget) = False Then
					Tar.Kill
					Goto nextone
				End If
				
			Next
			nextone:
		Loop
	End Sub
	
	Function IsFrostFeverOnAll(T as Long) as Boolean
		dim Tar as Target
		For Each Tar In AllTargets
			If Tar.FrostFever.isActive(T) = false Then return false
		Next
		Return True
	End Function
	Function IsBloodPlagueOnAll (T as Long) as Boolean
		dim Tar as Target
		For Each Tar In AllTargets
			If Tar.BloodPlague.isActive(T) = false Then return false
		Next
		Return True
	End Function
	
End Class
End Namespace