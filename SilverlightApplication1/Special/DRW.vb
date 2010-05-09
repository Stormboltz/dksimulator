Friend Class DRW
	Inherits Supertype
	
	Friend NextDRW As Long
	
	Friend ActiveUntil As Long
	Friend cd As Long
	Private Haste As Double
	Private SpellHaste As Double
	Private AP As Integer
	Private _Crit As Double
	Private _SpellCrit as Double
	
	Private MeleeMissChance As Single
	Private MeleeDodgeChance As Single
	Private MeleeGlacingChance As Single
	private SpellMissChance as Single
	Private Hyst As Boolean
	
	
	Friend PlaqueStrike As Strikes.Strike
	Friend Obliterate As Strikes.Strike
	Friend HeartStrike As Strikes.Strike
	Friend DeathStrike As Strikes.Strike
	Friend DeathCoil As Spells.Spell
	Friend IcyTouch as Spells.Spell
	
	
	Sub New(S As Sim)
		_Name = "DRW: Melee"
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		cd = 0
		ActiveUntil= 0
		NextDRW = 0
		TotalHit = 0
		TotalCrit = 0
		Sim = S
		sim.DamagingObject.Add(Me)
		ThreadMultiplicator = 1
		HasteSensible = True
		isGuardian = True
		
		PlaqueStrike = New Strikes.Strike(sim)
		PlaqueStrike._Name = "DRW: Plaque Strike"
		
		Obliterate = New Strikes.Strike(sim)
		Obliterate._Name = "DRW: Obliterate"
		
		HeartStrike = New Strikes.Strike(sim)
		HeartStrike._Name = "DRW: Heart Strike"
		
		DeathStrike = New Strikes.Strike(sim)
		DeathStrike._Name = "DRW: Death Strike"
		
		DeathCoil = New Spells.Spell(sim)
		DeathCoil._Name = "DRW: Death Coil"
		
		IcyTouch = New Spells.Spell(sim)
		IcyTouch._Name = "DRW: Icy Touch"
		
		
	End Sub
	
	Function IsActive(T as Long) As Boolean
		if ActiveUntil >= T then return true
	End Function
	function Summon(T As Long) as boolean
		If sim.runeforge.AreStarsAligned(T) = False Then
			'DKSIMVB.deathcoil.ApplyDamage(T,false)
			return false
		End If
		If cd <= T Then
			If sim.Hysteria.IsAvailable(T) then sim.Hysteria.use(T)
			If sim.Hysteria.IsActive(T) Then
				Hyst = True
			Else
				Hyst = false
			End If
			SpellHaste = sim.MainStat.SpellHaste
			Haste = sim.MainStat.Haste
			AP = sim.MainStat.AP
			_Crit = sim.MainStat.critAutoattack ' Crit seems based on charater crit
			_SpellCrit = sim.MainStat.SpellCrit '
			MeleeGlacingChance = 0.25
			MeleeMissChance = 0.08 - sim.MainStat.Hit
			If MeleeMissChance < 0 Then MeleeMissChance = 0
			MeleeDodgeChance =  MeleeMissChance * 0.065 / 0.08
			SpellMissChance = 0.17 - sim.MainStat.SpellHit
			if SpellMissChance  < 0 then SpellMissChance = 0
			cd = T + (1.5*6000)
			Sim.RunicPower.Use(60)
			ActiveUntil = T + 1200
			If sim.character.glyph.DRW Then
				ActiveUntil = ActiveUntil  + 500
			End If
			UseGCD(T)
			NextDRW = T
			sim.FutureEventManager.Add(NextDRW,"DRW")
			sim.combatlog.write(T  & vbtab &  "Summon DRW")
			return true
		End If
	End Function
	Function ApplyDamage(T As long) As boolean
		NextDRW = T + (100 * 3.5 / Haste)
		sim.FutureEventManager.Add(NextDRW,"DRW")
		
		Dim RNG As Double
		dim retour as Integer
		RNG = RngHit
		
		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "DRW fail")
			MissCount = MissCount + 1
			exit function
		End If
		
		If RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) Then
			retour = AvrgNonCrit(T)*0.7
			total = total + retour
			TotalGlance += retour
			GlancingCount = GlancingCount + 1
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "DRW glancing for " & retour)
		End If
		
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) and RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + Crit) Then
			'CRIT !
			retour = AvrgCrit(T)
			total = total + retour
			TotalCrit += retour
			CritCount = CritCount + 1
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "DRW crit for " & retour)
		End If
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + Crit) Then
			'normal hit3
			retour = AvrgNonCrit(T)
			HitCount = HitCount + 1
			total = total + retour
			TotalHit += retour
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "DRW hit for " & retour)
		End If
		Return True
	End Function
	Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = MHBaseDamage
		tmp = tmp * PhysicalDamageMultiplier(T)
		If sim.EPStat = "EP HasteEstimated" Then
			tmp = tmp*sim.MainStat.EstimatedHasteBonus
		End If
		'	tmp = tmp/2
		return tmp
	End Function
	Function CritCoef() As Double
		CritCoef = 1
	End Function
	Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	sub UseGCD(T as Long)
		Sim.UseGCD(T, True)
	End Sub
	Function PhysicalDamageMultiplier(T As Long,Optional target As Targets.Target = Nothing) As Double
		if target is nothing then target = sim.Targets.MainTarget
		dim tmp as Double
		tmp = 1
		tmp = tmp * getMitigation
		tmp = tmp * (1 + 0.03 *  sim.Character.Buff.PcDamage)
		tmp = tmp * (1 + 0.02 *  target.Debuff.PhysicalVuln)
		if Hyst then tmp = tmp * (1 + 0.2)
		return tmp
	End Function
	
	
	Function getMitigation(Optional target As Targets.Target = Nothing) As Double
		if target is nothing then target = sim.Targets.MainTarget
		Dim AttackerLevel As Integer = 80
		Dim tmpArmor As Integer
		Dim ArPDebuffs As Double
		dim l_sunder as double = 1.0
		Dim l_ff  As Double = 1.0
		dim _Mitigation as Double
		
		
		if target.Debuff.ArmorMajor > 0 then l_sunder = 1- 0.20
		If target.Debuff.ArmorMinor > 0 Then l_ff = 1 - 0.05
		ArPDebuffs = (l_sunder * l_ff)
		Dim ArmorConstant As Double = 400 + (85 * 80) + 4.5 * 85 * (80 - 59)
		
		tmpArmor = sim.MainStat.BossArmor  *  ArPDebuffs
		dim ArPCap as Double = Math.Min((tmpArmor + ArmorConstant) / 3, tmpArmor)
		tmpArmor = tmpArmor -  ArPCap * Math.Min(1,0)
		_Mitigation = ArmorConstant / (ArmorConstant + tmpArmor)
		return _Mitigation
	end function
	
	Function MagicalDamageMultiplier(T As Long,Optional target As Targets.Target = Nothing) As Double
		if target is nothing then target = sim.Targets.MainTarget
		Dim tmp As Double
		tmp = 1
		tmp = tmp * (1 + 0.03 *  sim.Character.Buff.PcDamage)
		tmp = tmp * (1 + 0.13 *  target.Debuff.SpellDamageTaken)
		return tmp
	End Function
	Function crit() As System.Double
		return _crit
