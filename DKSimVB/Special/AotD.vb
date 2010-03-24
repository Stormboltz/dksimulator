'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 20/03/2010
' Heure: 12:20
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class AotD
	Inherits Supertype
	
	Friend NextWhiteMainHit As Long
	Friend NextClaw as Long
	Friend ActiveUntil As Long
	Friend cd As Long
	Friend GhoulDoubleHaste As Boolean
	Private MeleeMissChance As Single
	Private MeleeDodgeChance As Single
	Private MeleeGlacingChance As Single
	Private SpellMissChance As Single
	
	Friend MHWeaponDPS As Integer
	Friend MHWeaponSpeed As Double
	
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
		MHWeaponDPS = 0
		MHWeaponSpeed = 2
		isGuardian = true
	End Sub
	
	Sub Summon(T As Long)
		If cd <= T Then
			MeleeMissChance = math.Max(0.08 - Hit,0)
			MeleeDodgeChance =  math.Max(0.065 - Expertise,0)
			SpellMissChance = math.Max(0.17 - SpellHit,0)
			ActiveUntil = T + 40 * 100
			cd =  T + (10*60*100) - (120*100*sim.TalentUnholy.NightoftheDead)
			if T <=1 Then
			Else
				sim.combatlog.write(T  & vbtab &  "Summon AoTD")
				UseGCD(T)
			End If
		End If
	End Sub
	
	
	Sub PrePull(T As Long)
		MeleeMissChance = math.Max(0.08 - Hit,0)
		MeleeDodgeChance =  math.Max(0.065 - Expertise,0)
		SpellMissChance = math.Max(0.17 - SpellHit,0)
		ActiveUntil = T + 30 * 100
		cd =  T + (10*60*100) - (120*100*sim.TalentUnholy.NightoftheDead)
		sim.combatlog.write(T  & vbtab &  "Pre-Pull AoTD")
	End Sub
	
	
	
	
	sub UseGCD(T as Long)
		Sim.NextFreeGCD = T + sim.latency/10 + 400
	End Sub
	
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
		Dim dégat As integer
		Dim WSpeed As Single
		WSpeed = MHWeaponSpeed
		NextWhiteMainHit = T + (WSpeed * 100) / ((1 + Haste))
		
		Dim RNG As Double
		RNG = sim.RandomNumberGenerator.RNGPet

		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			MissCount = MissCount + 8
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "AotD fail")
			exit function
		End If
		If RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) Then
			dégat = AvrgNonCrit(T)*0.7
			total = total + dégat
			totalhit += dégat
			HitCount = HitCount + 8
		End If
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) and RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance) Then
			'CRIT !
			dégat = AvrgCrit(T)
			CritCount = CritCount + 8
			totalcrit += dégat
			total = total + dégat
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "AotD crit for " & dégat)
		End If
		If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance) Then
			'normal hit
			HitCount = HitCount + 8
			dégat = AvrgNonCrit(T)
			total = total + dégat
			totalhit += dégat
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "AotD hit for " & dégat)
		End If
		return true
	End Function
	Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = MHBaseDamage
		tmp = tmp * PhysicalDamageMultiplier(T)
		If sim.EPStat = "EP HasteEstimated" Then
			tmp = tmp*sim.MainStat.EstimatedHasteBonus
		End If
		AvrgNonCrit = tmp
	End Function
	Function CritCoef() As Double
		CritCoef = 1
	End Function
	Function CritChance() As Double
		CritChance = crit
	End Function
	Function AvrgCrit(T As long) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	Function Claw(T As Long) As Boolean
		Dim RNG As Double
		Dim dégat As Integer
		
		RNG = sim.RandomNumberGenerator.RNGPet
		If RNG < (MeleeMissChance + MeleeDodgeChance) Then
			if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Ghoul's Claw fail")
			MissCount = MissCount + 1
			Exit function
		End If
		RNG = sim.RandomNumberGenerator.RNGPet
		If RNG <= CritChance Then
			dégat = ClawAvrgCrit(T)
			CritCount = CritCount + 1
			total = total + dégat
			totalcrit += dégat
			if sim.combatlog.LogDetails then 	sim.combatlog.write(T  & vbtab &  "Ghoul's Claw for " & dégat)
		Else
			dégat = ClawAvrgNonCrit(T)
			HitCount = HitCount + 1
			total = total + dégat
			totalhit += dégat
			if sim.combatlog.LogDetails then 	sim.combatlog.write(T  & vbtab &  "Ghoul's Claw hit for " & dégat)
		End If
		NextClaw = T+450
		return true
	End Function
	Function ClawAvrgNonCrit(T As long) As integer
		Dim tmp As Double
		tmp = MHBaseDamage*8/2
		tmp = tmp * PhysicalDamageMultiplier(T)
		tmp = tmp * 1.5
		return  tmp
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
	
	
	Function BaseAP() As Integer
		Dim tmp As Integer
		tmp = 1167
		Return tmp		
	End Function
	
	Function AP() As Integer
		AP = (Strength - 331 + BaseAP)
	End Function
	function Base_Str as integer
	end function
	Function Strength As Integer
		Dim tmp As Integer
		dim str as Integer
		tmp = 331
		str = sim.Character.Strength
		tmp += str/2
		tmp += str * (sim.talentunholy.ravenousdead*0.2)
		return tmp
	end function
	Function crit() As System.Double
		Dim tmp As Double
		tmp = 5  'BaseCrit
		tmp = tmp + 5 *  sim.Buff.MeleeCrit
		tmp = tmp + 3 *  sim.Buff.CritChanceTaken
		crit = tmp / 100
	End Function
	Function SpellCrit() As Single
		Dim tmp As Double
		tmp = tmp + 3 *  sim.Buff.CritChanceTaken
		tmp = tmp + 5 *  sim.Buff.SpellCrit
		tmp = tmp + 5  *  sim.Buff.SpellCritTaken
		SpellCrit = tmp / 100
	End Function
	
	
	Function SpellHaste() As Double
		Dim tmp As Double
		If sim.UnholyPresence = 1 Then
			SpellHaste = 0.5
		Else
			tmp = sim.Character.SpellHasteRating / 25.22 / 100
			tmp = tmp + 0.05 *  sim.Buff.SpellHaste
			tmp = tmp + 0.03 *  sim.Buff.Haste
			SpellHaste = tmp
		End If
	End Function
	Function Expertise() As Double
		Dim tmp As Double
		tmp =  sim.mainstat.Hit
		tmp = tmp * 214 / 32.79
		
		return tmp 
	End Function
	
	Function Hit() As Double
		Dim tmp As Double
		tmp = sim.mainstat.Hit
		return tmp 
	End Function
	
	Function SpellHit() As Double
		'Dim tmp As Double
		return sim.mainstat.spellHit
	End Function
	
	Function MHBaseDamage() As Double
		Dim tmp As Double
		tmp = (MHWeaponDPS + (AP / 14)) * MHWeaponSpeed
		
		return tmp
	End Function

	Function ArmorPen As Double
		Dim tmp As Double
		'tmp = character.ArmorPenetrationRating/15.39
		'tmp = tmp *1.25
		
		return tmp
	End Function
	
	Function ArmorMitigation() As Double
		Dim tmp As Double
		tmp = sim.MainStat.BossArmor
		tmp = tmp * (1- 20 *  sim.Buff.ArmorMajor / 100)
		tmp = tmp * (1- 5 *  sim.Buff.ArmorMinor / 100)
		tmp = tmp * (1 - ArmorPen / 100)
		tmp = (tmp /((467.5*83)+tmp-22167.5))
		
		Return tmp
	End Function
	
	Function PhysicalDamageMultiplier(T as long) As Double
		dim tmp as Double
		tmp = 1
		tmp = tmp * (1 - ArmorMitigation)
		tmp = tmp * (1 + 0.03 *  sim.Buff.PcDamage)
		tmp = tmp * (1 + 0.02 *  sim.Buff.PhysicalVuln)
		if sim.Character.Orc then tmp = tmp * 1.05
		return tmp
	End Function
	
	Function MagicalDamageMultiplier(T as long) As Double
		Dim tmp As Double
		tmp = 1
		tmp = tmp * (1 + 0.03 *  sim.Buff.PcDamage)
		tmp = tmp * (1 + 0.13 *  sim.Buff.SpellDamageTaken)
		
		Return tmp
	End Function
	
	
	
	
End Class
