﻿'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 11/12/2009
' Heure: 2:01 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class ScourgeStrikeMagical
	Inherits strikes.Strike
	
	friend tmpPhysical as Integer
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	public Overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
	
	
	Function ApplyDamage(PhysicalDamage As Integer,T As Long) As Boolean
		Dim RNG As Double
		dim tmp as Integer
		tmpPhysical = PhysicalDamage
		RNG = MyRNG
		
		If RNG <= CritChance Then
			CritCount +=1
			TotalCrit = TotalCrit + 1
			tmp = AvrgNonCrit(T)* (1 + CritCoef)
			sim.tryOnCrit
		Else
			HitCount += 1 
			tmp = AvrgNonCrit(T)
		End If
		
		total += tmp
	End Function
	
	
	public Overrides Function AvrgNonCrit(T As Long) As Double
		dim tmpMagical as Integer
		tmpMagical = tmpPhysical
		If sim.MainStat.T84PDPS = 1 Then
			tmpMagical = tmpMagical * (0.25 * Sim.NumDesease * 1.2)
		Else
			tmpMagical = tmpMagical * (0.25 * Sim.NumDesease )
		End If
		Dim tmp As Double
		tmp = 1
		
		tmp = tmp * (1 + sim.mainstat.BloodPresence * 0.15)
		tmp = tmp * (1 + 0.03 *  sim.Buff.PcDamage)
		If sim.Desolation.isActive(T) Then tmp = tmp * (1+sim.Desolation.Bonus)
		tmp = tmp * (1 + 0.02 * sim.BoneShield.Value(T))
		tmp = tmp * (1 + 0.02 * TalentBlood.BloodGorged)
		if sim.proc.T104PDPSFAde >= T then tmp = tmp * 1.03
		tmp = tmp * (1 + 0.13 *  sim.Buff.SpellDamageTaken)
		tmp = tmp * (1-0.05) 'Average partial resist
		tmpMagical = tmpMagical * tmp
		tmpMagical = tmpMagical * (1 + TalentFrost.BlackIce * 2 / 100)
		If sim.RuneForge.CinderglacierProc > 0 Then
			tmpMagical = tmpMagical * 1.2
			sim.RuneForge.CinderglacierProc = sim.RuneForge.CinderglacierProc -1
		End If

		Return tmpMagical
	End Function
	
	Public Overrides Function CritCoef() As Double
		return sim.ScourgeStrike.CritCoef
	End Function
	Public Overrides Function CritChance() As Double
		return sim.ScourgeStrike.CritChance
	End Function
End Class