Partial Public Class CombatLogLine
    Inherits UserControl

    Public Sub New(ByVal Line As String)
        InitializeComponent()
        SetContent(Line)
    End Sub

    Sub SetContent(ByVal Line As String)
        '[200:145:177]	319	SS Physical crit for 12682	RP left = 55'

        Dim splitedLine As String()
        splitedLine = Line.Split(vbTab)
        Dim sRunes = splitedLine(0)
        Dim sTime = splitedLine(1)
        Dim sLine = splitedLine(2)

        txtAction.Text = sLine
        txtTime.Text = (CDec(sTime) / 100).ToString & "s"

        sRunes = sRunes.Replace("[", "")
        sRunes = sRunes.Replace("]", "")
        Dim SplitedRune As String() = sRunes.Split(":")

        pbBlood1.Value = CInt(SplitedRune(0))
        If SplitedRune(1) = "D" Then
            pbBlood1.Foreground = New SolidColorBrush(Colors.Purple)
        Else
            pbBlood1.Foreground = New SolidColorBrush(Colors.Red)
        End If
        pbBlood2.Value = CInt(SplitedRune(2))
        If SplitedRune(3) = "D" Then
            pbBlood2.Foreground = New SolidColorBrush(Colors.Purple)
        Else
            pbBlood2.Foreground = New SolidColorBrush(Colors.Red)
        End If
        pbFrost1.Value = CInt(SplitedRune(4))
        If SplitedRune(5) = "D" Then
            pbFrost1.Foreground = New SolidColorBrush(Colors.Purple)
        Else
            pbFrost1.Foreground = New SolidColorBrush(Colors.Blue)
        End If
        pbFrost2.Value = CInt(SplitedRune(6))
        If SplitedRune(7) = "D" Then
            pbFrost2.Foreground = New SolidColorBrush(Colors.Purple)
        Else
            pbFrost2.Foreground = New SolidColorBrush(Colors.Blue)
        End If
        pbUnholy1.Value = CInt(SplitedRune(8))
        If SplitedRune(9) = "D" Then
            pbUnholy1.Foreground = New SolidColorBrush(Colors.Purple)
        Else
            pbUnholy1.Foreground = New SolidColorBrush(Colors.Green)
        End If
        pbUnholy2.Value = CInt(SplitedRune(10))
        If SplitedRune(11) = "D" Then
            pbUnholy2.Foreground = New SolidColorBrush(Colors.Purple)
        Else
            pbUnholy2.Foreground = New SolidColorBrush(Colors.Green)
        End If
        pbRunic.Value = SplitedRune(12)
        pbRunic.Maximum = SplitedRune(13)

    End Sub

End Class
