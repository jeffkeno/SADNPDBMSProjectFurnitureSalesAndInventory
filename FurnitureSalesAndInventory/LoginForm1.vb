Imports System
Imports System.Data
Imports System.Data.OleDb
Public Class LoginForm1
    Dim sql As String = ""
    Dim admin As Integer

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        sql = "select * from Administrator where UserName='" & txtUserName.Text & "' and Password='" & txtPassword.Text & "'"
        Try
            If sql = "UserName = '" & txtUserName.Text & "' and Password = '" & txtPassword.Text & "' and Type =1" Then
                admin = "1"
            ElseIf sql = "UserName = '" & txtUserName.Text & "' and Password = '" & txtPassword.Text & "' and Type =0" Then
                admin = "0"
            End If
            connection.Open()
            Dim cmd As OleDbCommand = New OleDbCommand(sql, connection)
            Dim rdr As OleDbDataReader = cmd.ExecuteReader
            If rdr.Read Then
                admin1()
                connection.Close()
                If admin = 1 Then
                    Form1.Show()
                Else
                    Form3.Show()
                End If
                Username = txtUserName.Text
                rdr.Close()
                Me.Hide()
            Else
                MsgBox("Unrecognized Account.")
                rdr.Close()
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.ToString)
        Finally
            connection.Close()
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Application.Exit()
    End Sub
    Public Sub admin1()
        Dim conn As OleDbConnection = New OleDbConnection(connString)
        conn.Open()
        Dim strsql As String
        strsql = "select Type from Administrator where UserName = '" & txtUserName.Text.Trim & "' and Password= '" & txtPassword.Text.Trim & "'"
        Dim sqlcmd As OleDbCommand = New OleDbCommand(strsql, conn)
        Dim obj As Object = sqlcmd.ExecuteScalar()
        Admin = IIf(IsDBNull(obj), "", obj.ToString())
        conn.Close()
    End Sub

    Private Sub LoginForm1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If MessageBox.Show("Close Furniture Inventory System?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then e.Cancel = True
    End Sub
End Class