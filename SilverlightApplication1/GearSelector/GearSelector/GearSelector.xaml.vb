Imports System.Xml.Linq

Partial Public Class GearSelector
    Inherits ChildWindow
    Friend Slot As String
    Friend SelectedItem As String = -1
    Friend MainFrame As FrmGearSelector


    Friend ItemDB As XDocument
    Private Sub New()
        InitializeComponent()
    End Sub
    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles CancelButton.Click
        Me.DialogResult = False
    End Sub
    Public Sub New(ByVal m As FrmGearSelector)
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()
        MainFrame = m
        ItemDB = m.ItemDB
    End Sub
    Function getItem(ByVal el As XElement) As aItem
        Dim itm As New aItem

        With itm
            .Id = el.<id>.Value
            .name = el.<name>.Value
            .ilvl = el.<ilvl>.Value
            .slot = el.<slot>.Value
            .classs = el.<classs>.Value
            .subclass = el.<subclass>.Value
            .heroic = el.<heroic>.Value
            .Str = el.<Strength>.Value
            .Agi = el.<Agility>.Value


            .Haste = el.<HasteRating>.Value
            .Exp = el.<ExpertiseRating>.Value
            .Hit = el.<HitRating>.Value
            .AP = el.<AttackPower>.Value
            .Crit = el.<CritRating>.Value
            .ArP = 0
            Try
                .ArP = el.<ArmorPenetrationRating>.Value
            Catch ex As Exception

                Log.Log("No ArP on " & .name, logging.Level.INFO)
                .ArP = 0
            End Try
            .Speed = el.<speed>.Value
            .DPS = el.<dps>.Value
            .setid = el.<setid>.Value
            .gembonus = el.<gembonus>.Value
            .keywords = el.<keywords>.Value
            .EPVAlue = getItemEPValue(el)
            Try
                .Armor = el.<Armor>.Value
            Catch
            End Try
            Try
                .Dodge = el.<Dodge>.Value
                .Parry = el.<Parry>.Value
                .Stamina = el.<Stamina>.Value
                .BonusArmor = el.<BonusArmor>.Value
            Catch ex As Exception
                .Dodge = 0
                .Parry = 0
                .Stamina = 0
                .BonusArmor = 0
            End Try


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
        itemList = (From el As XElement In ItemDB.<items>.Elements
                    Where el.<slot>.Value = Slot
                    Order By getItemEPValue(el) Descending
                    Select getItem(el)).ToList
        dGear.AutoGenerateColumns = True
        dGear.ItemsSource = itemList

    End Sub


    Sub FilterList(ByVal filter As String)
        Me.Slot = Slot
        Dim itemList As List(Of aItem)
        ItemDB = MainFrame.ItemDB
        itemList = (From el In ItemDB.<items>.Elements
                    Where el.<slot>.Value = Slot And Contains(el, filter)
                    Order By getItemEPValue(el) Descending
                    Select getItem(el)).ToList
        dGear.AutoGenerateColumns = True
        dGear.ItemsSource = itemList
    End Sub

    Function getItemEPValue(ByVal el As XElement) As Integer
        Dim tmp As Double = 0

        tmp += el.<Strength>.Value * MainFrame.EPvalues.Str

        tmp += el.<Agility>.Value * MainFrame.EPvalues.Agility
        tmp += el.<Armor>.Value * MainFrame.EPvalues.Armor
        tmp += el.<BonusArmor>.Value * MainFrame.EPvalues.Armor
        tmp += el.<ExpertiseRating>.Value * MainFrame.EPvalues.Exp
        tmp += el.<dps>.Value.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) * MainFrame.EPvalues.MHDPS
        tmp += el.<speed>.Value.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) * MainFrame.EPvalues.MHSpeed
        tmp += el.<HitRating>.Value * MainFrame.EPvalues.Hit
        tmp += el.<AttackPower>.Value * 1
        tmp += el.<CritRating>.Value * MainFrame.EPvalues.Crit
        Try
            tmp += el.<ArmorPenetrationRating>.Value * MainFrame.EPvalues.ArP
        Catch ex As Exception
            Log.Log("No ArP Error", logging.Level.INFO)
        End Try
        Try
            tmp += el.<MasteryRating>.Value * MainFrame.EPvalues.Mastery
        Catch ex As Exception
            Log.Log("No Mastery Error", logging.Level.INFO)
        End Try
        tmp += el.<HasteRating>.Value * MainFrame.EPvalues.Haste

        If el.<gem1>.Value <> 0 Then
            tmp += 20 * MainFrame.EPvalues.Str
        End If
        If el.<gem2>.Value <> 0 Then
            tmp += 20 * MainFrame.EPvalues.Str
        End If
        If el.<gem3>.Value <> 0 Then
            tmp += 20 * MainFrame.EPvalues.Str
        End If
        Return Convert.ToInt32(tmp)


    End Function
    Private Function Contains(ByVal el As XElement, ByVal filter As String) As Boolean
        Dim tmp As String
        Dim tBool As Boolean = True
        tmp = el.<name>.Value & " " & _
            el.<Strength>.Value & " " & _
            el.<Agility>.Value & " " & _
            el.<HasteRating>.Value & " " & _
            el.<ExpertiseRating>.Value & " " & _
            el.<HitRating>.Value & " " & _
            el.<AttackPower>.Value & " " & _
            el.<CritRating>.Value & " " & _
            el.<Mastery>.Value & " " & _
            el.<ilvl>.Value & " " & _
            el.<keywords>.Value & " " & _
            el.<speed>.Value & " " & _
        el.<dps>.Value
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

        Property Armor As Integer
        Property Haste As Integer
        Property Exp As Integer


        Property Hit As Integer
        Property AP As Integer
        Property Crit As Integer
        Property ArP As Integer
        Property Speed As String = "0"
        Property DPS As String = "0"

        Property BonusArmor As Integer
        Property Dodge As Integer
        Property Parry As Integer
        Property Stamina As Integer

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
            Log.Log(ex.StackTrace, logging.Level.ERR)
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
            Log.Log(ex.StackTrace, logging.Level.ERR)

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
