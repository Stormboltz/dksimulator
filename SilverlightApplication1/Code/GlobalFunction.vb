Imports System.IO.IsolatedStorage
Imports System.Xml.Linq
Imports System.IO

Module GlobalFunction
    Public RNGSeeder As Integer
    Public ReportPath As String

    Sub msgBox(ByVal s As String)

        MessageBox.Show(s)
    End Sub

    Function toDecimal(ByVal d As Double) As Decimal
        Try
            Return d.ToString(".#")
        Catch
            Return 0
        End Try
    End Function

    Function toDDecimal(ByVal d As Double) As Decimal
        Try
            Return d.ToString(".##")
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
        Return s.Replace("DKSIMVB.", "")
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
        Dim Tw As System.IO.TextWriter
        ReportPath = System.IO.Path.GetTempFileName
        Tw = System.IO.File.AppendText(ReportPath)
        Tw.WriteLine("<hmtl style='font-family:Verdana; font-size:10px;'><body>")
        Tw.Flush()
        Tw.Close()
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
