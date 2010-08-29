Friend class DeathCoil
	Inherits Spells.Spell
	Sub New(S As sim)
        MyBase.New(S)
        'Base Damage
        BaseDamage = 885
        If sim.Sigils.VengefulHeart Then BaseDamage += 380
        If sim.Sigils.WildBuck Then BaseDamage += 80

        Coeficient = (0.15 * (1 + 0.2 * sim.Character.Talents.Talent("Impurity").Value))
        Multiplicator = 1 + sim.Character.Talents.Talent("Morbidity").Value * 5 / 100
        If sim.Character.Glyph.DarkDeath Then
            Multiplicator = Multiplicator * (1.15)
        End If
        SpecialCritChance = 8 * sim.MainStat.T82PDPS / 100
    End Sub

	

	Function isAvailable(T As long) As Boolean
        Return sim.RunicPower.CheckRS(40) Or sim.proc.SuddenDoom.IsActive
    End Function

    Overrides Function ApplyDamage(ByVal T As Long) As Boolean

        If sim.proc.SuddenDoom.IsActive Then
            sim.proc.SuddenDoom.Use()
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
            sim.RunicPower.Use(40)
        End If
        UseGCD(T)
        Dim ret As Boolean = MyBase.ApplyDamage(T)
        If ret = False Then
            Return False
        End If

        sim.proc.tryProcs(Procs.ProcOnType.onRPDump)
        If sim.Character.Talents.Talent("UnholyBlight").Value = 1 Then
            sim.UnholyBlight.Apply(T, LastDamage)
        End If
        If sim.DRW.IsActive(T) Then
            sim.DRW.DRWDeathCoil()
        End If
        Return True

    End Function
    Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
        Dim tmp As Double
        tmp = MyBase.AvrgNonCrit(T, target)
        If sim.RuneForge.CheckCinderglacier(True) > 0 Then tmp *= 1.2
        Return tmp
    End Function

end class