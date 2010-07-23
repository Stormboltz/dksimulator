Friend class DeathCoil
	Inherits Spells.Spell
	Sub New(S As sim)
		MyBase.New(s)
	End Sub

	

	Function isAvailable(T As long) As Boolean
        'If sim.DRW.cd <= T And sim.Character.Talents.Talent("DRW = 1 And Sim.RunicPower.Value < 100 Then Return False
        'If sim.Gargoyle.cd <= T And sim.Character.Talents.Talent("SummonGargoyle = 1 And Sim.RunicPower.Value < 100 Then Return False
        'If glyph.DeathStrike And RunicPower.Value <= 65  Then Return False 'This is not really important
        Return Sim.RunicPower.CheckRS(40)
    End Function

    Overrides Function ApplyDamage(ByVal T As Long, ByVal SDoom As Boolean) As Boolean
        Dim RNG As Double

        If SDoom Then

        Else
            If sim.Character.Talents.Talent("DRW").Value = 1 Then
                If sim.DRW.cd < T And sim.RunicPower.Check(60) Then
                    If sim.DRW.Summon(T) = True Then Return True
                End If
            End If
            If sim.PetFriendly And sim.Character.Talents.Talent("SummonGargoyle").Value = 1 Then
                If sim.Gargoyle.cd < T And sim.RunicPower.Check(60) Then
                    If sim.Gargoyle.Summon(T) = True Then Return True
                End If
            End If
            UseGCD(T)
            Sim.RunicPower.Use(40)
        End If

        If DoMySpellHit = False Then
            sim.combatlog.write(T & vbtab & "DC fail")
            MissCount = MissCount + 1
            Return False
        End If

        RNG = RngCrit
        Dim dégat As Integer
        If RNG <= CritChance Then
            CritCount = CritCount + 1
            dégat = AvrgCrit(T)
            totalcrit += dégat


            If SDoom Then
                If sim.CombatLog.LogDetails Then
                    sim.combatlog.write(T & vbtab & "DC SDoom crit for " & dégat)
                End If
            Else
                sim.combatlog.write(T & vbtab & "DC crit for " & dégat)
            End If
        Else
            dégat = AvrgNonCrit(T)
            totalhit += dégat
            HitCount = HitCount + 1
            If SDoom Then
                If sim.CombatLog.LogDetails Then
                    sim.combatlog.write(T & vbtab & "DC SDoom hit for " & dégat)
                End If
            Else
                sim.combatlog.write(T & vbtab & "DC hit for " & dégat & vbtab)
            End If

        End If


        total = total + dégat
        sim.proc.TryOnSpellHit()
        sim.proc.TryOnonRPDumpProcs()
        If sim.Character.Talents.Talent("UnholyBlight").Value = 1 Then
            sim.UnholyBlight.Apply(T, dégat)
        End If
        If sim.DRW.IsActive(T) Then
            sim.DRW.DRWDeathCoil()
        End If
        Return True

    End Function
    Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
        Dim tmp As Double
        tmp = 885
        If sim.sigils.VengefulHeart Then tmp = tmp + 380
        If sim.Sigils.WildBuck Then tmp = tmp + 80
        tmp = tmp + (0.15 * (1 + 0.04 * sim.Character.Talents.Talent("Impurity").Value) * sim.MainStat.AP)
        tmp = tmp * (1 + sim.Character.Talents.Talent("Morbidity").Value * 5 / 100)
        tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)

        If sim.character.glyph.DarkDeath Then
            tmp = tmp * (1.15)
        End If
        If sim.RuneForge.CheckCinderglacier(True) > 0 Then tmp *= 1.2
        Return tmp
    End Function
	overrides Function CritCoef() As Double
		CritCoef = 1
		CritCoef = CritCoef * (1+0.06*sim.mainstat.CSD)
	End Function
	overrides Function CritChance() As Double
		CritChance = sim.MainStat.SpellCrit + 8/100 * sim.MainStat.T82PDPS
	End Function
	overrides Function AvrgCrit(T As long,target As Targets.Target) As Double
		AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef)
	End Function
	
end class