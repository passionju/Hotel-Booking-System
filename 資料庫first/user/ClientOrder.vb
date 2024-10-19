Imports System.Data.OleDb
Imports FxResources.System
Imports System.Data.SqlTypes
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Net.Mail

Public Class ClientOrder
    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    Private bitmap As Bitmap
    Dim dr As OleDbDataReader
    Dim mail As New String("")
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Try
            If String.IsNullOrWhiteSpace(Label37.Text) Then
                MessageBox.Show("這邊沒有訂房紀錄")
                Exit Sub
            End If
            Dim result As DialogResult = MessageBox.Show("確定取消?", "選擇", MessageBoxButtons.YesNo)

            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "delete from 訂單 where 房間編號 = '" + Label37.Text + "'"


            If result = DialogResult.Yes Then
                cmd.ExecuteNonQuery()
                MessageBox.Show("已取消房間")
                'Me.Hide()
            End If
            conn.Close()

            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            Dim mycommand As New OleDbCommand("select 電子信箱 from 顧客 where 身分證字號= '" + loginid.Text + "'", conn)
            dr = mycommand.ExecuteReader()
            While dr.Read()
                mail = dr.GetString(0)
            End While

            Dim EmailMessage As New MailMessage()

            ' mail = loginid.Text

            EmailMessage.From = New MailAddress("peizhengn@gmail.com")
            EmailMessage.To.Add(mail)
            EmailMessage.Subject = "訂房取消通知"
            EmailMessage.Body = "您的訂單已取消!!" & vbCrLf & "若有任何疑問在煩請聯繫我們"
            Dim SMTP As New SmtpClient("smtp.gmail.com")
            SMTP.Port = 587
            SMTP.EnableSsl = True
            SMTP.Credentials = New System.Net.NetworkCredential("peizhengn@gmail.com", "pqdxtfxuhoebrqat")
            SMTP.Send(EmailMessage)
            conn.Close()
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UserFunction.Show()
        Me.Hide()
    End Sub
    Public Sub AddRoom(roomInfo As String)
        '  ListBox1.Items.Add(roomInfo)
    End Sub
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb"

        Try
            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            Dim mycommand As New OleDbCommand("select * from 訂單 where 身分證字號= '" + loginid.Text + "'", conn)
            dr = mycommand.ExecuteReader()
            While dr.Read()
                If String.IsNullOrWhiteSpace(Label37.Text) Then '房間編號
                    Label37.Text = dr.GetString(8)
                ElseIf String.IsNullOrWhiteSpace(Label16.Text) Then
                    Label16.Text = dr.GetString(8)
                ElseIf String.IsNullOrWhiteSpace(Label31.Text) Then
                    Label31.Text = dr.GetString(8)
                End If
                If String.IsNullOrWhiteSpace(Label3.Text) Then '人數
                    Label3.Text = dr.GetString(7)
                ElseIf String.IsNullOrWhiteSpace(Label22.Text) Then
                    Label22.Text = dr.GetString(7)
                ElseIf String.IsNullOrWhiteSpace(Label39.Text) Then
                    Label39.Text = dr.GetString(7)
                End If

                If String.IsNullOrWhiteSpace(Label5.Text) Then '入住日期
                    Label5.Text = dr.GetString(1)
                ElseIf String.IsNullOrWhiteSpace(Label27.Text) Then
                    Label27.Text = dr.GetString(1)
                ElseIf String.IsNullOrWhiteSpace(Label43.Text) Then
                    Label43.Text = dr.GetString(1)
                End If

                If String.IsNullOrWhiteSpace(Label7.Text) Then '退房日期
                    Label7.Text = dr.GetString(2)
                ElseIf String.IsNullOrWhiteSpace(Label28.Text) Then
                    Label28.Text = dr.GetString(2)
                ElseIf String.IsNullOrWhiteSpace(Label44.Text) Then
                    Label44.Text = dr.GetString(2)
                End If
                If String.IsNullOrWhiteSpace(Label9.Text) Then '付款狀況
                    Label9.Text = dr.GetString(5)
                ElseIf String.IsNullOrWhiteSpace(Label23.Text) Then
                    Label23.Text = dr.GetString(5)
                ElseIf String.IsNullOrWhiteSpace(Label40.Text) Then
                    Label40.Text = dr.GetString(5)
                End If
            End While
            dr.Close()

            Dim mycommand_3 As New OleDbCommand("select 房型 from 房間 where 房間編號= '" + Label37.Text + "'", conn)
            dr = mycommand_3.ExecuteReader()
            While dr.Read()
                If String.IsNullOrWhiteSpace(Label2.Text) Then
                    Label2.Text = dr.GetString(0)
                End If
            End While
            dr.Close()

            Dim mycommand_4 As New OleDbCommand("select 房型 from 房間 where 房間編號= '" + Label16.Text + "'", conn)
            dr = mycommand_4.ExecuteReader()
            While dr.Read()
                If String.IsNullOrWhiteSpace(Label19.Text) Then
                    Label19.Text = dr.GetString(0)
                End If
            End While
            dr.Close()

            Dim mycommand_5 As New OleDbCommand("select 房型 from 房間 where 房間編號= '" + Label31.Text + "'", conn)
            dr = mycommand_5.ExecuteReader()
            While dr.Read()
                If String.IsNullOrWhiteSpace(Label34.Text) Then
                    Label34.Text = dr.GetString(0)
                End If
            End While
            dr.Close()

            Dim mycommand_2 As New OleDbCommand("select 姓名 from 顧客 where 身分證字號= '" + loginid.Text + "'", conn)
            dr = mycommand_2.ExecuteReader()
            While dr.Read()
                Label13.Text = dr.GetString(0)
            End While
            conn.Close()
        Catch ex As Exception

        End Try

        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("返回")
        Button1.BackColor = Color.Transparent
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0

        Button4.BackgroundImage = My.Resources.ResourceManager.GetObject("取消預定")
        Button4.BackColor = Color.Transparent
        Button4.BackgroundImageLayout = ImageLayout.Stretch
        Button4.FlatStyle = FlatStyle.Flat
        Button4.FlatAppearance.BorderSize = 0
        Button2.BackgroundImage = My.Resources.ResourceManager.GetObject("取消預定")
        Button2.BackColor = Color.Transparent
        Button2.BackgroundImageLayout = ImageLayout.Stretch
        Button2.FlatStyle = FlatStyle.Flat
        Button2.FlatAppearance.BorderSize = 0
        Button3.BackgroundImage = My.Resources.ResourceManager.GetObject("取消預定")
        Button3.BackColor = Color.Transparent
        Button3.BackgroundImageLayout = ImageLayout.Stretch
        Button3.FlatStyle = FlatStyle.Flat
        Button3.FlatAppearance.BorderSize = 0
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If String.IsNullOrWhiteSpace(Label16.Text) Then
                MessageBox.Show("這邊沒有訂房紀錄")
                Exit Sub
            End If
            Dim result As DialogResult = MessageBox.Show("確定取消?", "選擇", MessageBoxButtons.YesNo)
            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "delete from 訂單 where 房間編號 = '" + Label16.Text + "'"
            If result = DialogResult.Yes Then
                cmd.ExecuteNonQuery()
                MessageBox.Show("已取消房間")
                'Me.Hide()
            End If
            conn.Close()
            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            Dim mycommand As New OleDbCommand("select 電子信箱 from 顧客 where 身分證字號= '" + loginid.Text + "'", conn)
            dr = mycommand.ExecuteReader()
            While dr.Read()
                mail = dr.GetString(0)
            End While

            Dim EmailMessage As New MailMessage()

            ' mail = loginid.Text

            EmailMessage.From = New MailAddress("peizhengn@gmail.com")
            EmailMessage.To.Add(mail)
            EmailMessage.Subject = "訂房取消通知"
            EmailMessage.Body = "您的訂單已取消!!" & vbCrLf & "若有任何疑問在煩請聯繫我們"
            Dim SMTP As New SmtpClient("smtp.gmail.com")
            SMTP.Port = 587
            SMTP.EnableSsl = True
            SMTP.Credentials = New System.Net.NetworkCredential("peizhengn@gmail.com", "pqdxtfxuhoebrqat")
            SMTP.Send(EmailMessage)
            conn.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If String.IsNullOrWhiteSpace(Label31.Text) Then
                MessageBox.Show("這邊沒有訂房紀錄")
                Exit Sub
            End If
            Dim result As DialogResult = MessageBox.Show("確定取消?", "選擇", MessageBoxButtons.YesNo)
            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "delete from 訂單 where 房間編號 = '" + Label31.Text + "'"
            If result = DialogResult.Yes Then
                cmd.ExecuteNonQuery()
                MessageBox.Show("已取消房間")
                ' Me.Hide()
            End If
            conn.Close()

            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            Dim mycommand As New OleDbCommand("select 電子信箱 from 顧客 where 身分證字號= '" + loginid.Text + "'", conn)
            dr = mycommand.ExecuteReader()
            While dr.Read()
                mail = dr.GetString(0)
            End While

            Dim EmailMessage As New MailMessage()

            ' mail = loginid.Text

            EmailMessage.From = New MailAddress("peizhengn@gmail.com")
            EmailMessage.To.Add(mail)
            EmailMessage.Subject = "訂房取消通知"
            EmailMessage.Body = "您的訂單已取消!!" & vbCrLf & "若有任何疑問在煩請聯繫我們"
            Dim SMTP As New SmtpClient("smtp.gmail.com")
            SMTP.Port = 587
            SMTP.EnableSsl = True
            SMTP.Credentials = New System.Net.NetworkCredential("peizhengn@gmail.com", "pqdxtfxuhoebrqat")
            SMTP.Send(EmailMessage)
            conn.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class