Imports System.IO
Imports System.Text.RegularExpressions
Public Class FileSearch
    Public Shared Function GetFilesRecursive(ByVal initial As String) As List(Of String) ', lastRunDate As DateTime
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
    Public Shared Function RemoveDoubleQuotes(ByVal input As String) As String
        Dim pattern As String = "\"""
        Dim replacement As String = String.Empty
        Dim rgx As New Regex(pattern)
        Dim result As String = rgx.Replace(input, replacement)
        Return result
    End Function
    Public Shared Function RemoveSpecialCharacters(ByVal input As String) As String
        Dim r As New Regex("(?:[^a-z0-9 ]|(?<=['""])s)", RegexOptions.IgnoreCase Or RegexOptions.CultureInvariant Or RegexOptions.Compiled)
        Return r.Replace(input, [String].Empty)
    End Function
    Public Shared Sub GetFileInfo()
        Dim list As List(Of String) = GetFilesRecursive("C:\Users\bharats\Desktop\New folder")
        Dim f As New FormData
        ' Loop through and display each path.
        For Each path In list
            f.ProgramName = RemoveSpecialCharacters(System.IO.Path.GetFileNameWithoutExtension(path))
            f.File_Path = path
            f.Date_Modified = System.IO.File.GetLastWriteTime(path)
            Dim list_text As New List(Of String)
            Using r As StreamReader = New StreamReader(path)
                Dim line_text As String
                line_text = r.ReadLine
                Do While (Not line_text Is Nothing)
                    list_text.Add(line_text)
                    If line_text.Contains("FormID") Or line_text.Contains("Form ID") Or line_text.Contains("FORMID") Then
                        If line_text.Contains("=") Then
                            Dim values() As String = line_text.Split(CChar("=")).ToArray
                            Dim values1() As String = values(1).Split(UCase(CChar("USING"))).ToArray
                            f.FormId = values1(0)
                        ElseIf line_text.Contains(":") Then
                            Dim values() As String = line_text.Split(CChar(":")).ToArray
                            Dim values1() As String = values(1).Split(UCase(CChar("USING"))).ToArray
                            f.FormId = values1(0)
                        End If
                        f.FormId = RemoveDoubleQuotes(f.FormId)
                    End If
                    If line_text.Contains("offset") Or line_text.Contains("Offset") Or line_text.Contains("Off-set") Then
                        If line_text.Contains("=") Then
                            Dim values1() As String = line_text.Split(CChar("=")).ToArray
                            Dim values3() As String = values1(1).Split(CChar("(")).ToArray
                            f.Offset = values3(0)
                        ElseIf line_text.Contains(":") Then
                            Dim values1() As String = line_text.Split(CChar(":")).ToArray
                            Dim values3() As String = values1(1).Split(CChar("(")).ToArray
                            f.Offset = values3(0)
                        End If
                    End If
                    If line_text.Contains("formname") Or line_text.Contains("FormName") Or line_text.Contains("FORMTITLE") Then
                        If line_text.Contains("=") Then
                            Dim values2() As String = line_text.Split(CChar("=")).ToArray
                            f.Description = values2(1)
                        ElseIf line_text.Contains(":") Then
                            Dim values2() As String = line_text.Split(CChar(":")).ToArray
                            f.Description = values2(1)
                        End If
                        f.Description = RemoveDoubleQuotes(f.Description)
                        Exit Do
                    End If
                    line_text = r.ReadLine
                Loop
                f.Update()
                f.ProgramName = String.Empty
                f.FormId = String.Empty
                f.Offset = String.Empty
                f.Description = String.Empty
                f.File_Path = String.Empty
                f.Date_Modified = Nothing
            End Using
        Next
    End Sub
End Class