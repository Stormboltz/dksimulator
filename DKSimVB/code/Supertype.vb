'
' Created by SharpDevelop.
' User: Fabien
' Date: 01/01/2010
' Time: 18:15
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Class Supertype
	
	Friend MissCount As Integer
	Friend HitCount as Integer
	Friend CritCount As Integer
	Friend GlancingCount As Integer
	
	Public total As Long
	Friend TotalHit As Long
	Friend TotalCrit As Long
	Friend TotalGlance as Long
	Friend _Name  as String
	Friend isGuardian as Boolean = false
	Friend HasteSensible as Boolean
	Public ThreadMultiplicator As Double
	Friend uptime As Long
	Protected sim As Sim
	Protected _RNG as Random
	
	
	
	
	Overridable Function report As String
		Dim tmp As String
		If HitCount+CritCount = 0 Then Return ""
		
		tmp = ShortenName(me.Name)  & VBtab

		tmp = tmp & total & VBtab
		tmp = tmp & toDecimal(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & toDecimal(HitCount+CritCount+GlancingCount) & VBtab
		tmp = tmp & toDecimal(total/(HitCount+CritCount)) & VBtab
		
		tmp = tmp & toDecimal(HitCount) & VBtab
		tmp = tmp & toDecimal(100*HitCount/(HitCount+MissCount+CritCount+GlancingCount)) & VBtab
		tmp = tmp & toDecimal(totalhit/(HitCount)) & VBtab
		
		tmp = tmp & toDecimal(CritCount) & VBtab
		tmp = tmp & toDecimal(100*CritCount/(HitCount+MissCount+CritCount+GlancingCount)) & VBtab
		tmp = tmp & toDecimal(totalcrit/(CritCount)) & VBtab
				
		tmp = tmp & toDecimal(MissCount) & VBtab
		tmp = tmp & toDecimal(100*MissCount/(HitCount+MissCount+CritCount+GlancingCount)) & VBtab
		
		tmp = tmp & toDecimal(GlancingCount) & VBtab
		tmp = tmp & toDecimal(100*GlancingCount/(HitCount+MissCount+CritCount+GlancingCount)) & VBtab
		tmp = tmp & toDecimal(TotalGlance/(GlancingCount)) & VBtab

		If sim.FrostPresence Then
			tmp = tmp & toDecimal((100 * total * ThreadMultiplicator * 2.0735 ) / sim.TimeStamp) & VBtab
		End If
		
		tmp = tmp & ""& toDecimal(100*uptime/sim.MaxTime)  & "" & VBtab
		tmp = tmp & vbCrLf
		
		
		
		tmp = replace(tmp, VBtab & 0, vbtab)
		
		return tmp
	End Function
	
	Overridable Sub Merge()
		
	End Sub
	
	Overridable Public Function Name() As String
		if _name <> "" then return _name
		return me.ToString
	End Function
	
	Function MyRng as Double
		If _RNG Is nothing Then
			_RNG =  New Random(ConvertToInt(Me.name)+RNGSeeder)
			
		End If
		return _RNG.NextDouble
	End Function
	
	
End Class
