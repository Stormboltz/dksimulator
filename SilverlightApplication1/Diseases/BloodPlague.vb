Namespace Simulator.WowObjects.Diseases
    Friend Class BloodPlague
        Inherits Diseases.Disease



        Sub New(ByVal S As Sim)
            MyBase.New(S)
        End Sub

        Overrides Function PerfectUsage(ByVal T As Long) As Boolean
            If FadeAt > T + 300 Then
                Return False
            Else
                Return True
            End If

            If FadeAt < T Then Return True
            Return False

            'If FadeAt > T + 1000 Then Return False
            'If FadeAt <= sim.Runes.GetNextUnholy(T) Then
            '    ToReApply = True
            '    Return True
            'End If
            'Return False

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