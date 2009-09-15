'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 15/09/2009
' Heure: 16:15
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Module SimContructor
	Friend sim As Sim
	
	
	Sub Start(pb As ProgressBar,SimTime As Double, MainFrm As MainForm)
		Sim = New Sim
		Sim.Start(pb,Simtime, Mainfrm)
	End Sub
	
	
	
	
	
	
End Module
