Friend Class MainHand
	Inherits Strikes.Strike



Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	
	Friend NextWhiteMainHit As Long
	Friend dNextWhiteMainHit As double
	
	Protected Overrides sub init()
		MyBase.init()
		NextWhiteMainHit = 0
		HasteSensible = true
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
		
        dNextWhiteMainHit = dNextWhiteMainHit + (WSpeed * 100) / sim.MainStat.Haste
		NextWhiteMainHit = dNextWhiteMainHit
		
		
		If sim.FrostPresence = 1 Then
			if sim.RuneStrike.trigger = true and Sim.RunicPower.Check(20) then
				sim.RuneStrike.ApplyDamage(T)
				return true
			End If
		End If
		
		Dim RNG As Double
		RNG = RngHit
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
		
		ChanceNotToTouch = math.Max(0, MeleeMissChance - sim.mainstat.Hit) + math.Max(0, MeleeDodgeChance  - sim.mainstat.MHExpertise) + math.Max(0, MeleeParryChance - sim.mainstat.MHExpertise)
		
		If RNG < ChanceNotToTouch Then
			MissCount = MissCount + 1
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "MH fail")
			exit function
		End If
		
		If RNG < (ChanceNotToTouch + MeleeGlacingChance) Then
			dégat = AvrgNonCrit(T)*0.7
			GlancingCount = GlancingCount + 1
			totalGlance += dégat
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
		If sim.proc.MHBloodCakedBlade.TryMe(T) Then sim.BloodCakedBlade.ApplyDamage(T)
		sim.tryOnMHWhitehitProc
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
		If sim.EPStat = "EP HasteEstimated" Then
			tmp = tmp*sim.MainStat.EstimatedHasteBonus
		End If
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
		_Name = "Melee"
		If sim.MainStat.DualW = false Then exit sub
		Total += sim.OffHand.Total
		TotalHit += sim.OffHand.TotalHit
		TotalCrit += sim.OffHand.TotalCrit
		TotalGlance += sim.OffHand.TotalGlance

		MissCount = (MissCount + sim.OffHand.MissCount)
		HitCount = (HitCount + sim.OffHand.HitCount)
		CritCount = (CritCount + sim.OffHand.CritCount)
		GlancingCount = GlancingCount + sim.OffHand.GlancingCount
		
		
		sim.OffHand.Total = 0
		sim.OffHand.TotalHit = 0
		sim.OffHand.TotalCrit = 0
		sim.OffHand.TotalGlance = 0
	End Sub

end Class
