Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class Form1
    Inherits Form

    ' System.Windows.Forms
    Private WithEvents button1 As New Button()
    Private label1 As New Label()
    Private textBox1 As New TextBox()

    Public Sub New()
        ' Initialize components
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        ' Initialize button1
        button1.Location = New Point(10, 10)
        button1.Text = "Click Me"
        AddHandler button1.Click, AddressOf Button1_Click
        Me.Controls.Add(button1)

        ' Initialize label1
        label1.Location = New Point(10, 50)
        label1.Text = "Label"
        Me.Controls.Add(label1)

        ' Initialize textBox1
        textBox1.Location = New Point(10, 80)
        textBox1.Text = ""
        Me.Controls.Add(textBox1)

        ' Set up the form
        Me.Text = "Default Form"
        Me.Size = New Size(300, 200)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        ' Button click event handler
        label1.Text = "Hello, World!"
        textBox1.Text = "Button clicked!"
    End Sub

    Public Shared Sub Main()
        Application.Run(New Form1())
    End Sub
End Class
