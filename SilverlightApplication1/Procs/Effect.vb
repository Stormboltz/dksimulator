Namespace Simulator.WowObjects.Procs
    Public Class Effect
        Inherits WowObject

        Protected Value As Double
        Friend Stat As Sim.Stat
        Protected Lenght As Long
        Friend previousFade As Long
        Friend Currentstack As Integer
        Protected MaxStack As Integer = 1
        Protected FutureEvent As FutureEvent
        Friend IsActive As Boolean

        Sub New(ByVal s As Sim)
            MyBase.New(s)
            sim.EffectManager.Effects.Add(Me)
        End Sub
        Public Overrides Sub SoftReset()
            MyBase.SoftReset()
        End Sub


        Overridable Sub Apply()
            HitCount += 1
            IsActive = True
        End Sub
        Overridable Sub Fade()
            IsActive = False
        End Sub
        Sub AddUptime(ByVal T As Long)
            If Not sim.CalculateUPtime Then Return
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
            If Not sim.CalculateUPtime Then Return
            If EpStat <> "" Then Return
            If previousFade < T Then
            Else
                uptime -= (previousFade - T)
            End If
            previousFade = T
        End Sub

    End Class
    Class EffectManager
        Friend Effects As New List(Of Effect)
    End Class
End Namespace

