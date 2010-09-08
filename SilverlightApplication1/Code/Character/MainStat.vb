Imports System.Xml.Linq
Imports DKSIMVB.Simulator.WowObjects.Procs
'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 18:49
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.Character


    Friend Class MainStat
        Friend MHWeaponDPS As Double
        Friend MHWeaponSpeed As Double
        Friend OHWeaponDPS As Double
        Friend OHWeaponSpeed As Double
        Friend BossArmor As Integer
        Friend XmlCharacter As XDocument
        Friend T72PDPS As Integer
        Friend T74PDPS As Integer
        Friend T72PTNK As Integer
        Friend T74PTNK As Integer
        Friend T82PDPS As Integer
        Friend T84PDPS As Integer
        Friend T82PTNK As Integer
        Friend T84PTNK As Integer
        Friend T92PDPS As Integer
        Friend T94PDPS As Integer
        Friend T102PDPS As Integer
        Friend T104PDPS As Integer
        Friend T92PTNK As Integer
        Friend T102PTNK As Integer
        Private _Mitigation As Double
        Private _LastArP As Double
        Protected Sim As Sim
        Private _MaxAp As Integer
        Friend CSD As Integer
        Friend XmlConfig As XDocument

        Friend Strength As Stat
        Friend Agility As Stat
        Friend AttackPower As Stat
        Friend HitRating As Stat
        Friend CritRating As Stat
        Friend HasteRating As Stat
        Friend ArmorPenetrationRating As Stat
        Friend ExpertiseRating As Stat
        Friend Intel As Stat
        Friend Armor As Stat





        Private _Dual As Integer



        Friend MHExpertiseBonus As Integer
        Friend OHExpertiseBonus As Integer
        Friend Orc As Boolean
        Friend Troll As Boolean
        Friend BloodElf As Boolean

        Friend Talents As Talents

        Friend Buff As Buff

        Friend Glyph As glyph
        Sub New(ByVal S As Sim)

            Sim = S
            Talents = New Talents(Sim)
            Buff = New Buff(S)
            XmlConfig = Sim.XmlConfig
            Try
                Dim path As String
                path = XmlConfig.Element("config").Element("CharacterWithGear").Value
                XmlCharacter = S.XmlCharacter
                loadtemplate("Templates/" & XmlConfig.Element("config").Element("template").Value)
            Catch
                msgBox("Error finding Character config file")
            End Try
            BossArmor = 10643

            Sim.boss = New Boss(S)
            InitStats()
            InitTrinkets()
            InitSets()
            _Mitigation = 0
            _LastArP = 0





            
        End Sub

        Sub InitStats()
            Strength = New Stat(Simulator.Sim.Stat.Strength, Sim)
            Agility = New Stat(Simulator.Sim.Stat.Agility, Sim)
            AttackPower = New Stat(Simulator.Sim.Stat.AP, Sim)
            HitRating = New Stat(Simulator.Sim.Stat.Hit, Sim)
            CritRating = New Stat(Simulator.Sim.Stat.Crit, Sim)
            HasteRating = New Stat(Simulator.Sim.Stat.Haste, Sim)
            ArmorPenetrationRating = New Stat(Simulator.Sim.Stat.ArP, Sim)
            ExpertiseRating = New Stat(Simulator.Sim.Stat.Expertise, Sim)
            Intel = New Stat(Simulator.Sim.Stat.Intel, Sim)
            Armor = New Stat(Simulator.Sim.Stat.Armor, Sim)
            Try
                Strength.BaseValue = Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("Strength").Value)
                Agility.BaseValue = Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("Agility").Value)
                Intel.BaseValue = Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("Intel").Value)
                Armor.BaseValue = Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("Armor").Value)
                AttackPower.BaseValue = Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("AttackPower").Value)
                HitRating.BaseValue = Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("HitRating").Value)
                CritRating.BaseValue = Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("CritRating").Value)
                HasteRating.BaseValue = Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("HasteRating").Value)
                ArmorPenetrationRating.BaseValue = Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("ArmorPenetrationRating").Value)
                ExpertiseRating.BaseValue = Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("ExpertiseRating").Value)
                _Dual = Int32.Parse(XmlCharacter.Element("character").Element("weapon").Element("count").Value)

                MHExpertiseBonus = Int32.Parse(XmlCharacter.Element("character").Element("racials").Element("MHExpertiseBonus").Value)
                OHExpertiseBonus = Int32.Parse(XmlCharacter.Element("character").Element("racials").Element("OHExpertiseBonus").Value)
                Orc = XmlCharacter.Element("character").Element("racials").Element("Orc").Value
                Troll = XmlCharacter.Element("character").Element("racials").Element("Troll").Value
                BloodElf = XmlCharacter.Element("character").Element("racials").Element("BloodElf").Value
            Catch
                Diagnostics.Debug.WriteLine("Error reading Character config file.")
                msgBox("Error reading Character config file. You should open and check it. ")
            End Try

            Select Case EpStat
                Case "EP Strength"
                    Strength.BaseValue += Sim.EPBase

                   
                Case "EP AttackPower"
                    AttackPower.BaseValue += 2 * Sim.EPBase
                Case "EP AttackPower0T7"
                    AttackPower.BaseValue += 2 * Sim.EPBase
                Case "EP AttackPowerNoTrinket"
                    AttackPower.BaseValue += 2 * Sim.EPBase
                Case "EP AfterSpellHitBaseAP"
                    AttackPower.BaseValue += 2 * Sim.EPBase
                Case "EP ExpertiseRatingCapAP"
                    AttackPower.BaseValue += 2 * Sim.EPBase
                Case "EP HitRatingCapAP"
                    AttackPower.BaseValue += 2 * Sim.EPBase
                Case "EP HitRating"
                    HitRating.BaseValue += 263 - Talents.Talent("NervesofColdSteel").Value * 32.79 - Sim.EPBase
                Case "EP HitRatingCap", "EP HitRatingCapAP"
                    HitRating.BaseValue += 263 - Talents.Talent("NervesofColdSteel").Value * 32.79
                Case "EP SpellHitRating"
                    HitRating.BaseValue += 263 - Talents.Talent("NervesofColdSteel").Value * 32.79 + 20
                Case "EP AfterSpellHitBase", "EP AfterSpellHitBaseAP"
                    HitRating.BaseValue += Sim.Character.SpellHitCapRating
                Case "EP AfterSpellHitRating"
                    HitRating.BaseValue += Sim.Character.SpellHitCapRating + Sim.EPBase
                Case "EP RelativeHitRating"
                    HitRating.BaseValue += Sim.EPBase

                Case "EP HasteRating1"
                    HasteRating.BaseValue += Sim.EPBase
                Case "EP HasteRating2"
                    HasteRating.BaseValue += Sim.EPBase * 2
                Case "EP HasteRating3"
                    HasteRating.BaseValue += Sim.EPBase * 3
                Case "EP HasteRating4"
                    HasteRating.BaseValue += Sim.EPBase * 4
                Case "EP HasteRating5"
                    HasteRating.BaseValue += Sim.EPBase * 5
                Case "EP HasteRating6"
                    HasteRating.BaseValue += Sim.EPBase * 6

                Case "EP ArmorPenetrationRating"
                    ArmorPenetrationRating.BaseValue += Sim.EPBase
                Case "EP Agility"
                    Agility.BaseValue = +Sim.EPBase

                Case ""

                Case Else

                    If InStr(Sim.EPStat, "ScaAgility") Then
                        Agility.BaseValue += Replace(Sim.EPStat, "ScaAgility", "") * Sim.EPBase
                    End If

                    If InStr(Sim.EPStat, "ScaArP") Then
                        If InStr(Sim.EPStat, "ScaArPA") Then
                            ArmorPenetrationRating.BaseValue += Replace(Sim.EPStat, "ScaArPA", "") * Sim.EPBase
                        Else
                            ArmorPenetrationRating.BaseValue = Replace(Sim.EPStat, "ScaArP", "") * Sim.EPBase
                        End If
                    End If

                    If InStr(Sim.EPStat, "ScaHaste") Then
                        If InStr(Sim.EPStat, "ScaHasteA") Then
                            HasteRating.BaseValue += Replace(Sim.EPStat, "ScaHasteA", "") * Sim.EPBase
                        Else
                            HasteRating.BaseValue = Replace(Sim.EPStat, "ScaHaste", "") * Sim.EPBase
                        End If
                    End If

                    If InStr(Sim.EPStat, "ScaHit") Then
                        If InStr(Sim.EPStat, "ScaHitA") Then
                            HitRating.BaseValue += Replace(Sim.EPStat, "ScaHitA", "") * Sim.EPBase
                        Else
                            HitRating.BaseValue = Replace(Sim.EPStat, "ScaHit", "") * Sim.EPBase
                        End If
                    End If
                    If InStr(Sim.EPStat, "ScaStr") Then
                        Strength.BaseValue += Replace(Sim.EPStat, "ScaStr", "") * Sim.EPBase
                    End If



            End Select

            'check that Talent are loaded!
            AttackPower.AdditiveBuff += Int(Armor.Value / 180) * Talents.Talent("BladedArmor").Value


            Try
                MHWeaponDPS = (XmlCharacter.Element("character").Element("weapon").Element("mainhand").Element("dps").Value).Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                If Sim.EPStat = "EP WeaponDPS" Then
                    MHWeaponDPS = MHWeaponDPS + 10
                End If

                If InStr(Sim.EPStat, "ScaDPSA") Then
                    MHWeaponDPS += Replace(Sim.EPStat, "ScaDPSA", "")
                End If


                MHWeaponSpeed = (XmlCharacter.Element("character").Element("weapon").Element("mainhand").Element("speed").Value).Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                If Sim.EPStat = "EP WeaponSpeed" Then
                    MHWeaponSpeed = MHWeaponSpeed + 0.1
                End If
            Catch
                msgBox("Error reading MH Weapon characteristics")
            End Try

            Try
                OHWeaponDPS = (XmlCharacter.Element("character").Element("weapon").Element("offhand").Element("dps").Value).Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                OHWeaponSpeed = (XmlCharacter.Element("character").Element("weapon").Element("offhand").Element("speed").Value).Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
            Catch
                Diagnostics.Debug.WriteLine("Error reading OH Weapon characteristics")
            End Try

            Try
                If XmlCharacter.Element("character").Element("misc").Element("ChaoticSkyflareDiamond").Value = True Then
                    CSD = 1
                Else
                    CSD = 0
                End If
            Catch ex As Exception

            End Try




        End Sub
        Sub InitTrinkets()
            'Trinkets
            Sim.Trinkets = New Trinkets(Sim)


            Try
                Select Case Sim._EPStat
                    Case "EP NoTrinket"
                    Case "EP AttackPowerNoTrinket"
                    Case "EP MjolnirRunestone"
                        Sim.Trinkets.MjolRune.Equip()
                    Case "EP GrimToll"
                        Sim.Trinkets.GrimToll.Equip()
                    Case "EP BitterAnguish"
                        Sim.Trinkets.BitterAnguish.Equip()
                    Case "EP Mirror"
                        Sim.Trinkets.Mirror.Equip()
                    Case "EP Greatness"
                        Sim.Trinkets.Greatness.Equip()
                    Case "EP DCDeath"
                        Sim.Trinkets.DCDeath.Equip()
                    Case "EP Victory"
                        Sim.Trinkets.Victory.Equip()
                    Case "EP Necromantic"
                        Sim.Trinkets.Necromantic.Equip()
                    Case "EP Bandit"
                        Sim.Trinkets.Bandit.Equip()
                    Case "EP Pyrite"
                        Sim.Trinkets.Pyrite.Equip()
                    Case "EP DarkMatter"
                        Sim.Trinkets.DarkMatter.Equip()
                    Case "EP OldGod"
                        Sim.Trinkets.OldGod.Equip()
                    Case "EP Comet"
                        Sim.Trinkets.Comet.Equip()
                    Case "EP DeathChoice"
                        Sim.Trinkets.DeathChoice.Equip()
                    Case "EP DeathChoiceHeroic"
                        Sim.Trinkets.DeathChoiceHeroic.Equip()
                    Case "EP DeathbringersWill"
                        Sim.Trinkets.DeathbringersWill.Equip()
                    Case "EP TinyAbomination"
                        Sim.Trinkets.TinyAbomination.Equip()
                    Case "EP DeathbringersWillHeroic"
                        Sim.Trinkets.DeathbringersWillHeroic.Equip()
                    Case "EP WhisperingFangedSkull"
                        Sim.Trinkets.WhisperingFangedSkull.Equip()
                    Case "EP WhisperingFangedSkullHeroic"
                        Sim.Trinkets.WhisperingFangedSkullHeroic.Equip()
                    Case "EP NeedleEncrustedScorpion"
                        Sim.Trinkets.NeedleEncrustedScorpion.Equip()
                    Case "EP HerkumlWarToken"
                        Sim.Trinkets.HerkumlWarToken.Equip()
                    Case "EP MarkofSupremacy"
                        Sim.Trinkets.MarkofSupremacy.Equip()
                    Case "EP VengeanceoftheForsaken"
                        Sim.Trinkets.VengeanceoftheForsaken.Equip()
                    Case "EP VengeanceoftheForsakenHeroic"
                        Sim.Trinkets.VengeanceoftheForsakenHeroic.Equip()



                    Case Else
                        For Each el In XmlCharacter.Element("character").Element("trinket").Elements
                            Select Case el.Name
                                Case "MjolnirRunestone"
                                    Sim.Trinkets.MjolRune.Equip()
                                Case "GrimToll"
                                    Sim.Trinkets.GrimToll.Equip()
                                Case "BitterAnguish"
                                    Sim.Trinkets.BitterAnguish.Equip()
                                Case "Mirror"
                                    Sim.Trinkets.Mirror.Equip()
                                Case "Greatness"
                                    Sim.Trinkets.Greatness.Equip()
                                Case "DCDeath"
                                    Sim.Trinkets.DCDeath.Equip()
                                Case "Victory"
                                    Sim.Trinkets.Victory.Equip()
                                Case "Necromantic"
                                    Sim.Trinkets.Necromantic.Equip()
                                Case "Bandit"
                                    Sim.Trinkets.Bandit.Equip()
                                Case "Pyrite"
                                    Sim.Trinkets.Pyrite.Equip()
                                Case "DarkMatter"
                                    Sim.Trinkets.DarkMatter.Equip()
                                Case "OldGod"
                                    Sim.Trinkets.OldGod.Equip()
                                Case "Comet"
                                    Sim.Trinkets.Comet.Equip()
                                Case "DeathChoice"
                                    Sim.Trinkets.DeathChoice.Equip()
                                Case "DeathChoiceHeroic"
                                    Sim.Trinkets.DeathChoiceHeroic.Equip()
                                Case "DeathbringersWill"
                                    Sim.Trinkets.DeathbringersWill.Equip()
                                Case "DeathbringersWillHeroic"
                                    Sim.Trinkets.DeathbringersWillHeroic.Equip()
                                Case "WhisperingFangedSkull"
                                    Sim.Trinkets.WhisperingFangedSkull.Equip()
                                Case "WhisperingFangedSkullHeroic"
                                    Sim.Trinkets.WhisperingFangedSkullHeroic.Equip()
                                Case "NeedleEncrustedScorpion"
                                    Sim.Trinkets.NeedleEncrustedScorpion.Equip()
                                Case "TinyAbomination"
                                    Sim.Trinkets.TinyAbomination.Equip()
                                Case "HerkumlWarToken"
                                    Sim.Trinkets.HerkumlWarToken.Equip()
                                Case "MarkofSupremacy"
                                    Sim.Trinkets.MarkofSupremacy.Equip()
                                Case "VengeanceoftheForsaken"
                                    Sim.Trinkets.VengeanceoftheForsaken.Equip()
                                Case "VengeanceoftheForsakenHeroic"
                                    Sim.Trinkets.VengeanceoftheForsakenHeroic.Equip()

                            End Select
                        Next





                End Select
            Catch
                Diagnostics.Debug.WriteLine("ERROR init trinket")
            End Try
        End Sub
        Sub InitSets()
            T72PDPS = 0
            T74PDPS = 0
            T82PDPS = 0
            T84PDPS = 0
            T92PDPS = 0
            T94PDPS = 0
            T102PDPS = 0
            T104PDPS = 0


            Select Case Sim._EPStat
                Case "EP 0T7"
                Case "EP AttackPower0T7"
                Case "EP 2T7"
                    T72PDPS = 1
                Case "EP 4T7"
                    T74PDPS = 1
                Case "EP 2T8"
                    T82PDPS = 1
                Case "EP 4T8"
                    T84PDPS = 1
                Case "EP 2T9"
                    T92PDPS = 1
                Case "EP 4T9"
                    T94PDPS = 1
                Case "EP 2T10"
                    T102PDPS = 1
                Case "EP 4T10"
                    T104PDPS = 1
                Case Else
                    For Each el In XmlCharacter.Element("character").Element("Set").Elements
                        Select Case el.Name
                            Case "T72PDPS"
                                T72PDPS = 1
                            Case "T74PDPS"
                                T74PDPS = 1
                            Case "T82PDPS"
                                T82PDPS = 1
                            Case "T84PDPS"
                                T84PDPS = 1
                            Case "T92PDPS"
                                T92PDPS = 1
                            Case "T94PDPS"
                                T94PDPS = 1
                            Case "T72PTNK"
                                T72PTNK = 1
                            Case "T74PTNK"
                                T74PTNK = 1
                            Case "T82PTNK"
                                T82PTNK = 1
                            Case "T84PTNK"
                                T84PTNK = 1
                            Case "T102PDPS"
                                T102PDPS = 1
                            Case "T104PDPS"
                                T104PDPS = 1
                            Case "T92PTNK"
                                T92PTNK = 1
                            Case "T102PTNK"
                                T102PTNK = 1
                        End Select
                    Next
            End Select
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
            
            tmp += Sim.proc.GetActiveBonus(Sim.Stat.Strength)
            tmp = tmp + 155 * 1.15 * Buff.StrAgi
            tmp = tmp * (1 + 5 * Buff.StatMulti / 100)
            tmp = tmp * (1 + Talents.Talent("BrittleBones").Value * 2 / 100)
            tmp = tmp * (1 + Talents.Talent("AbominationMight").Value / 100)
            tmp = tmp * (1 + Talents.Talent("RavenousDead").Value / 100)

            If Sim.RuneForge.CheckFallenCrusader Then
                tmp = tmp * 1.15
            End If
            If Sim.PillarOfFrost.isActive Then
                tmp = tmp * 1.2
            End If

            Return tmp
        End Function
      
        Function Agility() As Integer
            Dim tmp As Integer
            tmp = _Agility
            
            tmp = (tmp + 155 * 1.15 * Buff.StrAgi) * (1 + 5 * Buff.StatMulti / 100)
            Return tmp
        End Function

        Function Intel() As Integer
            Dim tmp As Integer
            tmp = _Intel
            tmp = (tmp) * (1 + Buff.StatMulti / 10)
            Return tmp
        End Function

        Function Armor() As Integer
            Dim tmp As Integer
            Dim tmp2 As Integer
            tmp = _Armor
            tmp2 = Sim.boss.SpecialArmor
            tmp = tmp - tmp2
            tmp = tmp + (750 * 1.4 * Buff.Armor)
            tmp = tmp * (1 + Talents.Talent("Toughness").Value * 0.0333)
            If Sim.BloodPresence = 1 Then
                tmp = tmp * 1.6
            End If


            tmp2 += Sim.proc.GetActiveBonus(Sim.Stat.Armor)
            tmp = tmp + tmp2
            Return tmp
        End Function

        Function AttackPower() As Integer
            Dim tmp As Integer
            tmp = _AttackPower

            Return tmp
        End Function


    

      

        Function SpellHitRating() As Integer
            SpellHitRating = HitRating.Value()
        End Function

        Function SpellCritRating() As Integer
            SpellCritRating = CritRating.Value()
        End Function

        Function SpellHasteRating() As Integer
            SpellHasteRating = HasteRating.Value()
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
            _Dual = Int32.Parse(XmlCharacter.Element("character").Element("weapon").Element("count").Value)
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

        Function DualW() As Boolean
            Return Dual()
        End Function
     
