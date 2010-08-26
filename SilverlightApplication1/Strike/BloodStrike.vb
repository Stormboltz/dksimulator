
Friend Class BloodStrike
    Inherits Strikes.Strike

    Sub New(ByVal S As Sim)
        MyBase.New(S)
        BaseDamage = 611.2
        If sim.Sigils.DarkRider Then BaseDamage += 90
        Coeficient = 0.8
        Multiplicator = 1
        If sim.Character.Glyph.BloodStrike Then Multiplicator = Multiplicator * (1.2)
        Multiplicator = Multiplicator * (1 + sim.Character.Talents.Talent("BloodoftheNorth").Value * 5 / 100)
        If sim.MainStat.T92PTNK = 1 Then Multiplicator = Multiplicator * 1.05

    End Sub



    Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
        If OffHand = False Then
            UseGCD(T)
            If sim.proc.ThreatOfThassarian.TryMe(T) Then sim.OHBloodStrike.ApplyDamage(T)
        End If
        If MyBase.ApplyDamage(T) = False Then Return False
        If OffHand = False Then
            sim.RunicPower.add(10)
            sim.Runes.UseBlood(T, True)
        End If
        sim.proc.TryOnBloodStrike()
        Return True
    End Function

    Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
        Dim tmp As Double = MyBase.AvrgNonCrit(T, target)
        If sim.MainStat.T84PDPS = 1 Then
            tmp = tmp * (1 + 0.1 * target.NumDesease * 1.2)
        Else
            tmp = tmp * (1 + 0.1 * target.NumDesease)
        End If

        Return tmp
    End Function

    Public Overrides Sub Merge()
        If sim.MainStat.DualW = False Then Exit Sub
        total += sim.OHBloodStrike.total
        TotalHit += sim.OHBloodStrike.TotalHit
        TotalCrit += sim.OHBloodStrike.TotalCrit
        MissCount = (MissCount + sim.OHBloodStrike.MissCount) / 2
        HitCount = (HitCount + sim.OHBloodStrike.HitCount) / 2
        CritCount = (CritCount + sim.OHBloodStrike.CritCount) / 2
        sim.OHBloodStrike.total = 0
        sim.OHBloodStrike.TotalHit = 0
        sim.OHBloodStrike.TotalCrit = 0
    End Sub

End Class