Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO

Partial Public Class ReportFrame
    Inherits UserControl

    Dim PopUpFrame As frmPopReport

    Public Sub New()
        InitializeComponent()
    End Sub
    Sub OpenReport(ByVal path As String)
        Try


            Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
                Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream(path, FileMode.OpenOrCreate, FileAccess.Read, isoStore)
                    Dim myReader As XDocument = XDocument.Load(isoStream)
                    Dim Source As List(Of ReportDisplayLine) = (From el In myReader.Element("Table").Elements("row")
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
                            t = t & r.Damage_done_Pc & "|"
                            t = t & r.Damage_done_Count & "|"
                            t = t & r.Damage_done_Avg & "|"
                            t = t & r.hit_count & "|"
                            t = t & r.hit_count_Avg & "|"
                            t = t & r.hit_count_Pc & "|"
                            t = t & r.Crit_count & "|"
                            t = t & r.Crit_count_Avg & "|"
                            t = t & r.Crit_count_Pc & "|"
                            t = t & r.Miss_Count & "|"
                            t = t & r.Miss_Count_Pc & "|"
                            t = t & r.Glance_Count & "|"
                            t = t & r.Glance_Count_Avg & "|"
                            t = t & r.Glance_Count_Pc & "|"
                            t = t & r.TPS & "|"
                            t = t & r.Uptime & "|" & vbCrLf
                        Next

                        t &= "[/TABLE]" & vbCrLf
                        Me.txtBBCode.Text = t
                    End If

                    isoStream.Close()
                    Dim tmp As String = ""
                    For Each el In myReader.Element("Table").Elements("AdditionalInfo")
                        tmp += el.Attribute("Caption").Value & " " & el.Attribute("Text").Value & vbCr
                    Next
                    txtAdditionalInfo.Text = tmp

                End Using
            End Using
        Catch ex As Exception
            'msgBox("No report to open")
        End Try
    End Sub




    Function GetItem(ByVal el As XElement) As ReportDisplayLine
        Dim r As New ReportDisplayLine
        r.Ability = el.Element("Ability").Value
        r.Damage_done_Total = el.Element("Damage_done_Total").Value
        r.Damage_done_Pc = el.Element("Damage_done_Pc").Value
        r.Damage_done_Count = el.Element("Damage_done_Count").Value
        r.Damage_done_Avg = el.Element("Damage_done_Avg").Value
        r.hit_count = el.Element("hit_count").Value
        r.hit_count_Avg = el.Element("hit_count_Avg").Value
        r.hit_count_Pc = el.Element("hit_count_Pc").Value
        r.Crit_count = el.Element("Crit_count").Value
        r.Crit_count_Avg = el.Element("Crit_count_Avg").Value
        r.Crit_count_Pc = el.Element("Crit_count_Pc").Value
        r.Miss_Count = el.Element("Miss_Count").Value
        r.Miss_Count_Pc = el.Element("Miss_Count_Pc").Value
        r.Glance_Count = el.Element("Glance_Count").Value
        r.Glance_Count_Avg = el.Element("Glance_Count_Avg").Value
        r.Glance_Count_Pc = el.Element("Glance_Count_Pc").Value
        r.TPS = el.Element("TPS").Value
        r.Uptime = el.Element("Uptime").Value
        'If r.Damage_done_Total = "0" Then r.Damage_done_Total = ""
        'If r.Damage_done_Total = "0" Then r.Damage_done_Total = ""
        'If r.Damage_done_Count = "0" Then r.Damage_done_Count = ""
        'If r.Damage_done_Avg = "0" Then r.Damage_done_Avg = ""
        'If r.hit_count = "0" Then r.hit_count = ""
        'If r.hit_count_Avg = "0" Then r.hit_count_Avg = ""
        'If r.hit_count_Pc = "0" Then r.hit_count_Pc = ""
        'If r.Crit_count = "0" Then r.Crit_count = ""
        'If r.Crit_count_Avg = "0" Then r.Crit_count_Avg = ""
        'If r.Crit_count_Pc = "0" Then r.Crit_count_Pc = ""
        'If r.Miss_Count = "0" Then r.Miss_Count = ""
        'If r.Miss_Count_Pc = "0" Then r.Miss_Count_Pc = ""
        'If r.Glance_Count = "0" Then r.Glance_Count = ""
        'If r.Glance_Count_Avg = "0" Then r.Glance_Count_Avg = ""
        'If r.Glance_Count_Pc = "0" Then r.Glance_Count_Pc = ""
        'If r.TPS = "0" Then r.TPS = ""
        'If r.Uptime = "0" Then r.Uptime = ""

        Return r
    End Function
    Class ReportDisplayLine
        Property Ability As String
        Property Damage_done_Total As Double
        Property Damage_done_Pc As Double
        Property Damage_done_Count As Double
        Property Damage_done_Avg As Double
        Property hit_count As Double
        Property hit_count_Avg As Double
        Property hit_count_Pc As Double
        Property Crit_count As Double
        Property Crit_count_Avg As Double
        Property Crit_count_Pc As Double
        Property Miss_Count As Double
        Property Miss_Count_Pc As Double
        Property Glance_Count As Double
        Property Glance_Count_Avg As Double
        Property Glance_Count_Pc As Double
        Property TPS As Double
        Property Uptime As Double
    End Class

    Private Sub dgReport_AutoGeneratingColumn(ByVal sender As Object, ByVal e As System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs) Handles dgReport.AutoGeneratingColumn
        e.Column.Header = e.Column.Header.ToString.Replace("_", vbCrLf)
        e.Column.Header = e.Column.Header.ToString.Replace("Damage", "Dmg")
    End Sub

    
    Private Sub cmdShowDetails_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdShowDetails.Click
        If IsNothing(PopUpFrame) Then
            PopUpFrame = New frmPopReport(Me)
        End If
        PopUpFrame.Show()
    End Sub
End Class
