Imports System.Data.OleDb
Public Class Form2
    Dim sql As String = ""
    Dim formatd As String = ""
    Dim dateform As Date = DateValue(Now)
    Sub fill()
        sql = "select * from Sales"
        FillListView(ExecuteQuery(sql), ListView1)
    End Sub
    Sub fill2()
        sql = "select * from Orders where Order_ID='" & ListView1.SelectedItems(0).SubItems(2).Text() & "'"
        FillListView(ExecuteQuery(sql), ListView2)
    End Sub
    Private Sub Form2_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Form1.Show()
    End Sub

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fill()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        fill2()
    End Sub

    Private Sub btnLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Me.Close()
    End Sub
End Class