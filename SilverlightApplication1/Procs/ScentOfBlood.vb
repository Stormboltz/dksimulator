
'
Namespace Simulator.WowObjects.Procs
    Public Class ScentOfBlood
        Inherits Proc
        Sub New(ByVal S As Sim)
            MyBase.New(S)
        End Sub

        Overrides Sub ApplyMe(ByVal T As Long)
            If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(Sim.TimeStamp & vbTab & Me.ToString & " proc")
            Fade = T + ProcLenght * 100
            CurrentStack = MaxStack
            HitCount += 1
        End Sub

    End Class
End Namespace