#Region "Attack Power"

        Function BaseAP() As Integer
            Dim tmp As Integer
            tmp += Sim.proc.GetActiveBonus(Sim.Stat.AP)
            tmp = tmp + AttackPower.Value()
            tmp = tmp + Strength.Value() * 2
            tmp = tmp + 550
            tmp = tmp * (1 + Sim.Character.Buff.AttackPowerPc / 10)
            Return tmp
        End Function

        Function GetMaxAP() As Integer
            Dim tmp As Integer
            If _MaxAp <> 0 Then Return _MaxAp
            tmp += Sim.proc.GetMaxPossibleBonus(Sim.Stat.AP)
            tmp += AttackPower.MaxValue()
            tmp += Strength.MaxValue() * 2
            tmp += 550
            tmp = tmp * (1 + Sim.Character.Buff.AttackPowerPc / 10)
            _MaxAp = tmp
            Return tmp
        End Function

        Function AP() As Integer
            Return BaseAP()
        End Function
#End Region
#Region "Crit"

        Function CritRating() As Integer
            Dim tmp As Integer
            tmp = _CritRating
            If Sim.EPStat = "EP CritRating" Then
                tmp = tmp + Sim.EPBase
            End If
            If InStr(Sim.EPStat, "ScaCrit") Then
                If InStr(Sim.EPStat, "ScaCritA") Then
                    tmp = tmp + Replace(Sim.EPStat, "ScaCritA", "") * Sim.EPBase
                Else
                    tmp = Replace(Sim.EPStat, "ScaCrit", "") * Sim.EPBase
                End If
            End If
            tmp += Sim.proc.GetActiveBonus(Sim.Stat.Crit)
            Return tmp
        End Function

        Function crit(Optional ByVal target As Targets.Target = Nothing) As System.Double
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            tmp = tmp + CritRating.Value() / 45.91
            tmp = tmp + Agility.Value() * 0.016
            tmp = tmp + 5 * Sim.Character.Buff.Crit
            tmp = tmp - 4.8 'Crit malus vs bosses
            Return tmp / 100
        End Function
        Function critAutoattack(Optional ByVal target As Targets.Target = Nothing) As System.Double 'No Annihilation for autoattacks
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            tmp = tmp + CritRating.Value() / 45.91
            tmp = tmp + Agility.Value() / 62.5
            tmp = tmp + 5 * Sim.Character.Buff.Crit
            tmp = tmp - 4.8 'Crit malus vs bosses

            Return tmp / 100
        End Function
        Function SpellCrit(Optional ByVal target As Targets.Target = Nothing) As Single
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            tmp = SpellCritRating() / 45.91

            tmp = tmp + 5 * Sim.Character.Buff.Crit
            tmp = tmp + 5 * target.Debuff.SpellCritTaken
            tmp = tmp - 2.1 'Spell crit malus vs bosses

            Return tmp / 100
        End Function
