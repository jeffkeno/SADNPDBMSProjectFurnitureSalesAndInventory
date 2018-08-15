Imports System.Data.OleDb
Public Class Form3
    Dim sql As String = ""
    Dim formatd As String = ""
    Dim dateform As Date = DateValue(Now)
    Dim stock As Integer
    Dim Crit As Integer
    Dim quant As Integer
    Dim quant2 As Integer
    Dim quantDB As Integer
    Dim stockDB As Integer
    Dim aa As Integer = 0
    Dim a As Integer = 0
    Dim b As Integer = 0
    Dim c As Integer = 0
    Dim d As Integer = 0
    Sub fill()
        sql = "select * from Products"
        FillListView(ExecuteQuery("select * from Products where Quantity >= CriticalLevel"), ListView2)
    End Sub

    Private Sub Form3_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fill()
        ListView1.Columns.Add("Product Name", 250, HorizontalAlignment.Left)
        ListView1.Columns.Add("Quantity Stock", 100, HorizontalAlignment.Right)
        ListView1.Columns.Add("Quantity Ordered", 100, HorizontalAlignment.Right)
        ListView1.Columns.Add("Price", 90, HorizontalAlignment.Right)
        ListView1.Columns.Add("Total", 90, HorizontalAlignment.Right)

        dateform = DateTimePicker1.Value.Date
        formatd = Format(dateform, "ddMMMyyyy")
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        txtCash.Text = "0.00"
        txtChange.Text = "0.00"
        txtTotal.Text = "0.00"
        ListView1.Items.Clear()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim c = ListView1.SelectedItems(0).SubItems(3).Text
        Dim d = Val(txtTotal.Text)
        d -= Val(c)
        'txtTotal.Text = Format(d, "0.00")
        txtTotal.Text = "0.00"
        For Each lvItem As ListViewItem In ListView1.SelectedItems
            lvItem.Remove()
        Next
    End Sub

    Private Sub btnAddToCart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddToCart.Click
        Dim a = InputBox("How many?")
        If a = "" Then
            MsgBox("no input taken")
        Else
            If a > Val(ListView2.SelectedItems(0).SubItems(9).Text()) Then
                MsgBox("Input Quantity is greater than Quantity in stock")
            Else
                Dim aa = ListView2.SelectedItems(0).SubItems(9).Text()
                Dim b = ListView2.SelectedItems(0).SubItems(6).Text
                Dim c = a * b
                For Each lvItem As ListViewItem In ListView2.SelectedItems
                    Dim itemx As New ListViewItem(ListView2.SelectedItems(0).SubItems(1).Text, 0)
                    itemx.Checked = True
                    itemx.SubItems.Add(aa)
                    itemx.SubItems.Add(a)
                    itemx.SubItems.Add(b)
                    itemx.SubItems.Add(c)
                    ListView1.Items.AddRange(New ListViewItem() {itemx})
                Next
                Dim d = Val(txtTotal.Text)
                d += Val(c)
                txtTotal.Text = Format(d, "0.00")
            End If
        End If
    End Sub

    Private Sub txtPNSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPNSearch.TextChanged
        If txtPNSearch.Text = "" Then
            MsgBox("textbox for Search is Empty")
            fill()
        Else
            sql = "select * from Products where Product_Name like '%" & txtPNSearch.Text & "%'"
            FillListView(ExecuteQuery(sql), ListView2)
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        txtCode.Text = ListView2.SelectedItems(0).Text
        txtName.Text = ListView2.SelectedItems(0).SubItems(1).Text
        txtDecs.Text = ListView2.SelectedItems(0).SubItems(2).Text
        txtSupplier.Text = ListView2.SelectedItems(0).SubItems(4).Text
        txtOPrice.Text = ListView2.SelectedItems(0).SubItems(5).Text
        txtRPrice.Text = ListView2.SelectedItems(0).SubItems(6).Text
        txtStock.Text = ListView2.SelectedItems(0).SubItems(7).Text
        txtCritical.Text = ListView2.SelectedItems(0).SubItems(8).Text
        txtQty.Text = ListView2.SelectedItems(0).SubItems(9).Text
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'stock = ExecuteQuery("select StockLevel from Products where Product_Code='" & ListView2.SelectedItems(0).Text & "'").Rows(0)(0)
        'Crit = ExecuteQuery("select CriticalLevel from Products where Product_Code='" & ListView2.SelectedItems(0).Text & "'").Rows(0)(0)
        'MsgBox(stock & " " & Crit)
        If ListView1.Items.Count = 0 Then
            MsgBox("There are no items in the Cart yet")
        ElseIf txtCash.Text = "0.00" Then
            MsgBox("No Payment yet")
            txtCash.Focus()
        ElseIf Val(txtCash.Text) < Val(txtTotal.Text) Then
            MsgBox("Insufficient Payment")
            txtCash.Focus()
        Else

            Dim lvi As ListViewItem
            Dim a = 0
            Dim b = 0
            Dim c = 0
            Dim d = 0
            For Each lvi In ListView1.Items
                MessageBox.Show(ListView1.Items(a).SubItems(1).Text() & "-" & ListView1.Items(a).SubItems(2).Text())
                c = Val(ListView1.Items(a).SubItems(1).Text()) - Val(ListView1.Items(a).SubItems(2).Text())
                ExecuteQuery("update Products set Quantity=" & c & " where Product_Name='" & ListView1.Items(a).SubItems(0).Text() & "'")
                fill()
                a += 1
                b = 0
            Next

            Dim IDn = ""
            Dim IDm = ""
            Dim rand As New Random
            Dim fivenumbers As Integer = rand.Next(0, 99999)
            IDn = fivenumbers & "-" & UCase(formatd)
            IDm = "2-" & IDn
            ExecuteQuery("insert into Sales values('" & IDn & "', '" & UCase(formatd) & "', '" & IDm & "', '" & txtCash.Text & "', '" & txtChange.Text & "', '" & txtTotal.Text & "')")
            d = 0
            For Each lvi In ListView1.Items
                ExecuteQuery("insert into Orders values('" & IDm & "', '" & ListView1.Items(d).SubItems(0).Text() & "', '" & ListView1.Items(d).SubItems(2).Text() & "', '" & ListView1.Items(d).SubItems(4).Text() & "')")
                d += 1
            Next
            'cleared ungas
            txtCash.Text = "0.00"
            txtChange.Text = "0.00"
            txtTotal.Text = "0.00"
            ListView1.Items.Clear()
        End If
    End Sub

    Private Sub txtCash_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCash.TextChanged
        txtChange.Text = Val(txtCash.Text) - Val(txtTotal.Text)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class