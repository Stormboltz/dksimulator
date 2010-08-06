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
		MyBase.New(s)
	End Sub
	
	public Overrides Function isAvailable(T As long) As Boolean
		If sim.character.glyph.FrostStrike Then
			isAvailable = Sim.RunicPower.CheckRS(32)
		Else
			isAvailable = Sim.RunicPower.CheckRS(40)
		end if
	End Function
	
	public Overrides Function ApplyDamage(T As Long) As Boolean
		Dim RNG As Double
		If OffHand = False Then
			UseGCD(T)
			If sim.character.glyph.FrostStrike Then
				Sim.RunicPower.Use(32)
			Else
				Sim.RunicPower.Use(40)
			End If
			If sim.proc.ThreatOfThassarian.TryMe(T) Then sim.OHFrostStrike.ApplyDamage(T)
			If DoMyStrikeHit = false Then
				sim.combatlog.write(T  & vbtab & "FS fail")
				sim.proc.KillingMachine.Use
				If sim.character.glyph.FrostStrike Then
					Sim.RunicPower.add(29)
				Else
					Sim.RunicPower.add(36)
				End If
				MissCount = MissCount + 1
				Return False
			End If
		Else
            If DoMyToTHit() = False Then Return False
		End If
		Dim ccT As Double

        ccT = CritChance
        RNG = RngCrit
        If RNG < ccT Then
            LastDamage = AvrgCrit(T)
            sim.combatlog.write(T & vbtab & "FS crit for " & LastDamage)
            CritCount = CritCount + 1
            sim.proc.tryOnCrit()
            totalcrit += LastDamage
        Else
            LastDamage = AvrgNonCrit(T)
            HitCount = HitCount + 1
            totalhit += LastDamage
            sim.combatlog.write(T & vbtab & "FS hit for " & LastDamage)
        End If
        total = total + LastDamage
		If offhand Then
            sim.proc.TryOnOHHitProc()
		Else
            sim.proc.TryOnMHHitProc()
            sim.proc.KillingMachine.Use()
            sim.proc.TryOnonRPDumpProcs()
		End If
		Return True
	End Function
    Public Shadows Function AvrgNonCrit(ByVal T As Long, Optional ByVal target As Targets.Target = Nothing) As Double
        If target Is Nothing Then target = sim.Targets.MainTarget
        Dim tmp As Double
        If offhand = False Then
            tmp = sim.MainStat.NormalisedMHDamage * 1.1
        Else
            tmp = sim.MainStat.NormalisedOHDamage * 1.1

        End If
        tmp = tmp + 275
        If sim.sigils.VengefulHeart Then tmp = tmp + 113
        tmp = tmp * (1 + sim.Character.Talents.Talent("BloodoftheNorth").Value * 5 / 100)

        If sim.ExecuteRange Then tmp = tmp * (1 + 0.06 * sim.Character.Talents.Talent("MercilessCombat").Value)
        tmp = tmp * sim.MainStat.StandardMagicalDamageMultiplier(T)

        tmp *= sim.RuneForge.RazorIceMultiplier(T)
        If sim.RuneForge.CheckCinderglacier(offhand) > 0 Then tmp *= 1.2
        If offhand Then
            tmp = tmp * 0.5
            tmp = tmp * (1 + sim.Character.Talents.Talent("NervesofColdSteel").Value * 8.3333 / 100)
        End If
        Return tmp
    End Function
    
	public Overrides Function CritChance() As Double
		CritChance = sim.MainStat.Crit + 8/100 * sim.MainStat.T82PDPS
		if sim.proc.KillingMachine.IsActive()  = true then return 1
	End Function
	
	
	Public Overrides Sub Merge()
		If sim.MainStat.DualW = false Then exit sub
		Total += sim.OHFrostStrike.Total
		TotalHit += sim.OHFrostStrike.TotalHit
		TotalCrit += sim.OHFrostStrike.TotalCrit

		MissCount = (MissCount + sim.OHFrostStrike.MissCount)/2
		HitCount = (HitCount + sim.OHFrostStrike.HitCount)/2
		CritCount = (CritCount + sim.OHFrostStrike.CritCount)/2
		
		sim.OHFrostStrike.Total = 0
		sim.OHFrostStrike.TotalHit = 0
		sim.OHFrostStrike.TotalCrit = 0
	End Sub
End Class
