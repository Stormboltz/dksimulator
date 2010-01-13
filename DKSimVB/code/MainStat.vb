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
		dim XmlDoc As New Xml.XmlDocument
		XmlDoc = character.XmlDoc
		'XmlDoc.Load(GetFilePath(sim._MainFrm.cmbCharacter.Text) )
		
		MHWeaponDPS = (XmlDoc.SelectSingleNode("//character/weapon/mainhand/dps").InnerText).Replace(".",System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
		If sim.EPStat="EP WeaponDPS" Then
			MHWeaponDPS = MHWeaponDPS + 10
		End If
		
		If InStr(sim.EPStat,"ScaDPSA") Then
			MHWeaponDPS += Replace(sim.EPStat,"ScaDPSA","")
		End If
		
		
		MHWeaponSpeed = (XmlDoc.SelectSingleNode("//character/weapon/mainhand/speed").InnerText).Replace(".",System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
		If sim.EPStat="EP WeaponSpeed" Then
			MHWeaponSpeed = MHWeaponSpeed + 0.1
		End If
		
		
		
		
		
		OHWeaponDPS = (XmlDoc.SelectSingleNode("//character/weapon/offhand/dps").InnerText).Replace(".",System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
		OHWeaponSpeed = (XmlDoc.SelectSingleNode("//character/weapon/offhand/speed").InnerText).Replace(".",System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
		BossArmor = 10643
		
		
		
		
		
		
		
		
		
		If XmlDoc.SelectSingleNode("//character/misc/ChaoticSkyflareDiamond").InnerText = True Then
			CSD = 1
		Else
			CSD = 0
		End If

		'Trinkets
		Sim.Trinkets = New Trinkets(Sim)
		Sim.Trinkets.MHRazorIce.ProcValue = MHWeaponDPS * MHWeaponSpeed * 0.02
		Sim.Trinkets.OHRazorIce.ProcValue = MHWeaponDPS * MHWeaponSpeed * 0.02

		Try
			If XmlDoc.SelectSingleNode("//character/WeaponProc/MHSingedViskag").InnerText = 1 Then sim.trinkets.MHSingedViskag.Equip
			if XmlDoc.SelectSingleNode("//character/WeaponProc/MHtemperedViskag").InnerText= 1 then sim.trinkets.MHtemperedViskag.Equip
			If XmlDoc.SelectSingleNode("//character/WeaponProc/OHtemperedViskag").InnerText = 1 Then sim.trinkets.OHtemperedViskag.Equip
			If XmlDoc.SelectSingleNode("//character/WeaponProc/OHSingedViskag").InnerText = 1 Then sim.trinkets.OHSingedViskag.Equip
		Catch
		End Try
		Try
			if XmlDoc.SelectSingleNode("//character/WeaponProc/MHRagingDeathbringer").InnerText = 1 then sim.trinkets.MHRagingDeathbringer.Equip
			if XmlDoc.SelectSingleNode("//character/WeaponProc/OHRagingDeathbringer").InnerText = 1 then sim.trinkets.OHRagingDeathbringer.Equip
			if XmlDoc.SelectSingleNode("//character/WeaponProc/MHEmpoweredDeathbringer").InnerText = 1 then sim.trinkets.MHEmpoweredDeathbringer.Equip
			if XmlDoc.SelectSingleNode("//character/WeaponProc/OHEmpoweredDeathbringer").InnerText = 1 then sim.trinkets.OHEmpoweredDeathbringer.Equip
		Catch
		End Try
		Try
			if XmlDoc.SelectSingleNode("//character/WeaponProc/MHBryntroll").InnerText = 1 then sim.trinkets.Bryntroll.Equip
		Catch
			
		End Try
		
		
		Try
			If XmlDoc.SelectSingleNode("//character/misc/HandMountedPyroRocket").InnerText= True Then
				sim.trinkets.HandMountedPyroRocket.Equip
			End If
			
			If XmlDoc.SelectSingleNode("//character/misc/HyperspeedAccelerators").InnerText= True Then
				sim.trinkets.HyperspeedAccelerators.Equip
			End If

			If XmlDoc.SelectSingleNode("//character/misc/TailorEnchant").InnerText= True Then
				sim.trinkets.TailorEnchant.Equip
			End If
			
			If XmlDoc.SelectSingleNode("//character/misc/AshenBand").InnerText= True Then
				sim.trinkets.AshenBand.Equip
			End If
		Catch
		End Try
		try
			Select Case sim._EPStat
				Case "EP NoTrinket"
				Case "EP AttackPowerNoTrinket"
				Case "EP MjolRune"
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
				Case "EP NeedleEncrustedScorpion"
					Sim.Trinkets.NeedleEncrustedScorpion.Equip
				Case Else
					If XmlDoc.SelectSingleNode("//character/trinket/MjolnirRunestone").InnerText = 1 Then Sim.Trinkets.MjolRune.Equip
					if XmlDoc.SelectSingleNode("//character/trinket/GrimToll").InnerText = 1 then Sim.Trinkets.GrimToll.Equip
					if XmlDoc.SelectSingleNode("//character/trinket/BitterAnguish").InnerText = 1 then Sim.Trinkets.BitterAnguish.Equip
					if XmlDoc.SelectSingleNode("//character/trinket/Mirror").InnerText = 1 then Sim.Trinkets.Mirror.Equip
					If XmlDoc.SelectSingleNode("//character/trinket/Greatness").InnerText = 1 Then Sim.Trinkets.Greatness.Equip
					if XmlDoc.SelectSingleNode("//character/trinket/DCDeath").InnerText = 1 then Sim.Trinkets.DCDeath.Equip
					if XmlDoc.SelectSingleNode("//character/trinket/Victory").InnerText = 1 then Sim.Trinkets.Victory.Equip
					if XmlDoc.SelectSingleNode("//character/trinket/Necromantic").InnerText = 1 then Sim.Trinkets.Necromantic.Equip
					if XmlDoc.SelectSingleNode("//character/trinket/Bandit").InnerText = 1 then Sim.Trinkets.Bandit.Equip
					if XmlDoc.SelectSingleNode("//character/trinket/Pyrite").InnerText = 1 then Sim.Trinkets.Pyrite.Equip
					if XmlDoc.SelectSingleNode("//character/trinket/DarkMatter").InnerText = 1 then Sim.Trinkets.DarkMatter.Equip
					if XmlDoc.SelectSingleNode("//character/trinket/OldGod").InnerText = 1 then Sim.Trinkets.OldGod.Equip
					if XmlDoc.SelectSingleNode("//character/trinket/Comet").InnerText = 1 then Sim.Trinkets.Comet.Equip
					if XmlDoc.SelectSingleNode("//character/trinket/DeathChoice").InnerText = 1 then Sim.Trinkets.DeathChoice.Equip
					if XmlDoc.SelectSingleNode("//character/trinket/DeathChoiceHeroic").InnerText = 1 then Sim.Trinkets.DeathChoiceHeroic.Equip
					if XmlDoc.SelectSingleNode("//character/trinket/DeathbringersWill").InnerText = 1 then Sim.Trinkets.DeathbringersWill.Equip
					If XmlDoc.SelectSingleNode("//character/trinket/DeathbringersWillHeroic").InnerText = 1 Then Sim.Trinkets.DeathbringersWillHeroic.Equip
					If XmlDoc.SelectSingleNode("//character/trinket/WhisperingFangedSkull").InnerText = 1 Then Sim.Trinkets.WhisperingFangedSkull.Equip
					If XmlDoc.SelectSingleNode("//character/trinket/NeedleEncrustedScorpion").InnerText = 1 Then Sim.Trinkets.NeedleEncrustedScorpion.Equip
					If XmlDoc.SelectSingleNode("//character/trinket/TinyAbomination").InnerText = 1 Then Sim.Trinkets.TinyAbomination.Equip

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

					T92PTNK = XmlDoc.SelectSingleNode("//character/Set/T92PTNK").InnerText
					T102PTNK = XmlDoc.SelectSingleNode("//character/Set/T102PTNK").InnerText
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
	
	Function GetMAxAP As Integer
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
		tmp = Character.HasteRating / (32.79/1.3) / 100 '1.3 is the buff haste rating received
		tmp = tmp + sim.UnholyPresence * 0.15
		tmp = tmp + 0.05 * sim.TalentFrost.ImprovedIcyTalons
		tmp = tmp + 0.2 *  sim.Buff.MeleeHaste
		tmp = tmp + 0.03 *  sim.Buff.Haste
		If sim.Bloodlust.IsActive(sim.TimeStamp) Then tmp = tmp + 0.3
		if sim.proc.TrollRacial.IsActive then tmp = tmp * 1.2
		return tmp
	End Function
	Function SpellHaste() As Double
		Dim tmp As Double
		If sim.UnholyPresence = 1 Then
			SpellHaste = 0.5
		Else
			tmp = Character.SpellHasteRating / 32.79 / 100
			tmp = tmp + 0.05 * sim.Buff.SpellHaste
			tmp = tmp + 0.03 * sim.Buff.Haste
			If sim.Bloodlust.IsActive(sim.TimeStamp)  Then	tmp = tmp + 0.3
			If Sim.Trinkets.Comet.IsActive Then tmp = tmp + Sim.Trinkets.Comet.ProcValue/(32.79/1.3)/100
			If Sim.Trinkets.BitterAnguish.IsActive Then tmp = tmp + Sim.Trinkets.BitterAnguish.ProcValue/(32.79/1.3)/100
			return tmp
		End If
	End Function
	
	
	Function MHExpertise() As Double
		Dim tmp As Double
		tmp = Expertise
		If strings.InStr(sim.EPStat,"EP Expertise")<> 0 Then
		Else
			tmp += sim.Character.MHExpertiseBonus/0.25
		End If
		return tmp
	End Function
	
	Function OHExpertise() As Double
		Dim tmp As Double
		tmp = Expertise
		If strings.InStr(sim.EPStat,"EP Expertise")<> 0 Then
		Else
			tmp += sim.Character.OHExpertiseBonus/0.25
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

		If instr(sim.EPStat,"EP ")=0 Then
			If instr(sim.EPStat,"Hit")=0 Then
				tmp += sim.Buff.Draenei
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
		If instr(sim.EPStat,"EP ")=0 Then
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
	
	
	
	
	
	Function WhiteHitDamageMultiplier(T as long) As Double
		dim tmp as Double
		tmp = 1
		tmp = tmp * (1 + sim.BloodPresence * 0.15)
		tmp = tmp * (1 + 0.03 *  sim.Buff.PcDamage)
		If sim.Desolation.isActive(T) Then tmp = tmp * (1+sim.Desolation.Bonus)
		tmp = tmp * (1 + 0.02 * sim.BoneShield.Value(T))
		tmp = tmp * (1 + 0.02 * sim.TalentBlood.BloodGorged)
		
		tmp = tmp * getMitigation
		tmp = tmp * (1 + 0.04 *  sim.Buff.PhysicalVuln)
		tmp = tmp * (1 + 0.03 * sim.TalentBlood.BloodyVengeance)
		If sim.proc.T104PDPSFAde >= T Then
			tmp = tmp * 1.03
		End If
		if sim.Hysteria.IsActive(T) then tmp = tmp * 1.2
		
		return tmp
	End Function
	Function StandardPhysicalDamageMultiplier(T as long) As Double
		dim tmp as Double
		tmp = 1
		tmp = tmp * (1 + sim.BloodPresence * 0.15)
		tmp = tmp * (1 + 0.03 *  sim.Buff.PcDamage)
		If sim.Desolation.isActive(T) Then tmp = tmp * (1+sim.Desolation.Bonus)
		tmp = tmp * (1 + 0.02 * sim.BoneShield.Value(T))
		tmp = tmp * (1 + 0.02 * sim.TalentBlood.BloodGorged)
		
		tmp = tmp * getMitigation
		tmp = tmp * (1 + 0.04 *  sim.Buff.PhysicalVuln)
		tmp = tmp * (1 + 0.03 * sim.TalentBlood.BloodyVengeance)
		If sim.Hysteria.IsActive(T) Then tmp = tmp * 1.2
		
		If sim.FrostFever.isActive(T) or sim.Buff.FrostFever = 1 Then	tmp = tmp * (1 + 0.03 * sim.TalentFrost.TundraStalker)
		If sim.BloodPlague.isActive(T) or sim.Buff.BloodPlague = 1 Then tmp = tmp * (1 + 0.02 * sim.TalentUnholy.RageofRivendare)
		If sim.proc.T104PDPSFAde >= T Then tmp = tmp * 1.03
		
		return tmp
	End Function
	Function StandardMagicalDamageMultiplier(T as long) As Double
		Dim tmp As Double
		tmp = 1
		tmp = tmp * (1 + sim.BloodPresence * 0.15)
		tmp = tmp * (1 + 0.03 *  sim.Buff.PcDamage)
		If sim.Desolation.isActive(T) Then tmp = tmp * (1+sim.Desolation.Bonus)
		tmp = tmp * (1 + 0.02 * sim.BoneShield.Value(T))
		tmp = tmp * (1 + 0.02 * sim.TalentBlood.BloodGorged)
		
		If sim.FrostFever.isActive(T) or sim.Buff.FrostFever = 1 Then	tmp = tmp * (1 + 0.03 * sim.TalentFrost.TundraStalker)
		If sim.BloodPlague.isActive(T) or sim.Buff.BloodPlague = 1 Then tmp = tmp * (1 + 0.02 * sim.TalentUnholy.RageofRivendare)
		if sim.proc.T104PDPSFAde >= T then tmp = tmp * 1.03
		tmp = tmp * (1 + 0.13 *  sim.Buff.SpellDamageTaken)
		tmp = tmp * (1-15/(510+15)) 'Partial Resistance. It's about 0,029% less damage on average.
		
		return tmp
	End Function
End Class
