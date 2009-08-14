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
	
	Function IsAvailable(T as Long) as Boolean
		if CD <= T then return true
	End Function

	Function IsActive(T as Long) as Boolean
		if ActiveUntil > T then return true
	End Function
	
	Sub use(T As Long)
		CD = T + 3*60*100
		ActiveUntil = T + 3000
	End Sub
	
	
End Module
