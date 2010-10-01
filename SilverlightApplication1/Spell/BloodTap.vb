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
        Dim InternalCD As Integer

        Sub New(ByVal s As Sim)
            MyBase.New(s)
            logLevel = LogLevelEnum.Basic
            InternalCD = (60 - (15 * sim.Character.Talents("ImprovedBloodTap"))) * 100
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

            CD = sim.TimeStamp + InternalCD

            If Not sim.Runes.BloodRune2.Available Then
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

            Diagnostics.Debug.WriteLine("BT Warning case not managed")
Out:
            sim.RunicPower.add(10)
            sim.CombatLog.write(sim.TimeStamp & vbTab & "Blood Tap")

            Me.HitCount = Me.HitCount + 1
            sim._UseGCD(1)

        End Sub

        Function UseWithCancelBT(ByVal T As Long) As Boolean
            CD = T + InternalCD
            If sim.Runes.BloodRune1.AvailableTime > T And sim.Runes.BloodRune1.death = False Then
                sim.Runes.BloodRune1.Activate()
                sim.Runes.BloodRune1.death = True
                'sim.Runes.BloodRune1.BTuntil = T + 2000
            Else
                sim.Runes.BloodRune2.Activate()
                sim.Runes.BloodRune2.death = True
                'sim.Runes.BloodRune2.BTuntil = T + 2000
            End If
            sim.RunicPower.add(10)
            sim.CombatLog.write(T & vbTab & "Blood Tap with Cancel Aura")
            Me.HitCount = Me.HitCount + 1
            sim._UseGCD(1)
            Return True
        End Function


    End Class
End Namespace