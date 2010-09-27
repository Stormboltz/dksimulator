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
            Resource = New Resource(S, ResourcesEnum.None, 10, False)
        End Sub


        Function isAutoAvailable(ByVal T As Long) As Boolean
            If Sim.Rotate = True Then
                If Sim.Rotation.MyRotation.Contains("Horn") Then Return False
            Else
                If Sim.Priority.prio.Contains("Horn") Then Return False
            End If
            If Sim.Runes.RuneRefreshTheNextGCD(T) = True Then
                Return isAvailable()
            Else
                Return False
            End If
        End Function



        Overrides Function isAvailable() As Boolean
            If sim.RunicPower.CheckMax(9) Then Return False

            If CD <= sim.TimeStamp Then
                Return True
            Else
                Return False
            End If
        End Function

        Overrides Sub use()
            CD = sim.TimeStamp + 20 * 100
            MyBase.Use()
            HitCount = HitCount + 1
            UseGCD(sim.TimeStamp)
            sim.CombatLog.write(sim.TimeStamp & vbTab & "Horn used")
        End Sub

    End Class
End Namespace
