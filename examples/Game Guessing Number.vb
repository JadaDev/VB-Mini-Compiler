Imports System
Imports System.Windows.Forms
Imports System.Drawing

Public Class NumberGuessingGameForm
    Inherits Form

    Private WithEvents textBox As New TextBox()
    Private targetNumber As Integer
    Private resultLabel As New Label()
    Private attemptsLabel As New Label()
    Private attempts As Integer = 0

    Public Sub New()
        InitializeForm()
        InitializeGame()
        InitializeControls()
    End Sub

    Private Sub InitializeForm()
        Me.Text = "Number Guessing Game"
        Me.Size = New Size(650, 350)
        Me.FormBorderStyle = FormBorderStyle.Sizable
        Me.MaximizeBox = True
        Me.MinimizeBox = True
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(240, 240, 240)
    End Sub

    Private Sub InitializeGame()
        Dim rand As New Random()
        targetNumber = rand.Next(1, 101)
    End Sub

    Private Sub InitializeControls()
        InitializeLabels()
        InitializeTextBox()
        InitializeGuessButton()
        CreateLayout()
    End Sub

    Private Sub InitializeLabels()
        Dim titleLabel As New Label()
        titleLabel.Text = "Number Guessing Game"
        titleLabel.Font = New Font("Arial", 18, FontStyle.Bold)
        titleLabel.ForeColor = Color.SteelBlue
        titleLabel.Dock = DockStyle.Top
        titleLabel.TextAlign = ContentAlignment.MiddleCenter

        Dim messageLabel As New Label()
        messageLabel.Text = "Can you guess the number between 1 and 100?"
        messageLabel.Font = New Font("Arial", 12)
        messageLabel.ForeColor = Color.DimGray
        messageLabel.Dock = DockStyle.Top
        messageLabel.TextAlign = ContentAlignment.MiddleCenter

        resultLabel.Text = ""
        resultLabel.Font = New Font("Arial", 14)
        resultLabel.ForeColor = Color.Green
        resultLabel.Dock = DockStyle.Bottom
        resultLabel.TextAlign = ContentAlignment.MiddleCenter

        attemptsLabel.Text = "Attempts: 0"
        attemptsLabel.Font = New Font("Arial", 12)
        attemptsLabel.ForeColor = Color.DimGray
        attemptsLabel.Dock = DockStyle.Top
        attemptsLabel.TextAlign = ContentAlignment.MiddleCenter

        Me.Controls.Add(titleLabel)
        Me.Controls.Add(messageLabel)
        Me.Controls.Add(resultLabel)
        Me.Controls.Add(attemptsLabel)
    End Sub

    Private Sub InitializeGuessButton()
        Dim buttonPanel As New Panel()
        buttonPanel.Dock = DockStyle.Bottom

        Dim guessButton As New Button()
        guessButton.Text = "Guess"
        guessButton.Font = New Font("Arial", 20, FontStyle.Bold)
        guessButton.BackColor = Color.Skyblue
        guessButton.ForeColor = Color.White
        guessButton.Height = 60
        guessButton.Dock = DockStyle.Bottom

        AddHandler guessButton.Click, AddressOf GuessButton_Click

        buttonPanel.Controls.Add(guessButton)
        Me.Controls.Add(buttonPanel)
    End Sub

    Private Sub InitializeTextBox()
        textBox.Font = New Font("Arial", 12)
        textBox.ForeColor = Color.Black
        textBox.Dock = DockStyle.Top
        textBox.Multiline = False
        textBox.Height = 80
        textBox.TextAlign = HorizontalAlignment.Center
    End Sub

    Private Sub CreateLayout()
        Dim resultPanel As New Panel()
        resultPanel.Dock = DockStyle.Top
        resultPanel.Height = 80
        resultPanel.Controls.Add(resultLabel)

        Dim attemptsPanel As New Panel()
        attemptsPanel.Dock = DockStyle.Top
        attemptsPanel.Height = 40
        attemptsPanel.Controls.Add(attemptsLabel)

        Dim inputPanel As New Panel()
        inputPanel.Dock = DockStyle.Top
        inputPanel.Height = 30
        inputPanel.Controls.Add(textBox)

        Dim mainPanel As New Panel()
        mainPanel.Dock = DockStyle.Fill
        mainPanel.Padding = New Padding(10)
        mainPanel.Controls.Add(inputPanel)
        mainPanel.Controls.Add(attemptsPanel)
        mainPanel.Controls.Add(resultPanel)

        Me.Controls.Add(mainPanel)
    End Sub

    Private Sub GuessButton_Click(sender As Object, e As EventArgs)
        If Integer.TryParse(textBox.Text, Nothing) Then
            Dim guess As Integer = Integer.Parse(textBox.Text)
            attempts += 1
            attemptsLabel.Text = "Attempts: " & attempts.ToString()

            If guess < targetNumber Then
                resultLabel.Text = "Too low. Try again."
            ElseIf guess > targetNumber Then
                resultLabel.Text = "Too high. Try again."
            Else
                resultLabel.Text = "Congratulations! You guessed the number " & targetNumber.ToString() & " in " & attempts.ToString() & " attempts."
            End If
        Else
            resultLabel.Text = "Invalid input. Please enter a valid number."
        End If
    End Sub

    Public Shared Sub Main()
        Application.Run(New NumberGuessingGameForm())
    End Sub

End Class
