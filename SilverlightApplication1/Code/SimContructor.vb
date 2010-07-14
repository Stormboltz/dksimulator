Imports System.Xml.Linq
Imports System.Threading
Imports System.IO.IsolatedStorage
Imports System.IO

'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 15/09/2009
' Heure: 16:15
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Module SimConstructor
    Private StartTime As Date


    Friend PetFriendly As Boolean

    ' Friend ReportPath As String
    Friend EpStat As String
    Friend Rotate As Boolean
    Friend DPSs As New Collection
    Friend sThreadCollection As New Collection
    Friend EPBase As Integer
    'Friend ThreadCollection As New Collection
    Friend ThreadCollection As New Collections.Generic.List(Of Thread)
    Dim th As Thread
    Friend simCollection As New List(Of Sim)
    Public WithEvents _MainFrm As MainForm
    Public Event AllSimdone()



    Sub New()

    End Sub
    Sub OnSim_Closing(ByVal sender As Object, ByVal e As EventArgs)

        If TypeOf sender Is Sim Then
            Dim s As Sim
            s = sender
            simCollection.Remove(sender)
            RemoveStoppedthread()
            PauseResumeThread()
            If simCollection.Count = 0 Then
                RaiseEvent AllSimdone()


            Else
                Diagnostics.Debug.WriteLine("SIM remaining: " & simCollection.Count)
            End If
            RemoveHandler s.Sim_Closing, AddressOf OnSim_Closing
            s = Nothing
        End If
    End Sub


    Sub SingleSim_Done()
        RemoveHandler AllSimdone, AddressOf SingleSim_Done
        _MainFrm.TryToOpenReport()
    End Sub


    Sub PauseResumeThread()
        Dim t As Thread
        Dim core As Integer = Environment.ProcessorCount
        Dim i As Integer = 0
        i = 0
        For Each t In ThreadCollection
            If i < core Then
                If t.ThreadState = ThreadState.Unstarted Then t.Start()
            End If
            i += 1
        Next
    End Sub


    Sub Start(ByVal SimTime As Double, ByVal MainFrm As MainForm, Optional ByVal StartNow As Boolean = False)
        Dim sim As Sim
        Dim newthread As System.Threading.Thread
        sim = New Sim
        _MainFrm = MainFrm
        StartNow = True

        If EpStat <> "" Then
            sim.Prepare(SimTime, MainFrm, EpStat, EPBase)
        Else
            AddHandler AllSimdone, AddressOf SingleSim_Done
            sim.Prepare(SimTime, MainFrm)
        End If
      
        newthread = New Thread(AddressOf sim.Start)
        'newthread.IsBackground = True
        If sim.EPStat <> "" Then
            Try
                newthread.Name = sim.EPStat
            Catch ex As Exception

            End Try

        End If
        If StartNow Then
            newthread.Start()

            'simCollection.Clear()
        End If
        ThreadCollection.Add(newthread)
        simCollection.Add(sim)
        AddHandler sim.Sim_Closing, AddressOf OnSim_Closing
    End Sub

    Function GetFastDPS(ByVal MainFrm As MainForm) As Integer
        Dim i As Integer
        DPSs.Clear()
        ThreadCollection.Clear()
        simCollection.Clear()
        EpStat = "GetFastDPS"
        Start(10, MainFrm, True)
        Jointhread()
        Try
            i = DPSs.Item(1)

        Catch e As Exception
            Diagnostics.Debug.WriteLine(e.ToString)
        End Try
        Return i
    End Function
    Sub CalculateEP()
        Try
            RemoveHandler AllSimdone, AddressOf CalculateEP
        Catch ex As Exception

        End Try

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

        Dim rp As New Report
        EpStat = "EP DryRun"
        BaseDPS = DPSs(EpStat)

        EpStat = "EP AttackPower"
        APDPS = DPSs(EpStat)

        rp.AddAdditionalInfo(EpStat, "1 (" & toDDecimal((APDPS - BaseDPS) / (2 * EPBase)) & " DPS/per AP)")

        Try
            EpStat = "EP Strength"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / EPBase
            Str = toDDecimal(tmp2 / tmp1)
            rp.AddAdditionalInfo(EpStat, Str)
        Catch
        End Try
        Try
            EpStat = "EP Agility"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / EPBase
            Agility = toDDecimal(tmp2 / tmp1)
            rp.AddAdditionalInfo(EpStat, Agility)
        Catch
        End Try
        Try
            EpStat = "EP CritRating"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / EPBase
            Crit = toDDecimal(tmp2 / tmp1)
            rp.AddAdditionalInfo(EpStat, Crit)
        Catch
        End Try


        Try
            EpStat = "EP HasteEstimated"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / EPBase
            Haste = toDDecimal(tmp2 / tmp1)
            rp.AddAdditionalInfo(EpStat, Haste)

        Catch

        End Try

        Try
            EpStat = "EP HasteRating1"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / EPBase
            rp.AddAdditionalInfo(EpStat, toDDecimal(tmp2 / tmp1))

        Catch

        End Try
        Try
            EpStat = "EP ArmorPenetrationRating"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / EPBase
            ArP = toDDecimal(tmp2 / tmp1)
            rp.AddAdditionalInfo(EpStat, ArP)

        Catch
        End Try
        Try
            EpStat = "EP ExpertiseRating"
            DPS = DPSs(EpStat)


            tmp1 = (DPSs("EP ExpertiseRatingCapAP") - DPSs("EP ExpertiseRatingCap")) / (2 * EPBase)
            tmp2 = (DPS - DPSs("EP ExpertiseRatingCap")) / EPBase
            Exp = toDDecimal(-tmp2 / tmp1)
            rp.AddAdditionalInfo(EpStat, Exp)

            EpStat = "EP RelativeExpertiseRating"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / EPBase
            rp.AddAdditionalInfo("Personal Expertise value", toDDecimal(tmp2 / tmp1))
        Catch
        End Try


        Try
            EpStat = "EP ExpertiseRatingAfterCap"
            DPS = DPSs(EpStat)
            tmp1 = (DPSs("EP ExpertiseRatingCapAP") - DPSs("EP ExpertiseRatingCap")) / (2 * EPBase)
            tmp2 = (DPS - DPSs("EP ExpertiseRatingCap")) / EPBase
            rp.AddAdditionalInfo("ExpertiseRating After Dodge Cap", toDDecimal(tmp2 / tmp1))
        Catch
        End Try


        Try
            EpStat = "EP HitRating"
            DPS = DPSs(EpStat)
            tmp1 = (DPSs("EP HitRatingCapAP") - DPSs("EP HitRatingCap")) / (2 * EPBase)
            tmp2 = (DPS - DPSs("EP HitRatingCap")) / EPBase
            Hit = toDDecimal(-tmp2 / tmp1)
            rp.AddAdditionalInfo("Before Melee Hit Cap", Hit)
            '	WriteReport ("Average for " & EPStat & " | " & DPS)
        Catch
        End Try

        Try
            EpStat = "EP SpellHitRating"
            DPS = DPSs(EpStat)
            tmp1 = (DPSs("EP HitRatingCapAP") - DPSs("EP HitRatingCap")) / (2 * EPBase)
            tmp2 = (DPS - DPSs("EP HitRatingCap")) / 20
            SpHit = toDDecimal(tmp2 / tmp1)
            rp.AddAdditionalInfo(EpStat, SpHit)
        Catch
        End Try
        Try
            EpStat = "EP WeaponDPS"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / 10
            MHDPS = toDDecimal(tmp2 / tmp1)
            rp.AddAdditionalInfo(EpStat, MHDPS)
        Catch
        End Try
        Try
            EpStat = "EP WeaponSpeed"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / 0.1
            MHSpeed = toDDecimal(tmp2 / tmp1)
            rp.AddAdditionalInfo(EpStat, MHSpeed)
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
            rp.AddAdditionalInfo(EpStat, toDDecimal(tmp2 / tmp1))
        Catch
        End Try

        EpStat = ""
        EpStat = "EP 0T7"
        BaseDPS = DPSs(EpStat)

        EpStat = "EP AttackPower0T7"
        APDPS = DPSs(EpStat)

        Try
            EpStat = "EP 2T7"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            rp.AddAdditionalInfo(EpStat, toDDecimal(100 * tmp2 / tmp1))
        Catch
        End Try
        Try
            EpStat = "EP 4T7"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            rp.AddAdditionalInfo(EpStat, toDDecimal(100 * tmp2 / tmp1))

        Catch
        End Try
        Try
            EpStat = "EP 2T8"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            rp.AddAdditionalInfo(EpStat, toDDecimal(100 * tmp2 / tmp1))

        Catch
        End Try
        Try
            EpStat = "EP 4T8"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            rp.AddAdditionalInfo(EpStat, toDDecimal(100 * tmp2 / tmp1))

        Catch
        End Try
        Try
            EpStat = "EP 2T9"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            rp.AddAdditionalInfo(EpStat, toDDecimal(100 * tmp2 / tmp1))

        Catch
        End Try
        Try
            EpStat = "EP 4T9"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            rp.AddAdditionalInfo(EpStat, toDDecimal(100 * tmp2 / tmp1))
        Catch
        End Try
        Try
            EpStat = "EP 2T10"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            rp.AddAdditionalInfo(EpStat, toDDecimal(100 * tmp2 / tmp1))
        Catch
        End Try
        Try
            EpStat = "EP 4T10"
            DPS = DPSs(EpStat)
            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
            rp.AddAdditionalInfo(EpStat, toDDecimal(100 * tmp2 / tmp1))
        Catch
        End Try

        'rp.AddAdditionalInfo("Template", _MainFrm.cmbTemplate.SelectedValue)


        'If Rotate Then
        '    rp.AddAdditionalInfo("Rotation", _MainFrm.cmbRotation.SelectedValue)
        'Else
        '    rp.AddAdditionalInfo("Priority", _MainFrm.cmbPrio.SelectedValue)
        'End If
        'rp.AddAdditionalInfo("Presence", _MainFrm.cmdPresence.SelectedValue)
        'rp.AddAdditionalInfo("Sigil", _MainFrm.cmbSigils.SelectedValue)
        'Try
        '    rp.AddAdditionalInfo("RuneEnchant", _MainFrm.cmbRuneMH.SelectedValue & " / " & _MainFrm.cmbRuneOH.SelectedValue)
        'Catch ex As Exception
        'End Try
        'rp.AddAdditionalInfo("Pet Calculation", _MainFrm.ckPet.IsChecked)

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
        lootlink = "http://www.guildox.com/wr.asp?Cla=2048&s7=3.2&str=" & Str & "&Arm=" & 0.028 & "&mh=" & Haste & "&dps=" & MHDPS & "&mcr=" & Crit & _
         "&odps=" & 0 & "&Agi=" & Agility & "&mhit=" & Hit & "&map=1" & "&msp=" & MHSpeed & "&arp=" & ArP & "&osp=0" & "&Exp=" & Exp
        rp.AddAdditionalInfo("lootlink non hit caped", lootlink)

        lootlink = "http://www.guildox.com/wr.asp?Cla=2048&s7=3.2&str=" & Str & "&Arm=" & 0.028 & "&mh=" & Haste & "&dps=" & MHDPS & "&mcr=" & Crit & _
    "&odps=" & 0 & "&Agi=" & Agility & "&mhit=" & SpHit & "&map=1" & "&msp=" & MHSpeed & "&arp=" & ArP & "&osp=0" & "&Exp=" & Exp
        rp.AddAdditionalInfo("lootlink hit caped", lootlink)
        Dim pwan As String
        pwan = "Pawn: v1: " + Convert.ToChar(34) + "Non hit caped" + Convert.ToChar(34) + ": ArmorPenetration=" + ArP + ", HitRating=" + Hit + ", CritRating=" + Crit + ", Dps=" + MHDPS + ", Strength=" + Str + ", Armor=0.028, Agility=" + Agility + ", HasteRating=" + Haste + ", Speed=" + MHSpeed + ", ExpertiseRating=" + Exp + ", Ap=1, GemQualityLevel=82 )"
        rp.AddAdditionalInfo("pwan Non hit caped", pwan)
        pwan = "Pawn: v1: " + Convert.ToChar(34) + "Hit caped" + Convert.ToChar(34) + ": ArmorPenetration=" + ArP + ", HitRating=" + SpHit + ", CritRating=" + Crit + ", Dps=" + MHDPS + ", Strength=" + Str + ", Armor=0.028, Agility=" + Agility + ", HasteRating=" + Haste + ", Speed=" + MHSpeed + ", ExpertiseRating=" + Exp + ", Ap=1, GemQualityLevel=82 )"
        rp.AddAdditionalInfo("pwan hit caped", pwan)
        Dim dif As Decimal = ((Now.Ticks - StartTime.Ticks) / 10000000)
        rp.AddAdditionalInfo("Generated in ", Decimal.Round(dif, 2).ToString & " s")
        rp.Save("")
        EpStat = ""
        _MainFrm.TryToOpenTextReport()
    End Sub
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
        AddHandler AllSimdone, AddressOf CalculateEP
        StartTime = Now

        DPSs.Clear()
        ThreadCollection.Clear()
        simCollection.Clear()
        EPBase = MainFrm.txtEPBase.Text
        _MainFrm = MainFrm



        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/EPconfig.xml", FileMode.Open, FileAccess.Read, isoStore)
                Dim doc As XDocument = XDocument.Load(isoStream)




                If SimTime = 0 Then SimTime = 1
                If (From el In doc.Element("config").Element("Stats").Elements
                    Where el.Value = ("true")
                    Select el).Count = 0 Then
                    GoTo skipStats
                End If


                'Dry run
                EpStat = "EP DryRun"
                SimConstructor.Start(SimTime, MainFrm)


                EpStat = "EP AttackPower"
                SimConstructor.Start(SimTime, MainFrm)

                If doc.Element("config").Element("Stats").Element("chkEPStr").Value = "true" Then
                    EpStat = "EP Strength"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                If doc.Element("config").Element("Stats").Element("chkEPAgility").Value = "true" Then
                    EpStat = "EP Agility"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                If doc.Element("config").Element("Stats").Element("chkEPCrit").Value = "true" Then
                    EpStat = "EP CritRating"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                If doc.Element("config").Element("Stats").Element("chkEPHaste").Value = "true" Then
                    EpStat = "EP HasteRating1"
                    SimConstructor.Start(SimTime, MainFrm)
                    EpStat = "EP HasteEstimated"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                If doc.Element("config").Element("Stats").Element("chkEPArP").Value = "true" Then
                    EpStat = "EP ArmorPenetrationRating"
                    SimConstructor.Start(SimTime, MainFrm)
                End If


                If doc.Element("config").Element("Stats").Element("chkEPExp").Value = "true" Then
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

                If doc.Element("config").Element("Stats").Element("chkEPHit").Value = "true" Then
                    EpStat = "EP HitRating"
                    SimConstructor.Start(SimTime, MainFrm)
                    EpStat = "EP HitRatingCap"
                    SimConstructor.Start(SimTime, MainFrm)
                    EpStat = "EP HitRatingCapAP"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                If doc.Element("config").Element("Stats").Element("chkEPSpHit").Value = "true" Then
                    EpStat = "EP SpellHitRating"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                If doc.Element("config").Element("Stats").Element("chkEPSMHDPS").Value = "true" Then
                    EpStat = "EP WeaponDPS"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                If doc.Element("config").Element("Stats").Element("chkEPSMHSpeed").Value = "true" Then
                    EpStat = "EP WeaponSpeed"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                Dim tmpInt As Integer
                tmpInt = EPBase
                EPBase = 20
                If doc.Element("config").Element("Stats").Element("chkEPAfterSpellHitRating").Value = "true" Then
                    EpStat = "EP AfterSpellHitBase"
                    SimConstructor.Start(SimTime, MainFrm)
                    EpStat = "EP AfterSpellHitBaseAP"
                    SimConstructor.Start(SimTime, MainFrm)
                    EpStat = "EP AfterSpellHitRating"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                EPBase = tmpInt
                'Jointhread()



