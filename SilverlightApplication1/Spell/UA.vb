'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 23:59
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Spells
    Friend Class PillarOfFrost
        Inherits spells.Spell
        Friend previousFade As Long
        Friend Talented As Boolean

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            logLevel = LogLevelEnum.Basic
            If Sim.Character.Talents.Talent("PillarOfFrost").Value <> 0 Then Talented = True


        End Sub



        Function IsAvailable(ByVal T As Long) As Boolean

            If Not Talented Then Return False
            If CD >= T Then Return False
            If Sim.BloodTap.IsAvailable(T) And Sim.Runes.Frost(T) = False Then
                Return True
            Else
                Return False
            End If

        End Function
        Function Use(ByVal T As Long) As Boolean


            If Sim.Runes.Frost(T) = False Then
                If Sim.BloodTap.IsAvailable(T) Then
                    Sim.BloodTap.Use(T)
                Else
                    Return False
                End If
            End If

            CD = T + 60 * 100
            Sim.Runes.UseDeathBlood(T, True)
            ActiveUntil = T + 20 * 100

            Sim._UseGCD(T, 1)
            Sim.RunicPower.add(15)
            Sim.CombatLog.write(T & vbTab & "Pillar of Frost")
            Me.HitCount = Me.HitCount + 1
            AddUptime(T)
            Return True
        End Function
        Function isActive() As Boolean
            If ActiveUntil >= Sim.TimeStamp Then
                Return True
            Else
                Return False
            End If
        End Function


        Sub AddUptime(ByVal T As Long)
            Dim tmp As Long
            If ActiveUntil > Sim.NextReset Then
                tmp = (Sim.NextReset - T)
            Else
                tmp = ActiveUntil - T
            End If

            If previousFade < T Then
                uptime += tmp
            Else
                uptime += tmp - (previousFade - T)
            End If
            previousFade = T + tmp
        End Sub

        Sub RemoveUptime(ByVal T As Long)
            If previousFade < T Then
            Else
                uptime -= (previousFade - T)
            End If
            previousFade = T
        End Sub


    End Class
End Namespace
