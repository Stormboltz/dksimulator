'
' Created by SharpDevelop.
' User: Fabien
' Date: 15/03/2009
' Time: 22:44
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module Desolation
	Friend FadeAt As long
	Private _Bonus As Double
	
	
		
	Sub init()
		FadeAt=0
	End Sub
		
	
	
	Function Bonus as Double
	
		_bonus = TalentUnholy.Desolation/100
		'_bonus = _bonus/100
		Return _bonus
		
	End Function
	Function isActive(T As long) As Boolean
		If T >= FadeAt Then
			isActive = False
		Else
			isActive = True
		End If
		
	End Function
	Function Apply(T As long) As Boolean
		FadeAt = T + 2000
		return true
	End Function
End Module
