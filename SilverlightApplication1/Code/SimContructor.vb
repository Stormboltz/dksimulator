Imports System.Xml.Linq
Imports System.Threading

'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 15/09/2009
' Heure: 16:15
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Module SimConstructor


    Friend PetFriendly As Boolean

    ' Friend ReportPath As String
    Friend EpStat As String
    Friend Rotate As Boolean
    Friend DPSs As New Collection
    Friend sThreadCollection As New Collection
    Friend EPBase As Integer
    'Friend ThreadCollection As New Collection
    Friend ThreadCollection As New Collections.Generic.List(Of Thread)

    Friend simCollection As New collection
    Public _MainFrm As MainForm
    Sub New()

    End Sub

    Sub Start(ByVal SimTime As Double, ByVal MainFrm As MainForm, Optional ByVal StartNow As Boolean = False)
        Dim sim As Sim
        Dim newthread As System.Threading.Thread
        sim = New Sim
        _MainFrm = MainFrm


        If EpStat <> "" Then
            sim.Prepare(SimTime, MainFrm, EpStat, EPBase)
        Else
            sim.Prepare(SimTime, MainFrm)
        End If
        newthread = New System.Threading.Thread(AddressOf sim.Start)
        'newthread.Priority= Threading.ThreadPriority.BelowNormal
        If StartNow Then
            simCollection.Clear()
            newthread.Start()
        End If
        ThreadCollection.Add(newthread)
        simCollection.Add(sim)
    End Sub

    Function GetFastDPS(ByVal MainFrm As MainForm) As Integer
        Dim i As Integer
        DPSs.Clear()
        ThreadCollection.Clear()
        simCollection.Clear()
        Start(10, MainFrm, True)
        Jointhread()

        Try

            i = DPSs.Item(1)



        Catch e As Exception
            Diagnostics.Debug.WriteLine(e.ToString)
        End Try
        Return i
    End Function

    Sub GetFastEPValue(ByVal MainFrm As MainForm)
        DPSs.Clear()
        ThreadCollection.Clear()
        simCollection.Clear()
        EPBase = 20
        _MainFrm = MainFrm
        Dim EPVal As EPValues = MainFrm.EPVal
        Dim SimTime As Double = 10
        EpStat = "EP DryRun"
        SimConstructor.Start(SimTime, MainFrm)
        EpStat = "EP AttackPower"
        SimConstructor.Start(SimTime, MainFrm)
        EpStat = "EP Strength"
        SimConstructor.Start(SimTime, MainFrm)
        EpStat = "EP Agility"
        SimConstructor.Start(SimTime, MainFrm)
        EpStat = "EP CritRating"
        SimConstructor.Start(SimTime, MainFrm)
        EpStat = "EP HasteEstimated"
        SimConstructor.Start(SimTime, MainFrm)
        EpStat = "EP ArmorPenetrationRating"
        SimConstructor.Start(SimTime, MainFrm)
        EpStat = "EP RelativeExpertiseRating"
        SimConstructor.Start(SimTime, MainFrm)
        EpStat = "EP RelativeHitRating"
        SimConstructor.Start(SimTime, MainFrm)
        EpStat = "EP WeaponDPS"
        SimConstructor.Start(SimTime, MainFrm)
        EpStat = "EP WeaponSpeed"
        SimConstructor.Start(SimTime, MainFrm)

        Jointhread()

        Dim BaseDPS As Double
        Dim APDPS As Double
        Dim tmp1 As Double
        Dim tmp2 As Double
        Dim DPS As Double

        EpStat = "EP DryRun"
        BaseDPS = DPSs(EpStat)
        EpStat = "EP AttackPower"
        APDPS = DPSs(EpStat)
        tmp1 = (APDPS - BaseDPS) / (2 * EPBase)

        EpStat = "EP Strength"
        DPS = DPSs(EpStat)
        tmp2 = (DPS - BaseDPS) / EPBase
        EPVal.Str = Math.Max(0, toDDecimal(tmp2 / tmp1))

        EpStat = "EP Agility"
        DPS = DPSs(EpStat)
        tmp2 = (DPS - BaseDPS) / EPBase
        EPVal.Agility = Math.Max(0, toDDecimal(tmp2 / tmp1))

        EpStat = "EP CritRating"
        DPS = DPSs(EpStat)
        tmp2 = (DPS - BaseDPS) / EPBase
        EPVal.Crit = Math.Max(0, toDDecimal(tmp2 / tmp1))

        EpStat = "EP HasteEstimated"
        DPS = DPSs(EpStat)
        tmp2 = (DPS - BaseDPS) / EPBase
        EPVal.Haste = Math.Max(0, toDDecimal(tmp2 / tmp1))

        EpStat = "EP ArmorPenetrationRating"
        DPS = DPSs(EpStat)
        tmp2 = (DPS - BaseDPS) / EPBase
        EPVal.ArP = Math.Max(0, toDDecimal(tmp2 / tmp1))


        EpStat = "EP RelativeExpertiseRating"
        DPS = DPSs(EpStat)
        tmp2 = (DPS - BaseDPS) / EPBase
        EPVal.Exp = Math.Max(0, toDDecimal(tmp2 / tmp1))


        EpStat = "EP RelativeHitRating"
        DPS = DPSs(EpStat)
        tmp2 = (DPS - BaseDPS) / EPBase
        EPVal.Hit = Math.Max(0, toDDecimal(tmp2 / tmp1))

        EpStat = "EP WeaponDPS"
        DPS = DPSs(EpStat)
        tmp2 = (DPS - BaseDPS) / 10
        EPVal.MHDPS = Math.Max(0, toDDecimal(tmp2 / tmp1))

        EpStat = "EP WeaponSpeed"
        DPS = DPSs(EpStat)
        tmp2 = (DPS - BaseDPS) / 0.1
        EPVal.MHSpeed = Math.Max(0, toDDecimal(tmp2 / tmp1))


        EpStat = ""

