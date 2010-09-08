Namespace Simulator.WowObjects.Spells


    Public Class UnholyPresence
        Inherits Spell

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            logLevel = LogLevelEnum.Detailled
        End Sub


        Function IsAvailable(ByVal T As Long) As Boolean
            If Sim.Runes.Unholy() Then
                Return True
            Else
                Return False
            End If
        End Function


        Function Use(ByVal T As Long) As Boolean
            Sim.BloodPresence = 0
            Sim.UnholyPresence = 1
            Sim.FrostPresence = 0
            Sim.Runes.UseUnholy(T, False)
            Sim.CombatLog.write(T & vbTab & "Switch to Unholy Presence")
            Me.HitCount = Me.HitCount + 1
            Sim._UseGCD(T, 1)
            Return True
        End Function

    End Class
End Namespace