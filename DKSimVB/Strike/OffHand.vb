Friend Class OffHand
	Inherits Strikes.Strike
	
	friend NextWhiteOffHit as long
	
	Overrides Function ApplyDamage(T As long) As boolean
		Dim Nec As Double
		
		Dim WSpeed As Single
		Dim RNG As double
		WSpeed = MainStat.OHWeaponSpeed
		NextWhiteOffHit = T + (WSpeed * 100) / ((1 + MainStat.Haste))
		
		Dim MeleeMissChance As Single
		Dim MeleeDodgeChance As Single
		Dim MeleeGlacingChance As Single
		Dim MeleeParryChance As Single
		Dim ChanceNotToTouch As Single
		

		
		RNG = RNGWhiteHit
		MeleeGlacingChance = 0.25
		MeleeDodgeChance = 0.065
		MeleeMissChance = 0.27
		If mainstat.FrostPresence =1 Then
			MeleeParryChance = 0.14
		Else
			MeleeParryChance = 0
		End If
		
		ChanceNotToTouch = MeleeMissChance + MeleeDodgeChance  + MeleeParryChance
		
		If math.Min(mainstat.Expertise,MeleeDodgeChance)+ math.Min(mainstat.Expertise,MeleeParryChance) + math.Min (mainstat.Hit,MeleeMissChance) + RNG < ChanceNotToTouch Then
			MissCount = MissCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "OH fail")
			exit function
		End If

		dim dégat as Integer
		If RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) Then
			'Glancing
			dégat = AvrgNonCrit(T)*0.7
			HitCount = HitCount + 1
		End If
		
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance) and RNG < (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'CRIT !
			CritCount = CritCount + 1
			dégat = AvrgCrit(T)
			
			If combatlog.LogDetails Then combatlog.write(T  & vbtab &  "OH crit for " & dégat )
			TryBitterAnguish()
			TryMirror()
			TryPyrite()
			TryOldGod()
			
		End If
		
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'normal hit3
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "OH hit for " & dégat)
		End If
		
		If Lissage Then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance-MeleeGlacingChance) + AvrgNonCrit(T)* (MeleeGlacingChance)*0.7
		total = total + dégat

		If TalentUnholy.Necrosis > 0 Then
			Nec = sim.Necrosis.Apply(dégat, T)
		End If

		RNG = RNGWhiteHit * 100
		If RNG <= 10 * TalentUnholy.BloodCakedBlade Then
			sim.BloodCakedBlade.ApplyDamage(T,false)
		End If
		
		
		If OHRazorice and mainstat.DualW Then applyRazorice()
		TryOHCinderglacier
		TryOHBerserking
		TryOHFallenCrusader
		TryMjolRune
		TryGrimToll
		TryGreatness()
		TryDeathChoice()
		TryDCDeath()
		TryVictory()
		TryBandit()
		TryDarkMatter()
		TryComet()
		return true
		'   'Debug.Print T & vbTab & "WhiteOH for " & Range("Abilities!N19").Value
	End Function
	Overrides Function AvrgNonCrit(T as long) As Double
		Dim tmp As Double
		tmp = MainStat.OHBaseDamage
		tmp = tmp * MainStat.WhiteHitDamageMultiplier(T)
		tmp = tmp * 0.5
		tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
		AvrgNonCrit = tmp
	End Function
	Overrides Function CritCoef() As Double
		CritCoef = 1
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	Overrides Function CritChance() As Double
		Dim tmp As Double
		tmp = MainStat.critAutoattack
		CritChance = tmp
	End Function
	Overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
end Class