skipStats:
                If doc.Element("config").Element("Sets").Value.Contains("true") = False Then
                    GoTo skipSets
                End If

                EpStat = "EP 0T7"
                SimConstructor.Start(SimTime, MainFrm)

                EpStat = "EP AttackPower0T7"
                SimConstructor.Start(SimTime, MainFrm)

                If doc.Element("config").Element("Sets").Element("chkEP2T7").Value = "true" Then
                    EpStat = "EP 2T7"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                If doc.Element("config").Element("Sets").Element("chkEP4PT7").Value = "true" Then
                    EpStat = "EP 4T7"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                If doc.Element("config").Element("Sets").Element("chkEP2PT8").Value = "true" Then
                    EpStat = "EP 2T8"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                If doc.Element("config").Element("Sets").Element("chkEP4PT8").Value = "true" Then
                    EpStat = "EP 4T8"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                If doc.Element("config").Element("Sets").Element("chkEP2PT9").Value = "true" Then
                    EpStat = "EP 2T9"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                If doc.Element("config").Element("Sets").Element("chkEP4PT9").Value = "true" Then
                    EpStat = "EP 4T9"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                If doc.Element("config").Element("Sets").Element("chkEP2PT10").Value = "true" Then
                    EpStat = "EP 2T10"
                    SimConstructor.Start(SimTime, MainFrm)
                End If
                If doc.Element("config").Element("Sets").Element("chkEP4PT10").Value = "true" Then
                    EpStat = "EP 4T10"
                    SimConstructor.Start(SimTime, MainFrm)
                End If





