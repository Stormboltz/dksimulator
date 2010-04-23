Friend Class WanderingPlague
	Inherits Spells.Spell
	
	Friend nextTick As Double

	
	Sub New(S As sim)
		MyBase.New(s)
		nextTick = 0
	End Sub
	Function isAvailable(T As long) As Boolean
		'internal CD of 1s
		If nextTick < T Then
			isAvailable = True
		Else
			isAvailable = False
		End If
	End Function
	
	Function ApplyDamage(Damage As Double, T As long) As Double
		nextTick = T + 200
		Dim tmp As Integer
		Dim intCount As Integer
		For intCount = 1 To Sim.NumberOfEnemies
			If DoMySpellHit = false Then
				MissCount = MissCount + 1
			End If
			tmp =  Damage * sim.Character.talentunholy.WanderingPlague / 3
			total = total + tmp
			HitCount = HitCount + 1
			If sim.combatlog.LogDetails Then sim.combatlog.write(T  & vbtab &  "Wandering Plague hit for " & tmp )
		Next intCount
		return true
	End Function
	
End Class