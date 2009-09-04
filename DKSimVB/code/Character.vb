Friend Module Character
	Private XmlDoc As New Xml.XmlDocument
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
	
	
	Sub init()
		XmlDoc.Load(sim._MainFrm.GetFilePath(_MainFrm.cmbCharacter.Text) )
		
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
	End sub
	
	Function Strength() As Integer
		Dim tmp As Integer
		tmp = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/Strength").InnerText)
		If sim.EPStat="Strength" Then tmp = tmp + sim.EPBase
		If Sigils.Virulence Then
			if proc.VirulenceFade >= sim.TimeStamp then tmp = tmp + 200
		End If
		if T92PDPSFAde > sim.TimeStamp then tmp = tmp + 180
		tmp = tmp +155 * 1.15 * Buff.StrAgi
		tmp = tmp + 37 * 1.4 * Buff.StatAdd
		
		tmp = tmp * (1 + Buff.StatMulti / 10)
		tmp = tmp * (1 + talentblood.Vot3W*2/100)
		tmp = tmp * (1 + talentblood.AbominationMight/100)
		tmp = tmp * (1 + talentunholy.ravenousdead/100)
		tmp = tmp * (1 + 0.15 * RuneForge.FallenCrusaderProc)
		
		
		
		If greatnessFade > sim.TimeStamp Then tmp = tmp + 300
		If DeathChoiceFade > sim.TimeStamp Then tmp = tmp + 450
		If DeathChoiceHeroicFade > sim.TimeStamp Then tmp = tmp + 510
		
		if UA.isActive then tmp = tmp *1.25
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
		
		_Agility = (tmp + 155 * 1.15 * Buff.StrAgi + 37 * 1.4  * Buff.StatAdd) * (1 + Buff.StatMulti / 10)
		
		return _Agility
	End Function
	
	Function Intel() As Integer
		If _Intel <> 0 Then
			return _Intel
			exit function
		End If
		Dim tmp As Integer
		tmp = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/Intel").InnerText)
		
		_Intel = (tmp + 37 * 1.4  * Buff.StatAdd) * (1 + Buff.StatMulti / 10)
		
		return _Intel
	End Function
	
	Function Armor() As Integer
		If _Armor <> 0 Then
			return _Armor
			exit function
		End If
		Dim tmp As Integer
		tmp = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/Armor").InnerText)
		tmp = tmp + (750 * 1.4  * Buff.StatAdd)
		
		tmp = tmp * (1 + talentfrost.Toughness * 0.02)
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
		If sim.EPStat="AttackPowerNoTrinket" then tmp = tmp+100
		tmp = tmp + int(Armor/180)*BladedArmor
		_AttackPower = tmp + 548 * Buff.AttackPower
		
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
		return _HasteRating
	End Function
	
	Function ArmorPenetrationRating() As Integer

		Dim tmp As Integer
		tmp = int32.Parse(XmlDoc.SelectSingleNode("//character/stat/ArmorPenetrationRating").InnerText)
		If MjolRuneFade > sim.TimeStamp Then tmp = tmp + 665
		If GrimTollFade > sim.TimeStamp Then tmp = tmp + 612

		'If GrimTollFade > sim.TimeStamp Then Debug.Print( "GrimToll, now:" & tmp)
		_ArmorPenetrationRating = tmp
		If sim.EPStat="ArmorPenetrationRating" Then 
			_ArmorPenetrationRating = tmp+sim.EPBase
		End If
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
	
end module