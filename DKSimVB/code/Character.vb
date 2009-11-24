Friend Class Character
	Friend XmlDoc As New Xml.XmlDocument
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
	Friend XmlConfig As New Xml.XmlDocument
	
	Friend MHExpertiseBonus As Integer
	Friend OHExpertiseBonus As Integer
	Friend Orc As Boolean
	Friend Troll As Boolean
	Friend BloodElf as Boolean
	
	
	
	
	



	Sub New(S As Sim)
		Sim = S
		XmlConfig.Load("config.xml")
		XmlDoc.Load (Application.StartupPath & "\Characters\"  & XmlConfig.SelectSingleNode("//config/Character").InnerText)

		'XmlDoc.Load(GetFilePath(sim._MainFrm.cmbCharacter.Text) )
		
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
		
		Try
			MHExpertiseBonus = int32.Parse(XmlDoc.SelectSingleNode("//character/racials/MHExpertiseBonus").InnerText)
			OHExpertiseBonus = int32.Parse(XmlDoc.SelectSingleNode("//character/racials/OHExpertiseBonus").InnerText)
			Orc = XmlDoc.SelectSingleNode("//character/racials/Orc").InnerText
			Troll = XmlDoc.SelectSingleNode("//character/racials/Troll").InnerText
			BloodElf = XmlDoc.SelectSingleNode("//character/racials/BloodElf").InnerText
		Catch
			
		End Try
		sim.boss = New Boss(S)
		
	End Sub
	
	Function GetCharacterFileName() as String
		Return XmlConfig.SelectSingleNode("//config/Character").InnerText
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
		If sim.EPStat="Strength" Then tmp = tmp + sim.EPBase
		If InStr(sim.EPStat,"ScaStr") Then
			tmp = tmp + Replace(sim.EPStat,"ScaStr","") * sim.EPBase
		End If
		
		if sim.proc.Virulence.IsActive then tmp += sim.proc.Virulence.ProcValue
		If sim.proc.T92PDPS.IsActive  Then
			tmp += sim.proc.T92PDPS.ProcValue
		End If
		tmp = tmp +155 * 1.15 *  sim.Buff.StrAgi
		tmp = tmp + 37 * 1.4 *  sim.Buff.StatAdd
		
		tmp = tmp * (1 +  sim.Buff.StatMulti / 10)
		tmp = tmp * (1 + talentblood.Vot3W * 2 / 100)
		tmp = tmp * (1 + talentblood.AbominationMight / 100)
		tmp = tmp * (1 + talentunholy.ravenousdead / 100)
		If sim.proc.MHFallenCrusader.IsActive Or sim.proc.oHFallenCrusader.IsActive Then
			tmp = tmp * 1.15
		End If
		
		
		
		If sim.Trinkets.Greatness.fade > sim.TimeStamp Then tmp = tmp + sim.Trinkets.Greatness.ProcValue
		If Sim.Trinkets.DeathChoice.Fade > sim.TimeStamp Then tmp = tmp + Sim.Trinkets.DeathChoice.ProcValue
		If Sim.Trinkets.DeathChoiceHeroic.Fade > sim.TimeStamp Then tmp = tmp + Sim.Trinkets.DeathChoiceHeroic.ProcValue
		
		if sim.UnbreakableArmor.isActive then tmp = tmp * 1.1
		return tmp
	End Function
	
	Function Agility() As Integer
		
		Dim tmp As Integer
		tmp = _Agility
		If sim.EPStat="Agility" Then tmp = tmp +sim.EPBase
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
		tmp = tmp * (1 + talentfrost.Toughness * 0.02)
		If sim.MainStat.FrostPresence = 1 Then
			tmp = tmp * 1.6
		End If
		tmp = tmp + tmp2
		return tmp
	End Function
	
	Function AttackPower() As Integer
		Dim tmp As Integer
		tmp = _AttackPower
		If sim.EPStat="AttackPower" Then tmp = tmp+100
		If sim.EPStat="AttackPower0T7" Then tmp = tmp+100
		If sim.EPStat="AttackPowerNoTrinket" Then tmp = tmp+100
		If sim.EPStat="AfterSpellHitBaseAP" Then tmp = tmp+100
		tmp = tmp + int(Armor/180)*BladedArmor
		tmp = tmp + 687 *  sim.Buff.AttackPower
		
		return tmp
	End Function
	
	Function HitRating() As Integer
		Dim tmp As Integer
		tmp = _HitRating
		Return tmp
	End Function
	
	Function CritRating() As Integer
		Dim tmp As Integer
		tmp = _CritRating
		If sim.EPStat="CritRating" Then
			tmp = tmp+sim.EPBase
		End If
		If InStr(sim.EPStat,"ScaCrit") Then
			If InStr(sim.EPStat,"ScaCritA") Then
				tmp = tmp  + Replace(sim.EPStat,"ScaCritA","") * sim.EPBase
			Else
				tmp = Replace(sim.EPStat,"ScaCrit","") * sim.EPBase
			End If
		End If
		return tmp
	End Function
	
	Function HasteRating() As Integer
		Dim tmp As Integer
		tmp = _HasteRating
		If sim.EPStat="HasteRating" Then
			tmp = tmp+sim.EPBase
		End If
		If InStr(sim.EPStat,"ScaHaste") Then
			If InStr(sim.EPStat,"ScaHasteA") Then
				tmp =  tmp  + Replace(sim.EPStat,"ScaHasteA","") * sim.EPBase
			Else
				tmp =  Replace(sim.EPStat,"ScaHaste","") * sim.EPBase
			end if
		End If
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
		If sim.EPStat="ArmorPenetrationRating" Then
			tmp = tmp+sim.EPBase
		End If
		If Sim.Trinkets.MjolRune.Fade > sim.TimeStamp Then tmp = tmp + Sim.Trinkets.MjolRune.procvalue
		If Sim.Trinkets.GrimToll.Fade > sim.TimeStamp Then tmp = tmp + Sim.Trinkets.GrimToll.ProcValue
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