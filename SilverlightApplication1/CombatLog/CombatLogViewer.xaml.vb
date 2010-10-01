Imports System.IO.IsolatedStorage
Imports System.IO

Partial Public Class CombatLogViewer
    Inherits ChildWindow
    Dim FullCombatLog As New List(Of CombatLogTableLine)


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
        Try
            Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
                Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Combatlog/Combatlog.txt", FileMode.Open, isoStore)

                    Dim Reader As New StreamReader(isoStream)


                    Dim PrevLine As CombatLogTableLine = Nothing


                    Do Until (Reader.EndOfStream)

                        Dim l As CombatLogTableLine = GetCombatLogTableLine(Reader.ReadLine)

                        If Not PrevLine Is Nothing Then
                            l.GCD = Decimal.Round(CDec((l.timeStamp - PrevLine.timeStamp)), 2) & " ms"
                        Else
                            l.GCD = 0 & " ms"
                        End If

                        FullCombatLog.Add(l)
                        PrevLine = l
                        'Dim l As New CombatLogLine(Reader.ReadLine)
                        'If LogStack.Children.Count > 0 Then
                        '    l.txtGCD.Text = (l.TimeStamp - CType(LogStack.Children.Last, CombatLogLine).TimeStamp) & " ms"
                        'Else
                        '    l.txtGCD.Text = ""
                        'End If

                        'Me.LogStack.Children.Add(l)
                        i += 1
                    Loop
                    tblCombatLog.AutoGenerateColumns = True
                    tblCombatLog.ItemsSource = FullCombatLog
                    Reader.Close()
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub



    Private Function GetCombatLogTableLine(ByVal line As String) As CombatLogTableLine

        Dim splitedLine As String()
        splitedLine = line.Split(vbTab)
        Dim sRunes As String = splitedLine(0)
        Dim sTime As String = splitedLine(1)
        Dim sLine As String = splitedLine(2)
        Dim l As New CombatLogTableLine
        With l
            .timeStamp = CDec(sTime) / 100
            .Action = sLine
            .timeStamp = .timeStamp.ToString

            sRunes = sRunes.Replace("[", "")
            sRunes = sRunes.Replace("]", "")
            Dim SplitedRune As String() = sRunes.Split(":")
            .Blood1 = CInt(SplitedRune(0))
            If SplitedRune(1) = "D" Then .Blood1 = .Blood1 & "D"
            .Blood2 = CInt(SplitedRune(2))
            If SplitedRune(3) = "D" Then .Blood2 = .Blood2 & "D"
            .Frost1 = CInt(SplitedRune(4))
            If SplitedRune(5) = "D" Then .Frost1 = .Frost1 & "D"
            .Frost2 = CInt(SplitedRune(6))
            If SplitedRune(7) = "D" Then .Frost2 = .Frost2 & "D"
            .Unholy1 = CInt(SplitedRune(8))
            If SplitedRune(9) = "D" Then .Unholy1 = .Unholy1 & "D"
            .Unholy2 = CInt(SplitedRune(10))
            If SplitedRune(11) = "D" Then .Unholy2 = .Unholy2 & "D"
            .RunicPower = SplitedRune(12) & "/" & SplitedRune(13)

            
        End With
        Return l
    End Function
    Class CombatLogTableLine
    

        Property timeStamp As String
        Property Blood1 As String
        Property Blood2 As String
        Property Frost1 As String
        Property Frost2 As String
        Property Unholy1 As String
        Property Unholy2 As String
        Property RunicPower As String
        Property GCD As String

        Property Action As String
    End Class

    Private Sub tblCombatLog_AutoGeneratingColumn(ByVal sender As System.Object, ByVal e As System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs) Handles tblCombatLog.AutoGeneratingColumn
        e.Column.Header = e.Column.Header.ToString.Replace("Blood1", "B")
        e.Column.Header = e.Column.Header.ToString.Replace("Blood2", "B")
        e.Column.Header = e.Column.Header.ToString.Replace("Frost1", "F")
        e.Column.Header = e.Column.Header.ToString.Replace("Frost2", "F")
        e.Column.Header = e.Column.Header.ToString.Replace("Unholy1", "U")
        e.Column.Header = e.Column.Header.ToString.Replace("Unholy2", "U")
        e.Column.Header = e.Column.Header.ToString.Replace("RunicPower", "RP")
    End Sub
    Private Sub txtFilter_TextChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.TextChangedEventArgs) Handles txtFilter.TextChanged
        Dim FilterArry As String() = CType(sender, TextBox).Text.Trim.Split(" ")
        If FilterArry.Count = 0 Then
            tblCombatLog.ItemsSource = FullCombatLog
            Return
        End If
        tblCombatLog.ItemsSource = (From l In FullCombatLog
                                    Where ContainAny(l.Action, FilterArry)
                                    ).ToList
    End Sub

    Function ContainAny(ByVal Text As String, ByVal Array As String())
        For Each s In Array
            If Text.ToLower.Contains(s.ToLower) Then Return True
        Next
        Return False
    End Function

End Class




