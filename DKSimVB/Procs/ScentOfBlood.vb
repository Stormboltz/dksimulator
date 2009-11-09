'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 11/9/2009
' Heure: 3:03 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class ScentOfBlood
	Inherits Proc
	Sub New(S As Sim)
		MyBase.New(S)
	End Sub
	
	Overrides Function Use() As Boolean
		count -= 1
		if count <= 0 then Fade = 0
	End Function
	
	Overrides Sub TryMe(T As Long)
		If Equiped = 0 Or CD > T Then Exit Sub
		If RNGProc <= ProcChance Then
			If sim.combatlog.LogDetails Then sim.combatlog.write(sim.TimeStamp  & vbtab &  Me.ToString & " proc")
			Fade = T + ProcLenght * 100
			Count = Equiped
			HitCount += 1
		end if
	End Sub
	
End Class
