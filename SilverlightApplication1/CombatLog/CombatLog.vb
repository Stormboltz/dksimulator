Imports System.IO.IsolatedStorage
Imports System.IO

'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 19:47
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator
    Public Class CombatLog
        Private txtFile As StreamWriter

        Friend enable As Boolean
        Friend LogDetails As Boolean
        Private Sim As Sim
        Sub New(ByVal S As Sim)
            'txtFile = New System.IO.StreamWriter("Combatlog/Combatlog" & " _" & Now.Day & Now.Hour & Now.Minute & Now.Second & ".txt")
            LogDetails = True
            enable = True
            Sim = S
        End Sub

        Sub InitcombatLog()
            Try
                Dim x As IsolatedStorageFileStream
                x = New IsolatedStorageFileStream("KahoDKSim/Combatlog/Combatlog.txt", FileMode.Create, Sim.isoStore)
                txtFile = New StreamWriter(x)


            Catch ex As Exception
                msgBox(ex.StackTrace)
            End Try

        End Sub

        Sub WriteDetails(ByVal s As String)
            If LogDetails Then write(s)
        End Sub

        Sub write(ByVal s As String)
            If Sim.EPStat <> "" Then
                enable = False
                Return
            End If
            If Sim.TimeStamp > (10 * 60 * 100) Then Exit Sub 'To not explode Storage
            If enable Then
                Dim tmp As String
                tmp = ""
                tmp = Sim.Runes.RuneState()

                If txtFile Is Nothing Then InitcombatLog()
                txtFile.WriteLine(tmp & vbTab & s & vbTab & "RP left = " & Sim.RunicPower.GetValue())
            End If

        End Sub
        Sub finish()
            On Error Resume Next
            If enable Then txtFile.Close()
        End Sub
    End Class
End Namespace