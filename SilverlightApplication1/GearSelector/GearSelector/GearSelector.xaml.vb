Imports System.Xml.Linq

Partial Public Class GearSelector
    Inherits ChildWindow
    Friend Slot As String
    Friend SelectedItem As String = -1
    Friend MainFrame As GearSelectorMainForm


    Friend ItemDB As XDocument
    Public Sub New()
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
        ItemDB = XDocument.Load("../itemDB.xml")
    End Sub
    Function getItem(ByVal el As XElement) As Item
        Dim itm As Item

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
            .BonusArmor = el.Element("slot").Value
            .Armor = el.Element("slot").Value
            .HasteRating = el.Element("HasteRating").Value
            .ExpertiseRating = el.Element("ExpertiseRating").Value
            .HitRating = el.Element("HitRating").Value
            .AttackPower = el.Element("AttackPower").Value
            .CritRating = el.Element("CritRating").Value
            .ArmorPenetrationRating = el.Element("ArmorPenetrationRating").Value
            .Speed = el.Element("Speed").Value
            .DPS = el.Element("DPS").Value
            .setid = el.Element("setid").Value
            .gembonus = el.Element("gembonus").Value
            .keywords = el.Element("keywords").Value
        End With

        Return itm
    End Function


    Sub LoadItem(ByVal Slot As String)

        Me.Slot = Slot
        Dim itemList As List(Of Item)

        itemList = (From el In ItemDB.Elements _
                    Where el.Attribute("slot") = Slot _
                    Select getItem(el) _
                    ).ToList
        dGear.ItemsSource = itemList
    End Sub


    Sub FilterList(ByVal filter As String())

    End Sub
   
    Function getItemEPValue(ByVal txDoc As XDocument) As Double
        Dim tmp As Double = 0

        tmp += txDoc.Element("item").Element("Strength").Value * MainFrame.EPvalues.Str

        tmp += txDoc.Element("item").Element("Agility").Value * MainFrame.EPvalues.Agility
        tmp += txDoc.Element("item").Element("Armor").Value * MainFrame.EPvalues.Armor
        tmp += txDoc.Element("item").Element("BonusArmor").Value * MainFrame.EPvalues.Armor
        tmp += txDoc.Element("item").Element("ExpertiseRating").Value * MainFrame.EPvalues.Exp
        tmp += txDoc.Element("item").Element("item/dps").Value.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) * MainFrame.EPvalues.MHDPS
        tmp += txDoc.Element("item").Element("item/speed").Value.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) * MainFrame.EPvalues.MHSpeed
        tmp += txDoc.Element("item").Element("HitRating").Value * MainFrame.EPvalues.Hit
        tmp += txDoc.Element("item").Element("AttackPower").Value * 1
        tmp += txDoc.Element("item").Element("CritRating").Value * MainFrame.EPvalues.Crit
        tmp += txDoc.Element("item").Element("ArmorPenetrationRating").Value * MainFrame.EPvalues.ArP
        tmp += txDoc.Element("item").Element("HasteRating").Value * MainFrame.EPvalues.Haste

        If txDoc.Element("item").Element("gem1").Value <> 0 Then
            tmp += 20 * MainFrame.EPvalues.Str
        End If
        If txDoc.Element("item").Element("gem2").Value <> 0 Then
            tmp += 20 * MainFrame.EPvalues.Str
        End If
        If txDoc.Element("item").Element("gem3").Value <> 0 Then
            tmp += 20 * MainFrame.EPvalues.Str
        End If
        Return tmp


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
End Class
