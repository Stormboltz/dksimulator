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
		'Not Used
	End Sub
	
	Overrides Sub ApplyMe(T As Long)
		HitCount += 1
		Me.AddUptime(T)
		If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & Me.ToString & " proc")
		'sim.RuneForge.AddRazorIceStack(T)
	End Sub
End Class
