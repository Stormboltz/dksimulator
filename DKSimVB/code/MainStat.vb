'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 18:49
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module MainStat
	Friend MHWeaponDPS As double
	Friend MHWeaponSpeed As double
	Friend OHWeaponDPS As double
	Friend OHWeaponSpeed As double
	Friend BossArmor As Integer
	Friend BloodPresence As Integer
	Friend UnholyPresence As Integer
	Friend FrostPresence As Integer
	
	Friend CSD As Integer
	
	
	Function DualW As Boolean
		return character.Dual
	End Function
	
	Sub init()
		on error resume next
		dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load(sim._MainFrm.GetFilePath(_MainFrm.cmbCharacter.Text) )
		MHWeaponDPS = (XmlDoc.SelectSingleNode("//character/weapon/mainhand/dps").InnerText).Replace(".",System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
		If sim.EPStat="WeaponDPS" Then
			MHWeaponDPS = MHWeaponDPS + 10
		End If
		MHWeaponSpeed = (XmlDoc.SelectSingleNode("//character/weapon/mainhand/speed").InnerText).Replace(".",System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
		If sim.EPStat="WeaponSpeed" Then
			MHWeaponSpeed = MHWeaponSpeed + 0.1
		End If
		OHWeaponDPS = (XmlDoc.SelectSingleNode("//character/weapon/offhand/dps").InnerText).Replace(".",System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
		OHWeaponSpeed = (XmlDoc.SelectSingleNode("//character/weapon/offhand/speed").InnerText).Replace(".",System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
		BossArmor = 10643
		
		CSD = XmlDoc.SelectSingleNode("//character/ChaoticSkyflareDiamond").InnerText
		
		
		'Trinkets
		
		
		MjolRune = 0
		GrimToll = 0
		BitterAnguish = 0
		Mirror = 0
		Greatness = 0
		DCDeath = 0
		Victory = 0
		Necromantic = 0
		Bandit = 0
		Pyrite = 0
		DarkMatter = 0
		OldGod = 0
		Comet = 0
		DeathChoice = 0

		Select Case sim.EPStat
			Case "NoTrinket"
			Case "AttackPowerNoTrinket"
			Case "MjolRune"
				MjolRune = 1
			Case "GrimToll"
				GrimToll	= 1
			Case "BitterAnguish"
				BitterAnguish	= 1
			Case "Mirror"
				Mirror= 1
			Case "Greatness"
				Greatness= 1
			Case "DCDeath"
				DCDeath= 1
			Case "Victory"
				Victory= 1
			Case "Necromantic"
				Necromantic= 1
			Case "Bandit"
				Bandit= 1
			Case "Pyrite"
				Pyrite= 1
			Case "DarkMatter"
				DarkMatter= 1
			Case "OldGod"
				OldGod= 1
			Case "Comet"
				Comet= 1
			Case "DeathChoice"
				DeathChoice= 1
				
			Case Else
				
				MjolRune = XmlDoc.SelectSingleNode("//character/trinket/MjolnirRunestone").InnerText
				GrimToll = XmlDoc.SelectSingleNode("//character/trinket/GrimToll").InnerText
				BitterAnguish = XmlDoc.SelectSingleNode("//character/trinket/BitterAnguish").InnerText
				Mirror = XmlDoc.SelectSingleNode("//character/trinket/Mirror").InnerText
				Greatness = XmlDoc.SelectSingleNode("//character/trinket/Greatness").InnerText
				DCDeath = XmlDoc.SelectSingleNode("//character/trinket/DCDeath").InnerText
				Victory = XmlDoc.SelectSingleNode("//character/trinket/Victory").InnerText
				Necromantic = XmlDoc.SelectSingleNode("//character/trinket/Necromantic").InnerText
				Bandit = XmlDoc.SelectSingleNode("//character/trinket/Bandit").InnerText
				Pyrite = XmlDoc.SelectSingleNode("//character/trinket/Pyrite").InnerText
				DarkMatter = XmlDoc.SelectSingleNode("//character/trinket/DarkMatter").InnerText
				OldGod = XmlDoc.SelectSingleNode("//character/trinket/OldGod").InnerText
				Comet = XmlDoc.SelectSingleNode("//character/trinket/Comet").InnerText
				DeathChoice = XmlDoc.SelectSingleNode("//character/trinket/DeathChoice").InnerText
				
		End Select
		
		Select Case sim.EPStat
			Case "0T7"
				T72PDPS = 0
				T74PDPS = 0
				T82PDPS = 0
				T84PDPS = 0
				T92PDPS = 0
				T94PDPS = 0
			Case "AttackPower0T7"
				T72PDPS = 0
				T74PDPS = 0
				T82PDPS = 0
				T84PDPS = 0
				T92PDPS = 0
				T94PDPS = 0
			Case "2T7"
				T72PDPS = 1
				T74PDPS = 0
				T82PDPS = 0
				T84PDPS = 0
				T92PDPS = 0
				T94PDPS = 0
			Case "4T7"
				T72PDPS = 0
				T74PDPS = 1
				T82PDPS = 0
				T84PDPS = 0
				T92PDPS = 0
				T94PDPS = 0
			Case "2T8"
				T72PDPS = 0
				T74PDPS = 0
				T82PDPS = 1
				T84PDPS = 0
				T92PDPS = 0
				T94PDPS = 0
			Case "4T8"
				T72PDPS = 0
				T74PDPS = 0
				T82PDPS = 0
				T84PDPS = 1
				T92PDPS = 0
				T94PDPS = 0
			Case "2T9"
				T72PDPS = 0
				T74PDPS = 0
				T82PDPS = 0
				T84PDPS = 0
				T92PDPS = 1
				T94PDPS = 0
			Case "4T9"
				T72PDPS = 0
				T74PDPS = 0
				T82PDPS = 0
				T84PDPS = 0
				T92PDPS = 0
				T94PDPS = 1
				
				
			Case Else
				T72PDPS = XmlDoc.SelectSingleNode("//character/Set/T72PDPS").InnerText
				T74PDPS = XmlDoc.SelectSingleNode("//character/Set/T74PDPS").InnerText
				T82PDPS = XmlDoc.SelectSingleNode("//character/Set/T82PDPS").InnerText
				T84PDPS = XmlDoc.SelectSingleNode("//character/Set/T84PDPS").InnerText
				
				T72PTNK = XmlDoc.SelectSingleNode("//character/Set/T72PTNK").InnerText
				T74PTNK = XmlDoc.SelectSingleNode("//character/Set/T74PTNK").InnerText
				T82PTNK = XmlDoc.SelectSingleNode("//character/Set/T82PTNK").InnerText
				T84PTNK = XmlDoc.SelectSingleNode("//character/Set/T84PTNK").InnerText
				
				T92PDPS = XmlDoc.SelectSingleNode("//character/Set/T92PDPS").InnerText
				T94PDPS = XmlDoc.SelectSingleNode("//character/Set/T94PDPS").InnerText
		End Select
		
		
	End Sub
	
	Function BaseAP() As Integer
		dim tmp as integer
		if Sigils.Strife then
			if proc.StrifeFade >= sim.TimeStamp then tmp = 120
		End If
		
		If MirrorFade > sim.TimeStamp Then tmp = tmp + 1000
		If OldgodFade > sim.TimeStamp Then tmp = tmp + 1284
		If pyriteFade > sim.TimeStamp Then tmp = tmp + 1234
		If victoryFade > sim.TimeStamp Then tmp = tmp + 1008
		
		'Why +220 ?
		tmp = (tmp + Character.Strength * 2 + Character.AttackPower + 550) * (1 + Buff.AttackPowerPc / 10)
		return tmp
	End Function
	
	Function AP() As Integer
		return  BaseAP
	End Function
	
	Function crit() As System.Double
		Dim tmp As Double
		tmp = 5  'BaseCrit. What base crit?
		tmp = tmp + Character.CritRating / 45.91
		if Sigils.HauntedDreams then
			if proc.HauntedDreamsFade >= sim.TimeStamp then tmp = tmp + 173/45.91
		End If
		If DarkMatterFade > sim.TimeStamp Then
			tmp = tmp + 612 / 45.91
		End If
		
		tmp = tmp + Character.Agility*0.016
		tmp = tmp + 5 * Buff.MeleeCrit
		tmp = tmp + 3 * Buff.CritChanceTaken
		tmp = tmp + TalentBlood.Darkconv
		tmp = tmp + TalentUnholy.EbonPlaguebringer
		tmp = tmp + talentfrost.Annihilation
		tmp = tmp - 4.8 'Crit malus vs bosses
		
		return tmp / 100
	End Function
	Function critAutoattack() As System.Double 'No Annihilation for autoattacks
		Dim tmp As Double
		tmp = 5  'BaseCrit. What base crit?
		tmp = tmp + Character.CritRating / 45.91
		if Sigils.HauntedDreams then
			if proc.HauntedDreamsFade >= sim.TimeStamp then tmp = tmp + 173/45.91
		End If
		If DarkMatterFade > sim.TimeStamp Then
			tmp = tmp + 612 / 45.91
		End If
		
		tmp = tmp + Character.Agility / 62.5
		tmp = tmp + 5 * Buff.MeleeCrit
		tmp = tmp + 3 * Buff.CritChanceTaken
		tmp = tmp + TalentBlood.Darkconv
		tmp = tmp + TalentUnholy.EbonPlaguebringer
		tmp = tmp - 4.8 'Crit malus vs bosses
		
		return tmp / 100
	End Function
	Function SpellCrit() As Single
		Dim tmp As Double
		tmp = Character.SpellCritRating / 45.91
		if Sigils.HauntedDreams then
			if proc.HauntedDreamsFade >= sim.TimeStamp then tmp = tmp + 173/45.91
		End If
		If DarkMatterFade > sim.TimeStamp Then
			tmp = tmp + 612 / 45.91
		End If
		
		tmp = tmp + 3 * Buff.CritChanceTaken
		tmp = tmp + 5 * Buff.SpellCrit
		tmp = tmp + 5 * Buff.SpellCritTaken
		tmp = tmp + TalentBlood.Darkconv
		tmp = tmp + TalentUnholy.EbonPlaguebringer
		tmp = tmp - 2.1 'Spell crit malus vs bosses
		
		return  tmp / 100
	End Function
	Function Haste() As Double
		Dim tmp As Double
		tmp = Character.HasteRating / (32.79/1.3) / 100 '1.3 is the buff haste rating received
		tmp = tmp + UnholyPresence * 0.15
		tmp = tmp + 0.05 * talentfrost.ImprovedIcyTalons
		tmp = tmp + 0.2 * Buff.MeleeHaste
		tmp = tmp + 0.03 * Buff.Haste
		If Bloodlust.IsActive(sim.TimeStamp) Then tmp = tmp + 0.3
		If CometFade > sim.TimeStamp Then tmp = tmp + 726/(32.79/1.3)/100
		If BitterAnguishFade > sim.TimeStamp Then tmp = tmp + 410/(32.79/1.3)/100
		
		return tmp
	End Function
	Function SpellHaste() As Double
		Dim tmp As Double
		If MainStat.UnholyPresence = 1 Then
			SpellHaste = 0.5
		Else
			tmp = Character.SpellHasteRating / 32.79 / 100
			tmp = tmp + 0.05 * Buff.SpellHaste
			tmp = tmp + 0.03 * Buff.Haste
			If Bloodlust.IsActive(sim.TimeStamp)  Then	tmp = tmp + 0.3
			If CometFade > sim.TimeStamp Then tmp = tmp + 726/(32.79/1.3)/100
			If BitterAnguishFade > sim.TimeStamp Then tmp = tmp + 410/(32.79/1.3)/100
			
			return tmp
		End If
	End Function
	Function Expertise() As Double
		Dim tmp As Double
		tmp = Character.ExpertiseRating / 32.79
		tmp = tmp + 0.25 * talentblood.Vot3W*2
		tmp = tmp + 0.25 * talentfrost.TundraStalker
		tmp = tmp + 0.25 * talentunholy.RageofRivendare
		
		If sim.EPStat<>"" Then tmp = 6.5 'For most EP stats we assume being exp capped
		If sim.EPStat="ExpertiseRating" Then tmp = 6.5 - sim.EPBase / 32.79
		return  tmp / 100
	End Function
	Function Hit() As Double
		Dim tmp As Double
		tmp = (Character.HitRating / 32.79)
		If MainStat.DualW Then tmp = tmp + 1 * TalentFrost.NervesofColdSteel
		tmp = tmp + Draenei
		
		If sim.EPStat<>"" Then tmp = 8 'For most EP stats we assume being hit capped
		If sim.EPStat="HitRating" Then tmp = 8 - sim.EPBase / 32.79
		If sim.EPStat="SpellHitRating" Then tmp = 8 + sim.EPBase / 32.79
		Hit = tmp / 100
	End Function
	Function SpellHit() As Double
		Dim tmp As Double
		tmp = Character.SpellHitRating / 26.23
		tmp = tmp + 1 * TalentUnholy.Virulence
		tmp = tmp + Buff.SpellHitTaken * 3
		tmp = tmp + Draenei
		
		If sim.EPStat<>"" Then tmp = 263 / 26.23
		If sim.EPStat="HitRating" Then tmp = 263 / 26.23 - sim.EPBase / 26.23
		If sim.EPStat="SpellHitRating" Then tmp = 263 / 26.23 + sim.EPBase / 26.23
		SpellHit = tmp / 100
	End Function
	Function NormalisedMHDamage() As Double
		Dim tmp As Double
		tmp =  MHWeaponSpeed * MHWeaponDPS
		If DualW Then
			tmp =  tmp + 2.4*(AP / 14)
		Else
			tmp =  tmp + 3.3*(AP / 14)
			tmp = tmp * (1 + TalentBlood.Weapspec * 2 / 100)
		End If
		return tmp
	End Function
	Function NormalisedOHDamage() As Double
		Dim tmp As Double
		tmp =  OHWeaponSpeed * OHWeaponDPS
		tmp =  tmp + 2.4*(AP / 14)
		return tmp
	End Function
	Function MHBaseDamage() As Double
		Dim tmp As Double
		tmp = (MHWeaponDPS + (AP / 14)) * MHWeaponSpeed
		tmp = tmp * (1 + TalentBlood.Weapspec * 2 / 100)
		return tmp
	End Function
	Function OHBaseDamage() As Double
		OHBaseDamage = (OHWeaponDPS + (AP / 14)) * OHWeaponSpeed
	End Function
	Function ArmorPen As Double
		Dim tmp As Double
		tmp = character.ArmorPenetrationRating / 15.39
		tmp = tmp *1.25
		return tmp / 100
	End Function
	Function ArmorMitigation() As Double
		Dim tmp As Double
		
		Dim A As Double
		Dim B As Double
		Dim x As Double
		Dim y As Double
		Dim z as Double
		
		A = 15232.5
		B = BossArmor
		
		
		tmp = 1
		tmp = tmp * (1- 20 * buff.ArmorMajor / 100)
		tmp = tmp * (1- 5 * buff.ArmorMinor / 100)
		tmp = tmp * (1 - TalentBlood.BloodGorged * 2 / 100)
		tmp = 1
		x = ArmorPen
		
		y = tmp
		z = (A+B)/3 / B
		
		Dim retour As Double
		retour = A/(A+B*(1-(z*x+y)+B/(A+B)*x*(1-y)*z))
		
		
		
		'(tmp = tmp * (1 - ArmorPen / 100)
		'%Reduction = (Armor / ([467.5 * Enemy_Level] + Armor - 22167.5)) * 100
		' tmp = (tmp /((467.5*83)+tmp-22167.5))
		
		Return (1.0 - Math.max(0.0, retour))
	End Function
	function getMitigation() as Double
		dim l_bossArmor as double
		dim l_constant as double  = 15232.5
		l_bossArmor = 10643
		
		Dim l_personalArpPercent As Double = ArmorPen
		l_personalArpPercent = l_personalArpPercent + (TalentBlood.BloodGorged * 2 / 100)
		If l_personalArpPercent > 1 Then l_personalArpPercent = 1
		
		dim l_debuffPercent as double = 0.0
		dim l_sunder as double = 1.0
		dim l_ff  as double = 1.0
		if  buff.ArmorMajor > 0 then l_sunder = 1- 0.20
		If buff.ArmorMinor > 0 Then l_ff = 1 - 0.05
		l_debuffPercent = 1 - (l_sunder * l_ff)
		
		dim l_tempA as double = l_constant + l_bossArmor * (1.0 - l_debuffPercent)
		dim l_termA  as double = (((1.0 - l_debuffPercent) * l_bossArmor + l_constant) / 3.0)
		dim l_termB  as double = l_bossArmor * (1.0 - l_debuffPercent)
		dim l_tempB  as double
		
		if (l_termA < l_termB) then
			l_tempB = l_termA * l_personalArpPercent
		else
			l_tempB = l_termB * l_personalArpPercent
		end if
		
		dim l_answer  as double = l_constant / (l_tempA - l_tempB)
		
		If MjolRuneFade > sim.TimeStamp or GrimTollFade > sim.TimeStamp Then
			'Debug.Print( "l_personalArpPercent = " & l_personalArpPercent & " l_answer = " & l_answer & " (1.0 - Math.max(0.0, l_answer)) = " & (1.0 - Math.max(0.0, l_answer)) & sim.TimeStamp)
		End If
		
		'Debug.Print( "l_personalArpPercent = " & l_personalArpPercent & " l_answer = " & l_answer & " (1.0 - Math.max(0.0, l_answer)) = " & (1.0 - Math.max(0.0, l_answer)) & sim.TimeStamp)
		return l_answer
	End Function
	Function WhiteHitDamageMultiplier(T as long) As Double
		dim tmp as Double
		tmp = 1
		tmp = tmp * (1 + MainStat.BloodPresence * 0.15)
		tmp = tmp * (1 + 0.03 * Buff.PcDamage)
		If Desolation.isActive(T) Then tmp = tmp * (1+Desolation.Bonus)
		tmp = tmp * (1 + 0.02 * TalentUnholy.BoneShield)
		tmp = tmp * (1 + 0.02 * TalentBlood.BloodGorged)
		
		tmp = tmp * getMitigation
		tmp = tmp * (1 + 0.04 * Buff.PhysicalVuln)
		tmp = tmp * (1 + 0.02 * TalentBlood.BloodyVengeance)
		if Hysteria.IsActive(T) then tmp = tmp * 1.2
		
		return tmp
	End Function
	Function StandardPhysicalDamageMultiplier(T as long) As Double
		dim tmp as Double
		tmp = 1
		tmp = tmp * (1 + MainStat.BloodPresence * 0.15)
		tmp = tmp * (1 + 0.03 * Buff.PcDamage)
		If Desolation.isActive(T) Then tmp = tmp * (1+Desolation.Bonus)
		tmp = tmp * (1 + 0.02 * TalentUnholy.BoneShield)
		tmp = tmp * (1 + 0.02 * TalentBlood.BloodGorged)
		
		tmp = tmp * getMitigation
		tmp = tmp * (1 + 0.04 * Buff.PhysicalVuln)
		tmp = tmp * (1 + 0.02 * TalentBlood.BloodyVengeance)
		If Hysteria.IsActive(T) Then tmp = tmp * 1.2
		
		If FrostFever.isActive(T) Then	tmp = tmp * (1 + 0.03 * TalentFrost.TundraStalker)
		If BloodPlague.isActive(T) Then tmp = tmp * (1 + 0.02 * talentunholy.RageofRivendare)
		
		return tmp
	End Function
	Function StandardMagicalDamageMultiplier(T as long) As Double
		Dim tmp As Double
		tmp = 1
		tmp = tmp * (1 + MainStat.BloodPresence * 0.15)
		tmp = tmp * (1 + 0.03 * Buff.PcDamage)
		If Desolation.isActive(T) Then tmp = tmp * (1+Desolation.Bonus)
		tmp = tmp * (1 + 0.02 * TalentUnholy.BoneShield)
		tmp = tmp * (1 + 0.02 * TalentBlood.BloodGorged)
		
		If FrostFever.isActive(T) Then	tmp = tmp * (1 + 0.03 * TalentFrost.TundraStalker)
		If BloodPlague.isActive(T) Then tmp = tmp * (1 + 0.02 * talentunholy.RageofRivendare)
		
		tmp = tmp * (1 + 0.13 * Buff.SpellDamageTaken)
		tmp = tmp * (1-0.05) 'Average partial resist
		
		return tmp
	End Function
End Module
