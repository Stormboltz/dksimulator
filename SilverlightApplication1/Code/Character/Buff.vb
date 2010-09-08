Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO


Namespace Simulator.Character
    Friend Class Buff

        Friend StrAgi As Integer
        Friend AttackPowerPc As Integer
        Friend Bloodlust As Integer
        Friend PcDamage As Integer
        Friend Crit As Integer
        Friend MeleeHaste As Integer
        Friend SpellHaste As Integer
        Friend Armor As Integer
        Friend StatMulti As Integer
        Friend Draenei As Integer





        Protected sim As Sim
        Sub New(ByVal S As Sim)
            sim = S

            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Buffconfig.xml", FileMode.Open, FileAccess.Read, sim.isoStore)
                Dim doc As XDocument = XDocument.Load(isoStream)
                StrAgi = Bool2Int(doc.Element("config").Element("chkBStrAgi").Value)
                Armor = Bool2Int(doc.Element("config").Element("chkBArmor").Value)
                AttackPowerPc = Bool2Int(doc.Element("config").Element("chkBAPPc").Value)
                Bloodlust = Bool2Int(doc.Element("config").Element("chkBloodlust").Value)
                PcDamage = Bool2Int(doc.Element("config").Element("chkBPcDamage").Value)
                Crit = Bool2Int(doc.Element("config").Element("chkBCrit").Value)
                MeleeHaste = Bool2Int(doc.Element("config").Element("chkBMeleeHaste").Value)
                SpellHaste = Bool2Int(doc.Element("config").Element("chkBSpellHaste").Value)
                StatMulti = Bool2Int(doc.Element("config").Element("chkBStatMulti").Value)
                Draenei = Bool2Int(doc.Element("config").Element("chkDraeni").Value)
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
            AttackPowerPc = 0
            Bloodlust = 0
            PcDamage = 0
            Crit = 0
            MeleeHaste = 0
            SpellHaste = 0
            Armor = 0
            StatMulti = 0
        End Sub
    End Class
End Namespace
