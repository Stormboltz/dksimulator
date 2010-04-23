Friend Class UnholyBlight
	inherits Spells.Spell
	
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	
	Function Apply(T As Long,damage As Integer) As Double
		Dim dégat As Integer
		dégat = damage * 0.1

		If sim.character.glyph.UnholyBlight Then dégat = dégat * 1.4
		HitCount = HitCount + 1
		total = total + dégat
		if sim.CombatLog.LogDetails then sim.combatlog.write(T  & vbtab &  "UnholyBlight hit for " &  dégat )
	End Function
End Class
