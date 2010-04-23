'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 4/23/2010
' Heure: 11:37 AM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Targets
	Public Class Target
		Friend Debuff As Debuff
		protected sim as Sim
		
		Sub New(s As Sim)
			sim = s
			Debuff = New Debuff(s)
			'sim.TargetsManager.AllTargets.Add(me)
		End Sub
		
		Sub Unbuff()
			Debuff.Unbuff
		End Sub
	End Class
End Namespace