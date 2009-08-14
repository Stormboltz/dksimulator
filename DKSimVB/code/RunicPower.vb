Friend module RunicPower
	
	Friend Value As Integer
	
	Sub add(i As Integer)
		Threat = Threat  + i*5
		Value = i + Value
		'debug.Print ("RP= " & Value)
		If Value > MAxValue Then Value =  MaxValue
		if combatlog.LogDetails then CombatLog.write(sim.TimeStamp & vbtab & "Runic Power = " & Value)
	End Sub
	Function MaxValue as Integer
		return 100 + ( 15*talentfrost.RPM)
	End Function
	
	
end module