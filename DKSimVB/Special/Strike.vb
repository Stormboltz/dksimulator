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
	Public ThreadMultiplicator As Double
	Protected _RNG As Random
	Friend OffHand  As Boolean
	
	
	Sub UseGCD(T as Long)
		If sim.UnholyPresence Then
			Sim.NextFreeGCD = T + 100+ sim.latency/10
		Else
			Sim.NextFreeGCD = T + 150+ sim.latency/10
		End If
	End Sub
	
	
	
    
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

	
	Function Name() As String
		If offhand = False Then
			return me.ToString
		Else
			return me.ToString & "(OH)"
		End If
	End Function
	
	
	
	Function DoMyStrikeHit As Boolean
		Dim RNG As Double
		RNG = MyRNG
		If sim.FrostPresence = 1 Then
			If math.Min(sim.mainstat.MHExpertise,0.065)+ math.Min(sim.mainstat.MHExpertise,0.14) + math.Min (sim.mainstat.Hit,0.08) + RNG < 0.285 Then
				Return False
			Else
				return true
			End If
		Else
			If math.Min(sim.mainstat.MHExpertise,0.065) + math.Min (sim.mainstat.Hit,0.08) + RNG < 0.145 Then
				Return False
			Else
				return true
			End If
		End If
	End Function
	
	
	
	Function MyRng as Double 
		If _RNG Is nothing Then
			_RNG =  New Random(ConvertToInt(me.name)+RNGSeeder)
		End If
		return _RNG.NextDouble
	End Function
	
	
	Overridable Public Function isAvailable(T As Long) As Boolean
	End Function
	
	Overridable Public Function ApplyDamage(T As Long) As Boolean
	End Function
	
	Overridable Public Sub Merge()
	End Sub
	
	Overridable Function AvrgNonCrit(T as long) As Double
	End Function
	
	Overridable Function CritCoef() As Double
	End Function
	
	Overridable Function CritChance() As Double
	End Function
	
	Overridable Function AvrgCrit(T As long) As Double
	End Function
	Overridable Public Sub cleanup()
		Total = 0
		HitCount = 0
		MissCount =0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
	End Sub
	Overridable Function report As String
		dim tmp as String
		tmp = ShortenName(me.Name)  & VBtab

		tmp = tmp & total & VBtab
		tmp = tmp & toDecimal(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(total/(HitCount+CritCount)) & VBtab
		
		tmp = tmp & toDecimal(HitCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(totalhit/(HitCount)) & VBtab
		
		tmp = tmp & toDecimal(CritCount) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(totalcrit/(CritCount)) & VBtab
				
		tmp = tmp & toDecimal(MissCount) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab

		If sim.FrostPresence Then
			tmp = tmp & toDecimal((100 * total * ThreadMultiplicator * 2.0735 ) / sim.TimeStamp) & VBtab
		End If

		tmp = tmp & vbCrLf
		
		tmp = replace(tmp, VBtab & 0, vbtab)
		
		return tmp
	End Function
	
End Class
end Namespace