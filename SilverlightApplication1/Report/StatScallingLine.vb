Imports System.Linq
Public Class StatScallingLine
    Inherits ReportLine
    Dim RowColl As New Collections.Generic.List(Of aRow)
    Dim myStatName As String
    Sub New(ByVal StatName As String)
        myStatName = StatName
    End Sub

    Sub Add(ByVal RowName As String, ByVal RowValue As String)

        RowColl.Add(New aRow(RowName, RowValue))
    End Sub
    Overrides Function InnerText() As String
        Dim tmp As String = "<Chart name='" & myStatName & "'>"
        For Each r As aRow In (From x In RowColl Order By x.name Ascending).ToList
            tmp += "<col num='" & r.name & "'>" & r.Value & "</col>"
        Next
        tmp += "</Chart>"
        Return tmp
    End Function
    Class aRow
        Friend name As String
        Friend Value As String

        Sub New(ByVal RowName As String, ByVal RowValue As String)
            name = RowName
            Value = RowValue
        End Sub

    End Class


End Class
