Imports System.Xml.Linq

Partial Public Class GearSelector
    Inherits ChildWindow
    Friend Slot As String
    Friend SelectedItem As String = -1
    Friend MainFrame As GearSelectorMainForm


    Friend ItemDB As XDocument
    Private Sub New()
        InitializeComponent()
    End Sub
    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles CancelButton.Click
        Me.DialogResult = False
    End Sub
    Public Sub New(ByVal m As GearSelectorMainForm)
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()
        MainFrame = m
        ItemDB = m.ItemDB
    End Sub
    Function getItem(ByVal el As XElement) As aItem
        Dim itm As New aItem

        With itm
            .Id = el.Element("id").Value
            .name = el.Element("name").Value
            .ilvl = el.Element("ilvl").Value
            .slot = el.Element("slot").Value
            .classs = el.Element("classs").Value
            .subclass = el.Element("subclass").Value
            .heroic = el.Element("heroic").Value
            .Str = el.Element("Strength").Value
            .Agi = el.Element("Agility").Value
            .BonusArmor = el.Element("BonusArmor").Value
            .Armor = el.Element("Armor").Value
            .Haste = el.Element("HasteRating").Value
            .Exp = el.Element("ExpertiseRating").Value
            .Hit = el.Element("HitRating").Value
            .AP = el.Element("AttackPower").Value
            .Crit = el.Element("CritRating").Value
            .ArP = el.Element("ArmorPenetrationRating").Value
            .Speed = el.Element("speed").Value
            .DPS = el.Element("dps").Value
            .setid = el.Element("setid").Value
            .gembonus = el.Element("gembonus").Value
            .keywords = el.Element("keywords").Value
            .EPVAlue = getItemEPValue(el)
        End With

        Return itm
    End Function


    Sub LoadItem(ByVal Slot As String)

        Me.Slot = Slot
        If txtFilter.Text.Trim <> "" Then
            FilterList(txtFilter.Text)
            Return
        End If

        Dim itemList As List(Of aItem)
        ItemDB = MainFrame.ItemDB
        itemList = (From el In ItemDB.Element("items").Elements _
                    Where el.Element("slot") = Slot _
                    Order By getItemEPValue(el) Descending _
                    Select getItem(el)).ToList
        dGear.AutoGenerateColumns = True
        dGear.ItemsSource = itemList

    End Sub


    Sub FilterList(ByVal filter As String)
        Me.Slot = Slot
        Dim itemList As List(Of aItem)
        ItemDB = MainFrame.ItemDB
        itemList = (From el In ItemDB.Element("items").Elements
                    Where el.Element("slot") = Slot And Contains(el, filter)
                    Order By getItemEPValue(el) Descending
                    Select getItem(el)).ToList
        dGear.AutoGenerateColumns = True
        dGear.ItemsSource = itemList
    End Sub
   
    Function getItemEPValue(ByVal el As XElement) As Integer
        Dim tmp As Double = 0

        tmp += el.Element("Strength").Value * MainFrame.EPvalues.Str

        tmp += el.Element("Agility").Value * MainFrame.EPvalues.Agility
        tmp += el.Element("Armor").Value * MainFrame.EPvalues.Armor
        tmp += el.Element("BonusArmor").Value * MainFrame.EPvalues.Armor
        tmp += el.Element("ExpertiseRating").Value * MainFrame.EPvalues.Exp
        tmp += el.Element("dps").Value.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) * MainFrame.EPvalues.MHDPS
        tmp += el.Element("speed").Value.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) * MainFrame.EPvalues.MHSpeed
        tmp += el.Element("HitRating").Value * MainFrame.EPvalues.Hit
        tmp += el.Element("AttackPower").Value * 1
        tmp += el.Element("CritRating").Value * MainFrame.EPvalues.Crit
        Try
            tmp += el.Element("ArmorPenetrationRating").Value * MainFrame.EPvalues.ArP
        Catch
        End Try
        Try
            tmp += el.Element("Mastery").Value * MainFrame.EPvalues.Mastery
        Catch
        End Try
        tmp += el.Element("HasteRating").Value * MainFrame.EPvalues.Haste

        If el.Element("gem1").Value <> 0 Then
            tmp += 20 * MainFrame.EPvalues.Str
        End If
        If el.Element("gem2").Value <> 0 Then
            tmp += 20 * MainFrame.EPvalues.Str
        End If
        If el.Element("gem3").Value <> 0 Then
            tmp += 20 * MainFrame.EPvalues.Str
        End If
        Return Convert.ToInt32(tmp)


    End Function
    Private Function Contains(ByVal el As XElement, ByVal filter As String) As Boolean
        Dim tmp As String
        Dim tBool As Boolean = True
        tmp = el.Element("name").Value & " " & _
            el.Element("Strength").Value & " " & _
            el.Element("Agility").Value & " " & _
            el.Element("HasteRating").Value & " " & _
            el.Element("ExpertiseRating").Value & " " & _
            el.Element("HitRating").Value & " " & _
            el.Element("AttackPower").Value & " " & _
            el.Element("CritRating").Value & " " & _
            el.Element("Mastery").Value & " " & _
            el.Element("ilvl").Value & " " & _
            el.Element("keywords").Value & " " & _
            el.Element("speed").Value & " " & _
            el.Element("dps").Value
        For Each s In filter.Split(" ")
            If tmp.ToUpper.Contains(s.ToUpper) = False Then
                tBool = False
            End If
        Next
        Return tBool
    End Function

    Sub GearSelectorLoad(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Sub GearSelectorClose(ByVal sender As Object, ByVal e As EventArgs)
        If SelectedItem <> "" Then Me.DialogResult = True
    End Sub

    Sub CmdClearClick(ByVal sender As Object, ByVal e As EventArgs)
        SelectedItem = 0
        Me.Close()
    End Sub
    Class aItem
        Friend Id As Integer
        Property name As String
        Property ilvl As Integer
        Friend slot As Integer
        Friend classs As Integer
        Friend subclass As Integer
        Property heroic As Integer

        Property Str As Integer
        Friend Intel As Integer
        Property Agi As Integer
        Friend BonusArmor As Integer
        Property Armor As Integer
        Property Haste As Integer
        Property Exp As Integer


        Property Hit As Integer
        Property AP As Integer
        Property Crit As Integer
        Property ArP As Integer
        Property Speed As String = "0"
        Property DPS As String = "0"

        Friend setid As Integer
        Friend gembonus As Integer
        Friend keywords As String

        Property EPVAlue As Integer

    End Class

    Private Sub dGear_BeginningEdit(ByVal sender As Object, ByVal e As System.Windows.Controls.DataGridBeginningEditEventArgs) Handles dGear.BeginningEdit
        If IsNothing(sender.selecteditem) Then Exit Sub
        Dim a As aItem
        a = sender.selecteditem
        Try
            SelectedItem = a.Id
            Me.DialogResult = True
        Catch ex As Exception

        End Try
    End Sub
    
    Private Sub dGear_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) 'Handles dGear.SelectionChanged
        If IsNothing(sender.selecteditem) Then Exit Sub
        Dim a As aItem
        a = sender.selecteditem
        Try
            SelectedItem = a.Id
            Me.DialogResult = True
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txtFilter_TextChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.TextChangedEventArgs) Handles txtFilter.TextChanged
        If sender.Text.Trim <> "" Then
            FilterList(sender.Text)
        Else
            LoadItem(Me.Slot)
        End If
    End Sub
End Class
