'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 18:49
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class GhoulStat
	
	
	Friend MHWeaponDPS As Integer
	Friend MHWeaponSpeed As Double
	Private character As Character
	Private Sim as Sim
	Sub New(S As Sim)
		Sim = S
		Character = sim.Character
		MHWeaponDPS = 0
		MHWeaponSpeed = 2
		sim.Ghoul.NextClaw = 0
		sim.Ghoul.NextWhiteMainHit = 0
		sim.Ghoul.total = 0
		sim.ghoul.TotalHit = 0
		sim.ghoul.TotalCrit = 0
		If TalentUnholy.MasterOfGhouls Then 
			sim.Ghoul.ActiveUntil = sim.MaxTime
			sim.Ghoul.cd = sim.MaxTime
		Else
			sim.Ghoul.ActiveUntil = 0
			sim.Ghoul.cd = 0
		End If
	end Sub
	Function BaseAP() As Integer
		return 1167 
     	'tmp = (tmp + Character.Strength * 2 + Character.AttackPower + 220) * (1 +  sim. sim. sim.Buff.AttackPowerPc / 10)
		'return tmp
	End Function
	Function AP() As Integer
		AP = Strength - 331 + BaseAP
	End Function
	function Base_Str as integer
	
	end function
	Function Strength as integer
		if sim.glyph.ghoul then
				return 331 + (Character.Strength * (talentunholy.ravenousdead*0.7))+ (Character.Strength * .4) 
			else
				return 331 + (Character.Strength * (talentunholy.ravenousdead*0.7))
		end if
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
	Function Haste() As Double
		Dim tmp As Double
		tmp = Character.HasteRating / 32.79 / 100 'Haste change for 3.1 ?
		tmp = tmp + sim.mainstat.UnholyPresence * 0.15
		tmp = tmp + 0.05 * talentfrost.ImprovedIcyTalons
		tmp = tmp + 0.2 *  sim.Buff.MeleeHaste
		tmp = tmp + 0.03 *  sim.Buff.Haste
		Haste = tmp
	End Function
	Function SpellHaste() As Double
		Dim tmp As Double
		If sim.Mainstat.UnholyPresence = 1 Then
			SpellHaste = 0.5
		Else
			tmp = Character.SpellHasteRating / 32.79 / 100
			tmp = tmp + 0.05 * talentfrost.ImprovedIcyTalons
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
