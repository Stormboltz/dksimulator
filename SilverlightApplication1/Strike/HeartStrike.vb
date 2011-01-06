'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 23:11
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Strikes
    Friend Class HeartStrike
        Inherits Strike
        Sub New(ByVal S As Sim)
            MyBase.New(S)
                BaseDamage = 819
            Coeficient = 1

            If sim.Character.T92PTNK = 1 Then Coeficient += 0.05
            If sim.Character.T102PDPS <> 0 Then Coeficient += 0.07
            If sim.Character.Glyph("HeartStrike") Then Coeficient += 0.3
            Resource = New Resource(S, Resource.ResourcesEnum.BloodOrDeathRune, False, 10)
            logLevel = LogLevelEnum.Basic
        End Sub
        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            If Not OffHand Then UseGCD()
            If MyBase.ApplyDamage(T) = False Then
                'UseAlf()
                Return False
            End If
            Use()

            sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnBloodStrike)
            Dim tmp As Double = Multiplicator
            For Each Tar As Targets.Target In sim.Targets.AllTargets
                Dim i As Integer = 0
                If Tar.Equals(sim.Targets.CurrentTarget) = False Then
                    i += 1
                    Multiplicator = Math.Max(tmp * (1 - (0.25 * i)), 0)
                    MyBase.ApplyDamage(T)
                End If

                If i > 3 Then Exit For
            Next
            Multiplicator = tmp

            If sim.DRW.IsActive(T) Then sim.DRW.DRWHeartStrike()
            Return True
        End Function
    End Class
End Namespace