﻿'
' Created by SharpDevelop.
' User: Fabien
' Date: 12/03/2009
' Time: 18:44
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System.Xml
Public Partial Class MainForm
	Private EditorFilePAth As String
	Private TemplatePath As String
	friend btList As New collection
	
	
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		
	End Sub
	
	Function LoadBeforeSim() As Boolean
		
		saveConfig
		Try
			GetFilePath(CmbTemplate.SelectedItem.ToString)
		Catch
			msgbox("Could not determine template. Please reselect it.")
			exit function
		End try
		'		If sim.rotate Then
		'			try
		'				sim.rotationPath = GetFilePath( cmbRotation.SelectedItem.ToString)
		'			Catch
		'				msgbox("Could not determine Rotation file. Please reselect it.")
		'				exit function
		'			End try
		'		Else
		'			try
		'				sim.loadPriority (GetFilePath(CmbPrio.SelectedItem.ToString))
		'			Catch
		'				msgbox("Could not determine Priority file. Please reselect it.")
		'				exit function
		'			End try
		'		End If
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
		SimConstructor.start(PBsim,txtSimtime.Text,me)
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
		SimConstructor.startEP(PBsim,txtSimtime.Text,me)
	End Sub
	
	Sub MainFormLoad(sender As Object, e As EventArgs)
		lblversion.Text = "Version " & Application.ProductVersion
		loadWindow
		loadConfig
		LoadEPOptions
		LoadBuffOption
		LoadScaling
		LoadTankOptions
		CreateTreeTemplate
		initReport
		Randomize 'Initialize the random # generator
		'CombatLog.init
		LoadAvailablePrio
		_MainFrm = me
	End Sub
	
	Sub MainFormClose(sender As Object, e As EventArgs)
		
		
	End Sub
	
	Sub ChkCombatLogCheckedChanged(sender As Object, e As EventArgs)
		ckLogRP.Enabled = ChkCombatLog.Checked
	End Sub
	
	Sub CkPetCheckedChanged(sender As Object, e As EventArgs)
		SimConstructor.PetFriendly = ckPet.Checked
	End Sub
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	Sub CmdEditPrioClick(sender As Object, e As EventArgs)
		on error goto errH
		Dim tr As IO.Textreader
		
		
		EditorFilePAth = GetFilePath(cmbPrio.Text)
		tr =  new IO.StreamReader(EditorFilePAth)
		rtfEditor.Text =tr.ReadToEnd
		tr.Close
		tabControl1.SelectedIndex = 2
		errH:
		
	End Sub
	
	Sub CmdEditRotClick(sender As Object, e As EventArgs)
		on error goto errH
		Dim tr As IO.Textreader
		EditorFilePAth = GetFilePath(cmbRotation.Text)
		tr =  new IO.StreamReader(EditorFilePAth)
		rtfEditor.Text =tr.ReadToEnd
		tr.Close
		tabControl1.SelectedIndex = 2
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
		EditorFilePAth = GetFilePath(cmbCharacter.Text)
		tr =  new IO.StreamReader(EditorFilePAth )
		rtfEditor.Text =tr.ReadToEnd
		tr.Close
		tabControl1.SelectedIndex = 2
		errH:
	End Sub
	
	Sub TxtLatencyTextChanged(sender As Object, e As EventArgs)
		
		dim test as Boolean
		if Integer.TryParse(TxtLatency.Text,test ) = False then TxtLatency.Text = 0
		
	End Sub
	
	
	
	
	
	
	
	Sub CmdImportArmoryClick(sender As Object, e As EventArgs)
		Dim URL As String = txtArmory.Text
		url = "http://eu.wowarmory.com/character-sheet.xml?r=Chants+eternels&n=Kahorie"
		
		' Retrieve XML document
		Dim reader As XmlTextReader = New XmlTextReader(url)
		' Skip non-significant whitespace
		reader.WhitespaceHandling = WhitespaceHandling.Significant
		
		' Read nodes one at a time
		While reader.Read()
			' Print out info on node
			Console.WriteLine("{0}: {1}", reader.NodeType.ToString(), reader.Name)
		End While
		
		
		
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		Dim bw As New WebBrowser
		
		dim webClient as New System.Net.WebClient
		
		url = "http://eu.wowarmory.com/character-sheet.xml?r=Chants+eternels&n=Kahorie"
		Dim myUri As Uri = New Uri("http://eu.wowarmory.com/character-sheet.xml?r=Chants+eternels&n=Kahorie")
		webClient.Dispose()
		'		wbTemplate.Url = myUri
		'		wbTemplate.Navigate(myUri)
		
		'	bw.Navigate(myUri)
		doc.Load(URL)
		debug.Print(doc.OuterXml)
		
		errH:
		
	End Sub
	
	Sub CmbHeadSelectedIndexChanged(sender As Object, e As EventArgs)
		Dim xDoc As new Xml.XmlDocument
		xDoc.Load("wowheaddbEpic.xml")
		Dim node As Xml.XmlNode
		node = xDoc.SelectSingleNode("/wowhead/item[name=Furious Gladiator's Sigil of Strife]")
		'		textBox1.Text = node.InnerXml
		
	End Sub
	
	Sub CkBHeadSelectedIndexChanged(sender As Object, e As EventArgs)
		Dim xDoc As New Xml.XmlDocument
		on error resume next
		xDoc.Load("wowheaddbEpic.xml")
		Dim node As Xml.XmlNode
		node = xDoc.SelectSingleNode("/wowhead/item[name='" & sender.SelectedItem.ToString & "']")
		
		
		
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
			SimConstructor.Rotate = False
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
		if LoadBeforeSim = false then exit sub
		SimConstructor.StartScaling(PBsim,txtSimtime.Text,me)
	End Sub
	
	Sub BtCreateCMBClick(sender As Object, e As EventArgs)
		me.CreateCombobox
	End Sub
End Class