'		Dim tmp As Double
'		tmp = 20  'BaseCrit
'		tmp = tmp + 5 *  sim.Buff.MeleeCrit
'		tmp = tmp + 3 *  sim.Buff.CritChanceTaken
'		crit = tmp / 100
	End Function
	Function SpellCrit() As Single
		return _Spellcrit
'		Dim tmp As Double
'		tmp = 20
'		tmp = tmp + 3 *  sim.Buff.CritChanceTaken
'		tmp = tmp + 5 *  sim.Buff.SpellCrit
'		tmp = tmp + 5  *  sim.Buff.SpellCritTaken
'		SpellCrit = tmp / 100
	End Function
	Function Hit() As Double
		Dim tmp As Double
		tmp = sim.MainStat.Hit
		return tmp
	End Function
	Function SpellHit() As Double
		Dim tmp As Double
		tmp = sim.MainStat.SpellHit
		return tmp
	End Function
	Function MHBaseDamage() As Double
		Dim tmp As Double
		tmp = (sim.mainstat.MHWeaponDPS + (AP / 14)) * 3.5
		return tmp
	End Function
	Function NormalisedMHDamage() As Double
		Dim tmp As Double
		tmp =  sim.mainstat.MHWeaponSpeed * 3.5
		tmp =  tmp + 3.3*(AP / 14)
		return tmp
	End Function
	
	
	Sub DRWObliterate
		Dim RNG As Double
		dim damage as Integer
		RNG = Obliterate.RngHit
		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DRW Obliterate fail")
			Obliterate.MissCount += 1
			exit sub
		End If
		damage = NormalisedMHDamage * 0.8 + 467.2
		damage = damage * PhysicalDamageMultiplier(sim.TimeStamp)
		damage = damage * (1 + 0.125 * Sim.Targets.MainTarget.NumDesease)
		damage = damage /2
		
		RNG = Obliterate.RngCrit
		If RngCrit < crit Then
			damage = damage* 2
			Obliterate.CritCount += 1
			Obliterate.TotalCrit += damage
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DRW Obliterate crit for " & damage)
		Else
			Obliterate.hitcount += 1
			Obliterate.Totalhit += damage
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DRW Obliterate hit for " & damage)
		End If
		Obliterate.total= Obliterate.total+damage
	End Sub
	Sub DRWDeathStrike
		Dim RNG As Double
		dim damage as Integer
		RNG = DeathStrike.RngHit
		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DRW Death Strike fail")
			DeathStrike.MissCount = DeathStrike.MissCount + 1
			exit sub
		End If
		RNG = DeathStrike.RngCrit
		damage = NormalisedMHDamage*0.75 + 222.75
		damage = damage * PhysicalDamageMultiplier(sim.TimeStamp)
		'damage = damage /2
		If RNG < crit Then
			damage = damage* 2
			DeathStrike.CritCount = DeathStrike.CritCount +1
			DeathStrike.TotalCrit += damage
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DRW Death Strike crit for " & damage)
		Else
			DeathStrike.hitcount = DeathStrike.hitcount + 1
			DeathStrike.Totalhit += damage
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DRW Death Strike hit for " & damage)
		End If
		DeathStrike.total= DeathStrike.total+damage
	End Sub
	Sub DRWHeartStrike
		
		Dim RNG As Double
		Dim damage As Integer
		
		RNG = HeartStrike.RngHit
		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DRW Heart Strike fail")
			HeartStrike.MissCount = HeartStrike.MissCount + 1
			exit sub
		End If
		
		Dim intCount As Integer
		Dim t As Targets.Target
		intCount = 0
		For Each T In sim.Targets.AllTargets
				RNG = HeartStrike.RngCrit
				damage = NormalisedMHDamage * 0.5 + 368
				damage = damage * PhysicalDamageMultiplier(sim.TimeStamp)
				damage = damage * (1 + 0.1 * T.NumDesease)
				If RNG < crit Then
					damage = damage* 2
					HeartStrike.CritCount = HeartStrike.CritCount +1
					HeartStrike.TotalCrit += damage
					if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DRW Heart Strike crit for " & damage)
				Else
					HeartStrike.hitcount = HeartStrike.hitcount + 1
					HeartStrike.Totalhit += damage
					if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DRW Heart Strike hit for " & damage)
				End If
				
				If T.Equals(sim.Targets.MainTarget) Then
					HeartStrike.total= HeartStrike.total+damage
				ElseIf intCount =0 Then
					damage = damage * 0.5
					HeartStrike.total= HeartStrike.total+damage
					intCount += 1
				End If
		Next
		
	End Sub
	Sub DRWDeathCoil
		Dim RNG As Double
		Dim damage As Integer
		RNG = DeathCoil.RngHit
		
		If RNG < SpellMissChance Then
			DeathCoil.MissCount = DeathCoil.MissCount + 1
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "DRW Death Coil fail")
			exit sub
		end if
		RNG = DeathCoil.RngCrit
		
		damage = 0.15 * AP + 443
		damage = damage * MagicalDamageMultiplier(sim.TimeStamp)
		damage = damage /2
		
		If RNG <= sim.drw.SpellCrit Then
			damage = damage * 2
			DeathCoil.CritCount = DeathCoil.CritCount +1
			DeathCoil.TotalCrit += damage
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "DRW Death Coil crit for " & damage )
		Else
			DeathCoil.hitcount = DeathCoil.hitcount + 1
			DeathCoil.Totalhit += damage
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "DRW Death Coil hit for " & damage)
		End If
		DeathCoil.total = DeathCoil.total + damage
	End Sub
	Sub DRWPlagueStrike
		Dim RNG As Double
		Dim damage As Integer
		
		RNG = PlaqueStrike.RngHit
		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DRW Plague Strike fail")
			PlaqueStrike.MissCount = PlaqueStrike.MissCount + 1
			exit sub
		End If
		RNG = PlaqueStrike.RngCrit
		damage = NormalisedMHDamage * 0.5 + 189
		damage = damage * PhysicalDamageMultiplier(sim.TimeStamp)
		'damage = damage /2
		If RNG < crit Then
			damage = damage* 2
			DeathCoil.TotalCrit += damage
			PlaqueStrike.CritCount = PlaqueStrike.CritCount +1
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DRW Plague Strike crit for " & damage)
		Else
			PlaqueStrike.hitcount = PlaqueStrike.hitcount + 1
			PlaqueStrike.Totalhit += damage
			if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp  & vbtab &  "DRW Plague Strike hit for " & damage)
		End If
		PlaqueStrike.total= PlaqueStrike.total+damage
	End Sub
	Sub DRWIcyTouch
		
		Dim RNG As Double
		Dim damage As Integer
		RNG = IcyTouch.RngHit
		
		If RNG < SpellMissChance Then
			IcyTouch.MissCount = IcyTouch.MissCount + 1
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "DRW Icy Touch fail")
			exit sub
		end if
		RNG = IcyTouch.RngCrit
		
		
		damage = 0.1 * AP + 236
		damage = damage * MagicalDamageMultiplier(sim.TimeStamp)
		damage = damage /2
		If RNG <= sim.drw.SpellCrit Then
			damage = damage * 2
			IcyTouch.CritCount = IcyTouch.CritCount +1
			IcyTouch.TotalCrit += damage
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "DRW Icy Touch crit for " & damage )
		Else
			IcyTouch.hitcount = IcyTouch.hitcount + 1
			IcyTouch.Totalhit += damage
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "DRW Icy Touch hit for " & damage)
		End If
		IcyTouch.total = IcyTouch.total + damage
		
		
		
	End Sub
	Public Sub cleanup()
		Total = 0
		HitCount = 0
		MissCount =0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
	End Sub
	Function T() As Long
		return sim.TimeStamp
	End Function
	Public Overloads Overrides Sub Merge()
		_Name = "Dancing Rune Weapon"
		total += PlaqueStrike.total + Obliterate.total + HeartStrike.total + DeathStrike.total + DeathCoil.total + IcyTouch.total
		TotalHit += PlaqueStrike.TotalHit + Obliterate.TotalHit + HeartStrike.TotalHit + DeathStrike.TotalHit + DeathCoil.TotalHit + IcyTouch.TotalHit
		TotalCrit += PlaqueStrike.TotalCrit + Obliterate.TotalCrit + HeartStrike.TotalCrit + DeathStrike.TotalCrit + DeathCoil.TotalCrit + IcyTouch.TotalCrit

		MissCount += PlaqueStrike.MissCount + Obliterate.MissCount + HeartStrike.MissCount + DeathStrike.MissCount + DeathCoil.MissCount + IcyTouch.MissCount
		HitCount += PlaqueStrike.HitCount + Obliterate.HitCount + HeartStrike.HitCount + DeathStrike.HitCount + DeathCoil.HitCount + IcyTouch.HitCount
		CritCount += PlaqueStrike.CritCount + Obliterate.CritCount + HeartStrike.CritCount + DeathStrike.CritCount + DeathCoil.CritCount + IcyTouch.CritCount
		
		PlaqueStrike.cleanup
		
		Obliterate.cleanup
		HeartStrike.cleanup
		DeathStrike.cleanup
		DeathCoil.cleanup
		IcyTouch.cleanup
		

		
		
		
	End Sub
	
end Class
