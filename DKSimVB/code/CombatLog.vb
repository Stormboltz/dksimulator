'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 19:47
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Module CombatLog
	Private txtFile As System.IO.TextWriter
	Friend enable As Boolean
	Friend LogDetails As Boolean

	Sub Init()
		txtFile = new System.IO.StreamWriter("Combatlog/Combatlog" &" _" & now.Day & now.Hour & now.Minute & now.Second & ".txt")
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
	
	Sub initReport
		Dim Tw As System.IO.TextWriter
		
		ReportPath = System.IO.Path.GetTempFileName
		Tw  =system.IO.File.appendText(ReportPath )
		tw.WriteLine("<hmtl style='font-family:Verdana; font-size:10px;'><body>")
		tw.Flush
		tw.Close
		
	End Sub
	
	
End Module
