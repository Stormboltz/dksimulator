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

	Sub New()
		txtFile = New System.IO.StreamWriter("Combatlog/Combatlog" &" _" & now.Day & now.Hour & now.Minute & now.Second & ".txt")
		LogDetails = true
		enable = true
	End Sub
	Sub write(s As String)
		on error resume next
		Dim tmp As String
		tmp = ""
		
		 'tmp = 	Sim.Runes.RuneState()
	
			if enable then txtFile.WriteLine(tmp & vbtab & s)

	End sub
	Sub finish()
		if enable then txtFile.Close
	End Sub
End Class
