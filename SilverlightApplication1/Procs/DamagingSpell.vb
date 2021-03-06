﻿Namespace Simulator.WowObjects.Procs
    Public Class DamagingSpell
        Inherits Effect

        Sub New(ByVal s As Sim)
            MyBase.New(s)
        End Sub

        Public Overrides Sub SoftReset()
            MyBase.SoftReset()
            Fade()
        End Sub

        Overrides Sub Apply()
            TimeWasted.Start()
            MyBase.Apply()
            Dim T As Long = sim.TimeStamp
            If Currentstack > 1 Then sim.FutureEventManager.Remove(FutureEvent)
            FutureEvent = sim.FutureEventManager.Add(T + (Lenght * 100), "BuffFade", Me)

            AddUptime(T)
            TimeWasted.Pause()
        End Sub

        Overrides Sub Fade()
            MyBase.Fade()
            TimeWasted.Start()
            RemoveUptime(sim.TimeStamp)
            Currentstack = 0
            TimeWasted.Pause()
        End Sub




    End Class
    Class DamagingSpellManager
        Friend DamagingSpells As List(Of DamagingSpell)

        Sub FadeAll()
            For Each SB In DamagingSpells
                SB.Fade()
            Next
        End Sub


    End Class
End Namespace

