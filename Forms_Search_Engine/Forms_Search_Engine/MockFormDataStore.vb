Imports System.Data
Imports System.Data.SqlClient
Imports Forms_Search_Engine

Public Class MockFormDataStore
    Implements IDataStore

    Public Sub New(ByVal details)

    End Sub
    Public Function ExecuteQuery(query As String) As Tuple(Of SqlDataReader, String) Implements IDataStore.ExecuteQuery
        'Return Tuple.Create(Of DataSet, String)(New SqlDataReader, "")
    End Function
End Class
