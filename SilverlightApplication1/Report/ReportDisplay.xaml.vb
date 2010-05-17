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

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles CancelButton.Click
        Me.DialogResult = False
    End Sub

    Sub OpenReport(ByVal path As String)

        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream(path, FileMode.OpenOrCreate, FileAccess.Read, isoStore)
                Dim myReader As XDocument = XDocument.Load(isoStream)
                Dim Source As List(Of ReportDisplayLine) = (From el In myReader.Element("Table").Elements
                              Select GetItem(el)).ToList
                dgReport.ItemsSource = Source
                isoStream.Close()
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
        Return r
    End Function
    Class ReportDisplayLine
        Property Ability As String
        Property Damage_done_Total As String
        Property Damage_done_Pc As String
        Property Damage_done_Count As String
        Property Damage_done_Avg As String
        Property hit_count As String
        Property hit_count_Avg As String
        Property hit_count_Pc As String
        Property Crit_count As String
        Property Crit_count_Avg As String
        Property Crit_count_Pc As String
        Property Miss_Count As String
        Property Miss_Count_Pc As String
        Property Glance_Count As String
        Property Glance_Count_Avg As String
        Property Glance_Count_Pc As String
        Property TPS As String
        Property Uptime As String
        


    End Class
End Class