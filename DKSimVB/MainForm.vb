'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 18:44
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System.Xml
Imports System.Net

Public Partial Class MainForm
	Private EditorFilePAth As String
	Private TemplatePath As String
	Friend btList As New collection
	Friend colTrinketEP As New Collection
	Friend EPVal As New EPValues
	Friend ItemDB As New Xml.XmlDocument
	Friend GemDB As New Xml.XmlDocument
	Friend GemBonusDB As New Xml.XmlDocument
	Friend EnchantDB As New Xml.XmlDocument
	Friend trinketDB As New Xml.XmlDocument
	Friend SetBonusDB As New Xml.XmlDocument
	Friend WeapProcDB As New Xml.XmlDocument
	Friend FoodDB As new  Xml.XmlDocument
	Friend FlaskDB As New Xml.XmlDocument
	Friend ConsumableDB as New Xml.XmlDocument
	Friend gearSelector As GearSelectorMainForm
	
	
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		
	End Sub
	
	Function LoadBeforeSim() As Boolean
		
		saveConfig
		Try
			debug.Print( Application.StartupPath & "\Templates\"  & CmbTemplate.SelectedItem.ToString)
		Catch
			msgbox("Could not determine template. Please reselect it.")
			exit function
		End try
		
		SaveEPOptions()
		SaveBuffOption()
		saveScaling()
		saveTankOptions()
		saveall
		return true
	End Function
	
	
	
	
	
	Sub Button1Click(sender As Object, e As EventArgs)
		if LoadBeforeSim = false then exit sub
		me.tabControl1.SelectedIndex = 1
		SimConstructor.start(txtSimtime.Text,me,true)
	End Sub
	
	Sub BtEPClick(sender As Object, e As EventArgs)
		If txtSimtime.Text < 100 Then
			Dim ret As MsgBoxResult
			ret = msgbox("Short simulation time can give weird results. Try setting it to at least 100 hours.", MsgBoxStyle.OkCancel)
			if ret = MsgBoxResult.Cancel then exit sub
		End If
		'chkLissage.Checked	= true
		if LoadBeforeSim = false then exit sub
		me.tabControl1.SelectedIndex = 1
		SimConstructor.startEP(txtSimtime.Text,me)
	End Sub
	
	Sub MainFormLoad(sender As Object, e As EventArgs)
		Me.Text = "Kahorie's DK Simulator " & Application.ProductVersion
		LoadTrinket
		loadWindow
		loadConfig
		LoadEPOptions
		LoadBuffOption
		LoadScaling
		LoadTankOptions
		LoadDB()
		CreateTreeTemplate
		initReport
		Randomize 'Initialize the random # generator
		'CombatLog.init
		LoadAvailablePrio
		InitCharacterPanel
		_MainFrm = Me
		
	End Sub
	Sub LoadDB()
		ItemDB.Load(Application.StartupPath & "\GearSelector\" & "ItemDB.xml")
		GemDB.Load(Application.StartupPath & "\GearSelector\" & "gems.xml")
		GemBonusDB.Load(Application.StartupPath & "\GearSelector\" & "GemBonus.xml")
		EnchantDB.Load(Application.StartupPath & "\GearSelector\" & "Enchant.xml")
		trinketDB.Load(Application.StartupPath & "\GearSelector\" & "TrinketList.xml")
		SetBonusDB.Load(Application.StartupPath & "\GearSelector\" & "SetBonus.xml")
		WeapProcDB.Load(Application.StartupPath & "\GearSelector\" & "WeaponProcList.xml")
		FoodDB.Load(Application.StartupPath & "\GearSelector\" & "Food.xml")
		FlaskDB.Load(Application.StartupPath & "\GearSelector\" & "Flask.xml")
		ConsumableDB.Load(Application.StartupPath & "\GearSelector\" & "Consumables.xml")
	End Sub
	Sub MainFormClose(sender As Object, e As EventArgs)
		
		
	End Sub
	
	Sub ChkCombatLogCheckedChanged(sender As Object, e As EventArgs)
		ckLogRP.Enabled = ChkCombatLog.Checked
	End Sub
	
	Sub CkPetCheckedChanged(sender As Object, e As EventArgs)
		SimConstructor.PetFriendly = ckPet.Checked
	End Sub
	
	Sub CmdEditIntroClick(sender As Object, e As EventArgs)
		on error goto errH
		Dim tr As IO.Textreader
		CmbTemplate.SelectedItem.ToString
		
		EditorFilePAth = Application.StartupPath & "\Intro\"  & cmbIntro.Text
		tr =  new IO.StreamReader(EditorFilePAth)
		rtfEditor.Text =tr.ReadToEnd
		tr.Close
		tabControl1.SelectedIndex = 8
		OpenIntroForEdit(EditorFilePAth)
		cmdSaveRotation.Enabled=True
		cmdSave.Enabled=True
		cmdSaveNew.Enabled = True
		errH:
	End Sub
	
	
	
	
	Sub CmdEditPrioClick(sender As Object, e As EventArgs)
		on error goto errH
		Dim tr As IO.Textreader
		CmbTemplate.SelectedItem.ToString
		
		EditorFilePAth = Application.StartupPath & "\Priority\"  & cmbPrio.Text
		tr =  new IO.StreamReader(EditorFilePAth)
		rtfEditor.Text =tr.ReadToEnd
		tr.Close
		tabControl1.SelectedIndex = 8
		OpenPrioForEdit(EditorFilePAth)
		cmdSaveRotation.Enabled=True
		cmdSave.Enabled=true
		cmdSaveNew.Enabled = True
		errH:
		
	End Sub
	
	Sub CmdEditRotClick(sender As Object, e As EventArgs)
		'	on error goto errH
		Dim tr As IO.Textreader
		
		EditorFilePAth = Application.StartupPath & "\Rotation\"  & cmbRotation.Text
		tr =  new IO.StreamReader(EditorFilePAth)
		rtfEditor.Text =tr.ReadToEnd
		tr.Close
		tabControl1.SelectedIndex = 8
		OpenRotaForEdit(EditorFilePAth)
		cmdSaveRotation.Enabled=True
		cmdSave.Enabled=True
		cmdSaveNew.Enabled = True
		errH:
		
	End Sub
	
	Sub CmdSaveClick(sender As Object, e As EventArgs)
		'valiate XML
		on error goto errXml
		dim xmlDoc as New Xml.XmlDocument
		xmlDoc.LoadXml(rtfEditor.Text)
		on error goto errH
		Dim Tw As System.IO.TextWriter
		Tw  =system.IO.File.CreateText(EditorFilePAth)
		tw.WriteLine(rtfEditor.Text)
		tw.Close
		LockSaveButtons
		tabControl1.SelectedIndex = 0
		Exit Sub
		
		errXml:
		msgbox("Xml can not be validated")
		exit sub
		
		errH :
		msgbox("Error while saving file.")
		
	End Sub
	
	Sub CmdSaveNewClick(sender As Object, e As EventArgs)
		on error goto errXml
		dim xmlDoc as New Xml.XmlDocument
		xmlDoc.LoadXml(rtfEditor.Text)
		on error goto errH
		
		Dim truc As New Form1
		Dim res As DialogResult
		res = truc.ShowDialog
		If truc.textBox1.Text  <> "" and res = DialogResult.OK Then
			EditorFilePAth = Strings.Left(EditorFilePAth,strings.InStrRev(EditorFilePAth,"\")) & truc.textBox1.Text & ".xml"
		Else
			exit sub
		End If
		
		
		truc.Dispose
		Dim Tw As System.IO.TextWriter
		
		Tw=system.IO.File.CreateText(EditorFilePAth)
		tw.WriteLine(rtfEditor.Text)
		tw.Close
		loadWindow
		tabControl1.SelectedIndex = 0
		LockSaveButtons
		Exit Sub
		errXml:
		msgbox("Xml can not be validated")
		exit sub
		
		errH :
		msgbox("Error while saving file.")
	End Sub
	
	Sub CmdEditCharClick(sender As Object, e As EventArgs)
		on error goto errH
		Dim tr As IO.Textreader
		
		EditorFilePAth = Application.StartupPath & "\Characters\"  & cmbCharacter.Text
		tr =  new IO.StreamReader(EditorFilePAth )
		rtfEditor.Text =tr.ReadToEnd
		tr.Close
		tabControl1.SelectedIndex = 2
		LoadCharacter(EditorFilePAth)
		cmdSaveCharacter.Enabled=True
		cmdSave.Enabled=True
		cmdSaveNew.Enabled = True
		errH:
	End Sub
	
	Sub SetPBValue(ByVal i As integer)
		Me.PBsim.Value = i
	End Sub
	
	Sub TxtLatencyTextChanged(sender As Object, e As EventArgs)
		
		dim test as Boolean
		if Integer.TryParse(TxtLatency.Text,test ) = False then TxtLatency.Text = 0
		
	End Sub
	
	
	
	
	
	
	
	Sub CmdImportArmoryClick(sender As Object, e As EventArgs)
		Dim URL As String = txtArmory.Text
		url = "http://eu.wowarmory.com/character-sheet.xml?r=Chants+eternels&n=Kahorie"
		dim realmName as string = "Chants+eternels"
		dim characterName as String = "Kahorie"
		Dim webClient As WebClient = New WebClient()
		
		webClient.QueryString.Add("r", realmName)
		webClient.QueryString.Add("n", characterName)
		webClient.Headers.Add("user-agent", "MSIE 7.0")
		webClient.Proxy = WebRequest.GetSystemWebProxy
		webClient.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials
		dim xmlReaderSettings as XmlReaderSettings = new XmlReaderSettings()
		xmlReaderSettings.IgnoreComments = true
		xmlReaderSettings.IgnoreWhitespace = true
		Dim  xmlReader As XmlReader = XmlReader.Create(webClient.OpenRead("http://eu.wowarmory.com/character-sheet.xml"), xmlReaderSettings)
		Do While xmlReader.Read()
			If (xmlReader.NodeType = XmlNodeType.Element And xmlReader.Name = "item") Then
				debug.Print	("Name= " & xmlReader.GetAttribute("name") & "slot=" & xmlReader.GetAttribute(" slot") )
			End If
		Loop
	End Sub
	
	
	
	
	
	Sub ChkEPAfterSpellHitRatingCheckedChanged(sender As Object, e As EventArgs)
		if ChkEPAfterSpellHitRating.Checked then chkEPSpHit.Checked=true
	End Sub
	
	Sub ChkEPSpHitCheckedChanged(sender As Object, e As EventArgs)
		if ChkEPSpHit.Checked=false then ChkEPAfterSpellHitRating.Checked = false
	End Sub
	
	Sub RdPrioCheckedChanged(sender As Object, e As EventArgs)
		If rdPrio.Checked Then
			rdRot.Checked = False
			SimConstructor.Rotate = false
			cmbRotation.Enabled = False
			cmbPrio.Enabled = true
		End If
	End Sub
	
	Sub RdRotCheckedChanged(sender As Object, e As EventArgs)
		If rdRot.Checked Then
			rdprio.Checked = False
			SimConstructor.Rotate = true
			cmbRotation.Enabled = true
			cmbPrio.Enabled = false
		End If
	End Sub
	
	
	
	Sub CmdScalingClick(sender As Object, e As EventArgs)
		If LoadBeforeSim = False Then Exit Sub
		Me.tabControl1.SelectedIndex = 1
		SimConstructor.StartScaling(PBsim,txtSimtime.Text,me)
	End Sub
	
	Sub CmdRngSeederClick(sender As Object, e As EventArgs)
		RNGSeeder += 1
	End Sub
	
	Sub TxtSimtimeTextChanged(sender As Object, e As EventArgs)
		
	End Sub
	
	Sub TmrProgressTick(sender As Object, e As EventArgs)
		UpdateProgressBar
	End Sub
	
	Sub UpdateProgressBar()
		Dim s As Sim
		Dim i As Double
		on error resume next
		'		Me.PBsim.Maximum = 100
		If SimConstructor.simCollection.Count = 0 Then
			Me.PBsim.Value = 0
			Exit Sub
		End If
		i=0
		For Each s In SimConstructor.simCollection
			If s.MaxTime <> 0 Then
				i += (s.TimeStamp/s.MaxTime)/ SimConstructor.simCollection.Count
			Else
				i += 0
			End If
		Next
		i=i*100
		Me.PBsim.Value = i
	End Sub
	
	Sub CmdEditGearSelectorClick(sender As Object, e As EventArgs)
		If gearSelector Is Nothing Then
			gearSelector = new GearSelectorMainForm(Me)
		end if
		
		Try
			gearSelector.FilePath = cmbGearSelector.SelectedItem.ToString
			gearSelector.ShowDialog
			loadWindow
			cmbGearSelector.SelectedItem = gearSelector.FilePath
		Catch Err As Exception
			
			gearSelector.Dispose
			gearSelector = nothing
		End Try
		
	End Sub
	
	Sub Label45Click(sender As Object, e As EventArgs)
		
	End Sub
	
	Sub RCharacterCheckedChanged(sender As Object, e As EventArgs)
		If RCharacter.Checked Then
			rCharwithGear.Checked=False
			cmbCharacter.Enabled = True
			cmdEditChar.Enabled = True
			cmbGearSelector.Enabled = false
			cmdEditGearSelector.Enabled = false
		End If
	End Sub
	
	Sub RadioButton2CheckedChanged(sender As Object, e As EventArgs)
		If rCharwithGear.Checked Then
			RCharacter.Checked=False
			cmbCharacter.Enabled = false
			cmdEditChar.Enabled = false
			cmbGearSelector.Enabled = true
			cmdEditGearSelector.Enabled = true
		End If
	End Sub
	
	Sub CmdEditScenarioClick(sender As Object, e As EventArgs)
		Dim m As New Scenarios.ScenarioEditor
		Dim r As DialogResult
		'm.LoadAvailableScenario
		m.OpenForEdit(Application.StartupPath & "\scenario\scenario.xml")
		r = m.ShowDialog
		
	End Sub
	
	Sub GrpSimOptionEnter(sender As Object, e As EventArgs)
		
	End Sub
End Class
