'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 16/03/2010
' Heure: 11:04
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class EquipSlot2
    Inherits System.Windows.Forms.GroupBox


    Friend SlotId As Integer

    Friend lblBonus As Label
    Friend Item As Item


    Protected Mainframe As GearSelectorMainForm




    Sub init(ByVal m As GearSelectorMainForm, ByVal slot As Integer)
        Mainframe = m
        Me.SlotId = slot
        Mainframe.Controls.Add(Me)
        Mainframe.EquipmentList.Add(Me)

        Me.Height = 80
        Me.Width = 300

        Equipment = New Label
        lblGem1 = New Label
        lblGem2 = New Label
        lblGem3 = New Label

        lblGemcolor1 = New Label
        lblGemcolor2 = New Label
        lblGemcolor3 = New Label

        Me.Controls.Add(Equipment)
        Me.Controls.Add(lblGem1)
        Me.Controls.Add(lblGem2)
        Me.Controls.Add(lblGem3)

        Me.Controls.Add(lblGemcolor1)
        Me.Controls.Add(lblGemcolor2)
        Me.Controls.Add(lblGemcolor3)


        With Equipment
            .Enabled = True
            .Location = New System.Drawing.Point(20, 15)
            .Size = New System.Drawing.Size(10, 180)
            .AutoSize = True
            .TabIndex = 0
            .Text = Me.Text
            .Font = New Font("Arial", 8, FontStyle.Bold)
        End With
        AddHandler Me.Equipment.Click, AddressOf Me.EquipmentClick

        With lblGemcolor1
            .Enabled = True
            .Location = New System.Drawing.Point(20, 30)
            .Size = New System.Drawing.Size(10, 10)
            .AutoSize = False
            .TabIndex = 0
            .Text = ""
            .Name = "lblGemcolor1"
            AddHandler .Click, AddressOf Me.GemClick
        End With

        With lblGemcolor2
            .Enabled = True
            .Location = New System.Drawing.Point(20, 45)
            .Size = New System.Drawing.Size(10, 10)
            .AutoSize = False
            .TabIndex = 0
            .Text = ""
            .Name = "lblGemcolor2"
            AddHandler .Click, AddressOf Me.GemClick
        End With

        With lblGemcolor3
            .Enabled = True
            .Location = New System.Drawing.Point(20, 60)
            .Size = New System.Drawing.Size(10, 10)
            .AutoSize = False
            .TabIndex = 0
            .Text = ""
            .Name = "lblGemcolor3"
            AddHandler .Click, AddressOf Me.GemClick
        End With




        With lblGem1
            .Enabled = True
            .Location = New System.Drawing.Point(40, 30)
            .Size = New System.Drawing.Size(10, 180)
            .AutoSize = True
            .TabIndex = 0
            .Text = ""
            .Name = "lblGem1"
            AddHandler .Click, AddressOf Me.GemClick
        End With


        With lblGem2
            .Enabled = True
            .Location = New System.Drawing.Point(40, 45)
            .Size = New System.Drawing.Size(10, 180)
            .AutoSize = True
            .TabIndex = 0
            .Text = ""
            .Name = "lblGem2"
            AddHandler .Click, AddressOf Me.GemClick
        End With
        With lblGem3
            .Enabled = True
            .Location = New System.Drawing.Point(40, 60)
            .Size = New System.Drawing.Size(10, 180)
            .AutoSize = True
            .TabIndex = 0
            .Text = ""
            .Name = "lblGem3"
            AddHandler .Click, AddressOf Me.GemClick
        End With


        lblBonus = New Label
        Me.Controls.Add(lblBonus)
        With lblBonus
            .Enabled = False
            .Location = New System.Drawing.Point(200, 30)
            .AutoSize = True
            .Text = ""
            .Name = "lblBonus"
        End With
        lblEnchant = New Label
        Me.Controls.Add(lblEnchant)
        With lblEnchant
            .Enabled = True
            .Location = New System.Drawing.Point(200, 45)
            .AutoSize = True
            .Text = "Enchant"
            .Name = "lblEnchant"
            AddHandler .Click, AddressOf Me.EnchantClick
        End With
        If m.EnchantDB.SelectNodes("/enchant/item[slot=" & slot & "]").Count = 0 Then
            lblEnchant.Enabled = False
            lblEnchant.Opacity = False
        Else
            lblEnchant.Enabled = True
            lblEnchant.Opacity = True
        End If

        Item = New Item(Me.Mainframe, 0)
        xGemBonus = m.GemBonusDB
    End Sub

    Sub GemClick(ByVal sender As Object, ByVal e As EventArgs)
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
        GS.ShowDialog(Me)

        If GS.DialogResult = True Then
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

    Sub EnchantClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim GS As EnchantSelector
        GS = Mainframe.EnchantSelector
        Dim s As String
        s = sender.name
        GS.LoadItem(SlotId)
        GS.SelectedItem = "-1"
        GS.ShowDialog(Me)
        If GS.SelectedItem <> "-1" Then
            Item.Enchant.Attach(GS.SelectedItem)
        End If

        DisplayEnchant()
        Me.Focus()
    End Sub



    Sub DisplayEnchant()
        Dim xmlDB As xdocument = Mainframe.EnchantDB
        Dim xmlItem As New xdocument
        If Item.Enchant.Id <> 0 Then
            lblEnchant.text = Item.Enchant.name
        Else
            lblEnchant.text = "Enchant"
        End If
        Mainframe.GetStats()
    End Sub


    Sub DisplayGem()
        lblGem1.Text = Item.gem1.name
        lblGem2.Text = Item.gem2.name
        lblGem3.Text = Item.gem3.name
        If Item.gem1.ColorId <> 0 Then
            lblGemcolor1.Width = 10
            lblGemcolor1.backcolor = Item.gem1.GemSlotColor
            If Item.gem1.IsGemrightColor Then
                lblGemcolor1.Text = "X"
            Else
                lblGemcolor1.Text = " "
            End If
        Else
            lblGemcolor1.Text = ""
            lblGemcolor1.backcolor = Color.Transparent
        End If

        If Item.gem2.ColorId <> 0 Then
            lblGemcolor2.Width = 10
            lblGemcolor2.backcolor = Item.gem2.GemSlotColor
            If Item.gem2.IsGemrightColor Then
                lblGemcolor2.Text = "X"
            Else
                lblGemcolor2.Text = " "
            End If
        Else
            lblGemcolor2.Text = ""
            lblGemcolor2.backcolor = Color.Transparent
        End If

        If Item.gem3.ColorId <> 0 Then
            lblGemcolor3.Width = 10
            lblGemcolor3.backcolor = Item.gem3.GemSlotColor
            If Item.gem3.IsGemrightColor Then
                lblGemcolor3.Text = "X"
            Else
                lblGemcolor3.Text = " "
            End If
        Else
            lblGemcolor3.Text = ""
            lblGemcolor3.backcolor = Color.Transparent
        End If
        lblBonus.Enabled = Item.IsGembonusActif
        Mainframe.GetStats()
    End Sub

    Sub EquipmentClick(ByVal sender As Object, ByVal e As EventArgs)
        'Try
        Dim GS As GearSelector
        GS = Mainframe.GearSelector

        GS.LoadItem(Me.SlotId)
        GS.SelectedItem = "-1"
        GS.ShowDialog(Me)
        If GS.DialogResult = True Then
            If GS.SelectedItem <> "-1" Then
                Item.LoadItem(GS.SelectedItem)
                DisplayItem()
            End If
        End If
        Me.Focus()
    End Sub






    Sub DisplayItem()

        Me.Equipment.Text = Item.name & "(" & Item.ilvl & ")"
        If Item.gem1.GemSlotColorName <> "" Then
            lblGemcolor1.Text = " "
            lblGemcolor1.BackColor = Item.gem1.GemSlotColor
            lblGemcolor1.Width = 10
            lblGemcolor1.Enabled = True
        Else
            lblGemcolor1.Text = ""
            lblGem1.Text = ""
            lblGemcolor1.BackColor = Item.gem1.GemSlotColor
            lblGemcolor1.Enabled = False
        End If

        If Item.gem2.GemSlotColorName <> "" Then
            lblGemcolor2.Text = " "
            lblGemcolor2.BackColor = Item.gem2.GemSlotColor
            lblGemcolor2.Width = 10
            lblGemcolor2.Enabled = True
        Else
            lblGemcolor2.Text = ""
            lblGem2.Text = ""
            lblGemcolor2.BackColor = Item.gem2.GemSlotColor
            lblGemcolor2.Enabled = False
        End If

        If Item.gem3.GemSlotColorName <> "" Then
            lblGemcolor3.Text = " "
            lblGemcolor3.BackColor = Item.gem3.GemSlotColor
            lblGemcolor3.Width = 10
            lblGemcolor3.Enabled = True
        Else
            lblGemcolor3.Text = ""
            lblGemcolor3.BackColor = Item.gem3.GemSlotColor
            lblGem3.Text = ""
            lblGemcolor3.Enabled = False
        End If
        Try
            lblBonus.Text = xGemBonus.Element("/bonus/item[id=" & Item.gembonus & "]/Desc").Value
        Catch
            lblBonus.Text = ""
        End Try
        DisplayGem()
        DisplayEnchant()
        Mainframe.GetStats()
    End Sub


End Class
