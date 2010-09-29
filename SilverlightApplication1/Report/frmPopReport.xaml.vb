
Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO
Imports System.Windows.Controls.DataVisualization.Charting

Partial Public Class frmPopReport
    Inherits ChildWindow

    Friend ReportFrame As ReportFrame
    Dim myReader As XDocument
    Public Sub New()
        InitializeComponent()

    End Sub

    Public Sub New(ByVal ReportFrame As ReportFrame)
        InitializeComponent()
        Me.ReportFrame = ReportFrame
        dgReport.ItemsSource = ReportFrame.dgReport.ItemsSource
        txtAdditionalInfo.Text = ReportFrame.txtAdditionalInfo.Text

        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream(ReportFrame.ReportPath, FileMode.OpenOrCreate, FileAccess.Read, isoStore)
                myReader = XDocument.Load(isoStream)
                Try

                    LoadQueryInMem()
                    SetMinAndMAxChartAxis()
                    LoadChart(0)
                Catch ex As Exception
                End Try
            End Using
        End Using
        If Chart1.Series.Count = 0 Then
            Chart1.Visibility = Visibility.Collapsed
        End If




    End Sub


    Private Sub dgReport_AutoGeneratingColumn(ByVal sender As Object, ByVal e As System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs) Handles dgReport.AutoGeneratingColumn
        e.Column.Header = e.Column.Header.ToString.Replace("_", vbCrLf)
        e.Column.Header = e.Column.Header.ToString.Replace("Damage", "Dmg")
    End Sub


    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
    End Sub

    Sub SetMinAndMAxChartAxis()
        If queryCol(0).Count < 60 Then
            panelPrvNext.Visibility = Visibility.Collapsed
        End If

        Dim min As Long
        Dim max As Long

        For Each query In queryCol
            Dim lst As List(Of Point) = (From p In query
                                         Order By p.X Ascending).ToList
            If lst(0).X < min Or min = 0 Then
                min = lst(0).X
            End If
            If lst(lst.Count - 1).X > max Then
                max = lst(lst.Count - 1).X
            End If
            CType(Chart1.Axes(0), LinearAxis).Minimum = min
            CType(Chart1.Axes(0), LinearAxis).Maximum = max

        Next
    End Sub
    Dim queryCol As New List(Of List(Of Point))
    Dim SerieNames As New List(Of String)
    Sub LoadQueryInMem()
        For Each eLine As XElement In myReader.<Report>.<Chart>.Elements
            Dim Q As New List(Of Point)
            Dim pointList As String()
            pointList = eLine.Value.Split("|")
            For Each p In pointList
                If p.Contains(";") Then
                    Dim sp As String()
                    sp = p.Split(";")
                    Q.Add(New Point(sp(1), sp(0)))
                End If
            Next
            Dim nm As String = eLine.@name
            SerieNames.Add(nm)
            queryCol.Add(Q)
        Next
    End Sub

    Sub LoadChart(ByVal Start As Integer)
        Dim ColorNum As Integer = 0
        Dim serie As System.Windows.Controls.DataVisualization.Charting.LineSeries
        For j = 0 To queryCol.Count - 1
            Dim Q As List(Of Point) = queryCol(j)
            Dim toAdd As Boolean
            If Me.Chart1.Series.Count > j Then
                serie = Me.Chart1.Series(j)
                toAdd = False
            Else
                serie = New LineSeries
                serie.Title = SerieNames(j)
                toAdd = True
            End If
            serie.DependentValuePath = "X"
            serie.IndependentValuePath = "Y"
            Dim c As Collections.Generic.List(Of Point)
            If serie.ItemsSource Is Nothing Then
                c = New List(Of Point)
            Else
                c = serie.ItemsSource
            End If


            Dim cursor As Integer = 0
            Dim e As Point
            Try
                For i = Start To Start + 60
                    e = Q(i)

                    If c.Count <= cursor Then
                        Dim p As New Point(e.X, e.Y)
                        c.Add(p)
                    Else
                        Dim p As Point = c(cursor)
                        p.X = e.X
                        p.Y = e.Y
                        c(cursor) = p
                    End If
                    cursor += 1
                Next
            Catch ex As Exception

            End Try
            If serie.DataPointStyle Is Nothing Then
                Dim resStyle As Style = Application.Current.Resources("MyDotStyle")
                Dim dpStyle As New Style
                dpStyle.BasedOn = resStyle
                dpStyle.TargetType = resStyle.TargetType
                dpStyle.Setters.Add(New Setter(BackgroundProperty, GetColor(j)))
                serie.DataPointStyle = dpStyle
            End If
            serie.ItemsSource = c

            If toAdd Then Me.Chart1.Series.Add(serie)

        Next
    End Sub
    Function GetColor(ByVal i As Integer) As SolidColorBrush
        Select Case i
            Case 0
                Return New SolidColorBrush(Colors.Black)
            Case 1
                Return New SolidColorBrush(Colors.Blue)
            Case 2
                Return New SolidColorBrush(Colors.Cyan)
            Case 3
                Return New SolidColorBrush(Colors.Green)
            Case 4
                Return New SolidColorBrush(Colors.Magenta)
            Case 5
                Return New SolidColorBrush(Colors.Orange)
            Case 6
                Return New SolidColorBrush(Colors.Purple)
            Case 7
                Return New SolidColorBrush(Colors.Red)
            Case 8
                Return New SolidColorBrush(Colors.Yellow)
            Case 9
                Return New SolidColorBrush(Colors.White)
            Case 10
                Return New SolidColorBrush(Colors.Gray)
            Case Else
                Return New SolidColorBrush(Colors.Black)
        End Select

    End Function
    Dim CurChartStep As Integer = 0
    Private Sub Previous_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Previous.Click
        CurChartStep -= 1
        CurChartStep = Math.Max(CurChartStep, 0)
        LoadChart(CurChartStep * 60)
        LoadChart(CurChartStep * 60)
    End Sub

    Private Sub Next_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles [Next].Click
        CurChartStep += 1
        LoadChart(CurChartStep * 60)
        LoadChart(CurChartStep * 60)
    End Sub
End Class
