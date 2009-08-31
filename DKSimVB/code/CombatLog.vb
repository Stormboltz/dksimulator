'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 19:47
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module CombatLog
	Private txtFile As System.IO.TextWriter
	Friend enable As Boolean
	Friend LogDetails as Boolean
	Sub Init()
		if enable then	txtFile = new System.IO.StreamWriter("Combatlog/Combatlog" & sim.EPStat &" _" & now.Day & now.Hour & now.Minute & now.Second & ".txt")
	End Sub
	Sub write(s As String)
		if enable then txtFile.WriteLine(RuneState & vbtab & s)
	End sub
	Sub finish()
		if enable then txtFile.Close
	End Sub
	Function RuneState() As String
		Dim T As Long
		T = sim.TimeStamp
		Dim tmp As String
		tmp = "["
		
		If Rune1.AvailableTime <= T Then
			If Rune1.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "B"
			End If
		Else
			tmp = tmp & int(-(T - Rune1.AvailableTime)/100)
		End If
		If Rune2.AvailableTime <= T  Then
			If Rune2.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "B"
			End If
		Else
			tmp = tmp & int(-(T - Rune2.AvailableTime)/100)
		End If
		If Rune3.AvailableTime <= T Then
			If Rune3.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "F"
			End If
		Else
			tmp = tmp & int(-(T - Rune3.AvailableTime)/100)
			'debug.Print ("Rune3.AvailableTime:" & (Rune3.AvailableTime)/100)
		End If
		If Rune4.AvailableTime <= T Then
			If Rune4.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "F"
			End If
		Else
			tmp = tmp & int(-(T - Rune4.AvailableTime)/100)
			'debug.Print ("Rune4.AvailableTime:" & (Rune4.AvailableTime)/100)
		End If
		If Rune5.AvailableTime <= T Then
			If Rune5.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "U"
			End If
		Else
			tmp = tmp & int(-(T - Rune5.AvailableTime)/100)
		End If
		If Rune6.AvailableTime <= T Then
			If Rune6.death = True Then
				tmp = tmp & "D"
			Else
				tmp = tmp & "U"
			End If
		Else
			tmp = tmp & int(-(T - Rune6.AvailableTime)/100)
		End If
		
		tmp = tmp & "]"
		return tmp
	End Function
End Module
