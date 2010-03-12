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
	
	Friend ItemDB As New Xml.XmlDocument
	Friend Slot As String
	Friend SelectedItem as String
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
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
		
		ItemDB.Load("itemDB.xml")
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	Sub ListView1SelectedIndexChanged(sender As Object, e As EventArgs)
		SelectedItem = sender.SelectedItems.Item(0).subitems.item(1).text
		me.Close
	End Sub
	Sub ListView1ColumnClick(sender As Object, e As EventArgs)
		'ListView1.Sorting.
		'debug.Print("coucou")
	End Sub
	
	Sub LoadItem(Slot As String)
		Dim xList As Xml.XmlNodeList
		Dim xNode As Xml.XmlNode
		me.Slot = slot
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
				If 	lItem.SubItems.Item(col.Index).Text <> "0" Then
					toHide = false
				End If
			Next
			if toHide then col.Width=0
		Next
			
		
	End Sub
	
	
	
	Sub TextBox1TextChanged(sender As TextBox, e As EventArgs)
		If sender.Text.Trim <> "" Then
			listView1.Items.Clear
			FilterList(sender.Text)
		Else
			listView1.Items.Clear
			LoadItem(slot)
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
		txDoc.LoadXml ( xNode.OuterXml)
		cxNode = txDoc.SelectSingleNode("/item/name")
		myItem = ListView1.Items.Insert(0,cxNode.InnerText)
		myItem.BackColor = Color.Coral
		
		dim lvSubItem as ListView.ListViewItemCollection
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
	End Sub
	
	
	Sub GearSelectorLoad(sender As Object, e As EventArgs)
		
	End Sub
	Sub GearSelectorClose (sender As Object, e As FormClosingEventArgs)
		if SelectedItem <> "" then me.DialogResult = DialogResult.OK
		
		
	End Sub
End Class
