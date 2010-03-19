'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 2/22/2010
' Heure: 4:32 PM
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Partial Class GearSelector
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the control.
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
		Me.textBox1 = New System.Windows.Forms.TextBox
		Me.listView1 = New System.Windows.Forms.ListView
		Me.cmdClear = New System.Windows.Forms.Button
		Me.SuspendLayout
		'
		'textBox1
		'
		Me.textBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.textBox1.Location = New System.Drawing.Point(3, 16)
		Me.textBox1.Name = "textBox1"
		Me.textBox1.Size = New System.Drawing.Size(645, 20)
		Me.textBox1.TabIndex = 0
		AddHandler Me.textBox1.TextChanged, AddressOf Me.TextBox1TextChanged
		'
		'listView1
		'
		Me.listView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.listView1.Location = New System.Drawing.Point(3, 51)
		Me.listView1.Name = "listView1"
		Me.listView1.Size = New System.Drawing.Size(735, 487)
		Me.listView1.TabIndex = 2
		Me.listView1.UseCompatibleStateImageBehavior = false
		Me.listView1.View = System.Windows.Forms.View.Details
		AddHandler Me.listView1.SelectedIndexChanged, AddressOf Me.ListView1SelectedIndexChanged
		AddHandler Me.listView1.ColumnClick, AddressOf Me.ListView1ColumnClick
		'
		'cmdClear
		'
		Me.cmdClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdClear.Location = New System.Drawing.Point(654, 16)
		Me.cmdClear.Name = "cmdClear"
		Me.cmdClear.Size = New System.Drawing.Size(75, 23)
		Me.cmdClear.TabIndex = 4
		Me.cmdClear.Text = "None"
		Me.cmdClear.UseVisualStyleBackColor = true
		AddHandler Me.cmdClear.Click, AddressOf Me.CmdClearClick
		'
		'GearSelector
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(750, 550)
		Me.Controls.Add(Me.cmdClear)
		Me.Controls.Add(Me.listView1)
		Me.Controls.Add(Me.textBox1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
		Me.Name = "GearSelector"
		AddHandler Load, AddressOf Me.GearSelectorLoad
		AddHandler Closing, AddressOf Me.GearSelectorClose
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private cmdClear As System.Windows.Forms.Button
	Private listView1 As System.Windows.Forms.ListView
	Private textBox1 As System.Windows.Forms.TextBox
End Class