skipStats:
        DPSs.Clear()
        ThreadCollection.Clear()
        simCollection.Clear()


    End Sub

    Sub StartEP(ByVal SimTime As Double, ByVal MainFrm As MainForm)
        DPSs.Clear()
        ThreadCollection.Clear()
        simCollection.Clear()
        EPBase = MainFrm.txtEPBase.Text
        _MainFrm = MainFrm
        Dim sReport As String

        Dim doc As XDocument = New XDocument
        doc.Load("EPconfig.xml")



        Dim BaseDPS As Long
        Dim APDPS As Long
        Dim DPS As Long
        Dim tmp1 As Double
        Dim tmp2 As Double

        Dim Str As String
        Dim Agility As String
        Dim MHSpeed As String
        Dim Exp As String
        Dim MHDPS As String
        Dim SpHit As String
        Dim Hit As String
        Dim ArP As String
        Dim Haste As String
        Dim Crit As String

        Str = 0
        Agility = 0
        MHSpeed = 0
        Exp = 0
        MHDPS = 0
        SpHit = 0
        Hit = 0
        ArP = 0
        Haste = 0
        Crit = 0



        If SimTime = 0 Then SimTime = 1
        'Create EP table
        sReport = "<table border='0' cellspacing='0' style='font-family:Verdana; font-size:10px;'>"

        If doc.Element("//config/Stats").Value.Contains("True") = False Then
            GoTo skipStats
        End If

        'Dry run
        EpStat = "EP DryRun"
        SimConstructor.Start(SimTime, MainFrm)


        EpStat = "EP AttackPower"
        SimConstructor.Start(SimTime, MainFrm)

        If doc.Element("//config/Stats/chkEPStr").Value = "True" Then
            EpStat = "EP Strength"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        If doc.Element("//config/Stats/chkEPAgility").Value = "True" Then
            EpStat = "EP Agility"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        If doc.Element("//config/Stats/chkEPCrit").Value = "True" Then
            EpStat = "EP CritRating"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        If doc.Element("//config/Stats/chkEPHaste").Value = "True" Then
            EpStat = "EP HasteRating1"
            SimConstructor.Start(SimTime, MainFrm)
            EpStat = "EP HasteEstimated"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        If doc.Element("//config/Stats/chkEPArP").Value = "True" Then
            EpStat = "EP ArmorPenetrationRating"
            SimConstructor.Start(SimTime, MainFrm)
        End If


        If doc.Element("//config/Stats/chkEPExp").Value = "True" Then
            EpStat = "EP ExpertiseRating"
            SimConstructor.Start(SimTime, MainFrm)
            EpStat = "EP ExpertiseRatingCap"
            SimConstructor.Start(SimTime, MainFrm)
            EpStat = "EP ExpertiseRatingCapAP"
            SimConstructor.Start(SimTime, MainFrm)

            EpStat = "EP RelativeExpertiseRating"
            SimConstructor.Start(SimTime, MainFrm)


            If MainFrm.cmdPresence.SelectedItem = "Frost" Then
                EpStat = "EP ExpertiseRatingAfterCap"
                SimConstructor.Start(SimTime, MainFrm)
            End If
        End If

        If doc.Element("//config/Stats/chkEPHit").Value = "True" Then
            EpStat = "EP HitRating"
            SimConstructor.Start(SimTime, MainFrm)
            EpStat = "EP HitRatingCap"
            SimConstructor.Start(SimTime, MainFrm)
            EpStat = "EP HitRatingCapAP"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        If doc.Element("//config/Stats/chkEPSpHit").Value = "True" Then
            EpStat = "EP SpellHitRating"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        If doc.Element("//config/Stats/chkEPSMHDPS").Value = "True" Then
            EpStat = "EP WeaponDPS"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        If doc.Element("//config/Stats/chkEPSMHSpeed").Value = "True" Then
            EpStat = "EP WeaponSpeed"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        Dim tmpInt As Integer
        tmpInt = EPBase
        EPBase = 20
        If doc.Element("//config/Stats/chkEPAfterSpellHitRating").Value = "True" Then
            EpStat = "EP AfterSpellHitBase"
            SimConstructor.Start(SimTime, MainFrm)
            EpStat = "EP AfterSpellHitBaseAP"
            SimConstructor.Start(SimTime, MainFrm)
            EpStat = "EP AfterSpellHitRating"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        EPBase = tmpInt
        Jointhread()

        EpStat = "EP DryRun"
        BaseDPS = DPSs(EpStat)

        EpStat = "EP AttackPower"
        APDPS = DPSs(EpStat)
        sReport = sReport + ("<tr><td>" & EpStat & " | 1 (" & toDDecimal((APDPS - BaseDPS) / (2 * EPBase)) & " DPS/per AP) </td></tr>")

        Try
            EpStat = "EP Strength"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / EPBase
            Str = toDDecimal(tmp2 / tmp1)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(tmp2 / tmp1)) & "</td></tr>"
        Catch
        End Try
        Try
            EpStat = "EP Agility"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / EPBase
            Agility = toDDecimal(tmp2 / tmp1)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(tmp2 / tmp1)) & "</td></tr>"
        Catch
        End Try
        Try
            EpStat = "EP CritRating"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / EPBase
            Crit = toDDecimal(tmp2 / tmp1)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(tmp2 / tmp1)) & "</td></tr>"
            '	WriteReport ("Average for " & EPStat & " | " & DPS)
        Catch
        End Try


        Try
            EpStat = "EP HasteEstimated"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / EPBase
            Haste = toDDecimal(tmp2 / tmp1)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(tmp2 / tmp1)) & "</td></tr>"
        Catch

        End Try

        Try
            EpStat = "EP HasteRating1"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / EPBase
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(tmp2 / tmp1)) & "</td></tr>"
        Catch

        End Try
        Try
            EpStat = "EP ArmorPenetrationRating"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / EPBase
            ArP = toDDecimal(tmp2 / tmp1)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(tmp2 / tmp1)) & "</td></tr>"
            '	WriteReport ("Average for " & EPStat & " | " & DPS)
        Catch
        End Try
        Try
            EpStat = "EP ExpertiseRating"
            DPS = DPSs(EpStat)


            tmp1 = (DPSs("EP ExpertiseRatingCapAP") - DPSs("EP ExpertiseRatingCap")) / (2 * EPBase)
            tmp2 = (DPS - DPSs("EP ExpertiseRatingCap")) / EPBase
            Exp = toDDecimal(-tmp2 / tmp1)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(-tmp2 / tmp1)) & "</td></tr>"

            EpStat = "EP RelativeExpertiseRating"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / EPBase
            sReport = sReport + ("<tr><td>Personal Expertise value | " & toDDecimal(tmp2 / tmp1)) & "</td></tr>"



            '	WriteReport ("Average for " & EPStat & " | " & DPS)
        Catch
        End Try


        Try
            EpStat = "EP ExpertiseRatingAfterCap"
            DPS = DPSs(EpStat)
            tmp1 = (DPSs("EP ExpertiseRatingCapAP") - DPSs("EP ExpertiseRatingCap")) / (2 * EPBase)
            tmp2 = (DPS - DPSs("EP ExpertiseRatingCap")) / EPBase
            sReport = sReport + ("<tr><td>ExpertiseRating After Dodge Cap | " & toDDecimal(tmp2 / tmp1)) & "</td></tr>"
            '	WriteReport ("Average for " & EPStat & " | " & DPS)
        Catch
        End Try


        Try
            EpStat = "EP HitRating"
            DPS = DPSs(EpStat)
            tmp1 = (DPSs("EP HitRatingCapAP") - DPSs("EP HitRatingCap")) / (2 * EPBase)
            tmp2 = (DPS - DPSs("EP HitRatingCap")) / EPBase
            Hit = toDDecimal(-tmp2 / tmp1)
            sReport = sReport + ("<tr><td>BeforeMeleeHitCap<8% | " & toDDecimal(-tmp2 / tmp1)) & "</td></tr>"
            '	WriteReport ("Average for " & EPStat & " | " & DPS)
        Catch
        End Try

        Try
            EpStat = "EP SpellHitRating"
            DPS = DPSs(EpStat)
            tmp1 = (DPSs("EP HitRatingCapAP") - DPSs("EP HitRatingCap")) / (2 * EPBase)
            tmp2 = (DPS - DPSs("EP HitRatingCap")) / 20
            SpHit = toDDecimal(tmp2 / tmp1)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(tmp2 / tmp1)) & "</td></tr>"
        Catch
        End Try
        Try
            EpStat = "EP WeaponDPS"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / 10
            MHDPS = toDDecimal(tmp2 / tmp1)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(tmp2 / tmp1)) & "</td></tr>"
            '	WriteReport ("Average for " & EPStat & " | " & DPS)
        Catch
        End Try
        Try
            EpStat = "EP WeaponSpeed"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / 0.1
            MHSpeed = toDDecimal(tmp2 / tmp1)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(tmp2 / tmp1)) & "</td></tr>"
            '	WriteReport ("Average for " & EPStat & " | " & DPS)
        Catch
        End Try

        Try
            EpStat = "EP AfterSpellHitBase"
            BaseDPS = DPSs(EpStat)
            EpStat = "EP AfterSpellHitBaseAP"
            APDPS = DPSs(EpStat)
            EpStat = "EP AfterSpellHitRating"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / EPBase
            sReport = sReport + ("<tr><td>After spell hit cap | " & toDDecimal(tmp2 / tmp1) & "</td></tr>")
            '	WriteReport ("Average for " & EPStat & " | " & DPS)
        Catch
        End Try

        EpStat = ""

