'
' Created by SharpDevelop.
' User: Fabien
' Date: 28/03/2009
' Time: 18:22
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class Form1
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
		Me.textBox1 = New System.Windows.Forms.TextBox
		Me.label1 = New System.Windows.Forms.Label
		Me.label2 = New System.Windows.Forms.Label
		Me.btOk = New System.Windows.Forms.Button
		Me.SuspendLayout
		'
		'textBox1
		'
		Me.textBox1.Location = New System.Drawing.Point(12, 30)
		Me.textBox1.Name = "textBox1"
		Me.textBox1.Size = New System.Drawing.Size(340, 20)
		Me.textBox1.TabIndex = 0

		'
		'label1
		'
		Me.label1.Location = New System.Drawing.Point(358, 33)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(38, 23)
		Me.label1.TabIndex = 1
		Me.label1.Text = ".xml"
		'
		'label2
		'
		Me.label2.Location = New System.Drawing.Point(12, 9)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(340, 18)
		Me.label2.TabIndex = 2
		Me.label2.Text = "Input the new file name and press enter"
		'
		'btOk
		'
		Me.btOk.Location = New System.Drawing.Point(415, 27)
		Me.btOk.Name = "btOk"
		Me.btOk.Size = New System.Drawing.Size(75, 23)
		Me.btOk.TabIndex = 3
		Me.btOk.Text = "ok"
		Me.btOk.UseVisualStyleBackColor = true
		AddHandler Me.btOk.Click, AddressOf Me.BtOkClick
		'
		'Form1
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(502, 62)
		Me.Controls.Add(Me.btOk)
		Me.Controls.Add(Me.label2)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.textBox1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
		Me.Name = "Form1"
		Me.Text = "?"

		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private btOk As System.Windows.Forms.Button
	Private label2 As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
	Friend textBox1 As System.Windows.Forms.TextBox
End Class
