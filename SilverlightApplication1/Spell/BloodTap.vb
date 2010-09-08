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
        Function IsAvailable(ByVal T As Long) As Boolean

            If T >= CD Then
                Return True

            Else
                Return False
            End If
        End Function

        Sub CancelAura()
            If Sim.Runes.BloodRune1.BTuntil > Sim.TimeStamp Then
                Sim.Runes.BloodRune1.BTuntil = 0
                Sim.Runes.BloodRune1.death = False
            End If
            If Sim.Runes.BloodRune2.BTuntil > Sim.TimeStamp Then
                Sim.Runes.BloodRune2.BTuntil = 0
                Sim.Runes.BloodRune2.death = False
            End If
        End Sub


        Function Use(ByVal T As Long) As Boolean
            CD = T + 6000
            'Two Blood Runes available or Two Blood runes unavailable, it will convert one of them to a Death Rune and make it available.
            If Sim.Runes.BloodRune1.death = False And Sim.Runes.BloodRune2.death = False Then
                If Sim.Runes.BloodRune1.Available(T) = Sim.Runes.BloodRune2.Available(T) Then
                    Sim.Runes.BloodRune1.Activate()
                    Sim.Runes.BloodRune1.death = True
                    Sim.Runes.BloodRune1.BTuntil = T + 2000
                    GoTo Out
                Else
                    'One Blood Rune available and One Blood Rune unavailable, it will convert the available rune to a Death Rune and leave the other unavailable.
                    If Sim.Runes.BloodRune1.Available(T) Then
                        Sim.Runes.BloodRune1.Activate()
                        Sim.Runes.BloodRune1.death = True
                        Sim.Runes.BloodRune1.BTuntil = T + 2000
                        GoTo Out
                    Else
                        Sim.Runes.BloodRune2.Activate()
                        Sim.Runes.BloodRune2.death = True
                        Sim.Runes.BloodRune2.BTuntil = T + 2000
                        GoTo Out
                    End If
                End If
            End If

            'Two Death Runes and one or both Death Runes are unavailable, it will make one Death Rune available.

            If Sim.Runes.BloodRune1.death And Sim.Runes.BloodRune2.death Then
                If Sim.Runes.BloodRune1.Available(T) Then
                    Sim.Runes.BloodRune2.Activate()
                    Sim.Runes.BloodRune2.death = True
                    Sim.Runes.BloodRune2.BTuntil = T + 2000
                    GoTo Out
                Else
                    Sim.Runes.BloodRune1.Activate()
                    Sim.Runes.BloodRune1.death = True
                    Sim.Runes.BloodRune1.BTuntil = T + 2000
                    GoTo Out
                End If
            End If

            'One Blood Rune and one Death Rune 	and one or both are available, it will make the unavailable rune available and convert the Blood Rune to a Death Rune.
            'One Blood Rune and one Death rune and both are unavailable, it will make the Blood Rune available and convert it to a Death Rune.

            If Sim.Runes.BloodRune1.death <> Sim.Runes.BloodRune2.death Then
                If Sim.Runes.BloodRune1.death = True Then
                    Sim.Runes.BloodRune2.death = True
                    Sim.Runes.BloodRune2.BTuntil = T + 2000
                Else
                    Sim.Runes.BloodRune1.death = True
                    Sim.Runes.BloodRune1.BTuntil = T + 2000
                End If
                If Sim.Runes.BloodRune1.Available(T) Then
                    Sim.Runes.BloodRune2.Activate()
                    GoTo Out
                Else
                    Sim.Runes.BloodRune1.Activate()
                    GoTo Out
                End If
            End If


            Diagnostics.Debug.WriteLine("BT Warning case not managed")


Out:
            Sim.RunicPower.add(0)
            Sim.CombatLog.write(T & vbTab & "Blood Tap")

            Me.HitCount = Me.HitCount + 1
            Sim._UseGCD(T, 1)
            Return True
        End Function

        Function UseWithCancelBT(ByVal T As Long) As Boolean
            CD = T + 6000
            If Sim.Runes.BloodRune1.AvailableTime > T And Sim.Runes.BloodRune1.death = False Then
                Sim.Runes.BloodRune1.Activate()
                Sim.Runes.BloodRune1.death = True
                'sim.Runes.BloodRune1.BTuntil = T + 2000
            Else
                Sim.Runes.BloodRune2.Activate()
                Sim.Runes.BloodRune2.death = True
                'sim.Runes.BloodRune2.BTuntil = T + 2000
            End If
            Sim.RunicPower.add(0)
            Sim.CombatLog.write(T & vbTab & "Blood Tap with Cancel Aura")
            Me.HitCount = Me.HitCount + 1
            Sim._UseGCD(T, 1)
            Return True
        End Function


    End Class
End Namespace