Friend module OffHand
	
	Friend NextWhiteOffHit As long
	Friend total As long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
		Friend TotalHit As Long
	Friend TotalCrit as Long

	Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		NextWhiteOffHit = 0
		TotalHit = 0
		TotalCrit = 0

	End Sub
	
	Function ApplyDamage(T As long) As boolean
		Dim Nec As Double

		Dim WSpeed As Single
		Dim RNG As double
		WSpeed = MainStat.OHWeaponSpeed
		NextWhiteOffHit = T + (WSpeed * 100) / ((1 + MainStat.Haste))
		
		Dim MeleeMissChance As Single
		Dim MeleeDodgeChance As Single
		Dim MeleeGlacingChance As Single
		
		
		RNG = Rnd
		MeleeGlacingChance = 0.25
		MeleeDodgeChance = 0.065
		If mainstat.Expertise > MeleeDodgeChance Then
			MeleeDodgeChance = 0
		Else
			MeleeDodgeChance = MeleeDodgeChance-mainstat.Expertise
		End If
		MeleeMissChance = 0.27
		If mainstat.Hit > MeleeMissChance Then
			MeleeMissChance = 0
		Else
			MeleeMissChance = MeleeMissChance - mainstat.Hit	
		End If
		
		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			MissCount = MissCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "OH fail")
			exit function
		End If
		

		dim dégat as Integer
		If RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) Then
			dégat = AvrgNonCrit(T)*0.7
			HitCount = HitCount + 1
		End If
		
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) and RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance) Then
			'CRIT !
			CritCount = CritCount + 1
			dégat = AvrgCrit(T)
			
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "OH crit for " & dégat )
		End If 
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance) Then
			'normal hit3
			dégat = AvrgNonCrit(T)
			HitCount = HitCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "OH hit for " & dégat)
		End If
		If Lissage Then dégat = AvrgCrit(T)*CritChance + AvrgNonCrit(T)*(1-CritChance-MeleeGlacingChance) + AvrgNonCrit(T)* (MeleeGlacingChance)*0.7 
		total = total + dégat

		
		If TalentUnholy.Necrosis > 0 Then
			Nec = Necrosis.ApplyDamage(dégat, T)
		End If
		
		
		RNG = Rnd * 100
		If RNG <= 10 * TalentUnholy.BloodCakedBlade Then
			BloodCakedBlade.ApplyDamage(T,false)
		End If
		
		
		If OHRazorice Then applyRazorice()
		TryOHCinderglacier
		TryOHFallenCrusader
		TryMjolRune
		TryGrimToll
		return true
		'   'Debug.Print T & vbTab & "WhiteOH for " & Range("Abilities!N19").Value
	End Function
	Function AvrgNonCrit(T as long) As Double
		Dim tmp As Double
		tmp = MainStat.OHBaseDamage 		
		tmp = tmp * MainStat.WhiteHitDamageMultiplier(T)
		tmp = tmp * 0.5
		tmp = tmp * (1 + TalentFrost.NervesofColdSteel * 5 / 100)
		AvrgNonCrit = tmp
	End Function
	Function CritCoef() As Double
		CritCoef = 1
		CritCoef = CritCoef * (1+0.06*mainstat.CSD)
	End Function
	Function CritChance() As Double
		Dim tmp As Double
		tmp = MainStat.critAutoattack
		CritChance = tmp
	End Function
	Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	Function report As String
		dim tmp as String
		tmp = "Off Hand" & VBtab
	
		If total.ToString().Length < 8 Then
			tmp = tmp & total & "   " & VBtab
		Else
			tmp = tmp & total & VBtab
		End If
		tmp = tmp & int(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & int(HitCount+CritCount) & VBtab
		tmp = tmp & int(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
end module