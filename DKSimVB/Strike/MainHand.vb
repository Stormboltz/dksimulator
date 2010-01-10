Friend Class MainHand
	Inherits Strikes.Strike



Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	
	Friend NextWhiteMainHit As long
	
	Protected Overrides sub init()
		MyBase.init()
		NextWhiteMainHit = 0
	End Sub

	Overrides Function ApplyDamage(T As long) As boolean
		Dim dégat As long
		Dim WSpeed As Single
		Dim MeleeMissChance As Single
		Dim MeleeDodgeChance As Single
		Dim MeleeGlacingChance As Single
		Dim MeleeParryChance As Single
		Dim ChanceNotToTouch As Single
		
		WSpeed = sim.MainStat.MHWeaponSpeed
		NextWhiteMainHit = T + (WSpeed * 100) / ((1 + sim.MainStat.Haste))
		
		If sim.FrostPresence = 1 Then
			if sim.RuneStrike.trigger = true and Sim.RunicPower.Value >= 20 then
				sim.RuneStrike.ApplyDamage(T)
				return true
			End If
		End If
		
		Dim RNG As Double
		RNG = sim.RandomNumberGenerator.RNGWhiteHit
		MeleeGlacingChance = 0.25
		MeleeDodgeChance = 0.065
		If sim.FrostPresence =1 Then
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
		
		
		If math.Min(sim.mainstat.MHExpertise,MeleeDodgeChance)+ math.Min(sim.mainstat.MHExpertise,MeleeParryChance) + math.Min (sim.mainstat.Hit,MeleeMissChance) + RNG < ChanceNotToTouch Then
			MissCount = MissCount + 1
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "MH fail")
			exit function
		End If
		
		If RNG < (ChanceNotToTouch + MeleeGlacingChance) Then
			dégat = AvrgNonCrit(T)*0.7
			HitCount = HitCount + 1
			totalhit += dégat
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "MH glancing for " & dégat)
		End If
		
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance) and RNG < (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'CRIT !
			dégat = AvrgCrit(T)
			CritCount = CritCount + 1
			If sim.combatlog.LogDetails Then sim.combatlog.write(T  & vbtab &  "MH crit for " & dégat )
			sim.tryOnCrit
			
			
			totalcrit += dégat
			
		End If
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'normal hit3
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			totalhit += dégat
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "MH hit for " & dégat )
		End If
		

		total = total + dégat
		If sim.TalentUnholy.Necrosis > 0 Then sim.Necrosis.Apply(dégat, T)
		RNG = sim.RandomNumberGenerator.RNGWhiteHit * 100
		If RNG <= 10 * sim.TalentUnholy.BloodCakedBlade Then sim.BloodCakedBlade.ApplyDamage(T)
		sim.tryOnMHWhitehitProc
'		sim.Trinkets.MHRazorIce.TryMe(T)
'		sim.proc.KillingMachine.TryMe(T)
'		sim.Trinkets.MHSingedViskag.TryMe(T)
'		sim.Trinkets.MHtemperedViskag.TryMe(T)
'		sim.trinkets.MHEmpoweredDeathbringer.TryMe(T)
'		sim.trinkets.MHRagingDeathbringer.TryMe(T)
		
		If sim.proc.ScentOfBlood.IsActive  Then
			sim.proc.ScentOfBlood.Use
			Sim.RunicPower.add(10)
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
	
	Public Overrides Sub Merge()
		If sim.MainStat.DualW = false Then exit sub
		Total += sim.OffHand.Total
		TotalHit += sim.OffHand.TotalHit
		TotalCrit += sim.OffHand.TotalCrit

		MissCount = (MissCount + sim.OffHand.MissCount)
		HitCount = (HitCount + sim.OffHand.HitCount)
		CritCount = (CritCount + sim.OffHand.CritCount)
		
		sim.OffHand.Total = 0
		sim.OffHand.TotalHit = 0
		sim.OffHand.TotalCrit = 0
	End Sub

end Class
