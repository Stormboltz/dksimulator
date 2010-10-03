Partial Public Class frmStatSummary
    Inherits UserControl
    Event DPS_Stat_changed()
    Public Sub New()
        InitializeComponent()
    End Sub

    Event rd2H_Check(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
    Event rdDW_Check(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
    Private Sub rd2H_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles r2Hand.Checked
        RaiseEvent rd2H_Check(sender, e)
    End Sub

    Private Sub rdDW_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rDW.Checked
        RaiseEvent rdDW_Check(sender, e)
    End Sub

    Private Sub DPS_Stat_TextChanged() Handles txtHit.TextChanged, txtHaste.TextChanged, txtArP.TextChanged, txtCrit.TextChanged, txtExp.TextChanged, txtMast.TextChanged
        If chkManualInput.IsChecked Then RaiseEvent DPS_Stat_changed()
    End Sub
End Class
