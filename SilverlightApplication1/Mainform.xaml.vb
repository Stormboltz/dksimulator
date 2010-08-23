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

    Private WithEvents ScenarioEditor As ScenarioEditor




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
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Dim f As String
            For Each f In isoStore.GetFileNames
                System.Diagnostics.Debug.WriteLine(f)
            Next
        End Using

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
        For Each xNode In doc.Descendants
            Dim ckTrinket As New CheckBox
            Try
                ckTrinket.Name = "chkEP" & xNode.Name.ToString
                ckTrinket.Content = xNode.Name
                'ckTrinket.Height = 20
                'ckTrinket.Width = 180
                grpEPTrinkets.Children.Add(ckTrinket)


            Catch ex As Exception
                Log.Log(ex.StackTrace, logging.Level.ERR)
                System.Diagnostics.Debug.WriteLine("Err:" & xNode.Name.ToString)
            End Try
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
            Food.Attach(cmbFood.SelectedValue)
            GetStats()
        Catch ex As Exception
            Log.Log(ex.StackTrace, logging.Level.ERR)
        End Try

    End Sub
    Sub GetStats()
        If IsNothing(GearSelector) Then Exit Sub
        If GearSelector.InLoad Then Exit Sub
        Dim iSlot As VisualEquipSlot
        Dim Strength As Integer
        Dim Intel As Integer
        Dim Agility As Integer
        Dim BonusArmor As Integer
        Dim Armor As Integer
        Dim HasteRating As Integer
        Dim ExpertiseRating As Integer
        Dim HitRating As Integer
        Dim AttackPower As Integer
        Dim CritRating As Integer

        Dim MasteryRating As Integer
        Dim DodgeRating As Integer
        Dim ParryRating As Integer

        Dim ArmorPenetrationRating As Integer
        Dim Speed1 As String = "0"
        Dim Speed2 As String = "0"
        Dim DPS1 As String = "0"
        Dim DPS2 As String = "0"


        chkMeta.IsChecked = False
        chkTailorEnchant.IsChecked = False
        chkIngenieer.IsChecked = False
        chkAccelerators.IsChecked = False
        chkAshenBand.IsChecked = False
        chkBloodFury.IsChecked = False
        chkBerzerking.IsChecked = False
        chkArcaneTorrent.IsChecked = False

        cmbSetBonus1.Text = ""
        cmbSetBonus2.Text = ""

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


        txtMHExpBonus.Text = 0
        txtOHExpBonus.Text = 0

        For Each iSlot In GearSelector.EquipmentList

            If iSlot.Item.Id = 0 Then GoTo NextItem
            If iSlot.SlotId = 17 And rDW.IsChecked Then GoTo NextItem
            If iSlot.SlotId = 13 And r2Hand.IsChecked Then GoTo NextItem

            Dim subc As Integer = (From el In ItemDB.Element("items").Elements
                                   Where el.Element("id").Value = iSlot.Item.Id
                                   Select GearSelector.GearSelector.getItem(el)).First.subclass
            If iSlot.Text.ToString = "TwoHand" Or iSlot.Text.ToString = "MainHand" Then
                DPS1 = iSlot.Item.DPS
                Speed1 = iSlot.Item.Speed
                Try
                    cmbWeaponProc1.Text = "MH" & (
                        From el In WeapProcDB.Element("WeaponProcList").Elements("proc")
                        Where (el.Attribute("id").Value = iSlot.Item.Id)
                        Select el).First.Attribute("name").Value
                Catch ex As Exception
                    Log.Log(ex.StackTrace, logging.Level.ERR)
                    cmbWeaponProc1.Text = ""
                End Try
                Select Case GearSelector.ParentFrame.cmbRace.SelectedValue
                    Case "Dwarf"
                        If subc = 4 Or subc = 5 Then
                            txtMHExpBonus.Text = 5
                        End If
                    Case "Human"
                        If subc = 4 Or subc = 5 Or subc = 7 Or subc = 8 Then
                            txtMHExpBonus.Text = 3
                        End If
                    Case "Orc"
                        If subc = 0 Or subc = 1 Then
                            txtMHExpBonus.Text = 5
                        End If
                End Select



            End If

            If iSlot.Text.ToString = "OffHand" Then
                DPS2 = iSlot.Item.DPS
                Speed2 = iSlot.Item.Speed

                Try
                    cmbWeaponProc2.Text = "OH" & (
                        From el In WeapProcDB.Element("WeaponProcList").Elements("proc")
                        Where (el.Attribute("id").Value = iSlot.Item.Id)
                        Select el).First.Attribute("name").Value
                Catch ex As Exception
                    Log.Log(ex.StackTrace, logging.Level.ERR)
                    cmbWeaponProc1.Text = ""
                End Try

                Select Case GearSelector.ParentFrame.cmbRace.SelectedValue
                    Case "Dwarf"
                        If subc = 4 Or subc = 5 Then
                            txtOHExpBonus.Text = 5
                        End If
                    Case "Human"
                        If subc = 4 Or subc = 5 Or subc = 7 Or subc = 8 Then
                            txtOHExpBonus.Text = 3
                        End If
                    Case "Orc"
                        If subc = 0 Or subc = 1 Then
                            txtOHExpBonus.Text = 5
                        End If
                End Select
            End If
            If iSlot.Item.setid <> 0 Then
                cSetBonus.Add(iSlot.Item.setid)
            End If

            If iSlot.Text.ToString = "Trinket1" Then
                Try
                    cmbTrinket1.Text = (
                        From el In trinketDB.Element("TrinketList").Elements("trinket")
                        Where (el.Attribute("id").Value = iSlot.Item.Id)
                        Select el).FirstOrDefault.Attribute("name").Value
                Catch ex As Exception
                    Log.Log("No Effect on Trinket1", logging.Level.INFO)
                    cmbTrinket1.Text = ""
                End Try
            End If
            If iSlot.Text.ToString = "Trinket2" Then
                Try
                    cmbTrinket2.Text = (From el In trinketDB.Element("TrinketList").Elements("trinket")
                        Where (el.Attribute("id").Value = iSlot.Item.Id)
                        Select el).FirstOrDefault.Attribute("name").Value
                Catch ex As Exception
                    Log.Log("No Effect on Trinket1", logging.Level.INFO)
                    cmbTrinket2.Text = ""
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


                If iSlot.Item.gem1.Id <> 0 Then
                    Strength += iSlot.Item.gem1.Strength
                    Agility += iSlot.Item.gem1.Agility
                    HasteRating += iSlot.Item.gem1.HasteRating
                    ExpertiseRating += iSlot.Item.gem1.ExpertiseRating
                    HitRating += iSlot.Item.gem1.HitRating
                    AttackPower += iSlot.Item.gem1.AttackPower
                    CritRating += iSlot.Item.gem1.CritRating
                    ArmorPenetrationRating += iSlot.Item.gem1.ArmorPenetrationRating
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
                End If

                If iSlot.Item.IsGembonusActif And iSlot.Item.gembonus <> 0 Then
                    Try
                        Dim el As XElement = (From ele In GemBonusDB.Element("bonus").Elements
                                              Where ele.Element("id").Value = iSlot.Item.gembonus
                                              Select ele).FirstOrDefault
                        Strength += el.Element("Strength").Value
                        Agility += el.Element("Agility").Value
                        HasteRating += el.Element("HasteRating").Value
                        ExpertiseRating += el.Element("ExpertiseRating").Value
                        HitRating += el.Element("HitRating").Value
                        AttackPower += el.Element("AttackPower").Value
                        CritRating += el.Element("CritRating").Value
                        ArmorPenetrationRating += el.Element("ArmorPenetrationRating").Value
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
                chkMeta.IsChecked = True
            End If
            ' Tailor enchant
            If iSlot.Item.Enchant.Id = 7 Then
                chkTailorEnchant.IsChecked = True
            End If
            ' PyroRocket
            If iSlot.Item.Enchant.Id = 3603 Then
                chkIngenieer.IsChecked = True
            End If
            ' Hyperspeed accelerator
            If iSlot.Item.Enchant.Id = 3604 Then
                chkAccelerators.IsChecked = True
            End If

            ' Ashen band
            If iSlot.Item.Id = 50401 Or iSlot.Item.Id = 50402 Or iSlot.Item.Id = 52572 Or iSlot.Item.Id = 52571 Then
                chkAshenBand.IsChecked = True
            End If
