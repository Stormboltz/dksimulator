Imports System.Xml.Linq

'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 00:22
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class Procs
    Friend Bloodlust As Proc

    Friend DRM As Proc
    Friend SuddenDoom As Proc

    Friend AnnihilateDiseases As Proc
    Friend ThreatOfThassarian As Proc
    Friend ReapingBotN As Proc

    Friend T104PDPS As Proc
    Friend IcyTalons As Proc
    Friend Desolation As Proc

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
    Friend OnHitProcs As New List(Of Proc)

    Friend OnMHWhitehitProcs As New List(Of Proc)
    Friend OnMHhitProcs As New List(Of Proc)
    Friend OnOHhitProcs As New List(Of Proc)
    Friend OnFUProcs As New List(Of Proc)
    Friend OnCritProcs As New List(Of Proc)
    Friend OnDamageProcs As New List(Of Proc)
    Friend OnDoTProcs As New List(Of Proc)
    Friend OnBloodStrikeProcs As New List(Of Proc)
    Friend OnPlagueStrikeProcs As New List(Of Proc)
    Friend onRPDumpProcs As New List(Of Proc)
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


        Dim RunicEmpowerment = New RunicEmpowerment(s)
        With RunicEmpowerment
            ._Name = "Runic Empowerment"
            .ProcChance = 0.45
            .ProcOn = ProcOnType.onRPDump
            .Equip()
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
            .ProcChance = Sim.Character.Talents.Talent("DRM").Value * 0.33
            If .ProcChance > 0 Then
                If .ProcChance > 0.85 Then .ProcChance = 1.0
                .Equip()
            End If
        End With

        SuddenDoom = New Proc(s)
        With SuddenDoom
            ._Name = "SuddenDoom"
            .ProcChance = Sim.Character.Talents.Talent("SuddenDoom").Value * 0.05
            .ProcOn = Procs.ProcOnType.OnBloodStrike
            If .ProcChance > 0 Then
                .Equip()
            End If
        End With


        ThreatOfThassarian = New Proc(s)
        With ThreatOfThassarian
            ._Name = "ThreatOfThassarian"
            .ProcChance = 0.3 * Sim.Character.Talents.Talent("ThreatOfThassarian").Value
            If .ProcChance > 0 Then
                If .ProcChance > 0.85 Then .ProcChance = 1.0
                If Sim.MainStat.DualW Then .Equip()
            End If
        End With

        AnnihilateDiseases = New Proc(s)
        With AnnihilateDiseases
            ._Name = "AnnihilateDiseases"
            .ProcChance = 1 - 0.33 * Sim.Character.Talents.Talent("Annihilation").Value
            If .ProcChance > 0.1 Then
                .Equip()
            End If
        End With


        ReapingBotN = New Proc(s)
        With ReapingBotN
            If Sim.Character.Talents.Talent("Reaping").Value Then
                ._Name = "Reaping"
                .ProcChance = Sim.Character.Talents.Talent("Reaping").Value * 0.33
            ElseIf Sim.Character.Talents.Talent("BloodoftheNorth").Value Then
                ._Name = "BloodoftheNorth"
                .ProcChance = Sim.Character.Talents.Talent("BloodoftheNorth").Value * 0.3
            End If

            If .ProcChance > 0 Then
                If .ProcChance > 0.85 Then .ProcChance = 1.0
                .Equip()
            End If
        End With


        T104PDPS = New Proc(s)
        With T104PDPS
            ._Name = "T104PDPS"
            If Sim.MainStat.T104PDPS Then .Equip()
            .ProcLenght = 15
            .ProcValue = 3
            .ProcChance = 1

        End With

        IcyTalons = New Proc(s)
        With IcyTalons
            ._Name = "IcyTalons"
            If Sim.Character.Talents.Talent("IcyTalons").Value > 0 Then .Equip()
            .ProcValue = Sim.Character.Talents.Talent("IcyTalons").Value
            .ProcLenght = 20
            .ProcChance = 1
        End With

        Desolation = New Proc(s)
        With Desolation
            ._Name = "Desolation"
            .ProcOn = Procs.ProcOnType.OnBloodStrike
            .ProcValue = Sim.Character.Talents.Talent("Desolation").Value
            .ProcLenght = 20
            .ProcChance = 1
            If Sim.Character.Talents.Talent("Desolation").Value > 0 Then .Equip()
        End With


        KillingMachine = New Proc(s)
        With KillingMachine
            ._Name = "KillingMachine"
            .ProcOn = Procs.ProcOnType.OnMHWhiteHit
            If Sim.Character.Talents.Talent("KillingMachine").Value > 0 Then .Equip()
            .Equiped = Sim.Character.Talents.Talent("KillingMachine").Value
            .ProcLenght = 30
            .ProcChance = (Sim.Character.Talents.Talent("KillingMachine").Value) * s.MainStat.MHWeaponSpeed / 60
        End With

        Rime = New Proc(s)
        With Rime
            ._Name = "Rime"
            If Sim.Character.Talents.Talent("Rime").Value > 0 Then .Equip()
            .Equiped = Sim.Character.Talents.Talent("Rime").Value
            .ProcLenght = 15
            .ProcChance = 5 * Sim.Character.Talents.Talent("Rime").Value / 100
        End With

        ScentOfBlood = New ScentOfBlood(s)
        With ScentOfBlood
            ._Name = "ScentOfBlood"
            If s.FrostPresence = 1 Then
                .Equip()
                .Equiped = Sim.Character.Talents.Talent("ScentOfBlood").Value
            Else
                .Equiped = 0
            End If
            .ProcLenght = 60
            .ProcChance = 0.15
        End With

        With New Proc(s)
            ._Name = "Virulence"
            .ProcLenght = 20
            .ProcChance = 0.85
            .ProcValue = 200
            .ProcType = "str"
            .ProcOn = Procs.ProcOnType.OnFU
            If s.Sigils.Virulence Then .Equip()
        End With

        With New Proc(s)
            ._Name = "HangedMan"
            .ProcLenght = 15
            .ProcChance = 1
            .ProcValueStack = 73
            .ProcTypeStack = "str"
            .MaxStack = 3
            .ProcOn = Procs.ProcOnType.OnFU
            If s.Sigils.HangedMan Then .Equip()
        End With


        Strife = New Proc(s)
        With Strife
            ._Name = "Strife"
            .ProcChance = 1
            .ProcValue = 144
            .ProcLenght = 10
            .ProcType = "ap"
            If s.Sigils.Strife Then .Equip()
        End With

        With New Proc(s)
            ._Name = "T92PDPS"
            .ProcChance = 0.5
            .ProcValue = 180
            .ProcLenght = 15
            .InternalCD = 45
            .ProcType = "str"
            .ProcOn = Procs.ProcOnType.OnBloodStrike
            If s.MainStat.T92PDPS = 1 Then .Equip()
        End With

        With New Proc(s)
            ._Name = "HauntedDreams"
            .ProcChance = 0.15
            .ProcValue = 173
            .ProcLenght = 10
            .InternalCD = 45
            .ProcType = "crit"
            .ProcOn = Procs.ProcOnType.OnBloodStrike
            If s.Sigils.HauntedDreams Then .Equip()
        End With


        With New Proc(s)
            ._Name = "OrcRacial"
            .InternalCD = 120
            .ProcOn = Procs.ProcOnType.OnDamage
            .ProcChance = 1
            .ProcLenght = 15
            .ProcValue = 322
            .ProcType = "ap"
            If s.Character.Orc Then .Equip()
        End With

        TrollRacial = New Proc(s)
        With TrollRacial
            ._Name = "TrollRacial"
            .InternalCD = 180
            .ProcChance = 1
            .ProcLenght = 15
            .ProcValue = 0.2
            .ProcOn = Procs.ProcOnType.OnDamage
            If s.Character.Troll Then .Equip()
        End With

        With New WeaponProc(s)
            ._Name = "BElfRacial"
            .InternalCD = 120
            .ProcChance = 1
            .ProcLenght = 0
            .ProcValue = 15
            .DamageType = "torrent"
            .ProcOn = Procs.ProcOnType.OnDamage
            If s.Character.BloodElf Then .Equip()
        End With

        With New WeaponProc(s)
            ._Name = "BloodWorms"
            .InternalCD = 20
            .ProcChance = 3 * s.Character.Talents.Talent("BloodWorms").Value / 100
            .DamageType = "BloodWorms"
            .ProcOn = Procs.ProcOnType.OnHit
            If s.Character.Talents.Talent("BloodWorms").Value > 0 Then
                .Equip()
            End If
            .isGuardian = True
        End With
        Dim Shadowmourne As New WeaponProc(s)
        With Shadowmourne
            .ProcOn = Procs.ProcOnType.OnMHhit
            .ProcChance = 1
            .ProcValueStack = 30
            .ProcValue = 2000
            .ProcLenght = 60 ' Soul Fragment Duration
            .ProcTypeStack = "str"
            .DamageType = "Shadowmourne"
            .HasteSensible = True
            Try
                If XmlCharacter.Element("character").Element("WeaponProc").Element("MHShadowmourne").Value = 1 Then
                    ._Name = "Shadowmourne"
                    .Equip()
                    .InternalCD = 10 'Chaos Bane Duration
                End If
            Catch
            End Try
            Try
                If XmlCharacter.Element("character").Element("WeaponProc").Element("MHShadowmourneCancelCB").Value = 1 Then
                    ._Name = "Shadowmourne (Cancel CB)"
                    .Equip()
                    .InternalCD = 0.1 'Chaos Bane Duration
                End If
            Catch

            End Try
        End With

        Dim Bryntroll As New WeaponProc(s)
        With Bryntroll
            .ProcOn = Procs.ProcOnType.OnMHhit
            .ProcChance = 0.1133
            .DamageType = "Bryntroll"
            .ProcLenght = 0
            .HasteSensible = True
            Try
                If XmlCharacter.Element("character").Element("WeaponProc").Element("MHBryntrollHeroic").Value = 1 Then
                    ._Name = "BryntrollHeroic"
                    .ProcValue = 2538
                    .Equip()
                End If
            Catch
            End Try
            Try
                If XmlCharacter.Element("character").Element("WeaponProc").Element("MHBryntroll").Value = 1 Then
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
            .ProcType = "ap"
            .ProcOn = Procs.ProcOnType.OnHit
            Try
                If XmlCharacter.Element("character").Element("misc").Element("AshenBand").Value = True Then
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
            .ProcOn = Procs.ProcOnType.OnMHhit
            .HasteSensible = True
            Try
                If Sim.XmlCharacter.Element("character").Element("WeaponProc").Element("MHtemperedViskag").Value = 1 Then .Equip()
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
            .ProcOn = Procs.ProcOnType.OnOHhit
            .HasteSensible = True
            Try
                If Sim.XmlCharacter.Element("character").Element("WeaponProc").Element("OHtemperedViskag").Value = 1 Then .Equip()
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
            .ProcOn = Procs.ProcOnType.OnMHhit
            .HasteSensible = True
            Try
                If Sim.XmlCharacter.Element("character").Element("WeaponProc").Element("MHSingedViskag").Value = 1 Then .Equip()
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
            .ProcOn = Procs.ProcOnType.OnOHhit
            .HasteSensible = True
            Try
                If Sim.XmlCharacter.Element("character").Element("WeaponProc").Element("OHSingedViskag").Value = 1 Then .Equip()
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
            .ProcOn = Procs.ProcOnType.OnMHhit
            .HasteSensible = True
            Try
                If Sim.XmlCharacter.Element("character").Element("WeaponProc").Element("MHEmpoweredDeathbringer").Value = 1 Then .Equip()
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
            .ProcOn = Procs.ProcOnType.OnOHhit
            .HasteSensible = True
            Try
                If Sim.XmlCharacter.Element("character").Element("WeaponProc").Element("OHEmpoweredDeathbringer").Value = 1 Then .Equip()
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
            .ProcOn = Procs.ProcOnType.OnMHhit
            .HasteSensible = True
            Try
                If Sim.XmlCharacter.Element("character").Element("WeaponProc").Element("MHRagingDeathbringer").Value = 1 Then .Equip()
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
            .ProcOn = Procs.ProcOnType.OnOHhit
            .HasteSensible = True
            Try
                If Sim.XmlCharacter.Element("character").Element("WeaponProc").Element("OHRagingDeathbringer").Value = 1 Then .Equip()
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
            .ProcOn = Procs.ProcOnType.OnDamage
            Try
                If XmlCharacter.Element("character").Element("misc").Element("HandMountedPyroRocket").Value = True Then
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
            .ProcType = "haste"
            ._Name = "Hyperspeed Accelerators"
            .ProcOn = Procs.ProcOnType.OnHit
            Try
                If XmlCharacter.Element("character").Element("misc").Element("HyperspeedAccelerators").Value = True Then
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
            .ProcType = "ap"
            ._Name = "Swordguard Embroidery"
            .ProcOn = Procs.ProcOnType.OnHit

            Try
                If XmlCharacter.Element("character").Element("misc").Element("TailorEnchant").Value = True Then
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
            .ProcOn = Procs.ProcOnType.OnMHhit
            .HasteSensible = False
            Try
                If Sim.XmlCharacter.Element("character").Element("misc").Element("SaroniteBomb").Value Then .Equip()
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
            .ProcOn = Procs.ProcOnType.OnMHhit
            .HasteSensible = False
            Try
                If Sim.XmlCharacter.Element("character").Element("misc").Element("SapperCharge").Value Then .Equip()
            Catch ex As System.Exception
            End Try
        End With

        Dim IndestructiblePotion As New Proc(s)
        With IndestructiblePotion
            .ProcChance = 0.5
            .ProcLenght = 120
            .ProcValue = 2500
            .ProcType = "armor"
            .InternalCD = 6000 '10 minutes
            ._Name = "Indestructible Potion"
            .ProcOn = Procs.ProcOnType.OnMHhit
            .HasteSensible = False
            Try
                If XmlCharacter.Element("character").Element("misc").Element("IndestructiblePotion").Value Then .Equip()
            Catch ex As System.Exception
            End Try
        End With

        Dim PotionofSpeed As New Proc(s)
        With PotionofSpeed
            .ProcChance = 0.5
            .ProcLenght = 15
            .ProcValue = 500
            .ProcType = "haste"
            .InternalCD = 6000 '10 minutes
            ._Name = "Potion of Speed"
            .ProcOn = Procs.ProcOnType.OnMHhit
            .HasteSensible = False
            Try
                If Sim.XmlCharacter.Element("character").Element("misc").Element("PotionofSpeed").Value Then
                    .Equip()
                End If
            Catch ex As System.Exception
            End Try
        End With

    End Sub

    Function GetActiveBonus(ByVal stat As String) As Integer
        Dim prc As Proc
        Dim tmp As Integer
        For Each prc In EquipedProc
            If prc.ProcType = stat Then
                If prc.IsActive Then
                    tmp += prc.ProcValue
                End If
            End If
            If prc.ProcTypeStack = stat Then
                If prc.IsActive Then
                    tmp += prc.ProcValueStack * prc.Stack
                Else
                    prc.Stack = 0
                    'I don't think this ever arises in practice
                    'but when such buffs fade they should set the stack back to 0
                End If
            End If
        Next
        Return tmp
    End Function

    Function GetMaxPossibleBonus(ByVal stat As String) As Integer
        Dim prc As Proc
        Dim tmp As Integer
        For Each prc In EquipedProc
            If prc.ProcType = stat Then
                tmp += prc.ProcValue
            End If
        Next
        Return tmp
    End Function

    Sub tryT104PDPS(ByVal T As Long)
        If Sim.MainStat.T104PDPS = 0 Then Exit Sub
        If Sim.Runes.BloodRunes.Available Then Exit Sub
        If Sim.Runes.FrostRunes.Available Then Exit Sub
        If Sim.Runes.UnholyRunes.Available Then Exit Sub
        T104PDPS.ApplyMe(T)
    End Sub
    Sub TryOnMHHitProc()
        Dim obj As Proc
        For Each obj In OnMHhitProcs
            obj.TryMe(Sim.TimeStamp)
        Next

        For Each obj In OnHitProcs
            obj.TryMe(Sim.TimeStamp)
        Next
        tryOnDamageProc()
    End Sub

    Sub TryOnOHHitProc()
        Dim obj As Proc
        For Each obj In OnOHhitProcs
            obj.TryMe(Sim.TimeStamp)
        Next
        For Each obj In OnHitProcs
            obj.TryMe(Sim.TimeStamp)
        Next
        tryOnDamageProc()
    End Sub

    Sub TryOnFU()
        Dim obj As Proc
        For Each obj In OnFUProcs
            obj.TryMe(Sim.TimeStamp)
        Next
    End Sub

    Sub tryOnCrit()
        Dim obj As Proc
        For Each obj In OnCritProcs
            obj.TryMe(Sim.TimeStamp)
        Next
    End Sub

    Sub tryOnDoT()
        Dim obj As Proc
        For Each obj In OnDoTProcs
            obj.TryMe(Sim.TimeStamp)
        Next
        tryOnDamageProc()
    End Sub

    Sub TryOnSpellHit()
        Dim obj As Proc
        For Each obj In OnDamageProcs
            obj.TryMe(Sim.TimeStamp)
        Next
        tryOnDamageProc()
    End Sub

    Sub TryOnBloodStrike()
        Dim obj As Proc
        For Each obj In OnBloodStrikeProcs
            obj.TryMe(Sim.TimeStamp)
        Next
    End Sub
    Sub tryOnDamageProc()
        Dim obj As Proc
        For Each obj In OnDamageProcs
            obj.TryMe(Sim.TimeStamp)
        Next
    End Sub

    Sub tryOnMHWhitehitProc()
        Dim obj As Proc
        For Each obj In OnMHWhitehitProcs
            obj.TryMe(Sim.TimeStamp)
        Next
        TryOnMHHitProc()
    End Sub
    Sub TryOnonRPDumpProcs()
        Dim obj As Proc
        For Each obj In onRPDumpProcs
            obj.TryMe(Sim.TimeStamp)
        Next
    End Sub
End Class
