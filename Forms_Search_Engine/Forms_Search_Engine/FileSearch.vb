Imports System.IO
Imports System.Text.RegularExpressions
Public Class FileSearch
    Public Shared Function GetFilesRecursive(ByVal initial As String) As List(Of String)
        Dim result As New List(Of String)
        Dim stack As New Stack(Of String)
        stack.Push(initial)
        Do While (stack.Count > 0)
            Dim dir As String = stack.Pop
            Try
                result.AddRange(Directory.GetFiles(dir, "2*.bas"))
                If InStr(dir, "\src") Then
                    result.AddRange(Directory.GetFiles(dir, "_3*.vb"))
                End If
                Dim directoryName As String
                For Each directoryName In Directory.GetDirectories(dir)
                    stack.Push(directoryName)
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Loop
        Return result
    End Function
    Public Shared Function RemoveSpecialCharacters(ByVal input As String) As String
        Dim r As New Regex("(?:[^a-z0-9 ]|(?<=['""])s)", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant Or RegexOptions.Compiled)
        Return r.Replace(input, [String].Empty)
    End Function
End Class