Namespace Simulator.WowObjects.Diseases
    Friend Class FrostFever
        Inherits Diseases.Disease

        Sub New(ByVal S As Sim)
            MyBase.New(S)

        End Sub
        Overrides Function PerfectUsage(ByVal T As Long) As Boolean
            If FadeAt <= Sim.Runes.GetNextFrost(T) Then
                ToReApply = True
                Return True
            End If
            Return False
        End Function

        Overrides Function CalculateMultiplier(ByVal T As Long, ByVal target As Targets.Target) As Double
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            tmp = MyBase.CalculateMultiplier(T, target) * Sim.RuneForge.RazorIceMultiplier(T) 'TODO: only on main target
            If Sim.Character.Glyph.IcyTouch Then tmp = tmp * 1.2
            If sim.Character.Talents.GetNumOfThisSchool(Character.Talents.Schools.Frost) > 20 Then
                tmp = tmp * 1.2 'Frozen Heart
            End If

            Return tmp
        End Function

        Overrides Function Refresh(ByVal T As Long) As Boolean
            MyBase.Refresh(T)
            Return True
        End Function
        Public Overloads Overrides Sub Merge()
            If Me.Equals(Sim.Targets.MainTarget.FrostFever) = False Then
                With Sim.Targets.MainTarget.FrostFever
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