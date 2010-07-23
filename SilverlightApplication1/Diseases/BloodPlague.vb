NameSpace Diseases
	Friend Class BloodPlague
		Inherits Diseases.Disease
		
		
		
		Sub New(S As sim)
			MyBase.New(S)
		End Sub
		
		Overrides Function CalculateCritChance(T As Long) As Double
			If sim.MainStat.T94PDPS = 1 Then Return sim.MainStat.crit
			return 0.0
		End Function
		
		
		Overrides Function PerfectUsage(T As Long) As Boolean

            If FadeAt <= sim.Runes.GetNextUnholy(T) Then
                sim.Targets.MainTarget.FrostFever.ToReApply = True
                Return True
            End If
            Return False
		End Function
		Public Overloads Overrides Sub Merge()
			If Me.Equals(sim.Targets.MainTarget.BloodPlague) = False Then
				With sim.Targets.MainTarget.BloodPlague
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