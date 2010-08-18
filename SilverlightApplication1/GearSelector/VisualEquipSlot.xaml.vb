Imports System.Xml.Linq
Imports System.Windows.Media.Imaging

Partial Public Class VisualEquipSlot
    Inherits UserControl

    Public Sub New()
        InitializeComponent()
    End Sub
    Friend Mainframe As FrmGearSelector
    Friend Text As String
    Friend xGemBonus As XDocument
    Friend SlotId As Integer
    Friend Item As Item
    Protected initiated As Boolean = False
    Friend WithEvents GearS As GearSelector




    Sub init(ByVal m As FrmGearSelector, ByVal slot As Integer)
        Mainframe = m
        Me.SlotId = slot
        xGemBonus = m.GemBonusDB
        Mainframe.EquipmentList.Add(Me)
        Item = New Item(Me.Mainframe, 0)
        initiated = True
    End Sub

    Sub DisplayItem()
        Dim bmi As BitmapImage = New BitmapImage(New Uri("../images/icons/large/" & Me.Item.icon & ".jpg", UriKind.Relative))
        Me.ImSlot.Source = bmi
        Mainframe.ItemEditor1.Load(Me)
    End Sub

    Private Sub cmdEquipSlot_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdEquipSlot.Click
        GearS = Mainframe.GearSelector
        GearS.LoadItem(Me.SlotId)
        GearS.SelectedItem = "-1"
        AddHandler GearS.Closing, AddressOf GearS_close
        GearS.Show()
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


    Private Sub cmdEquipSlot_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles cmdEquipSlot.MouseMove
        Try
            If IsNothing(Mainframe) Then Exit Sub
            If Mainframe.ItemEditor1.Origin <> Me.Name Then
                Me.Cursor = Cursors.Wait
                Mainframe.ItemEditor1.Load(Me)
                Me.Cursor = Cursors.Arrow
            End If
        Catch ex As Exception
            Log.Log(ex.StackTrace, logging.Level.ERR)
        End Try
    End Sub
End Class
