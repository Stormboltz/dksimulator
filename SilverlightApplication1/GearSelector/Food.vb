Imports System.Xml.Linq

Public Class Food
    Inherits WowItem


    Protected foodDB As XDocument
    Protected MainFrame As FrmGearSelector

    Sub New(ByVal MainFrm As FrmGearSelector)
        MainFrame = MainFrm
        foodDB = MainFrame.FoodDB
    End Sub

    Sub Attach(ByVal FoodId As Integer)
        If FoodId = 0 Then
            Unload()
            Exit Sub
        End If
        Dim el As XElement = (From x In MainFrame.FoodDB.<food>.Elements
                              Where x.<id>.Value = FoodId
                              ).First
        Load(el)
        
    End Sub

    Sub Attach(ByVal FoodName As String)
        If FoodName = "" Then
            Unload()
            Exit Sub
        End If
        Dim el As XElement = (From x In MainFrame.FoodDB.<food>.Elements
                              Where x.<name>.Value = FoodName
                              ).First
        Load(el)
    End Sub



    
End Class