#End Region
#Region "Haste"

        Function PhysicalHaste() As Double
            Dim tmp As Double
            tmp = Haste()
            tmp = tmp * (1 + Sim.UnholyPresence * 0.1 + Sim.Character.Talents.Talent("ImprovedUnholyPresence").Value * 2.5 / 100)
            If Sim.Character.Talents.Talent("ImprovedIcyTalons").Value Then tmp = tmp * 1.05
            If Sim.Character.Talents.Talent("IcyTalons").Value = 1 Then tmp = tmp * (1.2)
            If Sim.proc.UnholyFrenzy.IsActive Then tmp = tmp * 1.2
            If Sim.Character.Buff.MeleeHaste Then tmp = tmp * 1.2
            If Sim.proc.Bloodlust.IsActive Then tmp = tmp * 1.3
            If Sim.proc.TrollRacial.IsActive Then tmp = tmp * 1.2
            Return tmp
        End Function

        Function Haste() As Double
            Dim tmp As Double
            tmp = 1 + HasteRating.Value() / (25.22) / 100 '1.3 is the buff haste rating received
            Return tmp
        End Function
        Function SpellHaste() As Double
            Dim tmp As Double
            tmp = Haste()
            If Sim.proc.TrollRacial.IsActive Then tmp = tmp * 1.2
            If Sim.Character.Buff.SpellHaste Then tmp = tmp * 1.05
            If Sim.proc.Bloodlust.IsActive Then tmp = tmp * 1.3
            Return tmp
        End Function
        Function EstimatedHasteBonus() As Double
            Dim tmp As Double
            tmp = 1 + (HasteRating.Value() + Sim.EPBase) / 25.22 / 100 'Haste change for 3.1 ?
            Return tmp / (1 + HasteRating.Value() / 25.22 / 100)
        End Function
