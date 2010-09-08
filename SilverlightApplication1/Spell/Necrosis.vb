Namespace Simulator.WowObjects.Strikes



    Friend Class Necrosis
        Inherits Strikes.Strike

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            HasteSensible = True
            Coeficient = 0.1 * Sim.Character.Talents.Talent("Necrosis").Value
            logLevel = LogLevelEnum.Detailled
        End Sub

        Function Apply(ByVal Damage As Double, ByVal T As Long) As Double
            Dim tmp As Double
            tmp = Damage * Coeficient
            tmp = tmp * (1 - 15 / (510 + 15)) 'Partial Resistance. It's about 0,029% less damage on average.
            total = total + tmp
            HitCount = HitCount + 1
            If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(T & vbTab & "Necrosis hit for " & tmp)
            Return tmp
        End Function

        Public Overrides Sub Merge()
            If Sim.Character.DualW = False Then Exit Sub
            total += Sim.OHNecrosis.total
            TotalHit += Sim.OHNecrosis.TotalHit
            HitCount = (HitCount + Sim.OHNecrosis.HitCount) / 2
            Sim.OHNecrosis.total = 0
            Sim.OHNecrosis.TotalHit = 0

        End Sub
    End Class
End Namespace
