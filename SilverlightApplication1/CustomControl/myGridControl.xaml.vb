Partial Public Class myGridControl
    Inherits UserControl

    Public Sub New 
        InitializeComponent()
    End Sub

    Dim previousSize As Double
    Dim reduced As Boolean = False

    Private Sub cmdExpand_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdExpand.Click
        Expand()
    End Sub

    Private Sub cmdReduce_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdReduce.Click
        Reduce()
    End Sub
    Sub Reduce()
        If reduced = False Then
            Dim Grd As Grid = CType(Me.Parent, Grid)
            If Grd.ActualHeight = 0 Then Return
            previousSize = Grd.ActualHeight
            Grd.Height = 20
            reduced = True
            Me.cmdReduce.IsEnabled = False
            Me.cmdExpand.IsEnabled = True
        End If
    End Sub
    Sub Expand()
        If reduced = True Then
            Dim Grd As Grid = CType(Me.Parent, Grid)
            Grd.Height = previousSize
            reduced = False
            Me.cmdReduce.IsEnabled = True
            Me.cmdExpand.IsEnabled = False
        End If
    End Sub

End Class
