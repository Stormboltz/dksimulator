'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 18:49
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class MainStat
	Friend MHWeaponDPS As double
	Friend MHWeaponSpeed As double
	Friend OHWeaponDPS As double
	Friend OHWeaponSpeed As double
	Friend BossArmor As Integer
	Friend BloodPresence As Integer
	Friend UnholyPresence As Integer
	Friend FrostPresence As Integer
	Private character As Character
	
	
	Friend T72PDPS As integer
	Friend T74PDPS As integer
	Friend T72PTNK As integer
	Friend T74PTNK As integer
	
	Friend T82PDPS As integer
	Friend T84PDPS As integer
	Friend T82PTNK As integer
	Friend T84PTNK As Integer
	
	
	Friend T92PDPS As integer
	Friend T94PDPS As Integer
	
	Friend T102PDPS As integer
	Friend T104PDPS As Integer
	
	Private _Mitigation As Double
	Private _LastArP as Double
	
	
	Protected Sim as Sim
	
	
	
	
	Friend CSD As Integer
	
	
	Function DualW As Boolean
		return character.Dual
	End Function
	
	Sub New(S As Sim)
		Sim = S
		
		_Mitigation = 0
		_LastArP = 0
		
		
		'On Error Resume Next
		character = sim.Character
		dim XmlDoc As New Xml.XmlDocument
		XmlDoc = character.XmlDoc
		'XmlDoc.Load(GetFilePath(sim._MainFrm.cmbCharacter.Text) )
		
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
		Sim.Trinkets = new Trinkets(Sim)
		
		Sim.Trinkets.MjolRune.Equiped = 0
		Sim.Trinkets.GrimToll.Equiped = 0
		Sim.Trinkets.BitterAnguish.Equiped = 0
		Sim.Trinkets.Mirror.Equiped = 0
		Sim.Trinkets.Greatness.Equiped = 0
		Sim.Trinkets.DCDeath.Equiped = 0
		Sim.Trinkets.Victory.Equiped = 0
		Sim.Trinkets.Necromantic.Equiped = 0
		Sim.Trinkets.Bandit.Equiped = 0
		Sim.Trinkets.Pyrite.Equiped = 0
		Sim.Trinkets.DarkMatter.Equiped = 0
		Sim.Trinkets.OldGod.Equiped = 0
		Sim.Trinkets.Comet.Equiped = 0
		Sim.Trinkets.DeathChoice.Equiped = 0
		Try
			sim.trinkets.MHSingedViskag.Equiped = XmlDoc.SelectSingleNode("//character/proc/MHSingedViskag").InnerText
			sim.trinkets.MHtemperedViskag.Equiped = XmlDoc.SelectSingleNode("//character/proc/MHtemperedViskag").InnerText
			sim.trinkets.OHtemperedViskag.Equiped = XmlDoc.SelectSingleNode("//character/proc/OHtemperedViskag").InnerText
			sim.trinkets.OHSingedViskag.Equiped = XmlDoc.SelectSingleNode("//character/proc/OHSingedViskag").InnerText
		Catch
		End Try
		Try
			sim.trinkets.MHRagingDeathbringer.Equiped = XmlDoc.SelectSingleNode("//character/proc/MHRagingDeathbringer").InnerText
			sim.trinkets.OHRagingDeathbringer.Equiped = XmlDoc.SelectSingleNode("//character/proc/OHRagingDeathbringer").InnerText
			sim.trinkets.MHEmpoweredDeathbringer.Equiped = XmlDoc.SelectSingleNode("//character/proc/MHEmpoweredDeathbringer").InnerText
			sim.trinkets.OHEmpoweredDeathbringer.Equiped = XmlDoc.SelectSingleNode("//character/proc/OHEmpoweredDeathbringer").InnerText
		Catch
		End Try
		
		Try
			sim.trinkets.HandMountedPyroRocket.Equiped = XmlDoc.SelectSingleNode("//character/proc/HandMountedPyroRocket").InnerText
		Catch
		End Try
		
		
		try
			
			Select Case sim._EPStat
				Case "NoTrinket"
				Case "AttackPowerNoTrinket"
				Case "MjolRune"
					Sim.Trinkets.MjolRune.Equiped = 1
				Case "GrimToll"
					Sim.Trinkets.GrimToll.Equiped	= 1
				Case "BitterAnguish"
					Sim.Trinkets.BitterAnguish.Equiped	= 1
				Case "Mirror"
					Sim.Trinkets.Mirror.Equiped= 1
				Case "Greatness"
					Sim.Trinkets.Greatness.Equiped = 1
				Case "DCDeath"
					Sim.Trinkets.DCDeath.Equiped= 1
				Case "Victory"
					Sim.Trinkets.Victory.Equiped= 1
				Case "Necromantic"
					Sim.Trinkets.Necromantic.Equiped= 1
				Case "Bandit"
					Sim.Trinkets.Bandit.Equiped= 1
				Case "Pyrite"
					Sim.Trinkets.Pyrite.Equiped= 1
				Case "DarkMatter"
					Sim.Trinkets.DarkMatter.Equiped= 1
				Case "OldGod"
					Sim.Trinkets.OldGod.Equiped= 1
				Case "Comet"
					Sim.Trinkets.Comet.Equiped= 1
				Case "DeathChoice"
					Sim.Trinkets.DeathChoice.Equiped= 1
				Case "DeathChoiceHeroic"
					Sim.Trinkets.DeathChoiceHeroic.Equiped= 1
				Case Else
					Sim.Trinkets.MjolRune.Equiped = XmlDoc.SelectSingleNode("//character/trinket/MjolnirRunestone").InnerText
					Sim.Trinkets.GrimToll.Equiped = XmlDoc.SelectSingleNode("//character/trinket/GrimToll").InnerText
					Sim.Trinkets.BitterAnguish.Equiped = XmlDoc.SelectSingleNode("//character/trinket/BitterAnguish").InnerText
					Sim.Trinkets.Mirror.Equiped = XmlDoc.SelectSingleNode("//character/trinket/Mirror").InnerText
					Sim.Trinkets.Greatness.Equiped = XmlDoc.SelectSingleNode("//character/trinket/Greatness").InnerText
					Sim.Trinkets.DCDeath.Equiped = XmlDoc.SelectSingleNode("//character/trinket/DCDeath").InnerText
					Sim.Trinkets.Victory.Equiped = XmlDoc.SelectSingleNode("//character/trinket/Victory").InnerText
					Sim.Trinkets.Necromantic.Equiped = XmlDoc.SelectSingleNode("//character/trinket/Necromantic").InnerText
					Sim.Trinkets.Bandit.Equiped = XmlDoc.SelectSingleNode("//character/trinket/Bandit").InnerText
					Sim.Trinkets.Pyrite.Equiped = XmlDoc.SelectSingleNode("//character/trinket/Pyrite").InnerText
					Sim.Trinkets.DarkMatter.Equiped = XmlDoc.SelectSingleNode("//character/trinket/DarkMatter").InnerText
					Sim.Trinkets.OldGod.Equiped = XmlDoc.SelectSingleNode("//character/trinket/OldGod").InnerText
					Sim.Trinkets.Comet.Equiped = XmlDoc.SelectSingleNode("//character/trinket/Comet").InnerText
					Sim.Trinkets.DeathChoice.Equiped = XmlDoc.SelectSingleNode("//character/trinket/DeathChoice").InnerText
					Sim.Trinkets.DeathChoiceHeroic.Equiped = XmlDoc.SelectSingleNode("//character/trinket/DeathChoiceHeroic").InnerText
			End Select
		Catch
		End Try
		
		T72PDPS = 0
		T74PDPS = 0
		T82PDPS = 0
		T84PDPS = 0
		T92PDPS = 0
		T94PDPS = 0
		T102PDPS = 0
		T104PDPS = 0
		
		
		Select Case sim._EPStat
			Case "0T7"
			Case "AttackPower0T7"
			Case "2T7"
				T72PDPS = 1
			Case "4T7"
				T74PDPS = 1
			Case "2T8"
				T82PDPS = 1
			Case "4T8"
				
				T84PDPS = 1
				
			Case "2T9"
				
				T92PDPS = 1
				
			Case "4T9"
				
				T94PDPS = 1
			Case "2T10"
				T102PDPS = 1
				
			Case "4T10"
				T104PDPS = 1
				
			Case Else
				T72PDPS = XmlDoc.SelectSingleNode("//character/Set/T72PDPS").InnerText
				T74PDPS = XmlDoc.SelectSingleNode("//character/Set/T74PDPS").InnerText
				
				T82PDPS = XmlDoc.SelectSingleNode("//character/Set/T82PDPS").InnerText
				T84PDPS = XmlDoc.SelectSingleNode("//character/Set/T84PDPS").InnerText
				
				T92PDPS = XmlDoc.SelectSingleNode("//character/Set/T92PDPS").InnerText
				T94PDPS = XmlDoc.SelectSingleNode("//character/Set/T94PDPS").InnerText
				
				T72PTNK = XmlDoc.SelectSingleNode("//character/Set/T72PTNK").InnerText
				T74PTNK = XmlDoc.SelectSingleNode("//character/Set/T74PTNK").InnerText
				T82PTNK = XmlDoc.SelectSingleNode("//character/Set/T82PTNK").InnerText
				T84PTNK = XmlDoc.SelectSingleNode("//character/Set/T84PTNK").InnerText
				Try
					T102PDPS = XmlDoc.SelectSingleNode("//character/Set/T102PDPS").InnerText
					T104PDPS = XmlDoc.SelectSingleNode("//character/Set/T104PDPS").InnerText
				Catch
					
				End Try
		End Select
		
		
	End Sub
	
	Function BaseAP() As Integer
		dim tmp as integer
		if sim.proc.Strife.isactive then tmp = sim.proc.Strife.ProcValue
		If Sim.Trinkets.Mirror.Fade > sim.TimeStamp Then tmp = tmp + Sim.Trinkets.Mirror.ProcValue
		If Sim.Trinkets.Oldgod.Fade > sim.TimeStamp Then tmp = tmp + Sim.Trinkets.Oldgod.ProcValue
		If Sim.Trinkets.pyrite.Fade > sim.TimeStamp Then tmp = tmp + Sim.Trinkets.pyrite.ProcValue
		If Sim.Trinkets.victory.Fade > sim.TimeStamp Then tmp = tmp + Sim.Trinkets.victory.ProcValue
		If Sim.RuneForge.OHBerserkingActiveUntil > sim.TimeStamp Then tmp = tmp + 400
		
		'Why +220 ?
		tmp = tmp + Character.AttackPower
		tmp = tmp + Character.Strength * 2
		tmp = tmp + 550
		tmp = tmp * (1 +  sim.Buff.AttackPowerPc / 10)
		'tmp = (tmp + Character.Strength * 2 + Character.AttackPower + 550) * (1 +  sim.Buff.AttackPowerPc / 10)
		return tmp
	End Function
	
	Function AP() As Integer
		return  BaseAP
	End Function
	
	Function crit() As System.Double
		Dim tmp As Double
		'tmp = 5  'BaseCrit. What base crit?
		tmp = tmp + Character.CritRating / 45.91
		


		if sim.proc.HauntedDreams.IsActive then tmp += sim.proc.HauntedDreams.ProcValue/45.91
		If Sim.Trinkets.DarkMatter.Fade > sim.TimeStamp Then
			tmp = tmp + Sim.Trinkets.DarkMatter.ProcValue / 45.91
		End If
		
		tmp = tmp + Character.Agility*0.016
		tmp = tmp + 5 *  sim.Buff.MeleeCrit
		tmp = tmp + 3 *  sim.Buff.CritChanceTaken
		tmp = tmp + TalentBlood.Darkconv
		tmp = tmp + TalentUnholy.EbonPlaguebringer
		tmp = tmp + talentfrost.Annihilation
		tmp = tmp - 4.8 'Crit malus vs bosses
		
		return tmp / 100
	End Function
	Function critAutoattack() As System.Double 'No Annihilation for autoattacks
		Dim tmp As Double
		'tmp = 5  'BaseCrit. What base crit?
		tmp = tmp + Character.CritRating / 45.91
		if sim.proc.HauntedDreams.IsActive then tmp += sim.proc.HauntedDreams.ProcValue/45.91
		If Sim.Trinkets.DarkMatter.Fade > sim.TimeStamp Then
			tmp = tmp + Sim.Trinkets.DarkMatter.ProcValue / 45.91
		End If
		
		tmp = tmp + Character.Agility / 62.5
		tmp = tmp + 5 *  sim.Buff.MeleeCrit
		tmp = tmp + 3 *  sim.Buff.CritChanceTaken
		tmp = tmp + TalentBlood.Darkconv
		tmp = tmp + TalentUnholy.EbonPlaguebringer
		tmp = tmp - 4.8 'Crit malus vs bosses
		
		return tmp / 100
	End Function
	Function SpellCrit() As Single
		Dim tmp As Double
		tmp = Character.SpellCritRating / 45.91
		if sim.proc.HauntedDreams.IsActive then tmp += sim.proc.HauntedDreams.ProcValue/45.91
		If Sim.Trinkets.DarkMatter.Fade > sim.TimeStamp Then
			tmp = Sim.Trinkets.DarkMatter.ProcValue + 612 / 45.91
		End If
		
		tmp = tmp + 3 *  sim.Buff.CritChanceTaken
		tmp = tmp + 5 *  sim.Buff.SpellCrit
		tmp = tmp + 5 *  sim.Buff.SpellCritTaken
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
		tmp = tmp + 0.2 *  sim.Buff.MeleeHaste
		tmp = tmp + 0.03 *  sim.Buff.Haste
		If sim.Bloodlust.IsActive(sim.TimeStamp) Then tmp = tmp + 0.3
		If Sim.Trinkets.Comet.Fade > sim.TimeStamp Then tmp = tmp + Sim.Trinkets.Comet.ProcValue/(32.79/1.3)/100
		If Sim.Trinkets.BitterAnguish.Fade > sim.TimeStamp Then tmp = tmp + Sim.Trinkets.BitterAnguish.ProcValue/(32.79/1.3)/100
		
		return tmp
	End Function
	Function SpellHaste() As Double
		Dim tmp As Double
		If UnholyPresence = 1 Then
			SpellHaste = 0.5
		Else
			tmp = Character.SpellHasteRating / 32.79 / 100
			tmp = tmp + 0.05 * sim.Buff.SpellHaste
			tmp = tmp + 0.03 * sim.Buff.Haste
			If sim.Bloodlust.IsActive(sim.TimeStamp)  Then	tmp = tmp + 0.3
			If Sim.Trinkets.Comet.Fade > sim.TimeStamp Then tmp = tmp + Sim.Trinkets.Comet.ProcValue/(32.79/1.3)/100
			If Sim.Trinkets.BitterAnguish.Fade > sim.TimeStamp Then tmp = tmp + Sim.Trinkets.BitterAnguish.ProcValue/(32.79/1.3)/100
			
			return tmp
		End If
	End Function
	Function Expertise() As Double
		Dim tmp As Double
		tmp = Character.ExpertiseRating / 32.79
		dim str as String
		tmp = tmp + 0.25 * talentblood.Vot3W*2
		tmp = tmp + 0.25 * talentfrost.TundraStalker
		tmp = tmp + 0.25 * talentunholy.RageofRivendare
		str = sim.EPStat
		If sim.EPStat<>"" And strings.InStr(sim.EPStat,"Sca")=0 Then
			tmp = 6.5 'For most EP stats we assume being exp capped
		End If
		If sim.EPStat="ExpertiseRating" Then tmp = 6.5 - sim.EPBase / 32.79
		If sim.EPStat="ExpertiseRatingAfterCap" Then tmp = 6.5 + sim.EPBase / 32.79
		If InStr(sim.EPStat,"ScaExp") Then
			If InStr(sim.EPStat,"ScaExpA") Then
				tmp = tmp +  Replace(sim.EPStat,"ScaExpA","") * sim.EPBase /  32.79
			Else
				tmp =  Replace(sim.EPStat,"ScaExp","") * sim.EPBase /  32.79
			End If
		End If
		
		
		return  tmp / 100
	End Function
	Function Hit() As Double
		Dim tmp As Double
		tmp = (Character.HitRating / 32.79)
		If DualW Then tmp = tmp + 1 * TalentFrost.NervesofColdSteel
		
		If sim.EPStat<>"" and strings.InStr(sim.EPStat,"Sca")=0 Then tmp = 8 'For most EP stats we assume being hit capped
		If sim.EPStat="HitRating" Then tmp = 8 - sim.EPBase / 32.79
		If sim.EPStat="SpellHitRating" Then tmp = 8 + 26 / 32.79  ' +26 to not go over spell hit cap
		
		If sim.EPStat="AfterSpellHitBase" Then tmp = SpellHitCapRating / 32.79
		If sim.EPStat="AfterSpellHitBaseAP" Then tmp = SpellHitCapRating / 32.79
		If sim.EPStat="AfterSpellHitRating" Then tmp = (SpellHitCapRating + sim.EPBase) / 32.79
		If sim.EPStat="" Then tmp = tmp + sim.Buff.Draenei
		If InStr(sim.EPStat,"ScaHit") Then
			If InStr(sim.EPStat,"ScaHitA") Then
				tmp = tmp + Replace(sim.EPStat,"ScaHitA","") * sim.EPBase / 32.79
			Else
				tmp = Replace(sim.EPStat,"ScaHit","") * sim.EPBase / 32.79
			end if
			tmp = tmp + sim.Buff.Draenei
		End If
		Hit = tmp / 100
	End Function
	
	Function SpellHitCapRating() as Integer
		dim tmp as integer
		tmp = 17
		tmp = tmp - TalentUnholy.Virulence
		tmp = tmp - 3*sim.Buff.SpellHitTaken
		tmp = tmp * 26.23
		return tmp
	End Function
	
	
	
	Function SpellHit() As Double
		Dim tmp As Double
		dim MeleHitCapRating as Integer
		tmp = Character.SpellHitRating / 26.23
		If sim.EPStat<>"" and strings.InStr(sim.EPStat,"Sca")=0 Then
			MeleHitCapRating = 263 - 32.79 * TalentFrost.NervesofColdSteel
			tmp = MeleHitCapRating / 26.23
			If sim.EPStat="HitRating" Then tmp = MeleHitCapRating / 26.23 - sim.EPBase / 26.23
			If sim.EPStat="SpellHitRating" Then tmp = MeleHitCapRating / 26.23 + 26 / 26.23
			
			If sim.EPStat="AfterSpellHitBase" Then tmp = SpellHitCapRating / 26.23
			If sim.EPStat="AfterSpellHitBaseAP" Then tmp = SpellHitCapRating / 26.23
			If sim.EPStat="AfterSpellHitRating" Then tmp = (SpellHitCapRating + sim.EPBase) / 26.23
		End If
		tmp = tmp + 1 * TalentUnholy.Virulence
		tmp = tmp +  sim.Buff.SpellHitTaken * 3
		if sim.EPStat="" then tmp = tmp + sim.Buff.Draenei
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
		tmp = tmp *1.1 '1.1 with Patch 3.2.2, before 1.25
		tmp = tmp + TalentBlood.BloodGorged * 2
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
		tmp = tmp * (1- 20 *  sim.Buff.ArmorMajor / 100)
		tmp = tmp * (1- 5 *  sim.Buff.ArmorMinor / 100)
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
'	function getMitigation() as Double
'		dim l_bossArmor as double
'		dim l_constant as double  = 15232.5
'		l_bossArmor = 10643
'
'		Dim l_personalArpPercent As Double = ArmorPen
'		'l_personalArpPercent = l_personalArpPercent + (TalentBlood.BloodGorged * 2 / 100)
'		If l_personalArpPercent > 1 Then l_personalArpPercent = 1
'
'		dim l_debuffPercent as double = 0.0
'		dim l_sunder as double = 1.0
'		dim l_ff  as double = 1.0
'		if   sim.Buff.ArmorMajor > 0 then l_sunder = 1- 0.20
'		If  sim.Buff.ArmorMinor > 0 Then l_ff = 1 - 0.05
'		l_debuffPercent = 1 - (l_sunder * l_ff)
'
'		dim l_tempA as double = l_constant + l_bossArmor * (1.0 - l_debuffPercent)
'		dim l_termA  as double = (((1.0 - l_debuffPercent) * l_bossArmor + l_constant) / 3.0)
'		dim l_termB  as double = l_bossArmor * (1.0 - l_debuffPercent)
'		dim l_tempB  as double
'
'		if (l_termA < l_termB) then
'			l_tempB = l_termA * l_personalArpPercent
'		else
'			l_tempB = l_termB * l_personalArpPercent
'		end if
'
'		Dim l_answer  As Double = l_constant / (l_tempA - l_tempB)
''		dim l_answer2 as Double = GetArmorDamageReduction
''		debug.Print ("Methode 1 give "		& l_answer)
''		debug.Print ("Methode 2 give "		& l_answer2)
'		Return l_answer
'	End Function
	

	Function getMitigation() As Double
		If _LastArP = ArmorPen and _Mitigation <>0 Then Return _Mitigation

		Dim AttackerLevel As Integer = 80
		Dim tmpArmor As Integer
		Dim ArPDebuffs As Double
		dim l_sunder as double = 1.0
		dim l_ff  as double = 1.0
		if sim.Buff.ArmorMajor > 0 then l_sunder = 1- 0.20
		If sim.Buff.ArmorMinor > 0 Then l_ff = 1 - 0.05
		ArPDebuffs = (l_sunder * l_ff)
        dim ArmorConstant as Double = 400 + (85 * 80) + 4.5 * 85 * (80 - 59)
        tmpArmor = BossArmor  *  ArPDebuffs
        dim ArPCap as Double = Math.Min((tmpArmor + ArmorConstant) / 3, tmpArmor)
        tmpArmor = tmpArmor -  ArPCap * Math.Min(1,ArmorPen)
        _Mitigation = ArmorConstant / (ArmorConstant + tmpArmor)
        return _Mitigation
      end function
	
	
	
	
	
	Function WhiteHitDamageMultiplier(T as long) As Double
		dim tmp as Double
		tmp = 1
		tmp = tmp * (1 + BloodPresence * 0.15)
		tmp = tmp * (1 + 0.03 *  sim.Buff.PcDamage)
		If sim.Desolation.isActive(T) Then tmp = tmp * (1+sim.Desolation.Bonus)
		tmp = tmp * (1 + 0.02 * sim.BoneShield.Value(T))
		tmp = tmp * (1 + 0.02 * TalentBlood.BloodGorged)
		
		tmp = tmp * getMitigation
		tmp = tmp * (1 + 0.04 *  sim.Buff.PhysicalVuln)
		tmp = tmp * (1 + 0.03 * TalentBlood.BloodyVengeance)
		If sim.proc.T104PDPSFAde >= T Then
			tmp = tmp * 1.03
		End If
		if sim.Hysteria.IsActive(T) then tmp = tmp * 1.2
		
		return tmp
	End Function
	Function StandardPhysicalDamageMultiplier(T as long) As Double
		dim tmp as Double
		tmp = 1
		tmp = tmp * (1 + BloodPresence * 0.15)
		tmp = tmp * (1 + 0.03 *  sim.Buff.PcDamage)
		If sim.Desolation.isActive(T) Then tmp = tmp * (1+sim.Desolation.Bonus)
		tmp = tmp * (1 + 0.02 * sim.BoneShield.Value(T))
		tmp = tmp * (1 + 0.02 * TalentBlood.BloodGorged)
		
		tmp = tmp * getMitigation
		tmp = tmp * (1 + 0.04 *  sim.Buff.PhysicalVuln)
		tmp = tmp * (1 + 0.03 * TalentBlood.BloodyVengeance)
		If sim.Hysteria.IsActive(T) Then tmp = tmp * 1.2
		
		If sim.FrostFever.isActive(T) Then	tmp = tmp * (1 + 0.03 * TalentFrost.TundraStalker)
		If sim.BloodPlague.isActive(T) Then tmp = tmp * (1 + 0.02 * talentunholy.RageofRivendare)
		If sim.proc.T104PDPSFAde >= T Then tmp = tmp * 1.03
		
		return tmp
	End Function
	Function StandardMagicalDamageMultiplier(T as long) As Double
		Dim tmp As Double
		tmp = 1
		tmp = tmp * (1 + BloodPresence * 0.15)
		tmp = tmp * (1 + 0.03 *  sim.Buff.PcDamage)
		If sim.Desolation.isActive(T) Then tmp = tmp * (1+sim.Desolation.Bonus)
		tmp = tmp * (1 + 0.02 * sim.BoneShield.Value(T))
		tmp = tmp * (1 + 0.02 * TalentBlood.BloodGorged)
		
		If sim.FrostFever.isActive(T) Then	tmp = tmp * (1 + 0.03 * TalentFrost.TundraStalker)
		If sim.BloodPlague.isActive(T) Then tmp = tmp * (1 + 0.02 * talentunholy.RageofRivendare)
		if sim.proc.T104PDPSFAde >= T then tmp = tmp * 1.03
		tmp = tmp * (1 + 0.13 *  sim.Buff.SpellDamageTaken)
		tmp = tmp * (1-0.05) 'Average partial resist
		
		return tmp
	End Function
End Class
