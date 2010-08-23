Partial Public Class WaitFrame
    Inherits ChildWindow

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub ChildWindow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Loaded
        Me.Cursor = System.Windows.Input.Cursors.Wait
    End Sub

    Private Sub ChildWindow_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Me.Cursor = System.Windows.Input.Cursors.Arrow
    End Sub
End Class