skipSets:

                GoTo skipTrinket
                'If doc.Element("config").Element("Trinket").Value.Contains("true") = False Then
                '    GoTo skipTrinket
                'End If

                'EpStat = "EP NoTrinket"
                'SimConstructor.Start(SimTime, MainFrm)

                'EpStat = "EP AttackPowerNoTrinket"
                'SimConstructor.Start(SimTime, MainFrm)


                'Dim trinketsList As XElement
                'Dim tNode As XElement
                'trinketsList = doc.Element("config/Trinket")

                'For Each tNode In trinketsList.Elements
                '    If tNode.Value = "true" Then
                '        EpStat = tNode.Name.ToString.Replace("chkEP", "EP ")
                '        SimConstructor.Start(SimTime, MainFrm)
                '    End If
                'Next
                'Jointhread()


                'EpStat = "EP NoTrinket"
                'BaseDPS = DPSs(EpStat)

                'EpStat = "EP AttackPowerNoTrinket"
                'APDPS = DPSs(EpStat)


                'For Each tNode In trinketsList.Elements
                '    If tNode.Value = "true" Then
                '        Try
                '            EpStat = tNode.Name.ToString.Replace("chkEP", "EP ")
                '            DPS = DPSs(EpStat)
                '            tmp1 = (APDPS - BaseDPS) / (2 * EPBase)
                '            tmp2 = (DPS - BaseDPS) / (2 * EPBase)
                '            sReport = sReport + ("<tr><td>" & EpStat & " | " & toDDecimal(100 * tmp2 / tmp1)) & "</td></tr>"
                '        Catch

                '        End Try
                '    End If
                'Next
skipTrinket:

                EpStat = ""
            End Using

        End Using

    End Sub


    Sub StartScaling(ByVal pb As ProgressBar, ByVal SimTime As Double, ByVal MainFrm As MainForm)

        DPSs.Clear()
        ThreadCollection.Clear()
        simCollection.Clear()
        EPBase = 50
        _MainFrm = MainFrm
        Dim sReport As String
        Dim doc As XDocument = New XDocument

        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/ScalingConfig.xml", FileMode.Open, FileAccess.Read, isoStore)
                doc = XDocument.Load(isoStream)
            End Using
        End Using

        Dim xNodelist As XElement
        xNodelist = doc.Element("config").Element("Stats")
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

        'WriteReport(sReport)
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
        xNodelist = doc.Element("Talents")
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
        'WriteReport(sReport)
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
                End If
                i += 1
            Next
        Loop
    End Sub

   



End Module
