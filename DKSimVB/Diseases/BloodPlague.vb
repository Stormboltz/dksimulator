Friend Class BloodPlague
	Inherits Diseases.Disease
	
	Sub New(S As sim)
		MyBase.New()
		Sim = S
	End Sub
	
	overrides Function PerfectUsage(T As Long) As Boolean
		If TalentUnholy.RageofRivendare>0 Then
			if isActive(T+150) = false then return true
		Else
			'if sim.Runes.UnholyOnly(T)=false then return false
			if isActive(T) = false then return true
		End If
		return false
	End Function
		
	overrides Function Apply(T As Long) As Boolean
		AP = sim.MainStat.AP
		DamageTick = AvrgNonCrit(T)
		FadeAt = T + 15 * 100 + 3 * 100 * talentunholy.Epidemic
		nextTick = T + 3 * 100
		sim.pestilence.BPToReapply = False
		ScourgeStrikeGlyphCounter = 0
		CritChance = sim.MainStat.crit
	End Function
	
	overrides Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = 26
		tmp = tmp + 0.055 * (1 + 0.04 * TalentUnholy.Impurity) * AP
		If  sim.Buff.CrypticFever Then
			tmp = tmp * 1.3
		Else
			tmp = tmp * (1 + TalentUnholy.CryptFever * 10 / 100)
		End If
		tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		tmp = tmp * 1.15
		AvrgNonCrit = tmp
	End Function

End Class