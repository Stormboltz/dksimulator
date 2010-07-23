Friend class Ghoul
	Inherits Supertype
	
	Friend NextWhiteMainHit As Long
	Friend NextClaw as Long
	Friend ActiveUntil As Long
	Friend cd As Long
	Friend _Haste As Double
	Friend _AP As Integer
	
	Private MeleeMissChance As Single
	Private MeleeDodgeChance As Single
	Private MeleeGlacingChance As Single
	Private SpellMissChance As Single
	Private Claw As Strikes.Strike
	
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
		HasteSensible = True
		Claw = New Strikes.Strike(Sim)
		Claw._Name = "Ghoul: Claw"
	End Sub
	
	Sub Summon(T As Long)
		If cd <= T Then
			sim.FutureEventManager.Add(T,"Ghoul")
			MeleeMissChance = math.Max(0.08 - sim.GhoulStat.Hit,0)
			MeleeDodgeChance =  math.Max(0.065 - sim.GhoulStat.Expertise,0)
			SpellMissChance = math.Max(0.17 - sim.GhoulStat.SpellHit,0)
            If sim.Character.Talents.Talent("MasterOfGhouls").Value Then
                ActiveUntil = sim.MaxTime
                cd = sim.MaxTime
                isGuardian = False
            Else
                _Haste = sim.MainStat.PhysicalHaste
                _AP = sim.GhoulStat.AP
                ActiveUntil = T + 60 * 100
                cd = ActiveUntil + (3 * 60 * 100) - (45 * 100 * sim.Character.Talents.Talent("NightoftheDead").Value)
                isGuardian = True
            End If
			If T <=1 Then
			Else
				sim.combatlog.write(T  & vbtab &  "Summon Ghoul")
				UseGCD(T)
			End If
		End If
	End Sub
	sub UseGCD(T as Long)
		Sim.UseGCD(T, True)
	End Sub
	
	Function NextActionTime() As Long
		return Math.Min(NextClaw, NextWhiteMainHit)
	End Function
	
	Sub TryActions(TimeStamp As Long)
		If NextWhiteMainHit <= TimeStamp Then ApplyDamage(TimeStamp)
		TryClaw(TimeStamp)
		If sim.isInGCD(TimeStamp) And Sim.Frenzy.IsAutoFrenzyAvailable(Timestamp) Then
			Sim.Frenzy.Frenzy(TimeStamp)
		End If
	End Sub
	
	Function Haste() As Double
		Dim tmp As Double
		If isGuardian Then
			return _Haste
		End If
        tmp = sim.MainStat.PhysicalHaste
        If sim.Frenzy.ActiveUntil >= sim.TimeStamp Then tmp = tmp * 1.5
		Return tmp

	End Function
	
	Function Agility() As Integer
		Dim tmp As Integer
		tmp = sim.GhoulStat.Agility
		tmp = tmp + 155 * 1.15 *  sim.Character.Buff.StrAgi
		tmp = tmp + 37 * 1.4 *  sim.Character.Buff.StatAdd
		tmp = tmp * (1 +  sim.Character.Buff.StatMulti / 10)
		return tmp
	End Function
	
	Function Strength() As Integer
		Dim tmp As Integer
		tmp = sim.GhoulStat.Strength
		tmp = tmp + 155 * 1.15 *  sim.Character.Buff.StrAgi
		tmp = tmp + 37 * 1.4 *  sim.Character.Buff.StatAdd
		tmp = tmp * (1 +  sim.Character.Buff.StatMulti / 10)
		return tmp
	End Function
	
	Function AP() As Integer
		Dim tmp As Integer
		If isGuardian Then Return _AP
		tmp = sim.GhoulStat.BaseAP + Strength() + Agility()
		return (tmp + 687 * sim.Character.Buff.AttackPower) * (1 +  sim.Character.Buff.AttackPowerPc / 10)
	End Function
	
	Function ApplyDamage(T As long) As boolean
		Dim dégat As integer
		
		
		Dim WSpeed As Single
		WSpeed = sim.GhoulStat.MHWeaponSpeed
		NextWhiteMainHit = T + sim.GhoulStat.SwingTime(Haste()) * 100
		sim.FutureEventManager.Add(NextWhiteMainHit,"Ghoul")
		Dim RNG As Double
		RNG = RngHit

		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			MissCount = MissCount + 1
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Ghoul fail")
            Return False
		End If
		If RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) Then
			dégat = AvrgNonCrit(T)*0.7
			total = total + dégat
			TotalGlance += dégat
			GlancingCount += 1
		End If
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) and RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance) Then
			'CRIT !
			dégat = AvrgCrit(T)
			CritCount = CritCount + 1
			totalcrit += dégat
			total = total + dégat
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Ghoul crit for " & dégat)
		End If
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance) Then
			'normal hit
			HitCount = HitCount + 1
			dégat = AvrgNonCrit(T)
			total = total + dégat
			totalhit += dégat
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Ghoul hit for " & dégat)
		End If
		return true
	End Function
	Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = sim.GhoulStat.MHBaseDamage(AP) * sim.GhoulStat.PhysicalDamageMultiplier(T)
		If sim.EPStat = "EP HasteEstimated" Then
			tmp = tmp*sim.MainStat.EstimatedHasteBonus
		End If
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
	Function TryClaw(T As Long) As Boolean
		Dim RNG As Double
		Dim dégat As Integer
		If NextClaw > T Then Return False
		RNG = me.Claw.RngHit
		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Ghoul's Claw fail")
			Claw.MissCount += 1
            Return False
		End If
		RNG = me.Claw.RngCrit
		If RNG <= CritChance Then
			dégat = ClawAvrgCrit(T)
			Claw.CritCount += 1
			Claw.total += dégat
			Claw.totalcrit += dégat
			if sim.combatlog.LogDetails then 	sim.combatlog.write(T  & vbtab &  "Ghoul's Claw for " & dégat)
		Else
			dégat = ClawAvrgNonCrit(T)
			Claw.HitCount += 1
			Claw.total += dégat
			Claw.totalhit += dégat
			if sim.combatlog.LogDetails then 	sim.combatlog.write(T  & vbtab &  "Ghoul's Claw hit for " & dégat)
		End If
		NextClaw = T+sim.GhoulStat.ClawTime(haste()) * 100
		sim.FutureEventManager.Add(NextClaw,"Ghoul")
		return true
	End Function
	Function ClawAvrgNonCrit(T As long) As integer
		return AvrgNonCrit(T) * 1.5
	End Function
	Function ClawAvrgCrit(T As long) As integer
		return  AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	Public Sub cleanup()
		Total = 0
		HitCount = 0
		MissCount =0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
	End Sub

    Public Overrides Function report() As ReportLine
        If isGuardian And Claw.total > 0 Then Merge() 'if we don't have a permaghoul merge in claw
        Return MyBase.Report()
    End Function

	Public Overrides Sub Merge()
		
		Total += Claw.Total
		TotalHit += Claw.TotalHit
		TotalCrit += Claw.TotalCrit

		MissCount = (MissCount + Claw.MissCount)/2
		HitCount = (HitCount + Claw.HitCount)/2
		CritCount = (CritCount + Claw.CritCount)/2
		
		Claw.Total = 0
		Claw.TotalHit = 0
		Claw.TotalCrit = 0
	End sub
	
end class
