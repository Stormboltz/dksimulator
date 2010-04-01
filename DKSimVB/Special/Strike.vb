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
	


	Friend OffHand  As Boolean
	
	
	Sub UseGCD(T as Long)
		Sim.UseGCD(T, False)
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
		_RNG1 = nothing
	End Sub
	
	Sub New(S As sim)
		me.New
		Sim = S
		sim.DamagingObject.Add(me)
	End Sub

	
	Overrides Function Name() As String
		if _Name <> "" then return _Name
		If offhand = False Then
			return me.ToString
		Else
			return me.ToString & "(OH)"
		End If
	End Function
	
	Function DoMyToTHit As Boolean
		RngHit
		return True
	End Function
	
	Function DoMyStrikeHit As Boolean
		Dim RNG As Double
		RNG = RngHit
		Dim exp As Double
		
		If Me.OffHand Then
			exp = sim.mainstat.OHExpertise
		Else
			exp = sim.mainstat.MHExpertise
		End If
		
		
		If sim.FrostPresence = 1 Then
			If math.Min(exp,0.065)+ math.Min(exp,0.14) + math.Min (sim.mainstat.Hit,0.08) + RNG < 0.285 Then
				Return False
			Else
				return true
			End If
		Else
			If math.Min(exp,0.065) + math.Min (sim.mainstat.Hit,0.08) + RNG < 0.145 Then
				Return False
			Else
				return true
			End If
		End If
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