'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 13/09/2009
' Heure: 14:25
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Strikes

Public Class Strike
	Public Total As  long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	Friend TotalHit As Long
	Friend TotalCrit As Long
	Protected Sim as Sim
	Friend ThreadMultiplicator As Double
	Protected _RNG as Random
	
	
    
    Public Sub New()
		init()
    End Sub
    
	Overridable Protected Sub init()
		Total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
		ThreadMultiplicator = 1
		_RNG = nothing
	End Sub
	
	Sub New(S As sim)
		me.New
		Sim = S
		sim.DamagingObject.Add(me)
	End Sub

	
	
	
	
	
	Function DoMyStrikeHit As Boolean
		Dim RNG As Double
		RNG = MyRNG
		If sim.MainStat.FrostPresence = 1 Then
			If math.Min(sim.mainstat.Expertise,0.065)+ math.Min(sim.mainstat.Expertise,0.14) + math.Min (sim.mainstat.Hit,0.08) + RNG < 0.285 Then
				Return False
			Else
				return true
			End If
		Else
			If math.Min(sim.mainstat.Expertise,0.065) + math.Min (sim.mainstat.Hit,0.08) + RNG < 0.145 Then
				Return False
			Else
				return true
			End If
		End If
	End Function
	
	
	
	Function MyRng as Double 
		If _RNG Is nothing Then
			_RNG =  New Random(ConvertToInt(me.ToString)+RNGSeeder)
		End If
		return _RNG.NextDouble
	End Function
	
	
	Overridable Public Function isAvailable(T As Long) As Boolean
	End Function
	
	Overridable Public Function ApplyDamage(T As Long) As Boolean
	End Function
	
	Overridable Public Function ApplyDamage(T As Long,MH as Boolean) As Boolean
	End Function
	
	Overridable Function AvrgNonCrit(T as long, MH as Boolean) As Double
	End Function
	Overridable Function AvrgNonCrit(T as long) As Double
	End Function
	
	Overridable Function CritCoef() As Double
	End Function
	
	Overridable Function CritChance() As Double
	End Function
	
	Overridable Function AvrgCrit(T As long, MH as Boolean) As Double
	End Function
	Overridable Function AvrgCrit(T As long) As Double
	End Function
	
	Overridable Function report As String
		dim tmp as String
		tmp = ShortenName(me.ToString)  & VBtab
		
		If total.ToString().Length < 8 Then
			tmp = tmp & total & "   " & VBtab
		Else
			tmp = tmp & total & VBtab
		End If
		tmp = tmp & toDecimal(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(total/(HitCount+CritCount)) & VBtab
		If sim.MainStat.FrostPresence Then
			tmp = tmp & toDecimal((100 * total * ThreadMultiplicator * 2.0735 ) / sim.TimeStamp) & VBtab
		End If

		tmp = tmp & vbCrLf
		return tmp
	End Function
	
End Class
end Namespace