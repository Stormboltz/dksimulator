'
' Created by SharpDevelop.
' User: Fabien
' Date: 28/03/2009
' Time: 18:22
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class frmArmoryImport
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
		Me.txtCharacter = New System.Windows.Forms.TextBox
		Me.btOk = New System.Windows.Forms.Button
		Me.txtRealm = New System.Windows.Forms.TextBox
		Me.cmbRegion = New System.Windows.Forms.ComboBox
		Me.label1 = New System.Windows.Forms.Label
		Me.label2 = New System.Windows.Forms.Label
		Me.label3 = New System.Windows.Forms.Label
		Me.SuspendLayout
		'
		'txtCharacter
		'
		Me.txtCharacter.Location = New System.Drawing.Point(319, 27)
		Me.txtCharacter.Name = "txtCharacter"
		Me.txtCharacter.Size = New System.Drawing.Size(268, 20)
		Me.txtCharacter.TabIndex = 0
		'
		'btOk
		'
		Me.btOk.Location = New System.Drawing.Point(609, 27)
		Me.btOk.Name = "btOk"
		Me.btOk.Size = New System.Drawing.Size(75, 23)
		Me.btOk.TabIndex = 3
		Me.btOk.Text = "ok"
		Me.btOk.UseVisualStyleBackColor = true
		AddHandler Me.btOk.Click, AddressOf Me.BtOkClick
		'
		'txtRealm
		'
		Me.txtRealm.Location = New System.Drawing.Point(149, 27)
		Me.txtRealm.Name = "txtRealm"
		Me.txtRealm.Size = New System.Drawing.Size(164, 20)
		Me.txtRealm.TabIndex = 0
		'
		'cmbRegion
		'
		Me.cmbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cmbRegion.FormattingEnabled = true
		Me.cmbRegion.Location = New System.Drawing.Point(13, 27)
		Me.cmbRegion.Name = "cmbRegion"
		Me.cmbRegion.Size = New System.Drawing.Size(130, 21)
		Me.cmbRegion.TabIndex = 4
		'
		'label1
		'
		Me.label1.Location = New System.Drawing.Point(13, 9)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(100, 15)
		Me.label1.TabIndex = 5
		Me.label1.Text = "Region"
		'
		'label2
		'
		Me.label2.Location = New System.Drawing.Point(149, 9)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(100, 15)
		Me.label2.TabIndex = 5
		Me.label2.Text = "Realm"
		'
		'label3
		'
		Me.label3.Location = New System.Drawing.Point(319, 9)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(100, 15)
		Me.label3.TabIndex = 5
		Me.label3.Text = "Name"
		'
		'frmArmoryImport
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(696, 68)
		Me.Controls.Add(Me.label3)
		Me.Controls.Add(Me.label2)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.cmbRegion)
		Me.Controls.Add(Me.btOk)
		Me.Controls.Add(Me.txtRealm)
		Me.Controls.Add(Me.txtCharacter)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
		Me.Name = "frmArmoryImport"
		Me.Text = "?"
		AddHandler Load, AddressOf Me.FrmArmoryImportLoad
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private label3 As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
	Friend txtCharacter As System.Windows.Forms.TextBox
	Friend cmbRegion As System.Windows.Forms.ComboBox
	Friend txtRealm As System.Windows.Forms.TextBox
	Private btOk As System.Windows.Forms.Button
End Class
