Imports System.Windows.Media.Imaging

Partial Public Class PrioButton
    Inherits UserControl

    Protected Mainfrm As PriorityEditor
    Friend ElementName As String

    Friend number As Integer
    Public Sub New(ByVal m As PriorityEditor)
        Mainfrm = m
        InitializeComponent()
    End Sub


    Sub SetName(ByVal name As String)

        Try
            Dim bmi As BitmapImage = New BitmapImage(New Uri("../images/spell/" & name & ".jpg", UriKind.Relative))
            Me.ImgButton.Source = bmi
        Catch ex As Exception
            Diagnostics.Debug.WriteLine(ex.Message)
        End Try

        Me.lbl.Content = name
        Me.ElementName = name
    End Sub

    

    Private Sub buttonAdd_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles buttonAdd.Click
        Mainfrm.AddPrio(Me)
    End Sub

    Private Sub buttonRemove_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles buttonRemove.Click
        Mainfrm.RemovePrio(Me)
    End Sub

    Private Sub buttonUp_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles buttonUp.Click
        Mainfrm.MoveUp(Me)
    End Sub

    Private Sub buttonDown_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles buttonDown.Click
        Mainfrm.MoveDown(Me)
    End Sub

    Private Sub PrioButton_DragEnter(ByVal sender As Object, ByVal e As System.Windows.DragEventArgs) Handles Me.DragEnter

    End Sub

    Private Sub PrioButton_DragLeave(ByVal sender As Object, ByVal e As System.Windows.DragEventArgs) Handles Me.DragLeave

    End Sub

    Private Sub PrioButton_DragOver(ByVal sender As Object, ByVal e As System.Windows.DragEventArgs) Handles Me.DragOver

    End Sub

    Private Sub PrioButton_Drop(ByVal sender As Object, ByVal e As System.Windows.DragEventArgs) Handles Me.Drop

    End Sub
End Class
