Imports System.Data
Imports System.Data.SqlClient

Public Interface IDataStore
    Function ExecuteQuery(query As String) As Tuple(Of SqlDataReader, String)
End Interface
