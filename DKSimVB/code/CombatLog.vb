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
	sub write(s As String)
		if enable then txtFile.WriteLine(s)
	End sub
	Sub finish()
		if enable then txtFile.Close
	End Sub
End Module
