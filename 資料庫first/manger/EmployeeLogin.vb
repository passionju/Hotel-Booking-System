Imports System.Data.OleDb

Public Class EmployeeLogin
    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    '   Dim accountlogin As String

    Dim dr As OleDbDataReader
    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            cmd.CommandText = "SELECT 員工編號 , 密碼 FROM 旅館職員 WHERE 員工編號 ='" + TextBox1.Text + "' AND 密碼 ='" + TextBox2.Text + "'"
            dr = cmd.ExecuteReader()
            checker = 0
            Do While (dr.Read())
                checker = checker + 1
            Loop
            If (checker = 1) Then
                MessageBox.Show("登錄成功")
                FuntionForm.Show()
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