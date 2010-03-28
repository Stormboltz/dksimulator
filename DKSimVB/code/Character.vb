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
	
	
	
	
	



	Sub New(S As Sim)
		Sim = S
		XmlConfig = Sim.XmlConfig
		Try
			If XmlConfig.SelectSingleNode("//config/UseCharacter").InnerText = True Then
				XmlDoc.Load (Application.StartupPath & "\Characters\"  & XmlConfig.SelectSingleNode("//config/Character").InnerText)
			Else
				XmlDoc.Load (Application.StartupPath & "\CharactersWithGear\"  & XmlConfig.SelectSingleNode("//config/CharacterWithGear").InnerText)
			End If
			
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
		tmp = tmp +155 * 1.15 *  sim.Buff.StrAgi
		tmp = tmp + 37 * 1.4 *  sim.Buff.StatAdd
		tmp = tmp * (1 + sim.Buff.StatMulti / 10)
		tmp = tmp * (1 + sim.TalentBlood.Vot3W * 2 / 100)
		tmp = tmp * (1 + sim.TalentBlood.AbominationMight / 100)
		tmp = tmp * (1 + sim.TalentUnholy.ravenousdead / 100)
		tmp = tmp * (1 + sim.TalentFrost.EndlessWinter * 2 / 100)
		If sim.proc.MHFallenCrusader.IsActive Or sim.proc.oHFallenCrusader.IsActive Then
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
		tmp = tmp +155 * 1.15 *  sim.Buff.StrAgi
		tmp = tmp + 37 * 1.4 *  sim.Buff.StatAdd
		tmp = tmp * (1 +  sim.Buff.StatMulti / 10)
		tmp = tmp * (1 + sim.TalentBlood.Vot3W * 2 / 100)
		tmp = tmp * (1 + sim.TalentBlood.AbominationMight / 100)
		tmp = tmp * (1 + sim.TalentUnholy.ravenousdead / 100)
		tmp = tmp * (1 + sim.TalentFrost.EndlessWinter * 2 / 100)
		If sim.proc.MHFallenCrusader.IsActive Or sim.proc.oHFallenCrusader.IsActive Then
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
		tmp = (tmp + 155 * 1.15 *  sim.Buff.StrAgi + 37 * 1.4  *  sim.Buff.StatAdd) * (1 +  sim.Buff.StatMulti / 10)
		return tmp
	End Function
	
	Function Intel() As Integer
		Dim tmp As Integer
		tmp = _Intel
		tmp = (tmp + 37 * 1.4  *  sim.Buff.StatAdd) * (1 +  sim.Buff.StatMulti / 10)
		return tmp
	End Function
	
	Function Armor() As Integer
		Dim tmp As Integer
		Dim tmp2 As Integer
		tmp = _Armor
		tmp2 = sim.boss.SpecialArmor
		tmp = tmp - tmp2
		tmp = tmp + (750 * 1.4  *  sim.Buff.StatAdd)
		tmp = tmp * (1 + sim.TalentFrost.Toughness * 0.02)
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
		tmp = tmp + int(Armor/180)*sim.talentblood.BladedArmor
		tmp = tmp + 687 * sim.Buff.AttackPower
		return tmp
	End Function
	
	Function HitRating() As Integer
		Dim tmp As Integer
		tmp = _HitRating
		Select Case sim.EPStat
			Case "EP HitRating"
				tmp = 263 - sim.TalentFrost.NervesofColdSteel*32.79 - sim.EPBase
			Case "EP HitRatingCap"
				tmp = 263 - sim.TalentFrost.NervesofColdSteel*32.79
			Case "EP HitRatingCapAP"
				tmp = 263 - sim.TalentFrost.NervesofColdSteel*32.79
			Case "EP SpellHitRating"
				tmp = 263 - sim.TalentFrost.NervesofColdSteel*32.79 + 20
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
	
end Class