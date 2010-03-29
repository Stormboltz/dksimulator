Friend Class FrostFever
	inherits Diseases.Disease

	Sub New(S As sim)
		MyBase.New(S)
	End Sub
	Overrides Function PerfectUsage(T As Long) As Boolean
		
		If sim.TalentFrost.TundraStalker > 0 Then
			If FadeAt <= sim.Runes.GetNextFrost(T) Then
				Sim.BloodPlague.ToReApply = true
				Return True
			End If
		Else
			if isActive(T) = false then return true
		End If
		return false
	End Function

	Overrides Function CalculateMultiplier(T As Long) As Double
		Dim tmp As Double
		tmp = MyBase.CalculateMultiplier(T) *(1+sim.RuneForge.RazorIceStack/100) 'TODO: only on main target
		If sim.glyph.IcyTouch Then tmp = tmp * 1.2
		return tmp
	End Function
	
	Overrides Function Refresh(T As Long) As Boolean
		sim.proc.IcyTalons.TryMe(T)
		Mybase.Refresh(T)
	End Function

End Class