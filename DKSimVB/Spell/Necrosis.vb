Friend Class Necrosis
	Inherits Spells.Spell
	
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	Function Apply(Damage As Double, T As long) As Double
		Dim tmp As Double
		tmp  = Damage * 0.04 * sim.TalentUnholy.Necrosis
		total = total + tmp
		HitCount = HitCount + 1
		if sim.combatlog.LogDetails then sim.combatlog.write(T  & vbtab &  "Necrosis hit for " & tmp)
		return tmp
	End Function
	
End Class