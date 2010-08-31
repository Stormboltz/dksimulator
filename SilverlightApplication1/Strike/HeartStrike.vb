'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 23:11
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class HeartStrike
	Inherits Strikes.Strike
	Sub New(S As sim)
        MyBase.New(S)
        BaseDamage = 368
        If sim.Sigils.DarkRider Then BaseDamage = BaseDamage + 90
        Coeficient = 1
        Multiplicator = (1 + sim.Character.Talents.Talent("BloodoftheNorth").Value * 5 / 100)
        If sim.MainStat.T102PDPS <> 0 Then Multiplicator = Multiplicator * 1.07

        If sim.MainStat.T92PTNK = 1 Then Multiplicator = Multiplicator * 1.05
        logLevel = LogLevelEnum.Basic
	End Sub
    Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean

        If MyBase.ApplyDamage(T) = False Then Return False

        UseGCD(T)
        sim.RunicPower.add(15)
        sim.Runes.UseBlood(T, False)
        sim.proc.tryProcs(Procs.ProcOnType.OnBloodStrike)
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

        If sim.DRW.IsActive(T) Then
            sim.DRW.DRWHeartStrike()
        End If

        Return True
    End Function
End Class
