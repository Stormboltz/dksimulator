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

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
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
            .Strength = el.Element("Strength").Value
            .Agility = el.Element("Agility").Value
            .BonusArmor = el.Element("BonusArmor").Value
            .Armor = el.Element("Armor").Value
            .HasteRating = el.Element("HasteRating").Value
            .ExpertiseRating = el.Element("ExpertiseRating").Value
            .HitRating = el.Element("HitRating").Value
            .AttackPower = el.Element("AttackPower").Value
            .CritRating = el.Element("CritRating").Value
            .ArmorPenetrationRating = el.Element("ArmorPenetrationRating").Value
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
        Dim itemList As List(Of aItem)
        ItemDB = MainFrame.ItemDB
        itemList = (From el In ItemDB.Element("items").Elements _
                    Where el.Element("slot") = Slot _
                    Order By getItemEPValue(el) Descending _
                    Select getItem(el)).ToList
        dGear.AutoGenerateColumns = True
        dGear.ItemsSource = itemList

    End Sub


    Sub FilterList(ByVal filter As String())

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
        tmp += el.Element("ArmorPenetrationRating").Value * MainFrame.EPvalues.ArP
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
        Property Id As Integer
        Property name As String
        Property ilvl As Integer
        Property slot As Integer
        Property classs As Integer
        Property subclass As Integer
        Property heroic As Integer

        Property Strength As Integer
        Property Intel As Integer
        Property Agility As Integer
        Property BonusArmor As Integer
        Property Armor As Integer
        Property HasteRating As Integer
        Property ExpertiseRating As Integer


        Property HitRating As Integer
        Property AttackPower As Integer
        Property CritRating As Integer
        Property ArmorPenetrationRating As Integer
        Property Speed As String = "0"
        Property DPS As String = "0"

        Property setid As Integer
        Property gembonus As Integer
        Property keywords As String

        Property EPVAlue As Integer

    End Class

    Private Sub dGear_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles dGear.SelectionChanged

        Dim a As aItem
        a = sender.selecteditem
        Try
            SelectedItem = a.Id
            Me.DialogResult = True
        Catch ex As Exception

        End Try
    End Sub
End Class
