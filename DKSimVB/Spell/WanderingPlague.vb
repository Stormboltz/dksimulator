Friend Class WanderingPlague
	
	Friend nextTick As Double
	Friend total As long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	Friend TotalHit As Long
	Friend TotalCrit as Long
	
	Protected sim as Sim
	Sub New(S As Sim)
		Sim = S
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		nextTick = 0
		TotalHit = 0
		TotalCrit = 0
		
	End Sub
	
	Function isAvailable(T As long) As Boolean
		'internal CD of 1s
		If nextTick < T Then
			isAvailable = True
		Else
			isAvailable = False
		End If
	End Function
	
	Function ApplyDamage(Damage As Double, T As long) As Double
		nextTick = T + 100
		
		If Sim.DoMySpellHit = false Then
			'combatlog.write(T  & vbtab &  "WP fail")
			MissCount = MissCount + 1
			Exit function
		End If
		Dim tmp As Integer
		tmp =  Damage * TalentUnholy.WanderingPlague / 3
		total = total + tmp
		HitCount = HitCount + 1
		If combatlog.LogDetails Then combatlog.write(T  & vbtab &  "Wandering Plague hit for " & tmp )
		'combatlog.write(T  & vbtab &  "WP hit for " & Damage * TalentUnholy.WanderingPlague / 3)
		return true
	End Function
	Function report As String
		dim tmp as String
		tmp = "Wandering Plague" & VBtab
		
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
End Class