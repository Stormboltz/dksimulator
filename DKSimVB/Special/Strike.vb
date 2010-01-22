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
	Inherits Supertype
	

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

	
	overrides Function Name() As String
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
	
	
	Overridable Function AvrgNonCrit(T as long) As Double
	End Function
	
	Overridable Function CritCoef() As Double
	End Function
	
	Overridable Function CritChance() As Double
	End Function
	
	Overridable Function AvrgCrit(T As Long) As Double
		Return AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	Overridable Public Sub cleanup()
		Total = 0
		HitCount = 0
		MissCount =0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
	End Sub
	
	
	
End Class
end Namespace