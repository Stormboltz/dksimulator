Friend Class OffHand
	Inherits Strikes.Strike
	
	Sub New(S As sim)
		MyBase.New(s)
		HasteSensible = true
	End Sub
	friend NextWhiteOffHit as long
	
	Overrides Function ApplyDamage(T As long) As boolean
		Dim Nec As Double
		
		Dim WSpeed As Single
		Dim RNG As double
		WSpeed = sim.MainStat.OHWeaponSpeed
		NextWhiteOffHit = T + (WSpeed * 100) / ((1 + sim.MainStat.Haste))
		
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
		
		ChanceNotToTouch = MeleeMissChance + MeleeDodgeChance  + MeleeParryChance
		
		If math.Min(sim.mainstat.OHExpertise,MeleeDodgeChance)+ math.Min(sim.mainstat.OHExpertise,MeleeParryChance) + math.Min (sim.mainstat.Hit,MeleeMissChance) + RNG < ChanceNotToTouch Then
			MissCount = MissCount + 1
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "OH fail")
			exit function
		End If

		dim dégat as Integer
		If RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) Then
			'Glancing
			dégat = AvrgNonCrit(T)*0.7
			totalhit += dégat
			HitCount = HitCount + 1
		End If
		
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance) and RNG < (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'CRIT !
			CritCount = CritCount + 1
			dégat = AvrgCrit(T)
			If sim.combatlog.LogDetails Then sim.combatlog.write(T  & vbtab &  "OH crit for " & dégat )
			sim.tryOnCrit
			totalcrit += dégat
		End If
		
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'normal hit3
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			If sim.combatlog.LogDetails Then sim.combatlog.write(T  & vbtab &  "OH hit for " & dégat)
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
'		sim.Trinkets.OHRazorIce.TryMe(T)
'		sim.Trinkets.OHSingedViskag.TryMe(T)
'		sim.Trinkets.OHtemperedViskag.TryMe(T)
'		sim.trinkets.MHEmpoweredDeathbringer.TryMe(T)
'		sim.trinkets.MHRagingDeathbringer.TryMe(T)
		
		return true
		'   'Debug.Print T & vbTab & "WhiteOH for " & Range("Abilities!N19").Value
	End Function
	Overrides Function AvrgNonCrit(T as long) As Double
		Dim tmp As Double
		tmp = sim.MainStat.OHBaseDamage
		tmp = tmp * sim.MainStat.WhiteHitDamageMultiplier(T)
		tmp = tmp * 0.5
		If sim.patch Then
				tmp = tmp * (1 + sim.TalentFrost.NervesofColdSteel * 8.3333 / 100)
			Else
				tmp = tmp * (1 + sim.TalentFrost.NervesofColdSteel * 5 / 100)
			End If
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