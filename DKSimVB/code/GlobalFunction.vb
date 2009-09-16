'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 14/09/2009
' Heure: 23:19
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Module GlobalFunction
	Function GetFilePath(s As String) As String
		on error resume next
		s = strings.Right(s,s.Length-InStr(s,"(") )
		s = strings.Left(s, InStrRev(s,")")-1 )
		return s
	End Function
	
	Function toDecimal(d As Double) As Decimal
		try
			Return d.ToString (".#")
		Catch
		End Try
	End Function
	
	Function toDDecimal(d As Double) As Decimal
		try
			Return d.ToString (".##")
		Catch
		End Try
	End Function
	
	Sub WriteReport(txt As String)
		Dim Tw As System.IO.TextWriter
		'On Error Resume Next
		
		Tw  =system.IO.File.appendText(ReportPath )
		tw.WriteLine(txt & "<br>")
		tw.Close
		
		_MainFrm.webBrowser1.Navigate(ReportPath)
		'Dim doc As HtmlDocument
		Application.DoEvents
		
		'MThreading problem
		'SimConstructor._MainFrm.webBrowser1.Document.Window.ScrollTo(0,32767)
'		doc = SimConstructor._MainFrm.webBrowser1.Document
'		'doc.Body.ScrollTop = Integer.MaxValue
'		doc.Window.ScrollTo(0,32767)
		'_MainFrm.webBrowser1.Select
	End Sub
	
	
End Module
