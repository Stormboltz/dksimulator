'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 14:24
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class FrostStrike
	Inherits Strikes.Strike

	Sub New(S As sim)
        MyBase.New(S)
        BaseDamage = 275
        If sim.Sigils.VengefulHeart Then BaseDamage = BaseDamage + 113
        Coeficient = 1.1
        Multiplicator = (1 + sim.Character.Talents.Talent("BloodoftheNorth").Value * 5 / 100)
        If S.Character.Talents.GetNumOfThisSchool(Talents.Schools.Frost) > 20 Then
            Multiplicator = Multiplicator * 1.2 'Frozen Heart
        End If

        SpecialCritChance = 8 / 100 * sim.MainStat.T82PDPS

	End Sub
	
	public Overrides Function isAvailable(T As long) As Boolean
		If sim.character.glyph.FrostStrike Then
			isAvailable = Sim.RunicPower.CheckRS(32)
		Else
			isAvailable = Sim.RunicPower.CheckRS(40)
		end if
	End Function
    Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
        Dim ret As Boolean = MyBase.ApplyDamage(T)

        If OffHand = False Then
            UseGCD(T)
            If sim.proc.ThreatOfThassarian.TryMe(T) Then sim.OHFrostStrike.ApplyDamage(T)
        End If

        If ret Then
            If sim.Character.Glyph.FrostStrike Then
                sim.RunicPower.Use(32)
            Else
                sim.RunicPower.Use(40)
            End If
        Else
            Return False
        End If


        If OffHand = False Then
            sim.proc.KillingMachine.Use()
            sim.proc.TryOnonRPDumpProcs()
        End If
        Return True
    End Function
    Public Shadows Function AvrgNonCrit(ByVal T As Long, Optional ByVal target As Targets.Target = Nothing) As Double
        If target Is Nothing Then target = sim.Targets.MainTarget
        Dim tmp As Double
        If offhand = False Then
            tmp = sim.MainStat.NormalisedMHDamage * Coeficient
        Else
            tmp = sim.MainStat.NormalisedOHDamage * Coeficient

        End If
        tmp = tmp + BaseDamage
        tmp = tmp * Multiplicator
        If sim.ExecuteRange Then tmp = tmp * (1 + 0.06 * sim.Character.Talents.Talent("MercilessCombat").Value)
        tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)
        tmp *= sim.RuneForge.RazorIceMultiplier(T)
        If sim.RuneForge.CheckCinderglacier(OffHand) > 0 Then tmp *= 1.2
        If offhand Then
            tmp = tmp * OffDamageBonus()
        End If
        Return tmp
    End Function
    Public Overrides Function CritChance() As Double
        If sim.proc.KillingMachine.IsActive() = True Then
            Return 1
        Else
            Return MyBase.CritChance
        End If
    End Function
    Public Overrides Sub Merge()
        If sim.MainStat.DualW = False Then Exit Sub
        total += sim.OHFrostStrike.total
        TotalHit += sim.OHFrostStrike.TotalHit
        TotalCrit += sim.OHFrostStrike.TotalCrit

        MissCount = (MissCount + sim.OHFrostStrike.MissCount) / 2
        HitCount = (HitCount + sim.OHFrostStrike.HitCount) / 2
        CritCount = (CritCount + sim.OHFrostStrike.CritCount) / 2

        sim.OHFrostStrike.total = 0
        sim.OHFrostStrike.TotalHit = 0
        sim.OHFrostStrike.TotalCrit = 0
    End Sub
End Class
