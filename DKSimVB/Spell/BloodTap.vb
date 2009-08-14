'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 23:59
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module BloodTap
	Friend cd As Double
	
	Function IsAvailable(T as long) As Boolean
		If T >= cd Then
			return true
		End If
	End Function
	Function Use(T As long) As Boolean
		cd = t + 6000
		If Rune1.AvailableTime > T And Rune1.death = False Then
			Rune1.AvailableTime = T
			Rune1.death = True
		Else
			Rune2.AvailableTime = T
			Rune2.death = True
		End If
		combatlog.write(T  & vbtab &  "Blood Tap")
		return true
	End Function
		
	Sub init()
		cd = 0
	End Sub
		
End Module
