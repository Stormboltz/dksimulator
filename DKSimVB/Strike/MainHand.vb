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
		
		WSpeed = MainStat.MHWeaponSpeed
		NextWhiteMainHit = T + (WSpeed * 100) / ((1 + MainStat.Haste))
		
		If MainStat.FrostPresence = 1 Then
			If RunicPower.Value >= 20 Then
				RuneStrike.ApplyDamage(T)
				return true
			End If
		End If
		
		Dim RNG As Double
		RNG = RNGWhiteHit
		MeleeGlacingChance = 0.25
		MeleeDodgeChance = 0.065
		If mainstat.FrostPresence =1 Then
			MeleeParryChance = 0.14
		Else
			MeleeParryChance = 0
		End If
		If mainstat.DualW Then
			MeleeMissChance = 0.27
		Else
			MeleeMissChance = 0.08
		End If
		
		ChanceNotToTouch = MeleeMissChance + MeleeDodgeChance  + MeleeParryChance
		
		
		If math.Min(mainstat.Expertise,MeleeDodgeChance)+ math.Min(mainstat.Expertise,MeleeParryChance) + math.Min (mainstat.Hit,MeleeMissChance) + RNG < ChanceNotToTouch Then
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
			TryBitterAnguish()
			TryMirror()
			TryPyrite()
			TryOldGod()
		End If
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'normal hit3
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "MH hit for " & dégat )
		End If
		
		If Lissage Then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance-MeleeGlacingChance) + AvrgNonCrit(T)* (MeleeGlacingChance)*0.7
		total = total + dégat
		
		proc.TryMHKillingMachine
		
		
		
		If MHRazorice Then applyRazorice()
		If TalentUnholy.Necrosis > 0 Then
			Nec = sim.Necrosis.Apply(dégat, T)
		End If
		RNG = RNGWhiteHit * 100
		If RNG <= 10 * TalentUnholy.BloodCakedBlade Then
			BCB = sim.BloodCakedBlade.ApplyDamage(T,true)
		End If
		TryMHCinderglacier
		TryMHFallenCrusader
		TryMjolRune
		TryGrimToll
		TryGreatness()
		TryDeathChoice()
		TryDCDeath()
		TryVictory()
		TryBandit()
		TryDarkMatter()
		TryComet()
		If proc.ScentOfBloodProc > 0 Then
			proc.ScentOfBloodProc  = proc.ScentOfBloodProc  -1
			RunicPower.add(5)
		End If
		
		
		return true
	End Function
	Overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = MainStat.MHBaseDamage
		tmp = tmp * MainStat.WhiteHitDamageMultiplier(T)
		AvrgNonCrit = tmp
	End Function
	Overrides Function CritCoef() As Double
		CritCoef = 1* (1+0.06*mainstat.CSD)
		
	End Function
	Overrides Function CritChance() As Double
		CritChance = MainStat.critAutoattack
	End Function
	Overrides Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function

end Class
