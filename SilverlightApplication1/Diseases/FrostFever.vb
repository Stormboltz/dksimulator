Namespace Simulator.WowObjects.Diseases
    Friend Class FrostFever
        Inherits Diseases.Disease

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            DamageSchool = DamageSchoolEnum.Frost
        End Sub
        Overrides Function PerfectUsage(ByVal T As Long) As Boolean
            If FadeAt > T + 300 Then
                Return False
            Else
                Return True
            End If

            'If FadeAt > T + 1000 Then Return False
            'If FadeAt <= sim.Runes.GetNextFrost(T) Then
            '    ToReApply = True
            '    Return True
            'End If
            'Return False
        End Function

        Overrides Function CalculateMultiplier(ByVal T As Long, ByVal target As Targets.Target) As Double
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            tmp = MyBase.CalculateMultiplier(T, target) * Sim.RuneForge.RazorIceMultiplier(T) 'TODO: only on main target
            If sim.Character.Glyph("IcyTouch") Then tmp = tmp * 1.2
            If sim.Character.Talents.GetNumOfThisSchool(Character.Talents.Schools.Frost) > 20 Then
                tmp = tmp * 1.2 'Frozen Heart
                tmp *= 1 + sim.Character.Mastery.Value * 2.5
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