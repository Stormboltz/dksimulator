Friend Class MainHand
	Inherits Strikes.Strike

	Friend NextWhiteMainHit As long
	
	Protected Overrides sub init()
		MyBase.init()
		NextWhiteMainHit = 0
	End Sub

	Overrides Function ApplyDamage(T As long) As boolean
		Dim dégat As long
		Dim BCB As Double
		Dim Nec As Double
		Dim WSpeed As Single
		Dim MeleeMissChance As Single
		Dim MeleeDodgeChance As Single
		Dim MeleeGlacingChance As Single
		Dim MeleeParryChance As Single
		Dim ChanceNotToTouch As Single
		
		WSpeed = sim.MainStat.MHWeaponSpeed
		NextWhiteMainHit = T + (WSpeed * 100) / ((1 + sim.MainStat.Haste))
		
		If sim.MainStat.FrostPresence = 1 Then
			If Sim.RunicPower.Value >= 20 Then
				sim.RuneStrike.ApplyDamage(T)
				return true
			End If
		End If
		
		Dim RNG As Double
		RNG = sim.RandomNumberGenerator.RNGWhiteHit
		MeleeGlacingChance = 0.25
		MeleeDodgeChance = 0.065
		If sim.mainstat.FrostPresence =1 Then
			MeleeParryChance = 0.14
		Else
			MeleeParryChance = 0
		End If
		If sim.mainstat.DualW Then
			MeleeMissChance = 0.27
		Else
			MeleeMissChance = 0.08
		End If
		
		ChanceNotToTouch = MeleeMissChance + MeleeDodgeChance  + MeleeParryChance
		
		
		If math.Min(sim.mainstat.Expertise,MeleeDodgeChance)+ math.Min(sim.mainstat.Expertise,MeleeParryChance) + math.Min (sim.mainstat.Hit,MeleeMissChance) + RNG < ChanceNotToTouch Then
			MissCount = MissCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "MH fail")
			exit function
		End If
		
		If RNG < (ChanceNotToTouch + MeleeGlacingChance) Then
			dégat = AvrgNonCrit(T)*0.7
			HitCount = HitCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "MH glancing for " & dégat)
		End If
		
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance) and RNG < (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'CRIT !
			dégat = AvrgCrit(T)
			CritCount = CritCount + 1
			If combatlog.LogDetails Then combatlog.write(T  & vbtab &  "MH crit for " & dégat )
			sim.tryOnCrit
		End If
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'normal hit3
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "MH hit for " & dégat )
		End If
		
		If Lissage Then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance-MeleeGlacingChance) + AvrgNonCrit(T)* (MeleeGlacingChance)*0.7
		total = total + dégat
		
		
		
		
		
		If sim.runeforge.MHRazorice Then sim.runeforge.applyRazorice()
		If TalentUnholy.Necrosis > 0 Then
			Nec = sim.Necrosis.Apply(dégat, T)
		End If
		RNG = sim.RandomNumberGenerator.RNGWhiteHit * 100
		If RNG <= 10 * TalentUnholy.BloodCakedBlade Then
			BCB = sim.BloodCakedBlade.ApplyDamage(T,true)
		End If
		sim.TryOnMHHitProc
		sim.proc.TryMHKillingMachine
		
		If sim.proc.ScentOfBloodProc > 0 Then
			sim.proc.ScentOfBloodProc  = sim.proc.ScentOfBloodProc  -1
			Sim.RunicPower.add(5)
		End If
		
		
		return true
	End Function
	Overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = sim.MainStat.MHBaseDamage
		tmp = tmp * sim.MainStat.WhiteHitDamageMultiplier(T)
		AvrgNonCrit = tmp
	End Function
	Overrides Function CritCoef() As Double
		CritCoef = 1* (1+0.06*sim.mainstat.CSD)
		
	End Function
	Overrides Function CritChance() As Double
		CritChance = sim.MainStat.critAutoattack
	End Function
	Overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function

end Class
