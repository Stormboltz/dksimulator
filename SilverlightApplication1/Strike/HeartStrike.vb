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
            BaseDamage = 368
            If sim.Sigils.DarkRider Then BaseDamage = BaseDamage + 90
            Coeficient = 1
            Multiplicator = (1 + sim.Character.Talents.Talent("BloodoftheNorth").Value * 5 / 100)
            If sim.Character.T102PDPS <> 0 Then Multiplicator = Multiplicator * 1.07
            If sim.Character.Glyph("HeartStrike") Then Multiplicator += 1.3
            If sim.Character.T92PTNK = 1 Then Multiplicator = Multiplicator * 1.05

            Resource = New Resource(S, ResourcesEnum.BloodOrDeathRune, False, 10)
            logLevel = LogLevelEnum.Basic
        End Sub
        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            If Not OffHand Then UseGCD()
            If MyBase.ApplyDamage(T) = False Then
                UseAlf()
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
                If Multiplicator <= 0 Then Exit For
            Next
            Multiplicator = tmp

            If sim.DRW.IsActive(T) Then sim.DRW.DRWHeartStrike()
            Return True
        End Function
    End Class
End Namespace