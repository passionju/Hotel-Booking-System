﻿Imports System.Collections.ObjectModel
Imports System.Data.OleDb
Imports System.Reflection.Emit

Public Class ChangeEmplye
    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    Dim queryString As String = "SELECT 姓名, 電話, 電子信箱,密碼 FROM 旅館職員 WHERE 員工編號 = '001'"
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim f9_2 As New EmployeeManage

        f9_2.Show()

        Me.Hide()
    End Sub

    Private Sub Form9_2_3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb"
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim userInputName As String = TextBox1.Text
        Dim a As Integer = 0
        Try
            If IsInputValidName(userInputName) Then
                a = 1
            Else
                MsgBox("請輸入中文姓名")
            End If

            Dim userInputEmail As String = TextBox2.Text
            Dim b As Integer = 0
            If IsInputValidEmail(userInputEmail) Then
                b = 1

            Else
                MsgBox("電子信箱格式不正確")

            End If

            Dim userInputPhone As String = TextBox3.Text
            Dim c As Integer = 0
            If IsInputValidPhone(userInputPhone) Then
                c = 1
            Else
                MsgBox("電話格式不正確")

            End If

            ' Dim userInputPSWD As String = TextBox4.Text
            'Dim d As Integer = 0
            'If IsInputValidPSWD(userInputPSWD) Then
            'd = 1

            'Else
            'MsgBox("密碼格式不正確")

            ' End If
            Dim p As Integer = 0
            Dim useinputposition As String = TextBox5.Text
            If IsInputValidposition(useinputposition) Then
                p = 1
            Else
                MsgBox("沒有輸入職位")
            End If


            If a = 1 And b = 1 And c = 1 And p = 1 Then
                conn.Open()
                cmd = conn.CreateCommand()
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "select 員工編號 from 旅館職員 where 員工編號= '" + Label4.Text + "'"
                Dim result As Object = cmd.ExecuteScalar()
                If result Is Nothing Then
                    MessageBox.Show("該員工不存在")
                    conn.Close()
                    Exit Sub
                End If
                cmd.CommandText = "UPDATE 旅館職員 SET 姓名='" + TextBox1.Text + "', 電話='" + TextBox3.Text + "', 電子信箱='" +
             TextBox2.Text + "', 密碼='" + TextBox4.Text + "', 職位='" + TextBox5.Text + "' WHERE 員工編號='" + Label4.Text + "'"

                cmd.ExecuteNonQuery()
                dt = New DataTable()
                da = New OleDbDataAdapter("select * from 旅館職員 ", conn)
                da.Fill(dt)
                DataGridView1.DataSource = dt
                conn.Close()
                MessageBox.Show("修改成功", "修改員工資料", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If
        Catch ex As Exception

        End Try
    End Sub
    ' Private Function IsInputValidPSWD(inputText As String) As Boolean
    'Dim pattern1 As String = "^[0-9]{4}$"
    'Dim regex1 As New System.Text.RegularExpressions.Regex(pattern1)
    'Return regex1.IsMatch(inputText)
    ' End Function

    Private Function IsInputValidEmail(inputText As String) As Boolean
        Dim pattern2 As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
        Dim regex2 As New System.Text.RegularExpressions.Regex(pattern2)
        Return regex2.IsMatch(inputText)
    End Function
    Private Function IsInputValidPhone(inputText As String) As Boolean
        Dim pattern3 As String = "^[0-9]{10}$"
        Dim regex3 As New System.Text.RegularExpressions.Regex(pattern3)
        Return regex3.IsMatch(inputText)
    End Function
    Private Function IsInputValidposition(inputText As String) As Boolean
        Dim pattern5 As String = "^[\u4E00-\u9FFF]+$"
        Dim regex5 As New System.Text.RegularExpressions.Regex(pattern5)
        Return regex5.IsMatch(inputText)
    End Function
    Private Function IsInputValidName(inputText As String) As Boolean
        Dim pattern4 As String = "^[\u4E00-\u9FFF]+$"
        Dim regex4 As New System.Text.RegularExpressions.Regex(pattern4)
        Return regex4.IsMatch(inputText)
    End Function
    Private Function IsInputValidID(inputText As String) As Boolean
        Dim pattern1 As String = "^[A-Z]{1}[0-9]{3}$"
        Dim regex1 As New System.Text.RegularExpressions.Regex(pattern1)
        Return regex1.IsMatch(inputText)
    End Function
    Private Sub viewer()

        DataGridView1.DataSource = Nothing
        DataGridView1.Refresh()

        conn.Open()
        cmd = conn.CreateCommand()
        cmd.CommandType = CommandType.Text
        da = New OleDbDataAdapter("select * from 旅館職員", conn)
        da.Fill(dt)
        DataGridView1.DataSource = dt
        conn.Close()
        DataGridView1.Columns(0).Width = 80
        DataGridView1.Columns(1).Width = 80
        DataGridView1.Columns(2).Width = 80
        DataGridView1.Columns(3).Width = 80
        DataGridView1.Columns(4).Width = 80
        DataGridView1.Columns(5).Width = 80
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim selectedGR As DataGridViewRow

        Try
            selectedGR = DataGridView1.Rows(e.RowIndex)
            Label4.Text = selectedGR.Cells(0).Value.ToString()
            TextBox3.Text = selectedGR.Cells(2).Value.ToString()
            TextBox4.Text = selectedGR.Cells(4).Value.ToString()
            TextBox2.Text = selectedGR.Cells(3).Value.ToString()
            TextBox5.Text = selectedGR.Cells(5).Value.ToString()
            TextBox1.Text = selectedGR.Cells(1).Value.ToString()
        Catch ex As Exception
            MessageBox.Show("請點選正確的位置")
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