skipStats:
        DPSs.Clear()
        ThreadCollection.Clear()
        simCollection.Clear()
        If doc.Element("//config/Sets").Value.Contains("True") = False Then
            GoTo skipSets
        End If

        EpStat = "EP 0T7"
        SimConstructor.Start(SimTime, MainFrm)

        EpStat = "EP AttackPower0T7"
        SimConstructor.Start(SimTime, MainFrm)

        If doc.Element("//config/Sets/chkEP2T7").Value = "True" Then
            EpStat = "EP 2T7"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        If doc.Element("//config/Sets/chkEP4PT7").Value = "True" Then
            EpStat = "EP 4T7"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        If doc.Element("//config/Sets/chkEP2PT8").Value = "True" Then
            EpStat = "EP 2T8"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        If doc.Element("//config/Sets/chkEP4PT8").Value = "True" Then
            EpStat = "EP 4T8"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        If doc.Element("//config/Sets/chkEP2PT9").Value = "True" Then
            EpStat = "EP 2T9"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        If doc.Element("//config/Sets/chkEP4PT9").Value = "True" Then
            EpStat = "EP 4T9"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        If doc.Element("//config/Sets/chkEP2PT10").Value = "True" Then
            EpStat = "EP 2T10"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        If doc.Element("//config/Sets/chkEP4PT10").Value = "True" Then
            EpStat = "EP 4T10"
            SimConstructor.Start(SimTime, MainFrm)
        End If
        Jointhread()


        EpStat = "EP 0T7"
        BaseDPS = DPSs(EpStat)

        EpStat = "EP AttackPower0T7"
        APDPS = DPSs(EpStat)

        Try
            EpStat = "EP 2T7"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(100 * tmp2 / tmp1)) & "</td></tr>"

        Catch
        End Try
        Try
            EpStat = "EP 4T7"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(100 * tmp2 / tmp1)) & "</td></tr>"

        Catch
        End Try
        Try
            EpStat = "EP 2T8"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(100 * tmp2 / tmp1)) & "</td></tr>"

        Catch
        End Try
        Try
            EpStat = "EP 4T8"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(100 * tmp2 / tmp1)) & "</td></tr>"

        Catch
        End Try
        Try
            EpStat = "EP 2T9"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(100 * tmp2 / tmp1)) & "</td></tr>"

        Catch
        End Try
        Try
            EpStat = "EP 4T9"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(100 * tmp2 / tmp1)) & "</td></tr>"
        Catch
        End Try
        Try
            EpStat = "EP 2T10"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(100 * tmp2 / tmp1)) & "</td></tr>"
        Catch
        End Try
        Try
            EpStat = "EP 4T10"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(100 * tmp2 / tmp1)) & "</td></tr>"
        Catch
        End Try

        WriteReport("")

