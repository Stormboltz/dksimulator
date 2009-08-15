Friend module MainHand
	
	Friend _NextWhiteMainHit As integer
	Friend total As Long
	
	Friend TotalHit As Long
	Friend TotalCrit as Long

	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	
	
	Function NextWhiteMainHit As integer
		return _NextWhiteMainHit
	End Function
	
	Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		_NextWhiteMainHit = 0
		TotalHit = 0
		TotalCrit = 0
	End Sub
	
	Function ApplyDamage(T As long) As boolean
		Dim dégat As long
		Dim BCB As Double
		Dim Nec As Double
		Dim WSpeed As Single
		Dim MeleeMissChance As Single
		Dim MeleeDodgeChance As Single
		Dim MeleeGlacingChance As Single
		WSpeed = MainStat.MHWeaponSpeed
		_NextWhiteMainHit = T + (WSpeed * 100) / ((1 + MainStat.Haste))
		
		If MainStat.FrostPresence = 1 Then
			If RunicPower.Value >= 20 Then
				RuneStrike.ApplyDamage(T)
				return true
			End If
		End If
		
		
		
		
		
		Dim RNG As Double
		RNG = Rnd
		MeleeGlacingChance = 0.25
		MeleeDodgeChance = 0.065
		If mainstat.Expertise > MeleeDodgeChance Then
			MeleeDodgeChance = 0
		Else
			MeleeDodgeChance = MeleeDodgeChance-mainstat.Expertise
		End If
		If mainstat.DualW Then
			MeleeMissChance = 0.27
		Else
			MeleeMissChance = 0.08
		End If
		If mainstat.Hit > MeleeMissChance Then
			MeleeMissChance = 0
		Else
		MeleeMissChance = MeleeMissChance - mainstat.Hit	
		End If
		
		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			MissCount = MissCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "MH fail")
			exit function
		End If

		If RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) Then
			dégat = AvrgNonCrit(T)*0.7
			HitCount = HitCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "MH glancing for " & dégat)
		End If
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) and RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance) Then
			'CRIT !
			dégat = AvrgCrit(T)
			CritCount = CritCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "MH crit for " & dégat )
		End If 
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance) Then
			'normal hit3
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "MH hit for " & dégat )
		End If
		
		If Lissage Then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance-MeleeGlacingChance) + AvrgNonCrit(T)* (MeleeGlacingChance)*0.7 
		total = total + dégat

		If Talentfrost.KillingMachine > 0 Then
			RNG = Rnd
			If RNG < (Talentfrost.KillingMachine)*MainStat.MHWeaponSpeed/60 Then
				if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Killing Machine Proc")
				proc.KillingMachine  = true
			End If
		End If
		
		If MHRazorice Then applyRazorice()
		If TalentUnholy.Necrosis > 0 Then
			Nec = Necrosis.ApplyDamage(dégat, T)
		End If
		RNG = Rnd * 100
		If RNG <= 10 * TalentUnholy.BloodCakedBlade Then
			BCB = BloodCakedBlade.ApplyDamage(T,true)
		End If
		TryMHCinderglacier
		TryMHFallenCrusader
		TryMjolRune
		TryGrimToll
		
		If proc.ScentOfBloodProc > 0 Then
			proc.ScentOfBloodProc  = proc.ScentOfBloodProc  -1
			RunicPower.add(5)
		End If
		
		
		return true
	End Function
	Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = MainStat.MHBaseDamage
		tmp = tmp * MainStat.WhiteHitDamageMultiplier(T)
		AvrgNonCrit = tmp
	End Function
	Function CritCoef() As Double
		CritCoef = 1* (1+0.06*mainstat.CSD)

	End Function
	Function CritChance() As Double
		CritChance = MainStat.critAutoattack
	End Function
	Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	Function report As String
		dim tmp as String
		tmp = "Main Hand" & VBtab
	
		If total.ToString().Length < 8 Then
			tmp = tmp & total & "   " & VBtab
		Else
			tmp = tmp & total & VBtab
		End If
		tmp = tmp & toDecimal(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
end module
