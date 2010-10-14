'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 20/03/2010
' Heure: 12:20
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Namespace Simulator.WowObjects.PetsAndMinions
    Public Class AotD
        Inherits WowObject

        Friend NextWhiteMainHit As Long
        Friend NextClaw As Long
        Friend ActiveUntil As Long
        Friend cd As Long
        Friend GhoulDoubleHaste As Boolean
        Private MeleeMissChance As Single
        Private MeleeDodgeChance As Single
        Private MeleeGlacingChance As Single
        Private SpellMissChance As Single

        Friend MHWeaponDPS As Integer
        Friend MHWeaponSpeed As Double

        Sub New(ByVal MySim As Sim)
            MyBase.New(MySim)
            _Name = "Army of the Dead"
            total = 0
            MissCount = 0
            HitCount = 0
            CritCount = 0
            cd = 0
            ActiveUntil = 0
            NextWhiteMainHit = 0
            NextClaw = 0
            TotalHit = 0
            TotalCrit = 0
            sim = MySim
            MeleeGlacingChance = 0
            sim.DamagingObject.Add(Me)
            ThreadMultiplicator = 0
            HasteSensible = True
            MHWeaponDPS = 0
            MHWeaponSpeed = 2
            isGuardian = True
            logLevel = LogLevelEnum.Detailled
        End Sub

        Sub Summon(ByVal T As Long)
            If cd <= T Then
                MeleeMissChance = Math.Max(0.08 - Hit(), 0)
                MeleeDodgeChance = Math.Max(0.065 - Expertise(), 0)
                SpellMissChance = Math.Max(0.17 - SpellHit(), 0)
                ActiveUntil = T + 40 * 100
                cd = T + (10 * 60 * 100)
                If T <= 1 Then
                Else
                    Sim.CombatLog.write(T & vbTab & "Summon AoTD")
                    UseGCD(False)
                End If
                Sim.FutureEventManager.Add(T, "AotD")
            End If
        End Sub


        Sub PrePull(ByVal T As Long)
            MeleeMissChance = Math.Max(0.08 - Hit(), 0)
            MeleeDodgeChance = Math.Max(0.065 - Expertise(), 0)
            SpellMissChance = Math.Max(0.17 - SpellHit(), 0)
            ActiveUntil = T + 30 * 100
            cd = T + (10 * 60 * 100)
            If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(T & vbTab & "Pre-Pull AoTD")
            Sim.FutureEventManager.Add(T, "AotD")
        End Sub

        Public Overrides Sub SoftReset()
            MyBase.SoftReset()
            NextWhiteMainHit = 0
            NextClaw = 0
            ActiveUntil = 0
            cd = 0
        End Sub


        Sub UseGCD(ByVal Spell As Boolean)
            sim._UseGCD(300)
        End Sub

        Function PhysicalHaste() As Double
            Dim tmp As Double
            tmp = sim.Character.PhysicalHaste.Value
            Return tmp
        End Function



        Function ApplyDamage(ByVal T As Long) As Boolean
            Dim LastDamage As Integer
            Dim WSpeed As Single
            WSpeed = MHWeaponSpeed
            NextWhiteMainHit = T + (WSpeed * 100) / PhysicalHaste()
            Sim.FutureEventManager.Add(NextWhiteMainHit, "AotD")
            Dim RNG As Double
            RNG = RngHit

            If RNG < (MeleeMissChance + MeleeDodgeChance) Then
                MissCount = MissCount + 8
                If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(T & vbTab & "AotD fail")
                Return False
            End If
            'If RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) Then
            '    LastDamage = AvrgNonCrit(T) * 0.7
            '    total = total + LastDamage
            '    totalhit += LastDamage
            '    HitCount = HitCount + 8
            'End If
            If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance) And RNG < (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance()) Then
                'CRIT !
                LastDamage = AvrgCrit(T)
                CritCount = CritCount + 8
                totalcrit += LastDamage
                total = total + LastDamage
                If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(T & vbTab & "AotD crit for " & LastDamage)
            End If
            If RNG >= (MeleeMissChance + MeleeDodgeChance + MeleeGlacingChance + CritChance()) Then
                'normal hit
                HitCount = HitCount + 8
                LastDamage = AvrgNonCrit(T)
                total = total + LastDamage
                totalhit += LastDamage
                If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(T & vbTab & "AotD hit for " & LastDamage)
            End If
            Return True
        End Function
        Function AvrgNonCrit(ByVal T As Long) As Double
            Dim tmp As Double
            tmp = MHBaseDamage()
            tmp = tmp * PhysicalDamageMultiplier(T)
            
            AvrgNonCrit = tmp
        End Function
        Function CritCoef() As Double
            Return 1
        End Function
        Function CritChance() As Double
            CritChance = crit()
        End Function
        Function AvrgCrit(ByVal T As Long) As Double
            AvrgCrit = AvrgNonCrit(T) * (1 + CritCoef())
        End Function
        Function Claw(ByVal T As Long) As Boolean
            Dim RNG As Double
            Dim LastDamage As Integer

            RNG = RngHit
            If RNG < (MeleeMissChance + MeleeDodgeChance) Then
                If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(T & vbTab & "AotD Ghoul's Claw fail")
                MissCount = MissCount + 1
                Return False
            End If
            RNG = RngCrit
            If RNG <= CritChance() Then
                LastDamage = ClawAvrgCrit(T)
                CritCount = CritCount + 1
                total = total + LastDamage
                totalcrit += LastDamage
                If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(T & vbTab & "AotD Ghoul's Claw for " & LastDamage)
            Else
                LastDamage = ClawAvrgNonCrit(T)
                HitCount = HitCount + 1
                total = total + LastDamage
                totalhit += LastDamage
                If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(T & vbTab & "AotD Ghoul's Claw hit for " & LastDamage)
            End If
            NextClaw = T + 450
            Sim.FutureEventManager.Add(NextClaw, "AotD")
            Return True
        End Function
        Function ClawAvrgNonCrit(ByVal T As Long) As Integer
            Dim tmp As Double
            tmp = MHBaseDamage() * 8 / 2
            tmp = tmp * PhysicalDamageMultiplier(T)
            tmp = tmp * 1.5
            Return tmp
        End Function
        Function ClawAvrgCrit(ByVal T As Long) As Integer
            Return AvrgNonCrit(T) * (1 + CritCoef())
        End Function
        Public Sub cleanup()
            Total = 0
            HitCount = 0
            MissCount = 0
            CritCount = 0
            TotalHit = 0
            TotalCrit = 0
        End Sub


        Function BaseAP() As Integer
            Dim tmp As Integer
            tmp = 1167
            Return tmp
        End Function

        Function AP() As Integer
            AP = (Strength() - 331 + BaseAP())
        End Function
        Function Base_Str() As Integer
            Return 0
        End Function
        Function Strength() As Integer
            Dim tmp As Integer
            Dim str As Integer
            tmp = 331
            str = sim.Character.Strength.Value
            tmp += str / 2
            Return tmp
        End Function
        Function crit(Optional ByVal target As Targets.Target = Nothing) As System.Double
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            tmp = 5  'BaseCrit
            tmp = tmp + 5 * sim.Character.Buff.Crit
            crit = tmp / 100
        End Function
        Function SpellCrit(Optional ByVal target As Targets.Target = Nothing) As Single
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            tmp = tmp + 5 * Sim.Character.Buff.Crit
            tmp = tmp + 5 * target.Debuff.SpellCritTaken
            SpellCrit = tmp / 100
        End Function


        Function SpellHaste() As Double
            Return sim.Character.HasteRating.Value()
        End Function
        Function Expertise() As Double
            Dim tmp As Double
            tmp = sim.Character.Hit.Value
            tmp = tmp * 214 / 32.79
            Return tmp
        End Function

        Function Hit() As Double
            Dim tmp As Double
            tmp = sim.Character.Hit.Value
            Return tmp
        End Function

        Function SpellHit() As Double
            'Dim tmp As Double
            Return sim.Character.SpellHit.Value
        End Function

        Function MHBaseDamage() As Double
            Dim tmp As Double
            tmp = (MHWeaponDPS + (AP() / 14)) * MHWeaponSpeed

            Return tmp
        End Function

        Function ArmorPen() As Double
            Dim tmp As Double
            'tmp = character.ArmorPenetrationRating/15.39
            'tmp = tmp *1.25

            Return tmp
        End Function

        Function ArmorMitigation(Optional ByVal target As Targets.Target = Nothing) As Double
            If target Is Nothing Then target = Sim.Targets.MainTarget

            Dim tmp As Double
            tmp = Sim.Character.BossArmor
            tmp = tmp * (1 - 12 * target.Debuff.ArmorMajor / 100)
            tmp = tmp * (1 - ArmorPen() / 100)
            tmp = (tmp / ((467.5 * 83) + tmp - 22167.5))

            Return tmp
        End Function

        Function PhysicalDamageMultiplier(ByVal T As Long, Optional ByVal target As Targets.Target = Nothing) As Double
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            tmp = 1
            tmp = tmp * (1 - ArmorMitigation())
            tmp = tmp * (1 + 0.03 * Sim.Character.Buff.PcDamage)
            tmp = tmp * (1 + 0.04 * target.Debuff.PhysicalVuln)
            If Sim.Character.Orc Then tmp = tmp * 1.05
            Return tmp
        End Function

        Function MagicalDamageMultiplier(ByVal T As Long, Optional ByVal target As Targets.Target = Nothing) As Double
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            tmp = 1
            tmp = tmp * (1 + 0.03 * Sim.Character.Buff.PcDamage)
            tmp = tmp * (1 + 0.08 * target.Debuff.SpellDamageTaken)

            Return tmp
        End Function




    End Class
End Namespace