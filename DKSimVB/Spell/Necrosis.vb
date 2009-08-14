Friend module Necrosis

Friend total As long
Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
		Friend TotalHit As Long
	Friend TotalCrit as Long

	
Function ApplyDamage(Damage As Double, T As long) As Double
	Dim tmp As Double
	tmp  = Damage * 0.04 * TalentUnholy.Necrosis
	'tmp  = tmp  * mainstat.StandardMagicalDamageMultiplier(T)
	'tmp = tmp * (1 + TalentFrost.BlackIce * 2 / 100)
	total = total + tmp 
	HitCount = HitCount + 1
	if combatlog.LogDetails then combatlog.write(T  & vbtab &  "Necrosis hit for " & tmp)
    return tmp
End Function
	Sub init()
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		TotalHit = 0
		TotalCrit = 0

	End Sub
Function report As String
		dim tmp as String
		tmp = "Necrosis" & VBtab
	
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