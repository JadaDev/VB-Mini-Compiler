Imports System.Windows.Forms
Imports System.Drawing
Imports System.Timers
Imports System

Public Class HelloVBCompiler
    Inherits Form

    Friend WithEvents Label1 As Label
    Friend WithEvents OkButton As Button
    Private rainbowTimer As System.Timers.Timer
    Private colorIndex As Integer = 0
    Private rainbowColors As Color() = {Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet}

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OkButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()

        Me.Label1.Font = New System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(264, 48)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Hello, JadaDev!"

        Me.OkButton.Location = New System.Drawing.Point(280, 16)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.TabIndex = 1
        Me.OkButton.Text = "Ok"
        AddHandler Me.OkButton.Click, AddressOf OkButton_Click

        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(362, 58)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.OkButton, Me.Label1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "HelloVBCompiler"
        Me.Text = "Visual Basic .NET Compiler"
        Me.ResumeLayout(False)

    End Sub

    Private Sub OkButton_Click(sender As Object, e As EventArgs)
        If rainbowTimer Is Nothing Then
            rainbowTimer = New System.Timers.Timer(100)
            AddHandler rainbowTimer.Elapsed, AddressOf RainbowTimer_Tick
            rainbowTimer.Start()
            OkButton.Text = "Stop"
        Else
            rainbowTimer.Stop()
            rainbowTimer.Dispose()
            rainbowTimer = Nothing
            Label1.ForeColor = Color.Black
            OkButton.Text = "Ok"
        End If
    End Sub

    Private Sub RainbowTimer_Tick(sender As Object, e As ElapsedEventArgs)
        Label1.ForeColor = rainbowColors(colorIndex)
        colorIndex = (colorIndex + 1) Mod rainbowColors.Length
    End Sub

    Public Shared Sub Main()
        Application.Run(New HelloVBCompiler())
    End Sub

End Class
