'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 23:29
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Class Obliterate
	Inherits Strikes.Strike
	
	
	Sub New(S As sim)
        MyBase.New(S)
        BaseDamage = 934.4
        If sim.Sigils.Awareness Then BaseDamage = BaseDamage + 336
        Coeficient = 1.6
        Multiplicator = (1 + sim.Character.Talents.Talent("Annihilation").Value * 10 / 100)
        If sim.Character.Glyph.Obliterate Then Multiplicator = Multiplicator * 1.2
        If sim.MainStat.T102PDPS <> 0 Then Multiplicator = Multiplicator * 1.1
        logLevel = LogLevelEnum.Basic
        If sim.MainStat.T72PDPS Then
            SpecialCritChance = 1.05
        Else
            SpecialCritChance = 1
        End If

        If sim.MainStat.T84PDPS = 1 Then
            DiseaseBonus = (0.125 * 1.2)
        Else
            DiseaseBonus = (0.125)
        End If

    End Sub
	
    Public Overrides Function ApplyDamage(ByVal T As Long) As Boolean
        UseGCD(T)
        If MyBase.ApplyDamage(T) = False Then
            sim.Runes.UseFU(T, False, True)
            Return False
        End If

        If OffHand = False Then

            sim.RunicPower.add(25 + 5 * sim.Character.Talents.Talent("ChillOfTheGrave").Value + 5 * sim.MainStat.T74PDPS)

            If sim.proc.DRM.TryMe(T) Then
                sim.Runes.UseFU(T, True)
            Else
                sim.Runes.UseFU(T, False)
            End If
            sim.proc.tryProcs(Procs.ProcOnType.OnFU)
            sim.proc.Rime.TryMe(T)
            If sim.DRW.IsActive(T) Then
                sim.DRW.DRWObliterate()
            End If
        End If
        Return True
    End Function

    Public Overrides Function AvrgNonCrit(ByVal T As Long, ByVal target As Targets.Target) As Double
        Dim tmp As Double
        tmp = MyBase.AvrgNonCrit(T, target)
        If sim.ExecuteRange Then tmp = tmp * (1 + 0.06 * sim.Character.Talents.Talent("MercilessCombat").Value)
        Return tmp
    End Function

End class
