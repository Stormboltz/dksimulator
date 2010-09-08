Namespace Simulator.WowObjects.Strikes
    Friend Class OffHand
        Inherits Strike

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            HasteSensible = True
            logLevel = LogLevelEnum.Detailled
        End Sub
        Friend NextWhiteOffHit As Long


        Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            Dim Nec As Double

            Dim WSpeed As Single
            Dim RNG As Double
            WSpeed = sim.Character.OHWeaponSpeed


            NextWhiteOffHit = T + (WSpeed * 100) / sim.Character.PhysicalHaste

            sim.FutureEventManager.Add(NextWhiteOffHit, "OffHand")



            Dim MeleeMissChance As Single
            Dim MeleeDodgeChance As Single
            Dim MeleeGlacingChance As Single
            Dim MeleeParryChance As Single
            Dim ChanceNotToTouch As Single


            RNG = RngHit()
            MeleeGlacingChance = 0.25
            MeleeDodgeChance = 0.065
            MeleeMissChance = 0.27
            If sim.BloodPresence = 1 Then
                MeleeParryChance = 0.14
            Else
                MeleeParryChance = 0
            End If

            ChanceNotToTouch = Math.Max(0, MeleeMissChance - sim.Character.Hit) + Math.Max(0, MeleeDodgeChance - sim.Character.OHExpertise) + Math.Max(0, MeleeParryChance - sim.Character.OHExpertise)

            If RNG < ChanceNotToTouch Then
                MissCount = MissCount + 1
                'If sim.combatlog.LogDetails Then 
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "OH fail")
                'End If
                Return False
            End If


            If RNG < (ChanceNotToTouch + MeleeGlacingChance) Then
                'Glancing
                LastDamage = AvrgNonCrit(T) * 0.7
                GlancingCount = GlancingCount + 1
                TotalGlance += LastDamage
                'If sim.combatlog.LogDetails Then 
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "OH glancing for " & LastDamage)
                'End If
            End If

            If RNG >= (ChanceNotToTouch + MeleeGlacingChance) And RNG < (ChanceNotToTouch + MeleeGlacingChance + CritChance()) Then
                'CRIT !
                CritCount = CritCount + 1
                LastDamage = AvrgCrit(T)
                'If sim.combatlog.LogDetails Then 
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "OH crit for " & LastDamage)
                'End If
                sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnCrit)
                TotalCrit += LastDamage
            End If

            If RNG >= (ChanceNotToTouch + MeleeGlacingChance + CritChance()) Then
                'normal hit3
                LastDamage = AvrgNonCrit(T)
                HitCount = HitCount + 1
                'If sim.combatlog.LogDetails Then 
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "OH hit for " & LastDamage)
                'End If
                TotalHit += LastDamage

            End If

            If sim.proc.ScentOfBlood.IsActive Then
                sim.proc.ScentOfBlood.Use()
                sim.RunicPower.add(10)
            End If
            total = total + LastDamage
            If sim.Character.Talents.Talent("Necrosis").Value > 0 Then
                Nec = sim.OHNecrosis.Apply(LastDamage, T)
            End If
            If sim.proc.OHBloodCakedBlade.TryMe(T) Then sim.OHBloodCakedBlade.ApplyDamage(T)

            sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnOHWhitehit)
            Return True
        End Function
        Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Dim tmp As Double
            tmp = sim.Character.OHBaseDamage
            tmp = tmp * sim.Character.WhiteHitDamageMultiplier(T)
            tmp = tmp * 0.5
            tmp = tmp * (1 + sim.Character.Talents.Talent("NervesofColdSteel").Value * 8.3333 / 100)
            If sim.EPStat = "EP HasteEstimated" Then
                tmp = tmp * sim.Character.EstimatedHasteBonus
            End If
            AvrgNonCrit = tmp
        End Function

        Overrides Function CritChance() As Double
            Dim tmp As Double
            tmp = sim.Character.critAutoattack
            CritChance = tmp
        End Function

    End Class
End Namespace