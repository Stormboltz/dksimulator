Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO

Friend Class Character



    Friend XmlDoc As XDocument
    Friend XmlConfig As XDocument



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
    Protected sim As Sim


    Friend MHExpertiseBonus As Integer
    Friend OHExpertiseBonus As Integer
    Friend Orc As Boolean
    Friend Troll As Boolean
    Friend BloodElf As Boolean

    Friend Talents As Talents

    Friend Buff As Buff

    Friend Glyph As glyph


    Sub New(ByVal S As Sim)
        sim = S
        Talents = New Talents(sim)
        Buff = New Buff(S)
        XmlConfig = sim.XmlConfig
        Try
            Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
                Dim path As String
                path = XmlConfig.Element("config").Element("CharacterWithGear").Value
                XmlDoc = S.XmlCharacter
                loadtemplate("Templates/" & XmlConfig.Element("config").Element("template").Value)
            End Using




        Catch

            msgBox("Error finding Character config file")
        End Try
        Try
            _Strength = Int32.Parse(XmlDoc.Element("character").Element("stat").Element("Strength").Value)
            _Agility = Int32.Parse(XmlDoc.Element("character").Element("stat").Element("Agility").Value)
            _Intel = Int32.Parse(XmlDoc.Element("character").Element("stat").Element("Intel").Value)
            _Armor = Int32.Parse(XmlDoc.Element("character").Element("stat").Element("Armor").Value)
            _AttackPower = Int32.Parse(XmlDoc.Element("character").Element("stat").Element("AttackPower").Value)
            _HitRating = Int32.Parse(XmlDoc.Element("character").Element("stat").Element("HitRating").Value)
            _CritRating = Int32.Parse(XmlDoc.Element("character").Element("stat").Element("CritRating").Value)
            _HasteRating = Int32.Parse(XmlDoc.Element("character").Element("stat").Element("HasteRating").Value)
            _ArmorPenetrationRating = Int32.Parse(XmlDoc.Element("character").Element("stat").Element("ArmorPenetrationRating").Value)
            _ExpertiseRating = Int32.Parse(XmlDoc.Element("character").Element("stat").Element("ExpertiseRating").Value)
            _Dual = Int32.Parse(XmlDoc.Element("character").Element("weapon").Element("count").Value)

            MHExpertiseBonus = Int32.Parse(XmlDoc.Element("character").Element("racials").Element("MHExpertiseBonus").Value)
            OHExpertiseBonus = Int32.Parse(XmlDoc.Element("character").Element("racials").Element("OHExpertiseBonus").Value)
            Orc = XmlDoc.Element("character").Element("racials").Element("Orc").Value
            Troll = XmlDoc.Element("character").Element("racials").Element("Troll").Value
            BloodElf = XmlDoc.Element("character").Element("racials").Element("BloodElf").Value
        Catch
            Diagnostics.Debug.WriteLine("Error reading Character config file.")
            msgBox("Error reading Character config file. You should open and check it. ")
        End Try
        sim.boss = New Boss(S)
    End Sub

    Function GetCharacterFileName() As String
        If XmlConfig.Element("config").Element("UseCharacter").Value = True Then
            Return XmlConfig.Element("config").Element("Character").Value
        Else
            Return XmlConfig.Element("config").Element("CharacterWithGear").Value
        End If
    End Function

    Function GetTemplateFileName() As String
        Return XmlConfig.Element("config").Element("template").Value
    End Function

    Function GetPriorityFileName() As String
        Return XmlConfig.Element("config").Element("priority").Value
    End Function

    Function GetRotationFileName() As String
        Return XmlConfig.Element("config").Element("rotation").Value
    End Function

    Function GetIntroFileName() As String
        Return XmlConfig.Element("config").Element("intro").Value
    End Function


    Function GetPresence() As String
        Return XmlConfig.Element("config").Element("presence").Value
    End Function

    Function GetSigil() As String
        Return XmlConfig.Element("config").Element("sigil").Value
    End Function

    Function GetMHEnchant() As String
        Return XmlConfig.Element("config").Element("mh").Value
    End Function

    Function GetOHEnchant() As String
        Return XmlConfig.Element("config").Element("oh").Value
    End Function

    Function GetPetCalculation() As Boolean
        Return XmlConfig.Element("config").Element("pet").Value
    End Function

    Function Strength() As Integer
        Dim tmp As Integer
        tmp = _Strength
        If sim.EPStat = "EP Strength" Then tmp = tmp + sim.EPBase
        If InStr(sim.EPStat, "ScaStr") Then
            tmp = tmp + Replace(sim.EPStat, "ScaStr", "") * sim.EPBase
        End If
        tmp += sim.proc.GetActiveBonus("str")
        tmp = tmp + 155 * 1.15 * Buff.StrAgi
        tmp = tmp + 37 * 1.4 * Buff.StatAdd
        tmp = tmp * (1 + Buff.StatMulti / 10)
        tmp = tmp * (1 + Talents.Talent("Vot3W").Value * 2 / 100)
        tmp = tmp * (1 + Talents.Talent("AbominationMight").Value / 100)
        tmp = tmp * (1 + Talents.Talent("RavenousDead").Value / 100)
        tmp = tmp * (1 + Talents.Talent("EndlessWinter").Value * 2 / 100)
        If sim.RuneForge.CheckFallenCrusader Then
            tmp = tmp * 1.15
        End If
        If sim.UnbreakableArmor.isActive Then tmp = tmp * 1.2
        Return tmp
    End Function

    Function MaxStrength() As Integer
        Dim tmp As Integer
        tmp = _Strength
        If sim.EPStat = "EP Strength" Then tmp = tmp + sim.EPBase
        If InStr(sim.EPStat, "ScaStr") Then
            tmp = tmp + Replace(sim.EPStat, "ScaStr", "") * sim.EPBase
        End If
        tmp += sim.proc.GetMaxPossibleBonus("str")
        tmp = tmp + 155 * 1.15 * Buff.StrAgi
        tmp = tmp + 37 * 1.4 * Buff.StatAdd
        tmp = tmp * (1 + Buff.StatMulti / 10)
        tmp = tmp * (1 + Talents.Talent("Vot3W").Value * 2 / 100)
        tmp = tmp * (1 + Talents.Talent("AbominationMight").Value / 100)
        tmp = tmp * (1 + Talents.Talent("RavenousDead").Value / 100)
        tmp = tmp * (1 + Talents.Talent("EndlessWinter").Value * 2 / 100)
        If sim.RuneForge.HasFallenCrusader Then
            tmp = tmp * 1.15
        End If
        If sim.UnbreakableArmor.isActive Then tmp = tmp * 1.1
        Return tmp
    End Function



    Function Agility() As Integer
        Dim tmp As Integer
        tmp = _Agility
        If sim.EPStat = "EP Agility" Then tmp = tmp + sim.EPBase
        If InStr(sim.EPStat, "ScaAgility") Then
            tmp = tmp + Replace(sim.EPStat, "ScaAgility", "") * sim.EPBase
        End If
        tmp = (tmp + 155 * 1.15 * Buff.StrAgi + 37 * 1.4 * Buff.StatAdd) * (1 + Buff.StatMulti / 10)
        Return tmp
    End Function

    Function Intel() As Integer
        Dim tmp As Integer
        tmp = _Intel
        tmp = (tmp + 37 * 1.4 * Buff.StatAdd) * (1 + Buff.StatMulti / 10)
        Return tmp
    End Function

    Function Armor() As Integer
        Dim tmp As Integer
        Dim tmp2 As Integer
        tmp = _Armor
        tmp2 = sim.boss.SpecialArmor
        tmp = tmp - tmp2
        tmp = tmp + (750 * 1.4 * Buff.StatAdd)
        tmp = tmp * (1 + Talents.Talent("Toughness").Value * 0.02)
        If sim.FrostPresence = 1 Then
            tmp = tmp * 1.6
        End If
        If sim.UnbreakableArmor.isActive Then tmp = tmp * 1.25

        tmp2 += sim.proc.GetActiveBonus("armor")
        tmp = tmp + tmp2
        Return tmp
    End Function

    Function AttackPower() As Integer
        Dim tmp As Integer
        tmp = _AttackPower
        Select Case sim.EPStat
            Case "EP AttackPower"
                tmp = tmp + 2 * sim.EPBase
            Case "EP AttackPower0T7"
                tmp = tmp + 2 * sim.EPBase
            Case "EP AttackPowerNoTrinket"
                tmp = tmp + 2 * sim.EPBase
            Case "EP AfterSpellHitBaseAP"
                tmp = tmp + 2 * sim.EPBase
            Case "EP ExpertiseRatingCapAP"
                tmp = tmp + 2 * sim.EPBase
            Case "EP HitRatingCapAP"
                tmp = tmp + 2 * sim.EPBase
            Case Else
        End Select
        tmp = tmp + Int(Armor() / 180) * Talents.Talent("BladedArmor").Value
        tmp = tmp + 687 * Buff.AttackPower
        Return tmp
    End Function

    Function HitRating() As Integer
        Dim tmp As Integer
        tmp = _HitRating
        Select Case sim.EPStat
            Case "EP HitRating"
                tmp = 263 - Talents.Talent("NervesofColdSteel").Value * 32.79 - sim.EPBase
            Case "EP HitRatingCap"
                tmp = 263 - Talents.Talent("NervesofColdSteel").Value * 32.79
            Case "EP HitRatingCapAP"
                tmp = 263 - Talents.Talent("NervesofColdSteel").Value * 32.79
            Case "EP SpellHitRating"
                tmp = 263 - Talents.Talent("NervesofColdSteel").Value * 32.79 + 20
            Case "EP AfterSpellHitBase"
                tmp = sim.MainStat.SpellHitCapRating
            Case "EP AfterSpellHitBaseAP"
                tmp = sim.MainStat.SpellHitCapRating
            Case "EP AfterSpellHitRating"
                tmp = sim.MainStat.SpellHitCapRating + sim.EPBase
            Case "EP RelativeHitRating"
                tmp += sim.EPBase
            Case ""
            Case Else
                If InStr(sim.EPStat, "ScaHit") Then
                    If InStr(sim.EPStat, "ScaHitA") Then
                        tmp += Replace(sim.EPStat, "ScaHitA", "") * sim.EPBase
                    Else
                        tmp = Replace(sim.EPStat, "ScaHit", "") * sim.EPBase
                    End If
                End If
        End Select
        Return tmp
    End Function





    Function CritRating() As Integer
        Dim tmp As Integer
        tmp = _CritRating
        If sim.EPStat = "EP CritRating" Then
            tmp = tmp + sim.EPBase
        End If
        If InStr(sim.EPStat, "ScaCrit") Then
            If InStr(sim.EPStat, "ScaCritA") Then
                tmp = tmp + Replace(sim.EPStat, "ScaCritA", "") * sim.EPBase
            Else
                tmp = Replace(sim.EPStat, "ScaCrit", "") * sim.EPBase
            End If
        End If
        tmp += sim.proc.GetActiveBonus("crit")
        Return tmp
    End Function

    Function HasteRating() As Integer
        Dim tmp As Integer
        tmp = _HasteRating
        Select Case sim.EPStat
            Case ""
            Case "EP HasteRating1"
                tmp = tmp + sim.EPBase
            Case "EP HasteRating2"
                tmp = tmp + sim.EPBase * 2
            Case "EP HasteRating3"
                tmp = tmp + sim.EPBase * 3
            Case "EP HasteRating4"
                tmp = tmp + sim.EPBase * 4
            Case "EP HasteRating5"
                tmp = tmp + sim.EPBase * 5
            Case "EP HasteRating6"
                tmp = tmp + sim.EPBase * 6
            Case Else
                If InStr(sim.EPStat, "ScaHaste") Then
                    If InStr(sim.EPStat, "ScaHasteA") Then
                        tmp = tmp + Replace(sim.EPStat, "ScaHasteA", "") * sim.EPBase
                    Else
                        tmp = Replace(sim.EPStat, "ScaHaste", "") * sim.EPBase
                    End If
                End If
        End Select
        tmp += sim.proc.GetActiveBonus("haste")
        Return tmp
    End Function

    Function ArmorPenetrationRating() As Integer
        Dim tmp As Integer
        tmp = _ArmorPenetrationRating
        If InStr(sim.EPStat, "ScaArP") Then
            If InStr(sim.EPStat, "ScaArPA") Then
                tmp = tmp + Replace(sim.EPStat, "ScaArPA", "") * sim.EPBase
            Else
                tmp = Replace(sim.EPStat, "ScaArP", "") * sim.EPBase
            End If
        End If
        If sim.EPStat = "EP ArmorPenetrationRating" Then
            tmp = tmp + sim.EPBase
        End If
        tmp += sim.proc.GetActiveBonus("arp")
        Return tmp
    End Function

    Function ExpertiseRating() As Integer
        Dim tmp As Integer
        tmp = _ExpertiseRating
        Return tmp
    End Function

    Function SpellHitRating() As Integer
        SpellHitRating = HitRating()
    End Function

    Function SpellCritRating() As Integer
        SpellCritRating = CritRating()
    End Function

    Function SpellHasteRating() As Integer
        SpellHasteRating = HasteRating()
    End Function

    Function Dual() As Boolean
        If _Dual <> 0 Then
            If _Dual = 2 Then
                Return True
            Else
                Return False
            End If
            Exit Function
        End If
        _Dual = Int32.Parse(XmlDoc.Element("character").Element("weapon").Element("count").Value)
        If _Dual = 2 Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub loadtemplate(ByVal file As String)
        Talents.ReadTemplate(file)
        Glyph = New glyph(file)
    End Sub

End Class