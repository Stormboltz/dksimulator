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
		
		StrengthMultiplier = 0.7 * (1 + sim.talentunholy.ravenousdead*0.2)
		if sim.Glyph.Ghoul then StrengthMultiplier = StrengthMultiplier + 0.4
	End Sub
	
	Function Strength As Integer
		Return StrengthMultiplier * Character.Strength + BaseStrength
	End Function
	
	Function AP() As Double 'non-permaghoul calculation
		return BaseAP + Strength + Agility
	End Function
	
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
		if sim.Character.Orc then tmp = tmp * 1.05
		Return tmp
	End Function
End Class
