Imports System.IO.IsolatedStorage
Imports System.IO

Partial Public Class CombatLogViewer
    Inherits ChildWindow

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
    End Sub

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Me.DialogResult = False
    End Sub

    Private Sub LayoutRoot_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles LayoutRoot.Loaded
        Dim i As Integer = 0
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Combatlog/Combatlog.txt", FileMode.Open, isoStore)
                Dim Reader As New StreamReader(isoStream)
                Do Until (Reader.EndOfStream Or i > 500)
                    Dim l As New CombatLogLine(Reader.ReadLine)
                    Me.LogStack.Children.Add(l)
                    i += 1
                Loop
                Reader.Close()
            End Using
        End Using
    End Sub
End Class
