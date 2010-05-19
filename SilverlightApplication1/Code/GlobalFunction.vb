Imports System.IO.IsolatedStorage
Imports System.Xml.Linq
Imports System.IO

Module GlobalFunction
    Public RNGSeeder As Integer
    Public ReportPath As String

    Sub msgBox(ByVal s As String)
        Try
            MessageBox.Show("s")
        Catch ex As Exception
            Diagnostics.Debug.WriteLine(s)
        End Try


    End Sub

    Function toDecimal(ByVal d As Double) As Decimal
        Dim dec As Decimal
        Try
            dec = Convert.ToDecimal(d)
            Return Decimal.Round(dec, 1)
        Catch
            Return 0
        End Try
    End Function

    Function toDDecimal(ByVal d As Double) As Decimal
        Dim dec As Decimal
        Try
            dec = Convert.ToDecimal(d)
            Return Decimal.Round(dec, 2)
        Catch
            Return 0
        End Try
    End Function

    Sub WriteReport(ByVal txt As String)
        Dim Tw As System.IO.TextWriter
        'On Error Resume Next
        Tw = System.IO.File.AppendText(ReportPath)
        Tw.WriteLine(txt & "<br>")
        Tw.Close()
        '_MainFrm.webBrowser1.Navigate(ReportPath)

    End Sub

    Function ShortenName(ByVal s As String) As String
        Try
            Dim i As Integer = InStrRev(s, ".")
            Return Strings.Mid(s, i + 1)
        Catch ex As Exception
            Return s
        End Try
        
    End Function

    Function GetIdFromGlyphName(ByVal s As String) As String
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/config/template.xml", FileMode.Open, isoStore)
                Dim doc As XDocument = XDocument.Load(isoStream)
                Try
                    Return doc.Element("Talents").Element("Glyphs").Element(s).Value
                Catch ex As Exception
                    Return "0"
                End Try
            End Using
        End Using
    End Function

    Sub initReport()
        ReportPath = "report.html"
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("report.html", FileMode.Create, isoStore)
                Dim Tw As StreamWriter = New StreamWriter(isoStream)
                Tw.WriteLine("<hmtl style='font-family:Verdana; font-size:10px;'><body>")
                Tw.Flush()
                Tw.Close()
            End Using
        End Using
        
    End Sub

    Function GetHigherValueofThisCollection(ByVal collec As Collection) As Integer
        Dim i As Integer
        Dim tmp As Integer
        tmp = collec.Item(1)
        For Each i In collec
            If i > tmp Then
                tmp = i
            End If
        Next
        Return tmp
    End Function

    Function GetLowerValueofThisCollection(ByVal collec As Collection) As Integer
        Dim i As Integer
        Dim tmp As Integer
        tmp = collec.Item(1)
        For Each i In collec
            If i < tmp Then
                tmp = i
            End If
        Next
        Return tmp

    End Function
    Function ConvertToInt(ByVal S As String) As Integer
        Dim tmp As Double
        For i As Integer = 0 To S.Length - 1
            tmp += Char.GetNumericValue(S.Chars(i))
        Next
        Return Integer.Parse(tmp)
    End Function
End Module
