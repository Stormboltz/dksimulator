'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 15/08/2009
' Heure: 21:30
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'

Public Class Trinket
	inherits WeaponProc
	
	Sub New(S As Sim)
		mybase.New(S)
		Sim = S
		s.TrinketsCollection.Add(Me)
	End Sub
End Class
