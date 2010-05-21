Imports System.IO.IsolatedStorage
Imports System.IO
Imports System.Linq
Imports System.Xml.Linq

Partial Public Class ReportDisplay
    Inherits ChildWindow

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
    End Sub

   

    Sub OpenReport(ByVal path As String)

        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream(path, FileMode.OpenOrCreate, FileAccess.Read, isoStore)
                Dim myReader As XDocument = XDocument.Load(isoStream)
                Dim Source As List(Of ReportDisplayLine) = (From el In myReader.Element("Table").Elements("row")
                              Select GetItem(el)).ToList
                dgReport.ItemsSource = Source
                isoStream.Close()
                Dim tmp As String = ""
                For Each el In myReader.Element("Table").Elements("AdditionalInfo")
                    tmp += el.Attribute("Caption").Value & " " & el.Attribute("Text").Value & vbCr
                Next
                txtAdditionalInfo.Text = tmp

            End Using
        End Using
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
End Class