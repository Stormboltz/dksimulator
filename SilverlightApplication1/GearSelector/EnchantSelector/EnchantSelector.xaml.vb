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
            .Str = el.Element("Strength").Value
            .Agi = el.Element("Agility").Value
            .Haste = el.Element("HasteRating").Value
            .Exp = el.Element("ExpertiseRating").Value
            .Hit = el.Element("HitRating").Value
            .AP = el.Element("AttackPower").Value
            .Crit = el.Element("CritRating").Value
            .ArP = el.Element("ArmorPenetrationRating").Value
            .Desc = el.Element("Desc").Value
            .EPVAlue = getItemEPValue(el)
        End With
        Return myEnch
    End Function
    Function getItemEPValue(ByVal el As XElement) As Integer
        Dim tmp As Double = 0

        tmp += el.Element("Strength").Value * GS.EPvalues.Str
        tmp += el.Element("Agility").Value * GS.EPvalues.Agility
        tmp += el.Element("ExpertiseRating").Value * GS.EPvalues.Exp
        tmp += el.Element("HitRating").Value * GS.EPvalues.Hit
        tmp += el.Element("AttackPower").Value * 1
        tmp += el.Element("CritRating").Value * GS.EPvalues.Crit
        tmp += el.Element("ArmorPenetrationRating").Value * GS.EPvalues.ArP
        tmp += el.Element("HasteRating").Value * GS.EPvalues.Haste
        Return Convert.ToInt32(tmp)


    End Function
    

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles CancelButton.Click
        Me.DialogResult = False
    End Sub

    



    Sub LoadItem(ByVal slot As Integer)

        Dim statusReport As List(Of aEnchant)

        Dim doc As XDocument = GS.EnchantDB
        statusReport = (From el In doc.Elements("enchant").Elements
                        Where el.Element("slot").Value = slot And (el.Element("reqskill").Value = "" Or el.Element("reqskill").Value = 0 Or el.Element("reqskill").Value = GetSkillID(Me.GS.cmbSkill1.SelectedItem) Or el.Element("reqskill").Value = GetSkillID(Me.GS.cmbSkill2.SelectedItem))
                        Order By getItemEPValue(el) Descending
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
        Friend Id As Integer
        Property name As String
        Friend slot As Integer
        Property Str As Integer
        Friend Intel As Integer
        Property Agi As Integer
        Property Haste As Integer
        Property Exp As Integer
        Property Hit As Integer
        Property AP As Integer
        Property Crit As Integer
        Property ArP As Integer
        Property EPVAlue As Integer
        Friend Property Desc As String
    End Class

    Private Sub gEnchant_BeginningEdit(ByVal sender As Object, ByVal e As System.Windows.Controls.DataGridBeginningEditEventArgs) Handles gEnchant.BeginningEdit
        If IsNothing(sender.selecteditem) Then Exit Sub
        Dim a As aEnchant
        a = sender.selecteditem
        Try
            SelectedItem = a.Id
            Me.DialogResult = True
        Catch ex As Exception
            Log.Log(ex.StackTrace, logging.Level.ERR)
        End Try
    End Sub

    Private Sub gEnchant_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles gEnchant.SelectionChanged

    End Sub
End Class
