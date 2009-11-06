'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 14/09/2009
' Heure: 23:19
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Module GlobalFunction
	Public RNGSeeder as Integer
	
	
'	
'	Function GetFilePath(s As String) As String
'		on error resume next
'		s = strings.Right(s,s.Length-InStr(s,"(") )
'		s = strings.Left(s, InStrRev(s,")")-1 )
'		return s
'	End Function
'	
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
	
	Function ShortenName(s As String) As String
		return s.Replace("DKSIMVB.","")
	End Function
	
	Function GetIdFromGlyphName(s As String) As String
		dim doc as new Xml.XmlDocument
		doc.Load("template.xml")
		Dim xNode As Xml.XmlNode
		
		xNode = doc.SelectSingleNode("/Talents/Glyphs/" & s)
		
		
		return xNode.InnerText
	End Function
	
	Sub initReport
		Dim Tw As System.IO.TextWriter
		
		ReportPath = System.IO.Path.GetTempFileName
		Tw  = system.IO.File.appendText(ReportPath )
		tw.WriteLine("<hmtl style='font-family:Verdana; font-size:10px;'><body>")
		tw.Flush
		tw.Close
		
	End Sub
	
	Function GetHigherValueofThisCollection(collec As Collection) As Integer
		Dim i As Integer
		Dim tmp As Integer
		tmp = collec.Item(1)
		For Each i In collec
			If i > tmp Then
				tmp = i
			End If
		Next
		return tmp
	End Function
	
	Function GetLowerValueofThisCollection(collec As Collection) As integer
		Dim i As Integer
		Dim tmp As Integer
		tmp = collec.Item(1)
		For Each i In collec
			If i < tmp Then
				tmp = i
			End If
		Next
		return tmp
		
	End Function
	Function ConvertToInt(S As String) As Integer

		Dim tmp As Integer
		Dim charEnum As CharEnumerator
		
		charEnum = s.GetEnumerator
		
		Do 	Until charEnum.MoveNext = False
			tmp = tmp  + Microsoft.VisualBasic.Asc(charEnum.Current)
			
		Loop
		
		
		return tmp
	End Function
	
	
End Module
