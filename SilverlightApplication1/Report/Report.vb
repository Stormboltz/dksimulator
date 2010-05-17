Imports System.IO.IsolatedStorage
Imports System.IO
Imports System.Xml.Linq

Public Class Report
    Friend Lines As New List(Of ReportLine)


    Friend Sub AddLine(ByVal Line As ReportLine)
        If IsNothing(Line) = False Then
            Lines.Add(Line)
        End If
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
                doc.Save(isoStream)
                isoStream.Close()
            End Using
        End Using
    End Sub
End Class

