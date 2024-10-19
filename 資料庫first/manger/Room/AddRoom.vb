
Imports System.Data.OleDb
Imports System.Security.Policy

Public Class AddRoom
    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    Private Sub Form9_1_1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb"
        viewer()

        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("新增房")
        Button1.BackColor = Color.Transparent
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0

        Button2.BackgroundImage = My.Resources.ResourceManager.GetObject("返回")
        Button2.BackColor = Color.Transparent
        Button2.BackgroundImageLayout = ImageLayout.Stretch
        Button2.FlatStyle = FlatStyle.Flat
        Button2.FlatAppearance.BorderSize = 0
    End Sub
    Private Sub viewer()

        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()

        conn.Open()
        cmd = conn.CreateCommand()
        cmd.CommandType = CommandType.Text
        da = New OleDbDataAdapter("select * from 房間", conn)
        da.Fill(dt)
        DataGridView1.DataSource = dt
        conn.Close()
        DataGridView1.Columns(0).Width = 80
        DataGridView1.Columns(1).Width = 80
        DataGridView1.Columns(2).Width = 80
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(ComboBox1.Text) Or String.IsNullOrWhiteSpace(TextBox3.Text) Then
                MessageBox.Show("請將資料輸入完整")
                Exit Sub
            End If

            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select 房間編號 from 房間 where 房間編號= '" + TextBox1.Text + "'"
            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing Then
                MessageBox.Show("該房間已經存在")
                conn.Close()
                Exit Sub
            End If
            cmd.CommandText = "insert into 房間(房間編號,房型,價格)values('" + TextBox1.Text +
                "', '" + ComboBox1.Text + "','" + TextBox3.Text + "')"
            cmd.ExecuteNonQuery()
            conn.Close()
            dt = New DataTable()
            da = New OleDbDataAdapter("select * from 房間 ", conn)
            da.Fill(dt)
            DataGridView1.DataSource = dt
            MessageBox.Show("新增房間成功", "新增房間", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim f9_1 As New FuntionForm

        FuntionForm.Show()

        Me.Hide()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        'Try
        'TextBox1.Text = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()
        '  TextBox2.Text = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()
        'TextBox3.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString()

        '   Catch ex As Exception
        ' MessageBox.Show("請點擊房間編號前方那列")
        '    End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class