'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 13/09/2009
' Heure: 14:25
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
NameSpace Spells
Public Class Spell
	Inherits Supertype
	

	Friend CD As Long
	Friend ActiveUntil As Long

	
	
	Function DoMySpellHit As Boolean
		Dim RNG As Double
		RNG = RngHit
		If math.Min(sim.mainstat.SpellHit,0.17) + RNG < 0.17 Then
			Return False
		Else
			return true
		End If
	End Function
	
	Sub New()		
		Init
	End Sub
	
	
	
	
	Overridable Sub Init()
		Total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
		CD = 0
		ActiveUntil = 0
		ThreadMultiplicator = 1
		_RNG1 = Nothing

	End Sub
	
	Sub New(S As sim)
		me.New
		Sim = S
		sim.DamagingObject.Add(me)
	End Sub
	
	sub UseGCD(T as Long)
		Sim.UseGCD(T, True)
	End sub
	
	
	
	
        Public Overridable Function ApplyDamage(ByVal T As Long) As Boolean
            Return False
        End Function

	
        Protected _CritCoef As Double = -1
        Overridable Function CritCoef() As Double
            If _CritCoef <> -1 Then Return _CritCoef
            _CritCoef = 1 + 0.06 * sim.MainStat.CSD
            Return _CritCoef
        End Function
	
        Overridable Function CritChance() As Double
            Return 0
        End Function
	
        Overridable Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Return 0
        End Function
        Overridable Function AvrgCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Return 0
        End Function
	
	
	Function AvrgNonCrit(T As Long) As Double
		return AvrgNonCrit(T,sim.Targets.MainTarget)
	End Function
	Function AvrgCrit(T As Long) As Double
		return AvrgCrit(T,sim.Targets.MainTarget)
	End Function	
	
	Public Sub cleanup()
		Total = 0
		HitCount = 0
		MissCount =0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0
	End Sub
	
	
End Class
end Namespace