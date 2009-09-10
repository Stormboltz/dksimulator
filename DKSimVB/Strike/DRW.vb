Friend module DRW
	
	Friend NextDRW As Long
	Friend total As Long
	Friend TotalHit As Long
	Friend TotalCrit as Long

	Friend ActiveUntil As Long
	Friend cd As Long
	Private Haste As Double
	Private SpellHaste As Double
	Private AP As Integer
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	Private MeleeMissChance As Single
	Private MeleeDodgeChance As Single
	Private MeleeGlacingChance As Single
	private SpellMissChance as Single
	private Hyst as Boolean
	Function IsActive(T as Long) As Boolean
		if ActiveUntil >= T then return true
	End Function
	Sub Summon(T As Long)
		
		If AreStarsAligned(T) = False Then
			'DKSIMVB.deathcoil.ApplyDamage(T,false)
			exit sub
		End If
		If Hysteria.IsAvailable(T) then Hysteria.use(T)
		If Hysteria.IsActive(T) Then
			Hyst = True
		Else
			Hyst = false
		End If
		SpellHaste = MainStat.SpellHaste
		Haste = MainStat.Haste
		AP = MainStat.AP
		If cd <= T Then
			MeleeGlacingChance = 0.25
			MeleeMissChance = 0.08 - MainStat.Hit
			If MeleeMissChance < 0 Then MeleeMissChance = 0
			MeleeDodgeChance =  MeleeMissChance * 0.065 / 0.08
			SpellMissChance = 0.17 - MainStat.SpellHit
			if SpellMissChance  < 0 then SpellMissChance = 0
			cd = T + (1.5*6000)
			RunicPower.Value = RunicPower.Value - 60
			ActiveUntil = T + 1200
			If glyph.DRW Then
				ActiveUntil = ActiveUntil  + 500
			End If
			Sim.NextFreeGCD = T + (150 / (1 + mainstat.SpellHaste))+ sim._MainFrm.txtLatency.Text/10
			NextDRW = T
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Summon DRW")
		End If
	End Sub
	Function ApplyDamage(T As long) As boolean
		NextDRW = T + (100*3.5 / (1 + Haste))
		Dim RNG As Double
		dim retour as Integer
		RNG = RNGPet

		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "DRW fail")
			MissCount = MissCount + 1
			exit function
		End If
		
		If RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) Then
			retour = AvrgNonCrit(T)*0.7
			total = total + retour
			HitCount = HitCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "DRW glancing for " & retour)
		End If
		
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) and RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + Crit) Then
			'CRIT !
			retour = AvrgCrit(T)
			total = total + retour
			CritCount = CritCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "DRW crit for " & retour)
		End If
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + Crit) Then
			'normal hit3
			retour = AvrgNonCrit(T)
			HitCount = HitCount + 1
			total = total + retour
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "DRW hit for " & retour)
		End If
		Return True
	End Function
	Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = MHBaseDamage
		tmp = tmp * PhysicalDamageMultiplier(T)
	'	tmp = tmp/2
		return tmp
	End Function
	Function CritCoef() As Double
		CritCoef = 1
	End Function
	Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	Function PhysicalDamageMultiplier(T as long) As Double
		dim tmp as Double
		tmp = 1
		tmp = tmp * (1 - ArmorMitigation)
		tmp = tmp * (1 + 0.03 * Buff.PcDamage)
		tmp = tmp * (1 + 0.02 * Buff.PhysicalVuln)
		if Hyst then tmp = tmp * (1 + 0.2)
		return tmp
	End Function
	Function ArmorMitigation() As Double
		Dim tmp As Double
		tmp = BossArmor
		tmp = tmp * (1- 20 * buff.ArmorMajor / 100)
		tmp = tmp * (1- 5 * buff.ArmorMinor / 100)
		tmp = (tmp /((467.5*83)+tmp-22167.5))
		Return tmp
	End Function
	Function MagicalDamageMultiplier(T as long) As Double
		Dim tmp As Double
		tmp = 1
		tmp = tmp * (1 + 0.03 * Buff.PcDamage)
		tmp = tmp * (1 + 0.13 * Buff.SpellDamageTaken)
		return tmp
	End Function
	Function crit() As System.Double
		Dim tmp As Double
		tmp = 20  'BaseCrit
		'tmp = tmp + Character.CritRating / 45.91
		'tmp = tmp + Character.Agility / 62.5
		tmp = tmp + 5 * Buff.MeleeCrit
		tmp = tmp + 3 * Buff.CritChanceTaken
		crit = tmp / 100
	End Function
	Function SpellCrit() As Single
		Dim tmp As Double
		tmp = 20
		'tmp = Character.SpellCritRating / 45.91
		tmp = tmp + 3 * Buff.CritChanceTaken
		tmp = tmp + 5 * Buff.SpellCrit
		tmp = tmp + 5  * Buff.SpellCritTaken
		SpellCrit = tmp / 100
	End Function
	Function Hit() As Double
		Dim tmp As Double
		tmp = MainStat.Hit
		return tmp
	End Function
	Function SpellHit() As Double
		Dim tmp As Double
		tmp = MainStat.SpellHit
		return tmp
	End Function
	Function MHBaseDamage() As Double
		Dim tmp As Double
		tmp = (mainstat.MHWeaponDPS + (AP / 14)) * 3.5
		
		return tmp
	End Function
	Function NormalisedMHDamage() As Double
		Dim tmp As Double
		tmp =  mainstat.MHWeaponSpeed * 3.5
		tmp =  tmp + 3.3*(AP / 14)
		
		return tmp
	End Function
	Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		cd = 0
		ActiveUntil= 0
		NextDRW = 0
		TotalHit = 0
		TotalCrit = 0

		
	End Sub
	Function report As String
		Dim tmp As String
		tmp = "Dancing Rune Weapon" & VBtab
		
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
	Sub Obliterate
		Dim RNG As Double
		dim damage as Integer
		RNG = RNGPet
		
		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "DRW Obliterate fail")
			MissCount = MissCount + 1
			exit sub
		End If
		RNG = RNGPet
		damage = NormalisedMHDamage * 0.8 + 467.2
		damage = damage * PhysicalDamageMultiplier(sim.TimeStamp)
		damage = damage * (1 + 0.125 * Sim.NumDesease)
		damage = damage /2
		If RNG < crit Then
			damage = damage* 2
			CritCount = CritCount +1
			if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "DRW Obliterate crit for " & damage)
		Else
			hitcount = hitcount + 1
			if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "DRW Obliterate hit for " & damage)
		End If
		total= total+damage
	End Sub
	Sub DeathStrike
		Dim RNG As Double
		dim damage as Integer
		RNG = RNGPet
		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "DRW Death Strike fail")
			MissCount = MissCount + 1
			exit sub
		End If
		RNG = RNGPet
		damage = NormalisedMHDamage*0.75 + 222.75
		damage = damage * PhysicalDamageMultiplier(sim.TimeStamp)
		'damage = damage /2
		If RNG < crit Then
			damage = damage* 2
			CritCount = CritCount +1
			if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "DRW Death Strike crit for " & damage)
		Else
			hitcount = hitcount + 1
			if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "DRW Death Strike hit for " & damage)
		End If
		total= total+damage
	End Sub
	Sub HeartStrike
		
		Dim RNG As Double
		dim damage as Integer
		RNG = RNGPet
		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "DRW Heart Strike fail")
			MissCount = MissCount + 1
			exit sub
		End If
		RNG = RNGPet
		damage = NormalisedMHDamage * 0.5 + 368
		damage = damage * PhysicalDamageMultiplier(sim.TimeStamp)
		damage = damage * (1 + 0.1 * Sim.NumDesease)
		'damage = damage /2
		If RNG < crit Then
			damage = damage* 2
			CritCount = CritCount +1
			if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "DRW Heart Strike crit for " & damage)
		Else
			hitcount = hitcount + 1
			if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "DRW Heart Strike hit for " & damage)
		End If
		total= total+damage
	End Sub
	Sub DeathCoil
		
		Dim RNG As Double
		Dim damage As Integer
		RNG = RNGPet
	
		If RNG < SpellMissChance Then
			MissCount = MissCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "DRW Death Coil fail")
			exit sub
		end if
		RNG = RNGPet
		
		damage = 0.15 * AP + 443
		damage = damage * MagicalDamageMultiplier(sim.TimeStamp)
		damage = damage /2
		
		If RNG <= drw.SpellCrit Then
			damage = damage * 2
			CritCount = CritCount +1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "DRW Death Coil crit for " & damage )
		Else
			hitcount = hitcount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "DRW Death Coil hit for " & damage)
		End If
		total = total + damage
	End Sub
	Sub PlagueStrike
		Dim RNG As Double
		dim damage as Integer
		RNG = RNGPet
		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "DRW Plague Strike fail")
			MissCount = MissCount + 1
			exit sub
		End If
		RNG = RNGPet
		damage = NormalisedMHDamage * 0.5 + 189
		damage = damage * PhysicalDamageMultiplier(sim.TimeStamp)
		'damage = damage /2
		If RNG < crit Then
			damage = damage* 2
			CritCount = CritCount +1
			if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "DRW Plague Strike crit for " & damage)
		Else
			hitcount = hitcount + 1
			if combatlog.LogDetails then combatlog.write(sim.TimeStamp  & vbtab &  "DRW Plague Strike hit for " & damage)
		End If
		total= total+damage
	End Sub
	Sub IcyTouch
		
		Dim RNG As Double
		Dim damage As Integer
		RNG = RNGPet

		If RNG < SpellMissChance Then
			MissCount = MissCount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "DRW Icy Touch fail")
			exit sub
		end if
		RNG = RNGPet
		
		
		damage = 0.1 * AP + 236
		damage = damage * MagicalDamageMultiplier(sim.TimeStamp)
		damage = damage /2
		If RNG <= drw.SpellCrit Then
			damage = damage * 2
			CritCount = CritCount +1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "DRW Icy Touch crit for " & damage )
		Else
			hitcount = hitcount + 1
			if combatlog.LogDetails then combatlog.write(T  & vbtab &  "DRW Icy Touch hit for " & damage)
		End If
		total = total + damage
		
		
		
	End Sub
	
	Function T As Long
		return sim.TimeStamp
	End Function
	
	
end module
