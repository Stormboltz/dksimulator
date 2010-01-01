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
	
	Friend nextTick As long
	Friend FadeAt As long
	
	Friend AP as Integer
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount As Integer
	Friend DamageTick As Integer
	Friend ScourgeStrikeGlyphCounter As Integer
	Friend OtherTargetsFade As Integer
	Friend CritChance As Double
	Public total As Long
	Friend TotalHit As Long
	Friend TotalCrit As Long
	Public ThreadMultiplicator As Double
	Protected sim As Sim
	
	Friend ToReApply as Boolean
	Protected _RNG as Random
	
	Function MyRng as Double 
		If _RNG Is nothing Then
			_RNG =  New Random(ConvertToInt(me.ToString)+RNGSeeder)
		End If
		return _RNG.NextDouble
	End Function
	

	
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
		_RNG=nothing
	End sub
	
	Overridable Function PerfectUsage(T As Long) As Boolean
		return false
	End Function
		
	Overridable Function isActive(T As Long) As Boolean
		If T > FadeAt Then
			isActive = False
		Else
			isActive = True
		End If
	End Function
	
	Overridable Function Apply(T As Long) As Boolean
	End Function
	
	
	Overridable Function AvrgNonCrit(T As long) As Double
	End Function
	
	Function ApplyDamage(T As long) As boolean
		Dim tmp As Double
		Dim intCount As Integer
		For intCount = 1 To Sim.NumberOfEnemies
			if intCount > 1 and OtherTargetsFade < T then return true
			If sim.MainStat.T94PDPS =1 Then
				If sim.RandomNumberGenerator.RNGT9P4 < CritChance Then
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
				HitCount = HitCount + 1
			End If
			total = total + tmp
			If sim.TalentUnholy.WanderingPlague > 0 Then
				If Sim.WanderingPlague.isAvailable(T) = True Then
					Dim RNG As Double
					RNG = MyRNG
					If RNG <= sim.MainStat.crit Then
						Sim.WanderingPlague.ApplyDamage(tmp, T)
					End If
				End If
			End If
			Sim.Trinkets.Necromantic.TryMe(T)
			nextTick = T + 300
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
	
	Function report As String
		dim tmp as String
		tmp = ShortenName(me.ToString) & VBtab
	
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
	
	Overridable Public Sub Merge()
	End Sub
	
	
	
End Class
end Namespace