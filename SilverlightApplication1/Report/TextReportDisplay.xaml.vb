Imports System.IO.IsolatedStorage
Imports System.IO
Imports System.Linq
Imports System.Xml.Linq

Partial Public Class TextReportDisplay
    Inherits ChildWindow

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
    End Sub

    Sub OpenReport(ByVal path As String)

        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream(path, FileMode.OpenOrCreate, FileAccess.Read, isoStore)
                Dim myReader As XDocument = XDocument.Load(isoStream)
                Dim tmp As String = ""
                For Each el In myReader.Element("Table").Elements("AdditionalInfo")
                    tmp += el.Attribute("Caption").Value & " " & el.Attribute("Text").Value & vbCr
                Next
                txtAdditionalInfo.Text = tmp

            End Using
        End Using
    End Sub

End Class
