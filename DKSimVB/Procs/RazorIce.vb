'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 11/12/2009
' Heure: 3:15 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class RazorIce
	Inherits Proc

	Sub New(S As Sim)
		MyBase.New(S)
		sim.RuneForge.RazorIceStack = 0
	End Sub
	
	Overrides Sub TryMe(T As Long)
		If Equiped = 0 Then Exit Sub
		If RNGProc <= ProcChance Then
			HitCount +=1
			me.AddUptime(T)
			If sim.combatlog.LogDetails Then sim.combatlog.write(sim.TimeStamp  & vbtab &  Me.ToString & " proc")
			sim.RuneForge.AddRazorIceStack(T)
		end if
	End Sub
End Class
