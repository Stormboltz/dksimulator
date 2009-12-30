Friend class Ghoul
	
	Friend NextWhiteMainHit As Long
	Friend NextClaw as Long
	Public total As Long
	Friend ActiveUntil As Long
	Friend cd As Long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	Friend GhoulDoubleHaste As Boolean
	Friend TotalHit As Long
	Friend TotalCrit As Long
	Private MeleeMissChance As Single
	Private MeleeDodgeChance As Single
	Private MeleeGlacingChance As Single
	Private SpellMissChance As Single
	Public ThreadMultiplicator as Double
	protected sim As Sim
	
	Sub new(MySim as Sim)
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		cd = 0
		ActiveUntil= 0
		NextWhiteMainHit = 0
		NextClaw = 0
		TotalHit = 0
		TotalCrit = 0
		sim = MySim
		MeleeGlacingChance = 0.25
		sim.DamagingObject.Add(Me)
		ThreadMultiplicator = 0
	End Sub
	
	Sub Summon(T As Long)
		If cd <= T Then
			
			
			MeleeMissChance = math.Max(0.08 - sim.GhoulStat.Hit,0)
			'If MeleeMissChance < 0 Then MeleeMissChance = 0
			MeleeDodgeChance =  math.Max(0.065 - sim.GhoulStat.Expertise,0)
			'If MeleeDodgeChance < 0 Then MeleeDodgeChance = 0
			SpellMissChance = math.Max(0.17 - sim.GhoulStat.SpellHit,0)
			'If SpellMissChance  < 0 Then SpellMissChance = 0
			If sim.TalentUnholy.MasterOfGhouls Then
				ActiveUntil = sim.MaxTime
				cd = sim.MaxTime
			Else
				ActiveUntil = T + 60 * 100
				cd = ActiveUntil + (3*60*100) - (45*100*sim.TalentUnholy.NightoftheDead)
			End If
			If T <=1 Then
			Else
				sim.combatlog.write(T  & vbtab &  "Summon Ghoul")
				UseGCD(T)
			End If
		End If
	End Sub
	sub UseGCD(T as Long)
		Sim.NextFreeGCD = T + (150 / (1 + sim.MainStat.SpellHaste)) + sim._MainFrm.txtLatency.Text/10
	End sub
	Function Haste As Double
		dim tmp as Double
		tmp = sim.MainStat.Haste
		
		If GhoulDoubleHaste Then
			tmp = tmp + 0.2 *  sim.Buff.MeleeHaste
			tmp = tmp + 0.03 *  sim.Buff.Haste
			if sim.Bloodlust.IsActive(sim.TimeStamp) then tmp = tmp + 0.3
		End If
		return tmp
	End Function
	
	Function ApplyDamage(T As long) As boolean
		Dim d�gat As integer
		
		
		Dim WSpeed As Single
		WSpeed = sim.GhoulStat.MHWeaponSpeed
		If sim.Frenzy.ActiveUntil >= T Then
			NextWhiteMainHit = T + (WSpeed * 100) / ((1 + Haste + 0.25))
		Else
			NextWhiteMainHit = T + (WSpeed * 100) / ((1 + Haste))
		End If
		Dim RNG As Double
		RNG = sim.RandomNumberGenerator.RNGPet

		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			MissCount = MissCount + 1
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Ghoul fail")
			exit function
		End If
		If RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) Then
			d�gat = AvrgNonCrit(T)*0.7
			total = total + d�gat
			totalhit += d�gat 
			HitCount = HitCount + 1
		End If
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) and RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance) Then
			'CRIT !
			d�gat = AvrgCrit(T)
			CritCount = CritCount + 1
			totalcrit += d�gat
			total = total + d�gat
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Ghoul crit for " & d�gat)
		End If
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance) Then
			'normal hit3
			HitCount = HitCount + 1
			d�gat = AvrgNonCrit(T)
			total = total + d�gat
			totalhit += d�gat 
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Ghoul hit for " & d�gat)
		End If
		return true
	End Function
	Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = sim.ghoulStat.MHBaseDamage
		tmp = tmp * sim.GhoulStat.PhysicalDamageMultiplier(T)
		AvrgNonCrit = tmp
	End Function
	Function CritCoef() As Double
		CritCoef = 1
	End Function
	Function CritChance() As Double
		CritChance = sim.ghoulStat.crit
	End Function
	Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	Function Claw(T As Long) As Boolean
		Dim RNG As Double
		Dim d�gat As Integer
		
		RNG = sim.RandomNumberGenerator.RNGPet
		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Ghoul's Claw fail")
			MissCount = MissCount + 1
			Exit function
		End If
		RNG = sim.RandomNumberGenerator.RNGPet
		If RNG <= CritChance Then
			d�gat = ClawAvrgCrit(T)
			CritCount = CritCount + 1
			total = total + d�gat
			totalcrit += d�gat
			if sim.combatlog.LogDetails then 	sim.combatlog.write(T  & vbtab &  "Ghoul's Claw for " & d�gat)
		Else
			d�gat = ClawAvrgNonCrit(T)
			HitCount = HitCount + 1
			total = total + d�gat
			totalhit += d�gat
			if sim.combatlog.LogDetails then 	sim.combatlog.write(T  & vbtab &  "Ghoul's Claw hit for " & d�gat)
		End If
		NextClaw = T+400
		return true
	End Function
	Function ClawAvrgNonCrit(T As long) As integer
		Dim tmp As Double
		tmp = sim.ghoulStat.MHBaseDamage
		tmp = tmp * sim.GhoulStat.PhysicalDamageMultiplier(T)
		tmp = tmp * 1.5
		return  tmp
	End Function
	Function ClawAvrgCrit(T As long) As integer
		return  AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
	Function report As String
		dim tmp as String
		tmp = "Ghoul" & VBtab
		
		tmp = tmp & total & VBtab
		tmp = tmp & toDecimal(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(total/(HitCount+CritCount)) & VBtab
		
		tmp = tmp & toDecimal(HitCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(totalhit/(HitCount)) & VBtab
		
		tmp = tmp & toDecimal(CritCount) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(totalcrit/(CritCount)) & VBtab
				
		tmp = tmp & toDecimal(MissCount) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
	

	
	
end class
