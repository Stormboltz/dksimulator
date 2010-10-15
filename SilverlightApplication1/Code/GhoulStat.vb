'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 18:49
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.PetsAndMinions
    Friend Class GhoulStat
        Friend BaseStrength As Integer
        Friend BaseAP As Integer
        Friend Agility As Integer
        Friend StrengthMultiplier As Double
        Friend MHWeaponDPS As Double
        Friend MHWeaponSpeed As Double
        Friend APtoDPS As Double
        Private character As Character.MainStat
        Private Sim As Sim
        Sub New(ByVal S As Sim)
            Sim = S
            character = Sim.Character
            MHWeaponDPS = 50.0
            MHWeaponSpeed = 2
            BaseAP = -20
            Agility = 856
            BaseStrength = 331
            APtoDPS = 0.89 / 14  'from observation

            StrengthMultiplier = 1
            If Sim.Character.Glyph("RaiseDead") Then StrengthMultiplier = StrengthMultiplier * 1.4
        End Sub

        Function Strength() As Integer
            Return StrengthMultiplier * character.Strength.Value + BaseStrength
        End Function

        Function AP() As Double 'non-permaghoul calculation
            Return BaseAP + Strength() * 2 + Agility
        End Function

        Function crit(Optional ByVal target As Targets.Target = Nothing) As System.Double
            If target Is Nothing Then target = Sim.Targets.MainTarget

            Return Sim.Character.Crit.Value
            'Dim tmp As Double
            'tmp = Sim.Character.Crit.Value
            'tmp = tmp + 5 * Sim.Character.Buff.Crit
            'crit = tmp / 100
        End Function
        Function SpellCrit(Optional ByVal target As Targets.Target = Nothing) As Single
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            tmp = tmp + 5 * Sim.Character.Buff.Crit
            tmp = tmp + 5 * target.Debuff.SpellCritTaken
            SpellCrit = tmp / 100
        End Function

        Function Expertise() As Double
            Dim tmp As Double
            tmp = Sim.Character.Hit.Value
            tmp = tmp * 214 / 32.79

            Return tmp
        End Function

        Function Hit() As Double
            Dim tmp As Double
            tmp = Sim.Character.Hit.Value
            Return tmp
        End Function

        Function SpellHit() As Double
            'Dim tmp As Double
            Return Sim.Character.SpellHit.Value
        End Function

        Function ArmorPen() As Double
            Dim tmp As Double
            'tmp = character.ArmorPenetrationRating/15.39
            'tmp = tmp *1.25

            Return tmp
        End Function

        Function MHBaseDamage(ByVal AP As Double) As Double
            Dim tmp As Double
            tmp = (MHWeaponDPS + (AP * APtoDPS)) * MHWeaponSpeed
            Return tmp
        End Function

        Function SwingTime(ByVal Haste As Double) As Double
            Return MHWeaponSpeed / Haste
        End Function

        Function ClawTime(ByVal Haste As Double) As Double
            Return 4.0 / Haste
        End Function


        Function ArmorMitigation(ByVal target As Targets.Target) As Double
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
            tmp = tmp * (1 - ArmorMitigation(target))
            tmp = tmp * (1 + 0.03 * Sim.Character.Buff.PcDamage)
            tmp = tmp * (1 + 0.04 * target.Debuff.PhysicalVuln)
            tmp = tmp * (1 + Sim.FrostPresence / 100)
            If Sim.Ghoul.ShadowInfusion.IsActive Then
                tmp = tmp * (1 + 0.1 * (Sim.Ghoul.ShadowInfusion.Stack))
            End If
            If Sim.DarkTransformation.DarkTransformationBuff.IsActive Then
                tmp = tmp * (2)
            End If

            If Sim.Character.Orc Then tmp = tmp * 1.05
            Return tmp
        End Function

        Function MagicalDamageMultiplier(ByVal T As Long, Optional ByVal target As Targets.Target = Nothing) As Double
            If target Is Nothing Then target = Sim.Targets.MainTarget
            Dim tmp As Double
            tmp = 1
            tmp = tmp * (1 + 0.03 * Sim.Character.Buff.PcDamage)
            tmp = tmp * (1 + 0.08 * target.Debuff.SpellDamageTaken)
            If Sim.Character.Orc Then tmp = tmp * 1.05
            Return tmp
        End Function
    End Class
End Namespace
