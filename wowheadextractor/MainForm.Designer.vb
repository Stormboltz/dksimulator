'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 2/22/2010
' Heure: 10:41 AM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Partial Class MainForm
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
		Me.cmdExtrator = New System.Windows.Forms.Button
		Me.cmdLoadGear = New System.Windows.Forms.Button
		Me.cmdSave = New System.Windows.Forms.Button
		Me.lvCharacter = New System.Windows.Forms.ListView
		Me.Slot = New System.Windows.Forms.ColumnHeader
		Me.SlotID = New System.Windows.Forms.ColumnHeader
		Me.ItemName = New System.Windows.Forms.ColumnHeader
		Me.lvGem1 = New System.Windows.Forms.ListView
		Me.columnHeader3 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader4 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader2 = New System.Windows.Forms.ColumnHeader
		Me.lvGem2 = New System.Windows.Forms.ListView
		Me.columnHeader5 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader6 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader7 = New System.Windows.Forms.ColumnHeader
		Me.lvGem3 = New System.Windows.Forms.ListView
		Me.columnHeader8 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader9 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader10 = New System.Windows.Forms.ColumnHeader
		Me.lvGembonus = New System.Windows.Forms.ListView
		Me.columnHeader11 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader12 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader13 = New System.Windows.Forms.ColumnHeader
		Me.ItemId = New System.Windows.Forms.ColumnHeader
		Me.SuspendLayout
		'
		'cmdExtrator
		'
		Me.cmdExtrator.Location = New System.Drawing.Point(736, 578)
		Me.cmdExtrator.Name = "cmdExtrator"
		Me.cmdExtrator.Size = New System.Drawing.Size(75, 23)
		Me.cmdExtrator.TabIndex = 0
		Me.cmdExtrator.Text = "Extract"
		Me.cmdExtrator.UseVisualStyleBackColor = true
		AddHandler Me.cmdExtrator.Click, AddressOf Me.CmdExtratorClick
		'
		'cmdLoadGear
		'
		Me.cmdLoadGear.Location = New System.Drawing.Point(531, 578)
		Me.cmdLoadGear.Name = "cmdLoadGear"
		Me.cmdLoadGear.Size = New System.Drawing.Size(75, 23)
		Me.cmdLoadGear.TabIndex = 2
		Me.cmdLoadGear.Text = "Load"
		Me.cmdLoadGear.UseVisualStyleBackColor = true
		AddHandler Me.cmdLoadGear.Click, AddressOf Me.CmdLoadGearClick
		'
		'cmdSave
		'
		Me.cmdSave.Location = New System.Drawing.Point(612, 578)
		Me.cmdSave.Name = "cmdSave"
		Me.cmdSave.Size = New System.Drawing.Size(75, 23)
		Me.cmdSave.TabIndex = 4
		Me.cmdSave.Text = "Save"
		Me.cmdSave.UseVisualStyleBackColor = true
		AddHandler Me.cmdSave.Click, AddressOf Me.CmdSaveClick
		'
		'lvCharacter
		'
		Me.lvCharacter.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Slot, Me.SlotID, Me.ItemName, Me.ItemId})
		Me.lvCharacter.Location = New System.Drawing.Point(12, 12)
		Me.lvCharacter.Name = "lvCharacter"
		Me.lvCharacter.Size = New System.Drawing.Size(348, 439)
		Me.lvCharacter.TabIndex = 6
		Me.lvCharacter.UseCompatibleStateImageBehavior = false
		Me.lvCharacter.View = System.Windows.Forms.View.Details
		AddHandler Me.lvCharacter.SelectedIndexChanged, AddressOf Me.LvCharacterSelectedIndexChanged
		'
		'Slot
		'
		Me.Slot.Text = "Slot"
		Me.Slot.Width = 69
		'
		'SlotID
		'
		Me.SlotID.Text = "SlotID"
		Me.SlotID.Width = 0
		'
		'Name
		'
		Me.ItemName.Text = "ItemName"
		Me.ItemName.Width = 258
		'
		'lvGem1
		'
		Me.lvGem1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader3, Me.columnHeader4, Me.columnHeader2})
		Me.lvGem1.Location = New System.Drawing.Point(366, 12)
		Me.lvGem1.Name = "lvGem1"
		Me.lvGem1.Size = New System.Drawing.Size(92, 439)
		Me.lvGem1.TabIndex = 6
		Me.lvGem1.UseCompatibleStateImageBehavior = false
		Me.lvGem1.View = System.Windows.Forms.View.Details
		AddHandler Me.lvGem1.SelectedIndexChanged, AddressOf Me.LvGem1SelectedIndexChanged
		'
		'columnHeader3
		'
		Me.columnHeader3.DisplayIndex = 1
		Me.columnHeader3.Text = "SlotID"
		Me.columnHeader3.Width = 100
		'
		'columnHeader4
		'
		Me.columnHeader4.DisplayIndex = 2
		Me.columnHeader4.Text = "Name"
		Me.columnHeader4.Width = 0
		'
		'columnHeader2
		'
		Me.columnHeader2.DisplayIndex = 0
		Me.columnHeader2.Text = "Slot"
		Me.columnHeader2.Width = 0
		'
		'lvGem2
		'
		Me.lvGem2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader5, Me.columnHeader6, Me.columnHeader7})
		Me.lvGem2.Location = New System.Drawing.Point(464, 12)
		Me.lvGem2.Name = "lvGem2"
		Me.lvGem2.Size = New System.Drawing.Size(92, 439)
		Me.lvGem2.TabIndex = 6
		Me.lvGem2.UseCompatibleStateImageBehavior = false
		Me.lvGem2.View = System.Windows.Forms.View.Details
		AddHandler Me.lvGem2.SelectedIndexChanged, AddressOf Me.LvGem1SelectedIndexChanged
		'
		'columnHeader5
		'
		Me.columnHeader5.DisplayIndex = 1
		Me.columnHeader5.Text = "SlotID"
		Me.columnHeader5.Width = 100
		'
		'columnHeader6
		'
		Me.columnHeader6.DisplayIndex = 2
		Me.columnHeader6.Text = "Name"
		Me.columnHeader6.Width = 0
		'
		'columnHeader7
		'
		Me.columnHeader7.DisplayIndex = 0
		Me.columnHeader7.Text = "Slot"
		Me.columnHeader7.Width = 0
		'
		'lvGem3
		'
		Me.lvGem3.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader8, Me.columnHeader9, Me.columnHeader10})
		Me.lvGem3.Location = New System.Drawing.Point(562, 12)
		Me.lvGem3.Name = "lvGem3"
		Me.lvGem3.Size = New System.Drawing.Size(92, 439)
		Me.lvGem3.TabIndex = 6
		Me.lvGem3.UseCompatibleStateImageBehavior = false
		Me.lvGem3.View = System.Windows.Forms.View.Details
		AddHandler Me.lvGem3.SelectedIndexChanged, AddressOf Me.LvGem1SelectedIndexChanged
		'
		'columnHeader8
		'
		Me.columnHeader8.DisplayIndex = 1
		Me.columnHeader8.Text = "SlotID"
		Me.columnHeader8.Width = 100
		'
		'columnHeader9
		'
		Me.columnHeader9.DisplayIndex = 2
		Me.columnHeader9.Text = "Name"
		Me.columnHeader9.Width = 0
		'
		'columnHeader10
		'
		Me.columnHeader10.DisplayIndex = 0
		Me.columnHeader10.Text = "Slot"
		Me.columnHeader10.Width = 0
		'
		'lvGembonus
		'
		Me.lvGembonus.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader11, Me.columnHeader12, Me.columnHeader13})
		Me.lvGembonus.Location = New System.Drawing.Point(660, 12)
		Me.lvGembonus.Name = "lvGembonus"
		Me.lvGembonus.Size = New System.Drawing.Size(92, 439)
		Me.lvGembonus.TabIndex = 6
		Me.lvGembonus.UseCompatibleStateImageBehavior = false
		Me.lvGembonus.View = System.Windows.Forms.View.Details
		'
		'columnHeader11
		'
		Me.columnHeader11.DisplayIndex = 1
		Me.columnHeader11.Text = "SlotID"
		Me.columnHeader11.Width = 100
		'
		'columnHeader12
		'
		Me.columnHeader12.DisplayIndex = 2
		Me.columnHeader12.Text = "Name"
		Me.columnHeader12.Width = 0
		'
		'columnHeader13
		'
		Me.columnHeader13.DisplayIndex = 0
		Me.columnHeader13.Text = "Slot"
		Me.columnHeader13.Width = 0
		'
		'ItemId
		'
		Me.ItemId.Width = 0
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(823, 613)
		Me.Controls.Add(Me.lvGembonus)
		Me.Controls.Add(Me.lvGem3)
		Me.Controls.Add(Me.lvGem2)
		Me.Controls.Add(Me.lvGem1)
		Me.Controls.Add(Me.lvCharacter)
		Me.Controls.Add(Me.cmdSave)
		Me.Controls.Add(Me.cmdLoadGear)
		Me.Controls.Add(Me.cmdExtrator)
		Me.Name = "MainForm"
		Me.Text = "wowheadextractor"
		AddHandler Load, AddressOf Me.MainFormLoad
		Me.ResumeLayout(false)
	End Sub
	Private ItemId As System.Windows.Forms.ColumnHeader
	Private ItemName As System.Windows.Forms.ColumnHeader
	Private lvGem1 As System.Windows.Forms.ListView
	Private lvGem2 As System.Windows.Forms.ListView
	Private lvGem3 As System.Windows.Forms.ListView
	Private lvGembonus As System.Windows.Forms.ListView
	Private columnHeader13 As System.Windows.Forms.ColumnHeader
	Private columnHeader12 As System.Windows.Forms.ColumnHeader
	Private columnHeader11 As System.Windows.Forms.ColumnHeader
	Private columnHeader10 As System.Windows.Forms.ColumnHeader
	Private columnHeader9 As System.Windows.Forms.ColumnHeader
	Private columnHeader8 As System.Windows.Forms.ColumnHeader
	Private columnHeader7 As System.Windows.Forms.ColumnHeader
	Private columnHeader6 As System.Windows.Forms.ColumnHeader
	Private columnHeader5 As System.Windows.Forms.ColumnHeader
	Private columnHeader2 As System.Windows.Forms.ColumnHeader
	Private columnHeader4 As System.Windows.Forms.ColumnHeader
	Private columnHeader3 As System.Windows.Forms.ColumnHeader
	Private SlotID As System.Windows.Forms.ColumnHeader
	Private Slot As System.Windows.Forms.ColumnHeader
	Private lvCharacter As System.Windows.Forms.ListView
	Private cmdSave As System.Windows.Forms.Button
	Private cmdLoadGear As System.Windows.Forms.Button
	Private cmdExtrator As System.Windows.Forms.Button
	
	
	
	
End Class
