'
' Crée par SharpDevelop.
' Utilisateur: Fabien
' Date: 15/03/2010
' Heure: 11:13
'
' Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
'
Public Class ListViewItemComparer
	    Implements IComparer
    Private col As Integer
    Private order As SortOrder
    
    
    Public Sub New()
        col = 0
        order = SortOrder.Ascending
    End Sub
    
    Public Sub New(column As Integer, order as SortOrder)
        col = column
        Me.order = order
    End Sub
    
    
    Public Function Compare(x As Object, y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim returnVal As Integer = -1
        
        Try 'Sort a number first
        	Dim FirstDouble As Double = Double.Parse(CType(x, ListViewItem).SubItems(col).Text)
        	Dim SecondDouble As Double = Double.Parse(CType(y, ListViewItem).SubItems(col).Text)
        	If FirstDouble > SecondDouble Then
        		returnVal = -1
        	Else
        		returnVal = 1
        	End If
        Catch 'Sort a string if error
        	returnVal = [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
	        If order = SortOrder.Descending Then
	            returnVal *= -1
	        End If
        End Try
        
        
        
        
        
	    
        Return returnVal
    End Function
End Class
