'
' Created by SharpDevelop.
' User: Fabien
' Date: 15/03/2009
' Time: 22:44
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend CLass Desolation
	Friend FadeAt As long
	
	private sim as sim
	
	Sub New(S As sim)
		sim = S
		Init
	End Sub
		
	Sub init()
		FadeAt=0
	End Sub
	Function bonus As double
		return (sim.TalentUnholy.Desolation/100)
	End Function
	Function isActive(T As long) As Boolean
		If T >= FadeAt Then
			isActive = False
		Else
			isActive = True
		End If
		
	End Function
	
	Function Apply(T As long) As Boolean
		FadeAt = T + 20 * 100
		return true
	End Function
	
End Class
