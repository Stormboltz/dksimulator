'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 15/08/2009
' Heure: 21:30
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'

Public Class Trinket
	inherits Proc
	
	Sub New()
		mybase.New
	End Sub
	Sub New(S As Sim)
		me.New
		Sim = S
		s.TrinketsCollection.Add(Me)
	End Sub
End Class
