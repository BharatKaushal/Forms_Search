﻿
Public Class FormDataInfoList
    Inherits List(Of FormDataInfoList.listItem)

    Public Shared Function Fetch(crit As Criteria) As FormDataInfoList
        Dim FormInfoList As New FormDataInfoList
        Dim Query As New Text.StringBuilder
        Dim ProgramName As String = crit.ProgramName
        Dim FormId As String = crit.FormId

        Query.AppendLine("SELECT * FROM FORMS_INFO ")
        Query.AppendLine("WHERE 1=1 ")
        If Not String.IsNullOrWhiteSpace(ProgramName) AndAlso Not String.IsNullOrWhiteSpace(FormId) Then
            Query.AppendFormat("AND ProgramName = '{0} AND FormId = {1}'", ProgramName, FormId)
        ElseIf Not String.IsNullOrWhiteSpace(ProgramName) Then
            Query.AppendFormat("AND ProgramName = '{0}'", ProgramName)
        ElseIf Not String.IsNullOrWhiteSpace(FormId) Then
            Query.AppendFormat("AND FormId = '{0}'", FormId)
        End If
        If Not String.IsNullOrEmpty(Query.ToString) Then
            Dim dt = DataManager.ExecuteQuery(Query.ToString)
            For Each dr In dt.Item1.Tables(0).Rows 'dt.Item1.Tables.First.Rows
                'Dim itm As New FormDataInfoList.listItem(dr("FormId"), dr("ProgramName"), dr("Descriprion"), dr("Offset"), dr("Date_Modified"))
                Dim itm As New FormDataInfoList.listItem
                With itm
                    .FormId = dr("FormID")
                    .ProgramName = dr("ProgramName")
                    .Description = dr("Descriprion")
                    .Offset = dr("Offset")
                    .Date_Modified = dr("Date_Modified")
                End With
                FormInfoList.Add(itm)
            Next
        Else
            MessageBox.Show("Enter Program Name or Form ID to Search the Database!!")
        End If
        Return FormInfoList
    End Function

    Public Class Criteria
        Public Property ProgramName As String
        Public Property FormId As String
        Public Property Description As String
        Public Property LastUpdated As DateTime?
        Public Property CreatedDate As Nullable(Of DateTime)
    End Class

    Public Class listItem
        Public Property FormId As String
        Public Property ProgramName As String
        Public Property Description As String
        Public Property Offset As String
        Public Property Date_Modified As DateTime
    End Class
End Class