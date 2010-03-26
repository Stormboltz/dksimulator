Friend Class OffHand
	Inherits Strikes.Strike
	
	Sub New(S As sim)
		MyBase.New(s)
		HasteSensible = true
	End Sub
	Friend NextWhiteOffHit As Long
	private dNextWhiteOffHit as Double
	
	Overrides Function ApplyDamage(T As long) As boolean
		Dim Nec As Double
		
		Dim WSpeed As Single
		Dim RNG As double
		WSpeed = sim.MainStat.OHWeaponSpeed
		Dim prvWhiteOffHit As Long
		prvWhiteOffHit = NextWhiteOffHit
		
        dNextWhiteOffHit = dNextWhiteOffHit + (WSpeed * 100) / sim.MainStat.Haste
		NextWhiteOffHit = dNextWhiteOffHit

		
		
		
		Dim MeleeMissChance As Single
		Dim MeleeDodgeChance As Single
		Dim MeleeGlacingChance As Single
		Dim MeleeParryChance As Single
		Dim ChanceNotToTouch As Single
		

		
		RNG = sim.RandomNumberGenerator.RNGWhiteHit
		MeleeGlacingChance = 0.25
		MeleeDodgeChance = 0.065
		MeleeMissChance = 0.27
		If sim.FrostPresence =1 Then
			MeleeParryChance = 0.14
		Else
			MeleeParryChance = 0
		End If
		
		ChanceNotToTouch = math.Max(0, MeleeMissChance - sim.mainstat.Hit) + math.Max(0, MeleeDodgeChance  - sim.mainstat.OHExpertise) + math.Max(0, MeleeParryChance - sim.mainstat.OHExpertise)
		
		If RNG < ChanceNotToTouch Then
			MissCount = MissCount + 1
			'If sim.combatlog.LogDetails Then 
				sim.combatlog.write(T  & vbtab &  "OH fail")
			'End If
			exit function
		End If

		dim dégat as Integer
		If RNG < (ChanceNotToTouch + MeleeGlacingChance) Then
			'Glancing
			dégat = AvrgNonCrit(T)*0.7
			GlancingCount = GlancingCount + 1
			totalGlance += dégat
			'If sim.combatlog.LogDetails Then 
				sim.combatlog.write(T  & vbtab &  "OH glancing for " & dégat)
			'End If
		End If
		
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance) and RNG < (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'CRIT !
			CritCount = CritCount + 1
			dégat = AvrgCrit(T)
			'If sim.combatlog.LogDetails Then 
				sim.combatlog.write(T  & vbtab &  "OH crit for " & dégat )
			'End If
			sim.tryOnCrit
			totalcrit += dégat
		End If
		
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'normal hit3
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			'If sim.combatlog.LogDetails Then 
				sim.combatlog.write(T  & vbtab &  "OH hit for " & dégat)
			'End If
			totalhit += dégat
			
		End If
		
		If sim.proc.ScentOfBlood.IsActive  Then
			sim.proc.ScentOfBlood.Use
			Sim.RunicPower.add(10)
		End If
		total = total + dégat
		If sim.TalentUnholy.Necrosis > 0 Then
			Nec = sim.Necrosis.Apply(dégat, T)
		End If
		RNG = sim.RandomNumberGenerator.RNGWhiteHit * 100
		If RNG <= 10 * sim.TalentUnholy.BloodCakedBlade Then sim.OHBloodCakedBlade.ApplyDamage(T)
		sim.TryOnOHHitProc
		return true
	End Function
	Overrides Function AvrgNonCrit(T as long) As Double
		Dim tmp As Double
		tmp = sim.MainStat.OHBaseDamage
		tmp = tmp * sim.MainStat.WhiteHitDamageMultiplier(T)
		tmp = tmp * 0.5
		tmp = tmp * (1 + sim.TalentFrost.NervesofColdSteel * 8.3333 / 100)
		If sim.EPStat = "EP HasteEstimated" Then
			tmp = tmp*sim.MainStat.EstimatedHasteBonus
		End If
		AvrgNonCrit = tmp
	End Function
	Overrides Function CritCoef() As Double
		CritCoef = 1
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	Overrides Function CritChance() As Double
		Dim tmp As Double
		tmp = sim.MainStat.critAutoattack
		CritChance = tmp
	End Function
	Overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function	
end Class