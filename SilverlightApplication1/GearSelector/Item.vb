Imports System.Xml.Linq
Imports System.Linq

'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 18/03/2010
' Heure: 16:45
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Item
    Inherits WowItem

    Friend setid As Integer
    Friend gembonus As Integer
    Friend keywords As String

    Friend gem1 As Gem
    Friend gem2 As Gem
    Friend gem3 As Gem
    Friend Enchant As Enchant

    Friend ReForgingFrom As String
    Friend ReForgingTo As String
    Friend ReForgingvalue As Integer

    Protected MainFrame As FrmGearSelector
    Protected ItemDB As XDocument
    Protected AdditionalGemNotSet As Boolean

    Sub New(ByVal MainFrm As FrmGearSelector, Optional ByVal ItemId As Integer = 0)
        MainFrame = MainFrm
        ItemDB = MainFrame.ItemDB

        gem1 = New Gem(Me.MainFrame, 0)
        gem2 = New Gem(Me.MainFrame, 0)
        gem3 = New Gem(Me.MainFrame, 0)
        Enchant = New Enchant(Me.MainFrame)
        If ItemId <> 0 Then LoadItem(ItemId)
    End Sub



    Sub LoadItem(ByVal ItemId As Integer)
        On Error Resume Next

        AdditionalGemNotSet = True
        Id = ItemId
        If Id = 0 Then
            Unload()
            Exit Sub
        End If

        Dim myItem As XElement
        myItem = (From el In ItemDB.<items>.Elements Where el.<id>.Value = ItemId).First

        MyBase.Load(myItem)
        setid = myItem.<setid>.Value

        Dim gem1Col As Integer = myItem.<gem1>.Value
        Dim gem2Col As Integer = myItem.<gem2>.Value
        Dim gem3Col As Integer = myItem.<gem3>.Value
        Dim i As Integer
        i = gem1.Id
        If gem1Col <> 0 Then
            i = gem1.Id
            gem1 = New Gem(Me.MainFrame, gem1Col)
            gem1.Attach(i)
        Else
            If AdditionalGem() And AdditionalGemNotSet Then
                gem1 = New Gem(Me.MainFrame, 16)
                gem1.Attach(i)
                AdditionalGemNotSet = False
            Else
                gem1 = New Gem(Me.MainFrame, 0)
            End If
        End If
        i = gem2.Id
        If gem2Col <> 0 Then
            gem2 = New Gem(Me.MainFrame, gem2Col)
            gem2.Attach(i)
        Else
            If AdditionalGem() And AdditionalGemNotSet Then
                gem2 = New Gem(Me.MainFrame, 16)
                gem2.Attach(i)
                AdditionalGemNotSet = False
            Else
                gem2 = New Gem(Me.MainFrame, 0)
            End If

        End If
        i = gem3.Id
        If gem3Col <> 0 Then
            gem3 = New Gem(Me.MainFrame, gem3Col)
            gem3.Attach(i)
        Else
            If AdditionalGem() And AdditionalGemNotSet Then
                gem3 = New Gem(Me.MainFrame, 16)
                gem3.Attach(i)
                AdditionalGemNotSet = False
            Else
                gem3 = New Gem(Me.MainFrame, 0)
            End If
        End If
        gembonus = myItem.<gembonus>.Value
        keywords = myItem.<keywords>.Value
        icon = myItem.<icon>.Value
    End Sub


    Overrides Sub Unload()
        MyBase.Unload()
        setid = 0
        gem1.Unload()
        gem2.Unload()
        gem3.Unload()
        Enchant.Unload()
        gembonus = 0
        keywords = ""
    End Sub

    Function AdditionalGem() As Boolean
        Select Case slot
            Case 9, 10
                If GetSkillID(Me.MainFrame.ParentFrame.cmbSkill1.SelectedItem) = 164 Or GetSkillID(Me.MainFrame.ParentFrame.cmbSkill2.SelectedItem) = 164 Then
                    Return True
                Else
                    Return False
                End If
            Case 6
                Return True
            Case Else
                Return False
        End Select

    End Function

    Function IsGembonusActif() As Boolean

        If gem1.IsGemrightColor Or gem1.ColorId = 0 Or gem1.ColorId = 16 Then
            If gem2.IsGemrightColor Or gem2.ColorId = 0 Or gem2.ColorId = 16 Then
                If gem3.IsGemrightColor Or gem3.ColorId = 0 Or gem3.ColorId = 16 Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function
End Class
