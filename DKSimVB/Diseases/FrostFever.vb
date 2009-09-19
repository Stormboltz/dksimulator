Friend Class FrostFever
	inherits Diseases.Disease

	Sub New(S As sim)
		MyBase.New()
		Sim = S
	End Sub
	
	overrides Function PerfectUsage(T As Long) As Boolean
		If Talentfrost.TundraStalker>0 Then
			if isActive(T+150) = false then return true
		Else
			if isActive(T) = false then return true
		End If
		return false
	End Function
	
	overrides Function Apply(T As Long) As Boolean

		AP = sim.MainStat.AP
		DamageTick = AvrgNonCrit(T)
		FadeAt = T + 15 * 100 + 3 * 100 * talentunholy.Epidemic
		nextTick = T + 3 * 100
		sim.pestilence.FFToReapply = False
		ScourgeStrikeGlyphCounter = 0
	End Function
	
	overrides	Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = 26
		tmp = tmp + 0.055 * (1 + 0.04 * TalentUnholy.Impurity) * AP
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		If  sim.Buff.CrypticFever Then
			tmp = tmp * 1.3
		Else
			tmp = tmp * (1 + TalentUnholy.CryptFever * 10 / 100)
		End If
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * 1.15
		'if MHRazorice or (OHRazorice and mainstat.DualW) then tmp = tmp *1.10 'TODO: only affect main target.
		AvrgNonCrit = tmp
	End Function
	
End Class