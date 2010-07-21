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
        pbBlood.Value = CInt(SplitedRune(0))
        pbFrost.Value = CInt(SplitedRune(1))
        pbUnholy.Value = CInt(SplitedRune(2))

    End Sub

End Class