skipSets:


        If doc.Element("//config/Trinket").Value.Contains("True") = False Then
            GoTo skipTrinket
        End If

        EpStat = "EP NoTrinket"
        SimConstructor.Start(SimTime, MainFrm)

        EpStat = "EP AttackPowerNoTrinket"
        SimConstructor.Start(SimTime, MainFrm)


        Dim trinketsList As XElement
        Dim tNode As XElement
        trinketsList = doc.Element("//config/Trinket")

        For Each tNode In trinketsList.Elements
            If tNode.Value = "True" Then
                EpStat = tNode.Name.Replace("chkEP", "EP ")
                SimConstructor.Start(SimTime, MainFrm)
            End If
        Next
        Jointhread()


        EpStat = "EP NoTrinket"
        BaseDPS = DPSs(EpStat)

        EpStat = "EP AttackPowerNoTrinket"
        APDPS = DPSs(EpStat)


        For Each tNode In trinketsList.Elements
            If tNode.Value = "True" Then
                Try
                    EpStat = tNode.Name.Replace("chkEP", "EP ")
                    DPS = DPSs(EpStat)
                    tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
                    tmp2 = (DPS - BaseDPS) / (2 * EPBase)
                    sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(100 * tmp2 / tmp1)) & "</td></tr>"
                Catch

                End Try
            End If
        Next
