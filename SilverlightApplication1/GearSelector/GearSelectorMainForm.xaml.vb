Imports System.Xml
Imports System.Net
Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO

Partial Public Class GearSelectorMainForm
    Inherits ChildWindow


    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
    End Sub

    Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles CancelButton.Click
        Me.DialogResult = False
    End Sub
    Friend EquipmentList As New Collection
    Friend InLoad As Boolean
    Friend EnchantSelector As New EnchantSelector(Me)
    Friend GemSelector As New GemSelector(Me)
    Friend GearSelector As New GearSelector(Me)
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


    Dim space As Integer = 10

    Dim Food As Food
    Dim Flask As Flask


    Friend FilePath As String
    Dim LastDPSResult As Integer

    Friend ParentFrame As MainForm
    Friend EPvalues As EPValues

    Public Sub New(ByVal PFrame As MainForm)
        ' The Me.InitializeComponent call is required for Windows Forms designer support.
        Me.InitializeComponent()

        '
        ' TODO : Add constructor code after InitializeComponents
        '
        ParentFrame = PFrame
        EPvalues = New EPValues

    End Sub

    Sub CmdExtratorClick(ByVal sender As Object, ByVal e As EventArgs)
        'exit sub
        'Dim MyExtractor As New Extractor
        'MyExtractor.Start()
    End Sub



    Sub GetStats()
        If InLoad Then Exit Sub
        Dim iSlot As EquipSlot
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

        Dim cSetBonus As New Collection



        Select Case cmbRace.SelectedItem.ToString
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
        Strength += Food.Strength
        Agility += Food.Agility
        HasteRating += Food.HasteRating
        ExpertiseRating += Food.ExpertiseRating
        HitRating += Food.HitRating
        AttackPower += Food.AttackPower
        CritRating += Food.CritRating
        ArmorPenetrationRating += Food.ArmorPenetrationRating

        'Flask
        Strength += Flask.Strength
        Agility += Flask.Agility
        HasteRating += Flask.HasteRating
        ExpertiseRating += Flask.ExpertiseRating
        HitRating += Flask.HitRating
        AttackPower += Flask.AttackPower
        CritRating += Flask.CritRating
        ArmorPenetrationRating += Flask.ArmorPenetrationRating
        Armor += Flask.Armor


        txtMHExpBonus.Text = 0
        txtOHExpBonus.Text = 0

        For Each iSlot In Me.EquipmentList

            If iSlot.Item.Id = 0 Then GoTo NextItem
            If iSlot.SlotId = 17 And rDW.IsChecked Then GoTo NextItem
            If iSlot.SlotId = 13 And r2Hand.IsChecked Then GoTo NextItem

            Dim subc As Integer = ItemDB.Element("items").Element("item[id=" & iSlot.Item.Id & "]").Element("subclass").Value

            If iSlot.Content.ToString = "TwoHand" Or iSlot.Content.ToString = "MainHand" Then
                DPS1 = iSlot.Item.DPS
                Speed1 = iSlot.Item.Speed
                Try
                    cmbWeaponProc1.Text = "MH" & WeapProcDB.Element("/WeaponProcList/proc[@id=" & iSlot.Item.Id & "]").Attribute("name").Value
                Catch
                    cmbWeaponProc1.Text = ""
                End Try
                Select Case cmbRace.SelectedItem.ToString
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

            If iSlot.Content.ToString = "OffHand" Then
                DPS2 = iSlot.Item.DPS
                Speed2 = iSlot.Item.Speed

                Try
                    cmbWeaponProc2.Text = "MH" & WeapProcDB.Element("/WeaponProcList/proc[@id=" & iSlot.Item.Id & "]").Attribute("name").Value
                Catch
                    cmbWeaponProc1.Text = ""
                End Try

                Select Case cmbRace.SelectedItem.ToString
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

            If iSlot.Content.ToString = "Trinket1" Then
                Try
                    cmbTrinket1.Text = trinketDB.Element("/TrinketList/trinket[@id=" & iSlot.Item.Id & "]").Attribute("name").Value
                Catch
                    cmbTrinket1.Text = ""
                End Try
            End If
            If iSlot.Content.ToString = "Trinket2" Then
                Try
                    cmbTrinket2.Text = trinketDB.Element("/TrinketList/trinket[@id=" & iSlot.Item.Id & "]").Attribute("name").Value
                Catch
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
                        Strength += GemBonusDB.Element("/bonus/item[id=" & iSlot.Item.gembonus & "]/Strength").Value
                        Agility += GemBonusDB.Element("/bonus/item[id=" & iSlot.Item.gembonus & "]/Agility").Value
                        HasteRating += GemBonusDB.Element("/bonus/item[id=" & iSlot.Item.gembonus & "]/HasteRating").Value
                        ExpertiseRating += GemBonusDB.Element("/bonus/item[id=" & iSlot.Item.gembonus & "]/ExpertiseRating").Value
                        HitRating += GemBonusDB.Element("/bonus/item[id=" & iSlot.Item.gembonus & "]/HitRating").Value
                        AttackPower += GemBonusDB.Element("/bonus/item[id=" & iSlot.Item.gembonus & "]/AttackPower").Value
                        CritRating += GemBonusDB.Element("/bonus/item[id=" & iSlot.Item.gembonus & "]/CritRating").Value
                        ArmorPenetrationRating += GemBonusDB.Element("/bonus/item[id=" & iSlot.Item.gembonus & "]/ArmorPenetrationRating").Value
                    Catch
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
        If cmbRace.SelectedItem = "Orc" Then
            chkBloodFury.IsChecked = True
        End If
        ' Berzerking
        If cmbRace.SelectedItem = "Troll" Then
            chkBerzerking.IsChecked = True
        End If
        ' Arcane torrent
        If cmbRace.SelectedItem = "Blood Elf" Then
            chkArcaneTorrent.IsChecked = True
        End If
        ' Set bonus1
        If cSetBonus.Count > 0 Then
            'cSetBonus.Sort()
            cSetBonus = TransformToSet(cSetBonus)
            Dim i As Integer
            Dim sId As String

            Do Until cSetBonus.Count = 0

                sId = cSetBonus.Item(0)
                i = CollectionDuplicateCount(cSetBonus, cSetBonus.Item(0))
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
        lblArP.Content = "Armor Penetration Rating(" & toDDecimal(ArmorPenetrationRating / 13.99) & ")"

        txtCrit.Text = CritRating
        lblCrit.Content = "CritRating(" & toDDecimal(CritRating / 45.91) & "%)"
        txtIntel.Text = Intel
        txtMHDPS.Text = DPS1
        txtMHWSpeed.Text = Speed1
        txtOHDPS.Text = DPS2
        txtOHWSpeed.Text = Speed2
    End Sub
    Function TransformToSet(ByVal col As Collection) As Collection
        Dim s As String
        Dim i As Integer
        Dim cl As New Collection
        For i = 0 To col.Count - 1
            s = SetBonusDB.Element("/SetBonus/set[id=" & col.Item(i) & "]").Attribute("name").Value
            s += SetBonusDB.Element("/SetBonus/set[id=" & col.Item(i) & "]").Attribute("type").Value
            cl.Add(s)
        Next
        Return cl
    End Function

    Function CollectionDuplicateCount(ByVal col As Collection, ByVal Value As String) As Integer
        Dim i As Integer = 0
        Dim v As String
        For Each v In col
            If v = Value Then i += 1
        Next
        Return i
    End Function





    Sub CmdSaveClick(ByVal sender As Object, ByVal e As EventArgs)
        SaveMycharacter()
        Me.Close()
    End Sub

    Sub SaveMycharacter()
        Dim xmlChar As New XDocument
        xmlChar.Parse("<character/>")

        'Sample
        'xmlChar.Element("character").Add( _
        '    New XElement("person", _
        '        New XElement("firstName", p2.FirstName), _
        '        New XElement("lastName", p2.LastName), _
        '        New XElement("address", _
        '            New XAttribute("city", p2.Address.City), _
        '            New XAttribute("country", p2.Address.Country) _
        '            ) _ 
        '        )_
        '    )

        xmlChar.Element("character").Add(New XElement("race", cmbRace.SelectedItem))
        xmlChar.Element("character").Add(New XElement("skill1", cmbSkill1.SelectedItem))
        xmlChar.Element("character").Add(New XElement("skill2", cmbSkill2.SelectedItem))
        xmlChar.Element("character").Add(New XElement("food", cmbFood.SelectedItem))
        xmlChar.Element("character").Add(New XElement("flask", cmbFlask.SelectedItem))
        xmlChar.Element("character").Add(New XElement("DW", rDW.IsChecked.ToString))
        xmlChar.Element("character").Add(New XElement("TwoHand", r2Hand.IsChecked.ToString))


        Dim iSlot As EquipSlot
        For Each iSlot In Me.EquipmentList
            xmlChar.Element("character").Add(New XElement(iSlot.Content.ToString))
            'TODO Add id + gems + enchant
            '<Head>
            '  <id>51312</id>
            '  <gem1>41398</gem1>
            '  <gem2>42142</gem2>
            '  <gem3>0</gem3>
            '  <enchant>1</enchant>
            '</Head>
        Next


        '<stat>
        '  <Strength>2577</Strength>
        '  <Agility>158</Agility>
        '  <Intel>35</Intel>
        '  <Armor>16349</Armor>
        '  <AttackPower>727</AttackPower>
        '  <HitRating>223</HitRating>
        '  <CritRating>1219</CritRating>
        '  <HasteRating>83</HasteRating>
        '  <ArmorPenetrationRating>848</ArmorPenetrationRating>
        '  <ExpertiseRating>158</ExpertiseRating>
        '</stat>
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
        xmlChar.Element("character").Element("stat").Add(New XElement("ExpertiseRating", CheckForInt(txtStr.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("Strength", CheckForInt(txtExp.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("Strength", CheckForInt(txtStr.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("Strength", CheckForInt(txtStr.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("Strength", CheckForInt(txtStr.Text)))
        xmlChar.Element("character").Element("stat").Add(New XElement("Strength", CheckForInt(txtStr.Text)))

        '<weapon>
        '   <count>1</count>
        '   <mainhand>
        '     <dps>344.1</dps>
        '     <speed>3.70</speed>
        '   </mainhand>
        '   <offhand>
        '     <dps>0</dps>
        '     <speed>0</speed>
        '   </offhand>
        ' </weapon>
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

        xmlChar.Element("character").Element("weapon").Element("offhand").Add(New XElement("dps", CheckForDouble(txtMHDPS.Text)))
        xmlChar.Element("character").Element("weapon").Element("offhand").Add(New XElement("speed", CheckForDouble(txtMHWSpeed.Text)))



        '<Set>
        '    <T104PDPS>1</T104PDPS>
        '    <T102PDPS>1</T102PDPS>
        '</Set>
        xmlChar.Element("character").Add(New XElement("Set", ""))
        If cmbSetBonus1.Text <> "" Then
            xmlChar.Element("character").Element("Set").Add(New XElement(cmbSetBonus1.Text, 1))
        End If
        If cmbSetBonus2.Text <> "" Then
            xmlChar.Element("character").Element("Set").Add(New XElement(cmbSetBonus2.Text, 1))
        End If




        '<trinket>
        '  <DeathbringersWill>1</DeathbringersWill>
        '  <DeathChoiceHeroic>1</DeathChoiceHeroic>
        '</trinket>




        xmlChar.Element("character").Add(New XElement("trinket", ""))

        If cmbTrinket1.Text <> "" Then
            xmlChar.Element("character").Element("trinket").Add(New XElement(cmbTrinket1.Text, 1))
        End If
        If cmbTrinket2.Text <> "" Then
            xmlChar.Element("character").Element("trinket").Add(New XElement(cmbTrinket2.Text, 1))
        End If

        '<WeaponProc>
        '  <MHShadowmourne>1</MHShadowmourne>
        '</WeaponProc>
        xmlChar.Element("character").Add(New XElement("WeaponProc", ""))
        If cmbWeaponProc1.Text <> "" Then
            xmlChar.Element("character").Element("WeaponProc").Add(New XElement(cmbWeaponProc1.Text, 1))
        End If
        If cmbWeaponProc2.Text <> "" Then
            xmlChar.Element("character").Element("WeaponProc").Add(New XElement(cmbWeaponProc2.Text, 1))
        End If


        '<misc>
        '  <HandMountedPyroRocket>False</HandMountedPyroRocket>
        '  <HyperspeedAccelerators>False</HyperspeedAccelerators>
        '  <ChaoticSkyflareDiamond>True</ChaoticSkyflareDiamond>
        '  <TailorEnchant>False</TailorEnchant>
        '  <AshenBand>True</AshenBand>
        '</misc>

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



        'Racials
        '<racials>
        '  <MHExpertiseBonus>0</MHExpertiseBonus>
        '  <OHExpertiseBonus>0</OHExpertiseBonus>
        '  <Orc>False</Orc>
        '  <Troll>False</Troll>
        '  <BloodElf>False</BloodElf>
        '</racials>
        xmlChar.Element("character").Add(New XElement("racials", ""))
        xmlChar.Element("character").Element("racials").Add(New XElement("MHExpertiseBonus", CheckForInt(txtMHExpBonus.Text)))
        xmlChar.Element("character").Element("racials").Add(New XElement("OHExpertiseBonus", CheckForInt(txtOHExpBonus.Text)))
        xmlChar.Element("character").Element("racials").Add(New XElement("Orc", chkBloodFury.IsChecked))
        xmlChar.Element("character").Element("racials").Add(New XElement("Troll", chkBerzerking.IsChecked))
        xmlChar.Element("character").Element("racials").Add(New XElement("BloodElf", chkArcaneTorrent.IsChecked))



        'xmlChar.Save()
    End Sub


    Sub ImportMyCharacter(ByVal region As String, ByVal realmName As String, ByVal characterName As String)

        '    Dim webClient As WebClient = New WebClient()

        '    webClient.QueryString.Add("r", realmName)
        '    webClient.QueryString.Add("n", characterName)
        '    webClient.Headers.Add("user-agent", "MSIE 7.0")
        '    webClient.Proxy = WebRequest.GetSystemWebProxy
        '    webClient.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials
        '    Dim xmlReaderSettings As XmlReaderSettings = New XmlReaderSettings()
        '    xmlReaderSettings.IgnoreComments = True
        '    xmlReaderSettings.IgnoreWhitespace = True
        '    Dim iSlot As EquipSlot
        '    Me.InLoad = True
        '    Dim charfound As Boolean = False
        '    Dim tmp As String = ""
        '    Try
        '        Dim xmlReader As XmlReader

        '        Select Case region
        '            Case "US"
        '                xmlReader = xmlReader.Create(webClient.OpenRead("http://www.wowarmory.com/character-sheet.xml"), xmlReaderSettings)
        '            Case "EU"
        '                xmlReader = xmlReader.Create(webClient.OpenRead("http://eu.wowarmory.com/character-sheet.xml"), xmlReaderSettings)
        '            Case "TW"
        '                xmlReader = xmlReader.Create(webClient.OpenRead("http://tw.wowarmory.com/character-sheet.xml"), xmlReaderSettings)
        '            Case "CN"
        '                xmlReader = xmlReader.Create(webClient.OpenRead("http://cn.wowarmory.com/character-sheet.xml"), xmlReaderSettings)
        '            Case "KR"
        '                xmlReader = xmlReader.Create(webClient.OpenRead("http://kr.wowarmory.com/character-sheet.xml"), xmlReaderSettings)
        '            Case Else
        '                xmlReader = Nothing
        '                Diagnostics.Diagnostics.Debug.WriteLine("region unknown: " & region)
        '        End Select




        '        Dim xmlChar As New xdocument


        '        Do While xmlReader.Read()
        '            tmp += xmlReader.ReadInnerXml
        '        Loop
        '        xmlChar.parse("<page globalSearch='1' lang='en_us' requestUrl='/character-sheet.xml'> " & tmp & "</page>")
        '        InLoad = True

        '        Try
        '            cmbRace.SelectedItem = xmlChar.Element("/page/characterInfo/character").Attribute("race").Value
        '        Catch
        '        End Try


        '        Try
        '            cmbFood.SelectedItem = Nothing
        '        Catch
        '        End Try

        '        Try
        '            cmbFlask.SelectedItem = Nothing
        '        Catch
        '        End Try

        '        Dim xItem As XmlNode
        '        cmbSkill1.SelectedItem = Nothing
        '        cmbSkill2.SelectedItem = Nothing
        '        For Each xItem In xmlChar.SelectNodes("/page/characterInfo/characterTab/professions/skill")
        '            If cmbSkill1.SelectedItem <> "" Then
        '                cmbSkill2.SelectedItem = xItem.Attribute("name").Value
        '            Else
        '                cmbSkill1.SelectedItem = xItem.Attribute("name").Value
        '            End If
        '        Next

        '        Try
        '            If xmlChar.Element("/page/characterInfo/characterTab/items/item[@slot=16]").OuterXml <> "" Then
        '                rDW.Ischecked = True
        '                r2Hand.Ischecked = False
        '            End If

        '        Catch
        '            rDW.Ischecked = False
        '            r2Hand.Ischecked = True
        '        End Try

        '        Dim itm As System.Windows.Forms.ToolStripMenuItem

        '        For Each itm In ddConsumable.DropDownItems
        '            itm.Ischecked = False
        '        Next


        '        For Each xItem In xmlChar.SelectNodes("/page/characterInfo/characterTab/items/item")
        '            charfound = True
        '            For Each iSlot In Me.EquipmentList
        '                If iSlot.Text = ArmorySlot2MySlot(xItem.Attribute("slot").Value) Then
        '                    Try
        '                        iSlot.Item.LoadItem(xItem.Attribute("id").Value)
        '                        iSlot.DisplayItem()
        '                        iSlot.Item.gem1.Attach(xItem.Attribute("gem0Id").Value)
        '                        iSlot.Item.gem2.Attach(xItem.Attribute("gem1Id").Value)
        '                        iSlot.Item.gem3.Attach(xItem.Attribute("gem2Id").Value)
        '                        iSlot.DisplayGem()
        '                        iSlot.Item.Enchant.Attach(xItem.Attribute("permanentenchant").Value)
        '                        iSlot.DisplayEnchant()
        '                    Catch ex As System.Exception
        '                        'Diagnostics.Debug.WriteLine (ex.ToString)
        '                    End Try

        '                End If
        '            Next

        '        Next
        '        InLoad = False
        '        GetStats()
        '    Catch ex As Exception

        '    Finally

        '        If charfound = False Then
        '            msgbox("Unable to retrieve the character")
        '        End If
        '        Me.InLoad = False
        '    End Try




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
                If rDW.IsChecked Then
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
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/CharactersWithGear/" & FilePath, FileMode.Open, isoStore)
                Dim xmlChar As XDocument = XDocument.Load(isoStream)


                'Dim root As xml.XmlElement = xmlChar.DocumentElement
                Dim iSlot As EquipSlot
                Try
                    cmbRace.SelectedItem = xmlChar.Element("character").Element("race").Value
                Catch
                End Try


                Try
                    cmbFood.SelectedItem = xmlChar.Element("character").Element("food").Value
                Catch
                End Try

                Try
                    cmbFlask.SelectedItem = xmlChar.Element("character").Element("flask").Value
                Catch
                End Try


                Try
                    cmbSkill1.SelectedItem = xmlChar.Element("character").Element("skill1").Value
                Catch
                End Try

                Try
                    cmbSkill2.SelectedItem = xmlChar.Element("character").Element("skill2").Value
                Catch
                End Try



                Try
                    rDW.IsChecked = xmlChar.Element("character").Element("DW").Value
                Catch
                End Try


                Try
                    r2Hand.IsChecked = xmlChar.Element("character").Element("TwoHand").Value

                Catch
                End Try


                Dim itm As CheckBox

                For Each itm In stackConsumable.Children
                    Try
                        itm.IsChecked = xmlChar.Element("character").Element("misc").Element(itm.Name).Value
                    Catch
                        itm.IsChecked = False
                    End Try
                Next





                For Each iSlot In Me.EquipmentList
                    Try
                        iSlot.Item.LoadItem(xmlChar.Element("character").Element(iSlot.text).Element("id").Value)
                        iSlot.DisplayItem()
                        Try
                            iSlot.Item.gem1.Attach(xmlChar.Element("character").Element(iSlot.text).Element("gem1").Value)
                        Catch
                        End Try
                        Try
                            iSlot.Item.gem2.Attach(xmlChar.Element("character").Element(iSlot.text).Element("gem2").Value)
                        Catch
                        End Try
                        Try
                            iSlot.Item.gem3.Attach(xmlChar.Element("character").Element(iSlot.text).Element("gem3").Value)
                        Catch
                        End Try
                        iSlot.DisplayGem()

                        Try
                            iSlot.Item.Enchant.Attach(xmlChar.Element("character").Element(iSlot.text).Element("enchant").Value)
                            iSlot.DisplayEnchant()
                        Catch
                        End Try



                    Catch er As Exception
                        Diagnostics.Debug.WriteLine(er.ToString)
                    End Try
                Next
            End Using
        End Using
        InLoad = False
        GetStats()
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
        cmbFlask.Items.Clear()
        cmbFlask.Items.Add("")
        For Each x In FlaskDB.Element("flask").Elements("item")
            cmbFlask.Items.Add(x.Element("name").Value)
        Next
        cmbFood.Items.Clear()
        cmbFood.Items.Add("")
        For Each x In FoodDB.Element("food").Elements("item")
            cmbFood.Items.Add(x.Element("name").Value)
        Next
        'TODO
        Dim itm As CheckBox
        stackConsumable.Children.Clear()
        For Each x In ConsumableDB.Elements("Consumables").Elements
            itm = New CheckBox
            itm.Name = x.Value.Replace(" ", "")
            itm.Content = itm.Name
            stackConsumable.Children.Add(itm)
        Next

        cmbRace.Items.Add("Blood Elf")
        cmbRace.Items.Add("Draenei")
        cmbRace.Items.Add("Dwarf")
        cmbRace.Items.Add("Gnome")
        cmbRace.Items.Add("Human")
        cmbRace.Items.Add("Night Elf")
        cmbRace.Items.Add("Orc")
        cmbRace.Items.Add("Tauren")
        cmbRace.Items.Add("Troll")
        cmbRace.Items.Add("Undead")
        cmbRace.Items.Add("Goblin")
        cmbRace.Items.Add("Worgen")
        cmbRace.SelectedIndex = 0


        cmbSkill1.Items.Add("Alchemy")
        cmbSkill1.Items.Add("Blacksmithing")
        cmbSkill1.Items.Add("Enchanting")
        cmbSkill1.Items.Add("Engineering")
        cmbSkill1.Items.Add("Inscription")
        cmbSkill1.Items.Add("Jewelcrafting")
        cmbSkill1.Items.Add("Leatherworking")
        cmbSkill1.Items.Add("Herb Gathering")
        cmbSkill1.Items.Add("Mining")
        cmbSkill1.Items.Add("Skinning")
        cmbSkill1.Items.Add("Tailoring")
        cmbSkill1.SelectedIndex = 0

        cmbSkill2.Items.Add("Alchemy")
        cmbSkill2.Items.Add("Blacksmithing")
        cmbSkill2.Items.Add("Enchanting")
        cmbSkill2.Items.Add("Engineering")
        cmbSkill2.Items.Add("Inscription")
        cmbSkill2.Items.Add("Jewelcrafting")
        cmbSkill2.Items.Add("Leatherworking")
        cmbSkill2.Items.Add("Herb Gathering")
        cmbSkill2.Items.Add("Mining")
        cmbSkill2.Items.Add("Skinning")
        cmbSkill2.Items.Add("Tailoring")
        cmbSkill2.SelectedIndex = 0


        With HeadSlot
            .text = "Head"
            .init(Me, 1)
            '.Location = New System.Drawing.Point(space * 2, toolStrip1.Top + toolStrip1.Height + space)

        End With

        With NeckSlot
            .text = "Neck"
            .init(Me, 2)
            '.Location = New System.Drawing.Point(HeadSlot.Left, HeadSlot.Top + HeadSlot.Height + space)

        End With

        With ShoulderSlot
            .text = "Shoulder"
            .init(Me, 3)
            '.Location = New System.Drawing.Point(NeckSlot.Left, NeckSlot.Top + NeckSlot.Height + space)
        End With


        With BackSlot
            .text = "Back"
            .init(Me, 16)
            '.Location = New System.Drawing.Point(ShoulderSlot.Left, ShoulderSlot.Top + ShoulderSlot.Height + space)

        End With


        With ChestSlot
            .text = "Chest"
            .init(Me, 5)
            '.Location = New System.Drawing.Point(BackSlot.Left, BackSlot.Top + BackSlot.Height + space)
        End With


        With WristSlot
            .text = "Wrist"
            .init(Me, 9)
            '.Location = New System.Drawing.Point(ChestSlot.Left, ChestSlot.Top + ChestSlot.Height + space)

        End With



        With TwoHWeapSlot
            .text = "TwoHand"
            .init(Me, 17)
            '.Location = New System.Drawing.Point(WristSlot.Left, WristSlot.Top + WristSlot.Height + space)
        End With


        With MHWeapSlot
            .text = "MainHand"
            .init(Me, 13)
            '.Location = New System.Drawing.Point(WristSlot.Left, WristSlot.Top + WristSlot.Height + space)
            '.isOpacity = False
        End With


        With OHWeapSlot
            .text = "OffHand"
            .init(Me, 13)
            '.Location = New System.Drawing.Point(MHWeapSlot.Left, MHWeapSlot.Top + MHWeapSlot.Height + space)
            '.Opacity = False
        End With


        With SigilSlot
            .text = "Sigil"
            .init(Me, 28)
            '.Location = New System.Drawing.Point(OHWeapSlot.left, OHWeapSlot.Top + OHWeapSlot.Height + space)

        End With


        With HandSlot
            .text = "Hand"
            .init(Me, 10)
            '.Location = New System.Drawing.Point(HeadSlot.left + HeadSlot.Width + space, HeadSlot.Top)

        End With


        With BeltSlot
            .text = "Waist"
            .init(Me, 6)
            '.Location = New System.Drawing.Point(HandSlot.left, HandSlot.Top + HandSlot.Height + space)
        End With


        With LegSlot
            .text = "Legs"
            .init(Me, 7)
            '.Location = New System.Drawing.Point(BeltSlot.left, BeltSlot.Top + BeltSlot.Height + space)

        End With

        With FeetSlot
            .text = "Feets"
            .init(Me, 8)
            '.Location = New System.Drawing.Point(LegSlot.left, LegSlot.Top + LegSlot.Height + space)

        End With


        With ring1Slot
            .text = "Finger1"
            .init(Me, 11)
            '.Location = New System.Drawing.Point(FeetSlot.left, FeetSlot.Top + FeetSlot.Height + space)

        End With


        With ring2Slot
            .text = "Finger2"
            .init(Me, 11)
            ' .Location = New System.Drawing.Point(ring1Slot.left, ring1Slot.Top + ring1Slot.Height + space)

        End With


        With Trinket1Slot
            .text = "Trinket1"
            .init(Me, 12)
            ' .Location = New System.Drawing.Point(ring2Slot.left, ring2Slot.Top + ring2Slot.Height + space)

        End With

        With Trinket2Slot
            .text = "Trinket2"
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


    Sub CmbRaceSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        GetStats()
    End Sub

    Sub RDWCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        If rDW.IsChecked Then
            r2Hand.IsChecked = False
            txtOHDPS.IsEnabled = True
            txtOHWSpeed.IsEnabled = True
            Dim eq As EquipSlot
            For Each eq In EquipmentList
                If eq.text = "TwoHand" Then eq.Opacity = False
                If eq.text = "MainHand" Then eq.Opacity = True
                If eq.text = "OffHand" Then eq.Opacity = True
            Next
            GetStats()
        End If
    End Sub

    Sub R2HandCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        If r2Hand.IsChecked Then
            rDW.IsChecked = False
            txtOHDPS.IsEnabled = False
            txtOHWSpeed.IsEnabled = False
            Dim eq As EquipSlot
            For Each eq In EquipmentList
                If eq.text = "TwoHand" Then eq.Opacity = True
                If eq.text = "MainHand" Then eq.Opacity = False
                If eq.text = "OffHand" Then eq.Opacity = False
            Next
            GetStats()
        End If
    End Sub



    Sub CmdGetDpsClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim tmp As String
        Dim i As Integer
        tmp = Me.FilePath
        Me.FilePath = "tmp.xml"
        SaveMycharacter()
        Me.ParentFrame.cmbGearSelector.Items.Add("tmp.xml")
        Me.ParentFrame.cmbGearSelector.SelectedItem = "tmp.xml"
        If Me.ParentFrame.LoadBeforeSim = True Then
            i = SimConstructor.GetFastDPS(Me.ParentFrame)
            lblDPS.Content = i & " dps (" & i - LastDPSResult & ")"
            LastDPSResult = i
        End If

        Me.ParentFrame.cmbGearSelector.SelectedItem = tmp
        Me.FilePath = tmp
        Me.ParentFrame.cmbGearSelector.Items.Remove("tmp.xml")
        'My.Computer.FileSystem.DeleteFile( "\CharactersWithGear\" & "tmp.xml")
    End Sub

    Sub TsGetQuickEPClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim tmp As String
        tmp = Me.FilePath
        Me.FilePath = "tmp.xml"
        SaveMycharacter()
        Me.ParentFrame.cmbGearSelector.Items.Add("tmp.xml")
        Me.ParentFrame.cmbGearSelector.SelectedItem = "tmp.xml"
        If Me.ParentFrame.LoadBeforeSim = True Then
            SimConstructor.GetFastEPValue(Me.ParentFrame)
        End If
        Me.ParentFrame.cmbGearSelector.SelectedItem = tmp
        Me.FilePath = tmp
        Me.ParentFrame.cmbGearSelector.Items.Remove("tmp.xml")
        'FileSystem.DeleteFile( & "\CharactersWithGear\" & "tmp.xml")
        Dim dis As New EPDisplay(Me)
        dis.Show()
    End Sub



    Sub CmdSaveAsNewClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim truc As New UserInput
        truc.Show()


        If truc.txtInput.Text <> "" And truc.DialogResult = True Then
            FilePath = truc.txtInput.Text & ".xml"
            SaveMycharacter()
            truc.Close()
            Me.Close()
        Else
            Exit Sub
        End If

    End Sub


    Sub CmbSkillClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim eq As EquipSlot
        If InLoad Then Exit Sub
        InLoad = True
        For Each eq In EquipmentList
            eq.Item.LoadItem(eq.Item.Id)
            eq.DisplayItem()
        Next
        InLoad = False
        GetStats()
    End Sub

    Sub CmbFlaskSelectionChange(ByVal sender As Object, ByVal e As EventArgs)
        Flask.Attach(cmbFlask.SelectedItem.ToString)
        GetStats()
    End Sub

    Sub cmbFoodSelectionChange(ByVal sender As Object, ByVal e As EventArgs)
        Food.Attach(cmbFood.SelectedItem.ToString)
        GetStats()
    End Sub



    Sub GreedyToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs)
        Diagnostics.Debug.WriteLine(e.ToString)
    End Sub

    Sub CmdArmoryImportClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim f As New frmArmoryImport
        If f.DialogResult = True Then
            ImportMyCharacter(f.cmbRegion.SelectedItem, f.txtServer.Text, f.txtCharacter.Text)
        End If
        f.Close()
    End Sub

    Private Sub ChildWindow_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        InitDisplay()
    End Sub
End Class
