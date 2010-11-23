
'
Namespace Simulator.WowObjects.Procs
    Public Class RazorIce
        Inherits Proc

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            'Not Used
        End Sub

        Overrides Sub ApplyMe(ByVal T As Long)
            HitCount += 1
            Me.AddUptime(T)
            If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(Sim.TimeStamp & vbTab & Me.ToString & " proc")
            'sim.RuneForge.AddRazorIceStack(T)
        End Sub
    End Class
End Namespace

