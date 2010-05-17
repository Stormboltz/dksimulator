Imports Microsoft.VisualBasic
Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO

Public Class Sim



    Friend Cataclysm As Boolean = False
    Friend UselessCheck As Long
    Friend UselessCheckColl As New Collection

    Friend FightLength As Integer
    Friend ScenarioPath As String


    'Friend Patch as Boolean
    Friend TotalDamageAlternative As Long
    Friend NextFreeGCD As Long
    Friend GCDUsage As Spells.Spell

    Friend latency As Long
    Friend TimeStamp As Long
    Friend TimeStampCounter As Long
    Friend _EPStat As String
    Friend EPBase As Integer
    Friend DPS As Long
    Friend TPS As Long
    Friend MaxTime As Long

    Friend RotationStep As Integer
    Friend Rotate As Boolean
    Friend rotationPath As String
    Friend IntroPath As String
    Friend PetFriendly As Boolean
    Friend BloodToSync As Boolean
    Friend KeepBloodSync As Boolean
    Friend ShowProc As Boolean
    Friend MultipleDamage As New Collections.Generic.List(Of Long)
    Friend NextReset As Integer
    Friend LastReset As Integer

    Private SimStart As Date
    Friend ICCDamageBuff As Integer
    Friend BoneShieldTTL As Integer
    Friend FutureEventManager As New FutureEventManager(Me)
    Friend Threat As Long
    Private AMSTimer As Long
    Private AMSCd As Integer
    Private AMSAmount As Integer
    Private ShowDpsTimer As Long

    Friend KeepRNGSeed As Boolean

    Friend KeepDiseaseOnOthersTarget As Boolean


    Friend RandomNumberGenerator As RandomNumberGenerator

    Friend Runes As Runes.runes

    Friend RunicPower As RunicPower
    Friend Character As Character
    Friend MainStat As MainStat
    Friend Sigils As Sigils
    Friend boss As Boss

    'Strike Creation
    Friend BloodCakedBlade As BloodCakedBlade
    Friend OHBloodCakedBlade As BloodCakedBlade

    Friend BloodStrike As BloodStrike
    Friend OHBloodStrike As BloodStrike

    Friend Obliterate As Obliterate
    Friend OHObliterate As Obliterate

    Friend PlagueStrike As PlagueStrike
    Friend OHPlagueStrike As PlagueStrike

    Friend FrostStrike As FrostStrike
    Friend OHFrostStrike As FrostStrike

    Friend RuneStrike As RuneStrike
    Friend OHRuneStrike As RuneStrike

    Friend DeathStrike As DeathStrike
    Friend OHDeathStrike As DeathStrike

    Friend HeartStrike As HeartStrike



    Friend ScourgeStrike As ScourgeStrike
    Friend ScourgeStrikeMagical As ScourgeStrikeMagical
    Friend MainHand As MainHand
    Friend OffHand As OffHand

    Friend GhoulStat As GhoulStat
    Friend Ghoul As Ghoul
    Friend AotD As AotD

    'Spell Creation
    Friend BloodBoil As BloodBoil
    Friend BloodTap As BloodTap
    Friend Butchery As Butchery
    Friend DeathandDecay As DeathandDecay
    Friend DeathChill As DeathChill

    'Friend Desolation As Desolation
    Friend Horn As Horn
    Friend HowlingBlast As HowlingBlast
    Friend Hysteria As Hysteria
    Friend IcyTouch As IcyTouch
    Friend Necrosis As Necrosis
    Friend OHNecrosis As Necrosis
    Friend DeathCoil As DeathCoil
    Friend Pestilence As Pestilence
    Friend UnbreakableArmor As UnbreakableArmor
    Friend UnholyBlight As UnholyBlight
    Friend DRW As DRW
    Friend WanderingPlague As WanderingPlague
    Friend Gargoyle As Gargoyle
    Friend BoneShield As BoneShield
    Friend Frenzy As Frenzy
    Friend ERW As EmpowerRuneWeapon




    Friend proc As Procs
    Friend Targets As Targets.TargetsManager




    Friend Priority As priority
    Friend Rotation As Rotation
    Friend RuneForge As RuneForge

    Friend Trinkets As Trinkets

    Friend DamagingObject As New Collections.Generic.List(Of Supertype)
    Friend TrinketsCollection As New Collections.Generic.List(Of Proc)
    Friend ProcCollection As New Collections.Generic.List(Of Proc)

    Friend MergeReport As Boolean
    Friend WaitForFallenCrusader As Boolean





    Friend CombatLog As New CombatLog(Me)
    Friend BoneShieldUsageStyle As Integer
    Friend BloodPresence As Integer
    Friend UnholyPresence As Integer
    Friend FrostPresence As Integer

    Friend BloodPresenceSwitch As BloodPresence
    Friend UnholyPresenceSwitch As UnholyPresence
    Friend FrostPresenceSwitch As FrostPresence


    Friend SaveRPForRS As Boolean


    Friend ReportName As String

    Friend XmlCharacter As New XDocument
    Friend XmlConfig As New XDocument


    Friend Scenario As Scenarios.Scenario





    Sub Init()
    End Sub





    Function EPStat() As String
        Return _EPStat
    End Function


    Function ExecuteRange() As Boolean
        If (TimeStamp - LastReset) / (NextReset - LastReset) > 0.75 Then
            Return True
        Else
            Return False
        End If
    End Function


    Private SimTime As Double
    Sub Prepare(ByVal SimTime As Double, ByVal MainFrm As MainForm)
        Me.SimTime = SimTime
        _MainFrm = MainFrm
    End Sub
    Sub Prepare(ByVal SimTime As Double, ByVal MainFrm As MainForm, ByVal EPS As String, ByVal EPBse As String)
        Me.SimTime = SimTime
        _MainFrm = MainFrm
        _EPStat = EPS
        EPBase = EPBse
    End Sub

    Sub PrePull(ByVal T As Long)
        AotD.PrePull(T)
        'Pot Usage
        Try
            If Me.Character.XmlDoc.Element("/character/misc/PrePullIndestructiblePotion").Value Then
                Dim p As Proc
                p = proc.Find("Indestructible Potion")
                If p.Equiped = 0 Then
                    p.Equip()
                    p.ProcChance = 0
                End If
                p.ApplyMe(T - 1000)
                p.CD = T + 120 * 100
            End If
        Catch ex As System.Exception

        End Try
        Try
            If Me.Character.XmlDoc.Element("/character/misc/PrePullPotionofSpeed").Value Then
                Dim p As Proc
                p = proc.Find("Potion of Speed")
                If p.Equiped = 0 Then
                    p.Equip()
                    p.ProcChance = 0
                End If
                p.ApplyMe(T - 1000)
                p.CD = T + 60 * 100
            End If
        Catch ex As System.Exception

        End Try
    End Sub

    Function ProcessEvent(ByVal FE As FutureEvent) As Boolean
        'this routine returns whether an explicit action was taken
        proc.Bloodlust.TryMe(TimeStamp)
        Select Case FE.Ev
            Case "Boss"
                If FrostPresence = 1 Then
                    If boss.NextHit <= TimeStamp Then
                        boss.ApplyDamage(TimeStamp)
                    End If
                End If
            Case "GCD", "Rune"
                If isInGCD(TimeStamp) Then Return True
                If Me.Rotation.IntroDone = False Then
                    Rotation.DoIntro(TimeStamp)
                    If isInGCD(TimeStamp) Then Return True
                End If
                If BoneShieldUsageStyle = 2 Then 'after BS
                    If Runes.BloodRune1.Available(TimeStamp) = False And Runes.BloodRune1.death = True Then
                        If Runes.BloodRune2.Available(TimeStamp) = False And Runes.BloodRune2.death = True Then
                            If BoneShield.IsAvailable(TimeStamp) Then
                                If BoneShield.Use(TimeStamp) Then Return True
                            End If
                            If UnbreakableArmor.IsAvailable(TimeStamp) Then
                                If UnbreakableArmor.Use(TimeStamp) Then Return True
                            End If
                        End If
                    End If
                End If
                If Me.Rotation.IntroDone = True Then
                    If Rotate Then
                        Rotation.DoRoration(TimeStamp)
                    Else
                        Priority.DoNext(TimeStamp)
                    End If
                End If
                If isInGCD(TimeStamp) Then Return True
                If BoneShieldUsageStyle = 4 Then 'after Death rune OB/SS with cancel aura
                    If Runes.BloodRune1.Available(TimeStamp) = False And Runes.BloodRune1.death = False Then
                        If Runes.BloodRune2.Available(TimeStamp) = False And Runes.BloodRune2.death = False Then
                            If BoneShield.IsAvailable(TimeStamp) Then
                                If BoneShield.Use(TimeStamp) Then
                                    BloodTap.CancelAura()
                                    Return True
                                End If
                            End If
                            If UnbreakableArmor.IsAvailable(TimeStamp) Then
                                If UnbreakableArmor.Use(TimeStamp) Then
                                    BloodTap.CancelAura()
                                    Return True
                                End If
                            End If
                        End If
                    End If
                End If
                If PetFriendly Then
                    If Ghoul.ActiveUntil < TimeStamp And Ghoul.cd < TimeStamp And CanUseGCD(TimeStamp) Then
                        Ghoul.Summon(TimeStamp)
                        If isInGCD(TimeStamp) Then Return True
                    End If
                    If AotD.cd < TimeStamp And CanUseGCD(TimeStamp) Then
                        AotD.Summon(TimeStamp)
                        If isInGCD(TimeStamp) Then Return True
                    End If
                End If
                If Me.Rotation.IntroDone Then
                    If Horn.isAutoAvailable(TimeStamp) And CanUseGCD(TimeStamp) Then
                        Horn.use(TimeStamp)
                        If isInGCD(TimeStamp) Then Return True
                    End If
                End If

            Case "AotD"
                If AotD.ActiveUntil >= TimeStamp Then
                    If AotD.NextWhiteMainHit <= TimeStamp Then AotD.ApplyDamage(TimeStamp)
                    If AotD.NextClaw <= TimeStamp Then AotD.Claw(TimeStamp)
                End If

            Case "Disease"
                Dim t As Targets.Target
                For Each t In Targets.AllTargets
                    If t.BloodPlague.isActive(TimeStamp) Then
                        If t.BloodPlague.nextTick = TimeStamp Then
                            t.BloodPlague.ApplyDamage(TimeStamp)
                        End If
                    End If
                    If t.FrostFever.isActive(TimeStamp) Then
                        If t.FrostFever.nextTick = TimeStamp Then
                            t.FrostFever.ApplyDamage(TimeStamp)
                        End If
                    End If
                Next
            Case "DRW"
                If DRW.IsActive(TimeStamp) Then
                    If DRW.NextDRW <= TimeStamp Then DRW.ApplyDamage(TimeStamp)
                End If
            Case "Gargoyle"
                If Gargoyle.ActiveUntil >= TimeStamp Then
                    If Gargoyle.NextGargoyleStrike <= TimeStamp Then Gargoyle.ApplyDamage(TimeStamp)
                End If

            Case "Ghoul"
                If Ghoul.ActiveUntil >= TimeStamp Then
                    Ghoul.TryActions(TimeStamp)
                End If

            Case "Butchery"
                Butchery.apply(TimeStamp)

            Case "MainHand"
                MainHand.ApplyDamage(TimeStamp)

            Case "OffHand"
                OffHand.ApplyDamage(TimeStamp)

            Case "D&D"
                DeathandDecay.ApplyDamage(TimeStamp)
            Case "AMS"
                AMSTimer = TimeStamp + AMSCd
                RunicPower.add(AMSAmount)
                FutureEventManager.Add(AMSTimer + AMSCd, "AMS")
            Case "RuneFill"
                Me.Runes.FillRunes()
            Case "Scenario"
                Dim e As Scenarios.Element

                For Each e In Me.Scenario.Elements
                    If e.Start <= TimeStamp And e.Ending >= TimeStamp Then
                        If e.CanTakeDiseaseDamage = False Then
                            FutureEventManager.Reschedule("Disease", e.Ending)
                        End If
                        If e.CanTakePetDamage = False Then
                            FutureEventManager.Reschedule("AotD", e.Ending)
                            FutureEventManager.Reschedule("Ghoul", e.Ending)
                            FutureEventManager.Reschedule("Gargoyle", e.Ending)
                            FutureEventManager.Reschedule("DRW", e.Ending)
                        End If
                        If e.CanTakePlayerStrike = False Then
                            FutureEventManager.Reschedule("GCD", e.Ending)
                            FutureEventManager.Reschedule("Rune", e.Ending)
                            FutureEventManager.Reschedule("MainHand", e.Ending)
                            FutureEventManager.Reschedule("OffHand", e.Ending)
                        End If

                    End If
                Next
            Case "AddPop"
                Dim e As Scenarios.Element
                For Each e In Me.Scenario.Elements
                    If e.Start = TimeStamp And e.AddPop <> 0 Then
                        Me.KeepDiseaseOnOthersTarget = e.SpreadDisease
                        Dim i As Integer = 0
                        Do Until i = e.AddPop
                            Dim Add As Targets.Target
                            Add = New Targets.Target(Me)
                            i += 1
                        Loop
                    End If
                Next

            Case "AddDepop"
                Dim e As Scenarios.Element
                For Each e In Me.Scenario.Elements
                    If e.Ending = TimeStamp And e.AddPop <> 0 Then
                        Dim i As Integer = 0
                        Targets.KillEveryoneExceptMainTarget()
                    End If
                Next
            Case "SuperBuff"
                Dim e As Scenarios.Element
                Dim prc As Proc
                Try
                    prc = Me.proc.Find("SuperBuff")
                Catch
                    prc = Nothing
                End Try

                If prc Is Nothing Then
                    prc = New Proc(Me)
                End If
                For Each e In Me.Scenario.Elements
                    If e.Start = TimeStamp And e.DamageBonus <> 0 Then
                        With prc
                            .ProcChance = 1
                            .ProcLenght = e.length / 100
                            .ProcValue = e.DamageBonus
                            .ProcType = "percent"
                            ._Name = "SuperBuff"
                            .ProcOn = Procs.ProcOnType.OnMisc
                            .Equip()
                            .TryMe(TimeStamp)
                            .ProcChance = 0
                        End With
                    End If
                Next
            Case "FightStop"
                StoreMyDamage(TotalDamage)
                LastReset = NextReset
                SoftReset()
            Case Else
                Diagnostics.Debug.WriteLine("WTF is this event ?")
        End Select
        Return False
    End Function

    Sub Start()
        SimStart = Now
        LoadConfig()

        Rnd(-1) 'Tell VB to initialize using Randomize's parameter
        RandomNumberGenerator = New RandomNumberGenerator 'init here, so that we don't get the same rng numbers for short fights.

        MaxTime = SimTime * 60 * 60 * 100
        TotalDamageAlternative = 0
        TimeStampCounter = 1

        Dim intCount As Integer
        'Init
        Initialisation()

        Dim resetTime As Integer

        TimeStamp = 1
        If Character.TalentUnholy.MasterOfGhouls = 1 Then Ghoul.Summon(1)
        Rotation.LoadIntro()
        If Rotate Then Rotation.loadRotation()
        ' Pre Pull Activities

        PrePull(TimeStamp)
        SoftReset()

        Dim FE As FutureEvent
        Do Until False
            MainForm.ProgressBar1.Dispatcher.BeginInvoke(MainForm.ProgressBarHelper)
            FE = Me.FutureEventManager.GetFirst
            TimeStamp = FE.T
            If TimeStamp >= MaxTime Then Exit Do
            ProcessEvent(FE)

        Loop

        TotalDamageAlternative = TotalDamageAlternative + TotalDamage()
        TimeStampCounter = TimeStampCounter + TimeStamp
        TimeStamp = TimeStampCounter



        Dim obj As Supertype
        If ICCDamageBuff > 0 Then
            For Each obj In DamagingObject
                If obj.isGuardian = False Then
                    obj.total *= (1 + ICCDamageBuff / 100)
                    obj.TotalCrit *= (1 + ICCDamageBuff / 100)
                    obj.TotalHit *= (1 + ICCDamageBuff / 100)
                    obj.TotalGlance *= (1 + ICCDamageBuff / 100)
                End If
            Next
        End If


        DPS = 100 * TotalDamage() / TimeStamp
        Report()
        Diagnostics.Debug.WriteLine("DPS=" & DPS & " " & "TPS=" & TPS & " " & EPStat() & " hit=" & MainStat.Hit & " sphit=" & MainStat.SpellHit & " exp=" & MainStat.Expertise)
        'Diagnostics.Debug.WriteLine("Max events in queue " & me.FutureEventManager.Max )
        CombatLog.finish()
        On Error Resume Next
        If Me.FrostPresence = 1 Then
            SimConstructor.DPSs.Add(TPS, Me.EPStat)
        Else
            SimConstructor.DPSs.Add(DPS, Me.EPStat)
        End If
        'SimConstructor.simCollection.Remove(me)
    End Sub




    Function TotalDamage() As Long
        Dim i As Long
        Dim obj As Supertype

        For Each obj In Me.DamagingObject
            i += obj.total
        Next

        Return i
        Diagnostics.Debug.WriteLine(i)
    End Function

    Sub _UseGCD(ByVal T As Long, ByVal Length As Long)
        Dim tmp As Long
        GCDUsage.HitCount += 1


        If Length + T > NextReset Then
            tmp = (NextReset - T)
        Else
            tmp = Length
        End If
        If NextFreeGCD > T Then
            'should never happen currently is called when use BS etc is after a special
            tmp += T - NextFreeGCD
        End If
        GCDUsage.uptime += tmp
        T += Length
        If T > NextFreeGCD Then
            NextFreeGCD = T
            FutureEventManager.Add(NextFreeGCD, "GCD")
        End If
    End Sub

    Sub UseGCD(ByVal T As Long, ByVal Spell As Boolean)
        Dim tmp As Long
        If UnholyPresence Then
            tmp = 100
        ElseIf Spell Then
            tmp = Math.Max(150.0 / MainStat.SpellHaste, 100)
        Else
            tmp = 150
        End If
        tmp += latency / 10
        _UseGCD(T, tmp)
    End Sub

    Function isInGCD(ByVal T As Long) As Boolean
        If NextFreeGCD <= T Then
            isInGCD = False
        Else
            isInGCD = True
        End If
    End Function

    Function CanUseGCD(ByVal T As Long) As Boolean
        CanUseGCD = True
        If Character.Glyph.Disease Then
            Dim tGDC As Long
            'return false
            If UnholyPresence Then
                tGDC = 100 + latency / 10 + 50
            Else
                tGDC = 150 + latency / 10 + 50
            End If

            If Math.Min(Targets.MainTarget.BloodPlague.FadeAt, Targets.MainTarget.FrostFever.FadeAt) < (T + tGDC) Then
                'Diagnostics.Debug.WriteLine (RuneState & "time left on disease= " & (math.Min(BloodPlague.FadeAt,FrostFever.FadeAt) -T)/100 & "s" & " - " & T/100)
                Return False
            End If
        End If
    End Function







    Function DoMyWhiteHit() As Boolean
        'unused
        Return False
    End Function

    Sub SoftReset()
        FutureEventManager.Clear()
        Me.RotationStep = 0
        Me.Rotation.IntroStep = 0
        Runes.SoftReset()
        FutureEventManager.Add(TimeStamp, "Rune")



        RunicPower.Reset()
        Targets.KillEveryoneExceptMainTarget()
        Dim T As Targets.Target
        For Each T In Targets.AllTargets
            T.BloodPlague.nextTick = 0
            T.BloodPlague.FadeAt = 0
            T.FrostFever.nextTick = 0
            T.FrostFever.FadeAt = 0
        Next


        BloodTap.CD = 0
        HowlingBlast.CD = 0
        Ghoul.cd = 0
        AotD.cd = 0
        Hysteria.CD = TimeStamp
        DeathChill.Cd = 0
        'Desolation = New Desolation(me)
        MainHand.NextWhiteMainHit = TimeStamp
        FutureEventManager.Add(TimeStamp, "MainHand")

        If MainStat.DualW Then
            OffHand.NextWhiteOffHit = TimeStamp
            FutureEventManager.Add(TimeStamp, "OffHand")
        End If

        Gargoyle.NextGargoyleStrike = 0

        Ghoul.NextClaw = TimeStamp
        Ghoul.NextWhiteMainHit = TimeStamp
        FutureEventManager.Add(TimeStamp, "Ghoul")
        Frenzy.CD = 0
        DeathandDecay.CD = 0
        Gargoyle.cd = 0
        Horn.CD = 0
        'Bloodlust.Cd = 0
        proc.SoftReset()
        Trinkets.SoftReset()
        BoneShield.CD = 0
        BoneShield.PreBuff()
        Scenario.SoftReset()
        NextFreeGCD = 0
        AMSTimer = TimeStamp + AMSCd
        If AMSAmount <> 0 And AMSCd <> 0 Then
            FutureEventManager.Add(AMSTimer + AMSCd, "AMS")
        End If

        ERW.CD = 0
        RuneForge.SoftReset()
        If Character.TalentBlood.Butchery > 0 Then
            Butchery.nextTick = TimeStamp + 500
            FutureEventManager.Add(Butchery.nextTick, "Butchery")
        End If
        Me.Rotation.IntroDone = False
        PrePull(TimeStamp)

    End Sub


    Sub Initialisation()
        'RandomNumberGenerator.Init 'done in Start
        'DamagingObject.Clear
        PetFriendly = SimConstructor.PetFriendly

        '_EpStat = SimConstructor.EpStat

        'Buff = New Buff(Me)

        Dim targ As New Targets.Target(Me)
        Targets.CurrentTarget = targ
        Targets.MainTarget = targ

        'Keep this order for RuneX -> Runse -> Rotation/Prio
        Runes = New Runes.runes(Me)
        Rotation = New Rotation(Me)

        UnholyBlight = New UnholyBlight(Me)

        BloodTap = New BloodTap(Me)
        HowlingBlast = New HowlingBlast(Me)

        Ghoul = New Ghoul(Me)
        AotD = New AotD(Me)
        GhoulStat = New GhoulStat(Me)
        Hysteria = New Hysteria(Me)
        DeathChill = New DeathChill(Me)
        UnbreakableArmor = New UnbreakableArmor(Me)
        Butchery = New Butchery(Me)
        DRW = New DRW(Me)
        RuneStrike = New RuneStrike(Me)
        'LoadConfig
        'Desolation = New Desolation(me)
        RunicPower.Reset()
        NextFreeGCD = 0
        Threat = 0


        ScourgeStrike = New ScourgeStrike(Me)
        ScourgeStrikeMagical = New ScourgeStrikeMagical(Me)

        Obliterate = New Obliterate(Me)
        PlagueStrike = New PlagueStrike(Me)
        BloodStrike = New BloodStrike(Me)
        FrostStrike = New FrostStrike(Me)

        OHObliterate = New Obliterate(Me)
        OHObliterate.OffHand = True
        OHPlagueStrike = New PlagueStrike(Me)
        OHPlagueStrike.OffHand = True
        OHBloodStrike = New BloodStrike(Me)
        OHBloodStrike.OffHand = True
        OHFrostStrike = New FrostStrike(Me)
        OHFrostStrike.OffHand = True
        OHRuneStrike = New RuneStrike(Me)
        OHRuneStrike.OffHand = True

        BloodPresenceSwitch = New BloodPresence(Me)
        UnholyPresenceSwitch = New UnholyPresence(Me)
        FrostPresenceSwitch = New FrostPresence(Me)


        MainHand = New MainHand(Me)
        OffHand = New OffHand(Me)
        DeathCoil = New DeathCoil(Me)
        IcyTouch = New IcyTouch(Me)
        Necrosis = New Necrosis(Me)
        OHNecrosis = New Necrosis(Me)
        OHNecrosis.OffHand = True
        WanderingPlague = New WanderingPlague(Me)

        Frenzy = New Frenzy(Me)
        BloodCakedBlade = New BloodCakedBlade(Me)
        OHBloodCakedBlade = New BloodCakedBlade(Me)
        OHBloodCakedBlade.OffHand = True

        DeathStrike = New DeathStrike(Me)
        OHDeathStrike = New DeathStrike(Me)
        BloodBoil = New BloodBoil(Me)
        HeartStrike = New HeartStrike(Me)
        DeathandDecay = New DeathandDecay(Me)
        Gargoyle = New Gargoyle(Me)


        Horn = New Horn(Me)
        'Bloodlust= new Bloodlust(Me)
        Pestilence = New Pestilence(Me)

        proc.Init()
        BoneShield = New BoneShield(Me)
        ERW = New EmpowerRuneWeapon(Me)

        Scenario = New Scenarios.Scenario(Me)


        AMSCd = XmlConfig.Element("config").Element("txtAMScd").Value * 100
        AMSTimer = XmlConfig.Element("config").Element("txtAMScd").Value * 100
        AMSAmount = XmlConfig.Element("config").Element("txtAMSrp").Value * 100


        ShowDpsTimer = 1

        GCDUsage = New Spells.Spell(Me)
        GCDUsage._Name = "GCD Usage"





    End Sub

    Sub LoadConfig()

        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/config.xml", FileMode.Open, isoStore)

                XmlConfig = XDocument.Load(isoStream)
                isoStream.Close()
                Dim strCharacter As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/CharactersWithGear/" & XmlConfig.Element("config").Element("CharacterWithGear").Value, FileMode.Open, isoStore)
                XmlCharacter = XDocument.Load(strCharacter)
                strCharacter.Close()
                IntroPath = "\Intro\" & XmlConfig.Element("config").Element("intro").Value

                ScenarioPath = "\scenario\" & XmlConfig.Element("config").Element("scenario").Value

                If System.IO.File.Exists(IntroPath) = False Then
                    IntroPath = "\Intro\NoIntro.xml"
                End If
                KeepBloodSync = XmlConfig.Element("config").Element("BloodSync").Value

                If XmlConfig.Element("config").Element("mode").Value <> "priority" Then
                    Rotate = True
                    rotationPath = "\Rotation\" & XmlConfig.Element("config").Element("rotation").Value
                Else
                    Rotate = False
                    loadPriority("\Priority\" & XmlConfig.Element("config").Element("priority").Value)
                End If

                latency = XmlConfig.Element("config").Element("latency").Value
                ShowProc = XmlConfig.Element("config").Element("ShowProc").Value

                Sigils = New Sigils(Me)
                Sigils.WildBuck = False
                Sigils.FrozenConscience = False
                Sigils.DarkRider = False
                Sigils.ArthriticBinding = False
                Sigils.Awareness = False
                Sigils.Strife = False
                Sigils.HauntedDreams = False
                Sigils.VengefulHeart = False
                Sigils.Virulence = False
                Sigils.HangedMan = False
                Select Case XmlConfig.Element("config").Element("sigil").Value
                    Case "WildBuck"
                        Sigils.WildBuck = True
                    Case "FrozenConscience"
                        Sigils.FrozenConscience = True
                    Case "DarkRider"
                        Sigils.DarkRider = True
                    Case "ArthriticBinding"
                        Sigils.ArthriticBinding = True
                    Case "Awareness"
                        Sigils.Awareness = True
                    Case "Strife"
                        Sigils.Strife = True
                    Case "HauntedDreams"
                        Sigils.HauntedDreams = True
                    Case "VengefulHeart"
                        Sigils.VengefulHeart = True
                    Case "Virulence"
                        Sigils.Virulence = True
                    Case "HangedMan"
                        Sigils.HangedMan = True
                End Select

                BloodPresence = 0
                UnholyPresence = 0
                FrostPresence = 0
                Select Case XmlConfig.Element("config").Element("presence").Value
                    Case "Blood"
                        BloodPresence = 1
                    Case "Unholy"
                        UnholyPresence = 1
                    Case "Frost"
                        FrostPresence = 1
                End Select


                RuneForge = New RuneForge(Me)
                RunicPower = New RunicPower(Me)
                proc = New Procs(Me)
                Character = New Character(Me)
                MainStat = New MainStat(Me)

                Me.CombatLog.enable = XmlConfig.Element("config").Element("log").Value
                Me.CombatLog.LogDetails = XmlConfig.Element("config").Element("logdetail").Value
                MergeReport = XmlConfig.Element("config").Element("chkMergeReport").Value
                ReportName = XmlConfig.Element("config").Element("txtReportName").Value
                WaitForFallenCrusader = XmlConfig.Element("config").Element("WaitFC").Value


                Select Case XmlConfig.Element("config").Element("BShOption").Value
                    Case "Instead of Blood Strike"
                        Me.BoneShieldUsageStyle = 1
                    Case "After BS/BB"
                        Me.BoneShieldUsageStyle = 2
                    Case "Instead of Blood Boil"
                        Me.BoneShieldUsageStyle = 3
                    Case "After Death rune OB/SS with cancel aura"
                        Me.BoneShieldUsageStyle = 4
                    Case Else
                        Me.BoneShieldUsageStyle = 2
                End Select
                Try
                    ICCDamageBuff = XmlConfig.Element("config").Element("ICCBuff").Value
                Catch

                    ICCDamageBuff = 0
                End Try

                Try
                    BoneShieldTTL = XmlConfig.Element("config").Element("BSTTL").Value
                Catch
                    BoneShieldTTL = 300
                End Try
            End Using
        End Using






        Exit Sub
errH:
        msgBox("Error reading config file")
    End Sub

    Sub loadPriority(ByVal file As String)
        Priority = New priority(Me)
        Priority.prio.Clear()
        Dim XmlDoc As New XDocument
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/" & file, FileMode.Open, isoStore)
                XmlDoc = XDocument.Load(isoStream)
                If KeepBloodSync Then
                    Priority.prio.Add("BloodSync", "BloodSync")
                End If

                For Each Nod In XmlDoc.Element("Priority").Elements
                    If Nod.Name = "SaveRPForRuneStrike" Then
                        SaveRPForRS = True
                    Else
                        Priority.prio.Add(Nod.Name.ToString, Nod.Name.ToString)
                    End If
                Next
            End Using
        End Using
    End Sub

    Sub Report()
        'on error resume next


        



                ' Sort report

                Dim myArray As New Collections.Generic.List(Of Long)

                Dim obj As Supertype

                'MergeDisease

                For Each obj In DamagingObject
                    If TypeOf obj Is Diseases.BloodPlague Then
                        If obj.total <> 0 Then
                            obj.Merge()
                        End If
                    End If
                Next

                For Each obj In DamagingObject
                    If TypeOf obj Is Diseases.FrostFever Then
                        If obj.total <> 0 Then
                            obj.Merge()
                        End If
                    End If
                Next




                If MergeReport Then
                    For Each obj In DamagingObject
                        If obj.total <> 0 Then
                            obj.Merge()
                        End If
                    Next
                End If

                For Each obj In DamagingObject
                    If obj.total <> 0 Then
                        myArray.Add(obj.total)
                    End If
                Next
                myArray.Sort()

                Dim ThreatBeforePresence As Long = Threat
                For Each obj In Me.DamagingObject
                    Threat += obj.total * obj.ThreadMultiplicator
                Next
                If FrostPresence = 1 Then
                    Threat = Threat * 2.0735
                Else
                    Threat = (Threat * 0.8) * (1 - Character.TalentBlood.Subversion * 8.333 / 100)
                End If

                Threat = Threat + ThreatBeforePresence
                TPS = 100 * Threat / TimeStamp

                If EPStat() <> "" Then Exit Sub

                Dim myReport As New Report
                '_MainFrm.ReportStack.Children.Add(myReport)
                Dim i As Integer
                Dim tot As Long
                Dim STmp As String
                For i = 0 To myArray.Count - 1
                    tot = (myArray.Item(myArray.Count - 1 - i))

                    For Each obj In DamagingObject
                        If obj.total = tot Then
                            myReport.AddLine(obj.Report)
                        End If

                    Next
                Next

                If ShowProc Then
                    If True Then
                        myReport.AddLine(GCDUsage.Report)
                        myReport.AddLine(Runes.BloodRune1.Report)
                        myReport.AddLine(Runes.BloodRune2.Report)
                        myReport.AddLine(Runes.FrostRune1.Report)

                        myReport.AddLine(Runes.FrostRune2.Report)
                        myReport.AddLine(Runes.UnholyRune1.Report)
                        myReport.AddLine(Runes.UnholyRune2.Report)
                    End If

                    If Horn.HitCount <> 0 Then
                        myReport.AddLine(Horn.Report)

                    End If
                    If Pestilence.HitCount <> 0 Then
                        myReport.AddLine(Pestilence.Report)

                    End If
                    If BoneShield.HitCount <> 0 Then
                        myReport.AddLine(BoneShield.Report)

                    End If
                    If BloodTap.HitCount <> 0 Then
                        myReport.AddLine(BloodTap.Report)

                    End If

                    If Frenzy.HitCount <> 0 Then
                        myReport.AddLine(Frenzy.Report)
                    End If

                    If UnbreakableArmor.HitCount <> 0 Then
                        myReport.AddLine(UnbreakableArmor.Report)

                    End If
                    'On Error Resume Next
                    Dim pr As Proc
                    For Each pr In proc.EquipedProc
                        If pr.total = 0 Then
                            myReport.AddLine(pr.Report)
                        End If
                    Next
                End If

                myReport.Save("")
                Exit Sub
                'Dim minDPS As Integer
                'Dim maxDPS As Integer
                'Dim MinMAx As Integer
                'Dim range As Double

                'If MultipleDamage.Count > 1 Then
                '    MultipleDamage.Sort()
                '    minDPS = MultipleDamage.Item(1) / (FightLength)
                '    maxDPS = MultipleDamage.Item(MultipleDamage.Count - 1) / (FightLength)
                '    MinMAx = Math.Max(DPS - minDPS, maxDPS - DPS)
                '    range = (maxDPS - minDPS) / (2 * DPS)
                '    STmp = STmp & "<tr><td COLSPAN=8>DPS<FONT COLOR='white'>|</FONT>" & vbTab & "<b>" & DPS & "(+/- " & MinMAx & ")</b></td></tr>"
                'Else
                '    STmp = STmp & "<tr><td COLSPAN=8>DPS<FONT COLOR='white'>|</FONT>" & vbTab & "<b>" & DPS & "</b></td></tr>"
                'End If
                'STmp = STmp & "<tr><td COLSPAN=8>Total Damage<FONT COLOR='white'>|</FONT>" & vbTab & Math.Round(TotalDamage() / 1000000, 2) & "m" & vbTab & "<FONT COLOR='white'>|</FONT> in " & MaxTime / 100 / 60 / 60 & "h</td></tr>"
                'STmp = STmp & "<tr><td COLSPAN=8>" & RunicPower.Report() & "</td></tr>"


                'STmp = STmp & "<tr><td COLSPAN=8>Threat Per Second<FONT COLOR='white'>|</FONT>" & vbTab & "<b>" & TPS & "</b></td></tr>"
                'STmp = STmp & "<tr><td COLSPAN=8>Generated in <FONT COLOR='white'>|</FONT>" & DateDiff(DateInterval.Second, SimStart, Now()) & "s</td></tr>"

                'STmp = STmp & "<tr><td COLSPAN=8>Template:<FONT COLOR='white'>|</FONT> " & Split(Character.GetTemplateFileName, ".")(0) & "</td></tr>"
                'If Rotate Then
                '    STmp = STmp & "<tr><td COLSPAN=8>Rotation: <FONT COLOR='white'>|</FONT>" & Split(Character.GetRotationFileName, ".")(0) & "</td></tr>"
                'Else
                '    STmp = STmp & "<tr><td COLSPAN=8>Priority: <FONT COLOR='white'>|</FONT>" & Split(Character.GetPriorityFileName, ".")(0) & "</td></tr>"
                'End If
                'STmp = STmp & "<tr><td COLSPAN=8>Presence: <FONT COLOR='white'>|</FONT>" & Character.GetPresence & vbCrLf & "</td></tr>"
                'STmp = STmp & "<tr><td COLSPAN=8>Sigil: <FONT COLOR='white'>|</FONT>" & Character.GetSigil & vbCrLf & "</td></tr>"

                'If MainStat.DualW Then
                '    STmp = STmp & "<tr><td COLSPAN=8>RuneEnchant: <FONT COLOR='white'>|</FONT> " & Character.GetMHEnchant & " / <FONT COLOR='white'>|</FONT>" & Character.GetOHEnchant & "</td></tr>"
                'Else
                '    STmp = STmp & "<tr><td COLSPAN=8>RuneEnchant: <FONT COLOR='white'>|</FONT>" & Character.GetMHEnchant & "</td></tr>"
                'End If

                'STmp = STmp & "<tr><td COLSPAN=8>Pet Calculation: <FONT COLOR='white'>|</FONT>" & Character.GetPetCalculation & "</td></tr>"

                'STmp = STmp & "</table><FONT COLOR='white'>[/TABLE]</FONT><hr width='80%' align='center' noshade ></hr>"
                '        Tw.Write(STmp)
                '        Tw.Flush()

                '        Tw.Close()




        


    End Sub


    Sub tryOnDamageProc()
        Dim obj As Proc
        For Each obj In Me.proc.OnDamageProcs
            obj.TryMe(TimeStamp)
        Next
    End Sub

    Sub tryOnMHWhitehitProc()
        Dim obj As Proc
        For Each obj In Me.proc.OnMHWhitehitProcs
            obj.TryMe(TimeStamp)
        Next
        TryOnMHHitProc()
    End Sub

    Sub StoreMyDamage(ByVal damage As Long)
        Dim tmp As Long
        Dim i As Integer
        On Error Resume Next
        tmp = damage
        For i = 0 To MultipleDamage.Count
            tmp = tmp - Integer.Parse(MultipleDamage.Item(i).ToString)
        Next
        MultipleDamage.Add(tmp)
    End Sub


    Sub TryOnMHHitProc()
        Dim obj As Proc
        For Each obj In Me.proc.OnMHhitProcs
            obj.TryMe(TimeStamp)
        Next

        For Each obj In Me.proc.OnHitProcs
            obj.TryMe(TimeStamp)
        Next
        tryOnDamageProc()
    End Sub

    Sub TryOnOHHitProc()
        Dim obj As Proc
        For Each obj In Me.proc.OnOHhitProcs
            obj.TryMe(TimeStamp)
        Next
        For Each obj In Me.proc.OnHitProcs
            obj.TryMe(TimeStamp)
        Next
        tryOnDamageProc()
    End Sub

    Sub TryOnFU()
        Dim obj As Proc
        For Each obj In Me.proc.OnFUProcs
            obj.TryMe(TimeStamp)
        Next
    End Sub

    Sub tryOnCrit()
        Dim obj As Proc
        For Each obj In Me.proc.OnCritProcs
            obj.TryMe(TimeStamp)
        Next
    End Sub

    Sub tryOnDoT()
        Dim obj As Proc
        For Each obj In Me.proc.OnDoTProcs
            obj.TryMe(TimeStamp)
        Next
        tryOnDamageProc()
    End Sub

    Sub TryOnSpellHit()
        Dim obj As Proc
        For Each obj In Me.proc.OnDamageProcs
            obj.TryMe(TimeStamp)
        Next
        tryOnDamageProc()
    End Sub

    Sub TryOnBloodStrike()
        Dim obj As Proc
        For Each obj In Me.proc.OnBloodStrikeProcs
            obj.TryMe(TimeStamp)
        Next
    End Sub





End Class