'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 23:59
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Spells
    Friend Class BloodTap
        Inherits Spells.Spell

        Sub New(ByVal s As Sim)
            MyBase.New(s)
            logLevel = LogLevelEnum.Basic
        End Sub
        Overrides Function IsAvailable() As Boolean
            If sim.TimeStamp >= CD Then
                Return True
            Else
                Return False
            End If
        End Function

        Sub CancelAura()
            If sim.Runes.BloodRune1.BTuntil > sim.TimeStamp Then
                sim.Runes.BloodRune1.BTuntil = 0
                sim.Runes.BloodRune1.death = False
            End If
            If sim.Runes.BloodRune2.BTuntil > sim.TimeStamp Then
                sim.Runes.BloodRune2.BTuntil = 0
                sim.Runes.BloodRune2.death = False
            End If
        End Sub


        Overrides Sub Use()

            CD = sim.TimeStamp + 6000
            'Two Blood Runes available or Two Blood runes unavailable, it will convert one of them to a Death Rune and make it available.
            If sim.Runes.BloodRune1.death = False And sim.Runes.BloodRune2.death = False Then
                If sim.Runes.BloodRune1.Available = sim.Runes.BloodRune2.Available Then
                    sim.Runes.BloodRune1.Activate()
                    sim.Runes.BloodRune1.death = True
                    sim.Runes.BloodRune1.BTuntil = sim.TimeStamp + 2000
                    GoTo Out
                Else
                    'One Blood Rune available and One Blood Rune unavailable, it will convert the available rune to a Death Rune and leave the other unavailable.
                    If sim.Runes.BloodRune1.Available Then
                        sim.Runes.BloodRune1.Activate()
                        sim.Runes.BloodRune1.death = True
                        sim.Runes.BloodRune1.BTuntil = sim.TimeStamp + 2000
                        GoTo Out
                    Else
                        sim.Runes.BloodRune2.Activate()
                        sim.Runes.BloodRune2.death = True
                        sim.Runes.BloodRune2.BTuntil = sim.TimeStamp + 2000
                        GoTo Out
                    End If
                End If
            End If

            'Two Death Runes and one or both Death Runes are unavailable, it will make one Death Rune available.

            If sim.Runes.BloodRune1.death And sim.Runes.BloodRune2.death Then
                If sim.Runes.BloodRune1.Available() Then
                    sim.Runes.BloodRune2.Activate()
                    sim.Runes.BloodRune2.death = True
                    sim.Runes.BloodRune2.BTuntil = sim.TimeStamp + 2000
                    GoTo Out
                Else
                    sim.Runes.BloodRune1.Activate()
                    sim.Runes.BloodRune1.death = True
                    sim.Runes.BloodRune1.BTuntil = sim.TimeStamp + 2000
                    GoTo Out
                End If
            End If

            'One Blood Rune and one Death Rune 	and one or both are available, it will make the unavailable rune available and convert the Blood Rune to a Death Rune.
            'One Blood Rune and one Death rune and both are unavailable, it will make the Blood Rune available and convert it to a Death Rune.

            If sim.Runes.BloodRune1.death <> sim.Runes.BloodRune2.death Then
                If sim.Runes.BloodRune1.death = True Then
                    sim.Runes.BloodRune2.death = True
                    sim.Runes.BloodRune2.BTuntil = sim.TimeStamp + 2000
                Else
                    sim.Runes.BloodRune1.death = True
                    sim.Runes.BloodRune1.BTuntil = sim.TimeStamp + 2000
                End If
                If sim.Runes.BloodRune1.Available(sim.TimeStamp) Then
                    sim.Runes.BloodRune2.Activate()
                    GoTo Out
                Else
                    sim.Runes.BloodRune1.Activate()
                    GoTo Out
                End If
            End If
            Diagnostics.Debug.WriteLine("BT Warning case not managed")
Out:
            sim.RunicPower.add(10)
            sim.CombatLog.write(sim.TimeStamp & vbTab & "Blood Tap")

            Me.HitCount = Me.HitCount + 1
            sim._UseGCD(sim.TimeStamp, 1)

        End Sub

        Function UseWithCancelBT(ByVal T As Long) As Boolean
            CD = T + 6000
            If sim.Runes.BloodRune1.AvailableTime > T And sim.Runes.BloodRune1.death = False Then
                sim.Runes.BloodRune1.Activate()
                sim.Runes.BloodRune1.death = True
                'sim.Runes.BloodRune1.BTuntil = T + 2000
            Else
                sim.Runes.BloodRune2.Activate()
                sim.Runes.BloodRune2.death = True
                'sim.Runes.BloodRune2.BTuntil = T + 2000
            End If
            sim.RunicPower.add(0)
            sim.CombatLog.write(T & vbTab & "Blood Tap with Cancel Aura")
            Me.HitCount = Me.HitCount + 1
            sim._UseGCD(T, 1)
            Return True
        End Function


    End Class
End Namespace