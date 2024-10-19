Imports System.Data.OleDb

Public Class roominfo2
    Dim conn As New OleDbConnection
    Dim cmd As OleDbCommand
    Dim dt As New DataTable
    Dim da As New OleDbDataAdapter(cmd)
    Dim dr As OleDbDataReader
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Me.Hide()
    End Sub

    Private Sub roominfo2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.ConnectionString = "Provider=Microsoft.JET.OLEDB.4.0;data source=D:\hotel_data.mdb"
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

        Button1.BackgroundImage = My.Resources.ResourceManager.GetObject("返回")
        Button1.BackColor = Color.Transparent
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderSize = 0
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class