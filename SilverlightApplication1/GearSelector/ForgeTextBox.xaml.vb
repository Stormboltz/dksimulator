Partial Public Class ForgeTextBox
    Inherits UserControl
    Friend MaxValue As Integer
    Friend Value As Integer
    Event ValueUpdated(ByVal NewValue As Integer)

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub btMinus_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btMinus.Click
        Value = Math.Max(Value - 1, 0)
        Update()
    End Sub

    Private Sub btPlus_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btPlus.Click
        Value = Math.Min(Value + 1, MaxValue)
        Update()
    End Sub
    Sub Update()
        Value = Math.Min(Value, MaxValue)
        Value = Math.Max(Value, 0)
        txtValue.Text = Value
        RaiseEvent ValueUpdated(Value)
    End Sub

    Private Sub txtValue_TextChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.TextChangedEventArgs) Handles txtValue.TextChanged
        Dim i As Integer
        If Integer.TryParse(txtValue.Text, i) Then
            Value = i
            Update()
        Else
            Update()
        End If
    End Sub
End Class
