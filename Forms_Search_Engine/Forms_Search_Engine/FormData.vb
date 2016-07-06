Imports System.Linq
Public Class FormData
    Public Property FormId As String
    Public Property ProgramName As String
    Public Property Description As String
    Public Property Offset As String
    Public Property DateModified As DateTime

    Public Sub New(ByVal id As String, name As String, desc As String, offset As String, modi As DateTime)
        FormId = id
        ProgramName = name
        Description = desc
        Me.Offset = offset
        DateModified = modi
    End Sub

    Public Shared Function FetchExisting(ByVal crit As String) As List(Of FormData)
        Dim l As New List(Of FormData)
        Dim dm As New DataManager
        Dim dt = dm.ExecuteQuery(crit)
        For Each dr In dt.Item1.Tables.First.Rows
            Dim itm As New FormData(dr("FormId"), dr("ProgramName"), ...)
            l.Add(itm)
        Next
        Return l
    End Function
End Class
