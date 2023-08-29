Imports System
Imports System.Windows.Forms
Imports System.Drawing

Public Class ListManagerForm
    Inherits Form

    Private listBox As New ListBox()
    Private addItemButton As New Button()
    Private removeItemButton As New Button()
    Private newItemTextBox As New TextBox()

    Public Sub New()
        InitializeForm()
        InitializeControls()
    End Sub

    Private Sub InitializeForm()
        Me.Text = "List Manager"
        Me.Size = New Size(400, 300)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.BackColor = Color.White
    End Sub

    Private Sub InitializeControls()
        InitializeNewItemTextBox()
        InitializeListBox()
        InitializeAddItemButton()
        InitializeRemoveItemButton()
        CreateLayout()
    End Sub

    Private Sub InitializeNewItemTextBox()
        newItemTextBox.Dock = DockStyle.Top
        newItemTextBox.Height = 30
        newItemTextBox.Font = New Font(newItemTextBox.Font.FontFamily, 12)
    End Sub

    Private Sub InitializeListBox()
        listBox.Dock = DockStyle.Fill
        listBox.Font = New Font(listBox.Font.FontFamily, 12)
    End Sub

    Private Sub InitializeAddItemButton()
        addItemButton.Text = "Add Item"
        addItemButton.Dock = DockStyle.Left
        addItemButton.Font = New Font(addItemButton.Font.FontFamily, 12)
        addItemButton.BackColor = Color.FromArgb(0, 192, 0) ' Green
        addItemButton.ForeColor = Color.White
        AddHandler addItemButton.Click, AddressOf AddItemButton_Click
    End Sub

    Private Sub InitializeRemoveItemButton()
        removeItemButton.Text = "Remove Item"
        removeItemButton.Dock = DockStyle.Left
        removeItemButton.Font = New Font(removeItemButton.Font.FontFamily, 12)
        removeItemButton.BackColor = Color.Red
        removeItemButton.ForeColor = Color.White
        AddHandler removeItemButton.Click, AddressOf RemoveItemButton_Click
    End Sub

    Private Sub CreateLayout()
        Dim buttonPanel As New TableLayoutPanel()
        buttonPanel.Dock = DockStyle.Bottom
        buttonPanel.RowCount = 1
        buttonPanel.ColumnCount = 3
        buttonPanel.Controls.Add(addItemButton)
        buttonPanel.Controls.Add(removeItemButton)
        buttonPanel.Controls.Add(newItemTextBox)

        Me.Controls.Add(listBox)
        Me.Controls.Add(buttonPanel)
    End Sub

    Private Sub AddItemButton_Click(sender As Object, e As EventArgs)
        Dim newItem As String = newItemTextBox.Text.Trim()
        If Not String.IsNullOrEmpty(newItem) Then
            listBox.Items.Add(newItem)
            newItemTextBox.Clear()
        End If
    End Sub

    Private Sub RemoveItemButton_Click(sender As Object, e As EventArgs)
        Dim selectedIndex As Integer = listBox.SelectedIndex
        If selectedIndex >= 0 Then
            listBox.Items.RemoveAt(selectedIndex)
        End If
    End Sub

    Public Shared Sub Main()
        Application.Run(New ListManagerForm())
    End Sub
End Class
