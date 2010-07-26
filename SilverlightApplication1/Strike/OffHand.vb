Friend Class OffHand
	Inherits Strikes.Strike
	
	Sub New(S As sim)
		MyBase.New(s)
		HasteSensible = true
	End Sub
	Friend NextWhiteOffHit As Long

	
	Overrides Function ApplyDamage(T As long) As boolean
		Dim Nec As Double
		
		Dim WSpeed As Single
		Dim RNG As double
		WSpeed = sim.MainStat.OHWeaponSpeed
		
		
        NextWhiteOffHit = T + (WSpeed * 100) / sim.MainStat.PhysicalHaste
		
		sim.FutureEventManager.Add(NextWhiteOffHit,"OffHand")
		
		
		
		Dim MeleeMissChance As Single
		Dim MeleeDodgeChance As Single
		Dim MeleeGlacingChance As Single
		Dim MeleeParryChance As Single
		Dim ChanceNotToTouch As Single
		
		
		RNG = RngHit
		MeleeGlacingChance = 0.25
		MeleeDodgeChance = 0.065
		MeleeMissChance = 0.27
        If sim.BloodPresence = 1 Then
            MeleeParryChance = 0.14
        Else
            MeleeParryChance = 0
        End If
		
		ChanceNotToTouch = math.Max(0, MeleeMissChance - sim.mainstat.Hit) + math.Max(0, MeleeDodgeChance  - sim.mainstat.OHExpertise) + math.Max(0, MeleeParryChance - sim.mainstat.OHExpertise)
		
		If RNG < ChanceNotToTouch Then
			MissCount = MissCount + 1
			'If sim.combatlog.LogDetails Then 
				if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "OH fail")
			'End If
            Return False
		End If

		dim dégat as Integer
		If RNG < (ChanceNotToTouch + MeleeGlacingChance) Then
			'Glancing
			dégat = AvrgNonCrit(T)*0.7
			GlancingCount = GlancingCount + 1
			totalGlance += dégat
			'If sim.combatlog.LogDetails Then 
				if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "OH glancing for " & dégat)
			'End If
		End If
		
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance) and RNG < (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'CRIT !
			CritCount = CritCount + 1
			dégat = AvrgCrit(T)
			'If sim.combatlog.LogDetails Then 
				if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "OH crit for " & dégat )
			'End If
            sim.proc.tryOnCrit()
			totalcrit += dégat
		End If
		
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'normal hit3
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			'If sim.combatlog.LogDetails Then 
				if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "OH hit for " & dégat)
			'End If
			totalhit += dégat
			
		End If
		
		If sim.proc.ScentOfBlood.IsActive  Then
			sim.proc.ScentOfBlood.Use
			Sim.RunicPower.add(10)
		End If
		total = total + dégat
        If sim.Character.Talents.Talent("Necrosis").Value > 0 Then
            Nec = sim.OHNecrosis.Apply(dégat, T)
        End If
		If sim.proc.OHBloodCakedBlade.TryMe(T) Then sim.OHBloodCakedBlade.ApplyDamage(T)
        sim.proc.TryOnOHHitProc()
		return true
	End Function
	Overrides Function AvrgNonCrit(T as long,target As Targets.Target) As Double
		Dim tmp As Double
		tmp = sim.MainStat.OHBaseDamage
		tmp = tmp * sim.MainStat.WhiteHitDamageMultiplier(T)
		tmp = tmp * 0.5
        tmp = tmp * (1 + sim.Character.Talents.Talent("NervesofColdSteel").Value * 8.3333 / 100)
		If sim.EPStat = "EP HasteEstimated" Then
			tmp = tmp*sim.MainStat.EstimatedHasteBonus
		End If
		AvrgNonCrit = tmp
	End Function
	
	Overrides Function CritChance() As Double
		Dim tmp As Double
		tmp = sim.MainStat.critAutoattack
		CritChance = tmp
	End Function
	
end Class