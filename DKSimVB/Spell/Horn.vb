'
' Created by SharpDevelop.
' User: Fabien
' Date: 07/07/2009
' Time: 19:04
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Class Horn
	Inherits Spells.Spell
	
	
	Function isAvailable(T As Long) As Boolean
		If RunicPower.Value + 20 >= RunicPower.MaxValue Then Return False
		If glyph.Disease Then 
			'return false
			'if math.Min(BloodPlague.FadeAt,FrostFever.FadeAt) < T + 500 then return false
		End If
		If cd <= T Then Return True
	End Function
	
	Sub use(T As Long)
		cd = t + 20 * 100
		RunicPower.add(10)
		HitCount = HitCount + 1
		Sim.NextFreeGCD = T + (150 / (1 + MainStat.SpellHaste))+ sim._MainFrm.txtLatency.Text/10
	End Sub
	
	
End Class
