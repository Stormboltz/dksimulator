

Friend Class Buff
		
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
	Friend Draenei as Integer

	
	
	
	
	Protected sim as Sim
	Sub New(S As Sim)
		Sim = S
		
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		Dim liveXml As new xml.XmlDocument
		dim tmp as String
		doc.Load("Buffconfig.xml")
		tmp  = doc.OuterXml
		
		tmp = tmp.Replace("True","1")
		tmp = tmp.Replace("False","0")
		liveXml.LoadXml(tmp)
		
		StrAgi = liveXml.SelectSingleNode("/config/chkBStrAgi").InnerText
		AttackPower = liveXml.SelectSingleNode("/config/chkBAP").InnerText
		AttackPowerPc = liveXml.SelectSingleNode("/config/chkBAPPc").InnerText
		PcDamage = liveXml.SelectSingleNode("/config/chkBPcDamage").InnerText
		Haste = liveXml.SelectSingleNode("/config/chkBHaste").InnerText
		MeleeHaste = liveXml.SelectSingleNode("/config/chkBMeleeHaste").InnerText
		MeleeCrit = liveXml.SelectSingleNode("/config/chkBMeleeCrit").InnerText
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
	End Sub
End Class