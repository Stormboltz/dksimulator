Friend Class Necrosis
	Inherits Spells.Spell
	
	Sub New(S As sim)
		MyBase.New()
		Sim = S
	End Sub
	
	Function Apply(Damage As Double, T As long) As Double
		Dim tmp As Double
		tmp  = Damage * 0.04 * TalentUnholy.Necrosis
		'tmp  = tmp  * mainstat.StandardMagicalDamageMultiplier(T)
		'tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		total = total + tmp
		HitCount = HitCount + 1
		if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Necrosis hit for " & tmp)
		return tmp
	End Function
	
End Class