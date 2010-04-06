'
' Created by SharpDevelop.
' User: Fabien
' Date: 28/03/2009
' Time: 18:22
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Partial Class frmArmoryImport
	Friend Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		
	End Sub
	
	
	
	Sub BtOkClick(sender As Object, e As EventArgs)
		me.DialogResult = DialogResult.ok
	End Sub
	
	
	Sub FrmArmoryImportLoad(sender As Object, e As EventArgs)
		cmbRegion.Items.Add("US")
		cmbRegion.Items.Add("EU")
		cmbRegion.Items.Add("CN")
		cmbRegion.Items.Add("KR")
		cmbRegion.Items.Add("TW")
	End Sub
End Class
