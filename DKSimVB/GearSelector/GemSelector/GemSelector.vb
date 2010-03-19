'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 2/22/2010
' Heure: 4:32 PM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Partial Class GemSelector
	Inherits System.Windows.Forms.Form
	
	Dim sortColumn As Integer = -1
	Friend gemDB As New Xml.XmlDocument
	Friend Slot As String
	Friend SelectedItem As String
	Friend MainFrame as GearSelectorMainForm
	Public Sub New(M As GearSelectorMainForm)
		
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		MainFrame = M
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
		ListView1.Columns.Add("EPVal")
		gemDB.Load(Application.StartupPath & "\GearSelector\" & "gems.xml")
		If sortColumn = -1 Then
			sortColumn = 10
			listView1.Sorting = SortOrder.Descending
			listView1.Sort()
			listView1.ListViewItemSorter = New ListViewItemComparer(sortColumn, listView1.Sorting)
		End If
		
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	Sub ListView1SelectedIndexChanged(sender As Object, e As EventArgs)
		SelectedItem = sender.SelectedItems.Item(0).subitems.item(1).text
		me.hide
	End Sub
	Sub ListView1ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs)
		' Determine whether the column is the same as the last column clicked.
	    If e.Column <> sortColumn Then
	        ' Set the sort column to the new column.
	        sortColumn = e.Column
	        ' Set the sort order to ascending by default.
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
	
	Sub LoadItem(Optional slot As Integer = -1)
		
		listView1.SelectedItems.Clear
		listView1.Items.Clear
		Me.Slot = slot
		If Me.textBox1.Text.Trim <> "" Then
			listView1.ListViewItemSorter = nothing
			FilterList(Me.textBox1.Text)
			listView1.ListViewItemSorter = New ListViewItemComparer(sortColumn, listView1.Sorting)
			exit sub
		End If
		
		
		Dim xList As Xml.XmlNodeList
		Dim xNode As Xml.XmlNode
		
		If slot = 1 Then
			xList = gemDB.SelectNodes("/gems/item[subclass='6']")
		Else
			xList = gemDB.SelectNodes("/gems/item[subclass!='6']")
		End If
		
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
			FilterList(sender.Text)
			listView1.ListViewItemSorter = New ListViewItemComparer(sortColumn, listView1.Sorting)
		Else
			listView1.ListViewItemSorter = nothing
			listView1.Items.Clear
			LoadItem(me.Slot)
			listView1.ListViewItemSorter = New ListViewItemComparer(sortColumn, listView1.Sorting)
		End If
	End Sub
	
	Sub FilterList( filter As String)
		Dim xList As Xml.XmlNodeList
		Dim xNode As Xml.XmlNode
		If slot = 1 Then
			xList = gemDB.SelectNodes("/gems/item[subclass='6']")
		Else
			xList = gemDB.SelectNodes("/gems/item[subclass!='6']")
		End If
		
		For Each xNode In xList
			If xNode.InnerText.ToUpper.Contains(filter.ToUpper) Then
				AddItem(xNode)
			End If
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
		myItem.SubItems.Add(getItemEPValue(txDoc))
		myItem.BackColor = GemColor(txDoc.SelectSingleNode("/item/subclass").InnerText)
	End Sub
	
	
	Sub GearSelectorLoad(sender As Object, e As EventArgs)
		
	End Sub
	Sub GearSelectorClose (sender As Object, e As FormClosingEventArgs)
		if SelectedItem <> "" then me.DialogResult = DialogResult.OK
	End Sub
	
	Function getItemEPValue(txDoc As Xml.XmlDocument) As Double
		Dim tmp As Double = 0
		tmp += txDoc.SelectSingleNode("/item/Strength").InnerText * MainFrame.EPvalues.Str
		tmp += txDoc.SelectSingleNode("/item/Agility").InnerText* MainFrame.EPvalues.Agility
		tmp += txDoc.SelectSingleNode("/item/ExpertiseRating").InnerText* MainFrame.EPvalues.Exp
		tmp += txDoc.SelectSingleNode("/item/HitRating").InnerText* MainFrame.EPvalues.Hit
		tmp += txDoc.SelectSingleNode("/item/AttackPower").InnerText* 1
		tmp += txDoc.SelectSingleNode("/item/CritRating").InnerText* MainFrame.EPvalues.Crit
		tmp += txDoc.SelectSingleNode("/item/ArmorPenetrationRating").InnerText* MainFrame.EPvalues.ArP
		tmp += txDoc.SelectSingleNode("/item/HasteRating").InnerText* MainFrame.EPvalues.Haste
		return tmp
	End Function
	Sub CmdClearClick(sender As Object, e As EventArgs)
		SelectedItem = 0
		me.Close
	End Sub
End Class
