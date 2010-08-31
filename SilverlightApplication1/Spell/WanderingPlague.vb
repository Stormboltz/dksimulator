Friend Class WanderingPlague
	Inherits Spells.Spell
	
	Friend nextTick As Double

	
	Sub New(S As sim)
		MyBase.New(s)
        nextTick = 0
        logLevel = LogLevelEnum.detailed
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
		
		Dim Tar As Targets.Target
		
		For Each Tar In sim.Targets.AllTargets
			If DoMySpellHit = false Then
				MissCount = MissCount + 1
			End If
            tmp = Damage * sim.Character.Talents.Talent("WanderingPlague").Value / 3
			total = total + tmp
			HitCount = HitCount + 1
			If sim.combatlog.LogDetails Then sim.combatlog.write(T  & vbtab &  "Wandering Plague hit for " & tmp )
		Next
        Return tmp
	End Function
	
End Class