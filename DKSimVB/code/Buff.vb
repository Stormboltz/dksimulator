

Friend module Buff
		
	Friend StrAgi As Integer 
	Friend AttackPower As Integer 
	Friend AttackPowerPc As Integer 
	Friend Bloodlust As Integer 
	Friend PcDamage As Integer 
	Friend Haste As Integer 
	Friend MeleeCrit As Integer 
	Friend MeleeHaste As Integer 
	Friend SpellCrit As Integer 
	Friend SpellHaste As Integer
	Friend SpellPower As Integer
	Friend StatAdd As Integer 
	Friend StatMulti As Integer 
	Friend ArmorMajor As Integer 
	Friend ArmorMinor As Integer
	Friend CritChanceTaken As Integer 
	Friend PhysicalVuln As Integer 
	Friend SpellCritTaken As Integer 
	Friend SpellDamageTaken As Integer 
	Friend SpellHitTaken As Integer 
	Friend Draenei as Integer
	
	Sub FullBuff()
		
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		Dim liveXml As new xml.XmlDocument
		dim tmp as String
		doc.Load("Buffconfig.xml")
		tmp  = doc.OuterXml
		
		
		tmp = tmp.Replace("True","1")
		tmp = tmp.Replace("False","0")
		liveXml.LoadXml(tmp)
		
		StrAgi = liveXml.SelectSingleNode("/config/chkBStrAgi").InnerText
		ArmorMajor = liveXml.SelectSingleNode("/config/chkBArmorMaj").InnerText
		AttackPower = liveXml.SelectSingleNode("/config/chkBAP").InnerText
		ArmorMinor = liveXml.SelectSingleNode("/config/chkBArmorMinor").InnerText
		AttackPowerPc = liveXml.SelectSingleNode("/config/chkBAPPc").InnerText
		CritChanceTaken = liveXml.SelectSingleNode("/config/chkBCritchanceTaken").InnerText
		PcDamage = liveXml.SelectSingleNode("/config/chkBPcDamage").InnerText
		PhysicalVuln = liveXml.SelectSingleNode("/config/chkBPhyVuln").InnerText
		Haste = liveXml.SelectSingleNode("/config/chkBHaste").InnerText
		SpellCritTaken = liveXml.SelectSingleNode("/config/chkBSpCrTaken").InnerText
		MeleeHaste = liveXml.SelectSingleNode("/config/chkBMeleeHaste").InnerText
		SpellDamageTaken = liveXml.SelectSingleNode("/config/chkBSpDamTaken").InnerText
		MeleeCrit = liveXml.SelectSingleNode("/config/chkBMeleeCrit").InnerText
		SpellHitTaken = liveXml.SelectSingleNode("/config/chkBSpHitTaken").InnerText
		SpellCrit = liveXml.SelectSingleNode("/config/chkBSpellCrit").InnerText
		SpellHaste = liveXml.SelectSingleNode("/config/chkBSpellHaste").InnerText
		StatAdd = liveXml.SelectSingleNode("/config/chkBStatAdd").InnerText
		StatMulti = liveXml.SelectSingleNode("/config/chkBStatMulti").InnerText
		Bloodlust = liveXml.SelectSingleNode("/config/chkBloodlust").InnerText
		Draenei = liveXml.SelectSingleNode("/config/chkDraeni").InnerText
		
	End Sub
	
	Sub UnBuff()
		StrAgi = 0
		AttackPower = 0
		AttackPowerPc = 0
		Bloodlust = 0
		PcDamage = 0
		Haste = 0
		MeleeCrit = 0
		MeleeHaste = 0
		SpellCrit = 0
		SpellHaste = 0
		SpellPower = 0
		StatAdd = 0
		StatMulti = 0
		ArmorMajor = 0
		ArmorMinor = 0
		CritChanceTaken = 0
		PhysicalVuln = 0
		SpellCritTaken = 0
		SpellDamageTaken = 0
		SpellHitTaken = 0
	End Sub
End Module