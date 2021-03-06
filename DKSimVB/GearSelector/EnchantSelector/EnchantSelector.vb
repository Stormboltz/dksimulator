﻿'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 2/22/2010
' Heure: 4:32 PM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Partial Class EnchantSelector
	Inherits System.Windows.Forms.Form
	
	Dim sortColumn As Integer = -1
	Friend gemDB As New Xml.XmlDocument
	Friend Slot As String
	Friend SelectedItem As String
	Friend MainFrame as GearSelectorMainForm
	
	
	
	
	Public Sub New(GS as GearSelectorMainForm)
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		MainFrame = GS
		
		ListView1.Columns.Add("Name")
		ListView1.Columns.Add("id")
		ListView1.Columns.Add("Str")
		ListView1.Columns.Add("Agi")
		ListView1.Columns.Add("Exp")
		ListView1.Columns.Add("Hit")
		ListView1.Columns.Add("AP")
		ListView1.Columns.Add("Crit")
		ListView1.Columns.Add("ArP")
		ListView1.Columns.Add("Haste")
		gemDB.Load(Application.StartupPath & "\GearSelector\" & "enchant.xml")
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	Sub ListView1SelectedIndexChanged(sender As Object, e As EventArgs)
		SelectedItem = sender.SelectedItems.Item(0).subitems.item(1).text
		me.hide
	End Sub
	Sub ListView1ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs)
		exit sub
		' Determine whether the column is the same as the last column clicked.
		If e.Column <> sortColumn Then
			sortColumn = e.Column
			listView1.Sorting = SortOrder.Descending
		Else
			' Determine what the last sort order was and change it.
			If listView1.Sorting = SortOrder.Ascending Then
				listView1.Sorting = SortOrder.Descending
			Else
				listView1.Sorting = SortOrder.Ascending
			End If
		End If
		' Call the sort method to manually sort.
		listView1.Sort()
		' Set the ListViewItemSorter property to a new ListViewItemComparer object.
		listView1.ListViewItemSorter = New ListViewItemComparer(e.Column, listView1.Sorting)
		
	End Sub
	
	Sub LoadItem(slot As Integer)
		
		me.Slot = slot
		listView1.SelectedItems.Clear
		listView1.Items.Clear
		If Me.textBox1.Text.Trim <> "" Then
			listView1.ListViewItemSorter = nothing
			FilterList(Me.textBox1.Text.Split(" "))
			exit sub
		End If
	
		listView1.Items.Clear
		If Me.textBox1.Text.Trim <> "" Then
			listView1.ListViewItemSorter = nothing
			FilterList(Me.textBox1.Text.Split(" "))
			exit sub
		End If
		
		
		Dim xList As Xml.XmlNodeList
		Dim xNode As Xml.XmlNode
		Me.Slot = slot
		xList = gemDB.SelectNodes("/enchant/item[slot="& slot & "][reqskill='0' or reqskill='" & GetSkillID(Me.MainFrame.cmbSkill1.SelectedItem) & "'    or reqskill='" & GetSkillID(Me.MainFrame.cmbSkill2.SelectedItem) & "']")
		
		
		For Each xNode In xList
			AddItem(xNode)
		Next
		Dim col As ColumnHeader
		'rezize
		For Each col In listView1.Columns
			col.AutoResize (ColumnHeaderAutoResizeStyle.HeaderSize)
		Next
		listView1.Columns.Item(0).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
		'hide unneeded colums
		Dim lItem As ListViewItem
		dim toHide as Boolean
		For Each col In listView1.Columns
			toHide = true
			For Each lItem In listView1.Items
				If 	lItem.SubItems.Item(col.Index).Text <> "0" Then
					toHide = false
				End If
			Next
			if toHide then col.Width=0
		Next
		
	End Sub
	
	
	
	Sub TextBox1TextChanged(sender As TextBox, e As EventArgs)
		If sender.Text.Trim <> "" Then
			listView1.ListViewItemSorter = nothing
			listView1.Items.Clear
			FilterList(sender.Text.Split(" "))
		Else
			listView1.ListViewItemSorter = nothing
			listView1.Items.Clear
			LoadItem(me.Slot)
		End If
	End Sub
	
	Sub FilterList( filter As String())
		Dim xList As Xml.XmlNodeList
		Dim xNode As Xml.XmlNode
	
		xList = gemDB.SelectNodes("/enchant/item[slot="& slot & "][reqskill='0' or reqskill='" & GetSkillID(Me.MainFrame.cmbSkill1.SelectedItem) & "'    or reqskill='" & GetSkillID(Me.MainFrame.cmbSkill2.SelectedItem) & "']")
		
		
		
		Dim s As String
		dim ToAdd as Boolean
		For Each xNode In xList
			ToAdd = true
			For Each s In filter
				If xNode.InnerText.ToUpper.Contains(S.ToUpper)=False Then
					ToAdd = false
				End If
			Next
			If ToAdd Then AddItem(xNode)
		Next
	End Sub
	Sub AddItem(xNode As Xml.XmlNode)
		Dim cxNode As Xml.XmlNode
		Dim txDoc As New Xml.XmlDocument
		Dim myItem As ListViewItem
		txDoc.LoadXml ( xNode.OuterXml)
		cxNode = txDoc.SelectSingleNode("/item/name")
		myItem = ListView1.Items.Insert(0,cxNode.InnerText)
		
		
		'dim lvSubItem as ListView.ListViewItemCollection
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/id").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/Strength").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/Agility").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/ExpertiseRating").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/HitRating").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/AttackPower").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/CritRating").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/ArmorPenetrationRating").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/HasteRating").InnerText)

	End Sub
	
	
	Sub GearSelectorLoad(sender As Object, e As EventArgs)
		
	End Sub
	Sub GearSelectorClose (sender As Object, e As FormClosingEventArgs)
		if SelectedItem <> "" then me.DialogResult = DialogResult.OK
	End Sub
	
	Sub CmdClearClick(sender As Object, e As EventArgs)
		SelectedItem = 0
		me.Close
	End Sub
End Class
