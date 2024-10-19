Imports FxResources.System
Imports System.Data.OleDb
Imports System.Data.SqlTypes
Imports System.Net.Mail
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Order_2
    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    Private bitmap As Bitmap
    Dim date1, date2 As String
    Dim dr As OleDbDataReader
    Dim k As String
    Dim totolmoney As Integer
    Dim intValue As Integer
    Public day As Integer
    Dim strmoney As String
    Public startdate, enddate As String

    Private Sub Order_2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb"
        If RadioButton1.Checked Then
            Label4.Text = "是"
        ElseIf RadioButton2.Checked Then
            Label4.Text = "否"
        End If
        conn.Open()
        cmd = conn.CreateCommand()
        cmd.CommandText = "select 房型,價格 from 房間 where 房間編號='" + Label9.Text + "'"
        cmd.ExecuteNonQuery()
        Dim reader As OleDbDataReader = cmd.ExecuteReader()
        If reader.Read() Then
            Label10.Text = reader("房型").ToString()
            Label12.Text = reader("價格").ToString()
        End If
        Integer.TryParse(Label12.Text, intValue)
        If day = 0 Then
            day = 1
        End If
        totolmoney = intValue * day

        Label5.Text = totolmoney
        strmoney = totolmoney.ToString()
        conn.Close()

        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("結帳")
        Button1.BackColor = Color.Transparent
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim maxOrderNumber As String = GetMaxOrderNumber()
        Dim nextOrderNumber As String = GenerateNextOrderNumber(maxOrderNumber)
        Dim fo As New Order
        fo.Owner = Me
        Dim fl As New Login
        fl.Owner = Me
        Dim userInput As String = TextBox1.Text
        Dim a As Integer = 0
        Dim b As Integer = 0
        Dim c As Integer = 0

        Dim mail As New String("")
        Try
            date1 = startdate
            date2 = enddate
            If Label10.Text = "單人房" Then
                If TextBox1.Text <= 1 Then
                    c = 1
                Else
                    MsgBox("無法填入大於所訂購房型的人數")
                    Exit Sub
                End If
            ElseIf Label10.Text = "雙人房" Then
                If TextBox1.Text <= 2 Then
                    c = 1
                Else
                    MsgBox("無法填入大於所訂購房型的人數")
                    Exit Sub
                End If

            ElseIf Label10.Text = "四人房" Then
                If TextBox1.Text <= 4 Then
                    c = 1
                Else
                    MsgBox("無法填入大於所訂購房型的人數")
                    Exit Sub
                End If
            ElseIf Label10.Text = "總統套房" Then
                If TextBox1.Text <= 5 Then
                    c = 1
                Else
                    MsgBox("無法填入大於所訂購房型的人數")
                    Exit Sub
                End If
            End If



            If RadioButton1.Checked = True Then
                Label4.Text = RadioButton1.Text
            ElseIf RadioButton2.Checked = True Then
                Label4.Text = RadioButton2.Text
            End If
            If IsInputValidroompeople(userInput) Then
                a = 1
            Else
                MsgBox("請輸入正確的人數")
                Exit Sub
            End If
            If RadioButton1.Checked Or RadioButton2.Checked = True Then
                b = 1
            Else
                MsgBox("請勾選是否用餐")
                Exit Sub
            End If

            If a = 1 And b = 1 And c = 1 Then
                conn.Open()
                cmd = conn.CreateCommand()
                cmd.CommandType = CommandType.Text



                cmd.CommandText = "INSERT INTO 訂單(訂單編號, 入住日期, 退房日期, 供餐資訊, 總金額, 付款狀態, 身分證字號, 入住人數, 房間編號,入住狀態) VALUES('" + nextOrderNumber + "', '" +
        date1 + "', '" + date2 + "', '" + Label4.Text + "', '" + strmoney + "', '未付款', '" + loginid.Text + "', '" + TextBox1.Text + "', '" + Label9.Text + "','未入住') "
                cmd.ExecuteNonQuery()
                MessageBox.Show("新增成功", "新增訂單", MessageBoxButtons.OK, MessageBoxIcon.Information)
                conn.Close()
                Dim fuf As New UserFunction
                fuf.Label1.Text = loginid.Text
                fuf.Show()
                Me.Hide()
            End If
        Catch ex As Exception
            MessageBox.Show("資料請輸入完整")
            Exit Sub
        End Try


        Dim EmailMessage As New MailMessage()
        ' Dim mail As New String("")
        'mail = loginid.Text
        Try
            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            Dim mycommand As New OleDbCommand("select 電子信箱 from 顧客 where 身分證字號= '" + loginid.Text + "'", conn)
            dr = mycommand.ExecuteReader()
            While dr.Read()
                mail = dr.GetString(0)
            End While

            EmailMessage.From = New MailAddress("peizhengn@gmail.com")
            EmailMessage.To.Add(mail)
            EmailMessage.Subject = "訂房成功通知"
            EmailMessage.Body = "訂房成功!! " & vbCrLf & " 感謝您" & vbCrLf & "您的入住日期為" & date1
            Dim SMTP As New SmtpClient("smtp.gmail.com")
            SMTP.Port = 587
            SMTP.EnableSsl = True
            SMTP.Credentials = New System.Net.NetworkCredential("peizhengn@gmail.com", "pqdxtfxuhoebrqat")
            SMTP.Send(EmailMessage)
        Catch ex As Exception

        End Try
    End Sub
    Public Function roomtypecorrect(inputText As String) As Boolean
        Dim pattern2 As String = "^[0-9]+$"
        Dim regex2 As New System.Text.RegularExpressions.Regex(pattern2)


        Return regex2.IsMatch(inputText)
    End Function
    Public Function GetMaxOrderNumber() As String
        Dim sql As String = "SELECT MAX(訂單編號) FROM 訂單"
        Dim maxOrderNumber As String = ""

        Using connection As New OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb")
            Using command As New OleDbCommand(sql, connection)
                connection.Open()

                Dim result As Object = command.ExecuteScalar()

                If result IsNot DBNull.Value Then
                    maxOrderNumber = result.ToString()
                End If
            End Using
        End Using

        Return maxOrderNumber
    End Function
    Private Function IsInputValidroompeople(inputText As String) As Boolean
        Dim pattern1 As String = "^[0-9]+$"
        Dim regex1 As New System.Text.RegularExpressions.Regex(pattern1)
        Return regex1.IsMatch(inputText)
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim fod As New Order
        fod.Show()
        Me.Hide()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub




    Public Function GenerateNextOrderNumber(maxOrderNumber As String) As String
        If String.IsNullOrEmpty(maxOrderNumber) Then
            ' 如果目前沒有任何訂單編號，則生成第一個編號
            Return "0001"
        Else
            ' 將目前最大編號轉換為數字並加1，再補零至4位數
            Dim nextNumber As Integer = Integer.Parse(maxOrderNumber) + 1
            Return nextNumber.ToString("D4")
        End If
    End Function
End Class