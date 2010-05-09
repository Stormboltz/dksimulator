Partial Public Class frmArmoryImport
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

    Private Sub ChildWindow_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        cmbRegion.Items.Clear()
        cmbRegion.Items.Add("US")
        cmbRegion.Items.Add("EU")
        cmbRegion.Items.Add("CN")
        cmbRegion.Items.Add("KR")
        cmbRegion.Items.Add("TW")
    End Sub
End Class
