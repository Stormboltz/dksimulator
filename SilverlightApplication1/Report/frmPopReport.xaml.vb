
Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO
Imports System.Windows.Controls.DataVisualization.Charting

Partial Public Class frmPopReport
    Inherits ChildWindow

    Friend ReportFrame As ReportFrame

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
                Dim myReader As XDocument = XDocument.Load(isoStream)
                Try
                    For Each eLine As XElement In myReader.Element("Table").Elements("Chart")
                        Dim serie As New System.Windows.Controls.DataVisualization.Charting.LineSeries
                        serie.DependentValuePath = "X"
                        serie.IndependentValuePath = "Y"
                        serie.Title = eLine.Attribute("name").Value
                        Dim c As New Collections.Generic.List(Of Point)
                        For Each e As XElement In eLine.Elements
                            Dim p As New Point(e.Value, e.Attribute("num").Value)
                            c.Add(p)
                        Next
                        serie.ItemsSource = c
                        Me.Chart1.Series.Add(serie)
                    Next
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
End Class
