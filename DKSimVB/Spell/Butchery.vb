'
' Created by SharpDevelop.
' User: Fabien
' Date: 14/03/2009
' Time: 23:38
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class Butchery
	Friend nextTick As long
	
	Sub New()
		init
	End Sub
	
	Function apply(T As long) as Boolean
		nextTick = T + 500
		Sim.runicpower.add(1*talentblood.Butchery)
	End Function
		
	private Sub init()
		nextTick = 0
	End Sub
		
End Class
