'
' Created by SharpDevelop.
' User: Fabien
' Date: 07/07/2009
' Time: 19:04
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Module Horn
	Friend TotalHit As Long
	Friend HitCount As Integer
	
	Friend cd As Long
	Function isAvailable(T as Long) as Boolean
		If cd <= T Then Return True
	End Function
	
	Sub init()
		HitCount = 0
		TotalHit = 0
		cd = 0
	End Sub
	
	Sub use(T As Long)
		cd = t + 20 * 100
		RunicPower.add(10)
		HitCount = HitCount + 1
		Sim.NextFreeGCD = T + (150 / (1 + MainStat.SpellHaste))+ sim._MainFrm.txtLatency.Text/10
	End Sub
	
	Function report As String
		Dim tmp As String
		Dim total As String
		total = "0"
		tmp = "Horn" & VBtab
		
		If total.ToString().Length < 8 Then
			tmp = tmp & total & "   " & VBtab
		Else
			tmp = tmp & total & VBtab
		End If
		tmp = tmp & int(100*total/sim.TotalDamage) & VBtab
		tmp = tmp & int(HitCount) & VBtab
		tmp = tmp & int(100*0/(HitCount+0+0)) & VBtab
		tmp = tmp & int(100*0/(HitCount+0+0)) & VBtab
		tmp = tmp & int(100*0/(HitCount+0+0)) & VBtab
		tmp = tmp & int(total/(HitCount+0)) & VBtab
		tmp = tmp & vbCrLf
		return tmp
	End Function
End Module
