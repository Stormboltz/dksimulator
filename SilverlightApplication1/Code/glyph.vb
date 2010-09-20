Imports System.Xml.Linq
Imports System.IO.IsolatedStorage
Imports System.IO

'
' Created by SharpDevelop.
' User: Fabien
' Date: 13/03/2009
' Time: 14:27
'
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Namespace Simulator.Character
    Friend Class Glyphs

        Default ReadOnly Property Glyph(ByVal name As String) As Boolean
            Get
                Return GetGlyph(name).Equiped
            End Get
        End Property


        Friend GlyphCol As New Collections.Generic.Dictionary(Of String, Glyph)
        Friend xmlGlyph As New XDocument
        Friend GoD As Boolean
        Dim sim As Sim

        Sub New(ByVal s As Sim, ByVal path As String)
            On Error Resume Next
            Using isoStore As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
                Using isoStream As IsolatedStorageFileStream = New IsolatedStorageFileStream("KahoDKSim/" & path, FileMode.Open, FileAccess.Read, isoStore)
                    xmlGlyph = XDocument.Load(isoStream)
                End Using
            End Using
            Dim Gl As Glyph
            For Each e As XElement In xmlGlyph.<Talents>.<Glyphs>.<Prime>.Elements
                Gl = New Glyph(GlyphCol, e.Name.ToString, e.Value)
            Next

            For Each e As XElement In xmlGlyph.<Talents>.<Glyphs>.<Major>.Elements
                Gl = New Glyph(GlyphCol, e.Name.ToString, e.Value)
            Next
            If Me("disease") Then GoD = True
        End Sub

        Function GetGlyph(ByVal Name As String) As Glyph
            Try
                Name = Name.ToLower
                Return GlyphCol(Name)
            Catch ex As Exception
                Diagnostics.Debug.WriteLine("WTF IS THIS Glyph: " & Name)
                Return GlyphCol("none")
            End Try
        End Function
    End Class
    Class Glyph
        Friend Name As String
        Friend Equiped As Boolean

        Sub New(ByVal s As Sim, ByVal GlyphName As String, ByVal Equip As Boolean)
            Equiped = Equip
            Name = GlyphName.ToLower
            Try
                s.Character.Glyph.GlyphCol.Add(Name, Me)
            Catch
                Diagnostics.Debug.WriteLine("Error Adding Glyph " & GlyphName)
            End Try

        End Sub
        Sub New(ByVal GlyphList As Dictionary(Of String, Glyph), ByVal GlyphName As String, ByVal Equip As Boolean)
            Equiped = Equip
            Name = GlyphName.ToLower
            Try
                GlyphList.Add(Name, Me)
            Catch
                Diagnostics.Debug.WriteLine("Error Adding Glyph " & GlyphName)
            End Try

        End Sub
    End Class
End Namespace
