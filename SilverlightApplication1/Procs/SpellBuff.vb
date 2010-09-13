Namespace Simulator.WowObjects
    Public Class SpellBuff
        Inherits WowObject
        Dim Value As Integer
        Friend Stat As Sim.Stat
        Dim Lenght As Long
        Friend previousFade As Long
        Friend Currentstack As Integer
        Dim MaxStack As Integer = 1
        Dim FutureEvent As FutureEvent

     


        Sub New(ByVal s As Sim, ByVal name As String, ByVal stat As Sim.Stat, ByVal value As Integer, ByVal Length As Long)
            MyBase.New(s)
            _Name = name
            Me.Stat = stat
            Me.Value = value
            Me.Lenght = Length
        End Sub
        Sub New(ByVal s As Sim, ByVal name As String, ByVal stat As Sim.Stat, ByVal Multiplicator As Double, ByVal Length As Long)
            MyBase.New(s)
            _Name = name
            Me.Stat = stat
            Me.Multiplicator = Multiplicator
            Me.Value = 0
            Me.Lenght = Length
        End Sub
        Sub New(ByVal s As Sim, ByVal name As String, ByVal stat As Sim.Stat, ByVal StackValue As Integer, ByVal MaxStack As Integer, ByVal length As Long)
            MyBase.New(s)
            _Name = name
            Me.Stat = stat
            Me.Value = StackValue
            Me.MaxStack = MaxStack
            Me.Lenght = length
        End Sub




        Sub AddUptime(ByVal T As Long)

            If EpStat <> "" Then Return
            Dim tmp As Long

            If Lenght * 100 + T > sim.NextReset Then
                tmp = (sim.NextReset - T) / 100
            Else
                tmp = Lenght
            End If

            If previousFade < T Then
                uptime += tmp * 100
            Else
                uptime += tmp * 100 - (previousFade - T)
            End If
            previousFade = T + tmp * 100
        End Sub

        Sub RemoveUptime(ByVal T As Long)
            If EpStat <> "" Then Return
            If previousFade < T Then
            Else
                uptime -= (previousFade - T)
            End If
            previousFade = T
        End Sub

        Sub Apply()
            TimeWasted.Start()
            If Currentstack < MaxStack Then
                Currentstack += 1
                If Multiplicator <> 1 Then
                    Select Case Stat
                        Case Simulator.Sim.Stat.Agility
                            sim.Character.Agility.AddMulti(Multiplicator)
                        Case Simulator.Sim.Stat.AP
                            sim.Character.AttackPower.AddMulti(Multiplicator)
                        Case Simulator.Sim.Stat.Armor
                            sim.Character.Armor.AddMulti(Multiplicator)
                        Case Simulator.Sim.Stat.ArP
                            sim.Character.ArmorPenetrationRating.AddMulti(Multiplicator)
                        Case Simulator.Sim.Stat.Crit
                            sim.Character.CritRating.AddMulti(Multiplicator)
                        Case Simulator.Sim.Stat.Expertise
                            sim.Character.ExpertiseRating.AddMulti(Multiplicator)
                        Case Simulator.Sim.Stat.Haste
                            sim.Character.HasteRating.AddMulti(Multiplicator)
                        Case Simulator.Sim.Stat.Hit
                            sim.Character.HitRating.AddMulti(Multiplicator)
                        Case Simulator.Sim.Stat.Intel
                            sim.Character.Intel.AddMulti(Multiplicator)
                        Case Simulator.Sim.Stat.Mastery
                            sim.Character.MasteryRating.AddMulti(Multiplicator)
                        Case Simulator.Sim.Stat.Strength
                            sim.Character.Strength.AddMulti(Multiplicator)
                        Case Else
                            Diagnostics.Debug.WriteLine("SpellBuff: WTF is this stat")
                            'Error 10
                    End Select
                ElseIf Value <> 0 Then
                    Select Case Stat
                        Case Simulator.Sim.Stat.Agility
                            sim.Character.Agility.Add(Value)
                        Case Simulator.Sim.Stat.AP
                            sim.Character.AttackPower.Add(Value)
                        Case Simulator.Sim.Stat.Armor
                            sim.Character.Armor.Add(Value)
                        Case Simulator.Sim.Stat.ArP
                            sim.Character.ArmorPenetrationRating.Add(Value)
                        Case Simulator.Sim.Stat.Crit
                            sim.Character.CritRating.Add(Value)
                        Case Simulator.Sim.Stat.Expertise
                            sim.Character.ExpertiseRating.Add(Value)
                        Case Simulator.Sim.Stat.Haste
                            sim.Character.HasteRating.Add(Value)
                        Case Simulator.Sim.Stat.Hit
                            sim.Character.HitRating.Add(Value)
                        Case Simulator.Sim.Stat.Intel
                            sim.Character.Intel.Add(Value)
                        Case Simulator.Sim.Stat.Mastery
                            sim.Character.MasteryRating.Add(Value)
                        Case Simulator.Sim.Stat.SpecialArmor
                            sim.Character.Armor.SpecialArmor += Value
                        Case Simulator.Sim.Stat.Strength
                            sim.Character.Strength.Add(Value)
                        Case Else
                            Diagnostics.Debug.WriteLine("SpellBuff: WTF is this stat")
                            'Error 10
                    End Select
                Else
                    Diagnostics.Debug.WriteLine("SpellBuff: WTF is this Spell")
                End If
            End If
            Dim T As Long = sim.TimeStamp
            If Currentstack > 1 Then sim.FutureEventManager.Remove(FutureEvent)
            FutureEvent = sim.FutureEventManager.Add(T + (Lenght * 100), "BuffFade", Me)

            AddUptime(T)
            TimeWasted.Pause()
        End Sub

        Sub Fade()
            TimeWasted.Start()
            If Multiplicator <> 1 Then
                Select Case Stat
                    Case Simulator.Sim.Stat.Agility
                        sim.Character.Agility.RemoveMulti(Multiplicator)
                    Case Simulator.Sim.Stat.AP
                        sim.Character.AttackPower.RemoveMulti(Multiplicator)
                    Case Simulator.Sim.Stat.Armor
                        sim.Character.Armor.RemoveMulti(Multiplicator)
                    Case Simulator.Sim.Stat.ArP
                        sim.Character.ArmorPenetrationRating.RemoveMulti(Multiplicator)
                    Case Simulator.Sim.Stat.Crit
                        sim.Character.CritRating.RemoveMulti(Multiplicator)
                    Case Simulator.Sim.Stat.Expertise
                        sim.Character.ExpertiseRating.RemoveMulti(Multiplicator)
                    Case Simulator.Sim.Stat.Haste
                        sim.Character.HasteRating.RemoveMulti(Multiplicator)
                    Case Simulator.Sim.Stat.Hit
                        sim.Character.HitRating.RemoveMulti(Multiplicator)
                    Case Simulator.Sim.Stat.Intel
                        sim.Character.Intel.RemoveMulti(Multiplicator)
                    Case Simulator.Sim.Stat.Mastery
                        sim.Character.MasteryRating.RemoveMulti(Multiplicator)
                    Case Simulator.Sim.Stat.Strength
                        sim.Character.Strength.RemoveMulti(Multiplicator)
                    Case Else
                        Diagnostics.Debug.WriteLine("SpellBuff: WTF is this stat")
                        'Error 10
                End Select
            ElseIf Value <> 0 Then
                Select Case Stat
                    Case Simulator.Sim.Stat.Agility
                        sim.Character.Agility.Remove(Value * Currentstack)
                    Case Simulator.Sim.Stat.AP
                        sim.Character.AttackPower.Remove(Value * Currentstack)
                    Case Simulator.Sim.Stat.Armor
                        sim.Character.Armor.Remove(Value * Currentstack)
                    Case Simulator.Sim.Stat.ArP
                        sim.Character.ArmorPenetrationRating.Remove(Value * Currentstack)
                    Case Simulator.Sim.Stat.Crit
                        sim.Character.CritRating.Remove(Value * Currentstack)
                    Case Simulator.Sim.Stat.Expertise
                        sim.Character.ExpertiseRating.Remove(Value * Currentstack)
                    Case Simulator.Sim.Stat.Haste
                        sim.Character.HasteRating.Remove(Value * Currentstack)
                    Case Simulator.Sim.Stat.Hit
                        sim.Character.HitRating.Remove(Value * Currentstack)
                    Case Simulator.Sim.Stat.Intel
                        sim.Character.Intel.Remove(Value * Currentstack)
                    Case Simulator.Sim.Stat.Mastery
                        sim.Character.MasteryRating.Remove(Value * Currentstack)
                    Case Simulator.Sim.Stat.SpecialArmor
                        sim.Character.Armor.SpecialArmor -= Value * Currentstack
                    Case Simulator.Sim.Stat.Strength
                        sim.Character.Strength.Remove(Value * Currentstack)
                    Case Else
                        Diagnostics.Debug.WriteLine("SpellBuff WTF is this stat")
                        'Error 10
                End Select
            Else
                Diagnostics.Debug.WriteLine("SpellBuff: WTF is this Spell")
            End If

            RemoveUptime(sim.TimeStamp)
            Currentstack = 0
            TimeWasted.Pause()

        End Sub

    End Class
End Namespace