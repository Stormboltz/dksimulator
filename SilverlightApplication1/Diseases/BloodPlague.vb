Namespace Simulator.WowObjects.Diseases
    Friend Class BloodPlague
        Inherits Diseases.Disease



        Sub New(ByVal S As Sim)
            MyBase.New(S)
        End Sub

        Overrides Function CalculateCritChance(ByVal T As Long) As Double
            If Sim.Character.T94PDPS = 1 Then Return Sim.Character.crit
            Return 0.0
        End Function


        Overrides Function PerfectUsage(ByVal T As Long) As Boolean
            If FadeAt <= Sim.Runes.GetNextUnholy(T) Then
                ToReApply = True
                Return True
            End If
            Return False
        End Function
        Public Overloads Overrides Sub Merge()
            If Me.Equals(Sim.Targets.MainTarget.BloodPlague) = False Then
                With Sim.Targets.MainTarget.BloodPlague
                    .Total += Total
                    .TotalHit += TotalHit
                    .TotalCrit += TotalCrit
                    .HitCount += HitCount
                    .CritCount += CritCount
                End With
                Total = 0
                TotalHit = 0
                TotalCrit = 0
                HitCount = 0
                CritCount = 0
            End If
        End Sub
    End Class
End Namespace