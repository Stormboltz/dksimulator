Friend Class Character
	
	
	
	Friend XmlDoc As New Xml.XmlDocument
	Friend XmlConfig As Xml.XmlDocument
	
	
	
	Private _Strength As Integer
	Private _Agility As Integer
	Private _Intel As Integer
	Private _Armor As Integer
	Private _AttackPower As Integer
	Private _HitRating As Integer
	Private _CritRating As Integer
	Private _HasteRating As Integer
	Private _ArmorPenetrationRating As Integer
	Private _ExpertiseRating As Integer
	Private _Dual As Integer
	Protected sim as Sim
	
	
	Friend MHExpertiseBonus As Integer
	Friend OHExpertiseBonus As Integer
	Friend Orc As Boolean
	Friend Troll As Boolean
	Friend BloodElf as Boolean
	
	
	
	Friend Buff as Buff
	Friend TalentBlood As TalentBlood
	Friend TalentFrost As TalentFrost
	Friend TalentUnholy as TalentUnholy
	Friend Glyph As Glyph


	Sub New(S As Sim)
		Sim = S
		Buff = new Buff(s)
		XmlConfig = Sim.XmlConfig
		Try
			If XmlConfig.SelectSingleNode("//config/UseCharacter").InnerText = True Then
				XmlDoc.Load (Application.StartupPath & "\Characters\"  & XmlConfig.SelectSingleNode("//config/Character").InnerText)
			Else
				XmlDoc.Load (Application.StartupPath & "\CharactersWithGear\"  & XmlConfig.SelectSingleNode("//config/CharacterWithGear").InnerText)
			End If
			loadtemplate (Application.StartupPath & "\Templates\" & XmlConfig.SelectSingleNode("//config/template").InnerText)
		Catch
			msgbox("Error finding Character config file")
		End Try
		Try
			_Strength = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/Strength").InnerText)
			_Agility = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/Agility").InnerText)
			_Intel = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/Intel").InnerText)
			_Armor = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/Armor").InnerText)
			_AttackPower = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/AttackPower").InnerText)
			_HitRating = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/HitRating").InnerText)
			_CritRating = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/CritRating").InnerText)
			_HasteRating = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/HasteRating").InnerText)
			_ArmorPenetrationRating = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/ArmorPenetrationRating").InnerText)
			_ExpertiseRating=int32.Parse(XmlDoc.SelectSingleNode("//character/stat/ExpertiseRating").InnerText)
			_Dual = int32.Parse(XmlDoc.SelectSingleNode("//character/weapon/count").InnerText)

			MHExpertiseBonus = int32.Parse(XmlDoc.SelectSingleNode("//character/racials/MHExpertiseBonus").InnerText)
			OHExpertiseBonus = int32.Parse(XmlDoc.SelectSingleNode("//character/racials/OHExpertiseBonus").InnerText)
			Orc = XmlDoc.SelectSingleNode("//character/racials/Orc").InnerText
			Troll = XmlDoc.SelectSingleNode("//character/racials/Troll").InnerText
			BloodElf = XmlDoc.SelectSingleNode("//character/racials/BloodElf").InnerText
		Catch
			debug.Print("Error reading Character config file.")
			msgbox("Error reading Character config file. You should open and check it. ")
		End Try
		sim.boss = New Boss(S)
	End Sub
	
	Function GetCharacterFileName() As String
		If XmlConfig.SelectSingleNode("//config/UseCharacter").InnerText = True Then
			Return XmlConfig.SelectSingleNode("//config/Character").InnerText
		Else
			Return XmlConfig.SelectSingleNode("//config/CharacterWithGear").InnerText
		End If
	End Function
	
	Function GetTemplateFileName() as String
		Return XmlConfig.SelectSingleNode("//config/template").InnerText
	End Function
	
	Function GetPriorityFileName() as String
		Return XmlConfig.SelectSingleNode("//config/priority").InnerText
	End Function
	
	Function GetRotationFileName() as String
		Return XmlConfig.SelectSingleNode("//config/rotation").InnerText
	End Function
	
	Function GetIntroFileName() as String
		Return XmlConfig.SelectSingleNode("//config/intro").InnerText
	End Function
	
	
	Function GetPresence() as String
		Return XmlConfig.SelectSingleNode("//config/presence").InnerText
	End Function
	
	Function GetSigil() as String
		Return XmlConfig.SelectSingleNode("//config/sigil").InnerText
	End Function
	
	Function GetMHEnchant() as String
		Return XmlConfig.SelectSingleNode("//config/mh").InnerText
	End Function
	
	Function GetOHEnchant() As String
		Return XmlConfig.SelectSingleNode("//config/oh").InnerText
	End Function
	
	Function GetPetCalculation() As Boolean
		Return XmlConfig.SelectSingleNode("//config/pet").InnerText
	End Function
	
	Function Strength() As Integer
		Dim tmp As Integer
		tmp = _Strength
		If sim.EPStat="EP Strength" Then tmp = tmp + sim.EPBase
		If InStr(sim.EPStat,"ScaStr") Then
			tmp = tmp + Replace(sim.EPStat,"ScaStr","") * sim.EPBase
		End If
		tmp += sim.proc.GetActiveBonus("str")
		tmp = tmp +155 * 1.15 * Buff.StrAgi
		tmp = tmp + 37 * 1.4 *  Buff.StatAdd
		tmp = tmp * (1 + Buff.StatMulti / 10)
		tmp = tmp * (1 + talentblood.Vot3W * 2 / 100)
		tmp = tmp * (1 + talentblood.AbominationMight / 100)
		tmp = tmp * (1 + talentunholy.ravenousdead / 100)
		tmp = tmp * (1 + talentfrost.EndlessWinter * 2 / 100)
		If sim.runeForge.CheckFallenCrusader Then
			tmp = tmp * 1.15
		End If
		if sim.UnbreakableArmor.isActive then tmp = tmp * 1.2
		return tmp
	End Function
	
	Function MaxStrength() As Integer
		Dim tmp As Integer
		tmp = _Strength
		If sim.EPStat="EP Strength" Then tmp = tmp + sim.EPBase
		If InStr(sim.EPStat,"ScaStr") Then
			tmp = tmp + Replace(sim.EPStat,"ScaStr","") * sim.EPBase
		End If
		tmp += sim.proc.GetMaxPossibleBonus("str")
		tmp = tmp +155 * 1.15 *  Buff.StrAgi
		tmp = tmp + 37 * 1.4 *  Buff.StatAdd
		tmp = tmp * (1 +  Buff.StatMulti / 10)
		tmp = tmp * (1 + talentblood.Vot3W * 2 / 100)
		tmp = tmp * (1 + talentblood.AbominationMight / 100)
		tmp = tmp * (1 + talentunholy.ravenousdead / 100)
		tmp = tmp * (1 + talentfrost.EndlessWinter * 2 / 100)
		If sim.RuneForge.HasFallenCrusader Then
			tmp = tmp * 1.15
		End If
		if sim.UnbreakableArmor.isActive then tmp = tmp * 1.1
		return tmp
	End Function
	
	
	
	Function Agility() As Integer
		Dim tmp As Integer
		tmp = _Agility
		If sim.EPStat="EP Agility" Then tmp = tmp +sim.EPBase
		If InStr(sim.EPStat,"ScaAgility") Then
			tmp = tmp + Replace(sim.EPStat,"ScaAgility","") * sim.EPBase
		End If
		tmp = (tmp + 155 * 1.15 *  Buff.StrAgi + 37 * 1.4  *  Buff.StatAdd) * (1 + Buff.StatMulti / 10)
		return tmp
	End Function
	
	Function Intel() As Integer
		Dim tmp As Integer
		tmp = _Intel
		tmp = (tmp + 37 * 1.4  *  Buff.StatAdd) * (1 +  Buff.StatMulti / 10)
		return tmp
	End Function
	
	Function Armor() As Integer
		Dim tmp As Integer
		Dim tmp2 As Integer
		tmp = _Armor
		tmp2 = sim.boss.SpecialArmor
		tmp = tmp - tmp2
		tmp = tmp + (750 * 1.4  *  Buff.StatAdd)
		tmp = tmp * (1 + talentfrost.Toughness * 0.02)
		If sim.FrostPresence = 1 Then
			tmp = tmp * 1.6
		End If
		If sim.UnbreakableArmor.isActive Then tmp = tmp * 1.25
		
		tmp2 += sim.proc.GetActiveBonus("armor")
		tmp = tmp + tmp2
		return tmp
	End Function
	
	Function AttackPower() As Integer
		Dim tmp As Integer
		tmp = _AttackPower
		Select Case sim.EPStat
			Case "EP AttackPower"
				tmp = tmp+ 2*sim.EPBase
			Case "EP AttackPower0T7"
				tmp = tmp+ 2*sim.EPBase
			Case "EP AttackPowerNoTrinket"
				tmp = tmp+ 2*sim.EPBase
			Case "EP AfterSpellHitBaseAP"
				tmp = tmp+ 2*sim.EPBase
			Case "EP ExpertiseRatingCapAP"
				tmp = tmp+ 2*sim.EPBase
			Case "EP HitRatingCapAP"
				tmp = tmp+ 2*sim.EPBase
			Case else
		End Select
		tmp = tmp + int(Armor/180)*talentblood.BladedArmor
		tmp = tmp + 687 * Buff.AttackPower
		return tmp
	End Function
	
	Function HitRating() As Integer
		Dim tmp As Integer
		tmp = _HitRating
		Select Case sim.EPStat
			Case "EP HitRating"
				tmp = 263 - talentfrost.NervesofColdSteel*32.79 - sim.EPBase
			Case "EP HitRatingCap"
				tmp = 263 - talentfrost.NervesofColdSteel*32.79
			Case "EP HitRatingCapAP"
				tmp = 263 - talentfrost.NervesofColdSteel*32.79
			Case "EP SpellHitRating"
				tmp = 263 - talentfrost.NervesofColdSteel*32.79 + 20
			Case "EP AfterSpellHitBase"
				tmp = sim.MainStat.SpellHitCapRating
			Case "EP AfterSpellHitBaseAP"
				tmp = sim.MainStat.SpellHitCapRating
			Case "EP AfterSpellHitRating"
				tmp = sim.MainStat.SpellHitCapRating + sim.EPBase
			Case "EP RelativeHitRating"
				tmp +=  sim.EPBase
			Case ""
			Case Else
				If InStr(sim.EPStat,"ScaHit") Then
					If InStr(sim.EPStat,"ScaHitA") Then
						tmp += Replace(sim.EPStat,"ScaHitA","") * sim.EPBase
					Else
						tmp = Replace(sim.EPStat,"ScaHit","") * sim.EPBase
					end if
				End If
		End Select
		Return tmp
	End Function
	
	
	
	
	
	Function CritRating() As Integer
		Dim tmp As Integer
		tmp = _CritRating
		If sim.EPStat="EP CritRating" Then
			tmp = tmp+sim.EPBase
		End If
		If InStr(sim.EPStat,"ScaCrit") Then
			If InStr(sim.EPStat,"ScaCritA") Then
				tmp = tmp  + Replace(sim.EPStat,"ScaCritA","") * sim.EPBase
			Else
				tmp = Replace(sim.EPStat,"ScaCrit","") * sim.EPBase
			End If
		End If
		tmp +=  sim.proc.GetActiveBonus("crit")
		return tmp
	End Function
	
	Function HasteRating() As Integer
		Dim tmp As Integer
		tmp = _HasteRating
		Select Case sim.EPStat
			Case ""
			Case "EP HasteRating1"
				tmp = tmp+sim.EPBase
			Case "EP HasteRating2"
				tmp = tmp+sim.EPBase*2
			Case "EP HasteRating3"
				tmp = tmp+sim.EPBase*3
			Case "EP HasteRating4"
				tmp = tmp+sim.EPBase*4
			Case "EP HasteRating5"
				tmp = tmp+sim.EPBase*5
			Case "EP HasteRating6"
				tmp = tmp+sim.EPBase*6
			Case Else
				If InStr(sim.EPStat,"ScaHaste") Then
					If InStr(sim.EPStat,"ScaHasteA") Then
						tmp =  tmp  + Replace(sim.EPStat,"ScaHasteA","") * sim.EPBase
					Else
						tmp =  Replace(sim.EPStat,"ScaHaste","") * sim.EPBase
					end if
				End If
		End Select
		tmp +=  sim.proc.GetActiveBonus("haste")
		return tmp
	End Function
	
	Function ArmorPenetrationRating() As Integer
		Dim tmp As Integer
		tmp = _ArmorPenetrationRating
		If InStr(sim.EPStat,"ScaArP") Then
			If InStr(sim.EPStat,"ScaArPA") Then
				tmp =  tmp  + Replace(sim.EPStat,"ScaArPA","") * sim.EPBase
			Else
				tmp =  Replace(sim.EPStat,"ScaArP","") * sim.EPBase
			End If
		End If
		If sim.EPStat="EP ArmorPenetrationRating" Then
			tmp = tmp+sim.EPBase
		End If
		tmp +=  sim.proc.GetActiveBonus("arp")
		return tmp
	End Function
	
	Function ExpertiseRating() As Integer
		Dim tmp As Integer
		tmp = _ExpertiseRating
		return tmp
	End Function
	
	Function SpellHitRating() As Integer
		SpellHitRating = HitRating
	End Function
	
	Function SpellCritRating() As Integer
		SpellCritRating = CritRating
	End Function
	
	Function SpellHasteRating() As Integer
		SpellHasteRating = HasteRating
	End Function
	
	Function Dual() As Boolean
		If _Dual<> 0 Then
			If _Dual = 2 Then
				Return True
			Else
				Return false
			End If
			exit function
		End If
		_Dual = int32.Parse(XmlDoc.SelectSingleNode("//character/weapon/count").InnerText)
		If _Dual = 2 Then
			Return True
		Else
			Return false
		End If
	End Function
	
	Sub loadtemplate(file As String)
		
		talentblood = New TalentBlood
		talentfrost = New TalentFrost
		talentunholy = new TalentUnholy
		
		dim XmlDoc As New Xml.XmlDocument
		XmlDoc.Load(file)
		
		if sim._EPStat <> "Butchery" then talentblood.Butchery = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Butchery").InnerText)
		if sim._EPStat <> "Subversion" then talentblood.Subversion  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Subversion").InnerText)
		if sim._EPStat <> "BladedArmor" then talentblood.BladedArmor  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BladedArmor").InnerText)
		if sim._EPStat <> "ScentOfBlood" then TalentBlood.ScentOfBlood = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ScentOfBlood").InnerText)
		if sim._EPStat <> "Weapspec" then talentblood.Weapspec  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Weapspec").InnerText)
		if sim._EPStat <> "Darkconv" then talentblood.Darkconv  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Darkconv").InnerText)
		if sim._EPStat <> "BloodyStrikes" then talentblood.BloodyStrikes  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodyStrikes").InnerText)
		if sim._EPStat <> "Vot3W" then talentblood.Vot3W  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Vot3W").InnerText)
		if sim._EPStat <> "BloodyVengeance" then talentblood.BloodyVengeance  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodyVengeance").InnerText)
		if sim._EPStat <> "AbominationMight" then talentblood.AbominationMight  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/AbominationMight").InnerText)
		If sim._EPStat <> "Hysteria" Then talentblood.Hysteria  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Hysteria").InnerText)
		If sim._EPStat <> "BloodWorms" Then talentblood.BloodWorms  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodWorms").InnerText)
		
		if sim._EPStat <> "ImprovedDeathStrike" then talentblood.ImprovedDeathStrike  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ImprovedDeathStrike").InnerText)
		if sim._EPStat <> "SuddenDoom" then talentblood.SuddenDoom  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/SuddenDoom").InnerText)
		if sim._EPStat <> "MightofMograine" then talentblood.MightofMograine  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/MightofMograine").InnerText)
		if sim._EPStat <> "BloodGorged" then talentblood.BloodGorged  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodGorged").InnerText)
		if sim._EPStat <> "DRW" then talentblood.DRW  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/DRW").InnerText)
		if sim._EPStat <> "DRM" then talentblood.DRM  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/DRM").InnerText)
		
		if sim._EPStat <> "RPM" then talentfrost.RPM  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/RPM").InnerText)
		if sim._EPStat <> "ImprovedIcyTouch" then talentfrost.ImprovedIcyTouch  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ImprovedIcyTouch").InnerText)
		if sim._EPStat <> "Toughness" then talentfrost.Toughness  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Toughness").InnerText)
		if sim._EPStat <> "BlackIce" then talentfrost.BlackIce  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BlackIce").InnerText)
		if sim._EPStat <> "NervesofColdSteel" then talentfrost.NervesofColdSteel  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/NervesofColdSteel").InnerText)
		if sim._EPStat <> "Annihilation" then talentfrost.Annihilation  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Annihilation").InnerText)
		if sim._EPStat <> "KillingMachine" then talentfrost.KillingMachine  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/KillingMachine").InnerText)
		if sim._EPStat <> "GlacierRot" then talentfrost.GlacierRot  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/GlacierRot").InnerText)
		If sim._EPStat <> "Deathchill" Then talentfrost.Deathchill  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Deathchill").InnerText)
		if sim._EPStat <> "IcyTalons" then talentfrost.IcyTalons  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/IcyTalons").InnerText)
		if sim._EPStat <> "ImprovedIcyTalons" then talentfrost.ImprovedIcyTalons  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ImprovedIcyTalons").InnerText)
		if sim._EPStat <> "MercilessCombat" then talentfrost.MercilessCombat  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/MercilessCombat").InnerText)
		if sim._EPStat <> "Rime" then talentfrost.Rime  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Rime").InnerText)
		if sim._EPStat <> "BloodoftheNorth" then talentfrost.BloodoftheNorth  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodoftheNorth").InnerText)
		if sim._EPStat <> "UnbreakableArmor" then talentfrost.UnbreakableArmor  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/UnbreakableArmor").InnerText)
		if sim._EPStat <> "GuileOfGorefiend" then talentfrost.GuileOfGorefiend  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/GuileOfGorefiend").InnerText)
		if sim._EPStat <> "TundraStalker" then talentfrost.TundraStalker  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/TundraStalker").InnerText)
		if sim._EPStat <> "ChillOfTheGrave" then talentfrost.ChillOfTheGrave  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ChillOfTheGrave").InnerText)
		if sim._EPStat <> "HowlingBlast" then TalentFrost.HowlingBlast = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/HowlingBlast").InnerText)
		If sim._EPStat <> "ThreatOfThassarian" Then TalentFrost.ThreatOfThassarian= Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ThreatOfThassarian").InnerText)
		If sim._EPStat <> "EndlessWinter" Then TalentFrost.EndlessWinter= Integer.Parse(XmlDoc.SelectSingleNode("//Talents/EndlessWinter").InnerText)
		if sim._EPStat <> "IcyTalons" then talentfrost.IcyTalons  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/IcyTalons").InnerText)
		
		if sim._EPStat <> "ViciousStrikes" then talentunholy.ViciousStrikes  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ViciousStrikes").InnerText)
		if sim._EPStat <> "Virulence" then talentunholy.Virulence  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Virulence").InnerText)
		if sim._EPStat <> "Epidemic" then talentunholy.Epidemic  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Epidemic").InnerText)
		if sim._EPStat <> "Morbidity" then talentunholy.Morbidity  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Morbidity").InnerText)
		if sim._EPStat <> "RavenousDead" then talentunholy.RavenousDead  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/RavenousDead").InnerText)
		if sim._EPStat <> "MasterOfGhouls" then talentunholy.MasterOfGhouls  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/MasterOfGhouls").InnerText)
		if sim._EPStat <> "Outbreak" then talentunholy.Outbreak  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Outbreak").InnerText)
		if sim._EPStat <> "Necrosis" then talentunholy.Necrosis  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Necrosis").InnerText)
		if sim._EPStat <> "BloodCakedBlade" then talentunholy.BloodCakedBlade  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BloodCakedBlade").InnerText)
		if sim._EPStat <> "UnholyBlight" then talentunholy.UnholyBlight  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/UnholyBlight").InnerText)
		if sim._EPStat <> "Impurity" then talentunholy.Impurity  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Impurity").InnerText)
		if sim._EPStat <> "CryptFever" then talentunholy.CryptFever  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/CryptFever").InnerText)
		if sim._EPStat <> "ImprovedUnholyPresence" then talentunholy.ImprovedUnholyPresence = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/ImprovedUnholyPresence").InnerText)
		if sim._EPStat <> "BoneShield" then talentunholy.BoneShield  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/BoneShield").InnerText)
		if sim._EPStat <> "NightoftheDead" then talentunholy.NightoftheDead  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/NightoftheDead").InnerText)
		if sim._EPStat <> "GhoulFrenzy" then TalentUnholy.GhoulFrenzy= Integer.Parse(XmlDoc.SelectSingleNode("//Talents/GhoulFrenzy").InnerText)
		if sim._EPStat <> "WanderingPlague" then talentunholy.WanderingPlague  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/WanderingPlague").InnerText)
		if sim._EPStat <> "EbonPlaguebringer" then talentunholy.EbonPlaguebringer  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/EbonPlaguebringer").InnerText)
		if sim._EPStat <> "RageofRivendare" then talentunholy.RageofRivendare  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/RageofRivendare").InnerText)
		if sim._EPStat <> "SummonGargoyle" then talentunholy.SummonGargoyle  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/SummonGargoyle").InnerText)
		if sim._EPStat <> "Dirge" then talentunholy.Dirge  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Dirge").InnerText)
		if sim._EPStat <> "Reaping" then talentunholy.Reaping  = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Reaping").InnerText)
		
		if sim._EPStat <> "Desecration" then talentunholy.Desecration = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Desecration").InnerText)
		if sim._EPStat <> "Desolation" then  talentunholy.Desolation = Integer.Parse(XmlDoc.SelectSingleNode("//Talents/Desolation").InnerText)
		
		
		
		
		
		Glyph = New glyph(file)
		
	End Sub
	
end Class