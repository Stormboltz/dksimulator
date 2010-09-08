Partial Public Class UserInput
    Inherits ChildWindow

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
    End Sub

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles CancelButton.Click
        Me.DialogResult = False
    End Sub

    
    
    Private Sub txtInput_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles txtInput.KeyDown
        If e.Key = Key.Enter Then
            Me.DialogResult = True
        End If
        If e.Key = Key.Escape Then
            Me.DialogResult = False
        End If
    End Sub
End Class
