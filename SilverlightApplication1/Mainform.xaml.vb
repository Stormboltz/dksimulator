Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO
Imports System.Windows.Resources
Imports System.ComponentModel
Imports System.Threading


Partial Public Class MainForm
    Inherits Page

    Friend Shared RefreshRequest As Integer = 0


    Private isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
    Friend EditType As String
    Friend EPVal As New EPValues

    Friend ItemDB As XDocument
    Friend GemDB As XDocument
    Friend GemBonusDB As XDocument
    Friend EnchantDB As XDocument
    Friend trinketDB As XDocument
    Friend SetBonusDB As XDocument
    Friend WeapProcDB As XDocument
    Friend FoodDB As XDocument
    Friend FlaskDB As XDocument
    Friend ConsumableDB As XDocument


    Private WithEvents TEdit As New TemplateEditor
    Private WithEvents PrioEditor As New PriorityEditor
    Private WithEvents GearSelector As GearSelectorMainForm
    Private WithEvents ScenarioEditor As ScenarioEditor



    Dim WithEvents _worker As BackgroundWorker = New BackgroundWorker()
    Public Sub New()
        InitializeComponent()

        'For some bizarre reason we need to tell the BackgoundWorker that we will be
        'reporting progress!
        _worker.WorkerReportsProgress = True
        ProgressBar1.Maximum = 100

        ' Wire up an event handler to respond to progress changes during the operation.
        'Let the BackgoundWorker know what operation to call when it's kicked off.
    End Sub
    Sub _worker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles _worker.DoWork
        DoSomethingHard()
    End Sub
    Private Sub DoSomethingHard()
        For i = 0 To 100
            '// Report some progress - this will result in the ProgressChanged event being
            '// raised
            _worker.ReportProgress(i, i.ToString & "% complete")

            Thread.Sleep(1000)
        Next
    End Sub
    Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles btEP.Click
        If SimConstructor.simCollection.Count > 0 Then Return
        If LoadBeforeSim() = False Then Exit Sub
        SimConstructor.StartEP(txtSimtime.Text, Me)
    End Sub
    Sub worker_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles _worker.ProgressChanged
        'This is the opportunity to update the controls on the main thread
        ProgressBar1.Value = e.ProgressPercentage
        'Executes when the user navigates to this page.
    End Sub


    Function LoadBeforeSim()
        saveConfig()
        SaveEPOptions()
        SaveBuffOption()
        saveScaling()
        saveTankOptions()

        Return True
    End Function

    Sub saveScaling()
        Dim doc As XDocument = XDocument.Parse("<config></config>")
        doc.Element("config").Add(New XElement("Stats", ""))
        Dim chkBox As CheckBox
        For Each stk In gbScaling.Children
            If TypeOf (stk) Is StackPanel Then
                For Each ctrl As Control In CType(stk, StackPanel).Children
                    If ctrl.Name.StartsWith("chk") Then
                        chkBox = ctrl
                        doc.Element("config").Element("Stats").Add(New XElement(chkBox.Name, chkBox.IsChecked))
                    End If
                Next
            End If

        Next

        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Scalingconfig.xml", FileMode.Create, isoStore)
                doc.Save(isoStream)
            End Using
        End Using
    End Sub
    Sub saveTankOptions()

        Dim doc As XDocument = XDocument.Parse("<config></config>")
        doc.Element("config").Add(New XElement("Stats", ""))
        Dim txtBox As TextBox
        For Each ctrl As Control In gbTank.Children
            If ctrl.Name.StartsWith("txt") Then
                txtBox = ctrl
                doc.Element("config").Element("Stats").Add(New XElement(txtBox.Name, txtBox.Text))
            End If
        Next
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/TankConfig.xml", FileMode.Create, isoStore)
                doc.Save(isoStream)
            End Using
        End Using
    End Sub
    Sub SaveBuffOption()


        Dim doc As XDocument = XDocument.Parse("<config></config>")
        Dim chkBox As CheckBox
        For Each ctrl As Control In GrpBuff.Children
            If ctrl.Name.StartsWith("chk") Then
                chkBox = ctrl
                doc.Element("config").Add(New XElement(chkBox.Name, chkBox.IsChecked))
            End If
        Next
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Buffconfig.xml", FileMode.Create, isoStore)
                doc.Save(isoStream)
            End Using
        End Using
    End Sub
    Sub SaveEPOptions()
        Dim doc As XDocument = XDocument.Parse("<config></config>")
        doc.Element("config").Add(New XElement("Stats", ""))
        doc.Element("config").Add(New XElement("Sets", ""))
        doc.Element("config").Add(New XElement("Trinket", ""))

        Dim chkBox As CheckBox
        For Each ctrl As Control In grpEPMain.Children
            If ctrl.Name.StartsWith("chk") Then
                chkBox = ctrl
                doc.Element("config").Element("Stats").Add(New XElement(chkBox.Name, chkBox.IsChecked))
            End If
        Next
        For Each ctrl As Control In grpEPSet.Children
            If ctrl.Name.StartsWith("chk") Then
                chkBox = ctrl
                doc.Element("config").Element("Sets").Add(New XElement(chkBox.Name, chkBox.IsChecked))
            End If
        Next
        For Each ctrl As Control In grpEPSet.Children
            If ctrl.Name.StartsWith("chk") Then
                chkBox = ctrl
                doc.Element("config").Element("Trinket").Add(New XElement(chkBox.Name, chkBox.IsChecked))
            End If
        Next
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/EPconfig.xml", FileMode.Create, isoStore)
                doc.Save(isoStream)
            End Using
        End Using
    End Sub
    Sub saveConfig()
        Dim doc As XDocument = XDocument.Parse("<config></config>")


        doc.Element("config").Add(New XElement("CharacterWithGear", cmbGearSelector.SelectedValue))
        doc.Element("config").Add(New XElement("template", cmbTemplate.SelectedValue))

        doc.Element("config").Add(New XElement("mode", "priority"))

        doc.Element("config").Add(New XElement("intro", cmbIntro.SelectedValue))
        doc.Element("config").Add(New XElement("priority", cmbPrio.SelectedValue))
        doc.Element("config").Add(New XElement("rotation", ""))
        doc.Element("config").Add(New XElement("presence", cmdPresence.SelectedValue))
        doc.Element("config").Add(New XElement("sigil", cmbSigils.SelectedValue))
        doc.Element("config").Add(New XElement("mh", cmbRuneMH.SelectedValue))
        doc.Element("config").Add(New XElement("oh", cmbRuneOH.SelectedValue))
        doc.Element("config").Add(New XElement("scenario", cmbScenario.SelectedValue))
        doc.Element("config").Add(New XElement("latency", txtLatency.Text))
        doc.Element("config").Add(New XElement("simtime", txtSimtime.Text))

        doc.Element("config").Add(New XElement("BSTTL", txtBSTTL.Text))
        doc.Element("config").Add(New XElement("log", chkCombatLog.IsChecked))
        doc.Element("config").Add(New XElement("logdetail", ckLogRP.IsChecked))
        doc.Element("config").Add(New XElement("ShowProc", chkShowProc.IsChecked))
        doc.Element("config").Add(New XElement("WaitFC", chkWaitFC.IsChecked))
        doc.Element("config").Add(New XElement("pet", ckPet.IsChecked))
        'Patch
        'doc.Element("config").Add(New XElement("Patch", chkPatch.isChecked))

        doc.Element("config").Add(New XElement("BloodSync", False))
        doc.Element("config").Add(New XElement("chkMergeReport", chkMergeReport.IsChecked))
        doc.Element("config").Add(New XElement("BShOption", cmbBShOption.SelectedValue))
        doc.Element("config").Add(New XElement("ICCBuff", cmbICCBuff.SelectedValue))

        doc.Element("config").Add(New XElement("txtAMSrp", txtAMSrp.Text))
        doc.Element("config").Add(New XElement("txtAMScd", txtAMScd.Text))
        doc.Element("config").Add(New XElement("txtReportName", ""))
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/config.xml", FileMode.Create, isoStore)
                doc.Save(isoStream)
            End Using
        End Using


    End Sub
    Private Sub Page_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Dim f As String
            For Each f In isoStore.GetFileNames
                System.Diagnostics.Debug.WriteLine(f)
            Next
        End Using
        SimOptReducer.Reduce()
        ReportOptReducer.Reduce()

        ConstrucFileDir()


        'Me..Text = "Kahorie's DK Simulator " & Application.ProductVersion
        LoadTrinket()
        loadWindow()
        loadConfig()
        LoadEPOptions()
        LoadBuffOption()
        LoadScaling()
        LoadTankOptions()
        LoadDB()

        initReport()
        'Randomize()

        'InitCharacterPanel()
        '_MainFrm = Me
    End Sub
    Sub ConstrucFileDir()
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            isoStore.CreateDirectory("KahoDKSim")
            'isoStore.CreateDirectory("KahoDKSim/Config")
            'CopyFileFromXAPtoISF("config/PrioritiesList.xml")
            'CopyFileFromXAPtoISF("config/RotationList.xml")
            'CopyFileFromXAPtoISF("config/Scenarios.xml")
            'CopyFileFromXAPtoISF("config/SetBonusList.xml")
            'CopyFileFromXAPtoISF("config/TrinketList.xml")
            'CopyFileFromXAPtoISF("config/WeaponProcList.xml")

            'isoStore.CreateDirectory("KahoDKSim/Characters")
            'CopyFileFromXAPtoISF("Characters/2h ICC.xml")
            'CopyFileFromXAPtoISF("Characters/DW ICC.xml")

            isoStore.CreateDirectory("KahoDKSim/CharactersWithGear")
            CopyFileFromXAPtoISF("CharactersWithGear/Empty.xml")


            isoStore.CreateDirectory("KahoDKSim/CombatLog")
            CopyFileFromXAPtoISF("CombatLog/DoNotDelete.txt")

            isoStore.CreateDirectory("KahoDKSim/Intro")
            CopyFileFromXAPtoISF("Intro/Intro.xml")
            CopyFileFromXAPtoISF("Intro/NoIntro.xml")

            isoStore.CreateDirectory("KahoDKSim/Priority")
            CopyFileFromXAPtoISF("Priority/Blood.xml")
            CopyFileFromXAPtoISF("Priority/Frost.xml")
            CopyFileFromXAPtoISF("Priority/Unholy.xml")

            isoStore.CreateDirectory("KahoDKSim/Rotation")
            CopyFileFromXAPtoISF("Rotation/Blood.xml")
            CopyFileFromXAPtoISF("Rotation/Unholy-Reaping.xml")
            CopyFileFromXAPtoISF("Rotation/Unholy-ReapingLess.xml")

            isoStore.CreateDirectory("KahoDKSim/scenario")
            CopyFileFromXAPtoISF("scenario/Anub.xml")
            CopyFileFromXAPtoISF("scenario/Blood-Queen Lana'thel.xml")
            CopyFileFromXAPtoISF("scenario/Scenario.xml")

            isoStore.CreateDirectory("KahoDKSim/Templates")
            CopyFileFromXAPtoISF("Templates/Empty.xml")

            isoStore.CreateDirectory("KahoDKSim/Report")
            'For Each f In isoStore.GetFileNames
            '    System.Diagnostics.Debug.WriteLine(f)
            'Next
        End Using
    End Sub
    Sub LoadEPOptions()
        Try


            Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
                Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/EPconfig.xml", FileMode.Open, isoStore)
                    Dim doc As XDocument = XDocument.Load(isoStream)


                    Dim ctrl As Control
                    Dim chkBox As CheckBox

                    For Each ctrl In grpEPMain.Children
                        Try
                            If ctrl.Name.StartsWith("chkEP") Then
                                chkBox = ctrl
                                chkBox.IsChecked = doc.Element("config").Element("Stats").Element(chkBox.Name).Value
                            End If
                        Catch ex As Exception
                            Log.Log(ex.StackTrace, logging.Level.ERR)
                        End Try

                    Next
                    For Each ctrl In grpEPSet.Children
                        Try


                            If ctrl.Name.StartsWith("chkEP") Then
                                chkBox = ctrl
                                chkBox.IsChecked = doc.Element("config").Element("Sets").Element(chkBox.Name).Value
                            End If
                        Catch ex As Exception
                            Log.Log(ex.StackTrace, logging.Level.ERR)

                        End Try
                    Next

                    'For Each ctrl In grpEPTrinkets.Children
                    '    Try
                    '        If ctrl.Name.StartsWith("chkEP") Then
                    '            chkBox = ctrl
                    '            chkBox.IsChecked = doc.Element("config").Element("Trinket").Element(chkBox.Name).Value
                    '        End If
                    '    Catch ex As Exception

                    '    End Try

                    'Next
                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub
    Sub loadConfig()
        'On Error Resume Next
        Try


            Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
                Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/config.xml", FileMode.Open, isoStore)
                    Dim doc As XDocument = XDocument.Load(isoStream)
                    isoStream.Close()
                    cmbGearSelector.SelectedValue = doc.Element("config").Element("CharacterWithGear").Value
                    cmbTemplate.SelectedValue = doc.Element("config").Element("template").Value

                    cmbPrio.IsEnabled = True
                    cmbIntro.SelectedValue = doc.Element("config").Element("intro").Value
                    cmbPrio.SelectedValue = doc.Element("config").Element("priority").Value

                    cmdPresence.SelectedValue = doc.Element("config").Element("presence").Value
                    cmbSigils.SelectedValue = doc.Element("config").Element("sigil").Value
                    cmbRuneMH.SelectedValue = doc.Element("config").Element("mh").Value
                    cmbRuneOH.SelectedValue = doc.Element("config").Element("oh").Value
                    cmbScenario.SelectedValue = doc.Element("config").Element("scenario").Value

                    cmbBShOption.SelectedItem = doc.Element("config").Element("BShOption").Value
                    cmbICCBuff.SelectedItem = doc.Element("config").Element("ICCBuff").Value
                    txtLatency.Text = doc.Element("config").Element("latency").Value
                    txtBSTTL.Text = doc.Element("config").Element("BSTTL").Value
                    txtSimtime.Text = doc.Element("config").Element("simtime").Value
                    chkCombatLog.IsChecked = doc.Element("config").Element("log").Value
                    ckLogRP.IsChecked = doc.Element("config").Element("logdetail").Value
                    chkShowProc.IsChecked = doc.Element("config").Element("ShowProc").Value
                    chkWaitFC.IsChecked = doc.Element("config").Element("WaitFC").Value
                    'chkPatch.Ischecked = doc.Element("config").Element("Patch").Value
                    ckPet.IsChecked = doc.Element("config").Element("pet").Value
                    txtAMSrp.Text = doc.Element("config").Element("txtAMSrp").Value
                    txtAMScd.Text = doc.Element("config").Element("txtAMScd").Value
                    chkMergeReport.IsChecked = doc.Element("config").Element("chkMergeReport").Value
                    'txtReportName.Text = doc.Element("config").Element("txtReportName").Value
                    'chkBloodSync.IsChecked = doc.Element("config").Element("BloodSync").Value
                End Using

            End Using
        Catch ex As Exception
            Log.Log(ex.StackTrace, logging.Level.ERR)
            LoadDefaultConfig()
        End Try
    End Sub
    Sub LoadDefaultConfig()
        cmbGearSelector.SelectedValue = "Empty.xml"
        cmbTemplate.SelectedValue = "Empty.xml"
        cmbIntro.SelectedValue = "NoIntro.xml"
        cmbPrio.SelectedValue = "Unholy.xml"
        cmdPresence.SelectedValue = "Frost"
        cmbSigils.SelectedValue = "Virulence"
        cmbRuneMH.SelectedValue = "FallenCrusader"
        cmbRuneOH.SelectedValue = "Razorice"
        cmbScenario.SelectedValue = "Scenario.xml"

    End Sub


    Sub LoadBuffOption()
        On Error GoTo sortie
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Buffconfig.xml", FileMode.Open, isoStore)
                Dim doc As XDocument = XDocument.Load(isoStream)
                Dim ctrl As Control
                Dim chkBox As CheckBox
                For Each ctrl In GrpBuff.Children
                    If ctrl.Name.StartsWith("chk") Then
                        chkBox = ctrl
                        chkBox.IsChecked = doc.Element("config").Element(chkBox.Name).Value
                    End If
                Next

            End Using
        End Using
