'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 23:59
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class BloodTap
	Inherits Spells.Spell
	
	Sub New(s As Sim)
		MyBase.New(s)
	End Sub
	Function IsAvailable(T as long) As Boolean
		If T >= cd Then
			return true
		End If
	End Function
	
	
	
	Function Use(T As long) As Boolean
		cd = t + 6000
		If sim.Runes.BloodRune1.AvailableTime > T And sim.runes.BloodRune1.death = False Then
			sim.Runes.BloodRune1.AvailableTime = T
			sim.Runes.BloodRune1.death = True
			sim.Runes.BloodRune1.BTuntil = T + 2000
		Else
			sim.Runes.BloodRune2.AvailableTime = T
			sim.Runes.BloodRune2.death = True
			sim.Runes.BloodRune2.BTuntil = T + 2000
		End If
		sim.combatlog.write(T  & vbtab &  "Blood Tap")
		sim.RunicPower.add(10)
		me.HitCount = me.HitCount + 1 
		return true
	End Function
		
	
		
End Class
