NameSpace Diseases
	Friend Class FrostFever
		inherits Diseases.Disease
		
		Sub New(S As sim)
			MyBase.New(S)
		End Sub
		Overrides Function PerfectUsage(T As Long) As Boolean
			
			If sim.Character.talentfrost.TundraStalker > 0 Then
				If FadeAt <= sim.Runes.GetNextFrost(T) Then
					sim.Targets.MainTarget.BloodPlague.ToReApply = true
					Return True
				End If
			Else
				if isActive(T) = false then return true
			End If
			return false
		End Function
		
		Overrides Function CalculateMultiplier(T As Long,target As Targets.Target) As Double
			if target is nothing then target = sim.Targets.MainTarget
			Dim tmp As Double
			tmp = MyBase.CalculateMultiplier(T,target) * sim.RuneForge.RazorIceMultiplier(T) 'TODO: only on main target
			If sim.character.glyph.IcyTouch Then tmp = tmp * 1.2
			return tmp
		End Function
		
		Overrides Function Refresh(T As Long) As Boolean
			sim.proc.IcyTalons.TryMe(T)
			Mybase.Refresh(T)
		End Function
		Public Overloads Overrides Sub Merge()
			If Me.Equals(sim.Targets.MainTarget.FrostFever) = False Then
				With sim.Targets.MainTarget.FrostFever
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