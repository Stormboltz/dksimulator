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
	
	Sub new(S as sim )
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
		sim = S
		ThreadMultiplicator = 1.5 * 1.17
	End Sub
	overrides Function ApplyDamage(T As long) As boolean
		Dim dégat As Integer
		Dim BCB As Double
		Dim Nec As Double
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

		
		If sim.MainStat.DualW And talentfrost.ThreatOfThassarian = 3 Then
			'Off hand
			If RNG < CritChance Then
				'CRIT !
				dégat = AvrgOHCrit(T)
				If sim.combatlog.LogDetails Then sim.combatlog.write(T  & vbtab &  "Rune Strike OH crit for " & dégat )
			Else
				dégat = AvrgOHNonCrit(T)
				if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Rune Strike OH hit for " & dégat )
			End If
			
		End If
		
		'MainHand
		If RNG < CritChance Then
			'CRIT !
			dégat = AvrgCrit(T)
			CritCount = CritCount + 1
			If sim.combatlog.LogDetails Then sim.combatlog.write(T  & vbtab &  "Rune Strike crit for " & dégat )
		Else
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Rune Strike hit for " & dégat )
		End If
'		if Lissage then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance )
		total = total + dégat
		
		sim.proc.KillingMachine.TryMe(T)
		
		If sim.RuneForge.MHRazorice Then sim.runeforge.applyRazorice()
		If TalentUnholy.Necrosis > 0 Then
			Nec = sim.Necrosis.Apply(dégat, T)
		End If
		RNG = sim.RandomNumberGenerator.RNGWhiteHit * 100
		If RNG <= 10 * TalentUnholy.BloodCakedBlade Then
			BCB = sim.BloodCakedBlade.ApplyDamage(T,true)
		End If
		sim.TryOnMHHitProc
		If sim.proc.ScentOfBlood.IsActive  Then
			sim.proc.ScentOfBlood.Use
			Sim.RunicPower.add(10)
		End If
		
		return true
	End Function
	overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = sim.MainStat.MHBaseDamage * 1.5
		tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		tmp = tmp * (1+ sim.MainStat.T82PTNK*0.1)
		return tmp
	End Function
	
	
	Function AvrgOHNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = sim.MainStat.OHBaseDamage * 1.5
		tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
		tmp = tmp * (1+ sim.MainStat.T82PTNK*0.1)
		tmp = tmp * 0.5
		tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
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
	
	Function AvrgOHCrit(T As long) As Double
		return AvrgOHNonCrit(T) * (1 + CritCoef)
	End Function
End Class



