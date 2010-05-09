Imports System.Xml.Linq


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
    Friend Draenei As Integer





    Protected sim As Sim
    Sub New(ByVal S As Sim)
        sim = S

        Dim doc As XDocument = New XDocument
        Dim liveXml As New XDocument
        Dim tmp As String
        doc.Load("Buffconfig.xml")
        tmp = doc.ToString

        tmp = tmp.Replace("True", "1")
        tmp = tmp.Replace("False", "0")
        liveXml.Parse(tmp)

        StrAgi = liveXml.Element("/config/chkBStrAgi").Value
        AttackPower = liveXml.Element("/config/chkBAP").Value
        AttackPowerPc = liveXml.Element("/config/chkBAPPc").Value
        PcDamage = liveXml.Element("/config/chkBPcDamage").Value
        Haste = liveXml.Element("/config/chkBHaste").Value
        MeleeHaste = liveXml.Element("/config/chkBMeleeHaste").Value
        MeleeCrit = liveXml.Element("/config/chkBMeleeCrit").Value
        SpellCrit = liveXml.Element("/config/chkBSpellCrit").Value
        SpellHaste = liveXml.Element("/config/chkBSpellHaste").Value
        StatAdd = liveXml.Element("/config/chkBStatAdd").Value
        StatMulti = liveXml.Element("/config/chkBStatMulti").Value
        Bloodlust = liveXml.Element("/config/chkBloodlust").Value
        Draenei = liveXml.Element("/config/chkDraeni").Value
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