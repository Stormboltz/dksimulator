'
' Created by SharpDevelop.
' User: Fabien
' Date: 28/03/2009
' Time: 18:22
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Scenarios
Partial Class ScenarioEditor
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
		Me.grpAvailableScenario = New System.Windows.Forms.GroupBox
		Me.cmdSaveAsNew = New System.Windows.Forms.Button
		Me.cmdSave = New System.Windows.Forms.Button
		Me.grpCurrentScenario = New System.Windows.Forms.GroupBox
		Me.SuspendLayout
		'
		'grpAvailableScenario
		'
		Me.grpAvailableScenario.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.grpAvailableScenario.AutoSize = true
		Me.grpAvailableScenario.Location = New System.Drawing.Point(406, 41)
		Me.grpAvailableScenario.Name = "grpAvailableScenario"
		Me.grpAvailableScenario.Size = New System.Drawing.Size(234, 401)
		Me.grpAvailableScenario.TabIndex = 7
		Me.grpAvailableScenario.TabStop = false
		Me.grpAvailableScenario.Text = "Available"
		'
		'cmdSaveAsNew
		'
		Me.cmdSaveAsNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdSaveAsNew.Location = New System.Drawing.Point(484, 12)
		Me.cmdSaveAsNew.Name = "cmdSaveAsNew"
		Me.cmdSaveAsNew.Size = New System.Drawing.Size(75, 23)
		Me.cmdSaveAsNew.TabIndex = 6
		Me.cmdSaveAsNew.Text = "Save as new"
		Me.cmdSaveAsNew.UseVisualStyleBackColor = true
		AddHandler Me.cmdSaveAsNew.Click, AddressOf Me.CmdSaveAsNewClick
		'
		'cmdSave
		'
		Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.cmdSave.Location = New System.Drawing.Point(565, 12)
		Me.cmdSave.Name = "cmdSave"
		Me.cmdSave.Size = New System.Drawing.Size(75, 23)
		Me.cmdSave.TabIndex = 5
		Me.cmdSave.Text = "Save"
		Me.cmdSave.UseVisualStyleBackColor = true
		AddHandler Me.cmdSave.Click, AddressOf Me.CmdSaveClick
		'
		'grpCurrentScenario
		'
		Me.grpCurrentScenario.AutoSize = true
		Me.grpCurrentScenario.Location = New System.Drawing.Point(12, 41)
		Me.grpCurrentScenario.Name = "grpCurrentScenario"
		Me.grpCurrentScenario.Size = New System.Drawing.Size(232, 401)
		Me.grpCurrentScenario.TabIndex = 8
		Me.grpCurrentScenario.TabStop = false
		Me.grpCurrentScenario.Text = "Current"
		'
		'ScenarioEditor
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoScroll = true
		Me.ClientSize = New System.Drawing.Size(652, 454)
		Me.Controls.Add(Me.grpCurrentScenario)
		Me.Controls.Add(Me.grpAvailableScenario)
		Me.Controls.Add(Me.cmdSaveAsNew)
		Me.Controls.Add(Me.cmdSave)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
		Me.Name = "ScenarioEditor"
		Me.Text = "?"
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private grpCurrentScenario As System.Windows.Forms.GroupBox
	Private cmdSaveAsNew As System.Windows.Forms.Button
	Private cmdSave As System.Windows.Forms.Button
	Private grpAvailableScenario As System.Windows.Forms.GroupBox
	

End Class
End Namespace