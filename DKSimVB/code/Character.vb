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
	Friend XmlConfig as New Xml.XmlDocument



	Sub New(S As Sim)
		Sim = S
		XmlConfig.Load("config.xml")
		
		XmlDoc.Load (Application.StartupPath & "\Characters\"  & XmlConfig.SelectSingleNode("//config/Character").InnerText)

		
		
		'XmlDoc.Load(GetFilePath(sim._MainFrm.cmbCharacter.Text) )
		
		_Strength=0
		_Agility=0
		_Intel=0
		_Armor=0
		_AttackPower=0
		_HitRating=0
		_CritRating=0
		_HasteRating=0
		_ArmorPenetrationRating=0
		_ExpertiseRating=0
		_Dual =0
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
		tmp = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/Strength").InnerText)
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
		_Strength= tmp
		
		return _Strength
	End Function
	
	Function Agility() As Integer
		If _Agility <> 0 Then
			return _Agility
			exit function
		End If
		Dim tmp As Integer
		tmp = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/Agility").InnerText)
		If sim.EPStat="Agility" Then tmp = tmp +sim.EPBase
		If InStr(sim.EPStat,"ScaAgility") Then
			tmp = tmp + Replace(sim.EPStat,"ScaAgility","") * sim.EPBase
		End If
		_Agility = (tmp + 155 * 1.15 *  sim.Buff.StrAgi + 37 * 1.4  *  sim.Buff.StatAdd) * (1 +  sim.Buff.StatMulti / 10)
		
		return _Agility
	End Function
	
	Function Intel() As Integer
		If _Intel <> 0 Then
			return _Intel
			exit function
		End If
		Dim tmp As Integer
		tmp = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/Intel").InnerText)
		
		_Intel = (tmp + 37 * 1.4  *  sim.Buff.StatAdd) * (1 +  sim.Buff.StatMulti / 10)
		
		return _Intel
	End Function
	
	Function Armor() As Integer
		If _Armor <> 0 Then
			return _Armor
			exit function
		End If
		Dim tmp As Integer
		Dim tmp2 As Integer
		
		tmp = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/Armor").InnerText)
		tmp2 = sim.boss.SpecialArmor
		tmp = tmp - tmp2
		tmp = tmp + (750 * 1.4  *  sim.Buff.StatAdd)
		tmp = tmp * (1 + talentfrost.Toughness * 0.02)
		If sim.MainStat.FrostPresence = 1 Then
			tmp = tmp * 1.6
		End If
		tmp = tmp + tmp2
		_Armor = tmp
		return _Armor
	End Function
	
	Function AttackPower() As Integer
		If _AttackPower <> 0 Then
			return _AttackPower
			exit function
		End If
		Dim tmp As Integer
		tmp = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/AttackPower").InnerText)
		If sim.EPStat="AttackPower" Then tmp = tmp+100
		If sim.EPStat="AttackPower0T7" Then tmp = tmp+100
		If sim.EPStat="AttackPowerNoTrinket" Then tmp = tmp+100
		If sim.EPStat="AfterSpellHitBaseAP" Then tmp = tmp+100
		tmp = tmp + int(Armor/180)*BladedArmor
		_AttackPower = tmp + 687 *  sim.Buff.AttackPower
		
		return _AttackPower
	End Function
	
	Function HitRating() As Integer
		If _HitRating <> 0 Then
			return _HitRating
			exit function
		End If
		Dim tmp As Integer
		tmp = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/HitRating").InnerText)
		_HitRating = tmp
		
		Return _HitRating
	End Function
	
	Function CritRating() As Integer
		If _CritRating <> 0 Then
			return _CritRating
			exit function
		End If
		Dim tmp As Integer
		tmp = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/CritRating").InnerText)
		
		_CritRating = tmp
		
		
		
		
		If sim.EPStat="CritRating" Then
			_CritRating = _CritRating+sim.EPBase
		End If
		
		If InStr(sim.EPStat,"ScaCrit") Then
			If InStr(sim.EPStat,"ScaCritA") Then
				_CritRating = _CritRating  + Replace(sim.EPStat,"ScaCritA","") * sim.EPBase
			Else
				_CritRating = Replace(sim.EPStat,"ScaCrit","") * sim.EPBase
			End If
		End If
		
		
		
		
		
		return _CritRating
	End Function
	
	Function HasteRating() As Integer
		If _HasteRating <> 0 Then
			return _HasteRating
			exit function
		End If
		Dim tmp As Integer
		tmp = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/HasteRating").InnerText)
		_HasteRating = tmp
		If sim.EPStat="HasteRating" Then
			_HasteRating = _HasteRating+sim.EPBase
		End If
		
		
		If InStr(sim.EPStat,"ScaHaste") Then
			If InStr(sim.EPStat,"ScaHasteA") Then
				_HasteRating =  _HasteRating  + Replace(sim.EPStat,"ScaHasteA","") * sim.EPBase
			Else
				_HasteRating =  Replace(sim.EPStat,"ScaHaste","") * sim.EPBase
			end if
		End If
		
		
		return _HasteRating
	End Function
	
	Function ArmorPenetrationRating() As Integer

		Dim tmp As Integer
		tmp = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/ArmorPenetrationRating").InnerText)
		
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

		'If GrimTollFade > sim.TimeStamp Then Debug.Print( "GrimToll, now:" & tmp)
		_ArmorPenetrationRating = tmp
		
		return _ArmorPenetrationRating
	End Function
	
	Function ExpertiseRating() As Integer
		If _ExpertiseRating <> 0 Then
			return _ExpertiseRating
			exit function
		End If
		Dim tmp As Integer
		tmp = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/ExpertiseRating").InnerText)
		_ExpertiseRating = tmp
		
		return _ExpertiseRating
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