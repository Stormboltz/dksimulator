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
	
	Sub New(S As sim)
		MyBase.New()
		Sim = S
	End Sub
	
	
	Function isAutoAvailable(T As Long) As Boolean
		If sim.Rotate = True Then
			If sim.Rotation.MyRotation.Contains("Horn") Then Return False
		Else
			if sim.Priority.prio.Contains("Horn") then return false
		End If
		return isAvailable(T)
	End Function
	
	
	
	Function isAvailable(T As Long) As Boolean
		If Sim.RunicPower.Value + 10 >= Sim.RunicPower.MaxValue Then Return False
		If sim.glyph.Disease Then 
			'return false
			'if math.Min(BloodPlague.FadeAt,FrostFever.FadeAt) < T + 500 then return false
		End If
		If cd <= T Then Return True
	End Function
	
	Function use(T As Long)
		cd = t + 20 * 100
		Sim.RunicPower.add(10)
		HitCount = HitCount + 1
		Sim.NextFreeGCD = T + (150 / (1 + sim.MainStat.SpellHaste))+ sim._MainFrm.txtLatency.Text/10
		sim.combatlog.write(T  & vbtab &  "Horn used")
		return true
	End Function
	
	
End Class
