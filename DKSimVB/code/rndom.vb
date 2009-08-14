'
' Created by SharpDevelop.
' User: Fabien
' Date: 03/04/2009
' Time: 23:40
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Module rndom
	friend RandomNumberGenerator as Random
	Sub InitRNG()
		RandomNumberGenerator = new Random
	End Sub
End Module
