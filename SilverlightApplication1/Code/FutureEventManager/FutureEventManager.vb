'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 11/04/2010
' Heure: 12:08
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class FutureEventManager
    Protected List As New Collections.Generic.List(Of FutureEvent)
	Friend Max As Integer
	Protected sim As Sim
	
	Sub New(s As Sim)
		sim = s
	End Sub
	
	
	Sub Add( T As Long, Ev As String)
		If T < sim.TimeStamp Then
            Diagnostics.Debug.WriteLine("Try to go back in time")
		End If
		Dim FE As New FutureEvent(T, Ev)
        List.Insert(GetRowToInsert(T), FE)
		'If List.Count > Max Then max = 	List.Count
	End Sub
	
	Function GetRowToInsert(T As Long) as Integer
		Dim FE As FutureEvent
		Dim i As Integer
		For i=0 To List.Count-1
			FE = List.Item(i)
			if FE.T > T  then return i
		Next
		return List.Count
	End Function
	
	Function GetFirst() As FutureEvent
		Dim FE As FutureEvent
		FE = list.Item(0)
		list.Remove(list.Item(0))
		Return FE
	End Function
	
	Sub Clear
		List.Clear
	End Sub
	
	Sub Remove(Ev As String)
		Dim FE As FutureEvent
		For Each FE In List
			If FE.Ev = Ev Then
                List.Remove(FE)
			End If
		Next
	End Sub
	
	Sub Reschedule(Ev As String, T As Long)
		Dim FE As FutureEvent
		For Each FE In List
			If FE.Ev = Ev Then FE.T = T
		Next
		Dim myComparer as myEventComparer = New myEventComparer()
        List.Sort(myComparer)
	End Sub
	
	Public Class myEventComparer

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer
            Dim FEx As FutureEvent
            Dim FEy As FutureEvent
            FEx = x
            FEy = y

            'Return New CaseInsensitiveComparer().Compare(FEx.T, FEy.T)
        End Function 'IComparer.Compare
   End Class

	
	
	
	
End Class
