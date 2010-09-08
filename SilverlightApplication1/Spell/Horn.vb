'
' Created by SharpDevelop.
' User: Fabien
' Date: 07/07/2009
' Time: 19:04
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Spells
    Public Class Horn
        Inherits Spells.Spell

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            logLevel = LogLevelEnum.Basic
        End Sub


        Function isAutoAvailable(ByVal T As Long) As Boolean
            If Sim.Rotate = True Then
                If Sim.Rotation.MyRotation.Contains("Horn") Then Return False
            Else
                If Sim.Priority.prio.Contains("Horn") Then Return False
            End If
            If Sim.Runes.RuneRefreshTheNextGCD(T) = True Then
                Return isAvailable(T)
            Else
                Return False
            End If
        End Function



        Function isAvailable(ByVal T As Long) As Boolean
            If Sim.RunicPower.CheckMax(9) Then Return False

            If CD <= T Then
                Return True
            Else
                Return False
            End If
        End Function

        Function use(ByVal T As Long) As Boolean
            CD = T + 20 * 100
            Sim.RunicPower.add(10)
            HitCount = HitCount + 1
            UseGCD(T)
            Sim.CombatLog.write(T & vbTab & "Horn used")
            Return True
        End Function


    End Class
End Namespace
