Friend Class MainHand
	Inherits Strikes.Strike



Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	
	Friend NextWhiteMainHit As Long

	
	Protected Overrides sub init()
		MyBase.init()
		NextWhiteMainHit = 0
		HasteSensible = true
	End Sub

	Overrides Function ApplyDamage(T As long) As boolean
		Dim d�gat As long
		Dim WSpeed As Single
		Dim MeleeMissChance As Single
		Dim MeleeDodgeChance As Single
		Dim MeleeGlacingChance As Single
		Dim MeleeParryChance As Single
		Dim ChanceNotToTouch As Single
		
		WSpeed = sim.MainStat.MHWeaponSpeed
		
        NextWhiteMainHit = T + (WSpeed * 100) / sim.MainStat.PhysicalHaste
		sim.FutureEventManager.Add(NextWhiteMainHit,"MainHand")
		
        If sim.BloodPresence = 1 Then
            If sim.RuneStrike.trigger = True And sim.RunicPower.Check(20) Then
                sim.RuneStrike.ApplyDamage(T)
                Return True
            End If
        End If
		
		Dim RNG As Double
		RNG = RngHit
		MeleeGlacingChance = 0.25
		MeleeDodgeChance = 0.065
        If sim.BloodPresence = 1 Then
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
            Return False
		End If
		
		If RNG < (ChanceNotToTouch + MeleeGlacingChance) Then
			d�gat = AvrgNonCrit(T)*0.7
			GlancingCount = GlancingCount + 1
			totalGlance += d�gat
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "MH glancing for " & d�gat)
		End If
		
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance) and RNG < (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'CRIT !
			d�gat = AvrgCrit(T)
			CritCount = CritCount + 1
			If sim.combatlog.LogDetails Then sim.combatlog.write(T  & vbtab &  "MH crit for " & d�gat )
            sim.proc.tryOnCrit()
			totalcrit += d�gat
		End If
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'normal hit3
			d�gat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			totalhit += d�gat
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "MH hit for " & d�gat )
		End If
		

		total = total + d�gat
        If sim.Character.Talents.Talent("Necrosis").Value > 0 Then sim.Necrosis.Apply(d�gat, T)
        If sim.proc.MHBloodCakedBlade.TryMe(T) Then sim.BloodCakedBlade.ApplyDamage(T)
        sim.proc.tryOnMHWhitehitProc()
        If sim.proc.ScentOfBlood.IsActive Then
            sim.proc.ScentOfBlood.Use()
            sim.RunicPower.add(10)
        End If
        Return True
    End Function
	Overrides Function AvrgNonCrit(T As long,target As Targets.Target) As Double
		Dim tmp As Double
		tmp = sim.MainStat.MHBaseDamage
		tmp = tmp * sim.MainStat.WhiteHitDamageMultiplier(T)
		If sim.EPStat = "EP HasteEstimated" Then
			tmp = tmp*sim.MainStat.EstimatedHasteBonus
		End If
		AvrgNonCrit = tmp
	End Function
	
	Overrides Function CritChance() As Double
		CritChance = sim.MainStat.critAutoattack
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
