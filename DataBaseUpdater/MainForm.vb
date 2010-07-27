'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 17/07/2010
' Heure: 21:21
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Partial Class MainForm
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	Sub Button1Click(sender As Object, e As EventArgs)
		Dim x As New Extractor
		
		'x.CataCreateDB
		x.CataUpdateDatabase
	End Sub
End Class
