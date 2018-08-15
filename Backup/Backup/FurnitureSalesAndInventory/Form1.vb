Imports System.Data.OleDb
Public Class Form1
    Dim sql As String = ""
    Dim formatd As String = ""
    Dim dateform As Date = DateValue(Now)
    Sub fill()
        sql = "select * from Products"
        FillListView(ExecuteQuery(sql), ListView1)
        FillListView(ExecuteQuery("select * from Products where Quantity <= CriticalLevel"), ListView2)
    End Sub
    Sub generateCode()
        Dim rand As New Random
        Dim fivenumbers As Integer = rand.Next(0, 99999)
        Dim letters As String = UCase(Mid(txtDecs.Text, 1, 3))
        txtCode.Text = letters & "-" & fivenumbers & "-" & UCase(formatd)
    End Sub
    Sub clear()
        txtCode.Text = ""
        txtName.Text = ""
        txtDecs.Text = ""
        txtOPrice.Text = "0"
        txtSupplier.Text = ""
        txtRPrice.Text = "0"
        txtStock.Text = "0"
        txtCritical.Text = "0"
        txtQty.Text = "0"
    End Sub
    Sub disebol()
        txtCode.Enabled = False
        txtName.Enabled = False
        txtDecs.Enabled = False
        txtOPrice.Enabled = False
        txtSupplier.Enabled = False
        txtRPrice.Enabled = False
        txtStock.Enabled = False
        txtCritical.Enabled = False
        txtQty.Enabled = False
    End Sub
    Sub enebol()
        txtName.Enabled = True
        txtDecs.Enabled = True
        txtOPrice.Enabled = True
        txtSupplier.Enabled = True
        txtRPrice.Enabled = True
        txtStock.Enabled = True
        txtCritical.Enabled = True
        txtQty.Enabled = True
    End Sub
    Sub enebol2()
        txtQty.Enabled = True
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fill()
        DateTimePicker1.Enabled = False : txtCode.Enabled = False : btnEdit.Enabled = False : btnDelete.Enabled = False
        dateform = DateTimePicker1.Value.Date
        formatd = Format(dateform, "ddMMMyyyy")
        disebol()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        enebol()
        If txtDecs.Text = "" Then
            MsgBox("textbox for Product Name is Empty")
            fill()
            txtName.Focus()
        Else
            generateCode()
            ExecuteQuery("insert into Products values('" & txtCode.Text & "', '" & UCase(txtName.Text) & "', '" & UCase(txtDecs.Text) & "', '" & DateTimePicker1.Value.Date & "', '" & UCase(txtSupplier.Text) & "', '" & txtOPrice.Text & "', '" & txtRPrice.Text & "', " & txtStock.Text & ", " & txtCritical.Text & ", " & txtQty.Text & ")")
            fill()
            clear()
            disebol()
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count = 1 Then
            btnEdit.Enabled = True : btnDelete.Enabled = True
            btnUptdQty.Enabled = False
        Else
            btnEdit.Enabled = False : btnDelete.Enabled = False
            btnEdit.Text = "Edit"
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If MessageBox.Show("you sure you want to delete?", "Delete", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            ExecuteQuery("delete from Products where Product_Code='" & ListView1.SelectedItems(0).Text & "'")
            fill()
        Else
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If txtsearch.Text = "" Then
            MsgBox("textbox for Search is Empty")
            fill()
        ElseIf ComboBox1.SelectedItem = "" Then
            MsgBox("combobox for Search is Empty")
            fill()
        Else
            sql = "select * from Products where " & ComboBox1.SelectedItem & " like '%" & txtsearch.Text & "%'"
            FillListView(ExecuteQuery(sql), ListView1)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Form3.Show()
        Me.Hide()
    End Sub

    Private Sub txtStock_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStock.TextChanged
        'txtCritical.Text = Val(txtStock.Text) * 0.2
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        fill()
        DateTimePicker1.Enabled = False : txtCode.Enabled = False : btnEdit.Enabled = False : btnDelete.Enabled = False
        dateform = DateTimePicker1.Value.Date
        formatd = Format(dateform, "ddMMMyyyy")
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        enebol()
        If btnEdit.Text = "Edit" Then
            txtCode.Text = ListView1.SelectedItems(0).Text
            txtName.Text = ListView1.SelectedItems(0).SubItems(1).Text
            txtDecs.Text = ListView1.SelectedItems(0).SubItems(2).Text
            txtSupplier.Text = ListView1.SelectedItems(0).SubItems(4).Text
            txtOPrice.Text = ListView1.SelectedItems(0).SubItems(5).Text
            txtRPrice.Text = ListView1.SelectedItems(0).SubItems(6).Text
            txtStock.Text = ListView1.SelectedItems(0).SubItems(7).Text
            txtCritical.Text = ListView1.SelectedItems(0).SubItems(8).Text
            txtQty.Text = ListView1.SelectedItems(0).SubItems(9).Text
            btnEdit.Text = "Save"
            btnUptdQty.Text = "Update Quantity"
        Else
            ExecuteQuery("update Products set Product_Name='" & txtName.Text & "', Product_Description='" & txtDecs.Text & "', Product_Supplier='" & txtSupplier.Text & "', Original_Price=" & txtOPrice.Text & ", Retail_Price=" & txtRPrice.Text & ", StockLevel=" & txtStock.Text & ", CriticalLevel=" & txtCritical.Text & ", Quantity=" & txtQty.Text & " where Product_Code='" & txtCode.Text & "'")
            fill()
            clear()
            btnEdit.Text = "Edit"
        End If
    End Sub

    Private Sub ListView2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView2.SelectedIndexChanged
        If ListView2.SelectedItems.Count = 1 Then
            If ListView2.SelectedItems.Count = 1 Then
                btnUptdQty.Enabled = True
                btnEdit.Enabled = False
            Else
                btnUptdQty.Enabled = False
                btnUptdQty.Text = "Update Quantity"
            End If
        End If
    End Sub

    Private Sub btnUptdQty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUptdQty.Click
        enebol2()
        If ListView2.SelectedItems.Count = 1 Then
            If btnUptdQty.Text = "Update Quantity" Then
                txtCode.Text = ListView2.SelectedItems(0).Text
                txtName.Text = ListView2.SelectedItems(0).SubItems(1).Text
                txtDecs.Text = ListView2.SelectedItems(0).SubItems(2).Text
                txtSupplier.Text = ListView2.SelectedItems(0).SubItems(4).Text
                txtOPrice.Text = ListView2.SelectedItems(0).SubItems(5).Text
                txtRPrice.Text = ListView2.SelectedItems(0).SubItems(6).Text
                txtStock.Text = ListView2.SelectedItems(0).SubItems(7).Text
                txtCritical.Text = ListView2.SelectedItems(0).SubItems(8).Text
                txtQty.Text = ListView2.SelectedItems(0).SubItems(9).Text
                btnUptdQty.Text = "Save"
                btnEdit.Text = "Edit"
                txtQty.Focus()
            Else
                ExecuteQuery("update Products set Product_Name='" & txtName.Text & "', Product_Description='" & txtDecs.Text & "', Product_Supplier='" & txtSupplier.Text & "', Original_Price=" & txtOPrice.Text & ", Retail_Price=" & txtRPrice.Text & ", StockLevel=" & txtStock.Text & ", CriticalLevel=" & txtCritical.Text & ", Quantity=" & txtQty.Text & " where Product_Code='" & txtCode.Text & "'")
                fill()
                clear()
                disebol()
                btnUptdQty.Text = "Update Quantity"
            End If
        Else
            MsgBox("Please Select an item from the Critical Items List First")
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        SplashScreen1.Show()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Hide()
        Form2.Show()
    End Sub

    Private Sub txtLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLogout.Click
        If (MessageBox.Show("Are you sure?", "Confirmation Message!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes) Then
            Application.Exit()
        End If
    End Sub
End Class
