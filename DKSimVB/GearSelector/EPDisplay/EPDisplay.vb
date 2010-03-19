'
' Created by SharpDevelop.
' User: Fabien
' Date: 28/03/2009
' Time: 18:22
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Friend Partial Class EPDisplay
	private ParentFrame as GearSelectorMainForm
	Friend Sub New(ParentF as Form)
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		ParentFrame = ParentF
		Me.InitializeComponent()
		
		
	End Sub
	
	Friend Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		
	End Sub
	
	
	
	
	
	Sub EPDisplayLoad(sender As Object, e As EventArgs)
		Dim GS As GearSelectorMainForm
		GS = ParentFrame
		dim EPVal as EPValues
		EPVal = GS.ParentFrame.EPVal
		
		listView1.Items.Add("Agility").SubItems.Add(EPVal.Agility)
		listView1.Items.Add("Armor").SubItems.Add(EPVal.Armor)
		listView1.Items.Add("ArP").SubItems.Add(EPVal.ArP)
		listView1.Items.Add("Crit").SubItems.Add(EPVal.Crit)
		listView1.Items.Add("Exp").SubItems.Add(EPVal.Exp)
		listView1.Items.Add("Haste").SubItems.Add(EPVal.Haste)
		listView1.Items.Add("Hit").SubItems.Add(EPVal.Hit)
		listView1.Items.Add("Weapon DPS").SubItems.Add(EPVal.MHDPS)
		listView1.Items.Add("Weapon speed").SubItems.Add(EPVal.MHSpeed)
		listView1.Items.Add("Str").SubItems.Add(EPVal.Str)
		listView1.ListViewItemSorter = new ListViewItemComparer(1,SortOrder.Descending)
		
		
		
	End Sub
End Class
