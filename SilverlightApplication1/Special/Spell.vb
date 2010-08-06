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
            Dim RNG As Double
            UseGCD(T)
            LastDamage = 0
            If DoMySpellHit() = False Then
                sim.CombatLog.write(T & vbTab & Me.Name & " fail")
                MissCount = MissCount + 1
                Return False
            End If
            RNG = RngCrit

            If RNG <= CritChance() Then
                CritCount = CritCount + 1
                LastDamage = AvrgCrit(T)
                TotalCrit += LastDamage
                sim.CombatLog.write(T & vbTab & Me.Name & " crit for " & LastDamage)

            Else
                LastDamage = AvrgNonCrit(T)
                TotalHit += LastDamage
                HitCount = HitCount + 1
                sim.CombatLog.write(T & vbTab & Me.Name & " hit for " & LastDamage & vbTab)
            End If
            total = total + LastDamage
            sim.proc.TryOnSpellHit()
            Return True
        End Function

	
        Protected _CritCoef As Double = -1
        Overridable Function CritCoef() As Double
            If _CritCoef <> -1 Then Return _CritCoef
            _CritCoef = 1 + 0.06 * sim.MainStat.CSD
            Return _CritCoef
        End Function
	
        Overridable Function CritChance() As Double
            Return sim.MainStat.SpellCrit
        End Function
	
        Overridable Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Dim tmp As Double
            tmp = BaseDamage + sim.MainStat.AP * Coeficient
            tmp = tmp * Multiplicator
            tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
            Return tmp
        End Function
        Overridable Function AvrgCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Return (AvrgNonCrit(T, target) * (1 + CritCoef()))
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