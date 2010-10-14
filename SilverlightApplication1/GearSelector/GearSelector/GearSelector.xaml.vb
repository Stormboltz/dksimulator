Imports System.Xml.Linq

Partial Public Class GearSelector
    Inherits ChildWindow
    Friend Slot As String
    Friend SelectedItem As String = -1
    Friend MainFrame As FrmGearSelector
    Dim ItemList As New List(Of aItem)

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
 

    Sub LoadItem(ByVal Slot As String)
        If itemList.Count = 0 Then
            ItemDB = MainFrame.ItemDB
            Dim itm As aItem
            For Each el In ItemDB.<items>.Elements
                itm = New aItem(el)
                itm.EPVAlue = getItemEPValue(itm)
                ItemList.Add(itm)
            Next
        End If
        Me.Slot = Slot
        If txtFilter.Text.Trim <> "" Then
            FilterList(txtFilter.Text)
            Return
        End If

        Dim SlotItemList = (From e In ItemList
                            Where e.slot = Slot
                            Order By e.EPVAlue Descending).ToList

        dGear.AutoGenerateColumns = True
        dGear.ItemsSource = SlotItemList

    End Sub


    Sub FilterList(ByVal filter As String)
        Me.Slot = Slot

        ItemDB = MainFrame.ItemDB
        Dim SlotItemList = (From e In ItemList
                            Where e.slot = Slot And Contains(e, filter)
                            Order By e.EPVAlue Descending).ToList
        dGear.AutoGenerateColumns = True
        dGear.ItemsSource = SlotItemList
    End Sub

    Function getItemEPValue(ByVal el As aItem) As Integer
        Dim tmp As Double = 0
        tmp += el.Str * MainFrame.EPvalues.Str
        tmp += el.Agi * MainFrame.EPvalues.Agility
        tmp += el.Exp * MainFrame.EPvalues.Exp
        tmp += el.Hit * MainFrame.EPvalues.Hit
        tmp += el.Armor * MainFrame.EPvalues.Armor
        tmp += el.BonusArmor * MainFrame.EPvalues.Armor
        tmp += el.AP
        tmp += el.Crit * MainFrame.EPvalues.Crit
        tmp += el.ArP * MainFrame.EPvalues.ArP
        tmp += el.Haste * MainFrame.EPvalues.Haste
        tmp += el.Mast * MainFrame.EPvalues.Mastery
        tmp += el.DPS * MainFrame.EPvalues.MHDPS
        tmp += el.Speed * MainFrame.EPvalues.MHSpeed
        tmp += el.ArP * MainFrame.EPvalues.ArP
        tmp += el.Mast * MainFrame.EPvalues.Mastery
        tmp += el.Gems * 20 * MainFrame.EPvalues.Str
        Return Convert.ToInt32(tmp)
    End Function
    Private Function Contains(ByVal el As aItem, ByVal filter As String) As Boolean
        Dim tmp As String
        Dim tBool As Boolean = True
        tmp = el.name & " " & _
            el.Str & " " & _
            el.Agi & " " & _
            el.Haste & " " & _
            el.Exp & " " & _
            el.Hit & " " & _
            el.AP & " " & _
            el.Crit & " " & _
            el.Mast & " " & _
            el.ilvl & " " & _
            el.keywords & " " & _
            el.Speed & " " & _
            el.DPS
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
        Property Mast As Integer

        Property Hit As Integer
        Property AP As Integer
        Property Crit As Integer
        Property ArP As Integer
        Property Speed As Integer = "0"
        Property DPS As Integer = "0"

        Property BonusArmor As Integer
        Property Dodge As Integer
        Property Parry As Integer
        Property Stamina As Integer

        Friend Gems As Integer

        Friend setid As Integer
        Friend gembonus As Integer
        Friend keywords As String

        Property EPVAlue As Integer

        Sub New(ByVal el As XElement)


            With Me
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
                .Speed = el.<speed>.Value.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                .DPS = el.<dps>.Value.Replace(".", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                .setid = el.<setid>.Value
                .gembonus = el.<gembonus>.Value
                .keywords = el.<keywords>.Value
                .Mast = el.<Mastery>.Value

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
                Try
                    If el.<gem1>.Value <> 0 Then
                        Gems += 1
                    End If
                    If el.<gem2>.Value <> 0 Then
                        Gems += 1
                    End If
                    If el.<gem3>.Value <> 0 Then
                        Gems += 1
                    End If
                Catch ex As Exception
                End Try
            End With
        End Sub



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

    Private Sub NoneButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles NoneButton.Click
        SelectedItem = 0
        Me.DialogResult = True
    End Sub
End Class
