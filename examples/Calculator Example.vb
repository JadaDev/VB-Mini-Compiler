Imports System
Imports System.Windows.Forms
Imports System.Drawing

Public Class CalculatorForm
    Inherits Form

    Private WithEvents textBox As New TextBox()
    Private numButtons(9) As Button
    Private operationButtons() As Button = {New Button(), New Button(), New Button(), New Button()}
    Private clearButton As New Button()
    Private equalsButton As New Button()

    Private firstOperand As Double
    Private operatorSelected As String = ""

    Public Sub New()
        InitializeForm()
        InitializeControls()
    End Sub

    Private Sub InitializeForm()
        Me.Text = "Simple Calculator"
        Me.Size = New Size(300, 400)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
    End Sub

    Private Sub InitializeControls()
        InitializeTextBox()
        InitializeNumButtons()
        InitializeOperationButtons()
        InitializeClearButton()
        InitializeEqualsButton()
        CreateLayout()
    End Sub

    Private Sub InitializeTextBox()
        textBox.Dock = DockStyle.Top
        textBox.Multiline = True
        textBox.Height = 50
        textBox.Font = New Font(textBox.Font.FontFamily, 20)
        textBox.TextAlign = HorizontalAlignment.Right
    End Sub

    Private Sub InitializeNumButtons()
        For i As Integer = 0 To 9
            numButtons(i) = New Button()
            numButtons(i).Text = i.ToString()
            numButtons(i).Font = New Font(numButtons(i).Font.FontFamily, 16)
            numButtons(i).Dock = DockStyle.Fill
            numButtons(i).Tag = i ' Store the numeric value in the Tag property
            AddHandler numButtons(i).Click, AddressOf NumButton_Click
        Next
    End Sub

    Private Sub InitializeOperationButtons()
        Dim operationText() As String = {"+", "-", "×", "÷"}

        For i As Integer = 0 To operationButtons.Length - 1
            operationButtons(i).Text = operationText(i)
            operationButtons(i).Dock = DockStyle.Fill
            operationButtons(i).Font = New Font(operationButtons(i).Font.FontFamily, 16)
            AddHandler operationButtons(i).Click, AddressOf OperationButton_Click
        Next
    End Sub

    Private Sub InitializeClearButton()
        clearButton.Text = "C"
        clearButton.Dock = DockStyle.Fill
        clearButton.Font = New Font(clearButton.Font.FontFamily, 16)
        AddHandler clearButton.Click, AddressOf ClearButton_Click
    End Sub

    Private Sub InitializeEqualsButton()
        equalsButton.Text = "="
        equalsButton.Dock = DockStyle.Fill
        equalsButton.Font = New Font(equalsButton.Font.FontFamily, 16)
        AddHandler equalsButton.Click, AddressOf EqualsButton_Click
    End Sub

    Private Sub CreateLayout()
        Dim numButtonTable As New TableLayoutPanel()
        numButtonTable.Dock = DockStyle.Fill
        numButtonTable.RowCount = 5
        numButtonTable.ColumnCount = 3

        ' Add numeric buttons to the table
        For i As Integer = 1 To 9
            numButtonTable.Controls.Add(numButtons(i))
        Next

        numButtonTable.Controls.Add(New Label()) ' Add empty label for spacing
        numButtonTable.Controls.Add(numButtons(0))
        numButtonTable.Controls.Add(New Label()) ' Add empty label for spacing
        numButtonTable.Controls.Add(clearButton)

        Dim operationButtonTable As New TableLayoutPanel()
        operationButtonTable.Dock = DockStyle.Fill
        operationButtonTable.RowCount = 5

        ' Add operation buttons to the table
        For Each btn As Button In operationButtons
            operationButtonTable.Controls.Add(btn)
        Next

        operationButtonTable.Controls.Add(New Label()) ' Add empty label for spacing
        operationButtonTable.Controls.Add(equalsButton) ' Added equals button
        operationButtonTable.Controls.Add(New Label()) ' Add empty label for spacing

        Dim mainTable As New TableLayoutPanel()
        mainTable.Dock = DockStyle.Fill
        mainTable.RowCount = 2

        ' Add the text box and button tables to the main table
        mainTable.Controls.Add(textBox)
        mainTable.Controls.Add(numButtonTable)
        mainTable.Controls.Add(operationButtonTable)

        Me.Controls.Add(mainTable)
    End Sub

    Private Sub NumButton_Click(sender As Object, e As EventArgs)
        Dim button As Button = DirectCast(sender, Button)
        textBox.Text &= button.Tag.ToString()
    End Sub

    Private Sub OperationButton_Click(sender As Object, e As EventArgs)
        Dim button As Button = DirectCast(sender, Button)
        Dim inputText As String = textBox.Text

        If Double.TryParse(inputText, firstOperand) Then
            operatorSelected = button.Text
            textBox.Clear()
        End If
    End Sub

    Private Sub EqualsButton_Click(sender As Object, e As EventArgs)
        CalculateResult()
    End Sub

    Private Sub CalculateResult()
        Dim secondOperand As Double

        If Double.TryParse(textBox.Text, secondOperand) Then
            Select Case operatorSelected
                Case "+"
                    textBox.Text = (firstOperand + secondOperand).ToString()
                Case "-"
                    textBox.Text = (firstOperand - secondOperand).ToString()
                Case "×"
                    textBox.Text = (firstOperand * secondOperand).ToString()
                Case "÷"
                    If secondOperand = 0 Then
                        textBox.Text = "Error"
                    Else
                        textBox.Text = (firstOperand / secondOperand).ToString()
                    End If
            End Select
        End If
    End Sub

    Private Sub ClearButton_Click(sender As Object, e As EventArgs)
        textBox.Clear()
        firstOperand = 0
        operatorSelected = ""
    End Sub

    Public Shared Sub Main()
        Application.Run(New CalculatorForm())
    End Sub
End Class
