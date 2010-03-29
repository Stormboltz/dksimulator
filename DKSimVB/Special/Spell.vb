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
	
	
	
	
	Overridable Public Function ApplyDamage(T As Long) As Boolean
	End Function
	Overridable Public Function ApplyDamage(T As Long, SDoom as Boolean) As Boolean
	End Function
		
	Overridable Function AvrgNonCrit(T as long) As Double
	End Function
	
	Overridable Function CritCoef() As Double
	End Function
	
	Overridable Function CritChance() As Double
	End Function
	
	Overridable Function AvrgCrit(T As long) As Double
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