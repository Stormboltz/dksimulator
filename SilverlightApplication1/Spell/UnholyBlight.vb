Friend Class UnholyBlight
	inherits Spells.Spell
	
	Sub New(S As sim)
		MyBase.New(s)
	End Sub
	
	
	Function Apply(T As Long,damage As Integer) As Double
		Dim d�gat As Integer
		d�gat = damage * 0.1

		If sim.character.glyph.UnholyBlight Then d�gat = d�gat * 1.4
		HitCount = HitCount + 1
		total = total + d�gat
		if sim.CombatLog.LogDetails then sim.combatlog.write(T  & vbtab &  "UnholyBlight hit for " &  d�gat )
	End Function
End Class
