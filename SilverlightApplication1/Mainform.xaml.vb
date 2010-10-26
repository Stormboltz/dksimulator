Imports System.Xml.Linq
Imports System.Linq
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
    Private WithEvents ScenarioEditor As ScenarioEditor

    Private WithEvents frmStatSummaryWithEvent As frmStatSummary



    Public Sub New()
        InitializeComponent()


        'For some bizarre reason we need to tell the BackgoundWorker that we will be
        'reporting progress!

        ProgressBar1.Maximum = 100

        ' Wire up an event handler to respond to progress changes during the operation.
        'Let the BackgoundWorker know what operation to call when it's kicked off.
    End Sub

    Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles btEP.Click
        If SimConstructor.simCollection.Count > 0 Then Return
        If LoadBeforeSim() = False Then Exit Sub
        SimConstructor.StartEP(txtSimtime.Text, Me)
    End Sub

    Function LoadBeforeSim()
        saveConfig()
        SaveEPOptions()
        SaveBuffOption()
        saveScaling()
        saveTankOptions()
        SaveMycharacter()
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
        For Each ctrl As Control In grpEPTrinkets.Children
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
        doc.Element("config").Add(New XElement("lvl85", level85))
        doc.Element("config").Add(New XElement("mode", "priority"))

        doc.Element("config").Add(New XElement("intro", cmbIntro.SelectedValue))
        doc.Element("config").Add(New XElement("priority", cmbPrio.SelectedValue))
        doc.Element("config").Add(New XElement("rotation", ""))
        doc.Element("config").Add(New XElement("presence", cmdPresence.SelectedValue))
        doc.Element("config").Add(New XElement("sigil", ""))
        doc.Element("config").Add(New XElement("mh", ""))
        doc.Element("config").Add(New XElement("oh", ""))
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
        frmStatSummaryWithEvent = Me.StatSummary
        ConstrucFileDir()
        Dim ass As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly

        If ass.FullName <> "" Then
            Dim parts As String() = ass.FullName.Split(",")
            Me.Title = "Kahorie's DK Simulator " & parts(1)
        End If




        'lblApplication.Content = "Kahorie's DK Simulator " & App.Current.
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
            CopyFileFromXAPtoISF("CharactersWithGear/Kahorie.xml", True)

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

                    For Each ctrl In grpEPTrinkets.Children
                        Try
                            If ctrl.Name.StartsWith("chkEP") Then
                                chkBox = ctrl
                                chkBox.IsChecked = doc.Element("config").Element("Trinket").Element(chkBox.Name).Value
                            End If
                        Catch ex As Exception

                        End Try

                    Next
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
                    cmbGearSelector.SelectedValue = doc.Element("config").<CharacterWithGear>.Value
                    cmbTemplate.SelectedValue = doc.Element("config").<template>.Value
                    level85 = doc.Element("config").<lvl85>.Value
                    If level85 Then btlvl85.Content = "Set to level 80"
                    cmbPrio.IsEnabled = True
                    cmbIntro.SelectedValue = doc.Element("config").<intro>.Value
                    cmbPrio.SelectedValue = doc.Element("config").<priority>.Value

                    cmdPresence.SelectedValue = doc.Element("config").<presence>.Value

                    cmbScenario.SelectedValue = doc.Element("config").<scenario>.Value

                    cmbBShOption.SelectedItem = doc.Element("config").<BShOption>.Value
                    cmbICCBuff.SelectedItem = doc.Element("config").<ICCBuff>.Value
                    txtLatency.Text = doc.Element("config").<latency>.Value
                    txtBSTTL.Text = doc.Element("config").<BSTTL>.Value
                    txtSimtime.Text = doc.Element("config").<simtime>.Value
                    chkCombatLog.IsChecked = doc.Element("config").<log>.Value
                    ckLogRP.IsChecked = doc.Element("config").<logdetail>.Value
                    chkShowProc.IsChecked = doc.Element("config").<ShowProc>.Value
                    chkWaitFC.IsChecked = doc.Element("config").<WaitFC>.Value
                    'chkPatch.Ischecked = doc.Element("config").<Patch>.Value
                    ckPet.IsChecked = doc.Element("config").<pet>.Value
                    txtAMSrp.Text = doc.Element("config").<txtAMSrp>.Value
                    txtAMScd.Text = doc.Element("config").<txtAMScd>.Value
                    chkMergeReport.IsChecked = doc.Element("config").<chkMergeReport>.Value
                    'txtReportName.Text = doc.Element("config").<txtReportName>.Value
                    'chkBloodSync.IsChecked = doc.Element("config").<BloodSync>.Value
                End Using

            End Using
        Catch ex As Exception
            Log.Log(ex.StackTrace, logging.Level.ERR)
            LoadDefaultConfig()
        End Try
        Try
            cmdEditCharacterWithGear_Click(Nothing, Nothing)
        Catch ex As Exception

        End Try

    End Sub
    Sub LoadDefaultConfig()
        cmbGearSelector.SelectedValue = "Empty.xml"
        cmbTemplate.SelectedValue = "Empty.xml"
        cmbIntro.SelectedValue = "NoIntro.xml"
        cmbPrio.SelectedValue = "Unholy.xml"
        cmdPresence.SelectedValue = "Frost"

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
        For Each xNode In doc.<TrinketList>.Elements
            Try
                Dim tkName As String = "chkEP" & xNode.Attribute("name").Value
                For Each c In grpEPTrinkets.Children
                    If CType(c, CheckBox).Name = tkName Then GoTo NextTrinket
                Next
                Dim ckTrinket As New CheckBox
                ckTrinket.Name = tkName
                ckTrinket.Content = xNode.Attribute("name").Value
                grpEPTrinkets.Children.Add(ckTrinket)
            Catch ex As Exception
                Log.Log(ex.StackTrace, logging.Level.ERR)
                System.Diagnostics.Debug.WriteLine("Err:" & xNode.Name.ToString)

            End Try
