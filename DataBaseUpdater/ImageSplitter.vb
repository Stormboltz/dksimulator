'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 20/09/2010
' Heure: 16:53
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class ImageSplitter
	
	Sub New()
		
	End Sub
Sub Split
	Dim bmOriginal As New Bitmap("C:\partage\DKSimVB\Deathknight_3.jpg")
	Dim i As Integer
	dim title as String = "Deathknight_3"
	
	For I=0 To (bmOriginal.Width/36)-1
		Dim bmTile1 As Bitmap = bmOriginal.Clone(New Rectangle(i*36,0,36,36),system.Drawing.Imaging.PixelFormat.DontCare )
		Dim bmTile2 As Bitmap = bmOriginal.Clone(New Rectangle(i*36,36,36,36),system.Drawing.Imaging.PixelFormat.DontCare )
		bmTile1.Save(title & "_" & i & ".jpg" ,system.Drawing.Imaging.ImageFormat.Jpeg)
		bmTile2.Save(title & "_" & i &"_g.jpg",system.Drawing.Imaging.ImageFormat.Jpeg)
	Next
	bmOriginal = Nothing
	
	
End Sub

End Class
