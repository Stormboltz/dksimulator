Imports System.IO.IsolatedStorage
Imports System.IO

Partial Public Class mytextEditor
    Inherits ChildWindow

    Public Sub New()
        InitializeComponent()
    End Sub

    Friend Folder As String
    Friend FileName As String
    Friend WithEvents myUserImput As UserInput

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles cmdOk.Click
        Dim UsrImput As New UserInput
        myUserImput = UsrImput
        myUserImput.Show()

    End Sub

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles cmdCancel.Click
        Me.DialogResult = False
    End Sub



    Private Sub myUserImput_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles myUserImput.Closing
        If myUserImput.DialogResult Then
            FileName = myUserImput.txtInput.Text & ".xml"
            SaveThisFile()
            Me.DialogResult = True
        Else
            Me.DialogResult = False
        End If
        
    End Sub

    Sub SaveThisFile()
        Dim path As String = Folder & "/" & FileName
        Try
            Dim isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Dim isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream(path, FileMode.Create, isoStore)
            Dim myWritter As StreamWriter = New StreamWriter(isoStream)
            myWritter.Write(txtEdit.Text)
            myWritter.Close()
            isoStream.Close()
            isoStore.Dispose()
        Catch ex As Exception
            msgBox("Error trying to save the file")
        End Try


    End Sub

End Class
