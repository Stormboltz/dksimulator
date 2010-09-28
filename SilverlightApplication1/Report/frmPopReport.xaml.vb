
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
                    ScrollBar1.Minimum = 0
                    SetMinAndMAxChartAxis()
                    LoadQueryInMem()
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

    Private Sub ScrollBar1_ValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.RoutedPropertyChangedEventArgs(Of System.Double)) Handles ScrollBar1.ValueChanged
        LoadChart(ScrollBar1.Value)
    End Sub
    Sub SetMinAndMAxChartAxis()

        Dim min As Long
        Dim max As Long

        For Each eLine As XElement In myReader.Element("Table").Elements("Chart")
            Dim lst As List(Of XElement) = (From xEl In eLine.Elements Order By CInt(xEl.Value) Ascending).ToList

            If lst(0).Value < min Or min = 0 Then
                min = lst(0).Value
            End If
            If lst(lst.Count - 1).Value > max Then
                max = lst(lst.Count - 1).Value
            End If

            CType(Chart1.Axes(0), LinearAxis).Minimum = min
            CType(Chart1.Axes(0), LinearAxis).Maximum = max
            ScrollBar1.Maximum = (From xEl In eLine.Elements).Count
        Next

    End Sub
    Dim queryCol As New List(Of List(Of XElement))
    Sub LoadQueryInMem()
        For Each eLine As XElement In myReader.Element("Table").Elements("Chart")
            Dim Q As List(Of XElement)
            Q = (From xEl As XElement In eLine.Elements
                 Order By CInt(xEl.Attribute("num").Value) Ascending).ToList
            queryCol.Add(Q)
        Next
    End Sub

    Sub LoadChart(ByVal Start As Integer)

        Dim ColorNum As Integer = 0
        Dim serie As System.Windows.Controls.DataVisualization.Charting.LineSeries
        For j = 0 To queryCol.Count - 1

            Dim Q As List(Of XElement) = queryCol(j)
            Dim toAdd As Boolean
            If Me.Chart1.Series.Count > j Then
                serie = Me.Chart1.Series(j)
                toAdd = False
            Else
                serie = New LineSeries
                toAdd = True
            End If
            serie.DependentValuePath = "X"
            serie.IndependentValuePath = "Y"
            Dim c As New Collections.Generic.List(Of Point)
            Dim cursor As Integer = 0
            For Each e As XElement In Q
                If c.Count < 30 Then
                    If cursor >= Start Then
                        Dim p As New Point(e.Value, e.Attribute("num").Value)
                        c.Add(p)
                    End If
                End If

                cursor += 1
            Next
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
        End Select

    End Function
End Class
