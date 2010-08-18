Partial Public Class RuneForgeTextBox
    Inherits UserControl
    Friend MaxValue As Integer
    Friend Value As Integer
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub btMinus_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btMinus.Click
        Value = Math.Max(Value - 1, 0)
    End Sub

    Private Sub btPlus_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btPlus.Click
        Value = Math.Min(Value + 1, MaxValue)
    End Sub
End Class
