'
' Created by SharpDevelop.
' User: Fabien
' Date: 07/07/2009
' Time: 19:04
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Module Horn
	Friend cd As Long
	Function isAvailable(T as Long) as Boolean
		If cd <= T Then Return True
	End Function
	
	Sub use(T As Long)
		cd = t+2000
		RunicPower.add(10)
		Sim.NextFreeGCD = T + (150 / (1 + MainStat.SpellHaste))+ sim._MainFrm.txtLatency.Text/10
	End Sub
	
	
	
End Module