NextItem:
        Next
        ' Bloodfury
        If GearSelector.ParentFrame.cmbRace.SelectedItem = "Orc" Then
            chkBloodFury.IsChecked = True
        End If
        ' Berzerking
        If GearSelector.ParentFrame.cmbRace.SelectedItem = "Troll" Then
            chkBerzerking.IsChecked = True
        End If
        ' Arcane torrent
        If cmbRace.SelectedItem = "Blood Elf" Then
            chkArcaneTorrent.IsChecked = True
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
                    cmbSetBonus1.Text = cSetBonus.Item(0)
                    cmbSetBonus1.Text = cmbSetBonus1.Text.Replace("DPS", "4PDPS")
                    cmbSetBonus1.Text = cmbSetBonus1.Text.Replace("TNK", "4PTNK")
                End If
                If i >= 2 Then
                    If cmbSetBonus1.Text <> "" Then
                        cmbSetBonus2.Text = cSetBonus.Item(0)
                        cmbSetBonus2.Text = cmbSetBonus2.Text.Replace("DPS", "2PDPS")
                        cmbSetBonus2.Text = cmbSetBonus2.Text.Replace("TNK", "2PTNK")
                    Else
                        cmbSetBonus1.Text = cSetBonus.Item(0)
                        cmbSetBonus1.Text = cmbSetBonus1.Text.Replace("DPS", "2PDPS")
                        cmbSetBonus1.Text = cmbSetBonus1.Text.Replace("TNK", "2PTNK")
                    End If
                End If
                Do Until cSetBonus.Contains(sId) = False
                    cSetBonus.Remove(sId)
                Loop
            Loop
        End If

        Armor += Agility * 2
        txtStr.Text = Strength
        txtAgi.Text = Agility
        '		BonusArmor
        txtArmor.Text = Armor
        txtHaste.Text = HasteRating
        lblHAste.Content = "Haste Rating (" & toDDecimal(HasteRating / 25.22) & "%)"

        txtExp.Text = ExpertiseRating
        lblExp.Content = "Expertise Rating(" & (toDDecimal(ExpertiseRating / 32.79)) * 4 & ")"
        txtHit.Text = HitRating
        lblHit.Content = "Hit Rating(" & toDDecimal(HitRating / 32.79) & "%)"
        txtAP.Text = AttackPower
        txtArP.Text = ArmorPenetrationRating
        lblArP.Content = "ArP(" & toDDecimal(ArmorPenetrationRating / 13.99) & ")"

        txtCrit.Text = CritRating
        lblCrit.Content = "CritRating(" & toDDecimal(CritRating / 45.91) & "%)"
        txtIntel.Text = Intel
        txtMHDPS.Text = DPS1
        txtMHWSpeed.Text = Speed1
        txtOHDPS.Text = DPS2
        txtOHWSpeed.Text = Speed2
    End Sub
    Sub SaveMycharacter()
        Dim xmlChar As XDocument = XDocument.Parse("<character/>")



        xmlChar.Element("character").Add(New XElement("race", cmbRace.SelectedItem))
        xmlChar.Element("character").Add(New XElement("skill1", cmbSkill1.SelectedItem))
        xmlChar.Element("character").Add(New XElement("skill2", cmbSkill2.SelectedItem))
        xmlChar.Element("character").Add(New XElement("food", cmbFood.SelectedItem))
        xmlChar.Element("character").Add(New XElement("flask", cmbFlask.SelectedItem))
        xmlChar.Element("character").Add(New XElement("DW", rDW.IsChecked.ToString))
        xmlChar.Element("character").Add(New XElement("TwoHand", r2Hand.IsChecked.ToString))


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
        xmlChar.Element("character").Element("stat").Add(New XElement("Strength", CheckForInt(txtStr.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("Agility", CheckForInt(txtAgi.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("Intel", CheckForInt(txtIntel.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("Armor", CheckForInt(txtArmor.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("AttackPower", CheckForInt(txtAP.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("HitRating", CheckForInt(txtHit.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("CritRating", CheckForInt(txtCrit.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("HasteRating", CheckForInt(txtHaste.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("ArmorPenetrationRating", CheckForInt(txtArP.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("ExpertiseRating", CheckForInt(txtExp.Text)))


        xmlChar.Element("character").Add(New XElement("weapon", ""))
        Dim i As Integer
        If r2Hand.IsChecked = True Then
            i = 1
        Else
            i = 2
        End If

        xmlChar.Element("character").Element("weapon").Add(New XElement("count", i))

        xmlChar.Element("character").Element("weapon").Add(New XElement("mainhand", ""))
        xmlChar.Element("character").Element("weapon").Add(New XElement("offhand", ""))


        xmlChar.Element("character").Element("weapon").Element("mainhand").Add(New XElement("dps", CheckForDouble(txtMHDPS.Text)))
        xmlChar.Element("character").Element("weapon").Element("mainhand").Add(New XElement("speed", CheckForDouble(txtMHWSpeed.Text)))

        xmlChar.Element("character").Element("weapon").Element("offhand").Add(New XElement("dps", CheckForDouble(txtOHDPS.Text)))
        xmlChar.Element("character").Element("weapon").Element("offhand").Add(New XElement("speed", CheckForDouble(txtOHWSpeed.Text)))




        xmlChar.Element("character").Add(New XElement("Set", ""))
        If cmbSetBonus1.Text <> "" Then
            xmlChar.Element("character").Element("Set").Add(New XElement(cmbSetBonus1.Text, 1))
        End If
        If cmbSetBonus2.Text <> "" Then
            xmlChar.Element("character").Element("Set").Add(New XElement(cmbSetBonus2.Text, 1))
        End If






        xmlChar.Element("character").Add(New XElement("trinket", ""))

        If cmbTrinket1.Text <> "" Then
            xmlChar.Element("character").Element("trinket").Add(New XElement(cmbTrinket1.Text, 1))
        End If
        If cmbTrinket2.Text <> "" Then
            xmlChar.Element("character").Element("trinket").Add(New XElement(cmbTrinket2.Text, 1))
        End If

        xmlChar.Element("character").Add(New XElement("WeaponProc", ""))
        If cmbWeaponProc1.Text <> "" Then
            xmlChar.Element("character").Element("WeaponProc").Add(New XElement(cmbWeaponProc1.Text, 1))
        End If
        If cmbWeaponProc2.Text <> "" Then
            xmlChar.Element("character").Element("WeaponProc").Add(New XElement(cmbWeaponProc2.Text, 1))
        End If



        xmlChar.Element("character").Add(New XElement("misc", ""))
        xmlChar.Element("character").Element("misc").Add(New XElement("HandMountedPyroRocket", chkIngenieer.IsChecked))
        xmlChar.Element("character").Element("misc").Add(New XElement("HyperspeedAccelerators", chkAccelerators.IsChecked))
        xmlChar.Element("character").Element("misc").Add(New XElement("ChaoticSkyflareDiamond", chkMeta.IsChecked))
        xmlChar.Element("character").Element("misc").Add(New XElement("TailorEnchant", chkTailorEnchant.IsChecked))
        xmlChar.Element("character").Element("misc").Add(New XElement("AshenBand", chkAshenBand.IsChecked))


        Dim itm As CheckBox

        For Each itm In stackConsumable.Children
            xmlChar.Element("character").Element("misc").Add(New XElement(itm.Name, itm.IsChecked))
        Next



        xmlChar.Element("character").Add(New XElement("racials", ""))
        xmlChar.Element("character").Element("racials").Add(New XElement("MHExpertiseBonus", CheckForInt(txtMHExpBonus.Text)))
        xmlChar.Element("character").Element("racials").Add(New XElement("OHExpertiseBonus", CheckForInt(txtOHExpBonus.Text)))
        xmlChar.Element("character").Element("racials").Add(New XElement("Orc", chkBloodFury.IsChecked))
        xmlChar.Element("character").Element("racials").Add(New XElement("Troll", chkBerzerking.IsChecked))
        xmlChar.Element("character").Element("racials").Add(New XElement("BloodElf", chkArcaneTorrent.IsChecked))
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/CharactersWithGear/" & GearSelector.FilePath, FileMode.Create, isoStore)
                xmlChar.Save(isoStream)
            End Using
        End Using



    End Sub
    Private Sub r2Hand_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles r2Hand.Checked, GearSelector.rd2H_Check
        GearSelector.TwoHWeapSlot.Opacity = 1
        GearSelector.TwoHWeapSlot.IsHitTestVisible = True
        GearSelector.MHWeapSlot.Opacity = 0
        GearSelector.OHWeapSlot.Opacity = 0
        GearSelector.MHWeapSlot.IsHitTestVisible = False
        GearSelector.OHWeapSlot.IsHitTestVisible = False

        rDW.IsChecked = False
        r2Hand.IsChecked = True
        GearSelector.rd2H.IsChecked = r2Hand.IsChecked
        GearSelector.rdDW.IsChecked = rDW.IsChecked
        GetStats()


    End Sub

    Private Sub rDW_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rDW.Checked, GearSelector.rdDW_Check
        GearSelector.TwoHWeapSlot.Opacity = 0
        GearSelector.TwoHWeapSlot.IsHitTestVisible = False
        GearSelector.MHWeapSlot.Opacity = 1
        GearSelector.OHWeapSlot.Opacity = 1
        GearSelector.MHWeapSlot.IsHitTestVisible = True
        GearSelector.OHWeapSlot.IsHitTestVisible = True

        rDW.IsChecked = True
        r2Hand.IsChecked = False
        GearSelector.rd2H.IsChecked = r2Hand.IsChecked
        GearSelector.rdDW.IsChecked = rDW.IsChecked
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
End Class
