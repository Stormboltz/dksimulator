Friend module UnholyBlight
	Friend total As long
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount as Integer
	Friend TotalHit As Long
	Friend TotalCrit as Long
	Function Apply(T As Long,damage As Integer) As Double
		Dim dégat As Integer
		dégat = damage * 0.2
		
		If glyph.UnholyBlight Then dégat = dégat * 1.4
		HitCount = HitCount + 1
		total = total + dégat
		combatlog.write(T  & vbtab &  "UnholyBlight hit for " &  dégat )	
	End Function
	Function AvrgNonCrit(T As long) As Double
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