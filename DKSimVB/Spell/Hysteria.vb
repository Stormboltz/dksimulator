'
' Created by SharpDevelop.
' User: Fabien
' Date: 20/03/2009
' Time: 11:12
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class Hysteria
	inherits Spells.Spell
	Sub New(S As sim)
		MyBase.New()
		Sim = S
	End Sub
	Function IsAvailable(T As Long) As Boolean
		If TalentBlood.Hysteria =  0 Then Return False 
		If TalentBlood.DRW = 1 and sim.DRW.cd > T then return false
		If CD <= T Then Return True
	End Function

	Function IsActive(T as Long) as Boolean
		if T <= ActiveUntil then return true
	End Function
	
	Sub use(T As Long)
		CD = T + 3*60*100
		ActiveUntil = T + 3000
	End Sub
	
	
End Class
