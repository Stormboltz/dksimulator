Friend Class UnholyBlight
	inherits Spells.Spell
	
	Sub New(S As sim)
		MyBase.New()
		Sim = S
	End Sub
	
	
	Function Apply(T As Long,damage As Integer) As Double
		Dim dégat As Integer
		If sim.BoneShield33 Then
			dégat = damage * 0.1
		Else
			dégat = damage * 0.2
		End If
		If sim.glyph.UnholyBlight Then dégat = dégat * 1.4
		HitCount = HitCount + 1
		total = total + dégat
		if sim.CombatLog.LogDetails then sim.combatlog.write(T  & vbtab &  "UnholyBlight hit for " &  dégat )
	End Function
End Class
