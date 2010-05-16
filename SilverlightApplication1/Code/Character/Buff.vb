Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO


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
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Buffconfig.xml", FileMode.Open, isoStore)
                Dim doc As XDocument = XDocument.Load(isoStream)
                StrAgi = Bool2Int(doc.Element("config").Element("chkBStrAgi").Value)
                AttackPower = Bool2Int(doc.Element("config").Element("chkBAP").Value)
                AttackPowerPc = Bool2Int(doc.Element("config").Element("chkBAPPc").Value)
                PcDamage = Bool2Int(doc.Element("config").Element("chkBPcDamage").Value)
                Haste = Bool2Int(doc.Element("config").Element("chkBHaste").Value)
                MeleeHaste = Bool2Int(doc.Element("config").Element("chkBMeleeHaste").Value)
                MeleeCrit = Bool2Int(doc.Element("config").Element("chkBMeleeCrit").Value)
                SpellCrit = Bool2Int(doc.Element("config").Element("chkBSpellCrit").Value)
                SpellHaste = Bool2Int(doc.Element("config").Element("chkBSpellHaste").Value)
                StatAdd = Bool2Int(doc.Element("config").Element("chkBStatAdd").Value)
                StatMulti = Bool2Int(doc.Element("config").Element("chkBStatMulti").Value)
                Bloodlust = Bool2Int(doc.Element("config").Element("chkBloodlust").Value)
                Draenei = Bool2Int(doc.Element("config").Element("chkDraeni").Value)
            End Using
        End Using
    End Sub
    Function Bool2Int(ByVal b As Boolean) As Integer
        If b = True Then
            Return 1
        Else
            Return 0
        End If
    End Function
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