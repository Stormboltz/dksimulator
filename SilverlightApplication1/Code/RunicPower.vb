Friend Class RunicPower
	Private Value As Double
	Friend MaxValue As Integer
	Friend Wasted As Integer
	Friend Total As Integer
	
	Protected sim as Sim
	Sub New(S As Sim)
		Sim = S
		Value = 0
		Total = 0
	End Sub
	
	Sub Reset()
        MaxValue = 100
		Value = 10 'Start fight with some RP
	End Sub
	
	Sub Use(Cost as Integer)
		Value -= Cost
		Total += Cost
	End Sub
	
	Function Check(Cost As Integer) As Boolean
		return Value >= Cost
	End Function
	
	Function Report() As String
        Return "Total runic power used: " & Total & " (" & Wasted & " wasted)"
	End Function
	
	Function CheckRS(Cost As Integer) As Boolean
		If Sim.SaveRPForRS Then
			return Value >= Cost + 20
		Else
			Return Value >= Cost
		End If
	End Function
	
	Function GetValue() As Integer
		return Value
	End Function
	
	Function CheckMax(Delta As Integer) As Boolean
		return Value + Delta >= MaxValue
	End Function
	
	
    Sub add(ByVal i As Double)
        i = i * (1 + sim.FrostPresence / 100)
        sim.Threat = sim.Threat + i * 5
        Value = i + Value
        'Diagnostics.Debug.WriteLine ("RP= " & Value)
        If Value > MaxValue Then
            Wasted += Value - MaxValue
            Value = MaxValue
        End If
        If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "Runic Power = " & Value)
    End Sub
	
	
end Class