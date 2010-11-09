Imports DKSIMVB.Simulator.WowObjects.Strikes

Namespace Simulator.WowObjects.PetsAndMinions



    Friend Class DRW
        Inherits WowObject

        Friend NextDRW As Long

        Friend ActiveUntil As Long
        Friend cd As Long
        Private Haste As Double
        Private SpellHaste As Double
        Private AP As Integer
        Private _Crit As Double
        Private _SpellCrit As Double

        Private MeleeMissChance As Single
        Private MeleeDodgeChance As Single
        Private MeleeGlacingChance As Single
        Private SpellMissChance As Single
        Private Hyst As Boolean


        Friend PlaqueStrike As Strike
        Friend Obliterate As Strikes.Strike
        Friend HeartStrike As Strikes.Strike
        Friend DeathStrike As Strikes.Strike
        Friend DeathCoil As Spells.Spell
        Friend IcyTouch As Spells.Spell

        Friend RuneStrike As Strikes.Strike


        Sub New(ByVal S As Sim)
            MyBase.New(S)
            _Name = "DRW: Melee"
            total = 0
            MissCount = 0
            HitCount = 0
            CritCount = 0
            cd = 0
            ActiveUntil = 0
            NextDRW = 0
            TotalHit = 0
            TotalCrit = 0
            sim = S
            sim.DamagingObject.Add(Me)
            ThreadMultiplicator = 1
            HasteSensible = True
            isGuardian = True

            If sim.Character.Talents.Talent("DRW").Value > 0 Then
                Talented = True
            End If
            PlaqueStrike = New Strikes.Strike(sim)
            PlaqueStrike._Name = "DRW: Plaque Strike"

            Obliterate = New Strikes.Strike(sim)
            Obliterate._Name = "DRW: Obliterate"

            HeartStrike = New Strikes.Strike(sim)
            HeartStrike._Name = "DRW: Heart Strike"

            DeathStrike = New Strikes.Strike(sim)
            DeathStrike._Name = "DRW: Death Strike"

            RuneStrike = New Strikes.Strike(sim)
            RuneStrike._Name = "DRW: Rune Strike"

            DeathCoil = New Spells.Spell(sim)
            DeathCoil._Name = "DRW: Death Coil"

            IcyTouch = New Spells.Spell(sim)
            IcyTouch._Name = "DRW: Icy Touch"

            logLevel = LogLevelEnum.Detailled
        End Sub
        Public Overrides Sub SoftReset()
            MyBase.SoftReset()
            cd = 0

        End Sub

        Function IsActive(ByVal T As Long) As Boolean
            If ActiveUntil >= T Then
                Return True
            Else
                Return False
            End If

        End Function
        Function Summon(ByVal T As Long) As Boolean
            If sim.RuneForge.AreStarsAligned(T) = False Then
                'DKSIMVB.deathcoil.ApplyDamage(T,false)
                Return False
            End If
            If cd <= T Then
                'If sim.Hysteria.IsAvailable(T) then sim.Hysteria.use(T)
                'If sim.Hysteria.IsActive(T) Then
                '	Hyst = True
                'Else
                '	Hyst = false
                'End If
                SpellHaste = sim.Character.SpellHaste.Value
                Haste = sim.Character.PhysicalHaste.Value
                AP = sim.Character.AP
                _Crit = sim.Character.Crit.Value ' Crit seems based on charater crit
                _SpellCrit = sim.Character.SpellCrit.Value '
                MeleeGlacingChance = 0.25
                MeleeMissChance = 0.08 - sim.Character.Hit.Value
                If MeleeMissChance < 0 Then MeleeMissChance = 0
                MeleeDodgeChance = MeleeMissChance * 0.065 / 0.08
                SpellMissChance = 0.17 - sim.Character.SpellHit.Value
                If SpellMissChance < 0 Then SpellMissChance = 0
                cd = T + (1.5 * 6000)
                sim.RunicPower.Use(60)
                ActiveUntil = T + 1200
                'If sim.Character.Glyph("DRW") Then ActiveUntil = ActiveUntil + 500
                'TODO: Add threat mod

                UseGCD(T)
                NextDRW = T
                sim.FutureEventManager.Add(NextDRW, "DRW")
                sim.CombatLog.write(T & vbTab & "Summon DRW")
                Return True
            End If
            Return False
        End Function
        Function ApplyDamage(ByVal T As Long) As Boolean
            NextDRW = T + (100 * 3.5 / Haste)
            sim.FutureEventManager.Add(NextDRW, "DRW")

            Dim RNG As Double
            Dim retour As Integer
            RNG = RngHit()

            If RNG < (MeleeMissChance + MeleeDodgeChance) Then
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "DRW fail")
                MissCount = MissCount + 1
                Return False
            End If

            If RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) Then
                retour = AvrgNonCrit(T) * 0.7
                total = total + retour
                TotalGlance += retour
                GlancingCount = GlancingCount + 1
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "DRW glancing for " & retour)
            End If

            If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) And RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + crit()) Then
                'CRIT !
                retour = AvrgCrit(T)
                total = total + retour
                TotalCrit += retour
                CritCount = CritCount + 1
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "DRW crit for " & retour)
            End If
            If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + crit()) Then
                'normal hit3
                retour = AvrgNonCrit(T)
                HitCount = HitCount + 1
                total = total + retour
                TotalHit += retour
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T & vbTab & "DRW hit for " & retour)
            End If
            Return True
        End Function
        Function AvrgNonCrit(ByVal T As Long) As Double
            Dim tmp As Double
            tmp = MHBaseDamage()
            tmp = tmp * PhysicalDamageMultiplier(T)


            Return tmp
        End Function
        Function CritCoef() As Double
            CritCoef = 1
        End Function
        Function AvrgCrit(ByVal T As Long) As Double
            AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef())
        End Function
        Sub UseGCD(ByVal T As Long)
            sim.UseGCD(True)
        End Sub
        Function PhysicalDamageMultiplier(ByVal T As Long, Optional ByVal target As Targets.Target = Nothing) As Double
            If target Is Nothing Then target = sim.Targets.MainTarget
            Dim tmp As Double
            tmp = 1
            tmp = tmp * getMitigation()
            tmp = tmp * (1 + 0.03 * sim.Character.Buff.PcDamage)
            tmp = tmp * (1 + 0.04 * target.Debuff.PhysicalVuln)
            If Hyst Then tmp = tmp * (1 + 0.2)
            Return tmp
        End Function


        Function getMitigation(Optional ByVal target As Targets.Target = Nothing) As Double
            If target Is Nothing Then target = sim.Targets.MainTarget
            Dim AttackerLevel As Integer = 80
            Dim tmpArmor As Integer
            Dim ArPDebuffs As Double
            Dim l_sunder As Double = 1.0
            Dim l_ff As Double = 1.0
            Dim _Mitigation As Double


            If target.Debuff.ArmorMajor > 0 Then l_sunder = 1 - 0.12

            ArPDebuffs = (l_sunder * l_ff)
            Dim ArmorConstant As Double = 400 + (85 * 80) + 4.5 * 85 * (80 - 59)

            tmpArmor = sim.Character.BossArmor * ArPDebuffs
            Dim ArPCap As Double = Math.Min((tmpArmor + ArmorConstant) / 3, tmpArmor)
            tmpArmor = tmpArmor - ArPCap * Math.Min(1, 0)
            _Mitigation = ArmorConstant / (ArmorConstant + tmpArmor)
            Return _Mitigation
        End Function

        Function MagicalDamageMultiplier(ByVal T As Long, Optional ByVal target As Targets.Target = Nothing) As Double
            If target Is Nothing Then target = sim.Targets.MainTarget
            Dim tmp As Double
            tmp = 1
            tmp = tmp * (1 + 0.03 * sim.Character.Buff.PcDamage)
            tmp = tmp * (1 + 0.08 * target.Debuff.SpellDamageTaken)
            Return tmp
        End Function
        Function crit() As System.Double
            Return _Crit
        End Function
        Function SpellCrit() As Single
            Return _SpellCrit
        End Function
        Function Hit() As Double
            Dim tmp As Double
            tmp = sim.Character.Hit.Value
            Return tmp
        End Function
        Function SpellHit() As Double
            Dim tmp As Double
            tmp = sim.Character.SpellHit.Value
            Return tmp
        End Function
        Function MHBaseDamage() As Double
            Dim tmp As Double
            tmp = (sim.Character.MHWeaponDPS + (AP / 14)) * 3.5
            Return tmp
        End Function
        Function NormalisedMHDamage() As Double
            Dim tmp As Double
            tmp = sim.Character.MHWeaponSpeed * 3.5
            tmp = tmp + 3.3 * (AP / 14)
            Return tmp
        End Function


        Sub DRWObliterate()
            Dim RNG As Double
            Dim damage As Integer
            RNG = Obliterate.RngHit
            If RNG < (MeleeMissChance + MeleeDodgeChance) Then
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "DRW Obliterate fail")
                Obliterate.MissCount += 1
                Exit Sub
            End If
            damage = NormalisedMHDamage() * 0.8 + 467.2
            damage = damage * PhysicalDamageMultiplier(sim.TimeStamp)
            damage = damage * (1 + 0.125 * sim.Targets.MainTarget.NumDisease)
            damage = damage / 2

            RNG = Obliterate.RngCrit
            If RngCrit < crit() Then
                damage = damage * 2
                Obliterate.CritCount += 1
                Obliterate.TotalCrit += damage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "DRW Obliterate crit for " & damage)
            Else
                Obliterate.HitCount += 1
                Obliterate.TotalHit += damage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "DRW Obliterate hit for " & damage)
            End If
            Obliterate.total = Obliterate.total + damage
        End Sub
        Sub DRWDeathStrike()
            Dim RNG As Double
            Dim damage As Integer
            RNG = DeathStrike.RngHit
            If RNG < (MeleeMissChance + MeleeDodgeChance) Then
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "DRW Death Strike fail")
                DeathStrike.MissCount = DeathStrike.MissCount + 1
                Exit Sub
            End If
            RNG = DeathStrike.RngCrit
            damage = NormalisedMHDamage() * 0.75 + 222.75
            damage = damage * PhysicalDamageMultiplier(sim.TimeStamp)
            'damage = damage /2
            If RNG < crit() Then
                damage = damage * 2
                DeathStrike.CritCount = DeathStrike.CritCount + 1
                DeathStrike.TotalCrit += damage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "DRW Death Strike crit for " & damage)
            Else
                DeathStrike.HitCount = DeathStrike.HitCount + 1
                DeathStrike.TotalHit += damage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "DRW Death Strike hit for " & damage)
            End If
            DeathStrike.total = DeathStrike.total + damage
        End Sub
        Sub DRWHeartStrike()

            Dim RNG As Double
            Dim damage As Integer

            RNG = HeartStrike.RngHit
            If RNG < (MeleeMissChance + MeleeDodgeChance) Then
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "DRW Heart Strike fail")
                HeartStrike.MissCount = HeartStrike.MissCount + 1
                Exit Sub
            End If

            Dim intCount As Integer
            Dim t As Targets.Target
            intCount = 0
            For Each t In sim.Targets.AllTargets
                RNG = HeartStrike.RngCrit
                damage = NormalisedMHDamage() * 0.5 + 368
                damage = damage * PhysicalDamageMultiplier(sim.TimeStamp)
                damage = damage * (1 + 0.1 * t.NumDisease)
                If RNG < crit() Then
                    damage = damage * 2
                    HeartStrike.CritCount = HeartStrike.CritCount + 1
                    HeartStrike.TotalCrit += damage
                    If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "DRW Heart Strike crit for " & damage)
                Else
                    HeartStrike.HitCount = HeartStrike.HitCount + 1
                    HeartStrike.TotalHit += damage
                    If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "DRW Heart Strike hit for " & damage)
                End If

                If t.Equals(sim.Targets.MainTarget) Then
                    HeartStrike.total = HeartStrike.total + damage
                ElseIf intCount = 0 Then
                    damage = damage * 0.5
                    HeartStrike.total = HeartStrike.total + damage
                    intCount += 1
                End If
            Next

        End Sub
        Sub DRWDeathCoil()
            Dim RNG As Double
            Dim damage As Integer
            RNG = DeathCoil.RngHit

            If RNG < SpellMissChance Then
                DeathCoil.MissCount = DeathCoil.MissCount + 1
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T() & vbTab & "DRW Death Coil fail")
                Exit Sub
            End If
            RNG = DeathCoil.RngCrit

            damage = 0.15 * AP + 443
            damage = damage * MagicalDamageMultiplier(sim.TimeStamp)
            damage = damage / 2

            If RNG <= sim.DRW.SpellCrit Then
                damage = damage * 2
                DeathCoil.CritCount = DeathCoil.CritCount + 1
                DeathCoil.TotalCrit += damage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T() & vbTab & "DRW Death Coil crit for " & damage)
            Else
                DeathCoil.HitCount = DeathCoil.HitCount + 1
                DeathCoil.TotalHit += damage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T() & vbTab & "DRW Death Coil hit for " & damage)
            End If
            DeathCoil.total = DeathCoil.total + damage
        End Sub
        Sub DRWPlagueStrike()
            Dim RNG As Double
            Dim damage As Integer

            RNG = PlaqueStrike.RngHit
            If RNG < (MeleeMissChance + MeleeDodgeChance) Then
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "DRW Plague Strike fail")
                PlaqueStrike.MissCount = PlaqueStrike.MissCount + 1
                Exit Sub
            End If
            RNG = PlaqueStrike.RngCrit
            damage = NormalisedMHDamage() * 0.5 + 189
            damage = damage * PhysicalDamageMultiplier(sim.TimeStamp)
            'damage = damage /2
            If RNG < crit() Then
                damage = damage * 2
                PlaqueStrike.TotalCrit += damage
                PlaqueStrike.CritCount = PlaqueStrike.CritCount + 1
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "DRW Plague Strike crit for " & damage)
            Else
                PlaqueStrike.HitCount = PlaqueStrike.HitCount + 1
                PlaqueStrike.TotalHit += damage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "DRW Plague Strike hit for " & damage)
            End If
            PlaqueStrike.total = PlaqueStrike.total + damage
        End Sub

        Sub DRWRuneStrike()
            Dim RNG As Double
            Dim damage As Integer

            RNG = RuneStrike.RngHit
            If RNG < (MeleeMissChance + MeleeDodgeChance) Then
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "DRW Rune Strike fail")
                RuneStrike.MissCount = RuneStrike.MissCount + 1
                Exit Sub
            End If
            RNG = RuneStrike.RngCrit
            damage = NormalisedMHDamage() * 1.5 + (15 * AP / 10)
            damage = damage * PhysicalDamageMultiplier(sim.TimeStamp)
            If RNG < crit() Then
                damage = damage * 2
                RuneStrike.TotalCrit += damage
                RuneStrike.CritCount = RuneStrike.CritCount + 1
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "DRW Plague Strike crit for " & damage)
            Else
                RuneStrike.HitCount = RuneStrike.HitCount + 1
                RuneStrike.TotalHit += damage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(sim.TimeStamp & vbTab & "DRW Plague Strike hit for " & damage)
            End If
            RuneStrike.total = RuneStrike.total + damage
        End Sub

        Sub DRWIcyTouch()

            Dim RNG As Double
            Dim damage As Integer
            RNG = IcyTouch.RngHit

            If RNG < SpellMissChance Then
                IcyTouch.MissCount = IcyTouch.MissCount + 1
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T() & vbTab & "DRW Icy Touch fail")
                Exit Sub
            End If
            RNG = IcyTouch.RngCrit


            damage = 0.1 * AP + 236
            damage = damage * MagicalDamageMultiplier(sim.TimeStamp)
            damage = damage / 2
            If RNG <= sim.DRW.SpellCrit Then
                damage = damage * 2
                IcyTouch.CritCount = IcyTouch.CritCount + 1
                IcyTouch.TotalCrit += damage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T() & vbTab & "DRW Icy Touch crit for " & damage)
            Else
                IcyTouch.HitCount = IcyTouch.HitCount + 1
                IcyTouch.TotalHit += damage
                If sim.CombatLog.LogDetails Then sim.CombatLog.write(T() & vbTab & "DRW Icy Touch hit for " & damage)
            End If
            IcyTouch.total = IcyTouch.total + damage



        End Sub
        Public Sub cleanup()
            total = 0
            HitCount = 0
            MissCount = 0
            CritCount = 0
            TotalHit = 0
            TotalCrit = 0
        End Sub
        Function T() As Long
            Return sim.TimeStamp
        End Function
        Public Overloads Overrides Sub Merge()
            _Name = "Dancing Rune Weapon"
            total += PlaqueStrike.total + Obliterate.total + HeartStrike.total + DeathStrike.total + DeathCoil.total + IcyTouch.total
            TotalHit += PlaqueStrike.TotalHit + Obliterate.TotalHit + HeartStrike.TotalHit + DeathStrike.TotalHit + DeathCoil.TotalHit + IcyTouch.TotalHit
            TotalCrit += PlaqueStrike.TotalCrit + Obliterate.TotalCrit + HeartStrike.TotalCrit + DeathStrike.TotalCrit + DeathCoil.TotalCrit + IcyTouch.TotalCrit

            MissCount += PlaqueStrike.MissCount + Obliterate.MissCount + HeartStrike.MissCount + DeathStrike.MissCount + DeathCoil.MissCount + IcyTouch.MissCount
            HitCount += PlaqueStrike.HitCount + Obliterate.HitCount + HeartStrike.HitCount + DeathStrike.HitCount + DeathCoil.HitCount + IcyTouch.HitCount
            CritCount += PlaqueStrike.CritCount + Obliterate.CritCount + HeartStrike.CritCount + DeathStrike.CritCount + DeathCoil.CritCount + IcyTouch.CritCount

            PlaqueStrike.cleanup()

            Obliterate.cleanup()
            HeartStrike.cleanup()
            DeathStrike.cleanup()
            DeathCoil.cleanup()
            IcyTouch.cleanup()





        End Sub

    End Class
End Namespace