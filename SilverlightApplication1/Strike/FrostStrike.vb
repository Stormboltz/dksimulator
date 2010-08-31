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
        DamageSchool = DamageSchoolEnum.Frost
        logLevel = LogLevelEnum.Basic
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
        sim.proc.KillingMachine.Use()
        If ret = False Then
            Return False
        End If
        If OffHand = False Then
            UseGCD(T)
            sim.RunicPower.add(25 + 5 * sim.Character.Talents.Talent("Dirge").Value)
            sim.RunicPower.add(5 * sim.MainStat.T74PDPS)
            If sim.Character.Glyph.FrostStrike Then
                sim.RunicPower.Use(32)
            Else
                sim.RunicPower.Use(40)
            End If
            sim.proc.tryProcs(Procs.ProcOnType.onRPDump)
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
