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
	Protected Sim as Sim
	Sub New(MySim as Sim)
		init
		Sim = Mysim
	End Sub
	
	Function apply(T As long) as Boolean
		nextTick = T + 500
		sim.FutureEventManager.Add(nextTick,"Butchery")
		Sim.runicpower.add(1*sim.talentblood.Butchery)
	End Function
		
	private Sub init()
		nextTick = 0
		
	End Sub
		
End Class
