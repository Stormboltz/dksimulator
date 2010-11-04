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

        Friend Strength As PrimaryStat
        Friend Agility As PrimaryStat
        Friend AttackPower As PrimaryStat
        Friend HitRating As PrimaryStat
        Friend CritRating As PrimaryStat
        Friend HasteRating As PrimaryStat
        Friend ArmorPenetrationRating As PrimaryStat
        Friend ExpertiseRating As PrimaryStat
        Friend Intel As PrimaryStat
        Friend MasteryRating As PrimaryStat
        Friend Armor As ArmorStat

        Friend Stamina As PrimaryStat
        Friend Health As CalculatedStats.CalculatedStat
        'Friend SpecialArmor As Stat

        Friend Crit As CalculatedStats.Crit
        Friend SpellCrit As CalculatedStats.SpellCrit

        Friend Expertise As CalculatedStats.Expertise
        Friend MHExpertise As CalculatedStats.MHExpertise
        Friend OHExpertise As CalculatedStats.OHExpertise

        Friend Haste As CalculatedStats.Haste
        Friend PhysicalHaste As CalculatedStats.PhysicalHaste
        Friend SpellHaste As CalculatedStats.SpellHaste
        Friend RuneRegeneration As CalculatedStats.RuneRegeneration

        Friend Hit As CalculatedStats.CalculatedStat
        Friend SpellHit As CalculatedStats.CalculatedStat

        Friend ArmorPenetration As CalculatedStats.CalculatedStat
        Friend Mastery As CalculatedStats.CalculatedStat

        Friend ModifiedMHWeaponSpeed As CalculatedStats.ModifiedWeaponSpeed
        Friend ModifiedOHWeaponSpeed As CalculatedStats.ModifiedWeaponSpeed

        Private _Dual As Integer



        Friend MHExpertiseBonus As Integer
        Friend OHExpertiseBonus As Integer
        Friend Orc As Boolean
        Friend Troll As Boolean
        Friend BloodElf As Boolean

        Friend Talents As Talents

        Friend Buff As RaidBuffs

        Friend Glyph As Glyphs
        Sub New(ByVal S As Sim)

            Sim = S
            Talents = New Talents(Sim)
            Buff = New RaidBuffs(S)
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

            _Mitigation = 0
            _LastArP = 0
        End Sub

       



        Sub InitStats()
            Strength = New PrimaryStat(Simulator.Sim.Stat.Strength, Sim, True, True)
            Agility = New PrimaryStat(Simulator.Sim.Stat.Agility, Sim, False, True)
            AttackPower = New PrimaryStat(Simulator.Sim.Stat.AP, Sim, True)
            HitRating = New PrimaryStat(Simulator.Sim.Stat.Hit, Sim)
            CritRating = New PrimaryStat(Simulator.Sim.Stat.Crit, Sim, True)
            HasteRating = New PrimaryStat(Simulator.Sim.Stat.Haste, Sim)
            ArmorPenetrationRating = New PrimaryStat(Simulator.Sim.Stat.ArP, Sim)
            ExpertiseRating = New PrimaryStat(Simulator.Sim.Stat.Expertise, Sim)
            Intel = New PrimaryStat(Simulator.Sim.Stat.Intel, Sim, True, True)
            Armor = New ArmorStat(Simulator.Sim.Stat.Armor, Sim)
            MasteryRating = New PrimaryStat(Simulator.Sim.Stat.Mastery, Sim, True)

            Stamina = New PrimaryStat(Simulator.Sim.Stat.Stamina, Sim, True, True)
            Health = New CalculatedStats.CalculatedStat(Sim, Stamina, 0.001)
            Health._Name = "Health"


            Try
                Strength.Add(Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("Strength").Value))
                Agility.Add(Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("Agility").Value))
                Intel.Add(Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("Intel").Value))
                Armor.Add(Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("Armor").Value))
                Armor.Add(Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("SpecialArmor").Value))
                AttackPower.Add(Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("AttackPower").Value))
                HitRating.Add(Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("HitRating").Value))
                CritRating.Add(Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("CritRating").Value))
                HasteRating.Add(Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("HasteRating").Value))
                ArmorPenetrationRating.Add(Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("ArmorPenetrationRating").Value))
                MasteryRating.Add(Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("MasteryRating").Value))
                ExpertiseRating.Add(Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("ExpertiseRating").Value))
                Stamina.Add(Int32.Parse(XmlCharacter.Element("character").Element("stat").Element("Stamina").Value))
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

            Select Case Sim.EPStat
                Case "EP Strength"
                    Strength.Add(Sim.EPBase)
                Case "EP AttackPower", "EP Trinket NoTrinketAP"
                    AttackPower.Add(2 * Sim.EPBase)
                Case "EP AttackPower0T7"
                    AttackPower.Add(2 * Sim.EPBase)
                Case "EP AfterSpellHitBaseAP"
                    AttackPower.Add(2 * Sim.EPBase)

                    HitRating.Replace(Sim.Character.SpellHitCapRating)
                Case "EP HitRatingCapAP"
                    AttackPower.Add(2 * Sim.EPBase)

                    HitRating.Replace(HitCapRating)
                Case "EP HitRating"

                    HitRating.Replace(HitCapRating() - Sim.EPBase)
                Case "EP HitRatingCap"

                    HitRating.Replace(HitCapRating)
                Case "EP SpellHitRating"

                    HitRating.Replace(HitCapRating() + 20)

                Case "EP AfterSpellHitBase"

                    HitRating.Replace(SpellHitCapRating)

                Case "EP AfterSpellHitRating"

                    HitRating.Replace(SpellHitCapRating() + Sim.EPBase)

                Case "EP RelativeHitRating"
                    HitRating.Add(Sim.EPBase)
                Case "EP ExpertiseRating"
                    ExpertiseRating.Replace(GetExpertiseRatingCap() - Sim.EPBase)
                Case "EP ExpertiseRatingCap"
                    ExpertiseRating.Replace(GetExpertiseRatingCap())
                Case "EP ExpertiseRatingCapAP"
                    AttackPower.Add(2 * Sim.EPBase)
                    ExpertiseRating.Replace(GetExpertiseRatingCap())

                Case "EP RelativeExpertiseRating"
                    ExpertiseRating.Add(Sim.EPBase)
                Case "EP HasteRating"
                    HasteRating.Add(Sim.EPBase)
                Case "EP MasteryRating"
                    MasteryRating.Add(Sim.EPBase)
                Case "EP ArmorPenetrationRating"
                    ArmorPenetrationRating.Add(Sim.EPBase)
                Case "EP Agility"
                    Agility.Add(Sim.EPBase)
                Case "EP CritRating"
                    CritRating.Add(Sim.EPBase)

                Case ""

                Case Else

                    If InStr(Sim.EPStat, "ScaCrit") Then
                        If InStr(Sim.EPStat, "ScaCritA") Then
                            CritRating.Add(Replace(Sim.EPStat, "ScaCritA", "") * Sim.EPBase)
                        Else
                            CritRating.Replace(Replace(Sim.EPStat, "ScaCrit", "") * Sim.EPBase)
                        End If
                    End If


                    If InStr(Sim.EPStat, "ScaAgility") Then
                        Agility.Add(Replace(Sim.EPStat, "ScaAgility", "") * Sim.EPBase)
                    End If

                    If InStr(Sim.EPStat, "ScaArP") Then
                        If InStr(Sim.EPStat, "ScaArPA") Then
                            ArmorPenetrationRating.Add(Replace(Sim.EPStat, "ScaArPA", "") * Sim.EPBase)
                        Else
                            ArmorPenetrationRating.Replace(Replace(Sim.EPStat, "ScaArP", "") * Sim.EPBase)
                        End If
                    End If

                    If InStr(Sim.EPStat, "ScaHaste") Then
                        If InStr(Sim.EPStat, "ScaHasteA") Then
                            HasteRating.Add(Replace(Sim.EPStat, "ScaHasteA", "") * Sim.EPBase)
                        Else
                            HasteRating.Replace(Replace(Sim.EPStat, "ScaHaste", "") * Sim.EPBase)
                        End If
                    End If

                    If InStr(Sim.EPStat, "ScaHit") Then
                        If InStr(Sim.EPStat, "ScaHitA") Then
                            HitRating.Add(Replace(Sim.EPStat, "ScaHitA", "") * Sim.EPBase)
                        Else
                            HitRating.Replace(Replace(Sim.EPStat, "ScaHit", "") * Sim.EPBase)
                        End If
                    End If
                    If InStr(Sim.EPStat, "ScaExp") Then
                        If InStr(Sim.EPStat, "ScaExpA") Then
                            ExpertiseRating.Add(Replace(Sim.EPStat, "ScaExpA", "") * Sim.EPBase)
                        Else
                            ExpertiseRating.Replace(Replace(Sim.EPStat, "ScaExp", "") * Sim.EPBase)
                        End If
                    End If
                    If InStr(Sim.EPStat, "ScaStr") Then
                        Strength.Add(Replace(Sim.EPStat, "ScaStr", "") * Sim.EPBase)
                    End If

                    If InStr(Sim.EPStat, "ScaMast") Then
                        If InStr(Sim.EPStat, "ScaMastA") Then
                            MasteryRating.Add(Replace(Sim.EPStat, "ScaMastA", "") * Sim.EPBase)
                        Else
                            MasteryRating.Replace(Replace(Sim.EPStat, "ScaMast", "") * Sim.EPBase)
                        End If
                    End If


            End Select

            Crit = New CalculatedStats.Crit(Sim, CritRating)
            Crit._Name = "Crit"
            SpellCrit = New CalculatedStats.SpellCrit(Sim, CritRating)
            SpellCrit._Name = "SpellCrit"

            Expertise = New CalculatedStats.Expertise(Sim, ExpertiseRating)
            Expertise._Name = "Expertise"
            MHExpertise = New CalculatedStats.MHExpertise(Sim, ExpertiseRating)
            MHExpertise._Name = "MHExpertise"
            OHExpertise = New CalculatedStats.OHExpertise(Sim, ExpertiseRating)
            OHExpertise._Name = "OHExpertise"
            Haste = New CalculatedStats.Haste(Sim, HasteRating)
            Haste._Name = "Haste"

            RuneRegeneration = New CalculatedStats.RuneRegeneration(Sim, Haste)
            RuneRegeneration._Name = "Rune Regeneration"
            PhysicalHaste = New CalculatedStats.PhysicalHaste(Sim, HasteRating)
            PhysicalHaste._Name = "PhysicalHaste"
            If Sim.UnholyPresence > 0 Then
                'TODO: Verify if additive or multplicative
                PhysicalHaste.AddMulti(1.1 + Sim.Character.Talents.Talent("ImprovedUnholyPresence").Value * 0.025)
                RuneRegeneration.AddMulti(1.1 + Sim.Character.Talents.Talent("ImprovedUnholyPresence").Value * 0.025)
            End If
            If Sim.BloodPresence > 0 Then
                Dim j As Integer = Sim.Character.Talents.Talent("ImprovedBloodPresence").Value
                RuneRegeneration.AddMulti(1 + 0.1 * j)
            End If

            If Sim.Character.Talents.Talent("ImprovedIcyTalons").Value Then PhysicalHaste.AddMulti(1.05)
            If Sim.Character.Talents.Talent("IcyTalons").Value = 1 Then PhysicalHaste.AddMulti(1.2)
            If Sim.Character.Buff.MeleeHaste Then PhysicalHaste.AddMulti(1.2)


            SpellHaste = New CalculatedStats.SpellHaste(Sim, HasteRating)
            SpellHaste._Name = "Spell Haste"
            If Sim.Character.Buff.SpellHaste Then SpellHaste.AddMulti(1.05)

            If Sim.level85 Then
                ArmorPenetration = New CalculatedStats.CalculatedStat(Sim, ArmorPenetrationRating, 90)
                Mastery = New CalculatedStats.CalculatedStat(Sim, MasteryRating, 179.28)
                Hit = New CalculatedStats.CalculatedStat(Sim, HitRating, 120.109)
                SpellHit = New CalculatedStats.CalculatedStat(Sim, HitRating, 102.446)
            Else
                ArmorPenetration = New CalculatedStats.CalculatedStat(Sim, ArmorPenetrationRating, 22.55)
                Mastery = New CalculatedStats.CalculatedStat(Sim, MasteryRating, 45.906)
                Hit = New CalculatedStats.CalculatedStat(Sim, HitRating, 30.7548)
                SpellHit = New CalculatedStats.CalculatedStat(Sim, HitRating, 26.232)
            End If
            Mastery.Add(0.08)
            ArmorPenetration._Name = "ArmorPenetration"
            Mastery._Name = "Mastery"
            Hit._Name = "Hit"
            If XmlCharacter.<character>.<racials>.<Dreani>.Value = True Then
                Hit.Add(1 / 100)
            End If

            If DualW() Then Hit.Add(Sim.Character.Talents.Talent("NervesofColdSteel").Value / 100)


            SpellHit._Name = "SpellHit"
            If XmlCharacter.<character>.<racials>.<Dreani>.Value = True Then
                SpellHit.Add(1 / 100)
            End If

            SpellHit.Add(Sim.Character.Talents.Talent("Virulence").Value * 2 / 100)


            If XmlCharacter.<character>.<racials>.<Worgen>.Value = True Then
                Crit.Add(1 / 100)
                SpellCrit.Add(1 / 100)
            End If

            If XmlCharacter.<character>.<racials>.<Goblin>.Value = True Then
                PhysicalHaste.Add(1 / 100)
            End If

            'check that Talent and buff are loaded!
            Stamina.AddMulti(1 + 5 * Buff.StatMulti / 100)
            Stamina.AddMulti(1 + Sim.Character.Talents.Talent("Vot3W").Value * 9 / 100)
            If Sim.BloodPresence Then Stamina.AddMulti(1.08)
            Stamina.AddMulti(1.05) 'Plate Specialization
            Stamina.Add(140) 'Fortitude

            Agility.Add(155 * 1.15 * Buff.StrAgi)
            Agility.AddMulti(1 + 5 * Buff.StatMulti / 100)

            Strength.Add(155 * 1.15 * Buff.StrAgi)
            Strength.AddMulti(1.05) 'Plate Specialization
            Strength.AddMulti(1 + 5 * Buff.StatMulti / 100)
            Strength.AddMulti(1 + Talents.Talent("BrittleBones").Value * 2 / 100)
            Strength.AddMulti(1 + Talents.Talent("AbominationMight").Value / 100)

            If Talents.Talent("UnholyMight").Value <> 0 Then
                Strength.AddMulti(1.15)
            End If



            Intel.AddMulti(1 + 5 * Buff.StatMulti / 100)


            Armor.Add(750 * 1.4 * Buff.Armor)
            Armor.AddMulti(1 + Talents.Talent("Toughness").Value * 0.0333)
            If Sim.BloodPresence = 1 Then
                Armor.AddMulti(1.6)
            End If
            Dim iT As Integer
            Dim iT2 As Integer
            iT = Armor.Value
            iT2 = Talents.Talent("BladedArmor").Value
            AttackPower.Add(iT2 * iT / 180)
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
                ModifiedMHWeaponSpeed = New CalculatedStats.ModifiedWeaponSpeed(Sim, MHWeaponSpeed, PhysicalHaste)
            Catch
                msgBox("Error reading MH Weapon characteristics")
            End Try

            Try
                OHWeaponDPS = (XmlCharacter.Element("character").Element("weapon").Element("offhand").Element("dps").Value).Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                OHWeaponSpeed = (XmlCharacter.Element("character").Element("weapon").Element("offhand").Element("speed").Value).Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                ModifiedOHWeaponSpeed = New CalculatedStats.ModifiedWeaponSpeed(Sim, OHWeaponSpeed, PhysicalHaste)
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



            'Diagnostics.Debug.WriteLine("Health= " & Health.Value & " Stam= " & Stamina.Value)
        End Sub
        Sub InitTrinkets()
            'Trinkets
            Sim.Trinkets = New Trinkets(Sim)
            Try
                If Sim.EPStat.Contains("EP Trinket") Then
                    Dim TkName As String = Sim._EPStat.Replace("EP Trinket ", "")
                    Try
                        If TkName = "NoTrinket" Then Exit Sub
                        If TkName = "NoTrinketAP" Then Exit Sub
                        Dim trk As Trinket = Sim.Trinkets(TkName)
                        If IsNothing(trk) Then
                            Log.Log(TkName & " is not implemented", logging.Level.ERR)
                        Else
                            Sim.Trinkets(TkName).Equip()
                        End If
                       
            Catch ex As Exception
                Diagnostics.Debug.WriteLine(TkName & " is not implemented")
                Log.Log(TkName & " is not implemented", logging.Level.ERR)
            End Try
                Else
                    For Each el In XmlCharacter.Element("character").Element("trinket").Elements
                        Try
                            Sim.Trinkets(el.Name.ToString).Equip()
                        Catch ex As Exception
                            Diagnostics.Debug.WriteLine(el.Name.ToString & "is not implemented")
                            Log.Log(el.Name.ToString & " is not implemented", logging.Level.ERR)
                        End Try
                    Next
                End If
            Catch
                Diagnostics.Debug.WriteLine("ERROR init trinket")
                Log.Log("ERROR init trinket", logging.Level.ERR)
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
        Sub loadtemplate(ByVal file As String)
            Talents.ReadTemplate(file)
            Glyph = New Glyphs(Sim, file)
        End Sub
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
        Function DualW() As Boolean
            Return Dual()
        End Function
     