#End Region
#Region "Expertise"
        Function ExpertiseRating() As Integer
            Dim tmp As Integer
            tmp = _ExpertiseRating
            Return tmp
        End Function

        Function MHExpertise() As Double
            Dim tmp As Double
            tmp = Expertise()
            If Strings.InStr(Sim.EPStat, "EP Expertise") <> 0 Then
            Else
                tmp += (Sim.Character.MHExpertiseBonus * 0.25 / 100)
            End If
            Return tmp
        End Function

        Function OHExpertise() As Double
            Dim tmp As Double
            tmp = Expertise()
            If Strings.InStr(Sim.EPStat, "EP Expertise") <> 0 Then
            Else
                tmp += (Sim.Character.OHExpertiseBonus * 0.25 / 100)
            End If

            Return tmp
        End Function




        Function Expertise() As Double
            Dim tmp As Double

            tmp = ExpertiseRating.Value() / 32.79
            tmp += 0.25 * Sim.Character.Talents.Talent("Vot3W").Value * 2
            Select Case Sim.EPStat
                Case ""
                Case "EP ExpertiseRating"
                    tmp = 6.5 - Sim.EPBase / 32.79
                Case "EP ExpertiseRatingCap"
                    tmp = 6.5
                Case "EP ExpertiseRatingCapAP"
                    tmp = 6.5
                Case "EP ExpertiseRatingAfterCap"
                    tmp = 6.5 + Sim.EPBase / 32.79
                Case "EP RelativeExpertiseRating"
                    tmp += Sim.EPBase / 32.79
                Case Else
                    If InStr(Sim.EPStat, "ScaExp") Then
                        If InStr(Sim.EPStat, "ScaExpA") Then
                            tmp = tmp + Replace(Sim.EPStat, "ScaExpA", "") * Sim.EPBase / 32.79
                        Else
                            tmp = Replace(Sim.EPStat, "ScaExp", "") * Sim.EPBase / 32.79
                        End If
                    End If
            End Select

            Return tmp / 100
        End Function
