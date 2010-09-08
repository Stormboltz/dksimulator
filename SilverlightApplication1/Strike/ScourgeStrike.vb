Namespace Simulator.WowObjects.Strikes


    Friend Class ScourgeStrike
        Inherits Strike

        Private tmpPhysical As Integer
        Private tmpMagical As Integer
        Private MagicHit As Long
        Private MagicCrit As Long
        Friend MagicTotal As Long

        Friend SSmagical As ScourgeStrikeMagical


        Sub New(ByVal S As Sim)
            MyBase.New(S)
            MagicCrit = 0
            MagicHit = 0
            MagicTotal = 0
            logLevel = LogLevelEnum.Basic

            BaseDamage = 561
            If sim.Sigils.Awareness Then BaseDamage = BaseDamage + 189
            If sim.Sigils.ArthriticBinding Then BaseDamage = BaseDamage + 91.35

            Coeficient = 1
            Multiplicator = 1
            Multiplicator = Multiplicator * (1 + 10 * sim.Character.Talents.Talent("CorruptingStrikes").Value / 100)
            If sim.Character.T102PDPS <> 0 Then Multiplicator = Multiplicator * 1.1


            SpecialCritChance = sim.Character.Talents.Talent("ViciousStrikes").Value * 3 / 100 + sim.Character.T72PDPS * 5 / 100

            _CritCoef = 1 + sim.Character.Talents.Talent("ViciousStrikes").Value * 15 / 100
            _CritCoef = _CritCoef * (1 + 0.06 * sim.Character.CSD)

            SSmagical = New ScourgeStrikeMagical(S)

        End Sub

        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            UseGCD(T)
            If MyBase.ApplyDamage(T) = False Then
                sim.Runes.UseUnholy(T, False, True)
                Return False
            End If

            If OffHand = False Then

                sim.ScourgeStrikeMagical.ApplyDamage(LastDamage, T, False)
                UseGCD(T)
                sim.RunicPower.add(15 + sim.Character.Talents.Talent("Dirge").Value * 5 + 5 * sim.Character.T74PDPS)
                sim.Runes.UseUnholy(T, False)

                If sim.Character.Glyph.ScourgeStrike Then
                    If sim.Targets.MainTarget.BloodPlague.ScourgeStrikeGlyphCounter < 3 Then
                        sim.Targets.MainTarget.BloodPlague.IncreaseDuration(300)
                        sim.Targets.MainTarget.BloodPlague.ScourgeStrikeGlyphCounter += 1
                    End If
                    If sim.Targets.MainTarget.FrostFever.ScourgeStrikeGlyphCounter < 3 Then
                        sim.Targets.MainTarget.FrostFever.IncreaseDuration(300)
                        sim.Targets.MainTarget.FrostFever.ScourgeStrikeGlyphCounter += 1
                    End If
                End If
                sim.proc.tryProcs(Procs.ProcsManager.ProcOnType.OnFU)

            End If
            Return True
        End Function


        Public Overrides Sub Merge()
            total += sim.ScourgeStrikeMagical.total
            TotalHit += sim.ScourgeStrikeMagical.TotalHit
            TotalCrit += sim.ScourgeStrikeMagical.TotalCrit

            MissCount = (MissCount + sim.ScourgeStrikeMagical.MissCount) / 2
            HitCount = (HitCount + sim.ScourgeStrikeMagical.HitCount) / 2
            CritCount = (CritCount + sim.ScourgeStrikeMagical.CritCount) / 2

            sim.ScourgeStrikeMagical.total = 0
            sim.ScourgeStrikeMagical.TotalHit = 0
            sim.ScourgeStrikeMagical.TotalCrit = 0
        End Sub



        Public Class ScourgeStrikeMagical
            Inherits Spells.Spell

            Friend tmpPhysical As Integer
            Sub New(ByVal S As Sim)
                MyBase.New(S)
                logLevel = LogLevelEnum.Basic
            End Sub

            Shadows Function ApplyDamage(ByVal PhysicalDamage As Integer, ByVal T As Long, ByVal IsCrit As Boolean) As Boolean
                Dim tmp As Integer
                tmpPhysical = PhysicalDamage
                tmp = AvrgNonCrit(T)
                HitCount += 1
                TotalHit += tmp
                total += tmp
                Return True
            End Function


            Public Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
                Dim tmpMagical As Integer
                tmpMagical = tmpPhysical * (0.12 * target.NumDesease)
                If sim.Character.T84PDPS = 1 Then tmpMagical = tmpMagical * 1.2
                tmpMagical *= sim.Character.StandardMagicalDamageMultiplier(T, target)
                If sim.RuneForge.CheckCinderglacier(True) > 0 Then tmpMagical *= 1.2
                Return tmpMagical
            End Function
        End Class
    End Class
End Namespace
