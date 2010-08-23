Partial Public Class frmPopReport
    Inherits ChildWindow

    Friend ReportFrame As ReportFrame

    Public Sub New()
        InitializeComponent()

    End Sub

    Public Sub New(ByVal ReportFrame As ReportFrame)
        InitializeComponent()
        Me.ReportFrame = ReportFrame
        dgReport.ItemsSource = ReportFrame.dgReport.ItemsSource
        txtAdditionalInfo.Text = ReportFrame.txtAdditionalInfo.Text
    End Sub


    Private Sub dgReport_AutoGeneratingColumn(ByVal sender As Object, ByVal e As System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs) Handles dgReport.AutoGeneratingColumn
        e.Column.Header = e.Column.Header.ToString.Replace("_", vbCrLf)
        e.Column.Header = e.Column.Header.ToString.Replace("Damage", "Dmg")
    End Sub


    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
    End Sub
End Class
