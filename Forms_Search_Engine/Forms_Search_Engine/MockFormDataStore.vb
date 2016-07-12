Imports System.Data
Imports Forms_Search_Engine

Public Class MockFormDataStore
    Implements IDataStore

    Public Sub New(ByVal details)

    End Sub
    Public Function ExecuteQuery(query As String) As Tuple(Of DataSet, String) Implements IDataStore.ExecuteQuery
        Return Tuple.Create(Of DataSet, String)(New DataSet, "")
    End Function
End Class
