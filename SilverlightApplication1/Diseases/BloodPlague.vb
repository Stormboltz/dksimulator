Namespace Simulator.WowObjects.Diseases
    Friend Class BloodPlague
        Inherits Diseases.Disease



        Sub New(ByVal S As Sim)
            MyBase.New(S)
        End Sub

       
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