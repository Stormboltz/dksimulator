Imports Microsoft.VisualBasic
Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO
Imports DKSIMVB.Simulator.WowObjects
Imports DKSIMVB.Simulator.WowObjects.PetsAndMinions
Imports DKSIMVB.Simulator.WowObjects.Strikes
Imports DKSIMVB.Simulator.WowObjects.Spells
Imports DKSIMVB.Simulator.WowObjects.Procs



Namespace Simulator

    Public Class Sim
        Friend CalculateUPtime As Boolean = True
        Dim DPSLine As New StatScallingLine("Real Time DPS")
        Dim DPSLineAverage As New StatScallingLine("AverageDPS")
        Friend TimeWastedAnaliser As New TimeWastedAnaliser
        Friend SpellBuffManager As New SpellBuffManager
        Friend SpellEffectManager As New SpellEffectManager
        Friend EffectManager As New EffectManager
        Public Event Sim_Closing(ByVal sender As Object, ByVal e As EventArgs)
        Friend isoStore As IsolatedStorage.IsolatedStorageFile

        'Friend Cataclysm As Boolean = True
        Friend UselessCheck As Long
        Friend UselessCheckColl As New Collection

        Friend FightLength As Integer
        Friend ScenarioPath As String


        'Friend Patch as Boolean
        Friend TotalDamageAlternative As Long
        Friend NextFreeGCD As Long
        Friend GCDUsage As Spell

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


        Friend Character As Character.MainStat
        Friend Sigils As Sigils
        Friend boss As Boss

        'Strike Creation
        Friend BloodCakedBlade As Strikes.BloodCakedBlade
        Friend OHBloodCakedBlade As Strikes.BloodCakedBlade

        Friend BloodStrike As Strikes.BloodStrike
        Friend OHBloodStrike As Strikes.BloodStrike

        Friend Obliterate As Strikes.Obliterate
        Friend OHObliterate As Strikes.Obliterate

        Friend PlagueStrike As Strikes.PlagueStrike
        Friend OHPlagueStrike As Strikes.PlagueStrike

        Friend FrostStrike As Strikes.FrostStrike
        Friend OHFrostStrike As Strikes.FrostStrike

        Friend RuneStrike As Strikes.RuneStrike
        Friend OHRuneStrike As Strikes.RuneStrike

        Friend DeathStrike As Strikes.DeathStrike
        Friend OHDeathStrike As Strikes.DeathStrike

        Friend HeartStrike As Strikes.HeartStrike



        Friend ScourgeStrike As Strikes.ScourgeStrike
        Friend FesteringStrike As Strikes.FesteringStrike
        Friend ScourgeStrikeMagical As Strikes.ScourgeStrike.ScourgeStrikeMagical
        Friend MainHand As Strikes.MainHand
        Friend OffHand As Strikes.OffHand

        Friend GhoulStat As GhoulStat
        Friend Ghoul As Ghoul
        Friend AotD As AotD

        'Spell Creation
        Friend BloodBoil As BloodBoil
        Friend BloodTap As BloodTap
        Friend Butchery As Butchery
        Friend DeathandDecay As DeathandDecay



        Friend Horn As Horn
        Friend HowlingBlast As HowlingBlast

        Friend IcyTouch As IcyTouch
        Friend Necrosis As Necrosis
        Friend OHNecrosis As Necrosis
        Friend DeathCoil As DeathCoil
        Friend Pestilence As Pestilence
        Friend PillarOfFrost As PillarOfFrost
        Friend UnholyBlight As UnholyBlight
        Friend DRW As DRW

        Friend Gargoyle As Gargoyle
        Friend BoneShield As BoneShield
        'Friend Frenzy As GhoulFrenzy
        Friend ERW As EmpowerRuneWeapon

        Friend DarkTransformation As DarkTransformation


        Friend proc As ProcsManager
        Friend Targets As Targets.TargetsManager




        Friend Priority As priority
        Friend Rotation As Rotation
        Friend RuneForge As RuneForge

        Friend Trinkets As Trinkets

        Friend DamagingObject As New Collections.Generic.List(Of WowObject)
        Friend TrinketsCollection As New Collections.Generic.List(Of Proc)
        Friend ProcCollection As New Collections.Generic.List(Of Proc)

        Friend MergeReport As Boolean
        Friend WaitForFallenCrusader As Boolean





        Friend CombatLog As New CombatLog(Me)
        Friend BoneShieldUsageStyle As Integer
        Friend BloodPresence As Double
        Friend UnholyPresence As Double
        Friend FrostPresence As Double

        Friend BloodPresenceSwitch As BloodPresence
        Friend UnholyPresenceSwitch As UnholyPresence
        Friend FrostPresenceSwitch As FrostPresence


        Friend SaveRPForRS As Boolean


        Friend ReportName As String

        Friend XmlCharacter As XDocument
        Friend XmlConfig As XDocument


        Friend Scenario As Scenarios.Scenario

        Public Enum Stat
            None = 0
            AP = 1
            Strength = 2
            Haste = 3
            Crit = 4
            Mastery = 5
            Expertise = 6
            Hit = 7
            Multiplicative = 8
            Armor = 9
            ArP = 10
            Agility = 11
            Intel = 12
            SpecialArmor = 13
            GhoulDamage = 14
            Stamina = 15
        End Enum



        Sub Init()
        End Sub





        Function EPStat() As String
            If _EPStat <> "" Then
                Return _EPStat
            Else
                Return ""
            End If

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
                If Me.Character.XmlCharacter.Element("character").Element("misc").Element("PrePullIndestructiblePotion").Value Then
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
                If Me.Character.XmlCharacter.Element("character").Element("misc").Element("PrePullPotionofSpeed").Value Then
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
        Dim PreviousDamage As Long
        Dim PreviousDamageTimeStamp As Long
        Function ProcessEvent(ByVal FE As FutureEvent) As Boolean
            'this routine returns whether an explicit action was taken
            proc.Bloodlust.TryMe(TimeStamp)
            Select Case FE.Ev
                Case "SaveCurrentDPS"
                    Dim CurrentDamage As Long
                    CurrentDamage = TotalDamage()
                    Dim CurrentDPS As Long
                    CurrentDPS = (CurrentDamage - PreviousDamage) / ((TimeStamp - PreviousDamageTimeStamp) / 100)
                    DPSLine.Add(CInt(TimeStamp / 100), CurrentDPS)
                    DPSLineAverage.Add(CInt(TimeStamp / 100), CurrentDamage / (TimeStamp / 100))
                    PreviousDamageTimeStamp = TimeStamp
                    PreviousDamage = CurrentDamage
                    FutureEventManager.Add(TimeStamp + 500, "SaveCurrentDPS")
                Case "BuffFade"
                    Dim SB As Effect = CType(FE.WowObj, Effect)
                    SB.FAde()
                    Return True
                Case "Boss"
                    If BloodPresence = 1 Then
                        If boss.NextHit <= TimeStamp Then
                            boss.ApplyDamage(TimeStamp)
                        End If
                    End If
                Case "GCD", "Rune"
                    Me.Runes.FillRunes()
                    If isInGCD(TimeStamp) Then Return True
                    If Me.Rotation.IntroDone = False Then
                        Rotation.DoIntro(TimeStamp)
                        If isInGCD(TimeStamp) Then Return True
                    End If
                    If BoneShieldUsageStyle = 2 Then 'after BS
                        If Runes.BloodRune1.Available(TimeStamp) = False And Runes.BloodRune1.death = True Then
                            If Runes.BloodRune2.Available(TimeStamp) = False And Runes.BloodRune2.death = True Then
                                If BoneShield.IsAvailable() Then
                                    BoneShield.Use()
                                    Return True
                                End If
                                If PillarOfFrost.IsAvailable() Then
                                    PillarOfFrost.Use()
                                    Return True
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
                                If BoneShield.IsAvailable() Then
                                    BoneShield.Use()
                                    BloodTap.CancelAura()
                                    Return True
                                End If
                            End If
                            If PillarOfFrost.IsAvailable() Then
                                PillarOfFrost.Use()
                                BloodTap.CancelAura()
                                Return True
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
                            Horn.use()
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
                                .ProcType = Sim.Stat.Multiplicative
                                ._Name = "SuperBuff"
                                .ProcOn = ProcsManager.ProcOnType.OnMisc
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
                    _MainFrm.TryToUpdateProgressBar()
                Case Else
                    Diagnostics.Debug.WriteLine("WTF is this event ?")
            End Select
            Return False
        End Function

        Sub Start()
            SimStart = Now
            Try
                isoStore = IsolatedStorageFile.GetUserStoreForApplication()
            Catch ex As Exception
                Diagnostics.Debug.WriteLine(ex.StackTrace)
                RaiseEvent Sim_Closing(Me, EventArgs.Empty)
                Return
            End Try


            LoadConfig()

            Rnd(-1) 'Tell VB to initialize using Randomize's parameter
            RandomNumberGenerator = New RandomNumberGenerator 'init here, so that we don't get the same rng numbers for short fights.

            MaxTime = SimTime * 60 * 60 * 100
            TotalDamageAlternative = 0
            TimeStampCounter = 1


            'Init

            Initialisation()



            TimeStamp = 1

            If Character.Talents.Talent("MasterOfGhouls").Value = 1 Then Ghoul.Summon(1)
            Rotation.LoadIntro()
            If Rotate Then Rotation.loadRotation()
            ' Pre Pull Activities

            PrePull(TimeStamp)

            SoftReset()
            FutureEventManager.Add(TimeStamp + 500, "SaveCurrentDPS")
            Dim FE As FutureEvent
            Do Until False

                'System.Threading.Thread.Sleep(1)
                FE = Me.FutureEventManager.GetFirst
                TimeStamp = FE.T
                If TimeStamp >= MaxTime Then Exit Do
                ProcessEvent(FE)
            Loop

            TotalDamageAlternative = TotalDamageAlternative + TotalDamage()
            TimeStampCounter = TimeStampCounter + TimeStamp
            TimeStamp = TimeStampCounter



            Dim obj As WowObject
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


            '

            'If MultipleDamage.Count > 1 Then
            '    MultipleDamage.Sort()
            '    DPS = GetMedianValue() / FightLength
            'Else
            DPS = 100 * TotalDamage() / TimeStamp
            'End If
            Report()
            Diagnostics.Debug.WriteLine("DPS=" & DPS & " " & "TPS=" & TPS & " " & EPStat() & " hit=" & Character.Hit.Value & " sphit=" & Character.SpellHit.Value & " exp=" & Character.Expertise.Value)
            'Diagnostics.Debug.WriteLine("Max events in queue " & me.FutureEventManager.Max )
            CombatLog.finish()
            'On Error Resume Next
            If Me.EPStat <> "" Then
                If Me.BloodPresence = 1 Then
                    SimConstructor.DPSs.Add(TPS, Me.EPStat)
                Else
                    SimConstructor.DPSs.Add(DPS, Me.EPStat)
                End If
            End If
            'SimConstructor.simCollection.Remove(me)
            RaiseEvent Sim_Closing(Me, EventArgs.Empty)
        End Sub




        Function TotalDamage() As Long
            Dim i As Long
            Dim obj As WowObject

            For Each obj In Me.DamagingObject
                i += obj.total
            Next

            Return i
            Diagnostics.Debug.WriteLine(i)
        End Function

        Sub _UseGCD(ByVal Length As Long)
            Dim tmp As Long

            GCDUsage.HitCount += 1
            If TimeStamp + Length > NextReset Then
                tmp = (NextReset - TimeStamp)
            Else
                tmp = Length
            End If
            If NextFreeGCD > TimeStamp Then
                'should never happen. currently is called when use BS etc is after a special
                tmp += NextFreeGCD - TimeStamp
            Else
                GCDUsage.uptime += tmp
            End If
            If (TimeStamp + Length) > NextFreeGCD Then
                NextFreeGCD = TimeStamp + Length
                FutureEventManager.Add(NextFreeGCD, "GCD")
            End If

            
        End Sub

        Sub UseGCD(ByVal Spell As Boolean)
            Dim tmp As Long
            If UnholyPresence > 0 Then
                tmp = 100
            Else
                If Spell Then
                    tmp = Math.Max(150.0 / Character.SpellHaste.Value, 100)
                Else
                    tmp = 150
                End If
            End If
            tmp += latency / 10
            _UseGCD(tmp)
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
            If Character.Glyph.GoD Then
                Dim tGDC As Long
                'return false
                tGDC = 150 + latency / 10 + 50
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
            SpellEffectManager.SoftReset()


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
            
            MainHand.NextWhiteMainHit = TimeStamp
            FutureEventManager.Add(TimeStamp, "MainHand")

            If Character.DualW Then
                OffHand.NextWhiteOffHit = TimeStamp
                FutureEventManager.Add(TimeStamp, "OffHand")
            End If

            Gargoyle.NextGargoyleStrike = 0

            Ghoul.NextClaw = TimeStamp
            Ghoul.NextWhiteMainHit = TimeStamp
            FutureEventManager.Add(TimeStamp, "Ghoul")
            'Frenzy.CD = 0
            DeathandDecay.CD = 0
            Gargoyle.cd = 0
            Horn.CD = 0
            'Bloodlust.Cd = 0
            proc.SoftReset()
            Trinkets.SoftReset()
            BoneShield.CD = 0
            BoneShield.PreBuff()
            Scenario.SoftReset()
            boss.SoftReset()
            NextFreeGCD = 0
            AMSTimer = TimeStamp + AMSCd
            If AMSAmount <> 0 And AMSCd <> 0 Then
                FutureEventManager.Add(AMSTimer + AMSCd, "AMS")
            End If

            ERW.CD = 0
            RuneForge.SoftReset()
            If Character.Talents.Talent("Butchery").Value > 0 Then
                Butchery.nextTick = TimeStamp + 500
                FutureEventManager.Add(Butchery.nextTick, "Butchery")
            End If
            Me.Rotation.IntroDone = False
            PrePull(TimeStamp)

        End Sub


        Sub Initialisation()
            'DamagingObject.Clear
            PetFriendly = XmlConfig.Element("config").Element("pet").Value

            'Keep this order for RuneX -> Runse -> Rotation/Prio
            Runes = New Runes.runes(Me)
            Rotation = New Rotation(Me)

            UnholyBlight = New UnholyBlight(Me)

            BloodTap = New BloodTap(Me)
            HowlingBlast = New HowlingBlast(Me)


            Ghoul = New Ghoul(Me)
            AotD = New AotD(Me)
            GhoulStat = New GhoulStat(Me)
            PillarOfFrost = New PillarOfFrost(Me)
            Butchery = New Butchery(Me)
            DRW = New DRW(Me)

            
            RunicPower.Reset()
            NextFreeGCD = 0
            Threat = 0


            ScourgeStrike = New Strikes.ScourgeStrike(Me)
            ScourgeStrikeMagical = New Strikes.ScourgeStrike.ScourgeStrikeMagical(Me)



            DarkTransformation = New DarkTransformation(Me)



            FesteringStrike = New Strikes.FesteringStrike(Me)
            Dim OHFesteringStrike = New Strikes.FesteringStrike(Me)
            OHFesteringStrike.OffHand = True
            FesteringStrike.OffHandStrike = OHFesteringStrike

            Obliterate = New Strikes.Obliterate(Me)
            OHObliterate = New Strikes.Obliterate(Me)
            OHObliterate.OffHand = True
            Obliterate.OffHandStrike = OHObliterate

            PlagueStrike = New Strikes.PlagueStrike(Me)
            OHPlagueStrike = New Strikes.PlagueStrike(Me)
            OHPlagueStrike.OffHand = True
            PlagueStrike.OffHandStrike = OHPlagueStrike

            BloodStrike = New Strikes.BloodStrike(Me)
            OHBloodStrike = New Strikes.BloodStrike(Me)
            OHBloodStrike.OffHand = True
            BloodStrike.OffHandStrike = OHBloodStrike

            FrostStrike = New Strikes.FrostStrike(Me)
            OHFrostStrike = New Strikes.FrostStrike(Me)
            OHFrostStrike.OffHand = True
            FrostStrike.OffHandStrike = OHFrostStrike

            RuneStrike = New Strikes.RuneStrike(Me)
            OHRuneStrike = New Strikes.RuneStrike(Me)
            OHRuneStrike.OffHand = True
            RuneStrike.OffHandStrike = OHRuneStrike


            BloodPresenceSwitch = New BloodPresence(Me)
            UnholyPresenceSwitch = New UnholyPresence(Me)
            FrostPresenceSwitch = New FrostPresence(Me)


            MainHand = New Strikes.MainHand(Me)
            OffHand = New Strikes.OffHand(Me)

            DeathCoil = New DeathCoil(Me)

            IcyTouch = New IcyTouch(Me)

            Necrosis = New Necrosis(Me)
            OHNecrosis = New Necrosis(Me)
            OHNecrosis.OffHand = True

            'Frenzy = New GhoulFrenzy(Me)

            BloodCakedBlade = New Strikes.BloodCakedBlade(Me)
            OHBloodCakedBlade = New Strikes.BloodCakedBlade(Me)
            OHBloodCakedBlade.OffHand = True


            DeathStrike = New Strikes.DeathStrike(Me)
            OHDeathStrike = New Strikes.DeathStrike(Me)
            OHDeathStrike.OffHand = True
            DeathStrike.OffHandStrike = OHDeathStrike


            BloodBoil = New BloodBoil(Me)
            HeartStrike = New Strikes.HeartStrike(Me)
            DeathandDecay = New DeathandDecay(Me)
            Gargoyle = New Gargoyle(Me)


            Horn = New Horn(Me)
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
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/config.xml", FileMode.Open, FileAccess.Read, isoStore)
                If IsNothing(XmlConfig) Then
                    XmlConfig = XDocument.Load(isoStream)
                    isoStream.Close()
                End If

                Dim strCharacter As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/CharactersWithGear/" & XmlConfig.Element("config").Element("CharacterWithGear").Value, FileMode.Open, FileAccess.Read, isoStore)
                XmlCharacter = XDocument.Load(strCharacter)
                strCharacter.Close()
                IntroPath = "\Intro\" & XmlConfig.Element("config").Element("intro").Value

                ScenarioPath = "\scenario\" & XmlConfig.Element("config").Element("scenario").Value

                If System.IO.File.Exists(IntroPath) = False Then
                    IntroPath = "\Intro\NoIntro.xml"
                End If


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
                Dim sigilId As Integer
                Try
                    sigilId = XmlCharacter.Element("character").Element("Sigil").Element("id").Value
                Catch ex As Exception
                    sigilId = 0
                End Try

                Select Case sigilId
                    Case 3
                        Sigils.WildBuck = True
                    Case 4
                        Sigils.FrozenConscience = True
                    Case 1
                        Sigils.DarkRider = True
                    Case 2
                        Sigils.ArthriticBinding = True
                    Case 40207
                        Sigils.Awareness = True
                    Case 51417, 42619, 42620, 42621, 42622
                        Sigils.Strife = True
                    Case 40715
                        Sigils.HauntedDreams = True
                    Case 45254
                        Sigils.VengefulHeart = True
                    Case 47673
                        Sigils.Virulence = True
                    Case 50459
                        Sigils.HangedMan = True
                End Select



                proc = New ProcsManager(Me)

                RuneForge = New RuneForge(Me)
                RunicPower = New RunicPower(Me)

                Character = New Character.MainStat(Me)

                Dim targ As New Targets.Target(Me)
                Targets.CurrentTarget = targ
                Targets.MainTarget = targ

                InitPresence()
                Character.InitStats()
                Character.InitTrinkets()
                Character.InitSets()

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
                        Me.BoneShieldUsageStyle = 1
                End Select
                Try
                    Dim s As String = XmlConfig.<config>.<ICCBuff>.Value
                    Integer.TryParse(s, ICCDamageBuff)
                Catch

                    ICCDamageBuff = 0
                End Try

                Try
                    BoneShieldTTL = XmlConfig.Element("config").Element("BSTTL").Value
                Catch
                    BoneShieldTTL = 300
                End Try
            End Using







            Exit Sub
