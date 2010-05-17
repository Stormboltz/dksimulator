Partial Public Class Report
    Inherits UserControl



    Public Sub New 
        InitializeComponent()
    End Sub

    Private Sub rdVisual_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rdVisual.Checked
        If rdForum.IsChecked Then
            txtReport.Opacity = 1
            tReport.Opacity = 0
        Else
            txtReport.Opacity = 0
            tReport.Opacity = 1
        End If
        
    End Sub

    Private Sub rdForum_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rdForum.Checked
        If rdForum.IsChecked Then
            txtReport.Opacity = 1
            tReport.Opacity = 0
        Else
            txtReport.Opacity = 0
            tReport.Opacity = 1
        End If
    End Sub
   
    Sub Display()
        Me.tReport.AutoGenerateColumns = True
        'Me.tReport.ItemsSource = ReportData
    End Sub

End Class
