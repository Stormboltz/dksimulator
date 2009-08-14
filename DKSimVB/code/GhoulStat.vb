'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 18:49
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module GhoulStat
	
	
	Friend MHWeaponDPS As Integer
	Friend MHWeaponSpeed As double
	Sub init()
		MHWeaponDPS = 0
		MHWeaponSpeed = 2
		Ghoul.NextClaw = 0
		Ghoul.NextWhiteMainHit = 0
		Ghoul.total = 0
		ghoul.TotalHit = 0
		ghoul.TotalCrit = 0

		If TalentUnholy.MasterOfGhouls Then 
			Ghoul.ActiveUntil = sim.MaxTime
			Ghoul.cd = sim.MaxTime
		Else
			Ghoul.ActiveUntil = 0
			Ghoul.cd = 0
		End If
	end Sub
	Function BaseAP() As Integer

		return 1167 
     	'tmp = (tmp + Character.Strength * 2 + Character.AttackPower + 220) * (1 + Buff.AttackPowerPc / 10)
		'return tmp
	End Function
	Function AP() As Integer
		AP = Strength - 331 + BaseAP
	End Function
	function Base_Str as integer
	
	
	end function
	
	
	Function Strength as integer
		if glyph.ghoul then
				return 331 + (Character.Strength * (talentunholy.ravenousdead*0.7))+ (Character.Strength * .4) 
			else
				return 331 + (Character.Strength * (talentunholy.ravenousdead*0.7))
		end if
		
	end function
	
	Function crit() As System.Double
		Dim tmp As Double
		tmp = 5  'BaseCrit
		'tmp = tmp + Character.CritRating / 45.91
'		tmp = tmp + Character.Agility / 62.5
		tmp = tmp + 5 * Buff.MeleeCrit
		tmp = tmp + 3 * Buff.CritChanceTaken
		crit = tmp / 100
	End Function
	Function SpellCrit() As Single
		Dim tmp As Double
		'tmp = Character.SpellCritRating / 45.91
		tmp = tmp + 3 * Buff.CritChanceTaken
		tmp = tmp + 5 * Buff.SpellCrit
		tmp = tmp + 5  * Buff.SpellCritTaken
		SpellCrit = tmp / 100
	End Function
	Function Haste() As Double
		Dim tmp As Double
		tmp = Character.HasteRating / 32.79 / 100
		'tmp = tmp * 1.3 ' Haste change for 3.1
		tmp = tmp + UnholyPresence * 0.15
		tmp = tmp + 0.05 * talentfrost.ImprovedIcyTalons
		tmp = tmp + 0.2 * Buff.MeleeHaste
		tmp = tmp + 0.03 * Buff.Haste
		Haste = tmp
	End Function
	
	Function SpellHaste() As Double
		Dim tmp As Double
		If MainStat.UnholyPresence = 1 Then
			SpellHaste = 0.5
		Else
			tmp = Character.SpellHasteRating / 32.79 / 100
			tmp = tmp + 0.05 * talentfrost.ImprovedIcyTalons
			tmp = tmp + 0.05 * Buff.SpellHaste
			tmp = tmp + 0.03 * Buff.Haste
			SpellHaste = tmp
		End If
		
		
		
	End Function
	
	Function Expertise() As Double
		
		return 0
		
	End Function
	
	
	Function Hit() As Double
		Dim tmp As Double
		tmp = (Character.HitRating / 32.79)
		tmp = tmp + Draenei
		return tmp / 100
	End Function
	
	Function SpellHit() As Double
		Dim tmp As Double
		tmp = Character.SpellHitRating / 26.23
		tmp = tmp + Buff.SpellHitTaken * 3
		tmp = tmp + Draenei
		SpellHit = tmp / 100
	End Function
	
	Function MHBaseDamage() As Double
		Dim tmp As Double
		tmp = (MHWeaponDPS + (AP / 14)) * MHWeaponSpeed
		return tmp
	End Function

	Function ArmorPen As Double
		Dim tmp As Double
		'tmp = character.ArmorPenetrationRating/15.39
		tmp = tmp *1.25
		return tmp
	End Function
	
	Function ArmorMitigation() As Double
		Dim tmp As Double
		tmp = BossArmor
		tmp = tmp * (1- 20 * buff.ArmorMajor / 100)
		tmp = tmp * (1- 5 * buff.ArmorMinor / 100)
		tmp = tmp * (1 - ArmorPen / 100)
		tmp = (tmp /((467.5*83)+tmp-22167.5))
		Return tmp
	End Function
	
	Function PhysicalDamageMultiplier(T as long) As Double
		dim tmp as Double
		tmp = 1
		tmp = tmp * (1 - ArmorMitigation)
		tmp = tmp * (1 + 0.03 * Buff.PcDamage)
		tmp = tmp * (1 + 0.02 * Buff.PhysicalVuln)
		return tmp
	End Function
	
	Function MagicalDamageMultiplier(T as long) As Double
		Dim tmp As Double
		tmp = 1
		tmp = tmp * (1 + 0.03 * Buff.PcDamage)
		tmp = tmp * (1 + 0.13 * Buff.SpellDamageTaken)
		return tmp
	End Function
	
	
End Module
