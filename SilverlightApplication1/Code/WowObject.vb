﻿'
' Created by SharpDevelop.
' User: Fabien
' Date: 01/01/2010
' Time: 18:15
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects
    Public Class WowObject
        Inherits SimObjet


        Friend ResourceType As Simulator.ResourcesEnum
        Friend Resource As Simulator.Resource


        Friend MissCount As Integer
        Friend HitCount As Integer
        Friend CritCount As Integer
        Friend GlancingCount As Integer

        Public total As Long
        Friend TotalHit As Long
        Friend TotalCrit As Long
        Friend TotalGlance As Long

        Friend isGuardian As Boolean = False
        Friend HasteSensible As Boolean

        Friend logLevel As LogLevelEnum
        Friend DiseaseBonus As Double
        Friend DamageSchool As DamageSchoolEnum
        Public ThreadMultiplicator As Double
        Friend uptime As Long

        Protected _RNG1 As Random
        Protected _RNG2 As Random
        Friend RngCrit As Double
        Friend Rng3 As Double


        Friend BaseDamage As Double
        Friend Coeficient As Double
        Friend Multiplicator As Double = 1
        Friend SpecialCritChance As Double



        Friend LastDamage As Integer
        Enum DamageSchoolEnum
            Physical = 0
            Frost = 1
            Shadow = 2
            OtherMagical = 3
        End Enum
        Enum LogLevelEnum
            No = 0
            Basic = 1
            Detailled = 2
        End Enum
        Overridable Function Report() As ReportLine
            If HitCount + CritCount = 0 Then Return Nothing
            Dim R As New ReportLine

            R.Ability = ShortenName(Me.Name)

            R.Damage_done_Total = total
            R.Damage_done_Pc = toDecimal(100 * total / sim.TotalDamage)
            R.Damage_done_Count = toDecimal(HitCount + CritCount + GlancingCount)
            R.Damage_done_Avg = toDecimal(total / (HitCount + CritCount))
            If HitCount <> 0 Then
                R.hit_count = toDecimal(HitCount)
                R.hit_count_Avg = toDecimal(TotalHit / (HitCount))
                R.hit_count_Pc = toDecimal(100 * HitCount / (HitCount + MissCount + CritCount + GlancingCount))
            End If

            If CritCount <> 0 Then
                R.Crit_count = toDecimal(CritCount)
                R.Crit_count_Avg = toDecimal(TotalCrit / (CritCount))
                R.Crit_count_Pc = toDecimal(100 * CritCount / (HitCount + MissCount + CritCount + GlancingCount))
            End If


            R.Miss_Count = toDecimal(MissCount)
            R.Miss_Count_Pc = toDecimal(100 * MissCount / (HitCount + MissCount + CritCount + GlancingCount))
            If GlancingCount <> 0 Then


                R.Glance_Count = toDecimal(GlancingCount)
                R.Glance_Count_Avg = toDecimal(TotalGlance / (GlancingCount))
                R.Glance_Count_Pc = toDecimal(100 * GlancingCount / (HitCount + MissCount + CritCount + GlancingCount))
            End If
            If sim.FrostPresence Then
                R.TPS = toDecimal((100 * total * ThreadMultiplicator * 2.0735) / sim.TimeStamp)
            End If

            R.Uptime = toDecimal(100 * uptime / sim.MaxTime)
            'tmp = Replace(tmp, vbTab & 0, vbTab)
            Return R
        End Function
        Overridable Sub Merge()

        End Sub
        

        Function RngHit() As Double
            TimeWasted.Start()
            If _RNG1 Is Nothing Then
                ' I have made that to not mess up the RNG is a strike miss
                _RNG1 = New Random(ConvertToInt(Me.Name) + RNGSeeder)
                _RNG2 = New Random(ConvertToInt(Me.Name) + RNGSeeder + 1)
            End If
            RngCrit = _RNG2.NextDouble
            Dim d As Double = _RNG1.NextDouble
            TimeWasted.Pause()
            Return d
        End Function

        Public Sub New(ByVal s As Sim)
            MyBase.New(s)
        End Sub

       
    End Class
End Namespace