errH:
            msgBox("Error reading config file")
        End Sub
        Sub InitPresence()
            BloodPresence = 0
            UnholyPresence = 0
            FrostPresence = 0
            Select Case XmlConfig.Element("config").Element("presence").Value
                Case "Blood"
                    BloodPresence = 1
                Case "Unholy"
                    UnholyPresence = 1
                Case "Frost"
                    If FrostPresenceSwitch Is Nothing Then
                        FrostPresenceSwitch = New FrostPresence(Me)
                    End If
                    Me.FrostPresenceSwitch.SetForFree()
                    'FrostPresence = 1
            End Select
        End Sub
        Sub loadPriority(ByVal file As String)
            Priority = New priority(Me)
            Priority.prio.Clear()
            Dim XmlDoc As New XDocument

            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/" & file, FileMode.Open, FileAccess.Read, isoStore)
                XmlDoc = XDocument.Load(isoStream)
                For Each Nod In XmlDoc.Element("Priority").Elements
                    If Nod.Name = "SaveRPForRuneStrike" Then
                        SaveRPForRS = True
                    Else
                        Priority.prio.Add(Nod.Name.ToString, Nod.Name.ToString)
                    End If
                Next
            End Using

        End Sub

        Sub Report()
            TimeWastedAnaliser.Report()
            Dim myArray As New Collections.Generic.List(Of Long)
            Dim obj As WowObject

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
            If BloodPresence = 1 Then
                Threat = Threat * 2.0735
            Else
                Threat = (Threat * 0.8)
            End If
            Threat = Threat + ThreatBeforePresence
            TPS = 100 * Threat / TimeStamp
            If EPStat() <> "" Then Exit Sub
            Dim myReport As New Report
            '_MainFrm.ReportStack.Children.Add(myReport)

            Dim DamagingObjectList As List(Of WowObject)
            DamagingObjectList = (From oj In DamagingObject
                 Where oj.total <> 0
                 Order By oj.total Descending
                 Select oj).ToList
            For Each obj In DamagingObjectList
                myReport.AddLine(obj.Report)
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
                If Horn.HitCount <> 0 Then myReport.AddLine(Horn.Report)
                If Pestilence.HitCount <> 0 Then myReport.AddLine(Pestilence.Report)
                If BoneShield.HitCount <> 0 Then myReport.AddLine(BoneShield.Report)
                If BloodTap.HitCount <> 0 Then myReport.AddLine(BloodTap.Report)
                'If Frenzy.HitCount <> 0 Then myReport.AddLine(Frenzy.Report)
                If PillarOfFrost.HitCount <> 0 Then myReport.AddLine(PillarOfFrost.Report)
                'On Error Resume Next
                Dim pr As Proc
                For Each pr In (From p In proc.EquipedProc
                                 Where p.total = 0 And p.Effects.Count = 0
                                 Order By p.uptime Descending)
                    If pr.total = 0 Then
                        myReport.AddLine(pr.Report)
                    End If
                Next
                For Each eff In EffectManager.Effects
                    myReport.AddLine(eff.Report)
                Next

            End If

            Dim minDPS As Integer
            Dim maxDPS As Integer
            Dim MinMAx As Integer
            Dim range As Double

            If MultipleDamage.Count > 1 Then
                MultipleDamage.Sort()
                minDPS = MultipleDamage.Item(1) / (FightLength)
                maxDPS = MultipleDamage.Item(MultipleDamage.Count - 1) / (FightLength)
                MinMAx = Math.Max(DPS - minDPS, maxDPS - DPS)
                range = (maxDPS - minDPS) / (2 * DPS)
                myReport.AddAdditionalInfo("DPS", DPS & "(+/- " & MinMAx & ")")
            Else
                myReport.AddAdditionalInfo("DPS", DPS)
            End If

            myReport.AddAdditionalInfo("Total Damage", Math.Round(TotalDamage() / 1000000, 2) & "m")
            myReport.AddAdditionalInfo("RunicPower", RunicPower.Report)
            myReport.AddAdditionalInfo("Threat Per Second", TPS)
            Dim dif As Decimal = ((Now.Ticks - SimStart.Ticks) / 10000000)
            myReport.AddAdditionalInfo("Generated in ", Decimal.Round(dif, 2).ToString & " s")
            myReport.AddAdditionalInfo("Template", Split(Character.GetTemplateFileName, ".")(0))
            If Rotate Then
                myReport.AddAdditionalInfo("Rotation", Split(Character.GetRotationFileName, ".")(0))
            Else
                myReport.AddAdditionalInfo("Priority", Split(Character.GetPriorityFileName, ".")(0))
            End If
            myReport.AddAdditionalInfo("Presence", Character.GetPresence)
            myReport.AddAdditionalInfo("Sigil", Character.GetSigil)
            If Character.DualW Then
                myReport.AddAdditionalInfo("RuneEnchant", Character.GetMHEnchant)
            Else
                myReport.AddAdditionalInfo("RuneEnchant", Character.GetMHEnchant)
            End If
            myReport.AddAdditionalInfo("Pet Calculation", Character.GetPetCalculation)
            myReport.AddLine(DPSLine)
            myReport.AddLine(DPSLineAverage)
            myReport.Save("")
        End Sub




        Sub StoreMyDamage(ByVal damage As Long)
            Dim tmp As Long
            Dim i As Integer
            'On Error Resume Next
            tmp = damage
            If MultipleDamage.Count > 0 Then
                For i = 0 To MultipleDamage.Count - 1
                    Try
                        tmp = tmp - Long.Parse(MultipleDamage.Item(i).ToString)
                    Catch ex As Exception
                        Diagnostics.Debug.WriteLine("error converting " & MultipleDamage.Item(i).ToString)
                        Diagnostics.Debug.WriteLine(ex.StackTrace)
                    End Try

                Next
            End If
            MultipleDamage.Add(tmp)
        End Sub
        Function GetMedianValue() As Long
            Dim i As Integer = MultipleDamage.Count / 2
            Return MultipleDamage(i)
        End Function







    End Class
End Namespace
