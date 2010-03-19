'
' Created by SharpDevelop.
' User: Fabien
' Date: 28/03/2009
' Time: 18:22
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class EPDisplay
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
		Me.listView1 = New System.Windows.Forms.ListView
		Me.columnHeader1 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader2 = New System.Windows.Forms.ColumnHeader
		Me.SuspendLayout
		'
		'listView1
		'
		Me.listView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.listView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1, Me.columnHeader2})
		Me.listView1.Location = New System.Drawing.Point(0, 0)
		Me.listView1.Name = "listView1"
		Me.listView1.Size = New System.Drawing.Size(504, 353)
		Me.listView1.TabIndex = 3
		Me.listView1.UseCompatibleStateImageBehavior = false
		Me.listView1.View = System.Windows.Forms.View.Details
		'
		'columnHeader1
		'
		Me.columnHeader1.Text = "Stat"
		Me.columnHeader1.Width = 131
		'
		'columnHeader2
		'
		Me.columnHeader2.Text = "Value"
		'
		'EPDisplay
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(502, 352)
		Me.Controls.Add(Me.listView1)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
		Me.Name = "EPDisplay"
		Me.Text = "?"
		AddHandler Load, AddressOf Me.EPDisplayLoad
		Me.ResumeLayout(false)
	End Sub
	Private columnHeader2 As System.Windows.Forms.ColumnHeader
	Private columnHeader1 As System.Windows.Forms.ColumnHeader
	Private listView1 As System.Windows.Forms.ListView
End Class
