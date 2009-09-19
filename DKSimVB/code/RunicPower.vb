Friend Class RunicPower
	Friend Value As Integer
	
	Protected sim as Sim
	Sub New(S As Sim)
		Sim = S
		Value = 0
	End Sub
	
	
	Sub add(i As Integer)
		sim.Threat = sim.Threat  + i*5
		Value = i + Value
		'debug.Print ("RP= " & Value)
		If Value > MAxValue Then Value =  MaxValue
		if sim.combatlog.LogDetails then sim.combatlog.write(sim.TimeStamp & vbtab & "Runic Power = " & Value)
	End Sub
	Function MaxValue as Integer
		return 100 + ( 15*talentfrost.RPM)
	End Function
	
	
end Class