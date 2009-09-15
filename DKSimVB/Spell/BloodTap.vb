'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 23:59
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class BloodTap
	Friend cd As Double
	Protected Sim As Sim
	
	Sub New(MySim as Sim)
		cd = 0
		sim = MySim 
	End Sub
	Function IsAvailable(T as long) As Boolean
		If T >= cd Then
			return true
		End If
	End Function
	
	
	
	Function Use(T As long) As Boolean
		cd = t + 6000
		If sim.Rune1.AvailableTime > T And sim.Rune1.death = False Then
			sim.Rune1.AvailableTime = T
			sim.Rune1.death = True
		Else
			sim.Rune2.AvailableTime = T
			sim.Rune2.death = True
		End If
		combatlog.write(T  & vbtab &  "Blood Tap")
		return true
	End Function
		
	
		
End Class
