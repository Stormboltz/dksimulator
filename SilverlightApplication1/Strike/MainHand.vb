Namespace Simulator.WowObjects.Strikes
    Friend Class MainHand
        Inherits Strike



        Sub New(ByVal S As Sim)
            MyBase.New(S)
        End Sub


        Friend NextWhiteMainHit As Long


        Protected Overrides Sub init()
            MyBase.init()
            NextWhiteMainHit = 0
            HasteSensible = True
            logLevel = LogLevelEnum.Detailled
        End Sub

        Overrides Function ApplyDamage(ByVal T As Long) As Boolean

            Dim WSpeed As Single
            Dim MeleeMissChance As Single
            Dim MeleeDodgeChance As Single
            Dim MeleeGlacingChance As Single
            Dim MeleeParryChance As Single
            Dim ChanceNotToTouch As Single

            WSpeed = sim.Character.MHWeaponSpeed

            NextWhiteMainHit = T + (WSpeed * 100) / sim.Character.PhysicalHaste
            sim.FutureEventManager.Add(NextWhiteMainHit, "MainHand")



            Dim RNG As Double
            RNG = RngHit()
            MeleeGlacingChance = 0.25
            MeleeDodgeChance = 0.065
            If sim.BloodPresence = 1 Then
                MeleeParryChance = 0.14
            Else
                MeleeParryChance = 0
            End If
            If sim.Character.DualW Then
                MeleeMissChance = 0.27
            Else
                MeleeMissChance = 0.08
            End If

            ChanceNotToTouch = Math.Max(0, MeleeMissChance - sim.Character.Hit) + Math.Max(0, MeleeDodgeChance - sim.Character.MHExpertise) + Math.Max(0, MeleeParryChance - sim.Character.MHExpertise)

            If RNG < ChanceNotToTouch Then
                MissCount = MissCount + 1
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "MH fail")
                Return False
            End If

            If RNG < (ChanceNotToTouch + MeleeGlacingChance) Then
                LastDamage = AvrgNonCrit(T) * 0.7
                GlancingCount = GlancingCount + 1
                TotalGlance += LastDamage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "MH glancing for " & LastDamage)
            End If

            If RNG >= (ChanceNotToTouch + MeleeGlacingChance) And RNG < (ChanceNotToTouch + MeleeGlacingChance + CritChance()) Then
                'CRIT !
                LastDamage = AvrgCrit(T)
                CritCount = CritCount + 1
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "MH crit for " & LastDamage)

                sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnCrit)

                TotalCrit += LastDamage
            End If
            If RNG >= (ChanceNotToTouch + MeleeGlacingChance + CritChance()) Then
                'normal hit3
                LastDamage = AvrgNonCrit(T)
                HitCount = HitCount + 1
                TotalHit += LastDamage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "MH hit for " & LastDamage)
            End If


            total = total + LastDamage
            If sim.Character.Talents.Talent("Necrosis").Value > 0 Then sim.Necrosis.Apply(LastDamage, T)
            If sim.proc.MHBloodCakedBlade.TryMe(T) Then sim.BloodCakedBlade.ApplyDamage(T)
            sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnMHWhiteHit)

            If sim.proc.ScentOfBlood.IsActive Then
                sim.proc.ScentOfBlood.Use()
                sim.RunicPower.add(10)
            End If
            Return True
        End Function
        Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Dim tmp As Double
            tmp = sim.Character.MHBaseDamage
            tmp = tmp * sim.Character.WhiteHitDamageMultiplier(T)
            If sim.EPStat = "EP HasteEstimated" Then
                tmp = tmp * sim.Character.EstimatedHasteBonus
            End If
            AvrgNonCrit = tmp
        End Function

        Overrides Function CritChance() As Double
            CritChance = sim.Character.critAutoattack
        End Function


        Public Overrides Sub Merge()
            _Name = "Melee"
            If sim.Character.DualW = False Then Exit Sub
            total += sim.OffHand.total
            TotalHit += sim.OffHand.TotalHit
            TotalCrit += sim.OffHand.TotalCrit
            TotalGlance += sim.OffHand.TotalGlance

            MissCount = (MissCount + sim.OffHand.MissCount)
            HitCount = (HitCount + sim.OffHand.HitCount)
            CritCount = (CritCount + sim.OffHand.CritCount)
            GlancingCount = GlancingCount + sim.OffHand.GlancingCount


            sim.OffHand.total = 0
            sim.OffHand.TotalHit = 0
            sim.OffHand.TotalCrit = 0
            sim.OffHand.TotalGlance = 0
        End Sub

    End Class
End Namespace