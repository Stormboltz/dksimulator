Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO
Imports System.Windows.Resources


Partial Public Class MainForm
    Inherits Page
    Private isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
    Friend EditType As String
    Friend EPVal As EPValues

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
    Private WithEvents GearSelector As New GearSelectorMainForm(Me)


    Public Sub New()
        InitializeComponent()
    End Sub
    'Executes when the user navigates to this page.
    Protected Overrides Sub OnNavigatedTo(ByVal e As System.Windows.Navigation.NavigationEventArgs)

    End Sub
    Function LoadBeforeSim()
        Return True
    End Function
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

        'initReport()
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

            isoStore.CreateDirectory("KahoDKSim/Characters")
            CopyFileFromXAPtoISF("Characters/2h ICC.xml")
            CopyFileFromXAPtoISF("Characters/DW ICC.xml")

            isoStore.CreateDirectory("KahoDKSim/CharactersWithGear")
            CopyFileFromXAPtoISF("CharactersWithGear/2H ICC.xml")
            CopyFileFromXAPtoISF("CharactersWithGear/DW ICC.xml")

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
            CopyFileFromXAPtoISF("Templates/Blood 51-00-20-GoD.xml")
            CopyFileFromXAPtoISF("Templates/Blood 51-00-20.xml")
            CopyFileFromXAPtoISF("Templates/Frost 00-54-17.xml")
            CopyFileFromXAPtoISF("Templates/Frost 03-53-15.xml")
            CopyFileFromXAPtoISF("Templates/Frost 15-54-02.xml")
            CopyFileFromXAPtoISF("Templates/Unholy 00-17-54.xml")
            CopyFileFromXAPtoISF("Templates/Unholy 12-00-59.xml")
            CopyFileFromXAPtoISF("Templates/Unholy 14-00-57.xml")
            CopyFileFromXAPtoISF("Templates/Unholy 17-00-54-3.3.xml")
            CopyFileFromXAPtoISF("Templates/Unholy 17-00-54.xml")
            CopyFileFromXAPtoISF("Templates/Unholy DW 0-17-54.xml")


            'For Each f In isoStore.GetFileNames
            '    System.Diagnostics.Debug.WriteLine(f)
            'Next
        End Using
    End Sub
    Sub LoadEPOptions()
        On Error GoTo sortie
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/EPconfig.xml", FileMode.Open, isoStore)
                Dim doc As XDocument = XDocument.Load(isoStream)


                Dim ctrl As Control
                Dim chkBox As CheckBox
                For Each ctrl In groupEPMain.Children
                    If ctrl.Name.StartsWith("chkEP") Then
                        chkBox = ctrl
                        chkBox.IsChecked = doc.Element("config").Element("Stats").Element(chkBox.Name).Value
                    End If
                Next
                For Each ctrl In groupEPSet.Children
                    If ctrl.Name.StartsWith("chkEP") Then
                        chkBox = ctrl
                        chkBox.IsChecked = doc.Element("config").Element("Sets").Element(chkBox.Name).Value
                    End If
                Next

                For Each ctrl In grpEPTrinkets.Children
                    If ctrl.Name.StartsWith("chkEP") Then
                        chkBox = ctrl
                        chkBox.IsChecked = doc.Element("config").Element("Trinket").Element(chkBox.Name).Value
                    End If
                Next
            End Using
        End Using

