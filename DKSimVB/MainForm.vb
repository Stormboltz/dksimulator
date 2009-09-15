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
	'Private sim as Sim 'TODO
	
	
	
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	
	Function LoadBeforeSim() As Boolean
		saveConfig()
		Try
		sim.loadtemplate (GetFilePath(CmbTemplate.SelectedItem.ToString))
		Catch
			msgbox("Could not determine template. Please reselect it.")
			exit function
		End try
		If sim.rotate Then
			try
				sim.rotationPath = GetFilePath( cmbRotation.SelectedItem.ToString)
			Catch
				msgbox("Could not determine Rotation file. Please reselect it.")
				exit function
			End try
		Else
			try
				sim.loadPriority (GetFilePath(CmbPrio.SelectedItem.ToString))
			Catch
				msgbox("Could not determine Priority file. Please reselect it.")
				exit function
			End try
		End If
		SaveEPOptions()
		SaveBuffOption()
		return true
	End Function
	
	Sub SaveBuffOption
		'on error resume next
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.LoadXml("<config></config>")
		' Create a new element node.
		Dim root as xml.XmlElement = doc.DocumentElement

		Dim newElem As xml.XmlNode
		Dim ctrl As Control
		dim chkBox as CheckBox
		For Each ctrl in grpBuff.Controls
			If ctrl.Name.StartsWith ("chk") Then
				chkBox = ctrl
				newElem = doc.CreateNode(xml.XmlNodeType.Element, chkBox.Name, "")
				newElem.InnerText = chkBox.Checked
				root.AppendChild(newElem)
			End If
		Next
		doc.Save("Buffconfig.xml")
	End Sub
	Sub LoadBuffOption
		on error goto sortie
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.Load("Buffconfig.xml")
		Dim ctrl As Control
		dim chkBox as CheckBox
		For Each ctrl in grpBuff.Controls
			If ctrl.Name.StartsWith ("chk") Then
				chkBox = ctrl
				chkBox.Checked = doc.SelectSingleNode("//config/" & chkBox.Name ).InnerText
			End If
		Next
		sortie:
	End Sub
	
	Sub SaveEPOptions
		'on error resume next
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.LoadXml("<config></config>")
		' Create a new element node.
		Dim xmlStat As xml.XmlNode = doc.CreateNode(xml.XmlNodeType.Element, "Stats", "")
		Dim xmlSet As xml.XmlNode = doc.CreateNode(xml.XmlNodeType.Element, "Sets", "")
		dim xmlTrinket As xml.XmlNode = doc.CreateNode(xml.XmlNodeType.Element, "Trinket", "")
		Dim root as xml.XmlElement = doc.DocumentElement
		root.AppendChild(xmlStat)
		root.AppendChild(xmlSet)
		root.AppendChild(xmlTrinket)
    	Dim newElem As xml.XmlNode
		
		Dim ctrl As Control
		dim chkBox as CheckBox
		For Each ctrl in groupBox1.Controls
			If ctrl.Name.StartsWith ("chkEP") Then
				chkBox = ctrl
				newElem = doc.CreateNode(xml.XmlNodeType.Element, chkBox.Name, "")
				newElem.InnerText = chkBox.Checked
				xmlStat.AppendChild(newElem)
			End If
		Next
		For Each ctrl in groupBox2.Controls
			If ctrl.Name.StartsWith ("chkEP") Then
				chkBox = ctrl
				newElem = doc.CreateNode(xml.XmlNodeType.Element, chkBox.Name, "")
				newElem.InnerText = chkBox.Checked
				xmlSet.AppendChild(newElem)
			End If
		Next
		For Each ctrl in groupBox3.Controls
			If ctrl.Name.StartsWith ("chkEP") Then
				chkBox = ctrl
				newElem = doc.CreateNode(xml.XmlNodeType.Element, chkBox.Name, "")
				newElem.InnerText = chkBox.Checked
				xmlTrinket.AppendChild(newElem)
			End If
		Next
		doc.Save("EPconfig.xml")
	End Sub
	
	Sub LoadEPOptions
		on error goto sortie
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.Load("EPconfig.xml")
		
		Dim ctrl As Control
		dim chkBox as CheckBox
		For Each ctrl in groupBox1.Controls
			If ctrl.Name.StartsWith ("chkEP") Then
				chkBox = ctrl
				chkBox.Checked = doc.SelectSingleNode("//config/Stats/" & chkBox.Name ).InnerText
			End If
		Next
		For Each ctrl in groupBox2.Controls
			If ctrl.Name.StartsWith ("chkEP") Then
				chkBox = ctrl
				chkBox.Checked = doc.SelectSingleNode("//config/Sets/" & chkBox.Name ).InnerText
			End If
		Next
		
		For Each ctrl in groupBox3.Controls
			If ctrl.Name.StartsWith ("chkEP") Then
				chkBox = ctrl
				chkBox.Checked = doc.SelectSingleNode("//config/Trinket/" & chkBox.Name ).InnerText
			End If
		Next
		
		
		sortie:
	End Sub
	
	Sub Button1Click(sender As Object, e As EventArgs)
		if LoadBeforeSim = false then exit sub
		me.tabControl1.SelectedIndex = 1
		sim.start(PBsim,txtSimtime.Text,me)
	End Sub
	
	Sub BtEPClick(sender As Object, e As EventArgs)
		If txtSimtime.Text < 100 Then
			Dim ret As MsgBoxResult
			ret = msgbox("Short simulation time can give weird results. Try setting it to at least 100 hours.", MsgBoxStyle.OkCancel)
			if ret = MsgBoxResult.Cancel then exit sub
		End If
		chkLissage.Checked	= true
		if LoadBeforeSim = false then exit sub
		me.tabControl1.SelectedIndex = 1
		sim.startEP(PBsim,True,txtSimtime.Text,me)
	End Sub
	
	Sub MainFormLoad(sender As Object, e As EventArgs)
		lblversion.Text = "Version " & Application.ProductVersion
		loadTemplate
		loadConfig
		LoadEPOptions
		LoadBuffOption
		Dim URL As Uri = new Uri("http://talent.mmo-champion.com/?deathknight=")
		wbTemplate.Url = URL
		Randomize 'Initialize the random # generator
		CombatLog.init
	End Sub
	
	Sub saveConfig
		on error resume next
		'	Try
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		doc.LoadXml("<config></config>")
		' Create a new element node.
		Dim newElem as xml.XmlNode = doc.CreateNode(xml.XmlNodeType.Element, "Character", "")
		newElem.InnerText = cmbCharacter.SelectedItem.tostring
		Dim root as xml.XmlElement = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "template", "")
		newElem.InnerText = cmbTemplate.SelectedItem.tostring
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "priority", "")
		newElem.InnerText = cmbPrio.SelectedItem.tostring
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "rotation", "")
		newElem.InnerText = cmbRotation.SelectedItem.tostring
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "presence", "")
		newElem.InnerText = cmdPresence.SelectedItem.tostring
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "sigil", "")
		newElem.InnerText = cmbSigils.SelectedItem.tostring
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "mh", "")
		newElem.InnerText = cmbRuneMH.SelectedItem.tostring
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "oh", "")
		newElem.InnerText = cmbRuneOH.SelectedItem.tostring
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		'Latence
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "latency", "")
		newElem.InnerText = txtLatency.Text
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		'Sim Time
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "simtime", "")
		newElem.InnerText = txtSimtime.Text
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		
		'Genere combat log
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "log", "")
		newElem.InnerText = chkCombatLog.Checked
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		
		
		'Detailed
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "logdetail", "")
		newElem.InnerText = ckLogRP.Checked
		root = doc.DocumentElement
		root.AppendChild(newElem)
		
		
		'Smooth
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "smooth", "")
		newElem.InnerText = chkLissage.Checked
		root = doc.DocumentElement
		root.AppendChild(newElem)
		

		'Ghouls2haste
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ghoulhaste", "")
		newElem.InnerText = chkGhoulHaste.Checked
		root = doc.DocumentElement
		root.AppendChild(newElem)
		

		'FCWait
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "WaitFC", "")
		newElem.InnerText = chkWaitFC.Checked
		root = doc.DocumentElement
		root.AppendChild(newElem)
		

		'Pets
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "pet", "")
		newElem.InnerText = ckPet.Checked
		root = doc.DocumentElement
		root.AppendChild(newElem)
	
		
		doc.Save("config.xml")
		
		'	Catch e As Exception
		
		'	End Try
	End Sub
	Sub loadConfig
		Dim doc As xml.XmlDocument = New xml.XmlDocument
		on error resume next
		doc.Load("config.xml")
		cmbCharacter.SelectedItem = doc.SelectSingleNode("//config/Character").InnerText
		cmbTemplate.SelectedItem = doc.SelectSingleNode("//config/template").InnerText
		cmbPrio.SelectedItem = doc.SelectSingleNode("//config/priority").InnerText
		cmbRotation.SelectedItem= doc.SelectSingleNode("//config/rotation").InnerText
		cmdPresence.SelectedItem = doc.SelectSingleNode("//config/presence").InnerText
		cmbSigils.SelectedItem = doc.SelectSingleNode("//config/sigil").InnerText
		cmbRuneMH.SelectedItem = doc.SelectSingleNode("//config/mh").InnerText
		cmbRuneOH.SelectedItem = doc.SelectSingleNode("//config/oh").InnerText
		txtLatency.Text = doc.SelectSingleNode("//config/latency").InnerText
		
		txtSimtime.Text = doc.SelectSingleNode("//config/simtime").InnerText
		chkCombatLog.Checked = doc.SelectSingleNode("//config/log").InnerText
		ckLogRP.Checked = doc.SelectSingleNode("//config/logdetail").InnerText
		chkLissage.Checked = doc.SelectSingleNode("//config/smooth").InnerText
		chkGhoulHaste.Checked = doc.SelectSingleNode("//config/ghoulhaste").InnerText
		chkWaitFC.Checked = doc.SelectSingleNode("//config/WaitFC").InnerText
		ckPet.Checked = doc.SelectSingleNode("//config/pet").InnerText
		errH:
	End Sub
	
	Sub MainFormClose(sender As Object, e As EventArgs)
		
		
	End Sub
	Sub loadTemplate()
		Dim item As String
		CmbCharacter.Items.Clear
		For Each item In system.IO.Directory.GetFiles(Application.StartupPath & "\Characters\")
			CmbCharacter.Items.Add(strings.Right(item,item.Length- InStrRev(item,"\") ) & "(" & item & ")")
		Next
		cmbTemplate.Items.Clear
		For Each item In system.IO.Directory.GetFiles(Application.StartupPath & "\Templates\")
			cmbTemplate.Items.Add(strings.Right(item,item.Length- InStrRev(item,"\") ) & "(" & item & ")")
		Next
		cmbPrio.Items.Clear
		For Each item In system.IO.Directory.GetFiles(Application.StartupPath & "\Priority\")
			cmbPrio.Items.Add(strings.Right(item,item.Length- InStrRev(item,"\") ) & "(" & item & ")")
		Next
		cmbRotation.Items.Clear
		For Each item In system.IO.Directory.GetFiles(Application.StartupPath & "\Rotation\")
			cmbRotation.Items.Add(strings.Right(item,item.Length- InStrRev(item,"\") ) & "(" & item & ")")
		Next
		cmdPresence.Items.Clear
		cmdPresence.Items.Add("Blood")
		cmdPresence.Items.Add("Unholy")
		cmdPresence.Items.Add("Frost")
		
		cmbSigils.Items.Clear
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
		
		cmbSigils.Sorted=true
		
		cmbRuneMH.Items.Clear
		cmbRuneMH.Items.Add("None")
		cmbRuneMH.Items.Add("Cinderglacier")
		cmbRuneMH.Items.Add("Razorice")
		cmbRuneMH.Items.Add("FallenCrusader")
		cmbRuneOH.Items.Clear
		cmbRuneOH.Items.Add("None")
		cmbRuneOH.Items.Add("Cinderglacier")
		cmbRuneOH.Items.Add("Razorice")
		cmbRuneOH.Items.Add("FallenCrusader")
		cmbRuneOH.Items.Add("Berserking")
		sim.PetFriendly = True
		
		
		
		sim.initReport
	End Sub
	
	Sub CmbTemplateSelectedIndexChanged(sender As Object, e As EventArgs)
		'		dim ds as New Data.DataSet
		'		ds.ReadXml(txtFilePath, Data.XmlReadMode.InferSchema)
		'Me.dataGrid1.SetDataBinding(ds, "Talents")
		'dataGrid1.captionText = txtFilePath
	End Sub
	

	
	Sub CmbPrioSelectedIndexChanged(sender As Object, e As EventArgs)
		sim.Rotate = False
		cmbRotation.BackColor=Color.Gray
		cmbPrio.BackColor=Color.White
	End Sub
	
	Sub CmdPresenceSelectedIndexChanged(sender As Object, e As EventArgs)
		Dim Presence As String
		Presence = cmdPresence.SelectedItem.ToString
		sim.MainStat.BloodPresence = 0
		sim.MainStat.UnholyPresence = 0
		sim.Mainstat.FrostPresence = 0
		Select Case Presence
			Case "Blood"
				sim.MainStat.BloodPresence = 1
			Case "Unholy"
				sim.MainStat.UnholyPresence=1
			Case "Frost"
				sim.Mainstat.FrostPresence = 1
		End Select
	End Sub
	
	Sub CmbSigilsSelectedIndexChanged(sender As Object, e As EventArgs)
'		Dim Sigil As String
'		Sigil = CmbSigils.SelectedItem.ToString
'		Sigils.WildBuck = false
'		Sigils.FrozenConscience = false
'		Sigils.DarkRider = false
'		Sigils.ArthriticBinding = false
'		Sigils.Awareness = false
'		Sigils.Strife = false
'		Sigils.HauntedDreams = false
'		sigils.VengefulHeart = False
'		sigils.Virulence = false
'		select case Sigil
'			case "WildBuck"
'				Sigils.WildBuck = true
'			case "FrozenConscience"
'				Sigils.FrozenConscience =true
'			case "DarkRider"
'				Sigils.DarkRider = true
'			case "ArthriticBinding"
'				Sigils.ArthriticBinding = true
'			case "Awareness"
'				Sigils.Awareness = true
'			case "Strife"
'				Sigils.Strife = true
'			case "HauntedDreams"
'				Sigils.HauntedDreams = True
'			Case "VengefulHeart"
'				sigils.VengefulHeart = True
'			Case "Virulence"
'				sigils.Virulence = true
'		end select
	End Sub
	
	Sub ComboBox1SelectedIndexChanged(sender As Object, e As EventArgs)
		sim.Rotate = True
		cmbRotation.BackColor=Color.White
		cmbPrio.BackColor=Color.Gray
	End Sub
	
	Sub CmbRuneMHSelectedIndexChanged(sender As Object, e As EventArgs)
		sim.RuneForge.MHCinderglacier = False
		sim.RuneForge.MHFallenCrusader = false
		sim.RuneForge.MHRazorice = false
		Select Case cmbRuneMH.SelectedItem.ToString
			Case "Cinderglacier"
				sim.RuneForge.MHCinderglacier = true
			Case "FallenCrusader"
				sim.RuneForge.MHFallenCrusader = true
			Case "Razorice"
				sim.RuneForge.MHRazorice = true
		End Select
	End Sub
	
	Sub CmbRuneOHSelectedIndexChanged(sender As Object, e As EventArgs)
		sim.RuneForge.OHCinderglacier = False
		sim.RuneForge.OHFallenCrusader = false
		sim.RuneForge.OHRazorice = False
		sim.Runeforge.OHBerserking = False
		
		Select Case cmbRuneOH.SelectedItem.ToString
			Case "Cinderglacier"
				sim.RuneForge.OHCinderglacier = true
			Case "FallenCrusader"
				sim.RuneForge.OHFallenCrusader = true
			Case "Razorice"
				sim.RuneForge.OHRazorice = True
			Case "Berserking"
				sim.Runeforge.OHBerserking = True
			end select
	End Sub
	
	Sub ChkCombatLogCheckedChanged(sender As Object, e As EventArgs)
		CombatLog.enable = ChkCombatLog.Checked
		ckLogRP.Enabled = ChkCombatLog.Checked
	End Sub
	
	Sub CkPetCheckedChanged(sender As Object, e As EventArgs)
		sim.PetFriendly = ckPet.Checked
	End Sub
	
	Sub CkLogRPCheckedChanged(sender As Object, e As EventArgs)
		CombatLog.LogDetails = ckLogRP.Checked
	End Sub
	
	Sub CmdEditTemplateClick(sender As Object, e As EventArgs)
		on error goto errH
		Dim tr As IO.Textreader
		EditorFilePAth = GetFilePath(cmbTemplate.Text)
		tr =  new IO.StreamReader(EditorFilePAth )
		rtfEditor.Text =tr.ReadToEnd
		tr.Close
		dim xmlDoc as New Xml.XmlDocument
		xmlDoc.Load(EditorFilePAth)
		Dim URL As Uri = new Uri(XmlDoc.SelectSingleNode("//Talents/URL").InnerText)
		wbTemplate.Url = URL
		TemplatePath = EditorFilePAth
		tabControl1.SelectedIndex = 4
		errH:
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
		loadTemplate
		tabControl1.SelectedIndex = 0
		Exit Sub
		
		
		errXml:
		msgbox("Xml can not be validated")
		exit sub
		
		errH :
		msgbox("Error while saving file.")
	End Sub
	
	Sub CmbCharacterSelectedIndexChanged(sender As Object, e As EventArgs)
		
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
	
	

Sub ChkShowDpsCheckedChanged(sender As Object, e As EventArgs)
	
End Sub





Sub TxtLatencyTextChanged(sender As Object, e As EventArgs)
	
	dim test as Boolean
	if Integer.TryParse(TxtLatency.Text,test ) = False then TxtLatency.Text = 0
	
End Sub

Sub CmdImportTemplateClick(sender As Object, e As EventArgs)
	ImportTemplate("")
End Sub
	
	
Sub ImportTemplate(name As String)
	
	Dim URL As String = txtImportTemplate.Text
	Dim tmp As String
	
		On Error GoTo errH
		
	tmp = strings.right(URL,URL.Length - instr(url,"="))
	tmp = strings.left (tmp,instr(tmp,"&")-1)
	
	
	Dim doc As xml.XmlDocument = New xml.XmlDocument
	doc.LoadXml("<Talents></Talents>")
	Dim root As xml.XmlElement = doc.DocumentElement
	Dim newElem as xml.XmlNode
	
	Dim i As Integer = 1
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "URL", "")
	newElem.InnerText = txtImportTemplate.Text
	root.AppendChild(newElem)
	
	
	'Blood
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "Butchery", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
	
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "Subversion", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BladeBarrier", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BladedArmor", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ScentOfBlood", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Weapspec", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "RuneTap", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Darkconv", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
	
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "DRM", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "IRuneTap", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "SpellDeflection", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Vendetta", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BloodyStrikes", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Vot3W", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "MarkBlood", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1

	newElem = doc.CreateNode(xml.XmlNodeType.Element, "BloodyVengeance", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "AbominationMight", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BloodWorms", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Hysteria", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "IBloodPresence", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ImprovedDeathStrike", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "SuddenDoom", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
	
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Vampiric", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "WillNecropolis", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "HeartStrike", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "MightofMograine", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BloodGorged", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "DRW", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
	
	
	'FROST

	
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "ImprovedIcyTouch", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "RPM", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Toughness", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "IcyReach", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BlackIce", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "NervesofColdSteel", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "IcyTalons", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "LichBorne", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Annihilation", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "KillingMachine", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ChillOfTheGrave", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "EndlessWinter", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Frigid", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "GlacierRot", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Deathchill", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ImprovedIcyTalons", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "MercilessCombat", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Rime", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ChillBlains", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "HungeringCold", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "IFrostPresence", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ThreatOfThassarian", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BloodoftheNorth", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "UnbreakableArmor", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Acclimatation", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "FrostStrike", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "GuileOfGorefiend", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "TundraStalker", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "HowlingBlast", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1

	' Unholy
			newElem = doc.CreateNode(xml.XmlNodeType.Element, "ViciousStrikes", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Virulence", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Anticipation", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Epidemic", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Morbidity", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "UnholyCommand", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "RavenousDead", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Outbreak", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Necrosis", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "CorpseExplosion", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
	
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "PaleHorse", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BloodCakedBlade", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "NightoftheDead", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "UnholyBlight", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Impurity", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Dirge", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Desecration", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
	
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "MagicSuppression", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
	
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Reaping", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "MasterOfGhouls", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "Desolation", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "AMZ", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ImprovedUnholyPresence", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "GhoulFrenzy", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "CryptFever", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "BoneShield", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "WanderingPlague", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "EbonPlaguebringer", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "ScourgeStrike", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "RageofRivendare", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
		newElem = doc.CreateNode(xml.XmlNodeType.Element, "SummonGargoyle", "")
	newElem.InnerText = strings.mid(tmp,i,1)
	root.AppendChild(newElem)
	i=i+1
	
	
	'Glyphs
	
	tmp = strings.right(URL,URL.Length - instr(url,"glyph=")-5)
	tmp = strings.left (tmp,instr(tmp,"&")-1)
	dim glyph1 as String
	dim glyph2 as String
	dim glyph3 as String
	glyph1 = tmp.Chars(0) + tmp.Chars(1)
	glyph2 = tmp.Chars(2) + tmp.Chars(3)
	glyph3 = tmp.Chars(4) + tmp.Chars(5)
	
	dim glyphID as String
	
	Dim xmlGlyph As xml.XmlNode = doc.CreateNode(xml.XmlNodeType.Element, "Glyphs", "")
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "BloodStrike", "")
	glyphID = "03"
	If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
		newElem.InnerText = 1
	Else
		newElem.InnerText = 0
	End If
		
	glyphID = "08"
	xmlGlyph.AppendChild(newElem)
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "DarkDeath", "")
	If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
		newElem.InnerText = 1
	Else
		newElem.InnerText = 0
	End If
	glyphID = "10"
	xmlGlyph.AppendChild(newElem)
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "DeathStrike", "")
	If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
		newElem.InnerText = 1
	Else
		newElem.InnerText = 0
	End If
	glyphID = "13"
	xmlGlyph.AppendChild(newElem)
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "FrostStrike", "")
	If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
		newElem.InnerText = 1
	Else
		newElem.InnerText = 0
	End If
	glyphID = "14"
	xmlGlyph.AppendChild(newElem)
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "HowlingBlast", "")
	If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
		newElem.InnerText = 1
	Else
		newElem.InnerText = 0
	End If
	glyphID = "17"
	xmlGlyph.AppendChild(newElem)
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "IcyTouch", "")
	If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
		newElem.InnerText = 1
	Else
		newElem.InnerText = 0
	End If
	glyphID = "18"
	xmlGlyph.AppendChild(newElem)
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "Obliterate", "")
	If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
		newElem.InnerText = 1
	Else
		newElem.InnerText = 0
	End If
	glyphID = "19"
	xmlGlyph.AppendChild(newElem)
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "PlagueStrike", "")
	If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
		newElem.InnerText = 1
	Else
		newElem.InnerText = 0
	End If
	glyphID = "22"
	xmlGlyph.AppendChild(newElem)
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "ScourgeStrike", "")
	If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
		newElem.InnerText = 1
	Else
		newElem.InnerText = 0
	End If
	glyphID = "25"
	xmlGlyph.AppendChild(newElem)
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "UnholyBlight", "")
	If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
		newElem.InnerText = 1
	Else
		newElem.InnerText = 0
	End If
	glyphID = "27"
	xmlGlyph.AppendChild(newElem)
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "Ghoul", "")
	If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
		newElem.InnerText = 1
	Else
		newElem.InnerText = 0
	End If
	glyphID = "11"
	xmlGlyph.AppendChild(newElem)
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "DeathandDecay", "")
	If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
		newElem.InnerText = 1
	Else
		newElem.InnerText = 0
	End If
	glyphID = "06"
	xmlGlyph.AppendChild(newElem)
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "DRW", "")
	If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
		newElem.InnerText = 1
	Else
		newElem.InnerText = 0
	End If
	glyphID = "12"
	xmlGlyph.AppendChild(newElem)
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "Disease", "")
	If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
		newElem.InnerText = 1
	Else
		newElem.InnerText = 0
	End If
	glyphID = "20"
	xmlGlyph.AppendChild(newElem)
	newElem = doc.CreateNode(xml.XmlNodeType.Element, "RuneStrike", "")
	If glyph1 = glyphID Or glyph2 = glyphID Or glyph3 = glyphID  Then
		newElem.InnerText = 1
	Else
		newElem.InnerText = 0
	End If
	
	xmlGlyph.AppendChild(newElem)
	
	root.AppendChild(xmlGlyph)
	
	
	
	if name = "" then
		Dim truc As New Form1
		Dim res As DialogResult
		res = truc.ShowDialog
		If truc.textBox1.Text  <> "" And res = DialogResult.OK Then
			doc.Save(application.StartupPath & "\Templates\" & truc.textBox1.Text & ".xml")
			EditorFilePAth = Strings.Left(EditorFilePAth,strings.InStrRev(EditorFilePAth,"\"))
		Else
			exit sub
		End If
		truc.Dispose
	Else
		doc.Save(name)
	End If
		
		Msgbox ( "Import done.")
		loadTemplate
		tabControl1.SelectedIndex = 0
		Exit Sub
		errH :
		msgbox("Error while importing talents !")
End Sub
	
	
	Sub CmdLoadMmoClick(sender As Object, e As EventArgs)
		
	End Sub
	
	Sub ChkLissageCheckedChanged(sender As Object, e As EventArgs)
		sim.Lissage = chkLissage.Checked
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
		
		
		
		
		
		
		
		
		
		
		
		
		
	'on error resume next
		
	'	dim myUri as Uri = new Uri(URL)

		
		'	On Error GoTo errH
			
'		tmp = strings.right(URL,URL.Length - instr(url,"="))
'		tmp = strings.left (tmp,instr(tmp,"&")-1)

		Dim doc As xml.XmlDocument = New xml.XmlDocument
		Dim bw As New WebBrowser
		
		dim webClient as New System.Net.WebClient

    	url = "http://eu.wowarmory.com/character-sheet.xml?r=Chants+eternels&n=Kahorie"
    	Dim myUri As Uri = New Uri("http://eu.wowarmory.com/character-sheet.xml?r=Chants+eternels&n=Kahorie")
    	webClient.Dispose()
		wbTemplate.Url = myUri
		wbTemplate.Navigate(myUri)
		
	'	bw.Navigate(myUri)
		doc.Load(URL)
		debug.Print(doc.OuterXml)
	
	errH:
	
	End Sub
	
	
	
	Sub ChkGhoulHasteCheckedChanged(sender As Object, e As EventArgs)
		'sim.Ghoul.GhoulDoubleHaste = chkGhoulHaste.Checked
	End Sub
	
	Sub CmdSaveTalentClick(sender As Object, e As EventArgs)
		Dim tmp As String
		dim i as Integer
		'debug.Print(wbTemplate.Document.Body.InnerText)
		i =  wbTemplate.Document.Body.InnerText.IndexOf("http://")
		tmp  = strings.right(wbTemplate.Document.Body.InnerText,wbTemplate.Document.Body.InnerText.Length - i)
		i = tmp.IndexOf(vbCrLf)
		tmp  = strings.left(tmp,i)
		'debug.Print(tmp)
		txtImportTemplate.Text = tmp
		ImportTemplate("")
		
	
	End Sub
	
	Sub Button2Click(sender As Object, e As EventArgs)
		Dim tmp As String
		dim i as Integer
		'debug.Print(wbTemplate.Document.Body.InnerText)
		i =  wbTemplate.Document.Body.InnerText.IndexOf("http://")
		tmp  = strings.right(wbTemplate.Document.Body.InnerText,wbTemplate.Document.Body.InnerText.Length - i)
		i = tmp.IndexOf(vbCrLf)
		tmp  = strings.left(tmp,i)
		'debug.Print(tmp)
		txtImportTemplate.Text = tmp
		ImportTemplate(TemplatePath)
	End Sub
	
'	Sub CmdLoadDBClick(sender As Object, e As EventArgs)
'		Dim xDoc As new Xml.XmlDocument
'		xDoc.Load("wowheaddbEpic.xml")
'		Dim nodes As Xml.XmlNodeList
'		nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Head']")
'		dim node as Xml.XmlNode
'		For Each node In nodes
'			cmbHead.Items.Add(node.SelectSingleNode("name").InnerText)
'			ckBHead.Items.Add(node.SelectSingleNode("name").InnerText)
'
'		Next
'
'
'		nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Neck']")
'		For Each node In nodes
'			cmbNeck.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'
'		nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Shoulder']")
'		For Each node In nodes
'			cmbShoulder.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'
'
'		nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Back']")
'		For Each node In nodes
'			cmbback.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'
'		nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Chest']")
'
'		For Each node In nodes
'			cmbChest.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'
'		nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Wrist']")
'
'		For Each node In nodes
'			cmbWrist.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'
'		nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Hands']")
'		For Each node In nodes
'			cmbHand.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'
'		nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Waist']")
'		For Each node In nodes
'			cmbWaist.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'		nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Legs']")
'		For Each node In nodes
'			cmbLeg.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'		nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Feet']")
'		For Each node In nodes
'			cmbFeet.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'
'			nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Finger']")
'		For Each node In nodes
'			cmbring1.Items.Add(node.SelectSingleNode("name").InnerText)
'			cmbring2.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'
'			nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Trinket']")
'		For Each node In nodes
'			cmbTrinket1.Items.Add(node.SelectSingleNode("name").InnerText)
'			cmbTrinket2.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'
'
'		nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Two-Hand']")
'		For Each node In nodes
'			cmbMH.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'		nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='One-Hand']")
'		For Each node In nodes
'			cmbMH.Items.Add(node.SelectSingleNode("name").InnerText)
'			cmbOH.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'
'		nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Main Hand']")
'		For Each node In nodes
'			cmbMH.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'		nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Off Hand']")
'		For Each node In nodes
'			cmboH.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'
'		nodes = xDoc.SelectNodes("/wowhead/item[inventorySlot='Relic']")
'		For Each node In nodes
'			cmbsigil.Items.Add(node.SelectSingleNode("name").InnerText)
'		Next
'	End Sub

	

	
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
End Class
