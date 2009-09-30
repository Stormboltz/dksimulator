Friend Class WanderingPlague
	Inherits Spells.Spell
	
	Friend nextTick As Double

	
	
	Sub New(S As Sim)
		Sim = S
		total = 0
		MissCount = 0
		HitCount = 0
		CritCount = 0
		nextTick = 0
		TotalHit = 0
		TotalCrit = 0
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
		nextTick = T + 100
		
		Dim tmp As Integer
		Dim intCount As Integer
		For intCount = 1 To Sim.NumberOfEnemies
			If DoMySpellHit = false Then
				'combatlog.write(T  & vbtab &  "WP fail")
				MissCount = MissCount + 1
				'Exit function
			End If
			
			tmp =  Damage * TalentUnholy.WanderingPlague / 3
			total = total + tmp
			HitCount = HitCount + 1
			If sim.combatlog.LogDetails Then sim.combatlog.write(T  & vbtab &  "Wandering Plague hit for " & tmp )
			'combatlog.write(T  & vbtab &  "WP hit for " & Damage * TalentUnholy.WanderingPlague / 3)
		Next intCount
		return true
	End Function
	
End Class