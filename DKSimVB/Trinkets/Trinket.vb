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
		_RNG = nothing
		Total = 0
		HitCount = 0
		MissCount =0
		CritCount = 0
		ProcChance = 0
		Equiped = 0
		ProcLenght = 0
		ProcValue = 0
		InternalCD = 0
	End Sub
	Sub New(S As Sim)
		me.New
		Sim = S
		s.TrinketsCollection.Add(me)
	End Sub
	
	Sub ApplyDamage(d As Integer)
		If RNGProc < (0.17 - sim.MainStat.SpellHit) Then
			MissCount = MissCount + 1
			Exit sub
		End If
		dim dégat as Integer
		If sim.RandomNumberGenerator.RNGProc <= sim.MainStat.SpellCrit Then
			CritCount = CritCount + 1
			dégat= d*1.5 * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
		Else
			dégat= d * sim.MainStat.StandardMagicalDamageMultiplier(sim.TimeStamp)
			HitCount = HitCount + 1
		End If
		total = total + dégat
	End Sub
End Class
