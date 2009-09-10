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
		Dim d�gat As long
		Dim BCB As Double
		Dim Nec As Double
		Dim WSpeed As Single
		Dim MeleeMissChance As Single
		Dim MeleeDodgeChance As Single
		Dim MeleeGlacingChance As Single
		Dim MeleeParryChance As Single
		Dim ChanceNotToTouch As Single
		
		WSpeed = MainStat.MHWeaponSpeed
		_NextWhiteMainHit = T + (WSpeed * 100) / ((1 + MainStat.Haste))
		
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
		
		
		
		
		Dim tmpExp As Double
		dim tmpHit as Double
		
		
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
			d�gat = AvrgNonCrit(T)*0.7
			HitCount = HitCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "MH glancing for " & d�gat)
		End If
		
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance) and RNG < (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'CRIT !
			d�gat = AvrgCrit(T)
			CritCount = CritCount + 1
			If combatlog.LogDetails Then combatlog.write(T  & vbtab &  "MH crit for " & d�gat )
			TryBitterAnguish()
			TryMirror()
			TryPyrite()
			TryOldGod()
		End If
		If RNG >= (ChanceNotToTouch + MeleeGlacingChance + CritChance) Then
			'normal hit3
			d�gat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "MH hit for " & d�gat )
		End If
		
		If Lissage Then d�gat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance-MeleeGlacingChance) + AvrgNonCrit(T)* (MeleeGlacingChance)*0.7
		total = total + d�gat
		
		
		
		If Talentfrost.KillingMachine > 0 Then
			RNG = RNGWhiteHit
			If RNG < (Talentfrost.KillingMachine)*MainStat.MHWeaponSpeed/60 Then
				if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Killing Machine Proc")
				proc.KillingMachine  = true
			End If
		End If
		
		If MHRazorice Then applyRazorice()
		If TalentUnholy.Necrosis > 0 Then
			Nec = Necrosis.ApplyDamage(d�gat, T)
		End If
		RNG = RNGWhiteHit * 100
		If RNG <= 10 * TalentUnholy.BloodCakedBlade Then
			BCB = BloodCakedBlade.ApplyDamage(T,true)
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
