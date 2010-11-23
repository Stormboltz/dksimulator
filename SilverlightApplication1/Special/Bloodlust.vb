
'
Namespace Simulator.WowObjects.Spells
    Public Class Bloodlust

        Friend Cd As Long
        Private ActiveUntil As Long
        Protected Sim As Sim
        Sub New(ByVal S As Sim)
            Sim = S
            Cd = 0
            ActiveUntil = 0

        End Sub

        Function IsAvailable(ByVal T As Long) As Boolean
            If Sim.Character.Buff.Bloodlust = 0 Then Return False
            If Cd <= T Then
                Return True
            Else
                Return False
            End If
        End Function

        Function IsActive(ByVal T As Long) As Boolean
            If ActiveUntil > T Then
                Return True
            Else
                Return False
            End If

        End Function

        Sub use(ByVal T As Long)
            Cd = T + 10 * 60 * 100
            ActiveUntil = T + 4000
        End Sub
    End Class
End Namespace