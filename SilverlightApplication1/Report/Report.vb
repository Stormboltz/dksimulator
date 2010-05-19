Imports System.IO.IsolatedStorage
Imports System.IO
Imports System.Xml.Linq

Public Class Report
    Friend Lines As New List(Of ReportLine)
    Friend AdditionalInfos As New List(Of AdditionalInfo)

    Friend Sub AddLine(ByVal Line As ReportLine)
        If IsNothing(Line) = False Then
            Lines.Add(Line)
        End If
    End Sub
    Sub AddAdditionalInfo(ByVal Caption As String, ByVal Text As String)
        Dim AI As New AdditionalInfo(Caption, Text)
        AdditionalInfos.Add(AI)
    End Sub
    Sub Save(ByVal path As String)
        If path = "" Then path = "Report.xml"
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Report/" & path, FileMode.Create, isoStore)
                Dim doc As XDocument = XDocument.Parse("<Table></Table>")
                For Each Line In Lines
                    Dim xEl As XElement = XElement.Parse(Line.InnerText)
                    doc.Element("Table").Add(xEl)
                Next

                For Each AdditionalInfo In AdditionalInfos
                    Try
                        Dim xEl As XElement = XElement.Parse(AdditionalInfo.InnerText)
                        doc.Element("Table").Add(xEl)
                    Catch ex As Exception
                        Diagnostics.Debug.WriteLine(AdditionalInfo.InnerText)
                    End Try

                    

                Next

                doc.Save(isoStream)
                isoStream.Close()
            End Using
        End Using
    End Sub
    Class AdditionalInfo
        Property Caption As String
        Property Text As String
        Sub New(ByVal Cap As String, ByVal T As String)
            Caption = Cap
            Text = T
            Try
                Dim xEl As XElement = XElement.Parse(InnerText)
            Catch ex As Exception
                Diagnostics.Debug.WriteLine("ISSUE with: " & InnerText())
            End Try
        End Sub
        Function InnerText() As String
            Dim tmp As String = ""
            tmp = "<AdditionalInfo "
            tmp += "Caption='" & Caption & "' "
            tmp += "Text='" & Text & "' >"
            tmp += "</AdditionalInfo>"
            Return tmp
        End Function
    End Class
End Class


