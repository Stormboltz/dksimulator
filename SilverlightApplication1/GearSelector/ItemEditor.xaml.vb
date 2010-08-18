Imports System.Xml.Linq
Imports System.Linq
Partial Public Class ItemEditor
    Inherits UserControl

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
        Mainframe = VSlot.Mainframe
        Me.SlotId = VSlot.SlotId
        Item = VSlot.Item
        Origin = VSlot.Name
        DisplayItem()
        DisplayEnchant()
        DisplayGem()
        displayReforgingFrom()
        displayReforgingTo()
    End Sub

    Sub displayReforgingFrom()
        cmbReforgeFrom.Items.Clear()
    End Sub
    Sub displayReforgingTo()
        cmbReforgeTo.Items.Clear()
        cmbReforgeTo.Items.Add("Haste")
        cmbReforgeTo.Items.Add("Crit")
        cmbReforgeTo.Items.Add("Hit")
        cmbReforgeTo.Items.Add("Exp")
        cmbReforgeTo.Items.Add("Mast")
        cmbReforgeTo.Items.Add("Dodge")
        cmbReforgeTo.Items.Add("Parry")
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
        Me.Equipment.Content = Item.name & "(" & Item.ilvl & ")"
        If Item.heroic = 1 Then
            lblHeroic.Content = "Heroic"
            lblHeroic.Foreground = New SolidColorBrush(Colors.Green)
        Else
            lblHeroic.Content = "Normal"
            lblHeroic.Foreground = New SolidColorBrush(Colors.Black)
        End If
        Dim i As Integer = 0
        Dim col As New Collections.Generic.List(Of String)
        If Item.Strength <> 0 Then col.Add("Strength = " & Item.Strength)
        If Item.Agility <> 0 Then col.Add("Agility = " & Item.Agility)
        If Item.HasteRating <> 0 Then col.Add("Haste Rating = " & Item.HasteRating)
        If Item.AttackPower <> 0 Then col.Add("Attack Power = " & Item.AttackPower)
        If Item.CritRating <> 0 Then col.Add("Crit Rating = " & Item.CritRating)
        If Item.HitRating <> 0 Then col.Add("Hit Rating = " & Item.HitRating)
        If Item.ArmorPenetrationRating <> 0 Then col.Add("Armor Penetration Rating = " & Item.ArmorPenetrationRating)
        If Item.ExpertiseRating <> 0 Then col.Add("Expertise Rating = " & Item.ExpertiseRating)
        lblStat1.Content = ""
        lblStat2.Content = ""
        lblStat3.Content = ""
        lblStat4.Content = ""
        lblStat5.Content = ""

        Dim ii As Integer
        ii = col.Count
        Select Case ii
            Case 1
                lblStat1.Content = col(0)
            Case 2
                lblStat1.Content = col(0)
                lblStat2.Content = col(1)
            Case 3
                lblStat1.Content = col(0)
                lblStat2.Content = col(1)
                lblStat3.Content = col(2)
            Case 4
                lblStat1.Content = col(0)
                lblStat2.Content = col(1)
                lblStat3.Content = col(2)
                lblStat4.Content = col(3)
            Case 5
                lblStat1.Content = col(0)
                lblStat2.Content = col(1)
                lblStat3.Content = col(2)
                lblStat4.Content = col(3)
                lblStat5.Content = col(4)
        End Select

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
                lblBonus.Content = (From el In xGemBonus.Element("bonus").Elements
                                Where el.Element("id").Value = Item.gembonus
                                Select el).First.Element("Desc").Value
            End If



        Catch ex As Exception
            Log.Log(ex.StackTrace, logging.Level.ERR)
            lblBonus.Content = "<bonus>"
        End Try
        DisplayGem()
        DisplayEnchant()
        Mainframe.ParentFrame.GetStats()
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
            End If
        End If


    End Sub
End Class
