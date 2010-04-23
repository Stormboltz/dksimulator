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
		If sim.Character.talentunholy.RageofRivendare>0 Then
			If FadeAt <= sim.Runes.GetNextUnholy(T) Then
				Sim.FrostFever.ToReApply = true
				Return True
			End If
		Else
			'if sim.Runes.UnholyOnly(T)=false then return false
			if isActive(T) = false then return true
		End If
		return false
	End Function
	
End Class