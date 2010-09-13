'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 14:24
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Strikes
    Friend Class FrostStrike
        Inherits Strike

        Sub New(ByVal S As Sim)
            MyBase.New(S)
            BaseDamage = 275
            If sim.Sigils.VengefulHeart Then BaseDamage = BaseDamage + 113
            Coeficient = 1.1
            Multiplicator = (1 + sim.Character.Talents.Talent("BloodoftheNorth").Value * 5 / 100)
            If S.Character.Talents.GetNumOfThisSchool(Character.Talents.Schools.Frost) > 20 Then
                Multiplicator = Multiplicator * 1.2 'Frozen Heart
            End If

            SpecialCritChance = 8 / 100 * sim.Character.T82PDPS
            DamageSchool = DamageSchoolEnum.Frost
            logLevel = LogLevelEnum.Basic
        End Sub

        Public Overrides Function isAvailable(ByVal T As Long) As Boolean
            If sim.Character.Glyph.FrostStrike Then
                isAvailable = sim.RunicPower.CheckRS(32)
            Else
                isAvailable = sim.RunicPower.CheckRS(40)
            End If
        End Function
        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            Dim ret As Boolean = MyBase.ApplyDamage(T)
            sim.proc.KillingMachine.Use()
            If ret = False Then
                UseGCD(T)
                If sim.Character.Glyph.FrostStrike Then
                    sim.RunicPower.Use(16)
                Else
                    sim.RunicPower.Use(20)
                End If
                Return False
            End If
            If OffHand = False Then
                UseGCD(T)
                sim.RunicPower.add(25)
                sim.RunicPower.add(5 * sim.Character.T74PDPS)
                If sim.Character.Glyph.FrostStrike Then
                    sim.RunicPower.Use(32)
                Else
                    sim.RunicPower.Use(40)
                End If
                sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.onRPDump)
            End If
            Return True

        End Function
        Public Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            If target Is Nothing Then target = sim.Targets.MainTarget
            Dim tmp As Double = MyBase.AvrgNonCrit(T, target)

            If sim.ExecuteRange Then tmp = tmp * (1 + 0.06 * sim.Character.Talents.Talent("MercilessCombat").Value)
            tmp *= sim.RuneForge.RazorIceMultiplier(T)
            If sim.RuneForge.CheckCinderglacier(OffHand) > 0 Then tmp *= 1.2
            Return tmp
        End Function
        Public Overrides Function CritChance() As Double
            If sim.proc.KillingMachine.IsActive() = True Then
                Return 1
            Else
                Return MyBase.CritChance
            End If
        End Function

    End Class
End Namespace
