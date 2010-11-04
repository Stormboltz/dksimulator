Imports System.Xml
Imports System.Net
Imports System.Xml.Linq
Imports System.Linq
Imports System.IO.IsolatedStorage
Imports System.IO
Partial Public Class FrmGearSelector
    Inherits UserControl
    Dim WithEvents UI As New UserInput
    Friend EquipmentList As New Collections.Generic.List(Of VisualEquipSlot)
    Friend InLoad As Boolean
    Friend EnchantSelector As New EnchantSelector(Me)
    Friend GemSelector As New GemSelector(Me)
    Friend GearSelector As New GearSelector(Me)
    Friend WithEvents ArmoryImport As New frmArmory
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

    Private StatSummary As frmStatSummary


    Dim space As Integer = 10

    Friend Food As Food
    Friend Flask As Flask


    Friend FilePath As String
    Dim LastDPSResult As Integer

    Friend ParentFrame As MainForm
    Friend EPvalues As EPValues

    Private Sub UI_closeEvent() Handles UI.Closing

        If UI.DialogResult Then
            If UI.txtInput.Text <> "" Then
                FilePath = UI.txtInput.Text & ".xml"
                ParentFrame.SaveMycharacter()


                ParentFrame.RefreshCharacterList(FilePath)

                'Me.Close()
            End If
        End If
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub cmdSaveAsNew() Handles cmdSaveNew.Click

        UI.Show()

    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
        ParentFrame.SaveMycharacter()
    End Sub

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)

    End Sub


    Public Sub New(ByVal PFrame As MainForm)
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()

        '
        ' TODO : Add constructor code after InitializeComponents
        '
        ParentFrame = PFrame
        EPvalues = New EPValues
        InitDisplay()
    End Sub

    Public Sub Init(ByVal PFrame As MainForm)
        ParentFrame = PFrame
        StatSummary = ParentFrame.StatSummary
        EPvalues = New EPValues
        InitDisplay()
    End Sub

    Function GetIDs(ByVal El As XElement) As String
        GetIDs = ""
        For Each SubElements In El.Elements("id")
            GetIDs += SubElements.Value
        Next
        Return GetIDs
    End Function
    Function TransformToSet(ByVal col As Collections.Generic.List(Of String)) As Collections.Generic.List(Of String)

        Dim s As String
        Dim i As Integer
        Dim cl As New Collections.Generic.List(Of String)
        For i = 0 To col.Count - 1
            Dim xElem As XElement
            xElem = (From el In SetBonusDB.Element("SetBonus").Elements
                      Where GetIDs(el).Contains(col(i))
                      Select el).First

            s = xElem.Attribute("name").Value
            s += xElem.Attribute("type").Value
            cl.Add(s)
        Next
        Return cl
    End Function

    Function CollectionDuplicateCount(ByVal col As Collections.Generic.List(Of String), ByVal Value As String) As Integer
        Dim i As Integer = 0
        Dim v As String
        For Each v In col
            If v = Value Then i += 1
        Next
        Return i
    End Function

    Sub CmdSaveClick(ByVal sender As Object, ByVal e As EventArgs)

        'Me.Close()
    End Sub



    Private Sub wc_OpenReadCompleted(ByVal sender As Object, ByVal e As OpenReadCompletedEventArgs)
        If e.Error IsNot Nothing Then
            msgBox(e.Error.InnerException.Message & " " & e.Error.InnerException.StackTrace)
            Diagnostics.Debug.WriteLine(e.Error.StackTrace)
            Return
        End If
        Using s As Stream = e.Result
            Dim reader As New StreamReader(s)
            Dim tmp As String
            tmp = reader.ReadToEnd()
            msgBox(tmp)
            ImportMyXMLCharacter(tmp)
        End Using
    End Sub
    Private Sub wc_OpenDonwloadCompleted(ByVal sender As Object, ByVal e As DownloadStringCompletedEventArgs)
        If e.Error IsNot Nothing Then
            msgBox(e.Error.InnerException.Message & " " & e.Error.InnerException.StackTrace)
            Diagnostics.Debug.WriteLine(e.Error.StackTrace)
            Return
        End If
        Dim s As String = e.Result

        'msgBox(s)
        ImportMyXMLCharacter(s)

    End Sub

    Sub ArmoryImport_Closing() Handles ArmoryImport.Closing
        If ArmoryImport.DialogResult = True Then
            ImportMyCharacter(ArmoryImport.cmbRegion.SelectedValue.ToString, ArmoryImport.txtServer.Text, ArmoryImport.txtCharacter.Text)
        End If
    End Sub




    Function ReadCallback(ByVal asynchronousResult As IAsyncResult)

        Dim request As WebRequest = CType(asynchronousResult.AsyncState, WebRequest)


        Dim postStream As Stream = request.EndGetRequestStream(asynchronousResult)
        Using streamReader1 As StreamReader = New StreamReader(postStream)
            Dim resultString As String = streamReader1.ReadToEnd()
            Diagnostics.Debug.WriteLine("Using HttpWebRequest: " + resultString)
            Return resultString
        End Using
    End Function


    Sub ImportMyXMLCharacter(ByVal xmltext As String)
        Try
            Dim xmlChar As XDocument = XDocument.Parse(xmltext)
            Dim iSlot As VisualEquipSlot
            Me.InLoad = True
            Dim charfound As Boolean = False
            Dim tmp As String = ""

            InLoad = True

            Try
                ParentFrame.cmbRace.SelectedItem = xmlChar.Element("page").Element("characterInfo").Element("character").Attribute("race").Value
            Catch ex As Exception
                Log.Log(ex.StackTrace, logging.Level.ERR)
            End Try
            Try
                ParentFrame.cmbFood.SelectedItem = Nothing
            Catch ex As Exception
                Log.Log(ex.StackTrace, logging.Level.ERR)
            End Try

            Try
                ParentFrame.cmbFlask.SelectedItem = Nothing
            Catch ex As Exception
                Log.Log(ex.StackTrace, logging.Level.ERR)
            End Try

            Dim xItem As XElement
            ParentFrame.cmbSkill1.SelectedItem = Nothing
            ParentFrame.cmbSkill2.SelectedItem = Nothing
            For Each xItem In xmlChar.Element("page").Element("characterInfo").Element("characterTab").Element("professions").Elements("skill")
                If ParentFrame.cmbSkill1.SelectedItem <> "" Then
                    ParentFrame.cmbSkill2.SelectedItem = xItem.Attribute("name").Value
                Else
                    ParentFrame.cmbSkill1.SelectedItem = xItem.Attribute("name").Value
                End If
            Next

            Try
                Dim d As Boolean
                d = ((From el As XElement In xmlChar.Element("page").Element("characterInfo").Element("characterTab").Element("items").Elements
                        Where el.Attribute("slot") = "16"
                        ).Count > 0)
                If d Then
                    StatSummary.rDW.IsChecked = True
                    StatSummary.r2Hand.IsChecked = False

                    Me.rd2H.IsChecked = False
                    Me.rdDW.IsChecked = True
                Else
                    StatSummary.rDW.IsChecked = False
                    StatSummary.r2Hand.IsChecked = True
                    Me.rd2H.IsChecked = True
                    Me.rdDW.IsChecked = False
                End If
            Catch ex As Exception
                Log.Log(ex.StackTrace, logging.Level.ERR)
                StatSummary.rDW.IsChecked = False
                StatSummary.r2Hand.IsChecked = True
                Me.rd2H.IsChecked = True
                Me.rdDW.IsChecked = False
            End Try

            For Each xItem In xmlChar.Element("page").Element("characterInfo").Element("characterTab").Element("items").Elements("item")
                charfound = True
                For Each iSlot In Me.EquipmentList
                    If iSlot.text = ArmorySlot2MySlot(xItem.Attribute("slot").Value) Then
                        Try
                            iSlot.Item.LoadItem(xItem.Attribute("id").Value)
                            iSlot.DisplayItem()
                            iSlot.Item.gem1.Attach(xItem.Attribute("gem0Id").Value)
                            iSlot.Item.gem2.Attach(xItem.Attribute("gem1Id").Value)
                            iSlot.Item.gem3.Attach(xItem.Attribute("gem2Id").Value)

                            iSlot.Item.Enchant.Attach(xItem.Attribute("permanentenchant").Value)

                        Catch ex As System.Exception
                            'Diagnostics.Debug.WriteLine (ex.ToString)
                        End Try

                    End If
                Next

            Next
            InLoad = False
            ParentFrame.GetStats()
        Catch ex As Exception
            Log.Log(ex.StackTrace, logging.Level.ERR)

        Finally
            Me.InLoad = False
        End Try
    End Sub


    Sub ImportMyCharacter(ByVal region As String, ByVal realmName As String, ByVal characterName As String)

        Dim url As String = "./armory.php?region=" & region.ToUpper & "&r=" & realmName & "&n=" & characterName
        Dim webClient As WebClient = New WebClient()
        AddHandler webClient.DownloadStringCompleted, AddressOf wc_OpenDonwloadCompleted
        Dim ur As New Uri(url, UriKind.Relative)
        webClient.DownloadStringAsync(ur)
        Exit Sub





    End Sub
    Function ArmorySlot2MySlot(ByVal armorySlotId As Integer) As String
        Select Case armorySlotId
            Case 0
                Return "Head"
            Case 1
                Return "Neck"
            Case 2
                Return "Shoulder"
            Case 3
                Return "Back"
            Case 4
                Return "Chest"
            Case 5
                Return "Waist"
            Case 6
                Return "Legs"
            Case 7
                Return "Feets"
            Case 8
                Return "Wrist"
            Case 9
                Return "Hand"
            Case 10
                Return "Finger1"
            Case 11
                Return "Finger2"
            Case 12
                Return "Trinket1"
            Case 13
                Return "Trinket2"
            Case 14
                Return "Back"
            Case 15
                If ParentFrame.StatSummary.rDW.IsChecked Then
                    Return "MainHand"
                Else
                    Return "TwoHand"
                End If

            Case 16
                Return "OffHand"
            Case 17
                Return "Sigil"

            Case Else
                Return ""
        End Select

    End Function

    Sub LoadMycharacter()

        InLoad = True

        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/CharactersWithGear/" & FilePath, FileMode.Open, FileAccess.Read, isoStore)
                Dim xmlChar As XDocument = XDocument.Load(isoStream)
                Try
                    Dim r As String = xmlChar.<character>.<ManualInput>.Value
                    If r <> "" Then ParentFrame.StatSummary.chkManualInput.IsChecked = xmlChar.<character>.<ManualInput>.Value

                    If ParentFrame.StatSummary.chkManualInput.IsChecked Then

                        StatSummary.txtStr.Text = xmlChar.<character>.<stat>.<Strength>.Value
                        StatSummary.txtAgi.Text = xmlChar.<character>.<stat>.<Agility>.Value
                        StatSummary.txtIntel.Text = xmlChar.<character>.<stat>.<Intel>.Value

                        StatSummary.txtStam.Text = xmlChar.<character>.<stat>.<Stamina>.Value
                        StatSummary.txtArmor.Text = xmlChar.<character>.<stat>.<Armor>.Value
                        StatSummary.txtAddArmor.Text = xmlChar.<character>.<stat>.<SpecialArmor>.Value
                        StatSummary.txtAP.Text = xmlChar.<character>.<stat>.<AttackPower>.Value
                        StatSummary.txtMast.Text = xmlChar.<character>.<stat>.<MasteryRating>.Value
                        StatSummary.txtHit.Text = xmlChar.<character>.<stat>.<HitRating>.Value
                        StatSummary.txtCrit.Text = xmlChar.<character>.<stat>.<CritRating>.Value
                        StatSummary.txtHaste.Text = xmlChar.<character>.<stat>.<HasteRating>.Value
                        StatSummary.txtArP.Text = xmlChar.<character>.<stat>.<ArmorPenetrationRating>.Value
                        StatSummary.txtExp.Text = xmlChar.<character>.<stat>.<ExpertiseRating>.Value


                        StatSummary.txtMHDPS.Text = xmlChar.<character>.<weapon>.<mainhand>.<dps>.Value
                        StatSummary.txtMHWSpeed.Text = xmlChar.<character>.<weapon>.<mainhand>.<speed>.Value

                        If xmlChar.<character>.<weapon>.<count>.Value = 2 Then
                            StatSummary.txtOHDPS.Text = xmlChar.<character>.<weapon>.<offhand>.<dps>.Value
                            StatSummary.txtOHWSpeed.Text = xmlChar.<character>.<weapon>.<offhand>.<speed>.Value
                        End If
                        Select Case xmlChar.<character>.<Set>.Elements.Count
                            Case 1
                                StatSummary.cmbSetBonus1.Text = xmlChar.<character>.<Set>.Elements.First.Name.ToString
                            Case 2
                                StatSummary.cmbSetBonus1.Text = xmlChar.<character>.<Set>.Elements.First.Name.ToString
                                StatSummary.cmbSetBonus2.Text = xmlChar.<character>.<Set>.Elements.Last.Name.ToString
                            Case Else

                        End Select

                        Select Case xmlChar.<character>.<trinket>.Elements.Count
                            Case 1
                                StatSummary.cmbTrinket1.Text = xmlChar.<character>.<trinket>.Elements.First.Name.ToString
                            Case 2
                                StatSummary.cmbTrinket1.Text = xmlChar.<character>.<trinket>.Elements.First.Name.ToString
                                StatSummary.cmbTrinket2.Text = xmlChar.<character>.<trinket>.Elements.Last.Name.ToString
                            Case Else

                        End Select


                        Select Case xmlChar.<character>.<WeaponProc>.Elements.Count
                            Case 1
                                StatSummary.cmbWeaponProc1.Text = xmlChar.<character>.<WeaponProc>.Elements.First.Name.ToString
                            Case 2
                                StatSummary.cmbWeaponProc1.Text = xmlChar.<character>.<WeaponProc>.Elements.First.Name.ToString
                                StatSummary.cmbWeaponProc2.Text = xmlChar.<character>.<WeaponProc>.Elements.Last.Name.ToString
                            Case Else

                        End Select

                        StatSummary.chkIngenieer.IsChecked = xmlChar.<character>.<misc>.<HandMountedPyroRocket>.Value
                        StatSummary.chkAccelerators.IsChecked = xmlChar.<character>.<misc>.<HyperspeedAccelerators>.Value
                        StatSummary.chkMeta.IsChecked = xmlChar.<character>.<misc>.<ChaoticSkyflareDiamond>.Value
                        StatSummary.chkTailorEnchant.IsChecked = xmlChar.<character>.<misc>.<TailorEnchant>.Value
                        StatSummary.chkAshenBand.IsChecked = xmlChar.<character>.<misc>.<AshenBand>.Value

                        StatSummary.txtMHExpBonus.Text = xmlChar.<character>.<racials>.<MHExpertiseBonus>.Value
                        StatSummary.txtOHExpBonus.Text = xmlChar.<character>.<racials>.<OHExpertiseBonus>.Value
                        StatSummary.chkBloodFury.IsChecked = xmlChar.<character>.<racials>.<Orc>.Value
                        StatSummary.chkBerzerking.IsChecked = xmlChar.<character>.<racials>.<Troll>.Value
                        StatSummary.chkArcaneTorrent.IsChecked = xmlChar.<character>.<racials>.<BloodElf>.Value
                        StatSummary.chkDraeni.IsChecked = xmlChar.<character>.<racials>.<Dreani>.Value
                        StatSummary.chkWorgen.IsChecked = xmlChar.<character>.<Worgen>.<Dreani>.Value
                        StatSummary.chkGoblin.IsChecked = xmlChar.<character>.<Goblin>.<Dreani>.Value
                    End If




                Catch ex As Exception

                End Try


                'Dim root As xml.XmlElement = xmlChar.DocumentElement
                Dim iSlot As VisualEquipSlot
                Try
                    Diagnostics.Debug.WriteLine(xmlChar.<character>.<race>.Value)
                    ParentFrame.cmbRace.SelectedValue = xmlChar.<character>.<race>.Value
                Catch ex As Exception
                    Log.Log("LoadMycharacter: Cannot get Race", logging.Level.WARNING)
                    ParentFrame.cmbRace.SelectedValue = "Orc"
                End Try


                Try
                    ParentFrame.cmbFood.SelectedValue = xmlChar.<character>.<food>.Value
                Catch ex As Exception
                    Log.Log("LoadMycharacter: Cannot get food", logging.Level.WARNING)
                    ParentFrame.cmbFood.SelectedValue = "AP Food"
                End Try

                Try
                    ParentFrame.cmbFlask.SelectedValue = xmlChar.<character>.<flask>.Value
                Catch ex As Exception
                    Log.Log("LoadMycharacter: Cannot get Flask", logging.Level.WARNING)
                    ParentFrame.cmbFlask.SelectedValue = "Flask of Endless Rage"
                End Try


                Try
                    ParentFrame.cmbSkill1.SelectedValue = xmlChar.<character>.<skill1>.Value
                Catch ex As Exception
                    Log.Log("LoadMycharacter: Cannot get skill1", logging.Level.WARNING)
                    ParentFrame.cmbSkill1.SelectedValue = "Jewelcrafting"
                End Try

                Try
                    ParentFrame.cmbSkill2.SelectedValue = xmlChar.<character>.<skill2>.Value
                Catch ex As Exception
                    Log.Log("LoadMycharacter: Cannot get skill2", logging.Level.WARNING)
                    ParentFrame.cmbSkill2.SelectedValue = "Blacksmithing"
                End Try



                Try
                    StatSummary.rDW.IsChecked = xmlChar.<character>.<DW>.Value
                    StatSummary.r2Hand.IsChecked = (StatSummary.rDW.IsChecked = False)
                    rdDW.IsChecked = xmlChar.<character>.<DW>.Value
                    rd2H.IsChecked = (xmlChar.<character>.<DW>.Value = False)
                Catch ex As Exception
                    Log.Log("LoadMycharacter: Cannot get DW", logging.Level.WARNING)
                    StatSummary.rDW.IsChecked = False
                    StatSummary.r2Hand.IsChecked = True
                    rdDW.IsChecked = False
                    rd2H.IsChecked = True
                End Try

                If StatSummary.r2Hand.IsChecked Then
                    Me.TwoHWeapSlot.Opacity = 1
                    Me.TwoHWeapSlot.IsHitTestVisible = True
                    Me.MHWeapSlot.Opacity = 0
                    Me.OHWeapSlot.Opacity = 0
                    Me.MHWeapSlot.IsHitTestVisible = False
                    Me.OHWeapSlot.IsHitTestVisible = False
                    rd2H.IsChecked = True
                    rdDW.IsChecked = False
                Else
                    Me.TwoHWeapSlot.Opacity = 0
                    Me.TwoHWeapSlot.IsHitTestVisible = False
                    Me.MHWeapSlot.Opacity = 1
                    Me.OHWeapSlot.Opacity = 1
                    Me.MHWeapSlot.IsHitTestVisible = True
                    Me.OHWeapSlot.IsHitTestVisible = True
                    rd2H.IsChecked = False
                    rdDW.IsChecked = True
                End If


                Dim itm As CheckBox

                For Each itm In ParentFrame.stackConsumable.Children
                    Try
                        itm.IsChecked = xmlChar.<character>.<misc>.Elements(itm.Name).Value
                    Catch ex As Exception
                        Log.Log("LoadMycharacter: Cannot get Consumable", logging.Level.WARNING)
                        itm.IsChecked = False
                    End Try
                Next

                For Each iSlot In Me.EquipmentList
                    Try
                        iSlot.Item.LoadItem(xmlChar.<character>.Elements(iSlot.Text).<id>.Value)
                        iSlot.DisplayItem()
                        Try
                            iSlot.Item.gem1.Attach(xmlChar.<character>.Elements(iSlot.Text).<gem1>.Value)
                        Catch ex As Exception
                            Log.Log("LoadMycharacter: Cannot get Gem1 for " & iSlot.Name, logging.Level.WARNING)

                        End Try
                        Try
                            iSlot.Item.gem2.Attach(xmlChar.<character>.Elements(iSlot.Text).<gem2>.Value)
                        Catch ex As Exception
                            Log.Log("LoadMycharacter: Cannot get Gem2 for " & iSlot.Name, logging.Level.WARNING)

                        End Try
                        Try
                            iSlot.Item.gem3.Attach(xmlChar.<character>.Elements(iSlot.Text).<gem3>.Value)
                        Catch ex As Exception
                            Log.Log("LoadMycharacter: Cannot get Gem3 for " & iSlot.Name, logging.Level.WARNING)
                        End Try
                        Try
                            iSlot.Item.Enchant.Attach(xmlChar.<character>.Elements(iSlot.Text).<enchant>.Value)
                        Catch ex As Exception
                            Log.Log("LoadMycharacter: Cannot get enchant for " & iSlot.Name, logging.Level.WARNING)
                        End Try

                        Try
                            iSlot.Item.ReForgingFrom = xmlChar.<character>.Elements(iSlot.Text).<reforge>.<from>.Value
                            iSlot.Item.ReForgingTo = xmlChar.<character>.Elements(iSlot.Text).<reforge>.<to>.Value
                            iSlot.Item.ReForgingvalue = xmlChar.<character>.Elements(iSlot.Text).<reforge>.<value>.Value
                        Catch ex As Exception
                            Log.Log("LoadMycharacter: Cannot get reforging for " & iSlot.Name, logging.Level.WARNING)
                        End Try

                       



                    Catch ex As Exception
                        Log.Log("LoadMycharacter: Cannot get " & iSlot.Name, logging.Level.WARNING)
                        iSlot.Item.Unload()
                        iSlot.DisplayItem()
                    End Try
                Next
            End Using
        End Using
        InLoad = False
        ParentFrame.GetStats()
    End Sub

    Sub Redraw()
        'With HeadSlot
        '    .Location = New System.Drawing.Point(space * 2, toolStrip1.Top + toolStrip1.Height + space)
        'End With
        'With NeckSlot
        '    .Location = New System.Drawing.Point(HeadSlot.Left, HeadSlot.Top + HeadSlot.Height + space)
        'End With
        'With ShoulderSlot
        '    .Location = New System.Drawing.Point(NeckSlot.Left, NeckSlot.Top + NeckSlot.Height + space)
        'End With
        'With BackSlot
        '    .Location = New System.Drawing.Point(ShoulderSlot.Left, ShoulderSlot.Top + ShoulderSlot.Height + space)
        'End With
        'With ChestSlot
        '    .Location = New System.Drawing.Point(BackSlot.Left, BackSlot.Top + BackSlot.Height + space)
        'End With
        'With WristSlot
        '    .Location = New System.Drawing.Point(ChestSlot.Left, ChestSlot.Top + ChestSlot.Height + space)
        'End With
        'With TwoHWeapSlot
        '    .Location = New System.Drawing.Point(WristSlot.Left, WristSlot.Top + WristSlot.Height + space)
        'End With
        'With MHWeapSlot
        '    .Location = New System.Drawing.Point(WristSlot.Left, WristSlot.Top + WristSlot.Height + space)
        'End With
        'With OHWeapSlot
        '    .Location = New System.Drawing.Point(MHWeapSlot.Left, MHWeapSlot.Top + MHWeapSlot.Height + space)
        'End With
        'With SigilSlot
        '    .Location = New System.Drawing.Point(OHWeapSlot.left, OHWeapSlot.Top + OHWeapSlot.Height + space)
        'End With
        'With HandSlot
        '    .Location = New System.Drawing.Point(HeadSlot.left + HeadSlot.Width + space, HeadSlot.Top)
        'End With
        'With BeltSlot
        '    .Location = New System.Drawing.Point(HandSlot.left, HandSlot.Top + HandSlot.Height + space)
        'End With
        'With LegSlot
        '    .Location = New System.Drawing.Point(BeltSlot.left, BeltSlot.Top + BeltSlot.Height + space)
        'End With
        'With FeetSlot
        '    .Location = New System.Drawing.Point(LegSlot.left, LegSlot.Top + LegSlot.Height + space)
        'End With
        'With ring1Slot
        '    .Location = New System.Drawing.Point(FeetSlot.left, FeetSlot.Top + FeetSlot.Height + space)
        'End With
        'With ring2Slot
        '    .Location = New System.Drawing.Point(ring1Slot.left, ring1Slot.Top + ring1Slot.Height + space)
        'End With
        'With Trinket1Slot
        '    .Location = New System.Drawing.Point(ring2Slot.left, ring2Slot.Top + ring2Slot.Height + space)
        'End With
        'With Trinket2Slot
        '    .Location = New System.Drawing.Point(Trinket1Slot.left, Trinket1Slot.Top + Trinket1Slot.Height + space)
        'End With
        'groupBox1.Left = Trinket1Slot.left + Trinket1Slot.Width + space
        'groupBox1.Top = HeadSlot.Top
        'Me.Size = New Size(groupBox1.Left + groupBox1.Width + space, SigilSlot.Top + SigilSlot.Height + space)
        'InLoad = False
    End Sub

    Sub InitDisplay()
        InLoad = True
        If IsNothing(ParentFrame.ItemDB) Then ParentFrame.LoadDB()

        ItemDB = ParentFrame.ItemDB
        GemDB = ParentFrame.GemDB
        GemBonusDB = ParentFrame.GemBonusDB
        EnchantDB = ParentFrame.EnchantDB
        trinketDB = ParentFrame.trinketDB
        SetBonusDB = ParentFrame.SetBonusDB
        WeapProcDB = ParentFrame.WeapProcDB
        FlaskDB = ParentFrame.FlaskDB
        FoodDB = ParentFrame.FoodDB
        ConsumableDB = ParentFrame.ConsumableDB
        Flask = New Flask(Me)
        Food = New Food(Me)



        'cmdExtrator.Hide()
        Dim x As XElement
        ParentFrame.cmbFlask.Items.Clear()
        ParentFrame.cmbFlask.Items.Add("")
        For Each x In FlaskDB.Element("flask").Elements("item")
            ParentFrame.cmbFlask.Items.Add(x.Element("name").Value)
        Next
        ParentFrame.cmbFood.Items.Clear()
        ParentFrame.cmbFood.Items.Add("")
        For Each x In FoodDB.Element("food").Elements("item")
            ParentFrame.cmbFood.Items.Add(x.Element("name").Value)
        Next
        'TODO
        Dim itm As CheckBox
        ParentFrame.stackConsumable.Children.Clear()
        For Each x In ConsumableDB.Elements("Consumables").Elements
            itm = New CheckBox
            itm.Name = x.Value.Replace(" ", "")
            itm.Content = itm.Name
            ParentFrame.stackConsumable.Children.Add(itm)
        Next
        ParentFrame.cmbRace.Items.Clear()
        ParentFrame.cmbRace.Items.Add("Blood Elf")
        ParentFrame.cmbRace.Items.Add("Draenei")
        ParentFrame.cmbRace.Items.Add("Dwarf")
        ParentFrame.cmbRace.Items.Add("Gnome")
        ParentFrame.cmbRace.Items.Add("Human")
        ParentFrame.cmbRace.Items.Add("Night Elf")
        ParentFrame.cmbRace.Items.Add("Orc")
        ParentFrame.cmbRace.Items.Add("Tauren")
        ParentFrame.cmbRace.Items.Add("Troll")
        ParentFrame.cmbRace.Items.Add("Undead")
        ParentFrame.cmbRace.Items.Add("Goblin")
        ParentFrame.cmbRace.Items.Add("Worgen")
        ParentFrame.cmbRace.SelectedIndex = 0

        ParentFrame.cmbSkill1.Items.Clear()
        ParentFrame.cmbSkill1.Items.Add("Alchemy")
        ParentFrame.cmbSkill1.Items.Add("Blacksmithing")
        ParentFrame.cmbSkill1.Items.Add("Enchanting")
        ParentFrame.cmbSkill1.Items.Add("Engineering")
        ParentFrame.cmbSkill1.Items.Add("Inscription")
        ParentFrame.cmbSkill1.Items.Add("Jewelcrafting")
        ParentFrame.cmbSkill1.Items.Add("Leatherworking")
        ParentFrame.cmbSkill1.Items.Add("Herb Gathering")
        ParentFrame.cmbSkill1.Items.Add("Mining")
        ParentFrame.cmbSkill1.Items.Add("Skinning")
        ParentFrame.cmbSkill1.Items.Add("Tailoring")
        ParentFrame.cmbSkill1.SelectedIndex = 0

        ParentFrame.cmbSkill2.Items.Clear()
        ParentFrame.cmbSkill2.Items.Add("Alchemy")
        ParentFrame.cmbSkill2.Items.Add("Blacksmithing")
        ParentFrame.cmbSkill2.Items.Add("Enchanting")
        ParentFrame.cmbSkill2.Items.Add("Engineering")
        ParentFrame.cmbSkill2.Items.Add("Inscription")
        ParentFrame.cmbSkill2.Items.Add("Jewelcrafting")
        ParentFrame.cmbSkill2.Items.Add("Leatherworking")
        ParentFrame.cmbSkill2.Items.Add("Herb Gathering")
        ParentFrame.cmbSkill2.Items.Add("Mining")
        ParentFrame.cmbSkill2.Items.Add("Skinning")
        ParentFrame.cmbSkill2.Items.Add("Tailoring")
        ParentFrame.cmbSkill2.SelectedIndex = 0

        EquipmentList.Clear()

        With HeadSlot
            .Text = "Head"
            .init(Me, 1)
            '.Location = New System.Drawing.Point(space * 2, toolStrip1.Top + toolStrip1.Height + space)

        End With

        With NeckSlot
            .Text = "Neck"
            .init(Me, 2)
            '.Location = New System.Drawing.Point(HeadSlot.Left, HeadSlot.Top + HeadSlot.Height + space)

        End With

        With ShoulderSlot
            .Text = "Shoulder"
            .init(Me, 3)
            '.Location = New System.Drawing.Point(NeckSlot.Left, NeckSlot.Top + NeckSlot.Height + space)
        End With


        With BackSlot
            .Text = "Back"
            .init(Me, 16)
            '.Location = New System.Drawing.Point(ShoulderSlot.Left, ShoulderSlot.Top + ShoulderSlot.Height + space)

        End With


        With ChestSlot
            .Text = "Chest"
            .init(Me, 5)
            '.Location = New System.Drawing.Point(BackSlot.Left, BackSlot.Top + BackSlot.Height + space)
        End With


        With WristSlot
            .Text = "Wrist"
            .init(Me, 9)
            '.Location = New System.Drawing.Point(ChestSlot.Left, ChestSlot.Top + ChestSlot.Height + space)

        End With



        With TwoHWeapSlot
            .Text = "TwoHand"
            .init(Me, 17)
            '.Location = New System.Drawing.Point(WristSlot.Left, WristSlot.Top + WristSlot.Height + space)
        End With


        With MHWeapSlot
            .Text = "MainHand"
            .init(Me, 13)
            '.Location = New System.Drawing.Point(WristSlot.Left, WristSlot.Top + WristSlot.Height + space)
            '.isOpacity = False
        End With


        With OHWeapSlot
            .Text = "OffHand"
            .init(Me, 13)
            '.Location = New System.Drawing.Point(MHWeapSlot.Left, MHWeapSlot.Top + MHWeapSlot.Height + space)
            '.Opacity = False
        End With


        With SigilSlot
            .Text = "Sigil"
            .init(Me, 28)
            '.Location = New System.Drawing.Point(OHWeapSlot.left, OHWeapSlot.Top + OHWeapSlot.Height + space)

        End With


        With HandSlot
            .Text = "Hand"
            .init(Me, 10)
            '.Location = New System.Drawing.Point(HeadSlot.left + HeadSlot.Width + space, HeadSlot.Top)

        End With


        With BeltSlot
            .Text = "Waist"
            .init(Me, 6)
            '.Location = New System.Drawing.Point(HandSlot.left, HandSlot.Top + HandSlot.Height + space)
        End With


        With LegSlot
            .Text = "Legs"
            .init(Me, 7)
            '.Location = New System.Drawing.Point(BeltSlot.left, BeltSlot.Top + BeltSlot.Height + space)

        End With

        With FeetSlot
            .Text = "Feets"
            .init(Me, 8)
            '.Location = New System.Drawing.Point(LegSlot.left, LegSlot.Top + LegSlot.Height + space)

        End With


        With ring1Slot
            .Text = "Finger1"
            .init(Me, 11)
            '.Location = New System.Drawing.Point(FeetSlot.left, FeetSlot.Top + FeetSlot.Height + space)

        End With


        With ring2Slot
            .Text = "Finger2"
            .init(Me, 11)
            ' .Location = New System.Drawing.Point(ring1Slot.left, ring1Slot.Top + ring1Slot.Height + space)

        End With


        With Trinket1Slot
            .Text = "Trinket1"
            .init(Me, 12)
            ' .Location = New System.Drawing.Point(ring2Slot.left, ring2Slot.Top + ring2Slot.Height + space)

        End With

        With Trinket2Slot
            .Text = "Trinket2"
            .init(Me, 12)
            '.Location = New System.Drawing.Point(Trinket1Slot.left, Trinket1Slot.Top + Trinket1Slot.Height + space)
        End With

        'groupBox1.Left = Trinket1Slot.left + Trinket1Slot.Width + space
        'groupBox1.Top = HeadSlot.Top
        'Me.Size = New Size(groupBox1.Left + groupBox1.Width + space, SigilSlot.Top + SigilSlot.Height + space)
        InLoad = False
    End Sub

    Sub MainFormLoad(ByVal sender As Object, ByVal e As EventArgs)

        'Dim xtr As New Extractor
        'xtr.Start
        'exit sub
        'Me.Size = New Size(980, 800)
        LoadMycharacter()
        'ImportMyCharacter
    End Sub









    Sub CmdSaveAsNewClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim truc As New UserInput
        truc.Show()


        If truc.txtInput.Text <> "" And truc.DialogResult = True Then
            FilePath = truc.txtInput.Text & ".xml"
            ParentFrame.SaveMycharacter()
            ParentFrame.RefreshCharacterList()
            ParentFrame.cmbGearSelector.SelectedValue = FilePath
        Else
            Exit Sub
        End If

    End Sub







    Sub CmdArmoryImportClick(ByVal sender As Object, ByVal e As EventArgs) Handles cmdArmoryImport.Click
        'ImportMyCharacter("EU", "Chants Eternels", "Kahorie")
        ArmoryImport.Show()
    End Sub





    Private Sub cmdViewXml_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdViewXml.Click
        Dim oldPath As String = FilePath


        Dim tmpPath As String
        tmpPath = "tempo.xml"
        FilePath = tmpPath
        ParentFrame.SaveMycharacter()
        Dim txtEditor As New myTextReader
        txtEditor.OpenFileFromISO("KahoDKSim/CharactersWithGear/" & tmpPath)
        txtEditor.Show()
        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            isoStore.DeleteFile("KahoDKSim/CharactersWithGear/" & tmpPath)
        End Using
        FilePath = oldPath
    End Sub

    Private Sub cmdQuickDPS_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.Cursor = Cursors.Wait
        Dim tmp As String
        Dim i As Integer
        tmp = Me.FilePath
        Me.FilePath = "tmp.xml"
        ParentFrame.SaveMycharacter()
        Me.ParentFrame.cmbGearSelector.Items.Add("tmp.xml")
        Me.ParentFrame.cmbGearSelector.SelectedItem = "tmp.xml"
        If Me.ParentFrame.LoadBeforeSim = True Then
            i = SimConstructor.GetFastDPS(Me.ParentFrame)
            'lblDPS.Content = i & " dps (" & i - LastDPSResult & ")"
            LastDPSResult = i
        End If

        Me.ParentFrame.cmbGearSelector.SelectedItem = tmp
        Me.FilePath = tmp
        Me.ParentFrame.cmbGearSelector.Items.Remove("tmp.xml")
        IsolatedStorageFile.GetUserStoreForApplication.DeleteFile("KahoDKSim/CharactersWithGear/tmp.xml")
        Me.Cursor = Cursors.Arrow
    End Sub


    Private Sub cmdQuickEP_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.Cursor = Cursors.Wait

        Dim tmp As String
        tmp = Me.FilePath
        Me.FilePath = "tmp.xml"
        ParentFrame.SaveMycharacter()
        Me.ParentFrame.cmbGearSelector.Items.Add("tmp.xml")
        Me.ParentFrame.cmbGearSelector.SelectedItem = "tmp.xml"
        If Me.ParentFrame.LoadBeforeSim = True Then
            SimConstructor.GetFastEPValue(Me.ParentFrame)
        End If
        Me.ParentFrame.cmbGearSelector.SelectedItem = tmp
        Me.FilePath = tmp
        Me.ParentFrame.cmbGearSelector.Items.Remove("tmp.xml")
        IsolatedStorageFile.GetUserStoreForApplication.DeleteFile("KahoDKSim/CharactersWithGear/tmp.xml")
        Dim dis As New EPDisplay(Me)
        Me.Cursor = Cursors.Arrow
        dis.Show()

    End Sub

    
    Event rd2H_Check(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
    Event rdDW_Check(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
    Private Sub rd2H_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rd2H.Checked
        RaiseEvent rd2H_Check(sender, e)
    End Sub

    Private Sub rdDW_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles rdDW.Checked
        RaiseEvent rdDW_Check(sender, e)
    End Sub
    Dim WithEvents TextEditor As mytextEditor

    Private Sub cmdImport_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles cmdImport.Click
        Dim xEdit As New mytextEditor
        TextEditor = xEdit
        TextEditor.Folder = "KahoDKSim/CharactersWithGear"
        TextEditor.Show()
    End Sub

    Private Sub TextEditor_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TextEditor.Closing
        If TextEditor.DialogResult Then
            FilePath = ""
            CType(App.Current.RootVisual, MainForm).RefreshCharacterList()
            CType(App.Current.RootVisual, MainForm).cmbGearSelector.SelectedValue = TextEditor.FileName
            FilePath = TextEditor.FileName
            LoadMycharacter()
        End If
    End Sub
End Class