NextTrinket:
        Next
    End Sub

    Sub RefreshCharacterList(Optional ByVal NewValue As String = "")

        RemoveHandler cmbGearSelector.SelectionChanged, AddressOf cmbGearSelector_SelectionChanged


        Dim sTemp As String = ""
        cmbGearSelector.Items.Clear()
        If (isoStore.DirectoryExists("KahoDKSim/CharactersWithGear")) Then
            For Each item In isoStore.GetFileNames("KahoDKSim/CharactersWithGear/*.xml")
                cmbGearSelector.Items.Add(Strings.Right(item, item.Length - InStrRev(item, "\")))
            Next
            If NewValue <> "" Then
                Try
                    cmbGearSelector.SelectedItem = NewValue
                Catch ex As Exception
                    cmbGearSelector.SelectedItem = sTemp
                End Try
            Else
                cmbGearSelector.SelectedItem = sTemp
            End If
        Else
            isoStore.CreateDirectory("KahoDKSim/CharactersWithGear")
        End If
        AddHandler cmbGearSelector.SelectionChanged, AddressOf cmbGearSelector_SelectionChanged
    End Sub

    Sub loadWindow()
        Dim item As String
        Dim sTemp As String = ""
        CharacterGrid.Children.Clear()
        If GearSelector Is Nothing Then
            GearSelector = New FrmGearSelector(Me)
        End If
        CharacterGrid.Children.Add(GearSelector)
        If IsNothing(cmbGearSelector.SelectedItem) Then sTemp = cmbGearSelector.SelectedItem
        cmbGearSelector.Items.Clear()
        If (isoStore.DirectoryExists("KahoDKSim/CharactersWithGear")) Then
            For Each item In isoStore.GetFileNames("KahoDKSim/CharactersWithGear/*.xml")
                cmbGearSelector.Items.Add(Strings.Right(item, item.Length - InStrRev(item, "\")))
            Next
            cmbGearSelector.SelectedItem = sTemp
        Else
            isoStore.CreateDirectory("KahoDKSim/CharactersWithGear")
        End If


        sTemp = ""


        If IsNothing(cmdPresence.SelectedItem) Then sTemp = cmdPresence.SelectedItem




        cmdPresence.Items.Clear()
        cmdPresence.Items.Add("Blood")
        cmdPresence.Items.Add("Unholy")
        cmdPresence.Items.Add("Frost")
        cmdPresence.SelectedItem = sTemp




        sTemp = ""
        If IsNothing(cmbBShOption.SelectedItem) Then sTemp = cmbBShOption.SelectedItem

        cmbBShOption.Items.Clear()
        cmbBShOption.Items.Add("Instead of Blood Strike")
        cmbBShOption.Items.Add("Instead of Blood Boil")
        cmbBShOption.Items.Add("After BS/BB")
        cmbBShOption.Items.Add("After Death rune OB/SS with cancel aura")
        cmbBShOption.SelectedItem = sTemp

        sTemp = ""
        If IsNothing(cmbICCBuff.SelectedItem) Then sTemp = cmbICCBuff.SelectedItem

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
        'SwitchMode()
        Me.TabControl1.SelectedIndex = 0
    End Sub
    Sub LoadScaling()
        'On Error GoTo OUT

        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            If isoStore.FileExists("KahoDKSim/Scalingconfig.xml") Then
                Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Scalingconfig.xml", FileMode.Open, isoStore)
                    Dim doc As XDocument = XDocument.Load(isoStream)
                    Dim chkBox As CheckBox


                    For Each stk In gbScaling.Children
                        If TypeOf (stk) Is StackPanel Then
                            For Each ctrl As Control In CType(stk, StackPanel).Children
                                If ctrl.Name.StartsWith("chk") Then
                                    chkBox = ctrl
                                    Try
                                        chkBox.IsChecked = doc.<config>.<Stats>.Elements(chkBox.Name).Value
                                    Catch ex As Exception
                                    End Try
                                End If
                            Next
                        End If
                    Next
                End Using
            End If
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
    Sub CopyFileFromXAPtoISF(ByVal XAPPAth As String, Optional ByVal overWrite As Boolean = False)
        Try


            Dim sr As StreamResourceInfo = Application.GetResourceStream(New Uri(XAPPAth, UriKind.Relative))

            Using fileStream As IO.Stream = sr.Stream
                Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
                    If isoStore.FileExists("KahoDKSim/" & XAPPAth) And overWrite = False Then Return

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
        Dim s As Simulator.Sim
        Dim i As Double
        Try
            If SimConstructor.simCollection.Count = 0 Then
                ProgressBar1.Value = 0
                Exit Sub
            End If
            i = 0
            For Each s In SimConstructor.simCollection
                If s.MaxTime <> 0 Then
                    i += (s.TimeStamp / s.MaxTime) / SimConstructor.SimCount
                Else
                    i += 0
                End If
            Next
            i += SimConstructor.SimDone / SimConstructor.SimCount
            i = i * 100
            ProgressBar1.Value = i
        Catch ex As Exception
            Diagnostics.Debug.WriteLine(ex.StackTrace)
        End Try
        'Diagnostics.Debug.WriteLine(RefreshRequest)
    End Sub

    Sub UpdateProgressBars()
        Dim s As Simulator.Sim
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

        If GearSelector.ParentFrame Is Nothing Then GearSelector.Init(Me)
        Try
            GearSelector.FilePath = cmbGearSelector.SelectedValue
            GearSelector.LoadMycharacter()
        Catch Err As Exception


        End Try
    End Sub

    Sub GearSelector_close()
        loadWindow()
        cmbGearSelector.SelectedItem = GearSelector.FilePath
    End Sub

    Private Sub cmdStartSim_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdStartSim.Click

        If isoStore.Quota < 24000000 Then
            Dim ret As Boolean = isoStore.IncreaseQuotaTo(25000000)
        End If

        If isoStore.AvailableFreeSpace < 10000 Then
            Dim ret As Boolean = isoStore.IncreaseQuotaTo(isoStore.Quota + 2000000)
        End If

        If LoadBeforeSim() = False Then Exit Sub

        If SimConstructor.simCollection.Count > 0 Then Return
        SimConstructor.SimDone = 0
        SimConstructor.SimCount = 0


        SimConstructor.EpStat = ""
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


        scrollReport.ScrollToBottom()
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
    Sub CmbRaceSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cmbRace.SelectionChanged
        GetStats()
    End Sub
    Sub CmbSkillClick(ByVal sender As Object, ByVal e As EventArgs) Handles cmbSkill1.SelectionChanged, cmbSkill2.SelectionChanged
        Dim eq As VisualEquipSlot
        If IsNothing(GearSelector) Then Exit Sub
        If GearSelector.InLoad Then Exit Sub
        GearSelector.InLoad = True
        For Each eq In GearSelector.EquipmentList
            eq.Item.LoadItem(eq.Item.Id)
            eq.DisplayItem()
        Next
        GearSelector.InLoad = False
        GetStats()
    End Sub

    Sub CmbFlaskSelectionChange(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFlask.SelectionChanged
        If IsNothing(sender.SelectedItem) Then Exit Sub
        Try
            GearSelector.Flask.Attach(cmbFlask.SelectedValue)
        Catch ex As Exception
            Log.Log(ex.StackTrace, logging.Level.ERR)
        End Try
        GetStats()
    End Sub

    Sub cmbFoodSelectionChange(ByVal sender As Object, ByVal e As EventArgs) Handles cmbFood.SelectionChanged
        If IsNothing(sender.SelectedItem) Then Exit Sub
        Try
            GearSelector.Food.Attach(cmbFood.SelectedValue)
            GetStats()
        Catch ex As Exception
            Log.Log(ex.StackTrace, logging.Level.ERR)
        End Try

    End Sub
    Sub GetStats() Handles frmStatSummaryWithEvent.DPS_Stat_changed
        If IsNothing(GearSelector) Then Exit Sub
        If GearSelector.InLoad Then Exit Sub
        If StatSummary.chkManualInput.IsChecked Then GoTo refreshRating
        Dim iSlot As VisualEquipSlot

        Dim Strength As Integer
        Dim Intel As Integer
        Dim Agility As Integer

        Dim Armor As Integer
        Dim HasteRating As Integer
        Dim ExpertiseRating As Integer
        Dim HitRating As Integer
        Dim AttackPower As Integer
        Dim CritRating As Integer

        Dim Stamina As Integer
        Dim BonusArmor As Integer
        Dim MasteryRating As Integer
        Dim DodgeRating As Integer
        Dim ParryRating As Integer

        Dim ArmorPenetrationRating As Integer
        Dim Speed1 As String = "0"
        Dim Speed2 As String = "0"
        Dim DPS1 As String = "0"
        Dim DPS2 As String = "0"


        StatSummary.chkMeta.IsChecked = False
        StatSummary.chkTailorEnchant.IsChecked = False
        StatSummary.chkIngenieer.IsChecked = False
        StatSummary.chkAccelerators.IsChecked = False
        StatSummary.chkAshenBand.IsChecked = False
        StatSummary.chkBloodFury.IsChecked = False
        StatSummary.chkBerzerking.IsChecked = False
        StatSummary.chkArcaneTorrent.IsChecked = False
        StatSummary.chkDraeni.IsChecked = False
        StatSummary.chkWorgen.IsChecked = False
        StatSummary.chkGoblin.IsChecked = False

        StatSummary.cmbSetBonus1.Text = ""
        StatSummary.cmbSetBonus2.Text = ""

        Dim cSetBonus As New Collections.Generic.List(Of String)
        Select Case GearSelector.ParentFrame.cmbRace.SelectedValue
            Case "Blood Elf"
                Strength = 172
                Agility = 114
                Intel = 39
            Case "Draenei"
                Strength = 176
                Agility = 109
                Intel = 36
            Case "Dwarf"
                Strength = 177
                Agility = 108
                Intel = 34
            Case "Gnome"
                Strength = 170
                Agility = 115
                Intel = 42
            Case "Human"
                Strength = 175
                Agility = 112
                Intel = 35
            Case "Night Elf"
                Strength = 172
                Agility = 117
                Intel = 35
            Case "Orc"
                Strength = 178
                Agility = 109
                Intel = 32
            Case "Tauren"
                Strength = 180
                Agility = 107
                Intel = 30
            Case "Troll"
                Strength = 176
                Agility = 114
                Intel = 31
            Case "Undead"
                Strength = 174
                Agility = 110
                Intel = 33
            Case "Goblin"
                Strength = 174
                Agility = 110
                Intel = 33
            Case "Worgen"
                Strength = 174
                Agility = 110
                Intel = 33
        End Select
        'Food
        Strength += GearSelector.Food.Strength
        Agility += GearSelector.Food.Agility
        HasteRating += GearSelector.Food.HasteRating
        ExpertiseRating += GearSelector.Food.ExpertiseRating
        HitRating += GearSelector.Food.HitRating
        AttackPower += GearSelector.Food.AttackPower
        CritRating += GearSelector.Food.CritRating
        ArmorPenetrationRating += GearSelector.Food.ArmorPenetrationRating

        'Flask
        Strength += GearSelector.Flask.Strength
        Agility += GearSelector.Flask.Agility
        HasteRating += GearSelector.Flask.HasteRating
        ExpertiseRating += GearSelector.Flask.ExpertiseRating
        HitRating += GearSelector.Flask.HitRating
        AttackPower += GearSelector.Flask.AttackPower
        CritRating += GearSelector.Flask.CritRating
        ArmorPenetrationRating += GearSelector.Flask.ArmorPenetrationRating
        Armor += GearSelector.Flask.Armor


        StatSummary.txtMHExpBonus.Text = 0
        StatSummary.txtOHExpBonus.Text = 0

        For Each iSlot In GearSelector.EquipmentList

            If iSlot.Item.Id = 0 Then GoTo NextItem
            If iSlot.SlotId = 17 And StatSummary.rDW.IsChecked Then GoTo NextItem
            If iSlot.SlotId = 13 And StatSummary.r2Hand.IsChecked Then GoTo NextItem

            Dim subc As Integer = iSlot.Item.subclass

            If iSlot.Text.ToString = "TwoHand" Or iSlot.Text.ToString = "MainHand" Then
                DPS1 = iSlot.Item.DPS
                Speed1 = iSlot.Item.Speed
                Try
                    If (From el In WeapProcDB.<WeaponProcList>.<proc>
                            Where (el.@<id> = iSlot.Item.Id)
                            Select el).Count = 0 Then
                        StatSummary.cmbWeaponProc1.Text = ""
                    Else
                        StatSummary.cmbWeaponProc1.Text = "MH" & (
                            From el In WeapProcDB.<WeaponProcList>.<proc>
                            Where (el.@<id> = iSlot.Item.Id)
                            Select el).First.@<name>
                    End If
                Catch ex As Exception
                    Log.Log(ex.StackTrace, logging.Level.ERR)
                    StatSummary.cmbWeaponProc1.Text = ""
                End Try
                Select Case GearSelector.ParentFrame.cmbRace.SelectedValue
                    Case "Dwarf"
                        If subc = 4 Or subc = 5 Then
                            StatSummary.txtMHExpBonus.Text = 5
                        End If
                    Case "Human"
                        If subc = 4 Or subc = 5 Or subc = 7 Or subc = 8 Then
                            StatSummary.txtMHExpBonus.Text = 3
                        End If
                    Case "Orc"
                        If subc = 0 Or subc = 1 Then
                            StatSummary.txtMHExpBonus.Text = 5
                        End If
                End Select



            End If

            If iSlot.Text.ToString = "OffHand" Then
                DPS2 = iSlot.Item.DPS
                Speed2 = iSlot.Item.Speed

                Try
                    If (
                            From el In WeapProcDB.<WeaponProcList>.<proc>
                            Where (el.@<id> = iSlot.Item.Id)
                            Select el).Count = 0 Then
                        StatSummary.cmbWeaponProc2.Text = ""
                    Else
                        StatSummary.cmbWeaponProc2.Text = "OH" & (
                                                From el In WeapProcDB.<WeaponProcList>.<proc>
                                                Where (el.@<id> = iSlot.Item.Id)
                                                Select el).First.@<name>
                    End If

                Catch ex As Exception
                    Log.Log(ex.StackTrace, logging.Level.ERR)
                    StatSummary.cmbWeaponProc1.Text = ""
                End Try

                Select Case GearSelector.ParentFrame.cmbRace.SelectedValue
                    Case "Dwarf"
                        If subc = 4 Or subc = 5 Then
                            StatSummary.txtOHExpBonus.Text = 5
                        End If
                    Case "Human"
                        If subc = 4 Or subc = 5 Or subc = 7 Or subc = 8 Then
                            StatSummary.txtOHExpBonus.Text = 3
                        End If
                    Case "Orc"
                        If subc = 0 Or subc = 1 Then
                            StatSummary.txtOHExpBonus.Text = 5
                        End If
                End Select
            End If
            If iSlot.Item.setid <> 0 Then
                cSetBonus.Add(iSlot.Item.setid)
            End If

            If iSlot.Text.ToString = "Trinket1" Then
                Try
                    StatSummary.cmbTrinket1.Text = (
                        From el In trinketDB.<TrinketList>.<trinket>
                        Where (el.@<id> = iSlot.Item.Id)
                        Select el).FirstOrDefault.@<name>
                Catch ex As Exception
                    Log.Log("No Effect on Trinket1", logging.Level.INFO)
                    StatSummary.cmbTrinket1.Text = ""
                End Try
            End If
            If iSlot.Text.ToString = "Trinket2" Then
                Try
                    StatSummary.cmbTrinket2.Text = (From el In trinketDB.<TrinketList>.<trinket>
                        Where (el.@<id> = iSlot.Item.Id)
                        Select el).FirstOrDefault.@<name>
                Catch ex As Exception
                    Log.Log("No Effect on Trinket1", logging.Level.INFO)
                    StatSummary.cmbTrinket2.Text = ""
                End Try
            End If



            If iSlot.Item.Id <> 0 Then
                Strength += iSlot.Item.Strength
                Agility += iSlot.Item.Agility
                BonusArmor += iSlot.Item.BonusArmor
                Armor += iSlot.Item.Armor
                HasteRating += iSlot.Item.HasteRating
                ExpertiseRating += iSlot.Item.ExpertiseRating
                HitRating += iSlot.Item.HitRating
                AttackPower += iSlot.Item.AttackPower
                CritRating += iSlot.Item.CritRating
                ArmorPenetrationRating += iSlot.Item.ArmorPenetrationRating
                BonusArmor += iSlot.Item.BonusArmor
                Stamina += iSlot.Item.Stamina
                MasteryRating += iSlot.Item.MasteryRating
                DodgeRating += iSlot.Item.DodgeRating
                ParryRating += iSlot.Item.ParryRating


                If iSlot.Item.gem1.Id <> 0 Then
                    Strength += iSlot.Item.gem1.Strength
                    Agility += iSlot.Item.gem1.Agility
                    HasteRating += iSlot.Item.gem1.HasteRating
                    ExpertiseRating += iSlot.Item.gem1.ExpertiseRating
                    HitRating += iSlot.Item.gem1.HitRating
                    AttackPower += iSlot.Item.gem1.AttackPower
                    CritRating += iSlot.Item.gem1.CritRating
                    ArmorPenetrationRating += iSlot.Item.gem1.ArmorPenetrationRating

                    BonusArmor += iSlot.Item.gem1.BonusArmor
                    Stamina += iSlot.Item.gem1.Stamina
                    MasteryRating += iSlot.Item.gem1.MasteryRating
                    DodgeRating += iSlot.Item.gem1.DodgeRating
                    ParryRating += iSlot.Item.gem1.ParryRating


                End If
                If iSlot.Item.gem2.Id <> 0 Then
                    Strength += iSlot.Item.gem2.Strength
                    Agility += iSlot.Item.gem2.Agility
                    HasteRating += iSlot.Item.gem2.HasteRating
                    ExpertiseRating += iSlot.Item.gem2.ExpertiseRating
                    HitRating += iSlot.Item.gem2.HitRating
                    AttackPower += iSlot.Item.gem2.AttackPower
                    CritRating += iSlot.Item.gem2.CritRating
                    ArmorPenetrationRating += iSlot.Item.gem2.ArmorPenetrationRating

                    BonusArmor += iSlot.Item.gem2.BonusArmor
                    Stamina += iSlot.Item.gem2.Stamina
                    MasteryRating += iSlot.Item.gem2.MasteryRating
                    DodgeRating += iSlot.Item.gem2.DodgeRating
                    ParryRating += iSlot.Item.gem2.ParryRating

                End If
                If iSlot.Item.gem3.Id <> 0 Then
                    Strength += iSlot.Item.gem3.Strength
                    Agility += iSlot.Item.gem3.Agility
                    HasteRating += iSlot.Item.gem3.HasteRating
                    ExpertiseRating += iSlot.Item.gem3.ExpertiseRating
                    HitRating += iSlot.Item.gem3.HitRating
                    AttackPower += iSlot.Item.gem3.AttackPower
                    CritRating += iSlot.Item.gem3.CritRating
                    ArmorPenetrationRating += iSlot.Item.gem3.ArmorPenetrationRating

                    BonusArmor += iSlot.Item.gem3.BonusArmor
                    Stamina += iSlot.Item.gem3.Stamina
                    MasteryRating += iSlot.Item.gem3.MasteryRating
                    DodgeRating += iSlot.Item.gem3.DodgeRating
                    ParryRating += iSlot.Item.gem3.ParryRating

                End If

                If iSlot.Item.IsGembonusActif And iSlot.Item.gembonus <> 0 Then
                    Try
                        Dim el As XElement = (From ele In GemBonusDB.<bonus>.Elements
                                              Where ele.<id>.Value = iSlot.Item.gembonus
                                              Select ele).FirstOrDefault
                        Strength += el.<Strength>.Value
                        Agility += el.<Agility>.Value
                        HasteRating += el.<HasteRating>.Value
                        ExpertiseRating += el.<ExpertiseRating>.Value
                        HitRating += el.<HitRating>.Value
                        AttackPower += el.<AttackPower>.Value
                        CritRating += el.<CritRating>.Value
                        ArmorPenetrationRating += el.<ArmorPenetrationRating>.Value

                        Stamina += el.<Stamina>.Value
                        MasteryRating += el.<MasteryRating>.Value
                        DodgeRating += el.<DodgeRating>.Value
                        ParryRating += el.<ParryRating>.Value
                        BonusArmor += el.<BonusArmor>.Value

                    Catch ex As Exception
                        Log.Log(ex.StackTrace, logging.Level.ERR)

                    End Try
                End If

                If iSlot.Item.Enchant.Id <> 0 Then
                    Strength += iSlot.Item.Enchant.Strength
                    Agility += iSlot.Item.Enchant.Agility
                    HasteRating += iSlot.Item.Enchant.HasteRating
                    ExpertiseRating += iSlot.Item.Enchant.ExpertiseRating
                    HitRating += iSlot.Item.Enchant.HitRating
                    AttackPower += iSlot.Item.Enchant.AttackPower
                    CritRating += iSlot.Item.Enchant.CritRating
                    ArmorPenetrationRating += iSlot.Item.Enchant.ArmorPenetrationRating

                    BonusArmor += iSlot.Item.Enchant.BonusArmor
                    Stamina += iSlot.Item.Enchant.Stamina
                    MasteryRating += iSlot.Item.Enchant.MasteryRating
                    DodgeRating += iSlot.Item.Enchant.DodgeRating
                    ParryRating += iSlot.Item.Enchant.ParryRating


                End If

                'Reforge
                If iSlot.Item.ReForgingvalue <> 0 Then
                    Select Case iSlot.Item.ReForgingFrom
                        Case "Crit"
                            CritRating -= iSlot.Item.ReForgingvalue
                        Case "Exp"
                            ExpertiseRating -= iSlot.Item.ReForgingvalue
                        Case "Haste"
                            HasteRating -= iSlot.Item.ReForgingvalue
                        Case "Hit"
                            HitRating -= iSlot.Item.ReForgingvalue
                        Case "Mast"
                            MasteryRating -= iSlot.Item.ReForgingvalue
                        Case "Dodge"
                            DodgeRating -= iSlot.Item.ReForgingvalue
                        Case "Parry"
                            ParryRating -= iSlot.Item.ReForgingvalue
                    End Select

                    Select Case iSlot.Item.ReForgingTo
                        Case "Crit"
                            CritRating += iSlot.Item.ReForgingvalue
                        Case "Exp"
                            ExpertiseRating += iSlot.Item.ReForgingvalue
                        Case "Haste"
                            HasteRating += iSlot.Item.ReForgingvalue
                        Case "Hit"
                            HitRating += iSlot.Item.ReForgingvalue
                        Case "Mast"
                            MasteryRating += iSlot.Item.ReForgingvalue
                        Case "Dodge"
                            DodgeRating += iSlot.Item.ReForgingvalue
                        Case "Parry"
                            ParryRating += iSlot.Item.ReForgingvalue
                    End Select

                End If



            End If

            ' Meta Gem
            If iSlot.Item.gem1.Id = 41398 Or iSlot.Item.gem1.Id = 41285 Then
                StatSummary.chkMeta.IsChecked = True
            End If
            ' Tailor enchant
            If iSlot.Item.Enchant.Id = 7 Then
                StatSummary.chkTailorEnchant.IsChecked = True
            End If
            ' PyroRocket
            If iSlot.Item.Enchant.Id = 3603 Then
                StatSummary.chkIngenieer.IsChecked = True
            End If
            ' Hyperspeed accelerator
            If iSlot.Item.Enchant.Id = 3604 Then
                StatSummary.chkAccelerators.IsChecked = True
            End If

            ' Ashen band
            If iSlot.Item.Id = 50401 Or iSlot.Item.Id = 50402 Or iSlot.Item.Id = 52572 Or iSlot.Item.Id = 52571 Then
                StatSummary.chkAshenBand.IsChecked = True
            End If
NextItem:
        Next
        ' Bloodfury
        If GearSelector.ParentFrame.cmbRace.SelectedItem = "Orc" Then
            StatSummary.chkBloodFury.IsChecked = True
        End If
        ' Berzerking
        If GearSelector.ParentFrame.cmbRace.SelectedItem = "Troll" Then
            StatSummary.chkBerzerking.IsChecked = True
        End If
        ' Arcane torrent
        If cmbRace.SelectedItem = "Blood Elf" Then
            StatSummary.chkArcaneTorrent.IsChecked = True
        End If
        If cmbRace.SelectedItem = "Worgen" Then
            StatSummary.chkWorgen.IsChecked = True
        End If
        If cmbRace.SelectedItem = "Draenei" Then
            StatSummary.chkDraeni.IsChecked = True
        End If
        If cmbRace.SelectedItem = "Goblin" Then
            StatSummary.chkGoblin.IsChecked = True
        End If

        ' Set bonus1
        If cSetBonus.Count > 0 Then
            cSetBonus.Sort()
            cSetBonus = GearSelector.TransformToSet(cSetBonus)
            Dim i As Integer
            Dim sId As String

            Do Until cSetBonus.Count = 0

                sId = cSetBonus.Item(0)
                i = GearSelector.CollectionDuplicateCount(cSetBonus, cSetBonus.Item(0))
                If i >= 4 Then
                    StatSummary.cmbSetBonus1.Text = cSetBonus.Item(0)
                    StatSummary.cmbSetBonus1.Text = StatSummary.cmbSetBonus1.Text.Replace("DPS", "4PDPS")
                    StatSummary.cmbSetBonus1.Text = StatSummary.cmbSetBonus1.Text.Replace("TNK", "4PTNK")
                End If
                If i >= 2 Then
                    If StatSummary.cmbSetBonus1.Text <> "" Then
                        StatSummary.cmbSetBonus2.Text = cSetBonus.Item(0)
                        StatSummary.cmbSetBonus2.Text = StatSummary.cmbSetBonus2.Text.Replace("DPS", "2PDPS")
                        StatSummary.cmbSetBonus2.Text = StatSummary.cmbSetBonus2.Text.Replace("TNK", "2PTNK")
                    Else
                        StatSummary.cmbSetBonus1.Text = cSetBonus.Item(0)
                        StatSummary.cmbSetBonus1.Text = StatSummary.cmbSetBonus1.Text.Replace("DPS", "2PDPS")
                        StatSummary.cmbSetBonus1.Text = StatSummary.cmbSetBonus1.Text.Replace("TNK", "2PTNK")
                    End If
                End If
                Do Until cSetBonus.Contains(sId) = False
                    cSetBonus.Remove(sId)
                Loop
            Loop
        End If

        Armor += Agility * 2
        StatSummary.txtStr.Text = Strength
        StatSummary.txtAgi.Text = Agility
        '		BonusArmor
        StatSummary.txtArmor.Text = Armor
        StatSummary.txtHaste.Text = HasteRating
        StatSummary.txtExp.Text = ExpertiseRating
        StatSummary.txtHit.Text = HitRating
        StatSummary.txtAP.Text = AttackPower
        StatSummary.txtArP.Text = ArmorPenetrationRating
        StatSummary.txtCrit.Text = CritRating
        StatSummary.txtIntel.Text = Intel
        StatSummary.txtMHDPS.Text = DPS1
        StatSummary.txtMHWSpeed.Text = Speed1
        StatSummary.txtOHDPS.Text = DPS2
        StatSummary.txtOHWSpeed.Text = Speed2
        StatSummary.txtMast.Text = MasteryRating
        StatSummary.txtStam.Text = Stamina
        StatSummary.txtDodge.Text = DodgeRating
        StatSummary.txtParry.Text = ParryRating
        StatSummary.txtAddArmor.Text = BonusArmor
refreshRating:
        If level85 Then
            StatSummary.lblHAste.Content = "Haste Rating (" & toDDecimal(StatSummary.txtHaste.Text / 128.05701) & "%)"
            StatSummary.lblExp.Content = "Expertise Rating(" & (toDDecimal(StatSummary.txtExp.Text / 120.109)) * 4 & ")"
            StatSummary.lblHit.Content = "Hit Rating(" & toDDecimal(StatSummary.txtHit.Text / 120.109) & "%)"
            StatSummary.lblArP.Content = "ArP(" & toDDecimal(StatSummary.txtArP.Text / 13.99) & "%)"
            StatSummary.lblCrit.Content = "CritRating(" & toDDecimal(StatSummary.txtCrit.Text / 179.28) & "%)"
            StatSummary.lblMast.Content = "Mastery Rating(" & toDDecimal(StatSummary.txtMast.Text / 179.28) & "%)"
        Else
            StatSummary.lblHAste.Content = "Haste Rating (" & toDDecimal(StatSummary.txtHaste.Text / 32.79) & "%)"
            StatSummary.lblExp.Content = "Expertise Rating(" & (toDDecimal(StatSummary.txtExp.Text / 30.7548)) * 4 & ")"
            StatSummary.lblHit.Content = "Hit Rating(" & toDDecimal(StatSummary.txtHit.Text / 30.7548) & "%)"
            StatSummary.lblArP.Content = "ArP(" & toDDecimal(StatSummary.txtArP.Text / 13.99) & ")"
            StatSummary.lblCrit.Content = "CritRating(" & toDDecimal(StatSummary.txtCrit.Text / 45.906) & "%)"
            StatSummary.lblMast.Content = "Mastery Rating(" & toDDecimal(StatSummary.txtMast.Text / 45.906) & "%)"
        End If





    End Sub
    Sub SaveMycharacter()
        Dim xmlChar As XDocument = XDocument.Parse("<character/>")
        xmlChar.Element("character").Add(New XElement("race", cmbRace.SelectedItem))
        xmlChar.Element("character").Add(New XElement("skill1", cmbSkill1.SelectedItem))
        xmlChar.Element("character").Add(New XElement("skill2", cmbSkill2.SelectedItem))
        xmlChar.Element("character").Add(New XElement("food", cmbFood.SelectedItem))
        xmlChar.Element("character").Add(New XElement("flask", cmbFlask.SelectedItem))
        xmlChar.Element("character").Add(New XElement("DW", StatSummary.rDW.IsChecked.ToString))
        xmlChar.Element("character").Add(New XElement("TwoHand", StatSummary.r2Hand.IsChecked.ToString))
        xmlChar.Element("character").Add(New XElement("ManualInput", StatSummary.chkManualInput.IsChecked))

        Dim iSlot As VisualEquipSlot
        For Each iSlot In GearSelector.EquipmentList
            xmlChar.Element("character").Add(New XElement(iSlot.Text.ToString))
            xmlChar.Element("character").Element(iSlot.Text.ToString).Add(New XElement("id", iSlot.Item.Id))
            xmlChar.Element("character").Element(iSlot.Text.ToString).Add(New XElement("gem1", iSlot.Item.gem1.Id))
            xmlChar.Element("character").Element(iSlot.Text.ToString).Add(New XElement("gem2", iSlot.Item.gem2.Id))
            xmlChar.Element("character").Element(iSlot.Text.ToString).Add(New XElement("gem3", iSlot.Item.gem3.Id))
            xmlChar.Element("character").Element(iSlot.Text.ToString).Add(New XElement("enchant", iSlot.Item.Enchant.Id))
            xmlChar.Element("character").Element(iSlot.Text.ToString).Add(New XElement("reforge"))
            xmlChar.Element("character").Element(iSlot.Text.ToString).Element("reforge").Add(New XElement("from", iSlot.Item.ReForgingFrom))
            xmlChar.Element("character").Element(iSlot.Text.ToString).Element("reforge").Add(New XElement("to", iSlot.Item.ReForgingTo))
            xmlChar.Element("character").Element(iSlot.Text.ToString).Element("reforge").Add(New XElement("value", iSlot.Item.ReForgingvalue))
        Next



        xmlChar.Element("character").Add(New XElement("stat", ""))
        xmlChar.Element("character").Element("stat").Add(New XElement("Strength", CheckForInt(StatSummary.txtStr.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("Agility", CheckForInt(StatSummary.txtAgi.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("Intel", CheckForInt(StatSummary.txtIntel.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("Stamina", CheckForInt(StatSummary.txtStam.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("Armor", CheckForInt(StatSummary.txtArmor.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("SpecialArmor", CheckForInt(StatSummary.txtAddArmor.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("AttackPower", CheckForInt(StatSummary.txtAP.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("MasteryRating", CheckForInt(StatSummary.txtMast.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("HitRating", CheckForInt(StatSummary.txtHit.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("CritRating", CheckForInt(StatSummary.txtCrit.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("HasteRating", CheckForInt(StatSummary.txtHaste.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("ArmorPenetrationRating", CheckForInt(StatSummary.txtArP.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("ExpertiseRating", CheckForInt(StatSummary.txtExp.Text)))


        xmlChar.Element("character").Add(New XElement("weapon", ""))
        Dim i As Integer
        If StatSummary.r2Hand.IsChecked = True Then
            i = 1
        Else
            i = 2
        End If

        xmlChar.Element("character").Element("weapon").Add(New XElement("count", i))

        xmlChar.Element("character").Element("weapon").Add(New XElement("mainhand", ""))
        xmlChar.Element("character").Element("weapon").Add(New XElement("offhand", ""))


        xmlChar.Element("character").Element("weapon").Element("mainhand").Add(New XElement("dps", CheckForDouble(StatSummary.txtMHDPS.Text)))
        xmlChar.Element("character").Element("weapon").Element("mainhand").Add(New XElement("speed", CheckForDouble(StatSummary.txtMHWSpeed.Text)))

        xmlChar.Element("character").Element("weapon").Element("offhand").Add(New XElement("dps", CheckForDouble(StatSummary.txtOHDPS.Text)))
        xmlChar.Element("character").Element("weapon").Element("offhand").Add(New XElement("speed", CheckForDouble(StatSummary.txtOHWSpeed.Text)))




        xmlChar.Element("character").Add(New XElement("Set", ""))
        If StatSummary.cmbSetBonus1.Text <> "" Then
            xmlChar.Element("character").Element("Set").Add(New XElement(StatSummary.cmbSetBonus1.Text, 1))
        End If
        If StatSummary.cmbSetBonus2.Text <> "" Then
            xmlChar.Element("character").Element("Set").Add(New XElement(StatSummary.cmbSetBonus2.Text, 1))
        End If






        xmlChar.Element("character").Add(New XElement("trinket", ""))

        If StatSummary.cmbTrinket1.Text <> "" Then
            xmlChar.Element("character").Element("trinket").Add(New XElement(StatSummary.cmbTrinket1.Text, 1))
        End If
        If StatSummary.cmbTrinket2.Text <> "" Then
            xmlChar.Element("character").Element("trinket").Add(New XElement(StatSummary.cmbTrinket2.Text, 1))
        End If

        xmlChar.Element("character").Add(New XElement("WeaponProc", ""))
        If StatSummary.cmbWeaponProc1.Text <> "" Then
            xmlChar.Element("character").Element("WeaponProc").Add(New XElement(StatSummary.cmbWeaponProc1.Text, 1))
        End If
        If StatSummary.cmbWeaponProc2.Text <> "" Then
            xmlChar.Element("character").Element("WeaponProc").Add(New XElement(StatSummary.cmbWeaponProc2.Text, 1))
        End If



        xmlChar.Element("character").Add(New XElement("misc", ""))
        xmlChar.Element("character").Element("misc").Add(New XElement("HandMountedPyroRocket", StatSummary.chkIngenieer.IsChecked))
        xmlChar.Element("character").Element("misc").Add(New XElement("HyperspeedAccelerators", StatSummary.chkAccelerators.IsChecked))
        xmlChar.Element("character").Element("misc").Add(New XElement("ChaoticSkyflareDiamond", StatSummary.chkMeta.IsChecked))
        xmlChar.Element("character").Element("misc").Add(New XElement("TailorEnchant", StatSummary.chkTailorEnchant.IsChecked))
        xmlChar.Element("character").Element("misc").Add(New XElement("AshenBand", StatSummary.chkAshenBand.IsChecked))


        Dim itm As CheckBox

        For Each itm In stackConsumable.Children
            xmlChar.Element("character").Element("misc").Add(New XElement(itm.Name, itm.IsChecked))
        Next



        xmlChar.Element("character").Add(New XElement("racials", ""))
        xmlChar.Element("character").Element("racials").Add(New XElement("MHExpertiseBonus", CheckForInt(StatSummary.txtMHExpBonus.Text)))
        xmlChar.Element("character").Element("racials").Add(New XElement("OHExpertiseBonus", CheckForInt(StatSummary.txtOHExpBonus.Text)))
        xmlChar.Element("character").Element("racials").Add(New XElement("Orc", StatSummary.chkBloodFury.IsChecked))
        xmlChar.Element("character").Element("racials").Add(New XElement("Troll", StatSummary.chkBerzerking.IsChecked))
        xmlChar.Element("character").Element("racials").Add(New XElement("BloodElf", StatSummary.chkArcaneTorrent.IsChecked))

        xmlChar.Element("character").Element("racials").Add(New XElement("Dreani", StatSummary.chkDraeni.IsChecked))
        xmlChar.Element("character").Element("racials").Add(New XElement("Worgen", StatSummary.chkWorgen.IsChecked))
        xmlChar.Element("character").Element("racials").Add(New XElement("Goblin", StatSummary.chkGoblin.IsChecked))



        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/CharactersWithGear/" & GearSelector.FilePath, FileMode.Create, isoStore)
                xmlChar.Save(isoStream)
            End Using
        End Using



    End Sub
    Private Sub r2Hand_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles StatSummary.rd2H_Check, GearSelector.rd2H_Check
        GearSelector.TwoHWeapSlot.Opacity = 1
        GearSelector.TwoHWeapSlot.IsHitTestVisible = True
        GearSelector.MHWeapSlot.Opacity = 0
        GearSelector.OHWeapSlot.Opacity = 0
        GearSelector.MHWeapSlot.IsHitTestVisible = False
        GearSelector.OHWeapSlot.IsHitTestVisible = False

        StatSummary.rDW.IsChecked = False
        StatSummary.r2Hand.IsChecked = True
        GearSelector.rd2H.IsChecked = StatSummary.r2Hand.IsChecked
        GearSelector.rdDW.IsChecked = StatSummary.rDW.IsChecked
        GetStats()


    End Sub

    Private Sub rDW_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles StatSummary.rdDW_Check, GearSelector.rdDW_Check
        GearSelector.TwoHWeapSlot.Opacity = 0
        GearSelector.TwoHWeapSlot.IsHitTestVisible = False
        GearSelector.MHWeapSlot.Opacity = 1
        GearSelector.OHWeapSlot.Opacity = 1
        GearSelector.MHWeapSlot.IsHitTestVisible = True
        GearSelector.OHWeapSlot.IsHitTestVisible = True

        StatSummary.rDW.IsChecked = True
        StatSummary.r2Hand.IsChecked = False
        GearSelector.rd2H.IsChecked = StatSummary.r2Hand.IsChecked
        GearSelector.rdDW.IsChecked = StatSummary.rDW.IsChecked
        GetStats()

    End Sub
    Friend AdvancedMode As Boolean = True
    Private Sub cmdAdvancedMode_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdAdvancedMode.Click

        If AdvancedMode = False Then
            AdvancedMode = True
            SwitchMode()

            Return
        End If

        If AdvancedMode = True Then
            AdvancedMode = False
            SwitchMode()
            Me.TabControl1.SelectedIndex = 0
            Return
        End If





    End Sub
    Sub SwitchMode()
        Dim Visib As Integer
        If AdvancedMode Then
            Visib = Visibility.Visible
            cmdAdvancedMode.Content = "Beginner Mode"
        Else
            Visib = Visibility.Collapsed
            cmdAdvancedMode.Content = "Advanced Mode"
        End If
        TabBuff.Visibility = Visib
        'tabConsumable.Visibility = Visib
        'tabConfig.Visibility = Visib
        TabEPOptions.Visibility = Visib
        'TabReport.Visibility = Visib
        TabReportOption.Visibility = Visib
        TabSimOption.Visibility = Visib
        TabStatScaling.Visibility = Visib
        tabStatSummary.Visibility = Visib
        tabDebug.Visibility = Visib
        TabTank.Visibility = Visib
        For Each tab As TabItem In TabControl1.Items
            tab.UpdateLayout()
        Next

        'If Me.TabControl1.SelectedIndex = 0 Then
        '    Me.TabControl1.SelectedIndex = 1
        '    'Me.TabControl1.SelectedIndex = 0
        'Else
        '    '  Dim i As Integer
        '    '  i = Me.TabControl1.SelectedIndex
        '    Me.TabControl1.SelectedIndex = 0
        '    ' Me.TabControl1.SelectedIndex = i
        'End If
    End Sub

    Private Sub cmbGearSelector_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles cmbGearSelector.SelectionChanged
        If GearSelector.FilePath <> "" Then
            SaveMycharacter()
            cmdEditCharacterWithGear_Click(Nothing, Nothing)

        End If
    End Sub

    Private Sub cmbDebugLevel_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles cmbDebugLevel.SelectionChanged

        Select Case cmbDebugLevel.SelectedValue.ToString
            Case "INFO"
                Log.LoggingLevel = logging.Level.INFO
            Case "DEBUG"
                Log.LoggingLevel = logging.Level.WARNING
            Case "ERROR"
                Log.LoggingLevel = logging.Level.ERR
            Case "FATAL"
                Log.LoggingLevel = logging.Level.FATAL
            Case "NO LOGGING"
                Log.LoggingLevel = logging.Level.NO
        End Select
    End Sub

    Private Sub cmdShowDebug_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdShowDebug.Click
        Dim txtEditor As New myTextReader
        txtEditor.OpenFileFromISO("Solution.Silverlight.log")
        txtEditor.Show()
    End Sub



    Private Sub cmdCleanDebug_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdCleanDebug.Click
        Log.Clean()
    End Sub

    Private Sub cmdScaling_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdScaling.Click
        SimConstructor.StartScaling(Me)
    End Sub

    Private Sub cmdRngSeeder_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdRngSeeder.Click
        RNGSeeder += 1
    End Sub
    Dim level85 As Boolean
    Private Sub btlvl85_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btlvl85.Click
        If level85 Then
            btlvl85.Content = "Set to level 85"
            level85 = False
            GetStats()
        Else
            btlvl85.Content = "Set to level 80"
            level85 = True
            GetStats()
        End If
        
    End Sub

    Sub CleanUp() Handles cmdCleanCache.Click
        Dim isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
       
        For Each fld In isoStore.GetDirectoryNames
            CleanAndDeleteFolder("/" & fld & "/")

        Next
        'and rebuild
        Page_Loaded(Nothing, Nothing)
    End Sub

    Sub CleanAndDeleteFolder(ByVal Path As String)
        Dim isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()

        For Each folder In isoStore.GetDirectoryNames(Path)
            CleanAndDeleteFolder(Path & "/" & folder & "/")
        Next
        For Each file In isoStore.GetFileNames(Path)
            isoStore.DeleteFile(Path & "/" & file)
        Next

    End Sub


    Private Sub cmdCompareLog_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdCompareLog.Click
        Dim cmp As New LogComparer
        cmp.Show()
    End Sub
End Class
