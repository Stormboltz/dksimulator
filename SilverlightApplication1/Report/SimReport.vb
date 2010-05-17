Public Class SimReport
    Friend ReportData As New List(Of ReportLine)

    Sub New()
        ReportData.Clear()
    End Sub
    Sub Addline(ByVal Line As ReportLine)
        If IsNothing(Line) = False Then
            ReportData.Add(Line)
        End If
    End Sub
    Sub Compile()
        Diagnostics.Debug.WriteLine(ReportData.ToString)
    End Sub
End Class
