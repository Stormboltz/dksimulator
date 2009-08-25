Friend module Ghoul
	
	Friend NextWhiteMainHit As Long
	Friend NextClaw as Long
	Friend total As Long
	Friend ActiveUntil As Long
	Friend cd As Long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	Friend FrenzyCd As Long
	Friend FrenzyUntil as Long
	Friend GhoulDoubleHaste As Boolean
	Friend TotalHit As Long
	Friend TotalCrit As Long
	Private MeleeMissChance As Single
	Private MeleeDodgeChance As Single
	Private MeleeGlacingChance As Single
	private SpellMissChance as Single

	Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		cd = 0
		ActiveUntil= 0
		NextWhiteMainHit = 0
		NextClaw = 0
		FrenzyCd = 0
		FrenzyUntil = 0
		TotalHit = 0
		TotalCrit = 0
	End Sub
	
	Sub Summon(T As Long)
		If cd <= T Then
			MeleeGlacingChance = 0.25
			MeleeMissChance = 0.08 - GhoulStat.Hit
			If MeleeMissChance < 0 Then MeleeMissChance = 0
			MeleeDodgeChance =  0.065 - GhoulStat.Expertise
			If MeleeDodgeChance < 0 Then MeleeDodgeChance = 0
			SpellMissChance = 0.17 - GhoulStat.SpellHit
			If SpellMissChance  < 0 Then SpellMissChance = 0 
			If TalentUnholy.MasterOfGhouls Then 
				ActiveUntil = sim.MaxTime
				cd = sim.MaxTime
			Else
				ActiveUntil = T + 60 * 100
				cd = ActiveUntil + (3*60*100) - (45*100*NightoftheDead)
			End If
			
			If MainStat.UnholyPresence Then
				Sim.NextFreeGCD = T + 100+ sim._MainFrm.txtLatency.Text/10
			Else
				Sim.NextFreeGCD = T + 150+ sim._MainFrm.txtLatency.Text/10
			End If
		End If
	End Sub
	
	Function Haste As Double
		dim tmp as Double
		tmp = MainStat.Haste
		
		If GhoulDoubleHaste Then
			tmp = tmp + 0.2 * Buff.MeleeHaste
			tmp = tmp + 0.03 * Buff.Haste
			if Bloodlust.IsActive(sim.TimeStamp) then tmp = tmp + 0.3
		End If
		return tmp
	End Function
	
	Function ApplyDamage(T As long) As boolean
		Dim retour As Double
		
		
		Dim WSpeed As Single
		WSpeed = GhoulStat.MHWeaponSpeed
		If FrenzyUntil >= T Then
			NextWhiteMainHit = T + (WSpeed * 100) / ((1 + Haste + 0.25))
		Else
			NextWhiteMainHit = T + (WSpeed * 100) / ((1 + Haste))
		End If
		Dim RNG As Double
		RNG = RNGPet		

		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			MissCount = MissCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Ghoul fail")
			exit function
		End If
		If RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) Then
			retour = AvrgNonCrit(T)*0.7
			total = total + retour
			HitCount = HitCount + 1
		End If
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) and RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance) Then
			'CRIT !
			retour = AvrgCrit(T)
			CritCount = CritCount + 1
			total = total + retour
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Ghoul crit for " & int(AvrgCrit(T)) )
		End If
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance) Then
			'normal hit3
			HitCount = HitCount + 1
			retour = AvrgNonCrit(T)
			total = total + retour
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Ghoul hit for " & int(AvrgNonCrit(T)))
		End If
		return true
	End Function
	Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = ghoulStat.MHBaseDamage
		tmp = tmp * GhoulStat.PhysicalDamageMultiplier(T)
		AvrgNonCrit = tmp
	End Function
	Function CritCoef() As Double
		CritCoef = 1
	End Function
	Function CritChance() As Double
		CritChance = ghoulStat.crit
	End Function
	Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	Function Claw(T As Long) As Boolean
		Dim RNG As Double
		RNG = Rngpet
		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Ghoul's Claw fail")
			MissCount = MissCount + 1
			Exit function
		End If
		RNG = RNGPet
		If RNG <= CritChance Then
			CritCount = CritCount + 1
			total = total + AvrgCrit(T)
			if combatlog.LogDetails then 	combatlog.write(T  & vbtab &  "Ghoul's Claw for " & int(ClawAvrgCrit(T)) )
		Else
			HitCount = HitCount + 1
			total = total + AvrgNonCrit(T)
			if combatlog.LogDetails then 	combatlog.write(T  & vbtab &  "Ghoul's Claw hit for " & int(ClawAvrgNonCrit(T)))
		End If
		NextClaw = T+400
		return true
	End Function
	Function ClawAvrgNonCrit(T As long) As integer
		Dim tmp As Double
		tmp = ghoulStat.MHBaseDamage
		tmp = tmp * GhoulStat.PhysicalDamageMultiplier(T)
		tmp = tmp * 1.5
		return  tmp
	End Function
	Function ClawAvrgCrit(T As long) As integer
		return  AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
	Function report As String
		dim tmp as String
		tmp = "Ghoul" & VBtab
		
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
	
	Function IsFrenzyAvailable(T As Long) As Boolean
		if TalentUnholy.GhoulFrenzy = 0 then return false
		If FrenzyCd < T  And runes.Unholy(T) Then Return True
	End Function
	Function IsAutoFrenzyAvailable(T As Long) As Boolean
		if TalentUnholy.GhoulFrenzy = 0 then return false
		if FrenzyCd < T  and runes.Unholy(T)=false and runes.Blood(T)=false and BloodTap.IsAvailable(T) then return true
	End Function
	
	Function Frenzy(T As Long) As Boolean
		if BloodTap.IsAvailable(T) then BloodTap.Use(t)
		runes.UseUnholy(T,True)
		RunicPower.add(10)
		FrenzyCd = T+3000
		FrenzyUntil = T+3000
		if combatlog.LogDetails then 	combatlog.write(T  & vbtab &  "Using Ghoul Frenzy")
		return true
	End Function
	
	
end module
