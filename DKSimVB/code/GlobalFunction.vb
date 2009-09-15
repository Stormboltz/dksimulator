'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 14/09/2009
' Heure: 23:19
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Module GlobalFunction
	Function GetFilePath(s As String) As String
		on error resume next
		s = strings.Right(s,s.Length-InStr(s,"(") )
		s = strings.Left(s, InStrRev(s,")")-1 )
		return s
	End Function
	
	Function toDecimal(d As Double) As Decimal
		try
			Return d.ToString (".#")
		Catch
		End Try
	End Function
	
	Function toDDecimal(d As Double) As Decimal
		try
			Return d.ToString (".##")
		Catch
		End Try
	End Function
	
End Module
