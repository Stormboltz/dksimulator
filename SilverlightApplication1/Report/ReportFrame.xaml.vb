Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO
Imports System.Windows.Controls.DataVisualization.Charting

Partial Public Class ReportFrame
    Inherits UserControl

    Dim PopUpFrame As frmPopReport
    Friend ReportPath As String

    Public Sub New()
        InitializeComponent()
    End Sub
    Sub OpenReport(ByVal path As String)
        ReportPath = path


        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream(path, FileMode.OpenOrCreate, FileAccess.Read, isoStore)
                Dim myReader As XDocument = XDocument.Load(isoStream)
                Dim Source As List(Of ReportDisplayLine) = (From el In myReader.Element("Report").Element("Table").Elements("row")
                             Select GetItem(el)).ToList

                If Source.Count = 0 Then
                    dgReport.Visibility = Windows.Visibility.Collapsed
                Else
                    dgReport.ItemsSource = Source
                    dgReport.Visibility = Windows.Visibility.Visible
                    Dim t As String = "[TABLE]"
                    t &= "Ability| Damage done|||| hits||| Crits||| Misses|| Glances|||TPS| Uptime" & vbCrLf
                    t &= "|Total| %| #| Avg| #| %| Avg| #| %| Avg|  #| Avg| #| Avg| %|  %| " & vbCrLf

                    For Each r As ReportDisplayLine In Source
                        t = t & r.Ability & "|"
                        t = t & r.Damage_done_Total & "|"
                        t = t & r.Damage_Pc & "|"
                        t = t & r.Damage_Count & "|"
                        t = t & r.Damage_Avg & "|"
                        t = t & r.hit_count & "|"
                        t = t & r.hit_Avg & "|"
                        t = t & r.hit_Pc & "|"
                        t = t & r.Crit_count & "|"
                        t = t & r.Crit_Avg & "|"
                        t = t & r.Crit_Pc & "|"
                        t = t & r.Miss_Count & "|"
                        t = t & r.Miss_Pc & "|"
                        t = t & r.Glance_Count & "|"
                        t = t & r.Glance_Avg & "|"
                        t = t & r.Glance_Pc & "|"
                        t = t & r.TPS & "|"
                        t = t & r.Uptime & "|" & vbCrLf
                    Next

                    t &= "[/TABLE]" & vbCrLf
                    Me.txtBBCode.Text = t
                End If

                isoStream.Close()
                Dim tmp As String = ""
                For Each el In myReader.Element("Report").Elements("AdditionalInfo")
                    tmp += el.Attribute("Caption").Value & " " & el.Attribute("Text").Value & vbCr
                Next
                txtAdditionalInfo.Text = tmp

            End Using
        End Using

    End Sub




    Function GetItem(ByVal el As XElement) As ReportDisplayLine
        Dim r As New ReportDisplayLine
        r.Ability = el.<Ability>.Value
        r.Damage_done_Total = el.<Damage_done_Total>.Value
        r.Damage_Pc = el.<Damage_Pc>.Value
        r.Damage_Count = el.<Damage_Count>.Value
        r.Damage_Avg = el.<Damage_Avg>.Value
        r.hit_count = el.<hit_Count>.Value
        r.hit_Avg = el.<hit_Avg>.Value
        r.hit_Pc = el.<hit_Pc>.Value
        r.Crit_count = el.<Crit_Count>.Value
        r.Crit_Avg = el.<Crit_Avg>.Value
        r.Crit_Pc = el.<Crit_Pc>.Value
        r.Miss_Count = el.<Miss_Count>.Value
        r.Miss_Pc = el.<Miss_Pc>.Value
        r.Glance_Count = el.<Glance_Count>.Value
        r.Glance_Avg = el.<Glance_Avg>.Value
        r.Glance_Pc = el.<Glance_Pc>.Value
        r.TPS = el.<TPS>.Value
        r.Uptime = el.<Uptime>.Value
        Return r
    End Function
    Class ReportDisplayLine
        Property Ability As String
        Property Damage_done_Total As Double
        Property Damage_Pc As Double
        Property Damage_Count As Double
        Property Damage_Avg As Double
        Property hit_count As Double
        Property hit_Avg As Double
        Property hit_Pc As Double
        Property Crit_count As Double
        Property Crit_Avg As Double
        Property Crit_Pc As Double
        Property Miss_Count As Double
        Property Miss_Pc As Double
        Property Glance_Count As Double
        Property Glance_Avg As Double
        Property Glance_Pc As Double
        Property TPS As Double
        Property Uptime As Double
    End Class

    Private Sub dgReport_AutoGeneratingColumn(ByVal sender As Object, ByVal e As System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs) Handles dgReport.AutoGeneratingColumn
        e.Column.Header = e.Column.Header.ToString.Replace("_", vbCrLf)
        e.Column.Header = e.Column.Header.ToString.Replace("Damage", "Dmg")
        e.Column.Header = e.Column.Header.ToString.Replace("Pc", "%")
        e.Column.Header = e.Column.Header.ToString.Replace("Count", "#")
    End Sub


    Private Sub cmdShowDetails_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdShowDetails.Click
        If IsNothing(PopUpFrame) Then
            PopUpFrame = New frmPopReport(Me)
        End If
        PopUpFrame.Show()
    End Sub
End Class
