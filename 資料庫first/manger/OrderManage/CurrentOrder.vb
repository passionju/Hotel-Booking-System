Imports System.Data.OleDb
Imports System.Reflection.Emit

Public Class CurrentOrder
    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    Dim CO As New ChangeOrder()
    Dim dr As OleDbDataReader
    Dim totalmoney As String = "0"
    Dim result As Object
    Private Sub viewer()
        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()

        conn.Open()
        cmd = conn.CreateCommand()
        cmd.CommandType = CommandType.Text
        da = New OleDbDataAdapter("select * from 訂單", conn)
        da.Fill(dt)
        DataGridView1.DataSource = dt
        conn.Close()
        DataGridView1.Columns(0).Width = 80
        DataGridView1.Columns(1).Width = 80
        DataGridView1.Columns(2).Width = 80
    End Sub
    Private Sub Form12_1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb"
        cmd = conn.CreateCommand()
        da = New OleDbDataAdapter(cmd)

        Button1.Enabled = False
        viewer()

        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("確定")
        Button1.BackColor = Color.Transparent
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0

        Button4.BackgroundImage = My.Resources.ResourceManager.GetObject("返回")
        Button4.BackColor = Color.Transparent
        Button4.BackgroundImageLayout = ImageLayout.Stretch
        Button4.FlatStyle = FlatStyle.Flat
        Button4.FlatAppearance.BorderSize = 0
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim f9_1 As New FuntionForm

        f9_1.Show()

        Me.Hide()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CO.Show()
        Me.Hide()
    End Sub



    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        Dim selectedGR As DataGridViewRow

        Try
            selectedGR = DataGridView1.Rows(e.RowIndex)
            Button1.Enabled = True
            CO.Label12.Text = selectedGR.Cells(0).Value.ToString()
            CO.TextBox1.Text = selectedGR.Cells(7).Value.ToString()
            CO.TextBox4.Text = selectedGR.Cells(8).Value.ToString()
            CO.TextBox3.Text = selectedGR.Cells(4).Value.ToString()
            If selectedGR.Cells(9).Value.ToString() = CO.RadioButton1.Text Then
                CO.RadioButton1.Checked = True
            Else
                CO.RadioButton2.Checked = True
            End If
            If selectedGR.Cells(5).Value.ToString() = CO.RadioButton3.Text Then
                CO.RadioButton3.Checked = True
            Else
                CO.RadioButton4.Checked = True
            End If
            CO.DateTimePicker1.Value = selectedGR.Cells(1).Value
            CO.DateTimePicker2.Value = selectedGR.Cells(2).Value
        Catch ex As Exception
            MsgBox("請點選正確的房間")
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try

            dt = New DataTable()
            da = New OleDbDataAdapter("Select * FROM 訂單 WHERE 
    (入住日期 <='" + DateTimePicker2.Value.ToString("yyyy-MM-dd") + "' AND 退房日期>='" + DateTimePicker1.Value.ToString("yyyy-MM-dd") + "') or(入住日期 <='" +'偏左
            DateTimePicker1.Value.ToString("yyyy-MM-dd") + "'AND 退房日期>= '" + DateTimePicker2.Value.ToString("yyyy-MM-dd") + "') ", conn)
            da.Fill(dt)
            DataGridView1.DataSource = dt


            conn.Open()
            Dim totalAmount As Decimal = 0
            Dim query As String = "SELECT SUM(總金額) FROM 訂單 where 付款狀態 = '已付款' and ((入住日期 <='" + DateTimePicker2.Value.ToString("yyyy-MM-dd") + "' AND 退房日期>='" + DateTimePicker1.Value.ToString("yyyy-MM-dd") + "') or(入住日期 <='" +'偏左
            DateTimePicker1.Value.ToString("yyyy-MM-dd") + "'AND 退房日期>= '" + DateTimePicker2.Value.ToString("yyyy-MM-dd") + "') )"
            Using cmd As New OleDbCommand(query, conn)
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot DBNull.Value Then
                    totalAmount = Convert.ToDecimal(result)
                End If
            End Using
            Label2.Text = totalAmount.ToString

            conn.Close()
        Catch ex As Exception
            MsgBox("請點選正確的房間")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            dt = New DataTable()
            da = New OleDbDataAdapter("select * from 訂單 ", conn)
            da.Fill(dt)
            DataGridView1.DataSource = dt

            Dim totalAmount As Decimal = 0
            Dim query As String = "SELECT SUM(總金額) FROM 訂單 where 付款狀態 = '已付款'"
            Using cmd As New OleDbCommand(query, conn)
                Dim result As Object = cmd.ExecuteScalar()
                If result IsNot DBNull.Value Then
                    totalAmount = Convert.ToDecimal(result)
                End If
            End Using
            Label2.Text = totalAmount.ToString
            conn.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Dim startDate As DateTime = DateTimePicker1.Value
        Dim endDate As DateTime = DateTimePicker2.Value
        Dim days As Integer = CInt((endDate - startDate).TotalDays)
        Try
            If days < 0 Then
                DateTimePicker2.Value = DateTimePicker1.Value

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        Dim startDate As DateTime = DateTimePicker1.Value
        Dim endDate As DateTime = DateTimePicker2.Value
        Dim days As Integer = CInt((endDate - startDate).TotalDays)
        Try
            If days < 0 Then
                DateTimePicker2.Value = DateTimePicker1.Value
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
End Class