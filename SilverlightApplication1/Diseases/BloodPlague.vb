Namespace Simulator.WowObjects.Diseases
    Friend Class BloodPlague
        Inherits Diseases.Disease



        Sub New(ByVal S As Sim)
            MyBase.New(S)
        End Sub

        Overrides Function CalculateMultiplier(ByVal T As Long, ByVal target As Targets.Target) As Double
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            Multiplicator = 1
            tmp = MyBase.CalculateMultiplier(T, target)
            If sim.Character.Talents.MainSpec = (Character.Talents.Schools.Unholy) Then
                If sim.NextPatch Then
                    Multiplicator += sim.Character.Mastery.Value * 2.5
                End If
            End If
            tmp *= Multiplicator
            Return tmp
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