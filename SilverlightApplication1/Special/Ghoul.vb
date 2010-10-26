Namespace Simulator.WowObjects.PetsAndMinions
    Friend Class Ghoul
        Inherits WowObject
        Friend ShadowInfusion As Procs.Proc
        Friend NextWhiteMainHit As Long
        Protected NextClaw As Long
        Friend ActiveUntil As Long
        Friend cd As Long
        Friend _Haste As Double
        Friend _AP As Integer

        Private MeleeMissChance As Single
        Private MeleeDodgeChance As Single
        Private MeleeGlacingChance As Single
        Private SpellMissChance As Single
        Private Claw As Strikes.Strike
        Private SuperClaw As Strikes.Strike


        Public Overrides Sub SoftReset()
            MyBase.SoftReset()
            cd = 0
            NextClaw = sim.TimeStamp
            NextWhiteMainHit = sim.TimeStamp
            sim.FutureEventManager.Add(sim.TimeStamp, "Ghoul")
        End Sub

        Sub New(ByVal MySim As Sim)
            MyBase.New(MySim)
            total = 0
            MissCount = 0
            HitCount = 0
            CritCount = 0
            cd = 0
            ActiveUntil = 0
            NextWhiteMainHit = 0
            NextClaw = 0
            TotalHit = 0
            TotalCrit = 0
            sim = MySim
            MeleeGlacingChance = 0.24
            sim.DamagingObject.Add(Me)
            ThreadMultiplicator = 0
            HasteSensible = True
            Claw = New Strikes.Strike(sim)
            Claw._Name = "Ghoul: Claw"
            logLevel = LogLevelEnum.Detailled

            SuperClaw = New Strikes.Strike(sim)
            SuperClaw._Name = "Ghoul: Sweeping Claw"


            ShadowInfusion = New Procs.Proc(MySim)
            If sim.Character.Talents.Talent("ShadowInfusion").Value > 0 Then
                With ShadowInfusion
                    .ProcOn = Procs.ProcsManager.ProcOnType.onRPDump
                    .ProcChance = sim.Character.Talents.Talent("ShadowInfusion").Value / 3
                    .MaxStack = 5
                    .ProcLenght = 30
                    .ProcType = Simulator.Sim.Stat.GhoulDamage
                    ._Name = "Shadow Infusion"
                    .Equip()
                End With
            End If
        End Sub

        Sub Summon(ByVal T As Long)
            If cd <= T Then
                sim.FutureEventManager.Add(T, "Ghoul")
                MeleeMissChance = Math.Max(0.08 - sim.GhoulStat.Hit, 0)
                MeleeDodgeChance = Math.Max(0.065 - sim.GhoulStat.Expertise, 0)
                SpellMissChance = Math.Max(0.17 - sim.GhoulStat.SpellHit, 0)
                If sim.Character.Talents.GetNumOfThisSchool(Character.Talents.Schools.Unholy) > 30 Then
                    ActiveUntil = sim.MaxTime
                    cd = sim.MaxTime
                    isGuardian = False
                Else
                    _Haste = sim.Character.PhysicalHaste.Value
                    _AP = sim.GhoulStat.AP
                    ActiveUntil = T + 60 * 100
                    cd = ActiveUntil + (3 * 60 * 100)
                    isGuardian = True
                End If
                If T <= 1 Then
                Else
                    sim.CombatLog.write(T & vbTab & "Summon Ghoul")
                    UseGCD()
                End If
            End If
        End Sub
        Sub UseGCD()
            sim.UseGCD(False)
        End Sub

        Function NextActionTime() As Long
            Return Math.Min(NextClaw, NextWhiteMainHit)
        End Function

        Sub TryActions(ByVal TimeStamp As Long)
            If NextWhiteMainHit <= TimeStamp Then ApplyDamage(TimeStamp)
            TryClaw(TimeStamp)
        End Sub

        Function Haste() As Double
            Dim tmp As Double
            If isGuardian Then
                Return _Haste
            End If
            tmp = sim.Character.PhysicalHaste.Value
            Return tmp
        End Function

        Function Agility() As Integer
            Dim tmp As Double
            Return 0
            tmp = sim.GhoulStat.Agility
            tmp = tmp + 155 * 1.15 * sim.Character.Buff.StrAgi
            tmp = tmp * (1 + 5 * sim.Character.Buff.StatMulti / 100)
            Return tmp
        End Function

        Function Strength() As Integer
            Dim tmp As Integer
            tmp = sim.GhoulStat.Strength
            tmp = tmp + 155 * 1.15 * sim.Character.Buff.StrAgi
            tmp = tmp * (1 + 5 * sim.Character.Buff.StatMulti / 100)
            Return tmp
        End Function

        Function AP() As Integer
            Dim tmp As Integer
            If isGuardian Then Return _AP
            tmp = sim.GhoulStat.BaseAP + Strength() * 2
            Return (tmp) * (1 + sim.Character.Buff.AttackPowerPc / 10)
        End Function

        Function ApplyDamage(ByVal T As Long) As Boolean
            Dim LastDamage As Integer
            Dim WSpeed As Single
            WSpeed = sim.GhoulStat.MHWeaponSpeed
            NextWhiteMainHit = T + sim.GhoulStat.SwingTime(Haste()) * 100
            sim.FutureEventManager.Add(NextWhiteMainHit, "Ghoul")
            Dim RNG As Double
            RNG = RngHit()

            If RNG < (MeleeMissChance + MeleeDodgeChance) Then
                MissCount = MissCount + 1
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "Ghoul fail")
                Return False
            End If
            If RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) Then
                LastDamage = AvrgNonCrit(T) * 0.7
                total = total + LastDamage
                TotalGlance += LastDamage
                GlancingCount += 1
            End If
            If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) And RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance()) Then
                'CRIT !
                LastDamage = AvrgCrit(T)
                CritCount = CritCount + 1
                TotalCrit += LastDamage
                total = total + LastDamage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "Ghoul crit for " & LastDamage)
            End If
            If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance()) Then
                'normal hit
                HitCount = HitCount + 1
                LastDamage = AvrgNonCrit(T)
                total = total + LastDamage
                TotalHit += LastDamage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "Ghoul hit for " & LastDamage)
            End If
            Return True
        End Function
        Function AvrgNonCrit(ByVal T As Long) As Double
            Dim tmp As Double
            tmp = sim.GhoulStat.MHBaseDamage(AP) * sim.GhoulStat.PhysicalDamageMultiplier(T)
            AvrgNonCrit = tmp
        End Function
        Function CritCoef() As Double
            CritCoef = 1
        End Function
        Function CritChance() As Double
            Return 0.05
            CritChance = sim.GhoulStat.crit
        End Function
        Function AvrgCrit(ByVal T As Long) As Double
            AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef())
        End Function


        Function SuperClaw_ApplyDamage(ByVal t As Long) As Boolean

            Dim RNG As Double
            Dim LastDamage As Integer
            If NextClaw > t Then Return False


            For Each Tar As Targets.Target In sim.Targets.AllTargets
                Dim i As Integer = 0
                RNG = Me.Claw.RngHit
                If RNG < (MeleeMissChance + MeleeDodgeChance) Then
                    If sim.CombatLog.LogDetails Then sim.CombatLog.write(t & vbTab & "Ghoul's Super Claw fail")
                    SuperClaw.MissCount += 1
                    Return False
                End If

                RNG = Me.Claw.RngCrit
                If RNG <= CritChance() Then
                    LastDamage = ClawAvrgCrit(t)
                    SuperClaw.CritCount += 1
                    SuperClaw.total += LastDamage
                    SuperClaw.TotalCrit += LastDamage
                    If sim.CombatLog.LogDetails Then sim.CombatLog.write(t & vbTab & "Ghoul's Super Claw for " & LastDamage)
                Else
                    LastDamage = ClawAvrgNonCrit(t)
                    SuperClaw.HitCount += 1
                    SuperClaw.total += LastDamage
                    SuperClaw.TotalHit += LastDamage
                    If sim.CombatLog.LogDetails Then sim.CombatLog.write(t & vbTab & "Ghoul's Super Claw hit for " & LastDamage)
                End If
                i += 1
                If i > 3 Then
                    Exit For
                End If
            Next

            NextClaw = t + sim.GhoulStat.ClawTime(Haste()) * 100
            sim.FutureEventManager.Add(NextClaw, "Ghoul")
            Return True
        End Function

        Function TryClaw(ByVal T As Long) As Boolean


            Dim RNG As Double
            Dim LastDamage As Integer
            If NextClaw > T Then Return False

            If Me.sim.DarkTransformation.DarkTransformationBuff.IsActive Then
                SuperClaw_ApplyDamage(T)
                Return True
            End If


            RNG = Me.Claw.RngHit
            If RNG < (MeleeMissChance + MeleeDodgeChance) Then
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "Ghoul's Claw fail")
                Claw.MissCount += 1
                Return False
            End If

            RNG = Me.Claw.RngCrit
            If RNG <= CritChance() Then
                LastDamage = ClawAvrgCrit(T)
                Claw.CritCount += 1
                Claw.total += LastDamage
                Claw.TotalCrit += LastDamage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "Ghoul's Claw for " & LastDamage)
            Else
                LastDamage = ClawAvrgNonCrit(T)
                Claw.HitCount += 1
                Claw.total += LastDamage
                Claw.TotalHit += LastDamage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "Ghoul's Claw hit for " & LastDamage)
            End If

            NextClaw = T + sim.GhoulStat.ClawTime(Haste()) * 100
            sim.FutureEventManager.Add(NextClaw, "Ghoul")
            Return True
        End Function


        Function ClawAvrgNonCrit(ByVal T As Long) As Integer
            If Me.sim.DarkTransformation.DarkTransformationBuff.IsActive Then
                Return AvrgNonCrit(T) * 1.5
            Else
                Return AvrgNonCrit(T) * 1.25
            End If
        End Function
        Function ClawAvrgCrit(ByVal T As Long) As Integer
            Return ClawAvrgNonCrit(T) * (1 + CritCoef())
        End Function
        Public Sub cleanup()
            total = 0
            HitCount = 0
            MissCount = 0
            CritCount = 0
            TotalHit = 0
            TotalCrit = 0
        End Sub

        Public Overrides Function report() As ReportLine
            If isGuardian And Claw.total > 0 Then Merge() 'if we don't have a permaghoul merge in claw

            Return MyBase.Report()
        End Function

        Public Overrides Sub Merge()

            total += Claw.total + SuperClaw.total
            TotalHit += Claw.TotalHit + SuperClaw.TotalHit
            TotalCrit += Claw.TotalCrit + SuperClaw.TotalCrit

            MissCount = (MissCount + Claw.MissCount + SuperClaw.MissCount)
            HitCount = (HitCount + Claw.HitCount + SuperClaw.HitCount)
            CritCount = (CritCount + Claw.CritCount + SuperClaw.CritCount)

            Claw.total = 0
            Claw.TotalHit = 0
            Claw.TotalCrit = 0

            Claw.HitCount = 0
            Claw.CritCount = 0
            Claw.MissCount = 0

            SuperClaw.total = 0
            SuperClaw.TotalHit = 0
            SuperClaw.TotalCrit = 0

            SuperClaw.HitCount = 0
            SuperClaw.CritCount = 0
            SuperClaw.MissCount = 0


        End Sub

    End Class
End Namespace
