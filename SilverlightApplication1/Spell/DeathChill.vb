'
' Created by SharpDevelop.
' User: e0030653
' Date: 26/03/2009
' Time: 09:42
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class DeathChill
	Friend Cd As Long
	Friend Active As Boolean
	Protected Sim as Sim
	
	
	
	Sub New(MySim as Sim)
		cd = 0
		Active= False
		Sim = Mysim	
	End Sub
	Function IsAvailable(T As Long) As Boolean
		if sim.Character.talentfrost.Deathchill = 1 and CD <= T then return true
	End Function
	Sub use(T As Long)
		CD = T + 3*60*100
		sim.Threat = sim.Threat + 55
		Active = true
	End Sub
End Class
