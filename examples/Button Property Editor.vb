Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel
Imports System.Reflection
Imports System
Imports System.Windows
Public Class Form1
    Inherits Form

    Private WithEvents btnCustomButton As Button
    Private WithEvents propertyGrid As PropertyGrid
    Private WithEvents richTextBoxCode As RichTextBox
    Private isDragging As Boolean = False
    Private offset As Point

    Public Sub New()
        ' Set form size and title
        Me.Size = New Size(1000, 600)
        Me.Text = "Button Property Editor"

        ' Create a custom button
        btnCustomButton = New Button()
        btnCustomButton.Text = "Custom Button"
        btnCustomButton.Width = 100
        btnCustomButton.Height = 50
        btnCustomButton.Location = New Point(10, 10)
        btnCustomButton.BackColor = Color.LightBlue
        btnCustomButton.FlatStyle = FlatStyle.Flat
        Me.Controls.Add(btnCustomButton)

        ' Create a PropertyGrid control to edit button properties
        propertyGrid = New PropertyGrid()
        propertyGrid.SelectedObject = btnCustomButton
        propertyGrid.Dock = DockStyle.Right
        propertyGrid.Width = 400
        Me.Controls.Add(propertyGrid)

        ' Create a RichTextBox to display button code
        richTextBoxCode = New RichTextBox()
        richTextBoxCode.Dock = DockStyle.Bottom
        richTextBoxCode.Size = New Size(300, 200)
        richTextBoxCode.ReadOnly = True
        Me.Controls.Add(richTextBoxCode)

        ' Subscribe to button property change events
        AddHandler btnCustomButton.TextChanged, AddressOf PropertyValueChanged
        AddHandler btnCustomButton.LocationChanged, AddressOf PropertyValueChanged
        AddHandler btnCustomButton.SizeChanged, AddressOf PropertyValueChanged
        AddHandler btnCustomButton.BackColorChanged, AddressOf PropertyValueChanged

        ' Subscribe to mouse events for button dragging
        AddHandler btnCustomButton.MouseDown, AddressOf Button_MouseDown
        AddHandler btnCustomButton.MouseMove, AddressOf Button_MouseMove
        AddHandler btnCustomButton.MouseUp, AddressOf Button_MouseUp

        ' Initialize the code display
        UpdateCode()
    End Sub

    Private Sub PropertyValueChanged(sender As Object, e As EventArgs)
        ' Handle property changes and regenerate code
        UpdateCode()
    End Sub

    Private Sub UpdateCode()
        ' Generate and display the code
        Dim code As String = GenerateCode(btnCustomButton)
        richTextBoxCode.Text = code
    End Sub

    Private Function GenerateCode(button As Button) As String
        ' Generate the code for the button
        Dim code As New System.Text.StringBuilder()
        code.AppendLine("Imports System.Windows.Forms")
        code.AppendLine("Imports System.Drawing")
        code.AppendLine()
        code.AppendLine("Dim btnCustomButton As New Button()")

        ' Use reflection to get the properties and their values
        Dim buttonType As Type = GetType(Button)
        For Each propInfo As PropertyInfo In buttonType.GetProperties(BindingFlags.Public Or BindingFlags.Instance)
            Dim propName As String = propInfo.Name
            Dim propValue As Object = propInfo.GetValue(button)
            code.AppendLine(String.Format("btnCustomButton.{0} = {1}", propName, GetPropertyString(propValue)))
        Next

        Return code.ToString()
    End Function

    Private Function GetPropertyString(propValue As Object) As String
        ' Convert property value to a string
        If propValue Is Nothing Then
            Return "Nothing"
        ElseIf TypeOf propValue Is String Then
            Return """" & propValue.ToString() & """"
        ElseIf TypeOf propValue Is Color Then
            Dim colorValue As Color = DirectCast(propValue, Color)
            Return String.Format("Color.FromArgb({0}, {1}, {2})", colorValue.R, colorValue.G, colorValue.B)
        ElseIf TypeOf propValue Is Font Then
            Dim fontValue As Font = DirectCast(propValue, Font)
            Return String.Format("New Font(""{0}"", {1}, FontStyle.{2})", fontValue.FontFamily.Name, fontValue.Size, fontValue.Style)
        Else
            Return propValue.ToString()
        End If
    End Function

    Private Sub Button_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            isDragging = True
            offset = e.Location
        End If
    End Sub

    Private Sub Button_MouseMove(sender As Object, e As MouseEventArgs)
        If isDragging Then
            Dim newLocation As Point = btnCustomButton.Location + (e.Location - offset)
            btnCustomButton.Location = newLocation
        End If
    End Sub

    Private Sub Button_MouseUp(sender As Object, e As MouseEventArgs)
        isDragging = False
    End Sub

    Public Shared Sub Main()
        Application.Run(New Form1())
    End Sub
End Class
