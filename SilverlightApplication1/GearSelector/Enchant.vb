Imports System.Xml.Linq

'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 18/03/2010
' Heure: 18:21
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class Enchant
    Inherits WowItem

    Protected EnchantDB As XDocument
    Protected MainFrame As FrmGearSelector

    Sub New(ByVal MainFrm As FrmGearSelector)
        MainFrame = MainFrm
        EnchantDB = MainFrame.EnchantDB
    End Sub
    Sub Attach(ByVal EnchantId As Integer)
        If EnchantId = 0 Then
            Unload()
            Exit Sub
        End If
        Id = EnchantId
        Try
            Dim el As XElement = (From x In MainFrame.EnchantDB.<enchant>.Elements
                                          Where x.<id>.Value = EnchantId
                                          ).First

            Load(el)

        Catch ex As Exception
            Log.Log(ex.StackTrace, logging.Level.ERR)
            Unload()
        End Try

    End Sub
    Public Overrides Sub Unload()
        MyBase.Unload()
        name = "Enchant"
    End Sub
    
End Class
