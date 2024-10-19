Imports System.Data.OleDb
Imports System.IO
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar

Public Class RoomChange
    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    Public roomnew As RoomInfo1
    'Public f1 As New StartForm
    Dim checklocation As String
    Private Sub Form11_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb"
        viewer()

        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("刪除房間")
        Button1.BackColor = Color.Transparent
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0

        Button4.BackgroundImage = My.Resources.ResourceManager.GetObject("返回")
        Button4.BackColor = Color.Transparent
        Button4.BackgroundImageLayout = ImageLayout.Stretch
        Button4.FlatStyle = FlatStyle.Flat
        Button4.FlatAppearance.BorderSize = 0

        Button5.BackgroundImage = My.Resources.ResourceManager.GetObject("修改房間")
        Button5.BackColor = Color.Transparent
        Button5.BackgroundImageLayout = ImageLayout.Stretch
        Button5.FlatStyle = FlatStyle.Flat
        Button5.FlatAppearance.BorderSize = 0
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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim f9_1 As New FuntionForm
        f9_1.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "delete * from  房間 where 房間編號 = '" + Label4.Text + "'"
            cmd.ExecuteNonQuery()
            dt = New DataTable()
            da = New OleDbDataAdapter("select * from 房間 ", conn)
            da.Fill(dt)
            DataGridView1.DataSource = dt
            conn.Close()


            MessageBox.Show("刪除成功", "刪除房間", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim selectedGR As DataGridViewRow
        Try
            selectedGR = DataGridView1.Rows(e.RowIndex)

            Label4.Text = selectedGR.Cells(0).Value.ToString()
            ComboBox1.Text = selectedGR.Cells(1).Value.ToString()
            TextBox4.Text = selectedGR.Cells(2).Value.ToString()
        Catch ex As Exception

        End Try

    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "update 房間 set  房型='" + ComboBox1.Text + "', 價格='" + TextBox4.Text + "', 簡介='" + TextBox1.Text + "'where 房間編號 = '" + Label4.Text + "'"
            cmd.ExecuteNonQuery()
            dt = New DataTable()
            da = New OleDbDataAdapter("select * from 房間 ", conn)
            da.Fill(dt)
            DataGridView1.DataSource = dt
            conn.Close()
            'roomnew.PictureBox1.Image = PictureBox1.Image
            ' f1.PictureBox1.Image = PictureBox1.Image
            ' PictureBox2.Image = PictureBox1.Image
            MessageBox.Show("修改房間成功", "修改房間", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim conn As New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb") '目前只能更改總統套房
        Try
            If ComboBox1.Text = "單人房" Then
                checklocation = 1
            ElseIf ComboBox1.Text = "雙人房" Then
                checklocation = 2
            ElseIf ComboBox1.Text = "四人房" Then
                checklocation = 3
            ElseIf ComboBox1.Text = "總統套房" Then
                checklocation = 4
            End If
            checklocation = 2
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
                PictureBox1.Load(OpenFileDialog1.FileName)

                Dim cmd As New OleDbCommand()
                cmd.Connection = conn
                conn.Open()

                Dim imagePath As String = OpenFileDialog1.FileName ' 圖片路徑
                Dim imageData As Byte() = File.ReadAllBytes(imagePath) ' 將圖片位元組資料新增到資料庫

                cmd.CommandType = CommandType.Text

                ' 替換為您要更新的圖片編號

                cmd.CommandText = "UPDATE 照片 SET 圖片資料 = @圖片資料 WHERE 圖片編號 = '1'"
                ' 替換為您的圖片編號 ，圖片存在哪個欄位
                cmd.Parameters.AddWithValue("@圖片資料", imageData) ' 替換圖片資料
                cmd.ExecuteNonQuery() '執行
                conn.Close()

            End If
        Catch ex As Exception
            MsgBox("圖片這邊有問題喔")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Try
            If OpenFileDialog2.ShowDialog() = DialogResult.OK Then '目前只能更改總統套房
                PictureBox2.Load(OpenFileDialog2.FileName)

                Dim cmd As New OleDbCommand()
                cmd.Connection = conn
                conn.Open()

                Dim imagePath1 As String = OpenFileDialog2.FileName ' 圖片路徑
                Dim imageData1 As Byte() = File.ReadAllBytes(imagePath1) ' 將圖片位元組資料新增到資料庫

                cmd.CommandType = CommandType.Text

                ' 替換為您要更新的圖片編號

                cmd.CommandText = "UPDATE 照片 SET 圖片資料 = @圖片資料 WHERE 圖片編號 = '2'"
                ' 替換為您的圖片編號 ，圖片存在哪個欄位
                cmd.Parameters.AddWithValue("@圖片資料", imageData1) ' 替換圖片資料
                cmd.ExecuteNonQuery() '執行
                conn.Close()

            End If
        Catch ex As Exception
            MsgBox("圖片這邊有問題喔")
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            If OpenFileDialog1.ShowDialog() = DialogResult.OK Then '目前只能更改總統套房
                PictureBox3.Load(OpenFileDialog1.FileName)

                Dim cmd As New OleDbCommand()
                cmd.Connection = conn
                conn.Open()

                Dim imagePath As String = OpenFileDialog1.FileName ' 圖片路徑
                Dim imageData As Byte() = File.ReadAllBytes(imagePath) ' 將圖片位元組資料新增到資料庫

                cmd.CommandType = CommandType.Text

                ' 替換為您要更新的圖片編號

                cmd.CommandText = "UPDATE 照片 SET 圖片資料 = @圖片資料 WHERE 圖片編號 = '3'"
                ' 替換為您的圖片編號 ，圖片存在哪個欄位
                cmd.Parameters.AddWithValue("@圖片資料", imageData) ' 替換圖片資料
                cmd.ExecuteNonQuery() '執行
                conn.Close()

            End If
        Catch ex As Exception
            MsgBox("圖片這邊有問題喔")
        End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class