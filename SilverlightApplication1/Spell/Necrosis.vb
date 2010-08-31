Friend Class Necrosis
	Inherits Strikes.Strike
	
	Sub New(S As sim)
		MyBase.New(s)
        HasteSensible = True
        Coeficient = 0.1 * sim.Character.Talents.Talent("Necrosis").Value
        logLevel = LogLevelEnum.Detailled
    End Sub
	
	Function Apply(Damage As Double, T As long) As Double
		Dim tmp As Double
        tmp = Damage * Coeficient
		tmp = tmp * (1-15/(510+15)) 'Partial Resistance. It's about 0,029% less damage on average.
		total = total + tmp
		HitCount = HitCount + 1
		if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Necrosis hit for " & tmp)
		return tmp
	End Function
	
	Public Overrides Sub Merge()
		If sim.MainStat.DualW = false Then exit sub
		Total += sim.OHNecrosis.Total
		TotalHit += sim.OHNecrosis.TotalHit
        HitCount = (HitCount + sim.OHNecrosis.HitCount) / 2
        sim.OHNecrosis.total = 0
		sim.OHNecrosis.TotalHit = 0

	End Sub
End Class