'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 25/08/2009
' Heure: 15:16
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Module RandomNumberGenerator
	Private _RNGWhiteHit as Random
	Private _RNGStrike as Random
	Private _RNGProc as Random
	private _RNGPet as Random
	Function Init
		Dim tmp As New Random(150)
		Dim tmp2 As New Random(7331)
		Dim tmp3 As New Random(1500)
		Dim tmp4 As New Random(5847)
		
		_RNGWhiteHit = tmp
		_RNGStrike = tmp2
		_RNGProc = tmp3
		_RNGPet = tmp4
	End Function
	
	
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
End Module
