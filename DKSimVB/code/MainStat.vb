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
	Private character As Character
	private XmlCharacter As Xml.XmlDocument
	
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
	
	Friend T92PTNK As integer
	Friend T102PTNK As Integer
	
	
	
	Private _Mitigation As Double
	Private _LastArP as Double
	
	
	Protected Sim As Sim
	Private _MaxAp as Integer
	
	
	
	
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
		
		XmlCharacter = character.XmlDoc
		
		
		try
			MHWeaponDPS = (XmlCharacter.SelectSingleNode("//character/weapon/mainhand/dps").InnerText).Replace(".",System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
			If sim.EPStat="EP WeaponDPS" Then
				MHWeaponDPS = MHWeaponDPS + 10
			End If
			
			If InStr(sim.EPStat,"ScaDPSA") Then
				MHWeaponDPS += Replace(sim.EPStat,"ScaDPSA","")
			End If
			
			
			MHWeaponSpeed = (XmlCharacter.SelectSingleNode("//character/weapon/mainhand/speed").InnerText).Replace(".",System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
			If sim.EPStat="EP WeaponSpeed" Then
				MHWeaponSpeed = MHWeaponSpeed + 0.1
			End If
		Catch
			Msgbox("Error reading MH Weapon characteristics")
		End Try
		
		try
			OHWeaponDPS = (XmlCharacter.SelectSingleNode("//character/weapon/offhand/dps").InnerText).Replace(".",System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
			OHWeaponSpeed = (XmlCharacter.SelectSingleNode("//character/weapon/offhand/speed").InnerText).Replace(".",System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
		Catch
			debug.Print("Error reading OH Weapon characteristics")
		End Try
		BossArmor = 10643
		
		If XmlCharacter.SelectSingleNode("//character/misc/ChaoticSkyflareDiamond").InnerText = True Then
			CSD = 1
		Else
			CSD = 0
		End If
		
		'Trinkets
		Sim.Trinkets = New Trinkets(Sim)
		
		
		
		try
			Select Case sim._EPStat
				Case "EP NoTrinket"
				Case "EP AttackPowerNoTrinket"
				Case "EP MjolnirRunestone"
					Sim.Trinkets.MjolRune.Equip
				Case "EP GrimToll"
					Sim.Trinkets.GrimToll.Equip
				Case "EP BitterAnguish"
					Sim.Trinkets.BitterAnguish.Equip
				Case "EP Mirror"
					Sim.Trinkets.Mirror.Equip
				Case "EP Greatness"
					Sim.Trinkets.Greatness.Equip
				Case "EP DCDeath"
					Sim.Trinkets.DCDeath.Equip
				Case "EP Victory"
					Sim.Trinkets.Victory.Equip
				Case "EP Necromantic"
					Sim.Trinkets.Necromantic.Equip
				Case "EP Bandit"
					Sim.Trinkets.Bandit.Equip
				Case "EP Pyrite"
					Sim.Trinkets.Pyrite.Equip
				Case "EP DarkMatter"
					Sim.Trinkets.DarkMatter.Equip
				Case "EP OldGod"
					Sim.Trinkets.OldGod.Equip
				Case "EP Comet"
					Sim.Trinkets.Comet.Equip
				Case "EP DeathChoice"
					Sim.Trinkets.DeathChoice.Equip
				Case "EP DeathChoiceHeroic"
					Sim.Trinkets.DeathChoiceHeroic.Equip
				Case "EP DeathbringersWill"
					Sim.Trinkets.DeathbringersWill.Equip
				Case "EP TinyAbomination"
					sim.Trinkets.TinyAbomination.Equip
				Case "EP DeathbringersWillHeroic"
					Sim.Trinkets.DeathbringersWillHeroic.Equip
				Case "EP WhisperingFangedSkull"
					Sim.Trinkets.WhisperingFangedSkull.Equip
				Case "EP WhisperingFangedSkullHeroic"
					Sim.Trinkets.WhisperingFangedSkullHeroic.Equip
				Case "EP NeedleEncrustedScorpion"
					Sim.Trinkets.NeedleEncrustedScorpion.Equip
				Case "EP HerkumlWarToken"
					Sim.Trinkets.HerkumlWarToken.Equip
				Case "EP MarkofSupremacy"
					Sim.Trinkets.MarkofSupremacy.Equip
				Case "EP VengeanceoftheForsaken"
					Sim.Trinkets.VengeanceoftheForsaken.Equip
				Case "EP VengeanceoftheForsakenHeroic"
					Sim.Trinkets.VengeanceoftheForsakenHeroic.Equip
					
					
					
				Case Else
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/MjolnirRunestone").InnerText = 1 Then Sim.Trinkets.MjolRune.Equip
					Catch
					End Try
					Try
						if XmlCharacter.SelectSingleNode("//character/trinket/GrimToll").InnerText = 1 then Sim.Trinkets.GrimToll.Equip
					Catch
					End Try
					Try
						if XmlCharacter.SelectSingleNode("//character/trinket/BitterAnguish").InnerText = 1 then Sim.Trinkets.BitterAnguish.Equip
					Catch
					End Try
					Try
						if XmlCharacter.SelectSingleNode("//character/trinket/Mirror").InnerText = 1 then Sim.Trinkets.Mirror.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/Greatness").InnerText = 1 Then Sim.Trinkets.Greatness.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/DCDeath").InnerText = 1 Then Sim.Trinkets.DCDeath.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/Victory").InnerText = 1 Then Sim.Trinkets.Victory.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/Necromantic").InnerText = 1 Then Sim.Trinkets.Necromantic.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/Bandit").InnerText = 1 Then Sim.Trinkets.Bandit.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/Pyrite").InnerText = 1 Then Sim.Trinkets.Pyrite.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/DarkMatter").InnerText = 1 Then Sim.Trinkets.DarkMatter.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/OldGod").InnerText = 1 Then Sim.Trinkets.OldGod.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/Comet").InnerText = 1 Then Sim.Trinkets.Comet.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/DeathChoice").InnerText = 1 Then Sim.Trinkets.DeathChoice.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/DeathChoiceHeroic").InnerText = 1 Then Sim.Trinkets.DeathChoiceHeroic.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/DeathbringersWill").InnerText = 1 Then Sim.Trinkets.DeathbringersWill.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/DeathbringersWillHeroic").InnerText = 1 Then Sim.Trinkets.DeathbringersWillHeroic.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/WhisperingFangedSkull").InnerText = 1 Then Sim.Trinkets.WhisperingFangedSkull.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/WhisperingFangedSkullHeroic").InnerText = 1 Then Sim.Trinkets.WhisperingFangedSkullHeroic.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/NeedleEncrustedScorpion").InnerText = 1 Then Sim.Trinkets.NeedleEncrustedScorpion.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/TinyAbomination").InnerText = 1 Then Sim.Trinkets.TinyAbomination.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/HerkumlWarToken").InnerText = 1 Then Sim.Trinkets.HerkumlWarToken.Equip
					Catch
					End Try
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/MarkofSupremacy").InnerText = 1 Then Sim.Trinkets.MarkofSupremacy.Equip
					Catch
					End Try
					
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/VengeanceoftheForsaken").InnerText = 1 Then Sim.Trinkets.VengeanceoftheForsaken.Equip
					Catch
					End Try
					
					Try
						If XmlCharacter.SelectSingleNode("//character/trinket/VengeanceoftheForsakenHeroic").InnerText = 1 Then Sim.Trinkets.VengeanceoftheForsakenHeroic.Equip
					Catch
					End Try
					
					
					
			End Select
		Catch
			debug.Print ("ERROR init trinket")
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
			Case "EP 0T7"
			Case "EP AttackPower0T7"
			Case "EP 2T7"
				T72PDPS = 1
			Case "EP 4T7"
				T74PDPS = 1
			Case "EP 2T8"
				T82PDPS = 1
			Case "EP 4T8"
				T84PDPS = 1
			Case "EP 2T9"
				T92PDPS = 1
			Case "EP 4T9"
				T94PDPS = 1
			Case "EP 2T10"
				T102PDPS = 1
			Case "EP 4T10"
				T104PDPS = 1
			Case Else
				Try
					T72PDPS = XmlCharacter.SelectSingleNode("//character/Set/T72PDPS").InnerText
				Catch
				End Try
				Try
					T74PDPS = XmlCharacter.SelectSingleNode("//character/Set/T74PDPS").InnerText
				Catch
				End Try
				Try
					T82PDPS = XmlCharacter.SelectSingleNode("//character/Set/T82PDPS").InnerText
				Catch
				End Try
				Try
					T84PDPS = XmlCharacter.SelectSingleNode("//character/Set/T84PDPS").InnerText
				Catch
				End Try
				Try
					
					T92PDPS = XmlCharacter.SelectSingleNode("//character/Set/T92PDPS").InnerText
				Catch
				End Try
				Try
					T94PDPS = XmlCharacter.SelectSingleNode("//character/Set/T94PDPS").InnerText
				Catch
				End Try
				Try
					
					T72PTNK = XmlCharacter.SelectSingleNode("//character/Set/T72PTNK").InnerText
				Catch
				End Try
				Try
					T74PTNK = XmlCharacter.SelectSingleNode("//character/Set/T74PTNK").InnerText
				Catch
				End Try
				Try
					T82PTNK = XmlCharacter.SelectSingleNode("//character/Set/T82PTNK").InnerText
				Catch
				End Try
				Try
					T84PTNK = XmlCharacter.SelectSingleNode("//character/Set/T84PTNK").InnerText
				Catch
				End Try
				Try
					T102PDPS = XmlCharacter.SelectSingleNode("//character/Set/T102PDPS").InnerText
				Catch
				End Try
				Try
					T104PDPS = XmlCharacter.SelectSingleNode("//character/Set/T104PDPS").InnerText
				Catch
				End Try
				Try
					
					T92PTNK = XmlCharacter.SelectSingleNode("//character/Set/T92PTNK").InnerText
				Catch
				End Try
				Try
					T102PTNK = XmlCharacter.SelectSingleNode("//character/Set/T102PTNK").InnerText
				Catch
				End Try
				
		End Select
		
		
	End Sub
	
	Function BaseAP() As Integer
		Dim tmp As Integer
		tmp += sim.proc.GetActiveBonus("ap")
		tmp = tmp + Character.AttackPower
		tmp = tmp + Character.Strength * 2
		tmp = tmp + 550
		tmp = tmp * (1 +  sim.Buff.AttackPowerPc / 10)
		return tmp
	End Function
	
	Function GetMaxAP As Integer
		Dim tmp As Integer
		If _MaxAp <> 0 Then Return _MaxAp
		tmp += sim.proc.GetMaxPossibleBonus("ap")
		tmp += Character.AttackPower
		tmp += Character.maxStrength * 2
		tmp += 550
		tmp = tmp * (1 +  sim.Buff.AttackPowerPc / 10)
		_MaxAp = tmp
		return tmp
	End Function
	
	
	
	Function AP() As Integer
		return  BaseAP
	End Function
	
	Function crit() As System.Double
		Dim tmp As Double
		tmp = tmp + Character.CritRating / 45.91
		tmp = tmp + Character.Agility*0.016
		tmp = tmp + 5 *  sim.Buff.MeleeCrit
		tmp = tmp + 3 *  sim.Buff.CritChanceTaken
		tmp = tmp + sim.TalentBlood.Darkconv
		tmp = tmp + sim.TalentUnholy.EbonPlaguebringer
		tmp = tmp + sim.TalentFrost.Annihilation
		tmp = tmp - 4.8 'Crit malus vs bosses
		
		return tmp / 100
	End Function
	Function critAutoattack() As System.Double 'No Annihilation for autoattacks
		Dim tmp As Double
		tmp = tmp + Character.CritRating / 45.91
		tmp = tmp + Character.Agility / 62.5
		tmp = tmp + 5 *  sim.Buff.MeleeCrit
		tmp = tmp + 3 *  sim.Buff.CritChanceTaken
		tmp = tmp + sim.TalentBlood.Darkconv
		tmp = tmp + sim.TalentUnholy.EbonPlaguebringer
		tmp = tmp - 4.8 'Crit malus vs bosses
		
		return tmp / 100
	End Function
	Function SpellCrit() As Single
		Dim tmp As Double
		tmp = Character.SpellCritRating / 45.91
		tmp = tmp + 3 *  sim.Buff.CritChanceTaken
		tmp = tmp + 5 *  sim.Buff.SpellCrit
		tmp = tmp + 5 *  sim.Buff.SpellCritTaken
		tmp = tmp + sim.TalentBlood.Darkconv
		tmp = tmp + sim.TalentUnholy.EbonPlaguebringer
		tmp = tmp - 2.1 'Spell crit malus vs bosses
		
		return  tmp / 100
	End Function
	
	Function Haste() As Double
		Dim tmp As Double
		tmp = 1 + character.HasteRating / (25.22) / 100 '1.3 is the buff haste rating received
		tmp = tmp * (1 + Sim.UnholyPresence * 0.15)
		If Sim.TalentFrost.ImprovedIcyTalons Then tmp = tmp * 1.05
		If Sim.proc.IcyTalons.IsActive Then tmp = tmp * (1 + 0.04 * Sim.proc.IcyTalons.ProcValue)
		If Sim.Buff.MeleeHaste Then tmp = tmp * 1.2
		If Sim.Buff.Haste Then tmp = tmp * 1.03
		If Sim.proc.Bloodlust.IsActive Then tmp = tmp * 1.3
		If Sim.proc.TrollRacial.IsActive Then tmp = tmp * 1.2
		Return tmp
	End Function
	Function SpellHaste() As Double
		Dim tmp As Double
		tmp = 1 + character.SpellHasteRating / 25.22 / 100
		If Sim.Buff.SpellHaste Then tmp = tmp * 1.05
		If Sim.Buff.Haste Then tmp = tmp * 1.03
		If Sim.proc.Bloodlust.IsActive Then tmp = tmp * 1.3
		Return tmp
	End Function
	Function EstimatedHasteBonus As Double
		Dim tmp As Double
		tmp = 1 + (character.HasteRating + Sim.EPBase) / 25.22 / 100 'Haste change for 3.1 ?
		Return tmp / (1 + character.HasteRating / 25.22 / 100)
	End Function
	Function MHExpertise() As Double
		Dim tmp As Double
		tmp = Expertise
		If strings.InStr(sim.EPStat,"EP Expertise")<> 0 Then
		Else
			tmp += (sim.Character.MHExpertiseBonus*0.25/100)
		End If
		return tmp
	End Function
	
	Function OHExpertise() As Double
		Dim tmp As Double
		tmp = Expertise
		If strings.InStr(sim.EPStat,"EP Expertise")<> 0 Then
		Else
			tmp += (sim.Character.OHExpertiseBonus*0.25/100)
		End If
		
		return tmp
	End Function
	
	
	
	
	Function Expertise() As Double
		Dim tmp As Double
		
		tmp = Character.ExpertiseRating / 32.79
		tmp += 0.25 * sim.TalentBlood.Vot3W*2
		tmp += 0.25 * sim.TalentFrost.TundraStalker
		tmp += 0.25 * sim.TalentUnholy.RageofRivendare
		
		Select Case Sim.EPStat
			Case ""
			Case "EP ExpertiseRating"
				tmp = 6.5 - sim.EPBase / 32.79
			Case "EP ExpertiseRatingCap"
				tmp = 6.5
			Case "EP ExpertiseRatingCapAP"
				tmp = 6.5
			Case "EP ExpertiseRatingAfterCap"
				tmp = 6.5 + sim.EPBase / 32.79
			Case "EP RelativeExpertiseRating"
				tmp += sim.EPBase / 32.79
			Case Else
				If InStr(sim.EPStat,"ScaExp") Then
					If InStr(sim.EPStat,"ScaExpA") Then
						tmp = tmp +  Replace(sim.EPStat,"ScaExpA","") * sim.EPBase /  32.79
					Else
						tmp =  Replace(sim.EPStat,"ScaExp","") * sim.EPBase /  32.79
					End If
				End If
		End Select
		
		return  tmp / 100
	End Function
	Function Hit() As Double
		Dim tmp As Double
		tmp = (Character.HitRating / 32.79)
		If DualW Then tmp += sim.TalentFrost.NervesofColdSteel
		
		If instr(sim.EPStat,"EP ")<>0 Then
			If instr(sim.EPStat,"Hit")=0 Then
				tmp += sim.Buff.Draenei
			Else
				If sim.EPStat = "EP RelativeHitRating" Then tmp += sim.Buff.Draenei
			End If
		Else
			tmp += sim.Buff.Draenei
		End If
		Hit = tmp / 100
	End Function
	
	Function SpellHitCapRating() as Integer
		dim tmp as integer
		tmp = 17
		tmp = tmp - sim.TalentUnholy.Virulence
		tmp = tmp - 3*sim.Buff.SpellHitTaken
		tmp = tmp * 26.23
		return tmp
	End Function
	
	
	
	Function SpellHit() As Double
		Dim tmp As Double
		tmp = Character.SpellHitRating / 26.23
		If instr(sim.EPStat,"EP ")<>0 Then
			If instr(sim.EPStat,"Hit")=0 Then
				tmp += sim.Buff.Draenei
			End If
		Else
			tmp += sim.Buff.Draenei
		End If
		tmp += 1 * sim.TalentUnholy.Virulence
		tmp += sim.Buff.SpellHitTaken * 3
		SpellHit = tmp / 100
	End Function
	
	Function NormalisedMHDamage() As Double
		Dim tmp As Double
		tmp =  MHWeaponSpeed * MHWeaponDPS
		If DualW Then
			tmp =  tmp + 2.4*(AP / 14)
		Else
			tmp =  tmp + 3.3*(AP / 14)
			tmp = tmp * (1 + sim.TalentBlood.Weapspec * 2 / 100)
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
		If DualW = false Then tmp = tmp * (1 + sim.TalentBlood.Weapspec * 2 / 100)
		return tmp
	End Function
	Function OHBaseDamage() As Double
		OHBaseDamage = (OHWeaponDPS + (AP / 14)) * OHWeaponSpeed
	End Function
	Function ArmorPen As Double
		Dim tmp As Double
		tmp = character.ArmorPenetrationRating / 15.39
		tmp = tmp *1.1 '1.1 with Patch 3.2.2, before 1.25
		tmp = tmp + sim.TalentBlood.BloodGorged * 2
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
		tmp = tmp * (1 - sim.TalentBlood.BloodGorged * 2 / 100)
		tmp = 1
		x = ArmorPen
		
		y = tmp
		z = (A+B)/3 / B
		
		Dim retour As Double
		retour = A/(A+B*(1-(z*x+y)+B/(A+B)*x*(1-y)*z))
		Return (1.0 - Math.max(0.0, retour))
	End Function
	
	
	Function getMitigation() As Double
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
	
	
	
	Function _BaseDamageMultiplier(ByVal T As Long) As Double
		Dim tmp As Double
		tmp = 1 + Sim.BloodPresence * 0.15
		tmp = tmp * (1 + 0.03 * Sim.Buff.PcDamage)
		tmp = tmp * (1 + 0.02 * Sim.BoneShield.Value(T))
		tmp = tmp * (1 + 0.02 * Sim.TalentBlood.BloodGorged)
		If Sim.proc.Desolation.IsActiveAt(T) Then tmp = tmp * (1 + Sim.proc.Desolation.ProcValue * 0.01)
		If Sim.proc.T104PDPS.IsActiveAt(T) Then tmp = tmp * 1.03
		Return tmp
	End Function

	Function WhiteHitDamageMultiplier(ByVal T As Long) As Double
		Dim tmp As Double
		tmp = _BaseDamageMultiplier(T) * getMitigation()
		tmp = tmp * (1 + 0.04 * Sim.Buff.PhysicalVuln)
		tmp = tmp * (1 + 0.03 * Sim.TalentBlood.BloodyVengeance)
		If Sim.Hysteria.IsActive(T) Then tmp = tmp * 1.2
		Return tmp
	End Function
	Function StandardPhysicalDamageMultiplier(ByVal T As Long) As Double
		Dim tmp As Double
		tmp = WhiteHitDamageMultiplier(T)
		If Sim.FrostFever.isActive(T) Or Sim.Buff.FrostFever = 1 Then tmp = tmp * (1 + 0.03 * Sim.TalentFrost.TundraStalker)
		If Sim.BloodPlague.isActive(T) Or Sim.Buff.BloodPlague = 1 Then tmp = tmp * (1 + 0.02 * Sim.TalentUnholy.RageofRivendare)
		Return tmp
	End Function
	Function StandardMagicalDamageMultiplier(ByVal T As Long) As Double
		Dim tmp As Double
		tmp = _BaseDamageMultiplier(T)
		If Sim.FrostFever.isActive(T) Or Sim.Buff.FrostFever = 1 Then tmp = tmp * (1 + 0.03 * Sim.TalentFrost.TundraStalker)
		If Sim.BloodPlague.isActive(T) Or Sim.Buff.BloodPlague = 1 Then tmp = tmp * (1 + 0.02 * Sim.TalentUnholy.RageofRivendare)
		tmp = tmp * (1 + 0.13 * Sim.Buff.SpellDamageTaken)
		tmp = tmp * (1 - 15 / (510 + 15)) 'Partial Resistance. It's about 0,029% less damage on average.

		Return tmp
	End Function
End Class
