'
' Created by SharpDevelop.
' User: Fabien
' Date: 06/04/2009
' Time: 22:07
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Class RuneStrike
	Inherits Strikes.Strike
	
	Friend trigger as Boolean
	
	
	
	
	
	Sub New(S As sim )
		MyBase.New(s)
		sim = S
		ThreadMultiplicator = 1.5 * 1.17
		HasteSensible = true
	End Sub
	
	overrides Function ApplyDamage(T As long) As boolean
		Dim dégat As Integer
		Dim MeleeMissChance As Single
		Dim RNG As Double
		
		trigger = false
		Sim.RunicPower.Value = Sim.RunicPower.Value - 20
		RNG = sim.RandomNumberGenerator.RNGWhiteHit
		MeleeMissChance = math.Min(sim.mainstat.Hit, 0.08)
		If MeleeMissChance + RNG < 0.08 Then
			MissCount = MissCount + 1
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Rune Strike fail")
			exit function
		End If
		
		RNG = sim.RandomNumberGenerator.RNGWhiteHit
		If sim.MainStat.DualW And sim.TalentFrost.ThreatOfThassarian = 3 Then
			if offhand=false then sim.OHRuneStrike.ApplyDamage(T)
		End If
		If RNG < CritChance Then
			'CRIT !
			dégat = AvrgCrit(T)
			sim.tryOnCrit
			
			critcount += 1
			totalcrit += dégat
			If sim.combatlog.LogDetails Then sim.combatlog.write(T  & vbtab &  "Rune Strike crit for " & dégat )
		Else
			dégat = AvrgNonCrit(T)
			hitcount += 1
			totalhit += dégat
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Rune Strike hit for " & dégat )
		End If
		total = total + dégat
		'If offhand=False Then sim.proc.KillingMachine.TryMe(T)
		
		'If sim.TalentUnholy.Necrosis > 0 Then sim.Necrosis.Apply(dégat, T)

'		RNG = sim.RandomNumberGenerator.RNGWhiteHit * 100
'		If RNG <= 10 * sim.TalentUnholy.BloodCakedBlade Then
'			If offhand=False Then
'				sim.BloodCakedBlade.ApplyDamage(T)
'			Else
'				sim.OHBloodCakedBlade.ApplyDamage(T)
'			End If
'		End If
		
		If offhand=False Then
			'sim.tryOnMHWhitehitProc
			sim.TryOnMHHitProc
'			sim.RuneForge.MHRazorIce.TryMe(T)
'			If sim.proc.ScentOfBlood.IsActive  Then
'				sim.proc.ScentOfBlood.Use
'				Sim.RunicPower.add(10)
'			End If
		Else
			sim.TryOnOHHitProc
'			sim.RuneForge.OHRazorIce.TryMe(T)
		End If
		return true
	End Function
	overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		If offhand Then
			tmp = sim.MainStat.OHBaseDamage * 1.5
		Else
			tmp = sim.MainStat.MHBaseDamage * 1.5
		End If
		tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		tmp = tmp * (1+ sim.MainStat.T82PTNK*0.1)
		If offhand Then
			tmp = tmp * 0.5
			tmp = tmp * (1 + sim.TalentFrost.NervesofColdSteel * 5 / 100)
		End If
		If sim.EPStat = "EP HasteEstimated" Then
			tmp = tmp*sim.MainStat.EstimatedHasteBonus
		End If
		return tmp
	End Function

	overrides Function CritCoef() As Double
		CritCoef = 1
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	
	overrides Function CritChance() As Double
		Dim tmp As double
		tmp =  sim.MainStat.critAutoattack
		
		If sim.glyph.RuneStrike Then
			tmp =  tmp + 0.1
		End If
		return tmp
	End Function
	
	overrides Function AvrgCrit(T As long) As Double
		return AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	

	Public Overrides Sub Merge()
		If sim.MainStat.DualW = false Then exit sub
		Total += sim.OHRuneStrike.Total
		TotalHit += sim.OHRuneStrike.TotalHit
		TotalCrit += sim.OHRuneStrike.TotalCrit

		MissCount = (MissCount + sim.OHRuneStrike.MissCount)/2
		HitCount = (HitCount + sim.OHRuneStrike.HitCount)/2
		CritCount = (CritCount + sim.OHRuneStrike.CritCount)/2
		
		sim.OHRuneStrike.Total = 0
		sim.OHRuneStrike.TotalHit = 0
		sim.OHRuneStrike.TotalCrit = 0
	End sub

End Class



