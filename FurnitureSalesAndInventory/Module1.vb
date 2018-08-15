Imports System.Data.OleDb
Module Module1
    Public connString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Application.StartupPath & "\batt.mdb "
    Public conn As OleDbConnection = New OleDbConnection(connString)
    Public connection As OleDbConnection = New OleDbConnection(connString)
    Public Username As String = ""
    Public Sub FillListView(ByVal sqlData As DataTable, ByVal lvList As ListView)
        lvList.Items.Clear()
        lvList.Columns.Clear()
        Dim i As Integer
        Dim j As Integer
        ' for filling up column headers
        For i = 0 To sqlData.Columns.Count - 1
            lvList.Columns.Add(sqlData.Columns(i).ColumnName)
        Next i
        'for filling up all items in the table
        For i = 0 To sqlData.Rows.Count - 1
            lvList.Items.Add(sqlData.Rows(i).Item(0))
            For j = 1 To sqlData.Columns.Count - 1
                If Not IsDBNull(sqlData.Rows(i).Item(j)) Then
                    lvList.Items(i).SubItems.Add(sqlData.Rows(i).Item(j))
                Else
                    lvList.Items(i).SubItems.Add("")
                End If
            Next j
        Next i
        'for sizing column headers
        For i = 0 To sqlData.Columns.Count - 1
            lvList.Columns(i).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
        Next i
    End Sub
    Public Function ExecuteQuery(ByVal sqlQuery As String) As DataTable
        Dim sqlDT As New DataTable
        Try
            Dim sqlconn As New OleDbConnection(connString)
            Dim sqlDA As New OleDbDataAdapter(sqlQuery, sqlconn)
            Dim sqlCB As New OleDbCommandBuilder(sqlDA)
            sqlDA.Fill(sqlDT)
        Catch ex As Exception
            MsgBox("Error: " & ex.ToString)
        End Try
        Return sqlDT
    End Function
    'fills a combo box
    Public Sub fillComboBox(ByVal dt As DataTable, ByVal cb As ComboBox)
        cb.Items.Clear()
        For row As Integer = 0 To dt.Rows.Count - 1
            cb.Items.Add(dt.Rows(row)(0))
        Next
    End Sub
End Module
