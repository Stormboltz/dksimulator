Imports System.Xml.Linq
Imports System.Linq

'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 00:22
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Procs
    Friend Class ProcsManager
        Friend Bloodlust As Proc

        Friend DRM As Proc
        Friend SuddenDoom As Proc
        Friend ScarletFever As Proc
        Friend UnholyFrenzy As Proc
        Friend RunicEmpowerment As Proc
        Friend ThreatOfThassarian As Proc
        Friend ReapingBotN As Proc
        Friend CrimsonScourge As Proc
        Friend T104PDPS As Proc

        'Friend Desolation As Proc

        Friend KillingMachine As Proc
        Friend Rime As Proc
        Friend ScentOfBlood As ScentOfBlood
        Friend Strife As Proc
        Friend TrollRacial As Proc

        Friend MHBloodCakedBlade As Proc
        Friend OHBloodCakedBlade As Proc




        Protected Sim As Sim
        'Friend T104PDPSFAde As Integer



        Friend AllProcs As New Collections.Generic.List(Of Proc)
        Friend EquipedProc As New Collections.Generic.List(Of Proc)


        Private XmlCharacter As XDocument

       

        Public Enum ProcOnType
            OnMisc = 0
            OnHit = 1
            OnMHhit = 2
            OnOHhit = 3
            OnCrit = 4
            OnDamage = 5
            OnDoT = 6
            OnMHWhiteHit = 7
            OnFU = 8
            OnBloodStrike = 9
            OnPlagueStrike = 10
            onRPDump = 11 'For Runic Empowerment.
            OnOHWhitehit = 12
            OnWhitehit = 13
            OnBossHitOrMiss = 14
            OnBloodBoil = 15
            'OnUse=9
        End Enum

        Sub New(ByVal S As Sim)
            Sim = S
            XmlCharacter = S.XmlCharacter
        End Sub

        Function Find(ByVal name As String) As Proc
            Dim prc As Proc
            For Each prc In AllProcs
                If prc.Name = name Then
                    Return prc
                Else
                    'Diagnostics.Debug.WriteLine (prc.Name)
                End If
            Next
            Return Nothing
        End Function

        Sub SoftReset()
            Dim prc As Proc
            For Each prc In AllProcs
                prc.CD = 0
                prc.Fade = 0
                prc.Stack = 0
            Next
            Bloodlust.CD = Sim.TimeStamp + 500
        End Sub

        Sub Init()
            Dim s As Sim
            s = Me.Sim

            s.RuneForge.Init()

            ScarletFever = New Proc(s)
            With ScarletFever
                ._Name = "Scarlet Fever"
                .ProcLenght = 30
                .ProcOn = ProcOnType.OnBloodBoil
                .ProcChance = Sim.Character.Talents.Talent("ScarletFever").Value / 2
                If .ProcChance > 0 Then .Equip()
            End With

            CrimsonScourge = New Proc(s)
            With CrimsonScourge
                ._Name = "Crimson Scourge"
                .ProcLenght = 30
                '.ProcOn = ProcOnType.OnPlagueStrike
                .ProcChance = Sim.Character.Talents.Talent("CrimsonScourge").Value / 2
                If .ProcChance > 0 Then .Equip()
            End With



            Dim MightOfFrozenWastes As New MightOfFrozenWastes(s)

            With MightOfFrozenWastes
                ._Name = "Might Of Frozen Wastes"

                If Not s.Character.Dual Then
                    .ProcChance = 15 * s.Character.Talents.Talent("MightOfFrozenWastes").Value / 100
                    .ProcOn = ProcOnType.OnMHWhiteHit
                    If .ProcChance > 0 Then .Equip()
                End If
            End With



            RunicEmpowerment = New RunicEmpowerment(s)
            With RunicEmpowerment
                ._Name = "Runic Empowerment"
                .ProcChance = 0.45
                .ProcOn = ProcOnType.onRPDump
                .Equip()
            End With

            UnholyFrenzy = New Proc(s)
            With UnholyFrenzy
                ._Name = "Unholy Frenzy"
                .ProcChance = 1
                .ProcLenght = 30
                .InternalCD = 180
                .ProcOn = ProcOnType.OnFU
                If Sim.Character.Talents.Talent("UnholyFrenzy").Value = 1 Then .Equip()
            End With

            MHBloodCakedBlade = New Proc(s)
            With MHBloodCakedBlade
                ._Name = "MH Blood-Caked Blade"
                .ProcChance = Sim.Character.Talents.Talent("BloodCakedBlade").Value * 0.1
                If .ProcChance > 0 Then
                    .Equip()
                End If
            End With

            OHBloodCakedBlade = New Proc(s)
            With OHBloodCakedBlade
                ._Name = "OH Blood-Caked Blade"
                .ProcChance = Sim.Character.Talents.Talent("BloodCakedBlade").Value * 0.1
                If .ProcChance > 0 Then
                    .Equip()
                End If
            End With


            Bloodlust = New Proc(s)
            With Bloodlust
                ._Name = "Bloodlust"
                .ProcChance = 1
                .ProcLenght = 40
                .InternalCD = 10 * 60
                .CD = 500
                If Sim.Character.Buff.Bloodlust Then .Equip()
            End With

            DRM = New Proc(s)
            With DRM
                ._Name = "DeathRuneMastery"
                .ProcChance = Sim.Character.Talents.Talent("DRM").Value
                If .ProcChance > 0 Then
                    If .ProcChance > 0.85 Then .ProcChance = 1.0
                    .Equip()
                End If
            End With

            SuddenDoom = New Proc(s)
            With SuddenDoom
                ._Name = "SuddenDoom"
                .ProcChance = Sim.Character.Talents.Talent("SuddenDoom").Value * 0.05
                .ProcOn = ProcsManager.ProcOnType.OnWhitehit
                .ProcLenght = 20
                If .ProcChance > 0 Then
                    .Equip()
                End If
            End With


            ThreatOfThassarian = New Proc(s)
            With ThreatOfThassarian
                ._Name = "ThreatOfThassarian"
                If Sim.Character.Dual Then
                    .ProcChance = 0.3 * Sim.Character.Talents.Talent("ThreatOfThassarian").Value
                End If
                If .ProcChance > 0 Then
                    If .ProcChance > 0.85 Then .ProcChance = 1.0
                    If Sim.Character.DualW Then .Equip()
                End If
            End With




            ReapingBotN = New Proc(s)
            With ReapingBotN
                If Sim.Character.Talents.Talent("Reaping").Value Then
                    ._Name = "Reaping"
                    .ProcChance = Sim.Character.Talents.Talent("Reaping").Value
                ElseIf Sim.Character.Talents.Talent("BloodoftheNorth").Value Then
                    ._Name = "BloodoftheNorth"
                    .ProcChance = Sim.Character.Talents.Talent("BloodoftheNorth").Value
                End If
                If .ProcChance > 0 Then
                    If .ProcChance > 0.85 Then .ProcChance = 1.0
                    '.ProcOn = ProcOnType.OnBloodStrike
                    .Equip()
                End If
            End With


            T104PDPS = New Proc(s)
            With T104PDPS
                ._Name = "T104PDPS"
                If Sim.Character.T104PDPS Then .Equip()
                .ProcLenght = 15
                .ProcValue = 3
                .ProcChance = 1

            End With

            KillingMachine = New Proc(s)
            With KillingMachine
                ._Name = "KillingMachine"
                .ProcOn = ProcsManager.ProcOnType.OnMHWhiteHit
                If Sim.Character.Talents.Talent("KillingMachine").Value > 0 Then .Equip()
                .Equiped = Sim.Character.Talents.Talent("KillingMachine").Value
                .ProcLenght = 30
                .ProcChance = (Sim.Character.Talents.Talent("KillingMachine").Value) * s.Character.MHWeaponSpeed / 60
            End With

            Rime = New Proc(s)
            With Rime
                ._Name = "Rime"
                If Sim.Character.Talents.Talent("Rime").Value > 0 Then .Equip()
                .Equiped = Sim.Character.Talents.Talent("Rime").Value
                .ProcLenght = 15
                .ProcChance = 15 * Sim.Character.Talents.Talent("Rime").Value / 100
            End With

            ScentOfBlood = New ScentOfBlood(s)
            With ScentOfBlood
                ._Name = "ScentOfBlood"
                If s.Character.GetPresence = "Blood" Then
                    .ProcOn = ProcOnType.OnBossHitOrMiss
                    .MaxStack = Sim.Character.Talents.Talent("ScentOfBlood").Value
                    .ProcValue = 3
                    .ProcLenght = 60
                    .ProcChance = 15 / 100
                    If .MaxStack > 0 Then .Equip()
                End If
            End With

            With New Proc(s)
                ._Name = "Virulence"
                .ProcLenght = 20
                .ProcChance = 0.85
                .ProcValue = 200
                .ProcType = Sim.Stat.Strength
                .ProcOn = ProcOnType.OnFU
                If s.Sigils.Virulence Then .Equip()
            End With

            With New Proc(s)
                ._Name = "HangedMan"
                .ProcLenght = 15
                .ProcChance = 1
                .ProcValueStack = 73
                .ProcTypeStack = Sim.Stat.Strength
                .MaxStack = 3
                .ProcOn = ProcsManager.ProcOnType.OnFU
                If s.Sigils.HangedMan Then .Equip()
            End With


            Strife = New Proc(s)
            With Strife
                ._Name = "Strife"
                .ProcChance = 1
                .ProcValue = 144
                .ProcLenght = 10
                .ProcType = Sim.Stat.AP
                If s.Sigils.Strife Then .Equip()
            End With

            With New Proc(s)
                ._Name = "T92PDPS"
                .ProcChance = 0.5
                .ProcValue = 180
                .ProcLenght = 15
                .InternalCD = 45
                .ProcType = Sim.Stat.Strength
                .ProcOn = ProcsManager.ProcOnType.OnBloodStrike
                If s.Character.T92PDPS = 1 Then .Equip()
            End With

            With New Proc(s)
                ._Name = "HauntedDreams"
                .ProcChance = 0.15
                .ProcValue = 173
                .ProcLenght = 10
                .InternalCD = 45
                .ProcType = Sim.Stat.Crit
                .ProcOn = ProcsManager.ProcOnType.OnBloodStrike
                If s.Sigils.HauntedDreams Then .Equip()
            End With


            With New Proc(s)
                ._Name = "OrcRacial"
                .InternalCD = 120
                .ProcOn = ProcsManager.ProcOnType.OnDamage
                .ProcChance = 1
                .ProcLenght = 15
                .ProcValue = 322
                .ProcType = Sim.Stat.AP
                If s.Character.Orc Then .Equip()
            End With

            TrollRacial = New Proc(s)
            With TrollRacial
                ._Name = "TrollRacial"
                .InternalCD = 180
                .ProcChance = 1
                .ProcLenght = 15
                .ProcValue = 0.2
                .ProcOn = ProcsManager.ProcOnType.OnDamage
                If s.Character.Troll Then .Equip()
            End With

            With New WeaponProc(s)
                ._Name = "BElfRacial"
                .InternalCD = 120
                .ProcChance = 1
                .ProcLenght = 0
                .ProcValue = 15
                .DamageType = "torrent"
                .ProcOn = ProcsManager.ProcOnType.OnDamage
                If s.Character.BloodElf Then .Equip()
            End With

            With New WeaponProc(s)
                ._Name = "BloodParasite"
                .InternalCD = 20
                .ProcChance = 3 * s.Character.Talents.Talent("BloodParasite").Value / 100
                .DamageType = "BloodWorms"
                .ProcOn = ProcsManager.ProcOnType.OnHit
                If s.Character.Talents.Talent("BloodParasite").Value > 0 Then
                    .Equip()
                End If
                .isGuardian = True
            End With
            Dim Shadowmourne As New WeaponProc(s)
            With Shadowmourne
                .ProcOn = ProcsManager.ProcOnType.OnMHhit
                .ProcChance = 1
                .ProcValueStack = 30
                .ProcValue = 2000
                .ProcLenght = 60 ' Soul Fragment Duration
                .ProcTypeStack = Sim.Stat.Strength
                .DamageType = "Shadowmourne"
                .HasteSensible = True
                Try
                    If XmlCharacter.<character>.<WeaponProc>.<MHShadowmourne>.Value = 1 Then
                        ._Name = "Shadowmourne"
                        .Equip()
                        .InternalCD = 10 'Chaos Bane Duration
                    End If
                Catch
                End Try
                Try
                    If XmlCharacter.<character>.<WeaponProc>.<MHShadowmourneCancelCB>.Value = 1 Then
                        ._Name = "Shadowmourne (Cancel CB)"
                        .Equip()
                        .InternalCD = 0.1 'Chaos Bane Duration
                    End If
                Catch

                End Try
            End With

            Dim Bryntroll As New WeaponProc(s)
            With Bryntroll
                .ProcOn = ProcsManager.ProcOnType.OnMHhit
                .ProcChance = 0.1133
                .DamageType = "Bryntroll"
                .ProcLenght = 0
                .HasteSensible = True
                Try
                    If XmlCharacter.<character>.<WeaponProc>.<MHBryntrollHeroic>.Value = 1 Then
                        ._Name = "BryntrollHeroic"
                        .ProcValue = 2538
                        .Equip()
                    End If
                Catch
                End Try
                Try
                    If XmlCharacter.<character>.<WeaponProc>.<MHBryntroll>.Value = 1 Then
                        ._Name = "Bryntroll"
                        .ProcValue = 2250
                        .Equip()
                    End If
                Catch
                End Try
            End With
            With New Proc(s)
                ._Name = "AshenBand"
                .ProcChance = 0.1
                .ProcLenght = 10
                .ProcValue = 480
                .InternalCD = 45
                .ProcType = Sim.Stat.AP
                .ProcOn = ProcsManager.ProcOnType.OnHit
                Try
                    If XmlCharacter.<character>.<misc>.<AshenBand>.Value = True Then
                        .Equip()
                    End If
                Catch ex As System.Exception

                End Try
            End With
            Dim MHtemperedViskag As New WeaponProc(s)
            With MHtemperedViskag
                .ProcChance = 0.04
                .ProcLenght = 0
                .ProcValue = 2222
                .InternalCD = 0
                .DamageType = "physical"
                ._Name = "MHtemperedViskag"
                .ProcOn = ProcsManager.ProcOnType.OnMHhit
                .HasteSensible = True
                Try
                    If Sim.XmlCharacter.<character>.<WeaponProc>.<MHtemperedViskag>.Value = 1 Then .Equip()
                Catch
                End Try
            End With

            Dim OHtemperedViskag As New WeaponProc(s)
            With OHtemperedViskag
                .ProcChance = 0.04
                .ProcLenght = 0
                .ProcValue = 2222
                .InternalCD = 0
                .DamageType = "physical"
                ._Name = "OHtemperedViskag"
                .ProcOn = ProcsManager.ProcOnType.OnOHhit
                .HasteSensible = True
                Try
                    If Sim.XmlCharacter.<character>.<WeaponProc>.<OHtemperedViskag>.Value = 1 Then .Equip()
                Catch
                End Try
            End With

            Dim MHSingedViskag As New WeaponProc(s)
            With MHSingedViskag
                .ProcChance = 0.04
                .ProcLenght = 0
                .ProcValue = 2000
                .InternalCD = 0
                .DamageType = "physical"
                ._Name = "MHSingedViskag"
                .ProcOn = ProcsManager.ProcOnType.OnMHhit
                .HasteSensible = True
                Try
                    If Sim.XmlCharacter.<character>.<WeaponProc>.<MHSingedViskag>.Value = 1 Then .Equip()
                Catch
                End Try


            End With

            Dim OHSingedViskag As New WeaponProc(s)
            With OHSingedViskag
                .ProcChance = 0.04
                .ProcLenght = 0
                .ProcValue = 2000
                .InternalCD = 0
                .DamageType = "physical"
                ._Name = "OHSingedViskag"
                .ProcOn = ProcsManager.ProcOnType.OnOHhit
                .HasteSensible = True
                Try
                    If Sim.XmlCharacter.<character>.<WeaponProc>.<OHSingedViskag>.Value = 1 Then .Equip()
                Catch
                End Try
            End With

            Dim MHEmpoweredDeathbringer As New WeaponProc(s)
            With MHEmpoweredDeathbringer
                .ProcChance = 0.065
                .ProcLenght = 0
                .ProcValue = 1500
                .InternalCD = 0
                .DamageType = "shadow"
                ._Name = "MH Empowered Deathbringer"
                .ProcOn = ProcsManager.ProcOnType.OnMHhit
                .HasteSensible = True
                Try
                    If Sim.XmlCharacter.<character>.<WeaponProc>.<MHEmpoweredDeathbringer>.Value = 1 Then .Equip()
                Catch ex As System.Exception

                End Try
            End With

            Dim OHEmpoweredDeathbringer As New WeaponProc(s)
            With OHEmpoweredDeathbringer
                .ProcChance = 0.065
                .ProcLenght = 0
                .ProcValue = 1500
                .InternalCD = 0
                .DamageType = "shadow"
                ._Name = "OH Deathbringer"
                .ProcOn = ProcsManager.ProcOnType.OnOHhit
                .HasteSensible = True
                Try
                    If Sim.XmlCharacter.<character>.<WeaponProc>.<OHEmpoweredDeathbringer>.Value = 1 Then .Equip()
                Catch ex As System.Exception
                End Try
            End With

            Dim MHRagingDeathbringer As New WeaponProc(s)
            With MHRagingDeathbringer
                .ProcChance = 0.065
                .ProcLenght = 0
                .ProcValue = 1666
                .InternalCD = 0
                .DamageType = "shadow"
                ._Name = "MH Raging Deathbringer"
                .ProcOn = ProcsManager.ProcOnType.OnMHhit
                .HasteSensible = True
                Try
                    If Sim.XmlCharacter.<character>.<WeaponProc>.<MHRagingDeathbringer>.Value = 1 Then .Equip()
                Catch
                End Try

            End With

            Dim OHRagingDeathbringer As New WeaponProc(s)
            With OHRagingDeathbringer
                .ProcChance = 0.065
                .ProcLenght = 0
                .ProcValue = 1666
                .InternalCD = 0
                .DamageType = "shadow"
                ._Name = "OH Raging Deathbringer"
                .ProcOn = ProcsManager.ProcOnType.OnOHhit
                .HasteSensible = True
                Try
                    If Sim.XmlCharacter.<character>.<WeaponProc>.<OHRagingDeathbringer>.Value = 1 Then .Equip()
                Catch ex As System.Exception

                End Try


            End With

            Dim HandMountedPyroRocket As New WeaponProc(s)
            With HandMountedPyroRocket
                .ProcChance = 1
                .ProcLenght = 0
                .ProcValue = 1837
                .InternalCD = 45
                .DamageType = "arcane"
                ._Name = "Hand Mounted Pyro Rocket"
                .ProcOn = ProcsManager.ProcOnType.OnDamage
                Try
                    If XmlCharacter.<character>.<misc>.<HandMountedPyroRocket>.Value = True Then
                        .Equip()
                    End If
                Catch ex As System.Exception

                End Try
            End With

            Dim HyperspeedAccelerators As New WeaponProc(s)
            With HyperspeedAccelerators
                .ProcChance = 0.5
                .ProcLenght = 12
                .ProcValue = 340
                .InternalCD = 60
                .ProcType = Sim.Stat.Haste
                ._Name = "Hyperspeed Accelerators"
                .ProcOn = ProcsManager.ProcOnType.OnHit
                Try
                    If XmlCharacter.<character>.<misc>.<HyperspeedAccelerators>.Value = True Then
                        .Equip()
                    End If
                Catch ex As System.Exception
                End Try
            End With





            Dim TailorEnchant As New Proc(s)
            With TailorEnchant
                .ProcChance = 0.25
                .ProcLenght = 15
                .ProcValue = 400
                .InternalCD = 60
                .ProcType = Sim.Stat.AP
                ._Name = "Swordguard Embroidery"
                .ProcOn = ProcsManager.ProcOnType.OnHit

                Try
                    If XmlCharacter.<character>.<misc>.<TailorEnchant>.Value = True Then
                        .Equip()
                    End If
                Catch ex As System.Exception

                End Try
            End With

            Dim SaroniteBomb As New WeaponProc(s)
            With SaroniteBomb
                .ProcChance = 0.5
                .ProcLenght = 0
                .ProcValue = 1325
                .DamageType = "SaroniteBomb"
                .InternalCD = 60
                ._Name = "Saronite Bomb"
                .ProcOn = ProcsManager.ProcOnType.OnMHhit
                .HasteSensible = False
                Try
                    If Sim.XmlCharacter.<character>.<misc>.<SaroniteBomb>.Value Then .Equip()
                Catch ex As System.Exception
                End Try
            End With

            Dim SapperCharge As New WeaponProc(s)
            With SapperCharge
                .ProcChance = 0.5
                .ProcLenght = 0
                .ProcValue = 2500
                .DamageType = "SapperCharge"
                .InternalCD = 300
                ._Name = "Global Thermal Sapper Charge"
                .ProcOn = ProcsManager.ProcOnType.OnMHhit
                .HasteSensible = False
                Try
                    If Sim.XmlCharacter.<character>.<misc>.<SapperCharge>.Value Then .Equip()
                Catch ex As System.Exception
                End Try
            End With

            Dim IndestructiblePotion As New Proc(s)
            With IndestructiblePotion
                .ProcChance = 0.5
                .ProcLenght = 120
                .ProcValue = 2500
                .ProcType = Sim.Stat.Armor
                .InternalCD = 6000 '10 minutes
                ._Name = "Indestructible Potion"
                .ProcOn = ProcsManager.ProcOnType.OnMHhit
                .HasteSensible = False
                Try
                    If XmlCharacter.<character>.<misc>.<IndestructiblePotion>.Value Then .Equip()
                Catch ex As System.Exception
                End Try
            End With

            Dim PotionofSpeed As New Proc(s)
            With PotionofSpeed
                .ProcChance = 0.5
                .ProcLenght = 15
                .ProcValue = 500
                .ProcType = Sim.Stat.Haste
                .InternalCD = 6000 '10 minutes
                ._Name = "Potion of Speed"
                .ProcOn = ProcsManager.ProcOnType.OnMHhit
                .HasteSensible = False
                Try
                    If Sim.XmlCharacter.<character>.<misc>.<PotionofSpeed>.Value Then
                        .Equip()
                    End If
                Catch ex As System.Exception
                End Try
            End With

        End Sub

        Function GetActiveBonus(ByVal stat As Sim.Stat) As Integer
            If stat = Sim.Stat.None Then Return 0
            Dim tmp As Integer
            For Each prc As Proc In (From p As Proc In EquipedProc
                             Where p.ProcType = stat And p.IsActive
                             )
                tmp += prc.ProcValue
            Next

            For Each prc As Proc In (From p As Proc In EquipedProc
                             Where p.ProcTypeStack = stat And p.IsActive
                             )
                tmp += prc.ProcValue * prc.Stack
            Next
            Return tmp
        End Function

        Function GetMaxPossibleBonus(ByVal stat As Sim.Stat) As Integer
            If stat = Sim.Stat.None Then Return 0
            Dim tmp As Integer
            For Each prc As Proc In (From p As Proc In EquipedProc
                             Where p.ProcType = stat
                             )
                tmp += prc.ProcValue
            Next

            For Each prc As Proc In (From p As Proc In EquipedProc
                             Where p.ProcTypeStack = stat
                             )
                tmp += prc.ProcValue * prc.Stack
            Next
            Return tmp
        End Function

        Sub tryT104PDPS(ByVal T As Long)
            If Sim.Character.T104PDPS = 0 Then Exit Sub
            If Sim.Runes.BloodRunes.Available Then Exit Sub
            If Sim.Runes.FrostRunes.Available Then Exit Sub
            If Sim.Runes.UnholyRunes.Available Then Exit Sub
            T104PDPS.ApplyMe(T)
        End Sub

        Sub tryProcs(ByVal ProcOnthat As ProcOnType)
            For Each obj In (From s As Proc In EquipedProc Where s.ProcOn = ProcOnthat)
                obj.TryMe(Sim.TimeStamp)
            Next
            Select Case ProcOnthat
                Case ProcOnType.OnMHWhiteHit
                    tryProcs(ProcOnType.OnWhitehit)
                    tryProcs(ProcOnType.OnMHhit)
                    tryProcs(ProcOnType.OnHit)

                Case ProcOnType.OnOHWhitehit
                    tryProcs(ProcOnType.OnWhitehit)
                    tryProcs(ProcOnType.OnOHhit)

                Case ProcOnType.OnMHhit
                    tryProcs(ProcOnType.OnHit)
                    tryProcs(ProcOnType.OnDamage)

                Case ProcOnType.OnOHhit
                    tryProcs(ProcOnType.OnHit)
                    tryProcs(ProcOnType.OnDamage)

                Case ProcOnType.OnDoT
                    tryProcs(ProcOnType.OnDamage)



            End Select


        End Sub
    End Class
End Namespace
