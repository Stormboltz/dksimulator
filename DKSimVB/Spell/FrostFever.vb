Friend module FrostFever
	Friend nextTick As long
	Friend FadeAt As long
	Friend total As Long
		Friend TotalHit As Long
	Friend TotalCrit as Long

	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	Friend AP as Integer
	
	Function isActive(T As long) As Boolean
		If T > FadeAt Then
			isActive = False
		Else
			isActive = True
		End If
	End Function
		
	Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		FadeAt = 0
		nextTick = 0
		TotalHit = 0
		TotalCrit = 0
		AP=0
	End Sub
	
	Function Apply(T As Long) As Boolean
		FadeAt = T + 1500 + 300 * talentunholy.Epidemic
		nextTick = T + 300
		AP = MainStat.AP
	End Function
	
	Function ApplyDamage(T As long) As boolean
		Dim tmp As Double

		HitCount = HitCount + 1
		tmp = AvrgNonCrit(T)
		total = total + tmp
		If TalentUnholy.WanderingPlague > 0 Then
			If WanderingPlague.isAvailable(T) = True Then
				Dim RNG As Double
				RNG = Rnd
				If RNG <= MainStat.crit Then
					WanderingPlague.ApplyDamage(tmp, T)
				End If
			End If
		End If
		nextTick = T + 300
		If combatlog.LogDetails Then combatlog.write(T  & vbtab &  "Frost Fever hit for " & tmp )
		return true
		'Debug.Print T & vbTab & "FrostFever for " & tmp
	End Function
	Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = 26
		tmp = tmp + 0.055 * (1 + 0.04 * TalentUnholy.Impurity) * AP
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		tmp = tmp * (1 + TalentUnholy.CryptFever * 10 / 100)
		tmp = tmp * MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * 1.15
		if MHRazorice or (OHRazorice and mainstat.DualW)  then tmp = tmp *1.10
		AvrgNonCrit = tmp
	End Function
	Function CritCoef() As Double
		
	End Function
	Function CritChance() As Double
		
	End Function
	Function AvrgCrit() As Double
		
	End Function
	Function report As String
		dim tmp as String
		tmp = "Frost Fever" & VBtab
	
		If total.ToString().Length < 8 Then
			tmp = tmp & total & "   " & VBtab
		Else
			tmp = tmp & total & VBtab
		End If
		tmp = tmp & int(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & int(HitCount+CritCount) & VBtab
		tmp = tmp & int(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & int(total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
end module