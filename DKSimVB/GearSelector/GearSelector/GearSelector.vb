'
' Crée par SharpDevelop.
' Utilisateur: e0030653
' Date: 2/22/2010
' Heure: 4:32 PM
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Partial Class GearSelector
	Inherits System.Windows.Forms.Form
	Dim sortColumn As Integer = -1
	
	Friend ItemDB As New Xml.XmlDocument
	Friend Slot As String
	Friend SelectedItem As String = -1
	Friend MainFrame as GearSelectorMainForm
	
	
	Public Sub New(m as GearSelectorMainForm )
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		MainFrame = m
		
		ListView1.Columns.Add("Name")
		ListView1.Columns.Add("id")
		ListView1.Columns.Add("iLvl")
		'ListView1.Columns.Add("Slot")
		ListView1.Columns.Add("HM")
		ListView1.Columns.Add("Str")
		ListView1.Columns.Add("Agi")
		ListView1.Columns.Add("Arm")
		ListView1.Columns.Add("+Arm")
		ListView1.Columns.Add("Exp")
		ListView1.Columns.Add("Dps")
		ListView1.Columns.Add("Speed")
		ListView1.Columns.Add("Hit")
		ListView1.Columns.Add("AP")
		ListView1.Columns.Add("Crit")
		ListView1.Columns.Add("ArP")
		ListView1.Columns.Add("Haste")
		
		ListView1.Columns.Add("Set")
		ListView1.Columns.Add("Gem1")
		ListView1.Columns.Add("Gem2")
		ListView1.Columns.Add("Gem3")
		ListView1.Columns.Add("GemBns")
		ListView1.Columns.Add("EPValue")
		
		ItemDB.Load(Application.StartupPath & "\GearSelector\" & "itemDB.xml")
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	Sub ListView1SelectedIndexChanged(sender As Object, e As EventArgs)
		SelectedItem = sender.SelectedItems.Item(0).subitems.item(1).text
		me.Close
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
	
	Sub LoadItem(Slot As String)
		me.Slot = slot
		listView1.SelectedItems.Clear
		listView1.Items.Clear
		If Me.textBox1.Text.Trim <> "" Then
			listView1.ListViewItemSorter = nothing
			FilterList(Me.textBox1.Text)
			exit sub
		End If
		
		
		
		
		Dim xList As Xml.XmlNodeList
		Dim xNode As Xml.XmlNode
		
		xList = ItemDB.SelectNodes("/items/item[slot="& slot &"]")
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
				Try
					If 	lItem.SubItems.Item(col.Index).Text <> "0" Then
						toHide = false
					End If
				Catch
					toHide = false
				End Try
			Next
			if toHide then col.Width=0
		Next
			
		If sortColumn = -1 Then
			sortColumn = 2
			listView1.Sorting = SortOrder.Descending
			listView1.Sort()
			listView1.ListViewItemSorter = New ListViewItemComparer(sortColumn, listView1.Sorting)
		End If
		
	End Sub
	
	
	
	Sub TextBox1TextChanged(sender As TextBox, e As EventArgs)
		If sender.Text.Trim <> "" Then
			listView1.Items.Clear
			listView1.ListViewItemSorter = nothing
			FilterList(sender.Text)
			listView1.ListViewItemSorter = New ListViewItemComparer(sortColumn, listView1.Sorting)
		Else
			listView1.Items.Clear
			listView1.ListViewItemSorter = nothing
			LoadItem(slot)
			listView1.ListViewItemSorter = New ListViewItemComparer(sortColumn, listView1.Sorting)
		End If
		
	End Sub
	
	Sub FilterList( filter As String)
		Dim xList As Xml.XmlNodeList
		Dim xNode As Xml.XmlNode
		xList = ItemDB.SelectNodes("/items/item[slot="& slot &"]")
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
		'on error resume next
		txDoc.LoadXml ( xNode.OuterXml)
		cxNode = txDoc.SelectSingleNode("/item/name")
		myItem = ListView1.Items.Insert(0,cxNode.InnerText)
		'myItem.BackColor = Color.Coral
		
		'dim lvSubItem as ListView.ListViewItemCollection
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/id").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/ilvl").InnerText)
		'myItem.SubItems.Add(txDoc.SelectSingleNode("/item/slot").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/heroic").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/Strength").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/Agility").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/Armor").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/BonusArmor").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/ExpertiseRating").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/dps").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/speed").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/HitRating").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/AttackPower").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/CritRating").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/ArmorPenetrationRating").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/HasteRating").InnerText)
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/setid").InnerText)
		
		dim lvsI as ListViewItem.ListViewSubItem
		
		lvsI = myItem.SubItems.Add(GemSlotColorName(txDoc.SelectSingleNode("/item/gem1").InnerText))
		lvsI = myItem.SubItems.Add(GemSlotColorName(txDoc.SelectSingleNode("/item/gem2").InnerText))
		lvsI = myItem.SubItems.Add(GemSlotColorName(txDoc.SelectSingleNode("/item/gem3").InnerText))
		myItem.SubItems.Add(txDoc.SelectSingleNode("/item/gembonus").InnerText)
		myItem.SubItems.Add(getItemEPValue(txDoc))
		
	End Sub
	Function getItemEPValue(txDoc As Xml.XmlDocument) As Double
		Dim tmp As Double = 0
		
		tmp += txDoc.SelectSingleNode("/item/Strength").InnerText * MainFrame.EPvalues.Str
		tmp += txDoc.SelectSingleNode("/item/Agility").InnerText* MainFrame.EPvalues.Agility
		tmp += txDoc.SelectSingleNode("/item/Armor").InnerText* MainFrame.EPvalues.Armor
		tmp += txDoc.SelectSingleNode("/item/BonusArmor").InnerText* MainFrame.EPvalues.Armor
		tmp += txDoc.SelectSingleNode("/item/ExpertiseRating").InnerText* MainFrame.EPvalues.Exp
		tmp += txDoc.SelectSingleNode("/item/dps").InnerText.Replace(".",System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) * MainFrame.EPvalues.MHDPS
		tmp += txDoc.SelectSingleNode("/item/speed").InnerText.Replace(".",System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)* MainFrame.EPvalues.MHSpeed
		tmp += txDoc.SelectSingleNode("/item/HitRating").InnerText* MainFrame.EPvalues.Hit
		tmp += txDoc.SelectSingleNode("/item/AttackPower").InnerText* 1
		tmp += txDoc.SelectSingleNode("/item/CritRating").InnerText* MainFrame.EPvalues.Crit
		tmp += txDoc.SelectSingleNode("/item/ArmorPenetrationRating").InnerText* MainFrame.EPvalues.ArP
		tmp += txDoc.SelectSingleNode("/item/HasteRating").InnerText* MainFrame.EPvalues.Haste
		If  txDoc.SelectSingleNode("/item/gem1").InnerText <> 0 Then
			tmp += 20 * MainFrame.EPvalues.Str
		End If
		If  txDoc.SelectSingleNode("/item/gem2").InnerText <> 0 Then
			tmp += 20 * MainFrame.EPvalues.Str
		End If
		If  txDoc.SelectSingleNode("/item/gem3").InnerText <> 0 Then
			tmp += 20 * MainFrame.EPvalues.Str
		End If
		return tmp
		
		
	End Function
	
	
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
