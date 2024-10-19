Imports System.Data.OleDb
Imports System.IO
Imports System.Reflection.Emit

Public Class roominfo6666
    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    Dim dr As OleDbDataReader
    Dim a As Integer = 0
    Dim index As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Me.Hide()
    End Sub

    Private Sub roominfo6666_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb" '目前只能更改總統套房
        conn.Open()
        cmd = conn.CreateCommand()
        cmd.CommandType = CommandType.Text
        Dim mycommand As New OleDbCommand("select 簡介 from 房間 where 房間編號= '" + Label2.Text + "'", conn)
        dr = mycommand.ExecuteReader()

        While dr.Read()
            Label1.Text = dr.GetString(0)
        End While
        dr.Close()
        conn.Close()

        Try
            a = 1
            conn.Open()

            cmd.CommandText = "SELECT 圖片資料 FROM 照片 WHERE 圖片編號 = @圖片編號" ' 要哪張圖片
            cmd.Parameters.AddWithValue("@圖片編號", "1") ' 將數字轉換為文字
            Dim imageData As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())

            cmd.CommandText = "SELECT 圖片資料 FROM 照片 WHERE 圖片編號 = @圖片編號" ' 要哪張圖片
            cmd.Parameters.AddWithValue("@圖片編號", "2") ' 將數字轉換為文字
            Dim imageData1 As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())

            cmd.CommandText = "SELECT 圖片資料 FROM 照片 WHERE 圖片編號 = @圖片編號"
            cmd.Parameters.AddWithValue("@圖片編號", "3") ' 將數字轉換為文字
            Dim imageData2 As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())

            If imageData IsNot Nothing Then
                Using ms As New MemoryStream(imageData)
                    Dim image1 As Image = Image.FromStream(ms)
                    ' 在這裡可以使用圖片物件進行顯示或其他操作
                    PictureBox1.Image = image1
                End Using
            End If
        Catch ex As Exception
            MsgBox("圖片相關有問題")
        End Try
        ' If imageData1 IsNot Nothing Then
        'Using ms2 As New MemoryStream(imageData1)
        ' Dim image2 As Image = Image.FromStream(ms2)
        ' 在這裡可以使用圖片物件進行顯示或其他操作
        '  PictureBox2.Image = image2
        ' End Using
        ' End If

        '  If imageData2 IsNot Nothing Then
        'Using ms3 As New MemoryStream(imageData2)
        ' Dim image3 As Image = Image.FromStream(ms3)
        ' 在這裡可以使用圖片物件進行顯示或其他操作
        'PictureBox3.Image = image3
        'End Using
        'End If

        conn.Close()
        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("返回")
        Button1.BackColor = Color.Transparent
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0
        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("返回")
        Button1.BackColor = Color.Transparent
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            conn.Open()
            Dim imageData As Byte() = DirectCast(cmd.ExecuteScalar(), Byte())
            cmd.CommandText = "SELECT 圖片資料 FROM 照片 WHERE 圖片編號 = @圖片編號"
            cmd.Parameters.AddWithValue("@圖片編號", "3") ' 將數字轉換為文字
            If imageData IsNot Nothing Then
                Using ms As New MemoryStream(imageData)
                    Dim image2 As Image = Image.FromStream(ms)
                    ' 在這裡可以使用圖片物件進行顯示或其他操作
                    PictureBox2.Image = image2
                End Using
            End If
            conn.Close()
        Catch ex As Exception
            MsgBox("圖片相關有問題")
        End Try
    End Sub
End Class