#End Region
#Region "Hit"
        Function Hit() As Double
            Dim tmp As Double
            tmp = (HitRating.Value() / 32.79)
            If DualW() Then tmp += Sim.Character.Talents.Talent("NervesofColdSteel").Value

            If InStr(Sim.EPStat, "EP ") <> 0 Then
                If InStr(Sim.EPStat, "Hit") = 0 Then
                    tmp += Sim.Character.Buff.Draenei
                Else
                    If Sim.EPStat = "EP RelativeHitRating" Then tmp += Sim.Character.Buff.Draenei
                End If
            Else
                tmp += Sim.Character.Buff.Draenei
            End If
            Hit = tmp / 100
        End Function

        Function SpellHitCapRating(Optional ByVal target As Targets.Target = Nothing) As Integer
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Integer
            tmp = 17
            tmp = tmp - Sim.Character.Talents.Talent("Virulence").Value * 2
            tmp = tmp * 26.23
            Return tmp
        End Function



        Function SpellHit(Optional ByVal target As Targets.Target = Nothing) As Double
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            tmp = SpellHitRating() / 26.23
            If InStr(Sim.EPStat, "EP ") <> 0 Then
                If InStr(Sim.EPStat, "Hit") = 0 Then
                    tmp += Sim.Character.Buff.Draenei
                End If
            Else
                tmp += Sim.Character.Buff.Draenei
            End If
            tmp += 1 * Sim.Character.Talents.Talent("Virulence").Value * 2
            SpellHit = tmp / 100
        End Function
