
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
        logLevel = LogLevelEnum.Basic
        If sim.MainStat.T84PDPS = 1 Then
            DiseaseBonus = 0.1 * 1.2
        Else
            DiseaseBonus = 0.1
        End If


    End Sub



    Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
        If MyBase.ApplyDamage(T) = False Then Return False
        If OffHand = False Then
            UseGCD(T)
            sim.RunicPower.add(15)
            sim.Runes.UseBlood(T, True)
            sim.proc.tryProcs(Procs.ProcOnType.OnBloodStrike)
        End If
        Return True
    End Function

   

End Class