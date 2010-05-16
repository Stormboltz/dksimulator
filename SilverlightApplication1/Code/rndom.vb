Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO

'
' Created by SharpDevelop.
' User: Fabien
' Date: 03/04/2009
' Time: 23:40
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Public Module rndom
    Friend RandomNumberGenerator As Random
    Friend SavedRNG As Random
    Friend RNGSeed As Integer




    Sub InitRNG()

        Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
            Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/EPconfig.xml", FileMode.Open, isoStore)
                Dim doc As XDocument = XDocument.Load(isoStream)
            


        If doc.Element("config/Misc/chkEPSeed").Value = "True" Then
            If RNGSeed = 0 Then
                RandomNumberGenerator = New Random
                RNGSeed = RandomNumberGenerator.Next()
                WriteReport("RNG seed= " & RNGSeed)
            End If
            RandomNumberGenerator = New Random(RNGSeed)
        Else
            RandomNumberGenerator = New Random
                End If
            End Using
        End Using
    End Sub
End Module
