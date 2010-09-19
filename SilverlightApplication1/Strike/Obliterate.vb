'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 23:29
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Strikes
    Friend Class Obliterate
        Inherits Strike


        Sub New(ByVal S As Sim)
            MyBase.New(S)
            BaseDamage = 934.4
            If Sim.Sigils.Awareness Then BaseDamage = BaseDamage + 336
            Coeficient = 1.6
            Multiplicator = (1 + sim.Character.Talents.Talent("Annihilation").Value * 15 / 100)
            If Sim.Character.Glyph.Obliterate Then Multiplicator = Multiplicator * 1.2
            If Sim.Character.T102PDPS <> 0 Then Multiplicator = Multiplicator * 1.1
            logLevel = LogLevelEnum.Basic
            If Sim.Character.T72PDPS Then
                SpecialCritChance = 1.05
            Else
                SpecialCritChance = 1
            End If

            If Sim.Character.T84PDPS = 1 Then
                DiseaseBonus = (0.125 * 1.2)
            Else
                DiseaseBonus = (0.125)
            End If

        End Sub

        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            UseGCD(T)
            If MyBase.ApplyDamage(T) = False Then
                Sim.Runes.UseFU(T, False, True)
                Return False
            End If

            If OffHand = False Then
                sim.proc.KillingMachine.Use()
                sim.RunicPower.add(25 + 5 * sim.Character.Talents.Talent("ChillOfTheGrave").Value + 5 * sim.Character.T74PDPS)

                If Sim.proc.DRM.TryMe(T) Then
                    Sim.Runes.UseFU(T, True)
                Else
                    Sim.Runes.UseFU(T, False)
                End If
                sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnFU)
                Sim.proc.Rime.TryMe(T)
                If Sim.DRW.IsActive(T) Then
                    Sim.DRW.DRWObliterate()
                End If
            End If
            Return True
        End Function

        Public Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
            Dim tmp As Double
            tmp = MyBase.AvrgNonCrit(T, target)
            If Sim.ExecuteRange Then tmp = tmp * (1 + 0.06 * Sim.Character.Talents.Talent("MercilessCombat").Value)
            Return tmp
        End Function

        Public Overrides Function CritChance() As Double
            If sim.proc.KillingMachine.IsActive Then Return 1
            Return MyBase.CritChance()
        End Function
    End Class
End Namespace