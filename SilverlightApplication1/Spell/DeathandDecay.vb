'
' Created by SharpDevelop.
' User: Fabien
' Date: 24/03/2009
' Time: 22:35
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class DeathandDecay
    Inherits Spells.Spell

    Friend nextTick As Long
	
	Sub New(MySim As Sim)
        MyBase.New(MySim)
        BaseDamage = 31
        Coeficient = (0.0475 * (1 + 0.2 * sim.Character.Talents.Talent("Impurity").Value))
        Multiplicator = 1
        If sim.Character.Glyph.DeathandDecay Then Multiplicator *= 1.2
        If sim.MainStat.T102PTNK = 1 Then Multiplicator *= 1.2
    End Sub
	
	Public Overloads Overrides Sub Init()
		MyBase.init()
		nextTick = 0
		ThreadMultiplicator = 1.9
	End Sub
	
	Function isAvailable(T As Long) As Boolean
		if CD > T then return false
        If sim.Runes.Unholy() Then Return True
        Return False
	End Function
	
	Function Apply(T As Long) As Boolean
		UseGCD(T)
        nextTick = T + 100
        sim.Runes.UseUnholy(T, False)
        ActiveUntil = T + 1000
        CD = T + 3000 - sim.Character.Talents.Talent("Morbidity").Value * 500
        sim.RunicPower.add(10)
        sim.combatlog.write(T & vbtab & "D&D ")
        sim.FutureEventManager.Add(nextTick, "D&D")

        Return True
    End Function

    Overrides Function ApplyDamage(ByVal T As Long) As Boolean
        Dim RNG As Double

        Dim Tar As Targets.Target

        For Each Tar In sim.Targets.AllTargets
            If DoMySpellHit = False Then
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & Me.Name & " fail")
                MissCount = MissCount + 1
            Else
                RNG = RngCrit
                If RNG <= CritChance() Then
                    LastDamage = AvrgCrit(T, Tar)
                    If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & Me.Name & " crit for " & LastDamage)
                    CritCount = CritCount + 1
                    TotalCrit += LastDamage
                Else
                    LastDamage = AvrgNonCrit(T, Tar)
                    HitCount = HitCount + 1
                    TotalHit += LastDamage
                    If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & Me.Name & " hit for " & LastDamage)
                End If
                total = total + LastDamage
            End If
        Next
        nextTick = T + 100
        If nextTick > ActiveUntil Then
            nextTick = T - 1
        Else
            sim.FutureEventManager.Add(nextTick, "D&D")
        End If
        Return True
    End Function

	Overrides Function AvrgCrit(T As long,target as Targets.Target) As Double
		AvrgCrit = AvrgNonCrit(T,target) * (0.5 + CritCoef)
	End Function
	

End Class
