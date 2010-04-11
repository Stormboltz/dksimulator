'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 11/04/2010
' Heure: 12:08
' 
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class FutureEventManager
	protected List As new Collections.ArrayList
	Friend Max As Integer
	
	Sub Add( T As Long, Ev As String)
		Dim FE As New FutureEvent(T, Ev)
		
		List.Insert(GetRowToInsert(T),FE)
		If List.Count > Max Then max = 	List.Count
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
	
	
	
	
	
End Class
