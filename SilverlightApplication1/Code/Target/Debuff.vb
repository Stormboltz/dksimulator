Imports System.Xml.Linq

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

            Dim doc As XDocument = New XDocument
            Dim liveXml As New XDocument
            Dim tmp As String
            doc.Load("Buffconfig.xml")
            'tmp = doc.OuterXml

            'tmp = tmp.Replace("True", "1")
            'tmp = tmp.Replace("False", "0")
            'liveXml.Parse(tmp)

            ArmorMajor = liveXml.Element("config").Element("chkBArmorMaj").Value
            ArmorMinor = liveXml.Element("config").Element("chkBArmorMinor").Value
            CritChanceTaken = liveXml.Element("config").Element("chkBCritchanceTaken").Value
            PhysicalVuln = liveXml.Element("config").Element("chkBPhyVuln").Value
            SpellCritTaken = liveXml.Element("config").Element("chkBSpCrTaken").Value
            SpellDamageTaken = liveXml.Element("config").Element("chkBSpDamTaken").Value
            SpellHitTaken = liveXml.Element("config").Element("chkBSpHitTaken").Value
            CrypticFever = liveXml.Element("config").Element("chkCrypticFever").Value
            BloodPlague = liveXml.Element("config").Element("chkBloodPlague").Value
            FrostFever = liveXml.Element("config").Element("chkFrostFever").Value
        End Sub
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