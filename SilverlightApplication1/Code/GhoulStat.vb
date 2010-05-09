'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 18:49
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class GhoulStat
	Friend BaseStrength As Integer
	Friend BaseAP As Integer
	Friend Agility As Integer
	Friend StrengthMultiplier As Double
	Friend MHWeaponDPS As Double
	Friend MHWeaponSpeed As Double
	Friend APtoDPS As Double
	Private character As Character
	Private Sim as Sim
	Sub New(S As Sim)
		Sim = S
		Character = sim.Character
		MHWeaponDPS = 50.0
		MHWeaponSpeed = 2
		BaseAP = -20
		Agility = 856
		BaseStrength = 331
		APtoDPS = 0.89 / 14  'from observation
		
		StrengthMultiplier = 0.7 * (1 + sim.Character.talentunholy.ravenousdead*0.2)
		if sim.character.glyph.Ghoul then StrengthMultiplier = StrengthMultiplier + 0.4
	End Sub
	
	Function Strength As Integer
		Return StrengthMultiplier * Character.Strength + BaseStrength
	End Function
	
	Function AP() As Double 'non-permaghoul calculation
		return BaseAP + Strength + Agility
	End Function
	
	Function crit(Optional target As Targets.Target = Nothing) As System.Double
		if target is nothing then target = sim.Targets.MainTarget
		Dim tmp As Double
		tmp = 5  'BaseCrit
		tmp = tmp + 5 *  sim.Character.Buff.MeleeCrit
		tmp = tmp + 3 *  target.Debuff.CritChanceTaken
		crit = tmp / 100
	End Function
	Function SpellCrit(Optional target As Targets.Target = Nothing) As Single
		if target is nothing then target = sim.Targets.MainTarget
		Dim tmp As Double
		tmp = tmp + 3 *  target.Debuff.CritChanceTaken
		tmp = tmp + 5 *  sim.Character.Buff.SpellCrit
		tmp = tmp + 5  *  target.Debuff.SpellCritTaken
		SpellCrit = tmp / 100
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
	
	Function ArmorPen As Double
		Dim tmp As Double
		'tmp = character.ArmorPenetrationRating/15.39
		'tmp = tmp *1.25
		
		return tmp
	End Function
	
	Function MHBaseDamage(AP as Double) As Double
		Dim tmp As Double
		tmp = (MHWeaponDPS + (AP * APtoDPS )) * MHWeaponSpeed
		return tmp
	End Function
	
	Function SwingTime(Haste As Double) As Double
		return MHWeaponSpeed / Haste
	End Function
	
	Function ClawTime(Haste As Double) As Double
		return 4.0
	End Function
	
	
	Function ArmorMitigation(target as Targets.Target ) As Double
		Dim tmp As Double
		tmp = sim.MainStat.BossArmor
		tmp = tmp * (1- 20 *  target.Debuff.ArmorMajor / 100)
		tmp = tmp * (1- 5 *  target.Debuff.ArmorMinor / 100)
		tmp = tmp * (1 - ArmorPen / 100)
		tmp = (tmp /((467.5*83)+tmp-22167.5))
		
		Return tmp
	End Function
	
	Function PhysicalDamageMultiplier(T As Long,Optional target As Targets.Target = Nothing) As Double
		if target is nothing then target = sim.Targets.MainTarget
		dim tmp as Double
		tmp = 1
		tmp = tmp * (1 - ArmorMitigation(target))
		tmp = tmp * (1 + 0.03 *  sim.Character.Buff.PcDamage)
		tmp = tmp * (1 + 0.02 *  target.Debuff.PhysicalVuln)
		if sim.Character.Orc then tmp = tmp * 1.05
		return tmp
	End Function
	
	Function MagicalDamageMultiplier(T As Long,Optional target As Targets.Target = Nothing) As Double
		if target is nothing then target = sim.Targets.MainTarget
		Dim tmp As Double
		tmp = 1
		tmp = tmp * (1 + 0.03 *  sim.Character.Buff.PcDamage)
		tmp = tmp * (1 + 0.13 *  target.Debuff.SpellDamageTaken)
		if sim.Character.Orc then tmp = tmp * 1.05
		Return tmp
	End Function
End Class
