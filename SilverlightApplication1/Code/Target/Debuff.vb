Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO

'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 4/23/2010
' Heure: 9:56 AM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Targets
    Public Class Debuff

        Friend ArmorMajor As Integer
        Friend ArmorMinor As Integer
        Friend CritChanceTaken As Integer
        Friend PhysicalVuln As Integer
        Friend SpellCritTaken As Integer
        Friend SpellDamageTaken As Integer
        Friend SpellHitTaken As Integer
        Friend CrypticFever As Integer
        Friend BloodPlague As Integer
        Friend FrostFever As Integer

        Protected sim As Sim
        Sub New(ByVal S As Sim)
            sim = S
            Dim doc As XDocument
            Dim liveXml As New XDocument

            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Buffconfig.xml", FileMode.Open, FileAccess.Read, sim.isoStore)
                doc = XDocument.Load(isoStream)
            End Using

            ArmorMajor = Bool2Int(doc.Element("config").Element("chkBArmorMaj").Value)
            ArmorMinor = Bool2Int(doc.Element("config").Element("chkBArmorMinor").Value)
            CritChanceTaken = Bool2Int(doc.Element("config").Element("chkBCritchanceTaken").Value)
            PhysicalVuln = Bool2Int(doc.Element("config").Element("chkBPhyVuln").Value)
            SpellCritTaken = Bool2Int(doc.Element("config").Element("chkBSpCrTaken").Value)
            SpellDamageTaken = Bool2Int(doc.Element("config").Element("chkBSpDamTaken").Value)
            SpellHitTaken = Bool2Int(doc.Element("config").Element("chkBSpHitTaken").Value)
            CrypticFever = Bool2Int(doc.Element("config").Element("chkCrypticFever").Value)
            BloodPlague = Bool2Int(doc.Element("config").Element("chkBloodPlague").Value)
            FrostFever = Bool2Int(doc.Element("config").Element("chkFrostFever").Value)
        End Sub
        Function Bool2Int(ByVal b As Boolean) As Integer
            If b = True Then
                Return 1
            Else
                Return 0
            End If
        End Function

        Sub Unbuff()
            ArmorMajor = 0
            ArmorMinor = 0
            CritChanceTaken = 0
            PhysicalVuln = 0
            SpellCritTaken = 0
            SpellDamageTaken = 0
            SpellHitTaken = 0
            CrypticFever = 0
            BloodPlague = 0
            FrostFever = 0
        End Sub


        '		Function RazorIceMultiplier(T As Long) as Double
        '			If RIProc Is Nothing Then Return 1.0
        '			Return 1.0 + 0.02 * RIProc.Stack
        '		End Function
        '		




    End Class
End Namespace