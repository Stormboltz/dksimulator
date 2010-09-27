'
' Created by SharpDevelop.
' User: Fabien
' Date: 20/03/2009
' Time: 15:26
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.WowObjects.Procs
    Friend Class RuneForge

        Private RazorIceStack As Integer

        Private CinderglacierProc As Integer
        Private WastedCG As Integer
        Friend CGMultiplier As Double
        Friend MHProc As WeaponProc
        Friend OHProc As WeaponProc

        Friend FallenCrusaderBuff As SpellEffect

        Friend RIProc As WeaponProc
        Friend CGProc As WeaponProc

        Friend OHRuneForge As String
        Friend MHRuneForge As String

        Friend OHBerserkingActiveUntil As Long

        Private Sim As Sim

        Sub New(ByVal S As Sim)
            Sim = S

            FallenCrusaderBuff = New SpellEffect(S, "Fallen Crusader", SpellEffectManager.SpeelEffectEnum.StrengthPercentage, 1.2, 15)

            CinderglacierProc = 0
            WastedCG = 0
            OHBerserkingActiveUntil = 0
            CGMultiplier = 1.2

           

        End Sub

        Sub SoftReset()
        End Sub




        Sub ConfigRuneForgeProc(ByVal Proc As WeaponProc, ByVal RuneForge As String)
            If RuneForge = "" Then Exit Sub
            With Proc
                .DamageType = RuneForge
                Select Case RuneForge
                    Case "3368"
                        .ProcChance *= 2
                        .ProcLenght = 15
                        .Multiplicator = 1.2
                        ._Name = "Fallen Crusader"
                        .Effects.Add(FallenCrusaderBuff)
                    Case "3370"
                        .ProcChance = 1
                        ._Name = "RazorIce"
                        RIProc = Proc

                    Case "3369"
                        .ProcChance *= 1.5
                        CGProc = Proc
                        ._Name = "Cinderglacier"

                    Case "3789"
                        .ProcChance *= 1.2
                        .ProcLenght = 15
                        .ProcValue = 400
                        .Effects.Add(New SpellBuff(Sim, "Berzerker", Simulator.Sim.Stat.AP, 400, 15))
                        ._Name = "Berzerker"

                    Case Else
                        Diagnostics.Debug.WriteLine("Runeforge: " & RuneForge & " not implemented")
                        .ProcChance = 0
                        Exit Sub

                End Select
            End With
        End Sub

        Sub Init()
            Dim s As Sim
            s = Sim
            Try

                If s.Character.Dual Then
                    MHRuneForge = s.XmlCharacter.Element("character").Element("MainHand").Element("enchant").Value
                Else
                    MHRuneForge = s.XmlCharacter.Element("character").Element("TwoHand").Element("enchant").Value
                End If
            Catch ex As Exception
                MHRuneForge = ""
            End Try

            MHProc = New WeaponProc(s)
            With MHProc
                If s.Character.DualW Then ._Name = "MH "
                .InternalCD = 0
                .ProcOn = ProcsManager.ProcOnType.OnMHhit
                .ProcChance = s.Character.MHWeaponSpeed / 60
            End With
            ConfigRuneForgeProc(MHProc, MHRuneForge)
            If MHProc.ProcChance > 0.0 Then MHProc.Equip()

            If s.Character.DualW Then
                OHProc = New WeaponProc(s)
                Try
                    OHRuneForge = s.XmlCharacter.Element("character").Element("OffHand").Element("enchant").Value
                Catch ex As Exception
                    OHRuneForge = ""
                End Try

                With OHProc
                    ._Name = "OH "
                    .InternalCD = 0
                    .ProcOn = ProcsManager.ProcOnType.OnOHhit
                    .ProcChance = s.Character.OHWeaponSpeed / 60
                End With
                ConfigRuneForgeProc(OHProc, OHRuneForge)
                If OHProc.ProcChance > 0.0 Then
                    OHProc.Equip()

                    If MHRuneForge = OHRuneForge Then
                        Dim Proc As New WeaponProc(s)
                        ConfigRuneForgeProc(Proc, MHRuneForge)
                        Proc._Name &= " (Combined)"
                        Proc.Equip()
                    End If
                End If
            Else
                OHRuneForge = ""



            End If
            If RIProc IsNot Nothing Then
                With RIProc
                    .ProcLenght = 20
                    .ProcValue = Sim.Character.MHWeaponSpeed * Sim.Character.MHWeaponDPS * 0.02
                    .MaxStack = 5
                    .ProcValueStack = 2
                End With
            End If

            If CGProc IsNot Nothing Then
                With CGProc
                    .ProcLenght = 30
                    .ProcValue = 20
                End With
            End If


        End Sub


        Sub ProcCinderglacier(ByVal Proc As WeaponProc, ByVal T As Long)
            'Proc.ApplyMe(T)

            If Proc IsNot CGProc Then CGProc.ApplyFade(T)
            CGProc.CurrentStack = 2
        End Sub


        Function CheckFallenCrusader() As Boolean
            If FallenCrusaderBuff Is Nothing Then Return False
            If FallenCrusaderBuff.Currentstack > 0 Then Return True
            Return False
        End Function

        Function HasFallenCrusader() As Boolean
            If FallenCrusaderBuff Is Nothing Then Return False
            If FallenCrusaderBuff.Currentstack > 0 Then Return True
            Return False
        End Function


        Function CheckCinderglacier(ByVal consume As Boolean) As Integer
            If CGProc Is Nothing Then Return 0
            Dim rv As Integer
            rv = CGProc.CurrentStack
            If consume Then CGProc.Use()
            Return rv
        End Function

        Function AreStarsAligned(ByVal T As Long) As Boolean
            If Sim.WaitForFallenCrusader = False Then Return True
            If CheckFallenCrusader() Then Return True
            If HasFallenCrusader() Then Return False
            Return True
            '		If sim.MainStat.AP >=  sim.MainStat.GetMaxAP * 0.7 Then
            '			Return True
            '		Else
            '			return false
            '		End If
        End Function

        Function RazorIceMultiplier(ByVal T As Long) As Double
            If RIProc Is Nothing Then Return 1.0
            Return 1.0 + 0.02 * RIProc.Stack
        End Function

        Sub ProcRazorIce(ByVal Proc As WeaponProc, ByVal T As Long)
            Dim tmp As Double
            tmp = RIProc.procvalue
            'Hastebonus should only be applied to procs from hasteable attacks
            'If sim.EPStat = "EP HasteEstimated" Then
            '	tmp *= sim.MainStat.EstimatedHasteBonus
            'End If
            RIProc.ApplyFade(T)

            If RIProc.Stack < RIProc.MaxStack Then RIProc.Stack += 1

            With Proc
                If Proc IsNot RIProc Then .HitCount += 1
                If Sim.CombatLog.LogDetails Then Sim.CombatLog.write(Sim.TimeStamp & vbTab & .ToString & " proc")
                .total += tmp
            End With
        End Sub

    End Class
End Namespace