sortie:




    End Sub
    Sub LoadTrinket()
        Dim xDoc As New Xml.Linq.XDocument()
        Dim doc As XDocument = XDocument.Load("config/TrinketList.xml")
        Dim item As Control
        For Each item In grpEPTrinkets.Children
            item = Nothing
        Next
        grpEPTrinkets.Children.Clear()
        For Each xNode In doc.Descendants
            Dim ckTrinket As New CheckBox
            Try
                ckTrinket.Name = "chkEP" & xNode.Name.ToString
                ckTrinket.Content = xNode.Name
                ckTrinket.Height = 20
                ckTrinket.Width = 180
                grpEPTrinkets.Children.Add(ckTrinket)
                Canvas.SetLeft(ckTrinket, 10)
                Canvas.SetTop(ckTrinket, -10 + grpEPTrinkets.Children.Count * 20)
                grpEPTrinkets.Height = (1 + grpEPTrinkets.Children.Count) * 20
            Catch ex As Exception
                Log.Log(ex.StackTrace, logging.Level.ERR)
                System.Diagnostics.Debug.WriteLine("Err:" & xNode.Name.ToString)
            End Try
        Next
    End Sub
    Sub loadWindow()
        Dim item As String
        Dim sTemp As String


        sTemp = cmbGearSelector.SelectedItem
        cmbGearSelector.Items.Clear()
        If (isoStore.DirectoryExists("KahoDKSim/CharactersWithGear")) Then
            For Each item In isoStore.GetFileNames("KahoDKSim/CharactersWithGear/*.xml")
                cmbGearSelector.Items.Add(Strings.Right(item, item.Length - InStrRev(item, "\")))
            Next
            cmbGearSelector.SelectedItem = sTemp
        Else
            isoStore.CreateDirectory("KahoDKSim/CharactersWithGear")
        End If








        sTemp = cmdPresence.SelectedItem
        cmdPresence.Items.Clear()
        cmdPresence.Items.Add("Blood")
        cmdPresence.Items.Add("Unholy")
        cmdPresence.Items.Add("Frost")
        cmdPresence.SelectedItem = sTemp

        sTemp = cmbSigils.SelectedItem
        cmbSigils.Items.Clear()
        cmbSigils.Items.Add("None")
        cmbSigils.Items.Add("WildBuck")
        cmbSigils.Items.Add("FrozenConscience")
        cmbSigils.Items.Add("DarkRider")
        cmbSigils.Items.Add("ArthriticBinding")
        cmbSigils.Items.Add("Awareness")
        cmbSigils.Items.Add("Strife")
        cmbSigils.Items.Add("HauntedDreams")
        cmbSigils.Items.Add("VengefulHeart")
        cmbSigils.Items.Add("Virulence")
        cmbSigils.Items.Add("HangedMan")
        cmbSigils.SelectedItem = sTemp
        'cmbSigils.Sorted=true

        sTemp = cmbRuneMH.SelectedItem
        cmbRuneMH.Items.Clear()
        cmbRuneMH.Items.Add("None")
        cmbRuneMH.Items.Add("Cinderglacier")
        cmbRuneMH.Items.Add("Razorice")
        cmbRuneMH.Items.Add("FallenCrusader")
        cmbRuneMH.SelectedItem = sTemp

        sTemp = cmbRuneOH.SelectedItem
        cmbRuneOH.Items.Clear()
        cmbRuneOH.Items.Add("None")
        cmbRuneOH.Items.Add("Cinderglacier")
        cmbRuneOH.Items.Add("Razorice")
        cmbRuneOH.Items.Add("FallenCrusader")
        cmbRuneOH.Items.Add("Berserking")
        cmbRuneOH.SelectedItem = sTemp

        sTemp = cmbBShOption.SelectedItem
        cmbBShOption.Items.Clear()
        cmbBShOption.Items.Add("Instead of Blood Strike")
        cmbBShOption.Items.Add("Instead of Blood Boil")
        cmbBShOption.Items.Add("After BS/BB")
        cmbBShOption.Items.Add("After Death rune OB/SS with cancel aura")
        cmbBShOption.SelectedItem = sTemp

        sTemp = cmbICCBuff.SelectedItem
        cmbICCBuff.Items.Clear()
        cmbICCBuff.Items.Add("0")
        cmbICCBuff.Items.Add("5")
        cmbICCBuff.Items.Add("10")
        cmbICCBuff.Items.Add("15")
        cmbICCBuff.Items.Add("20")
        cmbICCBuff.Items.Add("25")
        cmbICCBuff.Items.Add("30")
        cmbICCBuff.SelectedItem = sTemp


        RefreshTemplateList()
        RefreshPrioList()
        RefreshScenarioList()
        'SimConstructor.PetFriendly = Me.ckPet.Ischecked
        'LockSaveButtons()

    End Sub
    Sub LoadScaling()
        On Error GoTo OUT

        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Scalingconfig.xml", FileMode.Open, isoStore)
                Dim doc As XDocument = XDocument.Load(isoStream)

                Dim ctrl As Control
                Dim chkBox As CheckBox
                For Each ctrl In gbScaling.Children
                    If ctrl.Name.StartsWith("chkSca") Then
                        chkBox = ctrl
                        chkBox.IsChecked = doc.Element("config").Element("Stats").Element(chkBox.Name).Value
                    End If
                Next



            End Using
        End Using


OUT:
    End Sub
    Sub LoadTankOptions()
        On Error GoTo OUT
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/TankConfig.xml", FileMode.Open, isoStore)
                Dim doc As XDocument = XDocument.Load(isoStream)
                Dim ctrl As Control
                Dim txtBox As TextBox
                For Each ctrl In gbTank.Children
                    If ctrl.Name.StartsWith("txt") Then
                        txtBox = ctrl
                        txtBox.Text = doc.Element("config").Element("Stats").Element(txtBox.Name).Value
                    End If
                Next
            End Using
        End Using

OUT:
    End Sub

    Sub LoadDB()

        ItemDB = XDocument.Load("GearSelector/ItemDB.xml")
        GemDB = XDocument.Load("GearSelector/gems.xml")
        GemBonusDB = XDocument.Load("GearSelector/GemBonus.xml")
        EnchantDB = XDocument.Load("GearSelector/Enchant.xml")
        trinketDB = XDocument.Load("GearSelector/TrinketList.xml")
        SetBonusDB = XDocument.Load("GearSelector/SetBonus.xml")
        WeapProcDB = XDocument.Load("GearSelector/WeaponProcList.xml")
        FoodDB = XDocument.Load("GearSelector/Food.xml")
        FlaskDB = XDocument.Load("GearSelector/Flask.xml")
        ConsumableDB = XDocument.Load("GearSelector/Consumables.xml")
    End Sub
    Sub CopyFileFromXAPtoISF(ByVal XAPPAth As String)
        Try


            Dim sr As StreamResourceInfo = Application.GetResourceStream(New Uri(XAPPAth, UriKind.Relative))
            Using fileStream As IO.Stream = sr.Stream
                Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
                    Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/" & XAPPAth, FileMode.Create, FileAccess.Write, isoStore)
                        Do
                            Dim buffer(100000) As Byte
                            Dim count As Integer = fileStream.Read(buffer, 0, buffer.Length)
                            If count > 0 Then
                                isoStream.Write(buffer, 0, count)
                            Else
                                Exit Do
                            End If
                        Loop
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Log.Log(ex.StackTrace, logging.Level.ERR)

        End Try


    End Sub


    Private Sub rCharacter_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

    End Sub


    Delegate Sub UpdateProgressBar_Delegate()
    Friend Sub TryToUpdateProgressBar()
        Try
            Dim MyDelegate As New UpdateProgressBar_Delegate(AddressOf UpdateProgressBar)
            ProgressBar1.Dispatcher.BeginInvoke(MyDelegate)
        Catch ex As Exception
            Diagnostics.Debug.WriteLine(ex.StackTrace)
        End Try

    End Sub

    'Shared ProgressBar1 As ProgressBar

    Friend Sub UpdateProgressBar()
        Dim s As Sim
        Dim i As Double
        Try


            'RefreshRequest += 1
            'On Error Resume Next
            If SimConstructor.simCollection.Count = 0 Then
                ProgressBar1.Value = 0
                Exit Sub
            End If
            i = 0
            For Each s In SimConstructor.simCollection
                If s.MaxTime <> 0 Then
                    i += (s.TimeStamp / s.MaxTime) / SimConstructor.simCollection.Count
                Else
                    i += 0
                End If
            Next
            i = i * 100
            ProgressBar1.Value = i
        Catch ex As Exception
            Diagnostics.Debug.WriteLine(ex.StackTrace)
        End Try
        'Diagnostics.Debug.WriteLine(RefreshRequest)
    End Sub

    Sub UpdateProgressBars()
        Dim s As Sim
        Dim i As Double
        'On Error Resume Next
        If SimConstructor.simCollection.Count = 0 Then
            Me.ProgressBar1.Value = 0
            Exit Sub
        End If
        i = 0
        For Each s In SimConstructor.simCollection
            If s.MaxTime <> 0 Then
                i += (s.TimeStamp / s.MaxTime) / SimConstructor.simCollection.Count
            Else
                i += 0
            End If
        Next
        i = i * 100
        Me.ProgressBar1.Value = i
    End Sub
    Sub RefreshPrioList()

        Dim sTemp As String = cmbIntro.SelectedItem
        cmbIntro.Items.Clear()
        If (isoStore.DirectoryExists("KahoDKSim/Intro")) Then
            For Each item In isoStore.GetFileNames("KahoDKSim/Intro/*.xml")
                cmbIntro.Items.Add(Strings.Right(item, item.Length - InStrRev(item, "\")))
            Next
            cmbIntro.SelectedItem = sTemp
        Else
            isoStore.CreateDirectory("KahoDKSim/Intro")
        End If



        sTemp = cmbPrio.SelectedItem
        cmbPrio.Items.Clear()

        If (isoStore.DirectoryExists("KahoDKSim/Priority")) Then
            For Each item In isoStore.GetFileNames("KahoDKSim/Priority/*")
                cmbPrio.Items.Add(Strings.Right(item, item.Length - InStrRev(item, "\")))
            Next
            cmbPrio.SelectedItem = sTemp
        Else
            isoStore.CreateDirectory("KahoDKSim/Priority")
        End If





        If (isoStore.DirectoryExists("KahoDKSim/Rotation")) Then
        Else
            isoStore.CreateDirectory("KahoDKSim/Rotation")
        End If
    End Sub

    Sub RefreshScenarioList()
        Dim isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        Dim item As String
        Dim sTemp As String = ""
        Try
            sTemp = cmbScenario.SelectedValue
        Catch
        End Try
        cmbScenario.Items.Clear()
        If (isoStore.DirectoryExists("KahoDKSim/Priority")) Then
            For Each item In isoStore.GetFileNames("KahoDKSim/scenario/*")
                cmbScenario.Items.Add(Strings.Right(item, item.Length - InStrRev(item, "\")))
            Next
            cmbScenario.SelectedItem = sTemp
        Else
            isoStore.CreateDirectory("KahoDKSim/scenario")
        End If
        cmbScenario.SelectedItem = sTemp

    End Sub
    Sub RefreshTemplateList()

        Dim sTemp As String = cmbTemplate.SelectedItem
        cmbTemplate.Items.Clear()
        If (isoStore.DirectoryExists("KahoDKSim/Templates")) Then
            For Each item In isoStore.GetFileNames("KahoDKSim/Templates/*.xml")
                cmbTemplate.Items.Add(Strings.Right(item, item.Length - InStrRev(item, "\")))
            Next
            cmbTemplate.SelectedItem = sTemp
        Else
            isoStore.CreateDirectory("KahoDKSim/Templates")
        End If
    End Sub
    Private Sub cmdEditTemplate_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdEditTemplate.Click
        TEdit.DisplayTemplateInEditor(cmbTemplate.SelectedValue.ToString)
        TEdit.Show()
    End Sub

    Private Sub TEdit_Close() Handles TEdit.Closing
        RefreshTemplateList()
        cmbTemplate.SelectedValue = TEdit.FilePath
    End Sub

    Private Sub cmdEditRota_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        PrioEditor.EditType = PriorityEditor.PossibleEditType.Rotation
        PrioEditor.FilePath = ""
        PrioEditor.LoadAvailableElemnt()
        PrioEditor.OpenRotaForEdit(PrioEditor.FilePath)
        PrioEditor.Show()

    End Sub

    Private Sub cmdEditPrio_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdEditPrio.Click
        PrioEditor.EditType = PriorityEditor.PossibleEditType.Priority
        PrioEditor.FilePath = cmbPrio.SelectedValue
        PrioEditor.LoadAvailableElemnt()
        PrioEditor.OpenPrioForEdit(PrioEditor.FilePath)
        PrioEditor.Show()
    End Sub

    Private Sub cmdEditIntro_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdEditIntro.Click
        PrioEditor.EditType = PriorityEditor.PossibleEditType.Intro
        PrioEditor.FilePath = cmbIntro.SelectedValue
        PrioEditor.LoadAvailableElemnt()
        PrioEditor.OpenIntroForEdit(PrioEditor.FilePath)
        PrioEditor.Show()
    End Sub
    Private Sub PrioEditor_CLose() Handles PrioEditor.Closing
        RefreshPrioList()
        Select Case PrioEditor.EditType
            Case PriorityEditor.PossibleEditType.Intro
                cmbIntro.SelectedValue = PrioEditor.FilePath
            Case PriorityEditor.PossibleEditType.Priority
                cmbPrio.SelectedValue = PrioEditor.FilePath
            Case PriorityEditor.PossibleEditType.Rotation

        End Select


    End Sub

    Private Sub cmdEditCharacterWithGear_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdEditCharacterWithGear.Click
        If GearSelector Is Nothing Then
            GearSelector = New GearSelectorMainForm(Me)
        End If

        Try
            GearSelector.FilePath = cmbGearSelector.SelectedValue
            GearSelector.Show()
            GearSelector.LoadMycharacter()
        Catch Err As Exception


        End Try
    End Sub

    Sub GearSelector_close() Handles GearSelector.Closing
        loadWindow()
        cmbGearSelector.SelectedItem = GearSelector.FilePath
    End Sub

    Private Sub cmdStartSim_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdStartSim.Click
        If LoadBeforeSim() = False Then Exit Sub
        If SimConstructor.simCollection.Count > 0 Then Return
        EpStat = ""
        SimConstructor.Start(txtSimtime.Text, Me, True)
    End Sub

    Private Sub cmdEditScenario_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdEditScenario.Click
        If ScenarioEditor Is Nothing Then
            ScenarioEditor = New ScenarioEditor(Me)
        End If
        Try
            ScenarioEditor.OpenForEdit(cmbScenario.SelectedValue)
            ScenarioEditor.Show()

        Catch Err As Exception


        End Try
    End Sub
    Sub ScenarioEditor_Close() Handles ScenarioEditor.Closing
        RefreshScenarioList()
        cmbScenario.SelectedValue = ScenarioEditor.EditorFilePAth
    End Sub
    Delegate Sub OpenReport_Delegate()
    Friend Sub TryToOpenReport()
        Try
            Dim MyDelegate As New OpenReport_Delegate(AddressOf OpenReport)
            Me.Dispatcher.BeginInvoke(MyDelegate)
        Catch ex As Exception
            Diagnostics.Debug.WriteLine(ex.StackTrace)
        End Try

    End Sub

    Sub OpenReport()
        Dim txEdit As New ReportFrame
        txEdit.OpenReport("KahoDKSim/Report/Report.xml")
        ReportStack.Children.Add(txEdit)
        Me.TabReport.IsSelected = True
    End Sub

    Private Sub cmdReport_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdReport.Click
        OpenReport()
    End Sub



    Delegate Sub OpenTextReport_Delegate()
    Friend Sub TryToOpenTextReport()
        Try
            Dim MyDelegate As New OpenTextReport_Delegate(AddressOf OpenReport)
            Me.Dispatcher.BeginInvoke(MyDelegate)
        Catch ex As Exception
            Diagnostics.Debug.WriteLine(ex.StackTrace)
        End Try

    End Sub

    Private Sub cmdShowLog_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdShowLog.Click
        Dim Repfrm As New CombatLogViewer
        Repfrm.Show()
        'Dim txtEdi As New TextEditor
        'txtEdi.OpenFileFromISO("/KahoDKSim/CombatLog/Combatlog.txt")
        'txtEdi.Show()
    End Sub
End Class
