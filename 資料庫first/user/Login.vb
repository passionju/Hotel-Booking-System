Imports System.Collections.ObjectModel
Imports System.Data.OleDb
Imports Microsoft.Win32
Imports Microsoft.Win32.SafeHandles

Public Class Login
    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    '   Dim accountlogin As String
    Dim dr As OleDbDataReader
    Dim loginid As String
    Public userid As String
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb"

        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("登入2")
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
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim checker As Integer

        Try

            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SELECT 電子信箱,密碼,身分證字號   FROM 顧客 WHERE 密碼 ='" + TextBox3.Text + "' AND 電子信箱 ='" + TextBox1.Text + "'"
            dr = cmd.ExecuteReader()
            checker = 0
            Do While (dr.Read())
                checker = checker + 1
                loginid = dr.GetString(2)
            Loop
            If (checker = 1) Then
                MessageBox.Show("登錄成功")
                UserFunction.Label1.Text = loginid
                userid = loginid
                UserFunction.Show()
                Me.Hide()
            Else
                MessageBox.Show("登錄失敗")
            End If
            conn.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim fst As New StartForm
        fst.Show()
        Me.Hide()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class