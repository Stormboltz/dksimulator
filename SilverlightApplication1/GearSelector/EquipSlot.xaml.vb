Imports System.Xml.Linq
Imports System.Linq
Partial Public Class EquipSlot
    Inherits UserControl




    Public Sub New()
        InitializeComponent()
    End Sub

    Friend xGemBonus As XDocument
    Friend SlotId As Integer
    Friend Item As Item
    Friend text As String
    Friend WithEvents GS As EnchantSelector


    Protected Mainframe As GearSelectorMainForm

    Sub init(ByVal m As GearSelectorMainForm, ByVal slot As Integer)
        Mainframe = m
        Me.SlotId = slot
        Mainframe.EquipmentList.Add(Me)
        Dim query = From c As XElement In m.EnchantDB.Element("enchant").Elements Where c.Element("slot").Value = slot
        If query.Count > 0 Then
            lblEnchant.IsEnabled = True
            lblEnchant.Opacity = 1
        Else
            lblEnchant.IsEnabled = False
            lblEnchant.Opacity = 0
        End If

        Item = New Item(Me.Mainframe, 0)
        xGemBonus = m.GemBonusDB
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
        GS.Show() 'Dialog(Me)
        If GS.DialogResult = True Then
            If GS.SelectedItem <> "-1" Then
                Item.LoadItem(GS.SelectedItem)
                DisplayItem()
            End If
        End If
        Me.Focus()
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
            


        Catch
            lblBonus.Content = ""
        End Try
        DisplayGem()
        DisplayEnchant()
        Mainframe.GetStats()
    End Sub

    Private Sub lblGem_MouseEnter(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles lblGem2.MouseLeftButtonUp, lblGem3.MouseLeftButtonUp, lblGem1.MouseLeftButtonUp
        Dim GS As GemSelector
        GS = Mainframe.GemSelector
        Dim s As String
        s = sender.name

        Select Case Strings.Right(s, 1)
            Case 1
                GS.LoadItem(Item.gem1.ColorId)
            Case 2
                GS.LoadItem(Item.gem2.ColorId)
            Case 3
                GS.LoadItem(Item.gem3.ColorId)
        End Select
        GS.SelectedItem = "-1"
        GS.Show()


        If GS.DialogResult Then
            If GS.SelectedItem <> "-1" Then
                Select Case Strings.Right(s, 1)
                    Case 1
                        Item.gem1.Attach(GS.SelectedItem)
                    Case 2
                        Item.gem2.Attach(GS.SelectedItem)
                    Case 3
                        Item.gem3.Attach(GS.SelectedItem)
                End Select
            End If
        End If
        Me.Focus()
        DisplayGem()
    End Sub

    Private Sub Equipment_MouseLeftButtonDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Equipment.MouseLeftButtonDown
        Dim GS As GearSelector
        GS = Mainframe.GearSelector
        GS.LoadItem(Me.SlotId)
        GS.SelectedItem = "-1"
        GS.Show()
    End Sub

    Private Sub lblEnchant_MouseLeftButtonUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles lblEnchant.MouseLeftButtonUp
        GS = Mainframe.EnchantSelector
        Dim s As String
        s = sender.name
        GS.LoadItem(SlotId)
        GS.SelectedItem = "-1"
        GS.Show() ' (Me)
     
    End Sub

    Private Sub GS_close(ByVal sender As System.Object, ByVal e As EventArgs) Handles GS.Closing
        If GS.Slot <> Me.SlotId Then Exit Sub
        If GS.SelectedItem <> "-1" Then
            Item.Enchant.Attach(GS.SelectedItem)
            DisplayEnchant()

        End If
    End Sub
End Class
