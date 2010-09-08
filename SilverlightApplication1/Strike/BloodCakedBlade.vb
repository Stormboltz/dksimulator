'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 18:47
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Strikes
    Friend Class BloodCakedBlade
        Inherits Strike
        Sub New(ByVal S As Sim)
            MyBase.New(S)
            HasteSensible = True
            BaseDamage = 0
            Coeficient = 0.25
            Multiplicator = 1
            logLevel = LogLevelEnum.Detailled
            DiseaseBonus = 0.125
        End Sub

        Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Dim tmp As Double = MyBase.AvrgNonCrit(T, target)
            If Sim.EPStat = "EP HasteEstimated" Then
                tmp *= Sim.Character.EstimatedHasteBonus
            End If
            Return tmp
        End Function

        Public Overrides Function CritChance() As Double
            Return 0
        End Function


        Public Overrides Sub Merge()
            If Sim.Character.DualW = False Then Exit Sub
            total += Sim.OHBloodCakedBlade.total
            TotalHit += Sim.OHBloodCakedBlade.TotalHit
            TotalCrit += Sim.OHBloodCakedBlade.TotalCrit

            MissCount = (MissCount + Sim.OHBloodCakedBlade.MissCount) / 2
            HitCount = (HitCount + Sim.OHBloodCakedBlade.HitCount) / 2
            CritCount = (CritCount + Sim.OHBloodCakedBlade.CritCount) / 2

            Sim.OHBloodCakedBlade.total = 0
            Sim.OHBloodCakedBlade.TotalHit = 0
            Sim.OHBloodCakedBlade.TotalCrit = 0
        End Sub
    End Class
End Namespace