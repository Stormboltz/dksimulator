﻿Imports System.Xml.Linq
Imports System.Linq
Partial Public Class ItemEditor
    Inherits UserControl
    Dim inLoad As Boolean
    Dim _Text As String
    Public Sub New()
        InitializeComponent()
    End Sub

    Friend xGemBonus As XDocument
    Friend SlotId As Integer
    Friend Item As Item
    Friend Origin As String
    Property text As String
        Get
            Return _Text
        End Get
        Set(ByVal value As String)
            _Text = value
        End Set
    End Property
    Friend WithEvents EnchantS As EnchantSelector
    Friend WithEvents GemS As GemSelector
    Friend WithEvents GearS As GearSelector
    Protected initiated As Boolean = False

    Protected Mainframe As FrmGearSelector
    Sub Load(ByVal VSlot As VisualEquipSlot)
        inLoad = True
        Mainframe = VSlot.Mainframe
        Me.SlotId = VSlot.SlotId
        Item = VSlot.Item
        Origin = VSlot.Name
        'Item.ReForgingFrom = ""
        'Item.ReForgingTo = ""

        DisplayItem()
        DisplayEnchant()
        DisplayGem()
        displayReforgingTo()
        displayReforge()
        inLoad = False
    End Sub

    Sub displayReforge()
        Try
            cmbReforgeFrom.SelectedValue = Item.ReForgingFrom
            cmbReforgeTo.SelectedValue = Item.ReForgingTo
        Catch ex As Exception

        End Try

        txtReforge.Text = Item.ReForgingvalue
        If txtReforge.Text = 0 Then
            Dim tmp As String = ""
            For Each i In cmbReforgeFrom.Items
                tmp += GetValueToReforge(i.ToString) & " or "
            Next
            Try
                tmp = tmp.Remove(tmp.Length - 3)
                txtReforge.Text = tmp
            Catch ex As Exception

            End Try

        End If


    End Sub
    Sub displayReforgingTo()
        cmbReforgeTo.Items.Clear()
        cmbReforgeTo.Items.Add("Crit")
        cmbReforgeTo.Items.Add("Exp")
        cmbReforgeTo.Items.Add("Haste")
        cmbReforgeTo.Items.Add("Hit")
        cmbReforgeTo.Items.Add("Mast")
        cmbReforgeTo.Items.Add("Dodge")
        cmbReforgeTo.Items.Add("Parry")
        For Each e In cmbReforgeFrom.Items
            cmbReforgeTo.Items.Remove(e.ToString)
        Next
    End Sub




    Sub init(ByVal m As FrmGearSelector, ByVal slot As Integer)
        Mainframe = m
        Me.SlotId = slot
        Dim query = From c As XElement In m.EnchantDB.Element("enchant").Elements Where c.Element("slot").Value = slot
        If query.Count > 0 Then
            lblEnchant.IsEnabled = True
            lblEnchant.Opacity = 1
        Else
            lblEnchant.IsEnabled = False
            lblEnchant.Opacity = 0
        End If
        xGemBonus = m.GemBonusDB


        'Mainframe.EquipmentList.Add(Me)
        Item = New Item(Me.Mainframe, 0)
        initiated = True
    End Sub
    Sub DisplayEnchant()
        Dim xmlDB As XDocument = Mainframe.EnchantDB
        Dim xmlItem As New XDocument
        If Item.Enchant.Id <> 0 Then
            lblEnchant.Content = Item.Enchant.name
            lblEnchant.Foreground = New SolidColorBrush(Colors.Green)
        Else

            If (From el In xmlDB.Elements("enchant").Elements
                        Where el.Element("slot").Value = SlotId And (el.Element("reqskill").Value = "" Or el.Element("reqskill").Value = 0 Or el.Element("reqskill").Value = GetSkillID(Mainframe.ParentFrame.cmbSkill1.SelectedItem) Or el.Element("reqskill").Value = GetSkillID(Mainframe.ParentFrame.cmbSkill2.SelectedItem))
                        ).Count > 0 Then
                lblEnchant.Content = "No Enchant"
                lblEnchant.Foreground = New SolidColorBrush(Colors.Black)
            Else
                lblEnchant.Content = ""
            End If



        End If
        Mainframe.ParentFrame.GetStats()
    End Sub
    Sub DisplayGem()
        lblGem1.Content = Item.gem1.name
        lblGem2.Content = Item.gem2.name
        lblGem3.Content = Item.gem3.name

        lblGem1.Background = Nothing
        lblGem2.Background = Nothing
        lblGem3.Background = Nothing

        If Item.gem1.ColorId <> 0 Then
            lblGemColor1.Width = 10
            lblGemColor1.Background = Item.gem1.GemSlotColor
            lblGem1.Background = New SolidColorBrush(Item.gem1.Color)
            If Item.gem1.IsGemrightColor Then
                lblGemColor1.Content = "X"
            Else
                lblGemColor1.Content = " "
            End If
        Else
            lblGemColor1.Content = ""
            lblGemColor1.Background = Nothing
        End If

        If Item.gem2.ColorId <> 0 Then
            lblGemColor2.Width = 10
            lblGemColor2.Background = Item.gem2.GemSlotColor
            lblGem2.Background = New SolidColorBrush(Item.gem2.Color)
            If Item.gem2.IsGemrightColor Then
                lblGemColor2.Content = "X"
            Else
                lblGemColor2.Content = " "
            End If
        Else
            lblGemColor2.Content = ""
            lblGemColor2.Background = Nothing
        End If

        If Item.gem3.ColorId <> 0 Then
            lblGemColor3.Width = 10
            lblGemColor3.Background = Item.gem3.GemSlotColor
            lblGem3.Background = New SolidColorBrush(Item.gem3.Color)
            If Item.gem3.IsGemrightColor Then
                lblGemColor3.Content = "X"
            Else
                lblGemColor3.Content = " "
            End If
        Else
            lblGemColor3.Content = ""
            lblGemColor3.Background = Nothing
        End If

        If Item.IsGembonusActif Then
            lblBonus.Opacity = 1
        Else
            lblBonus.Opacity = 0.5
        End If

        Mainframe.ParentFrame.GetStats()
    End Sub
    Sub EquipmentClick(ByVal sender As Object, ByVal e As EventArgs)
        'Try
        Dim GS As GearSelector
        GS = Mainframe.GearSelector

        GS.LoadItem(Me.SlotId)
        GS.SelectedItem = "-1"
        AddHandler GS.Closing, AddressOf GearS_close
        GS.Show() 'Dialog(Me)

    End Sub
    Sub DisplayItem()
        txtReforge.Text = 0
        cmbReforgeFrom.Items.Clear()
        stkStats.Children.Clear()
        Dim lbl As Label
        Me.Equipment.Content = Item.name & "(" & Item.ilvl & ")"
        If Item.heroic = 1 Then
            lblHeroic.Content = "Heroic"
            lblHeroic.Foreground = New SolidColorBrush(Colors.Green)
        Else
            lblHeroic.Content = "Normal"
            lblHeroic.Foreground = New SolidColorBrush(Colors.Black)
        End If
        Dim i As Integer = 0
        If Item.BonusArmor <> 0 Then
            lblArmor.Content = "Armor = " & Item.Armor & "(+" & Item.BonusArmor & ")"
        Else
            lblArmor.Content = "Armor = " & Item.Armor
        End If


        If Item.Stamina <> 0 Then
            lbl = New Label
            lbl.Content = "Stamina = " & Item.Stamina
            stkStats.Children.Add(lbl)
        End If

        If Item.Strength <> 0 Then
            lbl = New Label
            lbl.Content = "Strength = " & Item.Strength
            stkStats.Children.Add(lbl)
        End If


        If Item.Agility <> 0 Then
            lbl = New Label
            lbl.Content = "Agility = " & Item.Agility
            stkStats.Children.Add(lbl)
        End If

        If Item.AttackPower <> 0 Then
            lbl = New Label
            lbl.Content = "Attack Power = " & Item.AttackPower
            stkStats.Children.Add(lbl)
        End If


        If Item.HasteRating <> 0 Then
            lbl = New Label
            lbl.Content = "Haste Rating = " & Item.HasteRating
            stkStats.Children.Add(lbl)
            cmbReforgeFrom.Items.Add("Haste")
            cmbReforgeTo.Items.Remove("Haste")
        End If

        If Item.CritRating <> 0 Then
            lbl = New Label
            lbl.Content = "Crit Rating = " & Item.CritRating
            stkStats.Children.Add(lbl)
            cmbReforgeFrom.Items.Add("Crit")
            cmbReforgeTo.Items.Remove("Crit")
        End If
        If Item.ArmorPenetrationRating <> 0 Then
            lbl = New Label
            lbl.Content = "Armor Penetration Rating = " & Item.ArmorPenetrationRating
            stkStats.Children.Add(lbl)


        End If
        If Item.HitRating <> 0 Then
            lbl = New Label
            lbl.Content = "Hit Rating = " & Item.HitRating
            stkStats.Children.Add(lbl)


            cmbReforgeFrom.Items.Add("Hit")
            cmbReforgeTo.Items.Remove("Hit")
        End If

        If Item.ExpertiseRating <> 0 Then
            lbl = New Label
            lbl.Content = "Expertise Rating = " & Item.ExpertiseRating
            stkStats.Children.Add(lbl)
            cmbReforgeFrom.Items.Add("Exp")
            cmbReforgeTo.Items.Remove("Exp")
        End If

        If Item.MasteryRating <> 0 Then
            lbl = New Label
            lbl.Content = "Mastery Rating = " & Item.MasteryRating
            stkStats.Children.Add(lbl)


            cmbReforgeFrom.Items.Add("Mast")
            cmbReforgeTo.Items.Remove("Mast")
        End If

        If Item.DodgeRating <> 0 Then
            lbl = New Label
            lbl.Content = "Dodge Rating = " & Item.DodgeRating
            stkStats.Children.Add(lbl)


            cmbReforgeFrom.Items.Add("Dodge")
            cmbReforgeTo.Items.Remove("Dodge")
        End If

        If Item.ParryRating <> 0 Then
            lbl = New Label
            lbl.Content = "Parry Rating = " & Item.ParryRating
            stkStats.Children.Add(lbl)
            cmbReforgeFrom.Items.Add("Parry")
            cmbReforgeTo.Items.Remove("Parry")
        End If

        If Item.gem1.GemSlotColorName <> "" Then
            lblGemColor1.Content = " "
            lblGemColor1.Background = Item.gem1.GemSlotColor
            lblGemColor1.Width = 10
            lblGemColor1.IsEnabled = True
        Else
            lblGemColor1.Content = ""
            lblGem1.Content = ""
            lblGemColor1.Background = Item.gem1.GemSlotColor
            lblGemColor1.IsEnabled = False
        End If

        If Item.gem2.GemSlotColorName <> "" Then
            lblGemColor2.Content = " "
            lblGemColor2.Background = Item.gem2.GemSlotColor
            lblGemColor2.Width = 10
            lblGemColor2.IsEnabled = True
        Else
            lblGemColor2.Content = ""
            lblGem2.Content = ""
            lblGemColor2.Background = Item.gem2.GemSlotColor
            lblGemColor2.IsEnabled = False
        End If

        If Item.gem3.GemSlotColorName <> "" Then
            lblGemColor3.Content = " "
            lblGemColor3.Background = Item.gem3.GemSlotColor
            lblGemColor3.Width = 10
            lblGemColor3.IsEnabled = True
        Else
            lblGemColor3.Content = ""
            lblGemColor3.Background = Item.gem3.GemSlotColor
            lblGem3.Content = ""
            lblGemColor3.IsEnabled = False
        End If
        Try
            If xGemBonus Is Nothing Then xGemBonus = Mainframe.GemBonusDB

            If Item.gembonus <> 0 Then
                lblBonus.Content = "+" & (From el In xGemBonus.<bonus>.Elements
                                Where el.<id>.Value = Item.gembonus
                                ).First.<Desc>.Value
            Else
                lblBonus.Content = ""
            End If

        Catch ex As Exception
            Log.Log(ex.StackTrace, logging.Level.ERR)
            lblBonus.Content = "<bonus>"
        End Try



        If cmbReforgeFrom.Items.Count = 0 Then
            cmbReforgeFrom.IsEnabled = False
            cmbReforgeFrom.SelectedItem = Nothing
            cmbReforgeTo.SelectedItem = Nothing
            cmbReforgeTo.IsEnabled = False
            btRemove.IsEnabled = False
            txtReforge.Text = 0
            txtReforge.IsEnabled = False
        Else
            cmbReforgeFrom.IsEnabled = True
            cmbReforgeTo.IsEnabled = True
            txtReforge.IsEnabled = True
            btRemove.IsEnabled = True
        End If
        'DisplayGem()
        'DisplayEnchant()
        'Mainframe.ParentFrame.GetStats()
    End Sub

    Private Sub lblGem_MouseEnter(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles lblGem2.MouseLeftButtonUp, lblGem3.MouseLeftButtonUp, lblGem1.MouseLeftButtonUp, lblGemColor1.MouseLeftButtonDown, lblGemColor2.MouseLeftButtonDown, lblGemColor3.MouseLeftButtonDown
        GemS = Mainframe.GemSelector
        Dim s As String
        s = sender.name
        Select Case Strings.Right(s, 1)
            Case 1
                GemS.LoadItem(1, SlotId, Item.gem1.ColorId)
            Case 2
                GemS.LoadItem(2, SlotId, Item.gem2.ColorId)
            Case 3
                GemS.LoadItem(3, SlotId, Item.gem3.ColorId)
        End Select
        GemS.SelectedItem = "-1"
        GemS.Show()



    End Sub

    Private Sub Equipment_MouseLeftButtonDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Equipment.MouseLeftButtonDown

        GearS = Mainframe.GearSelector
        GearS.LoadItem(Me.SlotId)
        GearS.SelectedItem = "-1"
        AddHandler GearS.Closing, AddressOf GearS_close
        GearS.Show()
    End Sub

    Private Sub lblEnchant_MouseLeftButtonUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles lblEnchant.MouseLeftButtonUp
        EnchantS = Mainframe.EnchantSelector
        Dim s As String
        s = sender.name
        EnchantS.LoadItem(SlotId)
        EnchantS.SelectedItem = "-1"
        EnchantS.Show() ' (Me)

    End Sub

    Private Sub EnchantS_close(ByVal sender As System.Object, ByVal e As EventArgs) Handles EnchantS.Closing
        If EnchantS.Slot <> Me.SlotId Then Exit Sub
        If EnchantS.SelectedItem <> "-1" Then
            Item.Enchant.Attach(EnchantS.SelectedItem)
            DisplayEnchant()
        End If
    End Sub
    Private Sub GemS_close(ByVal sender As System.Object, ByVal e As EventArgs) Handles GemS.Closing
        If GemS.Slot <> Me.SlotId Then Exit Sub

        If GemS.DialogResult Then
            If GemS.SelectedItem <> "-1" Then
                Select Case GemS.GemNum
                    Case 1
                        Item.gem1.Attach(GemS.SelectedItem)
                    Case 2
                        Item.gem2.Attach(GemS.SelectedItem)
                    Case 3
                        Item.gem3.Attach(GemS.SelectedItem)
                End Select
            End If
        End If
        DisplayGem()
    End Sub
    Private Sub GearS_close(ByVal sender As System.Object, ByVal e As EventArgs)
        If GearS.Slot <> Me.SlotId Then Exit Sub

        RemoveHandler GearS.Closing, AddressOf GearS_close
        If GearS.DialogResult = True Then
            If GearS.SelectedItem <> "-1" Then
                Item.LoadItem(GearS.SelectedItem)
                DisplayItem()
                Mainframe.ParentFrame.GetStats()
            End If
        End If


    End Sub

    Private Sub cmbReforgeFrom_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles cmbReforgeFrom.SelectionChanged
        If inLoad Then Exit Sub
        txtReforge.Text = GetValueToReforge(cmbReforgeFrom.SelectedValue)

        If IsNothing(cmbReforgeFrom.SelectedValue) = False Then
            Item.ReForgingFrom = cmbReforgeFrom.SelectedValue
            Item.ReForgingvalue = txtReforge.Text
        Else
            Item.ReForgingFrom = ""
            Item.ReForgingvalue = 0
        End If

        Mainframe.ParentFrame.GetStats()

    End Sub

    Function GetValueToReforge(ByVal stat As String)
        Select Case stat
            Case "Crit"
                Return Decimal.Truncate(Item.CritRating * 0.4)
            Case "Exp"
                Return Decimal.Truncate(Item.ExpertiseRating * 0.4)
            Case "Haste"
                Return Decimal.Truncate(Item.HasteRating * 0.4)
            Case "Hit"
                Return Decimal.Truncate(Item.HitRating * 0.4)
            Case "Mast"
                Return Decimal.Truncate(Item.MasteryRating * 0.4)
            Case "Dodge"
                Return Decimal.Truncate(Item.DodgeRating * 0.4)
            Case "Parry"
                Return Decimal.Truncate(Item.ParryRating * 0.4)
            Case Else
                Return 0
        End Select
    End Function

    
    

    Private Sub cmbReforgeTo_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles cmbReforgeTo.SelectionChanged
        If inLoad Then Exit Sub
        If IsNothing(cmbReforgeTo.SelectedValue) = False Then
            Item.ReForgingTo = cmbReforgeTo.SelectedValue
        Else
            Item.ReForgingTo = ""
        End If
        Mainframe.ParentFrame.GetStats()
    End Sub

    Private Sub btRemove_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btRemove.Click
        txtReforge.Text = 0
        cmbReforgeTo.SelectedItem = Nothing
        cmbReforgeFrom.SelectedItem = Nothing
        Item.ReForgingvalue = 0
        displayReforge()
        Mainframe.ParentFrame.GetStats()
    End Sub
End Class
