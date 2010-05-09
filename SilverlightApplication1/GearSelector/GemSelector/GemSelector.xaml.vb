Imports System.Xml.Linq

Partial Public Class GemSelector
    Inherits ChildWindow

    Friend gemDB As New XDocument
    Friend Slot As String
    Friend SelectedItem As String
    Friend MainFrame As GearSelectorMainForm
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
    End Sub

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles CancelButton.Click
        Me.DialogResult = False
    End Sub
    Public Sub New(ByVal M As GearSelectorMainForm)

        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()

        MainFrame = M
        gemDB.Load("\GearSelector\" & "gems.xml")

        '
        ' TODO : Add constructor code after InitializeComponents
        '
    End Sub

    Sub ListView1SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        SelectedItem = sender.SelectedItems.Item(0).subitems.item(1).text
        Me.Close()
    End Sub
    

    Sub LoadItem(Optional ByVal slot As Integer = -1)
        Dim statusReport As List(Of Gem)
        Dim doc As XDocument = New XDocument("../gems.xml")
        If slot = 1 Then
            statusReport = (From el In doc.Elements() Select AddGem(el)).ToList()
            'xList = gemDB.Element("/gems/item[subclass='6']")
        Else
            statusReport = (From el In doc.Elements() Select AddGem(el)).ToList()
            'xList = gemDB.Element("/gems/item[quality='4'][reqskill='0' or reqskill='" & GetSkillID(Me.MainFrame.cmbSkill1.SelectedItem) & "'    or reqskill='" & GetSkillID(Me.MainFrame.cmbSkill2.SelectedItem) & "']")
        End If
        gGems.ItemsSource = statusReport
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
        Dim statusReport As List(Of Gem)
        Dim doc As XDocument = New XDocument("../gems.xml")
        If Slot = 1 Then
            statusReport = (From el In doc.Elements() Select AddGem(el)).ToList()
            'xList = gemDB.Element("/gems/item[subclass='6']")
        Else
            statusReport = (From el In doc.Elements() Select AddGem(el)).ToList()
            'xList = gemDB.Element("/gems/item[quality='4'][reqskill='0' or reqskill='" & GetSkillID(Me.MainFrame.cmbSkill1.SelectedItem) & "'    or reqskill='" & GetSkillID(Me.MainFrame.cmbSkill2.SelectedItem) & "']")
        End If
        gGems.ItemsSource = statusReport
        Me.Slot = Slot
    End Sub
    Function AddGem(ByVal el As XElement) As Gem
        AddGem.Id = el.Element("id")
        With AddGem

            .name = el.Element("name").Value
            .Strength = el.Element("Strength").Value
            .Agility = el.Element("Agility").Value
            .HasteRating = el.Element("HasteRating").Value
            .ExpertiseRating = el.Element("ExpertiseRating").Value
            .HitRating = el.Element("HitRating").Value
            .AttackPower = el.Element("AttackPower").Value
            .CritRating = el.Element("CritRating").Value
            .ArmorPenetrationRating = el.Element("ArmorPenetrationRating").Value
        End With
        Return AddGem


        'myItem.SubItems.Add(getItemEPValue(txDoc))
        'myItem.BackColor = GemColor(txDoc.Element("/item/subclass").Value)
    End Function


    Sub GearSelectorLoad(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Sub GearSelectorClose(ByVal sender As Object, ByVal e As EventArgs)
        If SelectedItem <> "" Then Me.DialogResult = True
    End Sub

    Function getItemEPValue(ByVal txDoc As XDocument) As Double
        Dim tmp As Double = 0
        tmp += txDoc.Element("/item/Strength").Value * MainFrame.EPvalues.Str
        tmp += txDoc.Element("/item/Agility").Value * MainFrame.EPvalues.Agility
        tmp += txDoc.Element("/item/ExpertiseRating").Value * MainFrame.EPvalues.Exp
        tmp += txDoc.Element("/item/HitRating").Value * MainFrame.EPvalues.Hit
        tmp += txDoc.Element("/item/AttackPower").Value * 1
        tmp += txDoc.Element("/item/CritRating").Value * MainFrame.EPvalues.Crit
        tmp += txDoc.Element("/item/ArmorPenetrationRating").Value * MainFrame.EPvalues.ArP
        tmp += txDoc.Element("/item/HasteRating").Value * MainFrame.EPvalues.Haste



        Return tmp
    End Function
    Sub CmdClearClick(ByVal sender As Object, ByVal e As EventArgs)
        SelectedItem = 0
        Me.Close()
    End Sub
End Class
