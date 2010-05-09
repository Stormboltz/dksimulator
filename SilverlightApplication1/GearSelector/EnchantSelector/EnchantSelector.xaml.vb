Imports System.Xml.Linq
Imports System.Collections.Generic
Imports System.Linq


Partial Public Class EnchantSelector
    Inherits ChildWindow
    Friend GS As GearSelectorMainForm
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal GS As GearSelectorMainForm)
        Me.GS = GS
        InitializeComponent()
    End Sub

    

    
    Function getEnchant(ByVal el As XElement) As Enchant
        getEnchant.Id = el.Element("id").Value
        With getEnchant
            .Id = el.Element("id").Value
            .name = el.Element("name").Value
            .Strength = el.Element("Strength").Value
            .Agility = el.Element("Agility").Value
            .HasteRating = el.Element("HasteRating").Value
            .ExpertiseRating = el.Element("ExpertiseRating").Value
            .HitRating = el.Element("HitRating").Value
            .AttackPower = el.Element("AttackPower").Value
            .CritRating = el.Element("CritRating").Value
            .ArmorPenetrationRating = el.Element("ArmorPenetrationRating").Value
            '.reqskill = el.Element("reqskill").Value
            .Desc = el.Element("Desc").Value
        End With
        Return getEnchant
    End Function

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
    End Sub

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles CancelButton.Click
        Me.DialogResult = False
    End Sub

    Dim sortColumn As Integer = -1
    Friend gemDB As New XDocument
    Friend Slot As String
    Friend SelectedItem As String
    Friend MainFrame As GearSelectorMainForm



    Sub LoadItem(ByVal slot As Integer)

        Dim statusReport As List(Of Enchant)

        Dim doc As XDocument = New XDocument("../Enchant.xml")
        statusReport = (From el In doc.Elements() Select getEnchant(el)).ToList()
        gEnchant.ItemsSource = statusReport
        'xList = gemDB.SelectNodes("/enchant/item[slot=" & slot & "][reqskill='0' or reqskill='" & GetSkillID(Me.MainFrame.cmbSkill1.SelectedItem) & "'    or reqskill='" & GetSkillID(Me.MainFrame.cmbSkill2.SelectedItem) & "']")
        Me.Slot = slot

    End Sub



    Sub TextBox1TextChanged(ByVal sender As TextBox, ByVal e As EventArgs)
        If sender.Text.Trim <> "" Then
            FilterList(sender.Text.Split(" "))
        Else
            LoadItem(Me.Slot)
        End If
    End Sub

    Sub FilterList(ByVal filter As String())

        Dim statusReport As List(Of Enchant)

        Dim doc As XDocument = New XDocument("../Enchant.xml")
        statusReport = (From el In doc.Elements() Select getEnchant(el)).ToList()
        gEnchant.ItemsSource = statusReport
        'xList = gemDB.SelectNodes("/enchant/item[slot=" & Slot & "][reqskill='0' or reqskill='" & GetSkillID(Me.MainFrame.cmbSkill1.SelectedItem) & "'    or reqskill='" & GetSkillID(Me.MainFrame.cmbSkill2.SelectedItem) & "']")
    End Sub
    Sub GearSelectorClose(ByVal sender As Object, ByVal e As EventArgs)
        If SelectedItem <> "" Then Me.DialogResult = True
    End Sub

    Sub CmdClearClick(ByVal sender As Object, ByVal e As EventArgs)
        SelectedItem = 0
        Me.Close()
    End Sub
End Class
