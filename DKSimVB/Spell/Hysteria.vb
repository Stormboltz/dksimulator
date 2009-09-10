'
' Created by SharpDevelop.
' User: Fabien
' Date: 20/03/2009
' Time: 11:12
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module Hysteria
	Friend Cd as Long
	Private ActiveUntil As Long
	
	Sub init()
		cd = 0
		ActiveUntil= 0
	End Sub
	
	Function IsAvailable(T As Long) As Boolean
		If TalentBlood.Hysteria =  0 Then Return False 
		If TalentBlood.DRW = 1 and DRW.cd > T then return false
		If CD <= T Then Return True
		
	End Function

	Function IsActive(T as Long) as Boolean
		if T <= ActiveUntil then return true
	End Function
	
	Sub use(T As Long)
		CD = T + 3*60*100
		ActiveUntil = T + 3000
	End Sub
	
	
End Module