#Region "Attack Power"

        Function AP() As Integer
            Dim tmp As Integer

            tmp = AttackPower.Value()
            tmp = tmp + Strength.Value() * 2
            tmp = tmp + 550
            tmp = tmp * (1 + Sim.Character.Buff.AttackPowerPc / 10)
            ' Diagnostics.Debug.WriteLine(Sim.TimeStamp & ": AP is " & tmp)
            Return tmp
        End Function

#End Region
        Function SpellHitCapRating(Optional ByVal target As Targets.Target = Nothing) As Integer
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Integer
            tmp = 17
            tmp = tmp - Sim.Character.Talents.Talent("Virulence").Value * 2
            If XmlCharacter.<character>.<racials>.<Dreani>.Value = True Then
                tmp = tmp - 1
            End If

            tmp = tmp * 26.232
            Return tmp
        End Function
        Function GetExpertiseRatingCap() As Double
            Dim tmp As Double = 26
            tmp /= 25
            tmp *= 30.7548
            Return tmp
        End Function
        Function HitCapRating() As Integer
            Dim tmp As Integer
            tmp = 8
            If XmlCharacter.<character>.<racials>.<Dreani>.Value = True Then
                tmp = tmp - 1
            End If
            tmp = tmp - Sim.Character.Talents.Talent("NervesofColdSteel").Value
            tmp = tmp * 30.7548
            Return tmp
        End Function



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
            tmp *= (1 + Sim.ICCDamageBuff / 100)
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
            'tmp = tmp * (1 - 15 / (510 + 15)) 'Partial Resistance. It's about 0,029% less damage on average. No more Partial Resistance in Cata   

            Return tmp
        End Function

#End Region

    End Class
End Namespace