skipTrinket:
        sReport = sReport & "<tr><td COLSPAN=8> | Template | " & Split(_MainFrm.cmbTemplate.SelectedValue, ".")(0) & "</td></tr>"
        If Rotate Then
            sReport = sReport & "<tr><td COLSPAN=8> | Rotation | " & Split(_MainFrm.cmbRotation.SelectedValue, ".")(0) & "</td></tr>"
        Else
            sReport = sReport & "<tr><td COLSPAN=8> | Priority | " & Split(_MainFrm.cmbPrio.SelectedValue, ".")(0) & "</td></tr>"
        End If
        sReport = sReport & "<tr><td COLSPAN=8> | Presence | " & _MainFrm.cmdPresence.SelectedValue & vbCrLf & "</td></tr>"
        sReport = sReport & "<tr><td COLSPAN=8> | Sigil | " & _MainFrm.cmbSigils.SelectedValue & vbCrLf & "</td></tr>"
        sReport = sReport & "<tr><td COLSPAN=8> | RuneEnchant | " & _MainFrm.cmbRuneMH.SelectedValue & " / " & _MainFrm.cmbRuneOH.SelectedValue & "</td></tr>"
        sReport = sReport & "<tr><td COLSPAN=8> | Pet Calculation | " & _MainFrm.ckPet.IsChecked & "</td></tr>"
        Str = Str.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".")
        Agility = Agility.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".")
        MHSpeed = MHSpeed.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".")
        Exp = Exp.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".")
        MHDPS = MHDPS.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".")
        SpHit = SpHit.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".")
        Hit = Hit.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".")
        ArP = ArP.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".")
        Haste = Haste.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".")
        Crit = Crit.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".")




        Dim lootlink As String
        lootlink = "<tr><td COLSPAN=8><a href=http://www.guildox.com/wr.asp?Cla=2048&s7=3.2&str=" & Str & "&Arm=" & 0.028 & "&mh=" & Haste & "&dps=" & MHDPS & "&mcr=" & Crit & _
         "&odps=" & 0 & "&Agi=" & Agility & "&mhit=" & Hit & "&map=1" & "&msp=" & MHSpeed & "&arp=" & ArP & "&osp=0" & "&Exp=" & Exp & " target='_blank'>lootlink non hit caped</a></td></tr>"
        sReport = sReport & lootlink

        lootlink = "<tr><td COLSPAN=8><a href=http://www.guildox.com/wr.asp?Cla=2048&s7=3.2&str=" & Str & "&Arm=" & 0.028 & "&mh=" & Haste & "&dps=" & MHDPS & "&mcr=" & Crit & _
         "&odps=" & 0 & "&Agi=" & Agility & "&mhit=" & SpHit & "&map=1" & "&msp=" & MHSpeed & "&arp=" & ArP & "&osp=0" & "&Exp=" & Exp & " target='_blank'>lootlink hit caped</a></td></tr>"
        sReport = sReport & lootlink
        Dim pwan As String
        pwan = "<tr><td COLSPAN=8>Non hit caped ( Pawn: v1: " + Convert.ToChar(34) + "DK Sim" + Convert.ToChar(34) + ": ArmorPenetration=" + ArP + ", HitRating=" + Hit + ", CritRating=" + Crit + ", Dps=" + MHDPS + ", Strength=" + Str + ", Armor=0.028, Agility=" + Agility + ", HasteRating=" + Haste + ", Speed=" + MHSpeed + ", ExpertiseRating=" + Exp + ", Ap=1, GemQualityLevel=82 )</td></tr>"
        sReport = sReport + pwan
        pwan = "<tr><td COLSPAN=8>hit caped ( Pawn: v1: " + Convert.ToChar(34) + "DK Sim" + Convert.ToChar(34) + ": ArmorPenetration=" + ArP + ", HitRating=" + SpHit + ", CritRating=" + Crit + ", Dps=" + MHDPS + ", Strength=" + Str + ", Armor=0.028, Agility=" + Agility + ", HasteRating=" + Haste + ", Speed=" + MHSpeed + ", ExpertiseRating=" + Exp + ", Ap=1, GemQualityLevel=82 )</td></tr>"
        sReport = sReport + pwan
        sReport = sReport + ("<hr width='80%' align='center' noshade ></hr>")


        sReport = sReport + ("</table>")

        WriteReport(sReport)
        EpStat = ""

    End Sub


    Sub StartScaling(ByVal pb As ProgressBar, ByVal SimTime As Double, ByVal MainFrm As MainForm)

        DPSs.Clear()
        ThreadCollection.Clear()
        simCollection.Clear()
        EPBase = 50
        _MainFrm = MainFrm
        Dim sReport As String
        Dim doc As XDocument = New XDocument


        doc.Load("ScalingConfig.xml")
        Dim xNodelist As XElement
        xNodelist = doc.Element("//config/Stats")
        Dim xNode As XElement
        Dim i As Integer
        sReport = "<table border='0' cellspacing='0' style='font-family:Verdana; font-size:10px;'>"
        Dim max As Integer
        max = 200
        EPBase = 5

        sReport = sReport + ("<tr><td>Stat</td>")
        For i = 0 To max
            sReport = sReport & "<td>" & EPBase * i & "</td>"
        Next
        sReport = sReport + ("</tr>")

        Dim INSRTCOLOR As String
        For Each xNode In xNodelist.Elements

            If xNode.Value = "True" Then
                For i = 0 To max
                    EpStat = Replace(xNode.Name.ToString, "chk", "") & i
                    SimConstructor.Start(1, MainFrm)
                Next i
                Jointhread()
                EpStat = Replace(xNode.Name.ToString, "chk", "")

                INSRTCOLOR = ""
                Select Case EpStat
                    Case "ScaExp"
                        INSRTCOLOR = "Violet"
                    Case "ScaHit"
                        INSRTCOLOR = "Yellow"
                    Case "ScaArP"
                        INSRTCOLOR = "Maroon"
                    Case "ScaHaste"
                        INSRTCOLOR = "Pink"
                    Case "ScaCrit"
                        INSRTCOLOR = "Orange"
                    Case "ScaAgility"
                        INSRTCOLOR = "Purple"
                    Case "ScaStr"
                        INSRTCOLOR = "Red"
                End Select
                INSRTCOLOR = "Black"
                sReport = sReport + ("<tr><td><font color=" & INSRTCOLOR & ">" & EpStat & "</td>")
                For i = 0 To max
                    sReport = sReport + ("<td>" & DPSs(EpStat & i) & "</td>")
                Next i
                sReport = sReport + ("</tr>")
            End If

        Next
        sReport = sReport & "</table>"

        WriteReport(sReport)
        'createGraph
        EpStat = ""

    End Sub
    Sub StartSpecDpsValue(ByVal pb As ProgressBar, ByVal SimTime As Double, ByVal MainFrm As MainForm)
        Dim sReport As String

        'on error resume next
        DPSs.Clear()
        ThreadCollection.Clear()
        simCollection.Clear()
        EPBase = 50
        _MainFrm = MainFrm

        Dim doc As XDocument = New XDocument

        doc.Load("\Templates\" & MainFrm.cmbTemplate.SelectedValue)
        Dim xNodelist As XElement
        xNodelist = doc.Element("//Talents")
        Dim xNode As XElement

        EpStat = "OriginalSpec"
        SimConstructor.Start(MainFrm.txtSimtime.Text, MainFrm)



        For Each xNode In xNodelist.Elements
            If (xNode.Name <> "URL" And xNode.Name <> "Glyphs") And xNode.Value <> "0" Then
                EpStat = xNode.Name.ToString
                SimConstructor.Start(MainFrm.txtSimtime.Text, MainFrm)
            End If
        Next
        Jointhread()

        Dim BaseDPS As Integer
        EpStat = "OriginalSpec"
        BaseDPS = DPSs(EpStat)

        sReport = "<table border='0' cellspacing='0' style='font-family:Verdana; font-size:10px;'>"

        sReport += "<tr><td>Average for " & EpStat & " | " & BaseDPS & "</tr></td>"


        For Each xNode In xNodelist.Elements
            If (xNode.Name <> "URL" And xNode.Name <> "Glyphs") And xNode.Value <> "0" Then
                EpStat = xNode.Name.ToString
                sReport += "<tr><td>Value for " & EpStat & " | " & toDDecimal((BaseDPS - DPSs(EpStat)) / xNode.Value) & "</tr></td>"
            End If
        Next
        sReport += "</table>"
        sReport += "<hr width='80%' align='center' noshade ></hr>"
        WriteReport(sReport)
        EpStat = ""
    End Sub

    Sub RemoveStoppedthread()
        Dim j As Integer
        Dim t As Thread
        For j = 0 To ThreadCollection.Count - 1
            t = ThreadCollection.Item(j)
            If t.ThreadState = ThreadState.Stopped Then
                ThreadCollection.Remove(t)
                RemoveStoppedthread()
                Exit Sub
            End If
        Next
    End Sub



    Sub Jointhread()
        Dim t As Thread
        Dim core As Integer = Environment.ProcessorCount
        Dim i As Integer
        Do Until ThreadCollection.Count = 0
            RemoveStoppedthread()
            i = 0
            For Each t In ThreadCollection

                If i < core Then
                    If t.ThreadState = ThreadState.Unstarted Then t.Start()
                End If
                If i = 0 Then
                    t.Join(100)
                    _MainFrm.UpdateProgressBar()
                End If
                i += 1
            Next
        Loop
    End Sub





End Module
