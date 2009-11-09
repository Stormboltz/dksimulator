'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 11/9/2009
' Heure: 2:12 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Rime
	Inherits Proc
	

	
	Sub New(S As Sim)
		MyBase.New(S)
	End Sub
	
	Overrides Sub TryMe(T As Long)
		If Equiped = 0 Or CD > T Then Exit Sub
		If RNGProc <= ProcChance Then
			If sim.combatlog.LogDetails Then sim.combatlog.write(sim.TimeStamp  & vbtab &  Me.ToString & " proc")
			Fade = T + ProcLenght * 100
			Count += 1
			HitCount += 1
			sim.HowlingBlast.CD = 0
		end if
	End Sub
	
End Class
