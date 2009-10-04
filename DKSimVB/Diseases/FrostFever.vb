Friend Class FrostFever
	inherits Diseases.Disease

	Sub New(S As sim)
		MyBase.New()
		Sim = S
	End Sub
	
	Overrides Function PerfectUsage(T As Long) As Boolean
		
		If Talentfrost.TundraStalker > 0 Then
			If FadeAt <= sim.Runes.GetNextFrost(T) Then 
				Sim.BloodPlague.ToReApply = true
				Return True
			End If
		Else
			if isActive(T) = false then return true
		End If
		return false
	End Function
	
	overrides Function Apply(T As Long) As Boolean
		ToReApply = false 
		AP = sim.MainStat.AP
		DamageTick = AvrgNonCrit(T)
		FadeAt = T + 15 * 100 + 3 * 100 * talentunholy.Epidemic
		nextTick = T + 3 * 100
		sim.pestilence.FFToReapply = False
		ScourgeStrikeGlyphCounter = 0
		CritChance = sim.MainStat.SpellCrit
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
		if sim.RuneForge.MHRazorice or (sim.RuneForge.OHRazorice and sim.mainstat.DualW)  then tmp = tmp *1.10 'TODO: only on main target
		if sim.glyph.IcyTouchII then tmp = tmp * 1.2
		AvrgNonCrit = tmp
	End Function
	
End Class