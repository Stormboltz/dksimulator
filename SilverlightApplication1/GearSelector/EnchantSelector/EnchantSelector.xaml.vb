Imports System.Xml.Linq
Imports System.Collections.Generic
Imports System.Linq


Partial Public Class EnchantSelector
    Inherits ChildWindow
    Friend GS As GearSelectorMainForm
    Dim sortColumn As Integer = -1
    Friend gemDB As New XDocument
    Friend Slot As String
    Friend SelectedItem As String

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal GS As GearSelectorMainForm)
        Me.GS = GS
        InitializeComponent()
    End Sub

    

    
    Function getEnchant(ByVal el As XElement) As aEnchant
        Dim myEnch As New aEnchant
        myEnch.Id = el.Element("id").Value
        With myEnch
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
            .Desc = el.Element("Desc").Value
        End With
        Return myEnch
    End Function

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
    End Sub

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles CancelButton.Click
        Me.DialogResult = False
    End Sub

    



    Sub LoadItem(ByVal slot As Integer)

        Dim statusReport As List(Of aEnchant)

        Dim doc As XDocument = GS.EnchantDB
        statusReport = (From el In doc.Elements("enchant").Elements
                        Where el.Element("slot").Value = slot And (el.Element("reqskill").Value = "" Or el.Element("reqskill").Value = 0 Or el.Element("reqskill").Value = GetSkillID(Me.GS.cmbSkill1.SelectedItem) Or el.Element("reqskill").Value = GetSkillID(Me.GS.cmbSkill2.SelectedItem))
                        Select getEnchant(el)).ToList()
        gEnchant.ItemsSource = statusReport
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

        Dim statusReport As List(Of aEnchant)

        Dim doc As XDocument = New XDocument("../Enchant.xml")
        statusReport = (From el In doc.Elements("enchant")
                        Where el.Element("slot").Value = Slot And (el.Element("reqskill").Value = "" Or el.Element("reqskill").Value = 0 Or el.Element("reqskill").Value = GetSkillID(Me.GS.cmbSkill1.SelectedItem) Or el.Element("reqskill").Value = GetSkillID(Me.GS.cmbSkill2.SelectedItem))
                        Select getEnchant(el)).ToList()
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
    Class aEnchant
        Property Id As Integer
        Property name As String
        Property slot As Integer
        Property Strength As Integer
        Property Intel As Integer
        Property Agility As Integer
        Property HasteRating As Integer
        Property ExpertiseRating As Integer
        Property HitRating As Integer
        Property AttackPower As Integer
        Property CritRating As Integer
        Property ArmorPenetrationRating As Integer
        Property Desc As String
    End Class

    Private Sub gEnchant_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles gEnchant.SelectionChanged
        Dim a As aEnchant
        a = sender.selecteditem
        Try
            SelectedItem = a.Id
            Me.DialogResult = True
        Catch ex As Exception

        End Try
    End Sub
End Class
