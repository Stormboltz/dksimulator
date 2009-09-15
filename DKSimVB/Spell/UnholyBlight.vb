Friend Class UnholyBlight
	inherits Spells.Spell
	
	Sub New(S As sim)
		MyBase.New()
		Sim = S
	End Sub
	
	
	Function Apply(T As Long,damage As Integer) As Double
		Dim dégat As Integer
		dégat = damage * 0.2
		
		If sim.glyph.UnholyBlight Then dégat = dégat * 1.4
		HitCount = HitCount + 1
		total = total + dégat
		combatlog.write(T  & vbtab &  "UnholyBlight hit for " &  dégat )	
	End Function
End Class
