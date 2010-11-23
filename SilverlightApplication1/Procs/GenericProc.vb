
Namespace Simulator.WowObjects.Procs
    Public Class Proc
        Inherits WowObject

        Friend CD As Integer
        Friend Fade As Integer
        Friend ProcChance As Double
        Friend Equiped As Integer
        Friend ProcLenght As Integer
        Friend ProcValue As Integer
        'Friend ProcMultiplier As Double = 1
        Friend InternalCD As Integer
        Friend Stack As Integer
        Friend MaxStack As Integer
        Friend ProcTypeStack As Sim.Stat
        Friend ProcValueStack As Integer

        Friend CurrentStack As Integer

        Friend ProcType As Sim.Stat
        Friend ProcOn As ProcsManager.ProcOnType

        Friend previousFade As Long
        Friend Effects As New List(Of Effect)
        Friend isDebuff As Boolean


        Sub New(ByVal S As Sim)
            MyBase.New(S)
            _RNG1 = Nothing
            ProcChance = 0
            Equiped = 0
            ProcLenght = 0
            ProcValue = 0
            InternalCD = 0
            CurrentStack = 0
            ThreadMultiplicator = 1
            total = 0
            HitCount = 0
            MissCount = 0
            CritCount = 0
            TotalHit = 0
            TotalCrit = 0
            isDebuff = False

            _name = Me.ToString
            sim = S


            Sim.proc.AllProcs.Add(Me)
        End Sub

        Public Overrides Sub SoftReset()
            MyBase.SoftReset()
            CD = 0
            Fade = 0
            Stack = 0
        End Sub

        Overridable Sub Equip()
            If Equiped = 1 Then Exit Sub
            Equiped = 1
            Sim.proc.EquipedProc.Add(Me)
            Sim.DamagingObject.Add(Me)
        End Sub

        Overridable Sub UnEquip()
            Me.Equiped = 0
            Sim.proc.EquipedProc.Remove(Me)
            Sim.DamagingObject.Remove(Me)
        End Sub


        Overridable Function IsActiveAt(ByVal T As Long) As Boolean
            If Fade >= T Then
                Return True
            Else
                Return False
            End If
        End Function



        Overridable Function IsActive() As Boolean
            TimeWasted.Start()
            If Fade >= sim.TimeStamp Then
                TimeWasted.Pause()
                Return True
            Else
                TimeWasted.Pause()
                Return False
            End If
        End Function

        Sub Use()
            CurrentStack -= 1
            If CurrentStack <= 0 Then
                Fade = sim.TimeStamp
                CurrentStack = 0
                RemoveUptime(Sim.TimeStamp)
            End If
        End Sub
        Sub Cancel()
            CurrentStack = 0
            If Fade > sim.TimeStamp Then
                Fade = sim.TimeStamp
                RemoveUptime(sim.TimeStamp)
            End If
        End Sub
        Overridable Function IsAvailable(ByVal T As Long) As Boolean
            If Equiped = 0 Or CD > T Then Return False
            Return True
        End Function

        Overridable Function TryMe(ByVal T As Long) As Boolean
            If Equiped = 0 Then
                'sim.UselessCheck += 1
                'Diagnostics.Debug.WriteLine (me.Name)
                Return False
            End If
            If CD > T Then Return False
            Dim d As Double = RngHit()
            If d > ProcChance Then
                Return False
            End If

            ApplyMe(T)
            Return True
        End Function

        Sub ApplyFade(ByVal T As Long)
            If Sim.EPStat <> "" Then Exit Sub 'useless as no report generated.
            If ProcLenght Then
                AddUptime(T)
            End If

        End Sub

        Overridable Sub ApplyMe(ByVal T As Long)
            If Effects.Count > 0 Then
                For Each e In Effects
                    e.Apply()
                Next
            End If
            CD = T + InternalCD * 100
            If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & Me.ToString & " proc")
            If MaxStack <> 0 Then
                Stack = Math.Min(Stack + 1, MaxStack)
            End If
            If ProcLenght Then
                Fade = T + ProcLenght * 100
            End If
            HitCount += 1
            ApplyFade(T)
        End Sub



        Public Sub cleanup()
            Total = 0
            HitCount = 0
            MissCount = 0
            CritCount = 0
            TotalHit = 0
            TotalCrit = 0
        End Sub

        Sub AddUptime(ByVal T As Long)
            If Not sim.CalculateUPtime Then Return
            Dim tmp As Long

            If ProcLenght * 100 + T > sim.NextReset Then
                tmp = (sim.NextReset - T) / 100
            Else
                tmp = ProcLenght
            End If

            If previousFade < T Then
                uptime += tmp * 100
            Else
                uptime += tmp * 100 - (previousFade - T)
            End If
            previousFade = T + tmp * 100
        End Sub
        Sub RemoveUptime(ByVal T As Long)
            If previousFade < T Then
            Else
                uptime -= (previousFade - T)
            End If
            previousFade = T
        End Sub
    End Class
    Class MightOfFrozenWastes
        Inherits Proc
        Sub New(ByVal S As Sim)
            MyBase.New(S)
        End Sub
        Public Overrides Sub ApplyMe(ByVal T As Long)
            sim.RunicPower.add(10)
            Sim.CombatLog.write(Sim.TimeStamp & vbTab & Me.Name & " proc")
            MyBase.ApplyMe(T)
        End Sub

    End Class
 
    Class Shadowmourne
        Inherits WeaponProc

        Dim ChaosBaneBuff As SpellBuff
        Dim ChaosBaneSpell As WeaponProc

        Sub New(ByVal s As Sim)
            MyBase.New(s)
            ChaosBaneBuff = New SpellBuff(s, "Chaos Bane Buff", Simulator.Sim.Stat.Strength, 270, 10)
            ChaosBaneSpell = New WeaponProc(s)
            With ChaosBaneSpell
                .DamageType = "shadow"
                ._Name = "Chaos Bane - Damage"
                .ProcValue = 2000
                .ProcChance = 1
            End With
        End Sub

        Public Overrides Sub ApplyMe(ByVal T As Long)
            If Effects.Item(0).Currentstack > 9 Then
                Effects.Item(0).FAde()
                CD = T + 1000
                ChaosBaneBuff.Apply()
                ChaosBaneSpell.TryMe(T)
                Stack = 0
            Else
                MyBase.ApplyMe(T)
            End If
        End Sub
        Public Overrides Sub Equip()
            MyBase.Equip()
            ChaosBaneSpell.Equip()
        End Sub

    End Class
End Namespace
