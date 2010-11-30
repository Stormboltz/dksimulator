Imports System.Xml.Linq
Imports System.Linq
Imports System.IO.IsolatedStorage
Imports System.IO

Partial Public Class EnchantEditor
    Inherits ChildWindow

    Dim EnchantDB As XDocument

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs) Handles OKButton.Click
        Me.DialogResult = True
    End Sub


    Sub LoadEnchantList()
        dgEnchant.AutoGenerateColumns = True
        Dim doc As XDocument = XDocument.Load("GearSelector/Enchant.xml")

        Dim EnchantList As List(Of aEnchant)


        EnchantList = (From el In doc.Elements("enchant").Elements
                        Order By el.<slot>.Value Descending
                        Select getEnchant(el)).ToList()
        dgEnchant.ItemsSource = EnchantList
    End Sub



    Private Sub btnViewXML_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnViewXML.Click
        Dim xmlEnchant As XDocument = XDocument.Parse("<enchant/>")

        Dim EnchantList As List(Of aEnchant) = dgEnchant.ItemsSource

        For Each ench In EnchantList
            Dim xElem As New XElement("item")
            With ench
                xElem.Add(New XElement("id", .Id))
                xElem.Add(New XElement("slot", .slot))
                xElem.Add(New XElement("name", .name))
                xElem.Add(New XElement("Strength", .Str))
                xElem.Add(New XElement("Agility", .Agi))
                xElem.Add(New XElement("HasteRating", .Haste))
                xElem.Add(New XElement("ExpertiseRating", .Exp))
                xElem.Add(New XElement("HitRating", .Hit))
                xElem.Add(New XElement("AttackPower", .AP))
                xElem.Add(New XElement("CritRating", .Crit))
                xElem.Add(New XElement("ArmorPenetrationRating", .ArP))
                xElem.Add(New XElement("reqskill", .ReqSK))
                xElem.Add(New XElement("Desc", .Desc))
                xElem.Add(New XElement("Stamina", .Stam))
                xElem.Add(New XElement("BonusArmor", .BonusArmor))
                xElem.Add(New XElement("MasteryRating", .Mast))
                xElem.Add(New XElement("DodgeRating", .Dodge))
                xElem.Add(New XElement("ParryRating", .Parry))
            End With
            xmlEnchant.Element("enchant").Add(xElem)
        Next
        Dim isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
        Dim isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("tmp.xml", FileMode.OpenOrCreate, FileAccess.Write, isoStore)
        xmlEnchant.Save(isoStream)
        isoStream.Close()

        Dim xViewer As New myTextReader
        xViewer.OpenFileFromISO("tmp.xml")
        xViewer.Show()
        isoStore.DeleteFile("tmp.xml")



    End Sub

    Class aEnchant
        Property Id As Integer
        Property name As String
        Property slot As Integer
        Property Str As Integer
        Property Intel As Integer
        Property Agi As Integer
        Property Haste As Integer
        Property Exp As Integer
        Property Hit As Integer
        Property AP As Integer
        Property Crit As Integer
        Property Mast As Integer
        Property Desc As String
        Property BonusArmor As Integer
        Property Dodge As Integer
        Property Parry As Integer
        Property ReqSK As Integer
        Property ArP As Integer
        Property Stam As Integer
    End Class
    Function getEnchant(ByVal el As XElement) As aEnchant
        Dim myEnch As New aEnchant
        With myEnch
            .Id = el.Element("id").Value
            .name = el.Element("name").Value
            .slot = el.<slot>.Value
            .Str = el.Element("Strength").Value
            .Agi = el.Element("Agility").Value
            .Haste = el.Element("HasteRating").Value
            .Exp = el.Element("ExpertiseRating").Value
            .Hit = el.Element("HitRating").Value
            .AP = el.Element("AttackPower").Value
            .Crit = el.Element("CritRating").Value
            .Mast = el.<MasteryRating>.Value
            .Desc = el.Element("Desc").Value
            .ReqSK = el.<reqskill>.Value
            .BonusArmor = el.<BonusArmor>.Value
            .Dodge = el.<DodgeRating>.Value
            .Parry = el.<ParryRating>.Value
            .ArP = el.Element("ArmorPenetrationRating").Value
            .Stam = el.<Stamina>.Value
        End With
        Return myEnch
    End Function

    Private Sub ChildWindow_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        LoadEnchantList()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAdd.Click
        Dim EnchantList As List(Of aEnchant) = dgEnchant.ItemsSource
        Dim Ench As New aEnchant
        EnchantList.Add(Ench)
        dgEnchant.ItemsSource = EnchantList
    End Sub
End Class
