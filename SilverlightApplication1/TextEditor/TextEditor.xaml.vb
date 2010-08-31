Imports System.IO.IsolatedStorage
Imports System.IO

Partial Public Class TextEditor
    Inherits ChildWindow

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
    End Sub


    Sub OpenFileFromISO(ByVal path As String)
        Try

            Dim isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Dim isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream(path, FileMode.OpenOrCreate, FileAccess.Read, isoStore)
            Dim myReader As StreamReader = New StreamReader(isoStream)
            TextBox1.Text = myReader.ReadToEnd
            myReader.Close()
            isoStream.Close()
            isoStore.Dispose()
        Catch ex As Exception
            msgBox("Error trying to open the file")
        End Try
    End Sub
    Sub OpenFileFromXAP(ByVal path)

    End Sub


End Class