sortie:
    End Sub
    Sub loadConfig()
        On Error Resume Next
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/config.xml", FileMode.Open, isoStore)
                Dim doc As XDocument = XDocument.Load(isoStream)
                cmbGearSelector.SelectedItem = doc.Element("config").Element("CharacterWithGear").Value
                cmbTemplate.SelectedItem = doc.Element("config").Element("template").Value
                If doc.Element("config").Element("mode").Value <> "rotation" Then
                    rdPrio.IsChecked = True
                    cmbPrio.IsEnabled = True
                    cmbRotation.IsEnabled = False
                Else
                    rdRot.IsChecked = True
                    cmbPrio.IsEnabled = False
                    cmbRotation.IsEnabled = True
                End If
                cmbIntro.SelectedItem = doc.Element("config").Element("intro").Value
                cmbPrio.SelectedItem = doc.Element("config").Element("priority").Value
                cmbRotation.SelectedItem = doc.Element("config").Element("rotation").Value
                cmdPresence.SelectedItem = doc.Element("config").Element("presence").Value
                cmbSigils.SelectedItem = doc.Element("config").Element("sigil").Value
                cmbRuneMH.SelectedItem = doc.Element("config").Element("mh").Value
                cmbRuneOH.SelectedItem = doc.Element("config").Element("oh").Value
                cmbScenario.SelectedItem = doc.Element("config").Element("scenario").Value


                cmbBShOption.SelectedItem = doc.Element("config").Element("BShOption").Value
                cmbICCBuff.SelectedItem = doc.Element("config").Element("ICCBuff").Value
                txtLatency.Text = doc.Element("config").Element("latency").Value
                txtBSTTL.Text = doc.Element("config").Element("BSTTL").Value
                txtSimtime.Text = doc.Element("config").Element("simtime").Value
                chkCombatLog.IsChecked = doc.Element("config").Element("log").Value
                ckLogRP.IsChecked = doc.Element("config").Element("logdetail").Value
                chkShowProc.isChecked = doc.Element("config").Element("ShowProc").Value
                chkWaitFC.isChecked = doc.Element("config").Element("WaitFC").Value
                '		chkPatch.Ischecked = doc.Element("//config/Patch").Value

                ckPet.IsChecked = doc.Element("config").Element("pet").Value

                txtAMSrp.Text = doc.Element("config").Element("txtAMSrp").Value
                txtAMScd.Text = doc.Element("config").Element("txtAMScd").Value

                chkMergeReport.isChecked = doc.Element("config").Element("chkMergeReport").Value
                txtReportName.Text = doc.Element("config").Element("txtReportName").Value
                chkBloodSync.IsChecked = doc.Element("config").Element("BloodSync").Value
            End Using
        End Using

errH:
    End Sub
    Sub LoadBuffOption()
        On Error GoTo sortie
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/Buffconfig.xml", FileMode.Open, isoStore)
                Dim doc As XDocument = XDocument.Load(isoStream)
                Dim ctrl As Control
                Dim chkBox As CheckBox
                For Each ctrl In grpBuff.Children
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



    End Sub


    Private Sub rCharacter_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

    End Sub



    Private Sub rdRot_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rdRot.Checked
        cmbRotation.IsEnabled = True
        cmbPrio.IsEnabled = False

    End Sub
    Private Sub rdPrio_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rdPrio.Checked
        cmbRotation.IsEnabled = False
        cmbPrio.IsEnabled = True
    End Sub
    Sub UpdateProgressBar()

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




        sTemp = cmbRotation.SelectedItem
        cmbRotation.Items.Clear()
        If (isoStore.DirectoryExists("KahoDKSim/Rotation")) Then
            For Each item In isoStore.GetFileNames("KahoDKSim/Rotation/*")
                cmbRotation.Items.Add(Strings.Right(item, item.Length - InStrRev(item, "\")))
            Next
            cmbRotation.SelectedItem = sTemp
        Else
            isoStore.CreateDirectory("KahoDKSim/Rotation")
        End If
    End Sub

    Sub RefreshScenarioList()
        Dim isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        Dim item As String
        Dim sTemp As String = ""
        Try
            sTemp = cmbScenario.SelectedItem.ToString
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

    Private Sub cmdEditRota_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdEditRota.Click
        PrioEditor.EditType = PriorityEditor.PossibleEditType.Rotation
        PrioEditor.FilePath = cmbRotation.SelectedValue
        PrioEditor.LoadAvailableElemnt()
        PrioEditor.OpenRotaForEdit(PrioEditor.FilePath)
        PrioEditor.show()

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
                cmbRotation.SelectedValue = PrioEditor.FilePath
        End Select


    End Sub

    Private Sub cmdEditCharacterWithGear_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdEditCharacterWithGear.Click
    

        Try
            GearSelector.FilePath = cmbGearSelector.SelectedItem.ToString
            GearSelector.Show
            GearSelector.LoadMycharacter()
        Catch Err As Exception

        
        End Try
    End Sub

    Sub GearSelector_close() Handles GearSelector.Closing
        loadWindow()
        cmbGearSelector.SelectedItem = GearSelector.FilePath
    End Sub
End Class
