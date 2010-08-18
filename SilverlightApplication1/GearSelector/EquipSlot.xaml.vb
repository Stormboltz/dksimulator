Imports System.Xml.Linq
Imports System.Linq
Partial Public Class EquipSlot
    Inherits UserControl


    Public Sub New()
        InitializeComponent()
    End Sub
    Dim _Text As String
    Friend xGemBonus As XDocument
    Friend SlotId As Integer
    Friend Item As Item
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


        Mainframe.EquipmentList.Add(Me)
        Item = New Item(Me.Mainframe, 0)
        initiated = True
    End Sub

   
    Sub DisplayEnchant()
        Dim xmlDB As XDocument = Mainframe.EnchantDB
        Dim xmlItem As New XDocument
        If Item.Enchant.Id <> 0 Then
            lblEnchant.Content = Item.Enchant.name
        Else
            lblEnchant.Content = "Enchant"
        End If
        Mainframe.GetStats()
    End Sub


    Sub DisplayGem()
        lblGem1.Content = Item.gem1.name
        lblGem2.Content = Item.gem2.name
        lblGem3.Content = Item.gem3.name
        If Item.gem1.ColorId <> 0 Then
            lblGemcolor1.Width = 10
            lblGemcolor1.Background = Item.gem1.GemSlotColor
            If Item.gem1.IsGemrightColor Then
                lblGemcolor1.Content = "X"
            Else
                lblGemcolor1.Content = " "
            End If
        Else
            lblGemcolor1.Content = ""
            lblGemcolor1.Background = Nothing
        End If

        If Item.gem2.ColorId <> 0 Then
            lblGemcolor2.Width = 10
            lblGemcolor2.Background = Item.gem2.GemSlotColor
            If Item.gem2.IsGemrightColor Then
                lblGemcolor2.Content = "X"
            Else
                lblGemcolor2.Content = " "
            End If
        Else
            lblGemcolor2.Content = ""
            lblGemcolor2.Background = Nothing
        End If

        If Item.gem3.ColorId <> 0 Then
            lblGemcolor3.Width = 10
            lblGemcolor3.Background = Item.gem3.GemSlotColor
            If Item.gem3.IsGemrightColor Then
                lblGemcolor3.Content = "X"
            Else
                lblGemcolor3.Content = " "
            End If
        Else
            lblGemcolor3.Content = ""
            lblGemcolor3.Background = Nothing
        End If
        lblBonus.IsEnabled = Item.IsGembonusActif
        Mainframe.GetStats()
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
        If Item.gem1.GemSlotColorName <> "" Then
            lblGemcolor1.Content = " "
            lblGemcolor1.Background = Item.gem1.GemSlotColor
            lblGemcolor1.Width = 10
            lblGemcolor1.IsEnabled = True
        Else
            lblGemcolor1.Content = ""
            lblGem1.Content = ""
            lblGemcolor1.Background = Item.gem1.GemSlotColor
            lblGemcolor1.IsEnabled = False
        End If

        If Item.gem2.GemSlotColorName <> "" Then
            lblGemcolor2.Content = " "
            lblGemcolor2.Background = Item.gem2.GemSlotColor
            lblGemcolor2.Width = 10
            lblGemcolor2.IsEnabled = True
        Else
            lblGemcolor2.Content = ""
            lblGem2.Content = ""
            lblGemcolor2.Background = Item.gem2.GemSlotColor
            lblGemcolor2.IsEnabled = False
        End If

        If Item.gem3.GemSlotColorName <> "" Then
            lblGemcolor3.Content = " "
            lblGemcolor3.Background = Item.gem3.GemSlotColor
            lblGemcolor3.Width = 10
            lblGemcolor3.IsEnabled = True
        Else
            lblGemcolor3.Content = ""
            lblGemcolor3.Background = Item.gem3.GemSlotColor
            lblGem3.Content = ""
            lblGemcolor3.IsEnabled = False
        End If
        Try
            If Item.gembonus <> 0 Then
                lblBonus.Content = (From el In xGemBonus.Element("bonus").Elements
                                Where el.Element("id").Value = Item.gembonus
                                Select el).First.Element("Desc").Value
            End If
            


        Catch ex As execption
            Log.Log(ex.StackTrace, logging.Level.ERR)
            lblBonus.Content = ""
        End Try
        DisplayGem()
        DisplayEnchant()
        Mainframe.GetStats()
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
