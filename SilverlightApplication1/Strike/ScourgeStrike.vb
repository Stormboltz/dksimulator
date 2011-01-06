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


            BaseDamage = 624



            Coeficient = 1

            Coeficient += sim.Character.Talents.Talent("RageOfRivendare").Value * 15 / 100

            If sim.Character.T102PDPS <> 0 Then Coeficient += 0.1
            _CritCoef = (1 + 0.06 * sim.Character.CSD)
            Dim rp As Integer = 10 + 5 * sim.Character.T74PDPS
            SSmagical = New ScourgeStrikeMagical(S)
            SSmagical._Name = "Scourge Strike Magical"
            Resource = New Resource(sim, Resource.ResourcesEnum.UnholyRune, False, rp)

        End Sub

        Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
            If Not OffHand Then UseGCD()
            If MyBase.ApplyDamage(T) = False Then
                'UseAlf()
                Return False
            End If

            If OffHand = False Then

                sim.ScourgeStrikeMagical.ApplyDamage(LastDamage, T, False)

                Use()
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
                If sim.Character.Glyph("ScourgeStrike") Then Multiplicator += 0.3
                Multiplicator += sim.Character.Talents.Talent("RageOfRivendare").Value * 15 / 100
                logLevel = LogLevelEnum.Basic
                Resource = New Resource(sim, Resource.ResourcesEnum.None)
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
                tmpMagical = tmpPhysical * (0.12 * target.NumDisease)
                If sim.Character.T84PDPS = 1 Then tmpMagical = tmpMagical * 1.2
                tmpMagical *= sim.Character.StandardMagicalDamageMultiplier(T, target)
                If sim.RuneForge.CheckCinderglacier(True) > 0 Then tmpMagical *= 1.2
                tmpMagical *= Multiplicator
                Return tmpMagical
            End Function
        End Class
    End Class
End Namespace
