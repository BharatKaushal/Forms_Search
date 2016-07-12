Imports System.Data

Public Interface IDataStore
    Function ExecuteQuery(query As String) As Tuple(Of DataSet, String)
End Interface
