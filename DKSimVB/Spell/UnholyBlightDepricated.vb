Friend module UnholyBlightDepricated
	Friend nextTick As long
	Friend FadeAt As long
	Friend total As long
	
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	Friend TotalHit As Long
	Friend TotalCrit as Long

	
	Function isAvailable(T As long) As Boolean
		If Gargoyle.ActiveUntil >= T Then
			If RunicPower.Value >= 80 Then 			return True
		Else
			If RunicPower.Value >= 40 Then 			return True
		End If
	End Function
	Function isActive(T As long) As Boolean
		If T > FadeAt Then
			isActive = False
			nextTick = 0
		Else
			isActive = True
		End If
	End Function
	Function Apply(T As Long) As Double
		
		If glyph.UnholyBlight Then
				FadeAt = T + 3000
		Else
				FadeAt = T + 2000
		End If
		RunicPower.Value = RunicPower.Value - 40
		combatlog.write(T  & vbtab &  "UnholyBlight" &  vbtab & "RP left = " & RunicPower.Value)	
		nextTick = T + 100
		Sim.NextFreeGCD = T + (150 / (1 + MainStat.SpellHaste))+ sim._MainFrm.txtLatency.Text/10
	End Function
	
	Function ApplyDamage(T As Long) As Boolean
		
		
		dim RNG as Double
		RNG = Rnd
		If mainstat.SpellHit >= 0.17 Then
			RNG = RNG+0.17
		Else
			RNG = RNG+mainstat.SpellHit
		End If
		If RNG < 0.17 Then
			'combatlog.write(T  & vbtab &  "WP fail")
			MissCount = MissCount + 1
			Exit function
		End If
		total = total + AvrgNonCrit(T)
		HitCount = HitCount + 1
		nextTick = T + 100
		return true		
	End Function
	Function AvrgNonCrit(T As long) As Double
		Dim tmp As Double
		tmp = 48
		tmp = tmp + 0.013 * (1 + 0.04 * TalentUnholy.Impurity) * MainStat.AP
		tmp = tmp * MainStat.StandardMagicalDamageMultiplier(T)
		tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
		tmp = tmp * (1 + TalentUnholy.CryptFever * 10 / 100)
		AvrgNonCrit = tmp
	End Function
	
	Function CritCoef() As Double
		
	End Function
	Function CritChance() As Double
		
	End Function
	Function AvrgCrit() As Double
		
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

	End Sub
	Function report As String
		dim tmp as String
		tmp = "UB" & VBtab
	
		If total.ToString().Length < 8 Then
			tmp = tmp & total & "   " & VBtab
		Else
			tmp = tmp & total & VBtab
		End If
		tmp = tmp & toDecimal(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount)) & VBtab
		tmp = tmp & toDecimal(total/(HitCount+CritCount)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
end module