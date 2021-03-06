Namespace Simulator.WowObjects.PetsAndMinions
    Friend Class Gargoyle
        Inherits WowObject

        Friend NextGargoyleStrike As Long

        Friend ActiveUntil As Long
        Friend cd As Long
        Private StrikeCastTime As Long
        Private AP As Integer
        Private SpellHit As Double
        Private CritChance As Double

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            total = 0
            MissCount = 0
            HitCount = 0
            CritCount = 0
            cd = 0
            ActiveUntil = 0
            TotalHit = 0
            TotalCrit = 0
            sim = S
            sim.DamagingObject.Add(Me)
            ThreadMultiplicator = 0
            HasteSensible = True
            isGuardian = True
            logLevel = LogLevelEnum.Detailled
            If sim.Character.Talents.Talent("SummonGargoyle").Value Then Talented = True
        End Sub


        Public Overrides Sub SoftReset()
            MyBase.SoftReset()
            cd = 0
            ActiveUntil = 0
            NextGargoyleStrike = 0
        End Sub

        Function Summon(ByVal T As Long) As Boolean
            If Sim.RuneForge.AreStarsAligned(T) = False Then
                Return False
            End If

            If cd <= T Then
                sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnGargoyleSummon)
                StrikeCastTime = Math.Max(1, (2.0 / sim.Character.PhysicalHaste.Value) * 100) 'no haste cap for Garg.
                AP = sim.Character.AP.CurrentValue
                CritChance = SpellCrit() 'sim.Character.SpellCrit.Value
                sim.RunicPower.Use(60)
                'sim.CombatLog.write(T & vbTab & "Gargoyle use")
                cd = T + 3 * 60 * 100
                ActiveUntil = T + 30 * 100
                SpellHit = sim.Character.SpellHit.Value
                UseGCD(T)
                NextGargoyleStrike = T + 1000
                sim.FutureEventManager.Add(NextGargoyleStrike, "Gargoyle")
                sim.CombatLog.write(T & vbTab & "Summon Gargoyle")
                Return True
            Else
                Return False
            End If
        End Function
        Sub UseGCD(ByVal T As Long)
            sim.UseGCD(True)
        End Sub
        Function ApplyDamage(ByVal T As Long) As Boolean
            NextGargoyleStrike = T + StrikeCastTime
            Sim.FutureEventManager.Add(NextGargoyleStrike, "Gargoyle")
            Dim RNG As Double

            RNG = RngHit()
            If SpellHit >= 0.17 Then
                RNG = RNG + 0.17
            Else
                RNG = RNG + SpellHit
            End If
            If RNG < 0.17 Then
                If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(T & vbTab & "Gargoyle Strike fail")
                MissCount = MissCount + 1
                Return False
            End If

            RNG = RngCrit
            Dim LastDamage As Integer
            If RNG <= CritChance Then
                LastDamage = AvrgCrit(T)
                CritCount = CritCount + 1
                TotalCrit += LastDamage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "Gargoyle Strike crit for " & LastDamage)
            Else
                LastDamage = AvrgNonCrit(T)
                HitCount = HitCount + 1
                TotalHit += LastDamage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "Gargoyle Strike hit for " & LastDamage)
            End If


            total = total + LastDamage

            Return True
        End Function
        Function AvrgNonCrit(ByVal T As Long) As Double
            Dim tmp As Double
            tmp = 120
            tmp = tmp + (AP * 0.3333)
            tmp = tmp * MagicalDamageMultiplier(T)
            
            Return tmp
        End Function
        Function CritCoef() As Double
            Return 1
        End Function
        'Function CritChance() As Double
        '    CritChance = SpellCrit()
        'End Function
        Function AvrgCrit(ByVal T As Long) As Double
            AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef())
        End Function

        Function MagicalDamageMultiplier(ByVal T As Long, Optional ByVal target As Targets.Target = Nothing) As Double
            If target Is Nothing Then target = sim.Targets.MainTarget
            Dim tmp As Double
            tmp = sim.Character.StandardMagicalDamageMultiplier(T, target)
            Return tmp
        End Function
        Function SpellCrit(Optional ByVal target As Targets.Target = Nothing) As Single
            'Return 0
            If target Is Nothing Then target = sim.Targets.MainTarget
            Dim tmp As Double
            tmp = tmp + 5 * sim.Character.Buff.Crit
            tmp = tmp + 5 * target.Debuff.SpellCritTaken
            SpellCrit = tmp / 100
        End Function
        Public Sub cleanup()
            total = 0
            HitCount = 0
            MissCount = 0
            CritCount = 0
            TotalHit = 0
            TotalCrit = 0
        End Sub
    End Class
End Namespace
