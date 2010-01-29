'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 19:47
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Class CombatLog
	Private txtFile As System.IO.TextWriter
	Friend enable As Boolean
	Friend LogDetails As Boolean
	Private Sim as Sim
	Sub New(S as Sim)
		'txtFile = New System.IO.StreamWriter("Combatlog/Combatlog" &" _" & now.Day & now.Hour & now.Minute & now.Second & ".txt")
		LogDetails = true
		enable = True
		Sim = S
	End Sub
	
	Sub InitcombatLog
		txtFile = New System.IO.StreamWriter("Combatlog/Combatlog" &" _" & now.Day & now.Hour & now.Minute & now.Second & ".txt")
	End Sub
	
	
	Sub write(s As String)
		
		on error resume next
		Dim tmp As String
		tmp = ""
		
		 tmp = 	Sim.Runes.RuneState()
	
			If enable Then 
				If txtFile is Nothing Then InitcombatLog
				txtFile.WriteLine(tmp & vbtab & s)
			End If

	End sub
	Sub finish()
		on error resume next
		if enable then txtFile.Close
	End Sub
End Class
