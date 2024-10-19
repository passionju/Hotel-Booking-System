Imports System.Data.OleDb
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar

Public Class DelEmployee
    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim f9_2 As New EmployeeManage

        f9_2.Show()

        Me.Hide()
    End Sub

    Private Sub Form9_2_2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb"

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
        viewer()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            conn.Open()
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "delete from 旅館職員 where 員工編號 = '" + Label2.Text + "'"
            cmd.ExecuteNonQuery()
            conn.Close()
            dt = New DataTable()
            da = New OleDbDataAdapter("select * from 旅館職員 ", conn)
            da.Fill(dt)
            DataGridView1.DataSource = dt
            MessageBox.Show("刪除成功", "刪除員工資料", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception

        End Try

    End Sub
    Private Function IsInputValidID(inputText As String) As Boolean
        Dim pattern1 As String = "^[A-Z]{1}[0-9]{3}$"
        Dim regex1 As New System.Text.RegularExpressions.Regex(pattern1)
        Return regex1.IsMatch(inputText)
    End Function

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim selectedGR As DataGridViewRow
        selectedGR = DataGridView1.Rows(e.RowIndex)
        Try
            Label2.Text = selectedGR.Cells(0).Value.ToString()

        Catch ex As Exception
            MessageBox.Show("點選正確的位置")
        End Try
    End Sub
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

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class