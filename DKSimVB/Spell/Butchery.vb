'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 23:38
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Module Butchery
	Friend nextTick As Double
	
	Function apply(T As long) as Boolean
		nextTick = T + 500
		runicpower.add(1*talentblood.Butchery)
	End Function
		
	Sub init()
		nextTick = 0
	End Sub
		
End Module
