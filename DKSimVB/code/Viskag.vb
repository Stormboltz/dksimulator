'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 03/10/2009
' Heure: 14:57
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Viskag
	Friend MHtemperedViskag as Integer
	Friend MHSingedViskag as Integer
	Friend OHtemperedViskag as Integer
	Friend OHSingedViskag As Integer
	
	Friend Total as long
	Friend HitCount as Integer
	Friend MissCount as Integer
	Friend CritCount as Integer
	
	Protected _RNG as Random
	
	Function MyRng as Double
		If _RNG Is nothing Then
			_RNG =  New Random(ConvertToInt(me.ToString)+RNGSeeder)
		End If
		return _RNG.NextDouble
	End Function
	
	
	Protected sim as Sim
	Sub New(S As Sim)
		sim = S
		MHtemperedViskag = 0
		MHSingedViskag = 0
		OHSingedViskag = 0
		OHtemperedViskag = 0
		total = 0
		HitCount = 0
		MissCount = 0
		CritCount = 0
		_RNG=nothing
	End Sub
	
	Sub TryMHtemperedViskag
		if MHtemperedViskag <> 1 then exit sub
		If MyRng <= 0.04 Then
			Dim tmp As Double
			tmp = 2222
			tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(sim.TimeStamp)
			If sim.MainStat.crit >= MyRng Then
				HitCount = HitCount + 1
				total = total + tmp
			Else
				CritCount = CritCount + 1
				total = total + tmp * CritCoef
			End If
		End If
	End Sub
	Sub TryMHSingedViskag
		if MHSingedViskag <> 1 then exit sub
		If MyRng <= 0.04 Then
			Dim tmp As Double
			tmp = 2000
			tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(sim.TimeStamp)
			If sim.MainStat.crit >= MyRng Then
				HitCount = HitCount + 1
				total = total + tmp
			Else
				CritCount = CritCount + 1
				total = total + tmp * CritCoef
			End If
		End If	
	End Sub
	Sub TryOHtemperedViskag
		if OHtemperedViskag <> 1 then exit sub
		If MyRng <= 0.04 Then
			Dim tmp As Double
			tmp = 2222
			tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(sim.TimeStamp)
			If sim.MainStat.crit >= MyRng Then
				HitCount = HitCount + 1
				total = total + tmp
			Else
				CritCount = CritCount + 1
				total = total + tmp * CritCoef
			End If
		End If
	End Sub
	Sub TryOHSingedViskag
		if OHSingedViskag <> 1 then exit sub
		If MyRng <= 0.04 Then
			Dim tmp As Double
			tmp = 2000
			tmp = tmp * sim.MainStat.StandardPhysicalDamageMultiplier(sim.TimeStamp)
			If sim.MainStat.crit >= MyRng Then
				HitCount = HitCount + 1
				total = total + tmp
			Else
				CritCount = CritCount + 1
				total = total + tmp * CritCoef
			End If
		End If
	End Sub
	
	
    Function CritCoef() As Double
		CritCoef = 1 * (1+0.06*sim.mainstat.CSD)
	End Function
	
	
	
	Function report as String
		dim tmp as String
		tmp = "Vis'kag" & VBtab
		tmp = tmp & total & VBtab
		tmp = tmp & toDecimal(100*Total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(Total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
	
	
End Class
