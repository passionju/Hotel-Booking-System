Imports System.Data.OleDb
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar

Public Class ChangeOrder
    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    Dim inroom As String
    Dim paymoney As String
    Dim date1, date2 As String



    Private Sub viewer()
        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb"
        Dim changeordercommand As String = "SELECT * FROM 房間 WHERE (房間編號 NOT IN(SELECT 房間編號 FROM 訂單 WHERE 
(入住日期 <='" + DateTimePicker2.Value.ToString("yyyy-MM-dd") + "' AND 退房日期>='" + DateTimePicker1.Value.ToString("yyyy-MM-dd") + "') or(入住日期 <='" +'偏左
        DateTimePicker1.Value.ToString("yyyy-MM-dd") + "'AND 退房日期>= '" + DateTimePicker2.Value.ToString("yyyy-MM-dd") + "')))  "

        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()

        conn.Open()
        cmd = conn.CreateCommand()
        cmd.CommandType = CommandType.Text

        da = New OleDbDataAdapter(changeordercommand, conn)
        da.Fill(dt)
        DataGridView1.DataSource = dt
        conn.Close()
        DataGridView1.Columns(0).Width = 80
        DataGridView1.Columns(1).Width = 80
        DataGridView1.Columns(2).Width = 80

    End Sub
    Private Sub viewer2()

        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()

        conn.Open()
        cmd = conn.CreateCommand()
        cmd.CommandType = CommandType.Text
        da = New OleDbDataAdapter("select * from 房間", conn)
        da.Fill(dt)
        DataGridView1.DataSource = dt

        dt = New DataTable()
        da = New OleDbDataAdapter("select * from 房間", conn)
        da.Fill(dt)
        DataGridView1.DataSource = dt
        conn.Close()
        DataGridView1.Columns(0).Width = 80
        DataGridView1.Columns(1).Width = 80
        DataGridView1.Columns(2).Width = 80
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim f12_1 As New CurrentOrder
        f12_1.Show()
        Me.Hide()
    End Sub

    Private Sub Form12_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb"

        viewer2()


        DataGridView1.Columns(0).Width = 80
        DataGridView1.Columns(1).Width = 80
        DataGridView1.Columns(2).Width = 80
        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("確定")
        Button1.BackColor = Color.Transparent
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0

        Button2.BackgroundImage = My.Resources.ResourceManager.GetObject("取消訂單")
        Button2.BackColor = Color.Transparent
        Button2.BackgroundImageLayout = ImageLayout.Stretch
        Button2.FlatStyle = FlatStyle.Flat
        Button2.FlatAppearance.BorderSize = 0

        Button4.BackgroundImage = My.Resources.ResourceManager.GetObject("返回")
        Button4.BackColor = Color.Transparent
        Button4.BackgroundImageLayout = ImageLayout.Stretch
        Button4.FlatStyle = FlatStyle.Flat
        Button4.FlatAppearance.BorderSize = 0

    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim selectedGR As DataGridViewRow
        Try
            selectedGR = DataGridView1.Rows(e.RowIndex)
            TextBox4.Text = selectedGR.Cells(0).Value.ToString()
        Catch ex As Exception

        End Try

    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "delete * from 訂單 where 訂單編號 = '" + Label12.Text + "' "
            cmd.ExecuteNonQuery()


            da = New OleDbDataAdapter("select * from 房間 ", conn)
            da.Fill(dt)
            DataGridView1.DataSource = dt
            conn.Close()
            MessageBox.Show("刪除成功", "刪除訂單資料", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'viewer()
            Dim co As New CurrentOrder()
            co.Show()
            Me.Hide()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim c As Integer = 0
        Dim tproom As String
        Try
            date1 = DateTimePicker1.Value.ToString("yyyy-MM-dd")
            date2 = DateTimePicker2.Value.ToString("yyyy-MM-dd")
            If RadioButton1.Checked Then
                inroom = RadioButton1.Text
            ElseIf RadioButton2.Checked Then
                inroom = RadioButton2.Text
            End If
            If RadioButton3.Checked Then
                paymoney = RadioButton3.Text
            ElseIf RadioButton4.Checked Then
                paymoney = RadioButton4.Text
            End If
            c = 0
            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select 房型 from 房間 where 房間編號='" + TextBox4.Text + "'"
            tproom = cmd.ExecuteScalar()
            If tproom = "單人房" Then
                If TextBox1.Text <= 1 Then
                    c = 1
                Else
                    MsgBox("無法填入大於所訂購房型的人數")
                    conn.Close()
                    Exit Sub
                End If
            ElseIf tproom = "雙人房" Then
                If TextBox1.Text <= 2 Then
                    c = 1
                Else
                    MsgBox("無法填入大於所訂購房型的人數")
                    conn.Close()
                    Exit Sub
                End If
            ElseIf tproom = "四人房" Then
                If TextBox1.Text <= 4 Then
                    c = 1
                Else
                    MsgBox("無法填入大於所訂購房型的人數")
                    conn.Close()
                    Exit Sub
                End If
            ElseIf tproom = "總統套房" Then
                If TextBox1.Text <= 5 Then
                    c = 1
                Else
                    MsgBox("無法填入大於所訂購房型的人數")
                    conn.Close()
                    Exit Sub
                End If
            End If
            If c = 1 Then
                cmd.CommandText = "UPDATE 訂單 SET 入住人數 = '" + TextBox1.Text + "', 總金額 = '" + TextBox3.Text + "', 付款狀態 = '" +
                 paymoney + "', 房間編號 = '" + TextBox4.Text + "', 入住狀態 = '" + inroom + "',入住日期 = '" + date1 + "',退房日期='" + date2 +
                "'WHERE 訂單編號 = '" + Label12.Text + "'"

                cmd.ExecuteNonQuery()
                conn.Close()
                MessageBox.Show("修改成功", "修改訂單資料", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("資料請輸入正確")
        End Try
    End Sub


    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb"
        Try

            Dim changeordercommand As String = "SELECT * FROM 房間 WHERE (房間編號 NOT IN(SELECT 房間編號 FROM 訂單 WHERE 
(入住日期 <='" + DateTimePicker2.Value.ToString("yyyy-MM-dd") + "' AND 退房日期>='" + DateTimePicker1.Value.ToString("yyyy-MM-dd") + "') or(入住日期 <='" +'偏左
        DateTimePicker1.Value.ToString("yyyy-MM-dd") + "'AND 退房日期>= '" + DateTimePicker2.Value.ToString("yyyy-MM-dd") + "')))  "

            Dim startDate As DateTime = DateTimePicker1.Value
            Dim endDate As DateTime = DateTimePicker2.Value
            Dim days As Integer = CInt((endDate - startDate).TotalDays)

            If days < 0 Then
                DateTimePicker2.Value = DateTimePicker1.Value

            End If
            conn.Open()
            dt = New DataTable()
            da = New OleDbDataAdapter(changeordercommand, conn)
            da.Fill(dt)
            DataGridView1.DataSource = dt
            conn.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb"

        Try
            Dim changeordercommand As String = "SELECT * FROM 房間 WHERE (房間編號 NOT IN(SELECT 房間編號 FROM 訂單 WHERE 
(入住日期 <='" + DateTimePicker2.Value.ToString("yyyy-MM-dd") + "' AND 退房日期>='" + DateTimePicker1.Value.ToString("yyyy-MM-dd") + "') or(入住日期 <='" +'偏左
        DateTimePicker1.Value.ToString("yyyy-MM-dd") + "'AND 退房日期>= '" + DateTimePicker2.Value.ToString("yyyy-MM-dd") + "')))  "

            Dim startDate As DateTime = DateTimePicker1.Value
            Dim endDate As DateTime = DateTimePicker2.Value
            Dim days As Integer = CInt((endDate - startDate).TotalDays)

            If days < 0 Then
                DateTimePicker2.Value = DateTimePicker1.Value

            End If
            conn.Open()
            dt = New DataTable()
            da = New OleDbDataAdapter(changeordercommand, conn)
            da.Fill(dt)
            DataGridView1.DataSource = dt
            conn.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class