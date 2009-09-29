'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 25/08/2009
' Heure: 15:16
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class RandomNumberGenerator
	Private _RNGWhiteHit as Random
	Private _RNGStrike as Random
	Private _RNGProc as Random
	Private _RNGPet As Random
	Private _RNGT9P4 As Random
	Private _RNGTank as Random
	
	
	sub New
		Dim tmp As New Random(150)
		Dim tmp2 As New Random(7331)
		Dim tmp3 As New Random(1500)
		Dim tmp4 As New Random(5847)
		Dim tmp5 As New Random(131279)	
		dim tmp6 as New Random(1478963)
		_RNGWhiteHit = tmp
		_RNGStrike = tmp2
		_RNGProc = tmp3
		_RNGPet = tmp4
		_RNGT9P4 = tmp5
		_RNGTank = tmp6
	End sub
	
	
	Function RNGWhiteHit As Double
		return _RNGWhiteHit.NextDouble
	End Function
	
	Function RNGStrike As Double
		return _RNGStrike.NextDouble
	End Function
	
	Function RNGProc As Double
	 return _RNGProc.NextDouble
	End Function

	Function RNGPet As Double
	 return _RNGPet.NextDouble
	End Function
	
	Function RNGT9P4 As Double
		return _RNGT9P4.NextDouble
	End Function
	
	Function RNGTank As Double
		return _RNGTank.NextDouble
	End Function
End Class
