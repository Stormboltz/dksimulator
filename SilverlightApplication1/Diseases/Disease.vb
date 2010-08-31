'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 13/09/2009
' Heure: 14:25
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
NameSpace Diseases
	Public Class Disease
		Inherits Supertype
		
		Friend nextTick As long
		Friend FadeAt As long
		
		Friend AP as Integer
		
		Friend DamageTick As Integer
		Friend ScourgeStrikeGlyphCounter As Integer
		Friend OtherTargetsFade As Integer
		Friend CritChance As Double
		Friend Multiplier As Double
		
		Private _Lenght As Integer
		Friend previousFade As Long
		
		
		Friend Cinder As Boolean
		
		
		Friend ToReApply as Boolean
		
		Sub New
			init()
		End Sub
		
		Sub New(S As sim)
			me.New
			Sim = S
			sim.DamagingObject.Add(me)
		End Sub
		
		Overridable Protected Sub init()
			nextTick = 0
			FadeAt= 0
			total = 0
			MissCount = 0
			HitCount = 0
			CritCount = 0
			TotalHit = 0
			TotalCrit = 0
			AP = 0
			OtherTargetsFade = 0
			ThreadMultiplicator = 1
			ToReApply = 0
			_RNG1=nothing
		End sub
		
		Function Lenght() as Integer
			If _Lenght = 0 Then
                _Lenght = 3000 + 600 * sim.Character.Talents.Talent("Epidemic").Value
			End If
			return _Lenght
		End Function

        Sub IncreaseDuration(ByVal T As Long)
            FadeAt += T
            uptime += T
        End Sub
		
		
		Overridable Function PerfectUsage(T As Long) As Boolean 'Unused
			return false
		End Function
		
		Overridable Function isActive(T As Long) As Boolean
			If T > FadeAt Then
				isActive = False
			Else
				isActive = True
			End If
		End Function
		
		Overridable Function ShouldReapply(T As Long) As Boolean
			return ToReapply or not isActive(T)
		End Function
		
		Overridable Function CalculateCritChance(T As Long) As Double
			return 0.0
		End Function
		
		Overridable Function CalculateMultiplier(T As Long,target As Targets.Target) As Double
			
			Dim tmp As Double
			tmp = sim.MainStat.StandardMagicalDamageMultiplier(T)
			if sim.RuneForge.CheckCinderglacier(False) > 0 then tmp  *= 1.2
            tmp = tmp * (1 + sim.Character.Talents.Talent("EbonPlaguebringer").Value * 15 / 100)
            If sim.Character.Talents.GetNumOfThisSchool(Talents.Schools.Unholy) > 20 Then
                tmp = tmp * 1.2 'Blightcaller
            End If
            Return tmp
        End Function

        Function Apply(ByVal T As Long) As Boolean
            Apply(T, sim.Targets.MainTarget)
            Return True
        End Function

        Overridable Function Apply(ByVal T As Long, ByVal target As Targets.Target) As Boolean
            ToReApply = False
            If nextTick <= T Then
                nextTick = T + 3 * 100
            End If

            sim.FutureEventManager.Add(nextTick, "Disease")
            ScourgeStrikeGlyphCounter = 0
            CritChance = CalculateCritChance(T)
            If sim.RuneForge.CheckCinderglacier(False) > 0 Then
                Cinder = True
            Else
                Cinder = False
            End If
            Multiplier = CalculateMultiplier(T, target)
            Refresh(T)
            Return True
        End Function

        Overridable Function Refresh(ByVal T As Long) As Boolean
            FadeAt = T + Lenght
            AP = sim.MainStat.AP
            DamageTick = AvrgNonCrit(T)
            AddUptime(T)
            Return True
        End Function

        Overridable Function AvrgNonCrit(ByVal T As Long) As Double
            Return Multiplier * 1.15 * (26 + 0.055 * (1 + 0.2 * sim.Character.Talents.Talent("Impurity").Value) * AP)
        End Function

        Function ApplyDamage(ByVal T As Long) As Boolean
            Dim tmp As Double
            Dim intCount As Integer
            RngHit()

            If intCount > 1 And OtherTargetsFade < T Then Return True
            If RngCrit < CritChance Then
                tmp = AvrgCrit(T)
                CritCount = CritCount + 1
                totalcrit += tmp
            Else
                tmp = DamageTick
                HitCount = HitCount + 1
                totalhit += tmp
            End If
            total = total + tmp
            
            sim.proc.tryProcs(Procs.ProcOnType.OnDoT)
            nextTick = T + 300
            sim.FutureEventManager.Add(nextTick, "Disease")
            If sim.combatlog.LogDetails Then sim.combatlog.write(T & vbtab & Me.ToString & " hit for " & tmp)

            Return True
        End Function
		
        Protected _CritCoef As Double = -1
        Overridable Function CritCoef() As Double
            If _CritCoef <> -1 Then Return _CritCoef
            _CritCoef = 1 + 0.06 * sim.MainStat.CSD
            Return _CritCoef
        End Function
		
		Overridable Function AvrgCrit(T As long) As Double
			return DamageTick * (1 + CritCoef)
		End Function
		
		
		
		Public Sub cleanup()
			Total = 0
			HitCount = 0
			MissCount =0
			CritCount = 0
			TotalHit = 0
			TotalCrit = 0
		End Sub
		
		Sub AddUptime(T As Long)
			dim tmp as Long
			If Lenght + T > sim.NextReset Then
				tmp = (sim.NextReset - T)
			Else
				tmp = Lenght
			End If
			
			If previousFade < T  Then
				uptime += tmp
			Else
				uptime += tmp - (previousFade-T)
			End If
			previousFade = T + tmp
		End Sub
		
		
		
		
		
		
		Sub RemoveUptime(T As Long)
			If previousfade < T  Then
			Else
				uptime -= (previousFade-T)
			End If
			previousFade = T
		End Sub
		
			
	End Class
end Namespace