#End Region
#Region "Weapon Damage"


        Function NormalisedMHDamage() As Double
            Dim tmp As Double
            tmp = MHWeaponSpeed * MHWeaponDPS
            If DualW() Then
                tmp = tmp + 2.4 * (AP() / 14)
            Else
                tmp = tmp + 3.3 * (AP() / 14)

            End If
            Return tmp
        End Function
        Function NormalisedOHDamage() As Double
            Dim tmp As Double
            tmp = OHWeaponSpeed * OHWeaponDPS
            tmp = tmp + 2.4 * (AP() / 14)
            Return tmp
        End Function
        Function MHBaseDamage() As Double
            Dim tmp As Double
            tmp = (MHWeaponDPS + (AP() / 14)) * MHWeaponSpeed
            Return tmp
        End Function
        Function OHBaseDamage() As Double
            OHBaseDamage = (OHWeaponDPS + (AP() / 14)) * OHWeaponSpeed
        End Function
#End Region
        Function ArmorPen() As Double
            Dim tmp As Double
            tmp = ArmorPenetrationRating.Value() / 15.39
            tmp = tmp * 1.1 '1.1 with Patch 3.2.2, before 1.25
            Return tmp / 100
        End Function



#Region "Damage Multiplier"

        Function getMitigation(Optional ByVal target As Targets.Target = Nothing) As Double
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim AttackerLevel As Integer = 80
            Dim tmpArmor As Integer
            Dim ArPDebuffs As Double
            Dim l_sunder As Double = 1.0
            Dim l_ff As Double = 1.0
            If target.Debuff.ArmorMajor > 0 Then l_sunder = 1 - 0.12
            ArPDebuffs = (l_sunder * l_ff)
            Dim ArmorConstant As Double = 400 + (85 * AttackerLevel) + 4.5 * 85 * (AttackerLevel - 59)
            tmpArmor = BossArmor * ArPDebuffs
            Dim ArPCap As Double = Math.Min((tmpArmor + ArmorConstant) / 3, tmpArmor)
            tmpArmor = tmpArmor - ArPCap * Math.Min(1, ArmorPen)
            _Mitigation = ArmorConstant / (ArmorConstant + tmpArmor)
            Return _Mitigation
        End Function

        Private Function _BaseDamageMultiplier(ByVal T As Long) As Double
            Dim tmp As Double
            tmp = 1 + Sim.FrostPresence / 100
            tmp = tmp * (1 + 0.03 * Sim.Character.Buff.PcDamage)
            tmp = tmp * (1 + 0.02 * Sim.BoneShield.Value(T))
            tmp = tmp * (1 + Sim.proc.GetActiveBonus(Sim.Stat.Multiplicative) / 100)
            'If Sim.proc.Desolation.IsActiveAt(T) Then tmp = tmp * (1 + Sim.proc.Desolation.ProcValue * 0.01)
            If Sim.proc.T104PDPS.IsActiveAt(T) Then tmp = tmp * 1.03
            Return tmp
        End Function

        Function WhiteHitDamageMultiplier(ByVal T As Long, Optional ByVal target As Targets.Target = Nothing) As Double
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            tmp = _BaseDamageMultiplier(T) * getMitigation()
            tmp = tmp * (1 + 0.04 * target.Debuff.PhysicalVuln)

            'If Sim.Hysteria.IsActive(T) Then tmp = tmp * 1.2
            Return tmp
        End Function
        Function StandardPhysicalDamageMultiplier(ByVal T As Long, Optional ByVal target As Targets.Target = Nothing) As Double
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            tmp = WhiteHitDamageMultiplier(T)
            Return tmp
        End Function
        Function StandardMagicalDamageMultiplier(ByVal T As Long, Optional ByVal target As Targets.Target = Nothing) As Double
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            tmp = _BaseDamageMultiplier(T)

            tmp = tmp * (1 + 0.08 * target.Debuff.SpellDamageTaken)
            tmp = tmp * (1 - 15 / (510 + 15)) 'Partial Resistance. It's about 0,029% less damage on average.

            Return tmp
        End Function

#End Region

    End Class
End Namespace