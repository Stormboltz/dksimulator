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

	End Sub
    Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
        Dim RNG As Double
        UseGCD(T)
        If DoMyStrikeHit() = False Then
            sim.CombatLog.write(T & vbTab & "HS fail")
            MissCount = MissCount + 1
            Return False
        End If
        sim.RunicPower.add(10)
        Dim intCount As Integer = 0
        Dim Tar As Targets.Target
        For Each Tar In sim.Targets.AllTargets
            RNG = RngCrit
            If Tar.Equals(sim.Targets.MainTarget) Then
                If RNG <= CritChance() Then
                    CritCount = CritCount + 1
                    LastDamage = AvrgCrit(T, Tar)
                    TotalCrit += LastDamage
                    sim.CombatLog.write(T & vbTab & "HS crit for " & LastDamage)
                Else
                    HitCount = HitCount + 1
                    LastDamage = AvrgNonCrit(T)
                    TotalHit += LastDamage
                    sim.CombatLog.write(T & vbTab & "HS hit for " & LastDamage)
                End If
            ElseIf intCount = 0 Then
                intCount = 1
                If RNG <= CritChance() Then
                    CritCount = CritCount + 1
                    LastDamage = AvrgCrit(T, Tar) / 2
                    TotalCrit += LastDamage
                    sim.CombatLog.write(T & vbTab & "HS crit for " & LastDamage)
                Else
                    HitCount = HitCount + 1
                    LastDamage = AvrgNonCrit(T) / 2
                    TotalHit += LastDamage
                    sim.CombatLog.write(T & vbTab & "HS hit for " & LastDamage)
                End If
            End If
            total = total + LastDamage
            sim.proc.TryOnBloodStrike()
            sim.proc.TryOnMHHitProc()
        Next
        sim.Runes.UseBlood(T, False)

        If sim.DRW.IsActive(T) Then
            sim.DRW.DRWHeartStrike()
        End If


        Return True
    End Function
End Class
