Friend Class UnholyBlight
	inherits Spells.Spell
	
	Sub New(S As sim)
		MyBase.New()
		Sim = S
	End Sub
	
	
	Function Apply(T As Long,damage As Integer) As Double
		Dim d�gat As Integer
		d�gat = damage * 0.2
		
		If sim.glyph.UnholyBlight Then d�gat = d�gat * 1.4
		HitCount = HitCount + 1
		total = total + d�gat
		combatlog.write(T  & vbtab &  "UnholyBlight hit for " &  d�gat )	
	End Function
End Class
