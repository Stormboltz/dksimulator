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
			_Lenght = 1500 + 300 * sim.TalentUnholy.Epidemic
		End If
		return _Lenght
	End Function
	
	
	
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
	
	Overridable Function CalculateMultiplier(T As Long) As Double
		Dim tmp As Double
		tmp = sim.MainStat.StandardMagicalDamageMultiplier(T)
		if sim.RuneForge.CheckCinderglacier(False) > 0 then tmp  *= 1.2
		If  sim.Buff.CrypticFever Then
			tmp = tmp * 1.3
		Else
			tmp = tmp * (1 + sim.TalentUnholy.CryptFever * 10 / 100)
		End If
		tmp = tmp * (1 + sim.TalentFrost.BlackIce * 2 / 100)
		return tmp
	End Function
	
	Overridable Function Apply(T As Long) As Boolean
		ToReApply = false
		nextTick = T + 3 * 100
		sim.FutureEventManager.Add(nextTick,"Disease")
		ScourgeStrikeGlyphCounter = 0
		CritChance = sim.MainStat.crit
		If sim.RuneForge.CheckCinderglacier(False) > 0 Then
			cinder = True
		Else
			cinder = False
		End If
		Multiplier = CalculateMultiplier(T)
		Refresh(T)
	End Function
	
	Overridable Function Refresh(T As Long) As Boolean
		FadeAt = T + Lenght
		AP = sim.MainStat.AP
		DamageTick = AvrgNonCrit(T)
		AddUptime(T)
	End Function
	
	Overridable Function AvrgNonCrit(T As Long) As Double
		Return Multiplier * 1.15 * (26 + 0.055 * (1 + 0.04 * sim.TalentUnholy.Impurity) * AP)
	End Function
	
	Function ApplyDamage(T As long) As boolean
		Dim tmp As Double
		Dim intCount As Integer
		RngHit
		
		For intCount = 1 To Sim.NumberOfEnemies
			if intCount > 1 and OtherTargetsFade < T then return true
			If sim.MainStat.T94PDPS =1 Then
				If RngCrit < CritChance Then
					tmp = AvrgCrit(T)
					CritCount = CritCount + 1
					totalcrit += tmp
				Else
					tmp = DamageTick
					HitCount = HitCount + 1
					totalhit += tmp
				End If
			Else
				tmp = DamageTick
			'	totalhit += tmp
				HitCount = HitCount + 1
			End If
			total = total + tmp
			If sim.TalentUnholy.WanderingPlague > 0 Then
				If Sim.WanderingPlague.isAvailable(T) = True Then
					Dim RNG As Double
					RNG = Rng3
					If RNG <= sim.MainStat.crit Then
						Sim.WanderingPlague.ApplyDamage(tmp, T)
					End If
				End If
			End If
			sim.tryOnDoT
			nextTick = T + 300
			sim.FutureEventManager.Add(nextTick,"Disease")
			If sim.combatlog.LogDetails Then sim.combatlog.write(T  & vbtab & Me.ToString & " hit for " & tmp )
		Next intCount
		return true
	End Function
	
	Overridable Function CritCoef() As Double
		return (1+0.06*sim.mainstat.CSD)
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
		If Lenght + T > sim.MaxTime Then
			tmp = (sim.MaxTime - T)
		Else
			tmp = Lenght
		End If
		
		If previousfade < T  Then
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