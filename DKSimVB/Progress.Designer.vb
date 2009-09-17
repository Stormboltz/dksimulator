'
' Created by SharpDevelop.
' User: Fabien
' Date: 28/03/2009
' Time: 18:22
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class ProgressFrm
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
		Me.PBsim = New System.Windows.Forms.ProgressBar
		Me.SuspendLayout
		'
		'PBsim
		'
		Me.PBsim.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.PBsim.Location = New System.Drawing.Point(0, 0)
		Me.PBsim.Name = "PBsim"
		Me.PBsim.Size = New System.Drawing.Size(502, 23)
		Me.PBsim.TabIndex = 3
		'
		'ProgressFrm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(502, 23)
		Me.ControlBox = false
		Me.Controls.Add(Me.PBsim)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		Me.IsMdiContainer = true
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "ProgressFrm"
		Me.ShowIcon = false
		Me.ShowInTaskbar = false
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "?"
		AddHandler Load, AddressOf Me.ProgressFrmLoad
		Me.ResumeLayout(false)
	End Sub
	Friend PBsim As System.Windows.Forms.ProgressBar
	

End Class
