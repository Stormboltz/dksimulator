Public Class FesteringStrike
    Inherits Strikes.Strike
    Sub New(ByVal S As Sim)
        MyBase.New(s)
    End Sub

    'Festering Strike *New* - An instant attack that deals 150% weapon damage 
    'plus 840.54 and increases the duration of your Blood Plague, Frost Fever, 
    'and Chains of Ice effects on the target by up to 6 sec. / 5 yd range, Blood+Frost

    public Overrides Function ApplyDamage(T As long) As boolean
        Dim RNG As Double
        UseGCD(T)

        If DoMyStrikeHit() = False Then
            sim.CombatLog.write(T & vbTab & "FeS fail")
            MissCount = MissCount + 1
            Return False
        End If
        Sim.RunicPower.add(10)
        Dim intCount As Integer = 0
        Dim Tar As Targets.Target

        RNG = RngCrit
        Dim dégat As Integer
        If RNG <= CritChance() Then
            CritCount = CritCount + 1
            dégat = AvrgCrit(T, Tar)
            TotalCrit += dégat
            sim.CombatLog.write(T & vbTab & "FeS crit for " & dégat)
        Else
            HitCount = HitCount + 1
            dégat = AvrgNonCrit(T)
            TotalHit += dégat
            sim.CombatLog.write(T & vbTab & "FeS hit for " & dégat)
        End If

        total = total + dégat
        sim.proc.TryOnMHHitProc()
        sim.Targets.MainTarget.BloodPlague.FadeAt = sim.Targets.MainTarget.BloodPlague.FadeAt + 6 * 100
        sim.Targets.MainTarget.FrostFever.FadeAt = sim.Targets.MainTarget.FrostFever.FadeAt + 6 * 100

        sim.Runes.UseBF(T, True)

        Return True
    End Function
    Public Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
        Dim tmp As Double
        tmp = sim.MainStat.NormalisedMHDamage * 1.5
        tmp = tmp + 840
        tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(T)
        AvrgNonCrit = tmp
    End Function

    Public Overrides Function CritCoef() As Double
        CritCoef = 1
        CritCoef = CritCoef * (1 + 0.06 * sim.mainstat.CSD)
    End Function
    Public Overrides Function CritChance() As Double
        CritChance = sim.MainStat.crit
    End Function
    Public Overrides Function AvrgCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
        AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
    End Function
End Class
