Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraGrid.Views.Layout
Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Controls
Imports System.IO


Namespace WindowsApplication3
	Partial Public Class Form1
		Inherits XtraForm
		Public Sub New()
			InitializeComponent()
		End Sub

		Private separatorChar As Char

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' TODO: This line of code loads data into the 'carsDBDataSet.Cars' table. You can move, or remove it, as needed.
			Me.carsTableAdapter.Fill(Me.carsDBDataSet.Cars)
			separatorChar = checkedComboBoxEdit1.Properties.SeparatorChar
		End Sub

		Private Sub OnGetActiveObject(ByVal sender As Object, ByVal e As DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs) Handles toolTipController1.GetActiveObjectInfo
			If e.SelectedControl Is checkedComboBoxEdit1 Then
				Dim editInfo As PopupContainerEditViewInfo = TryCast(checkedComboBoxEdit1.GetViewInfo(), PopupContainerEditViewInfo)
				Dim hitInfo As EditHitInfo = editInfo.CalcHitInfo(e.ControlMousePosition)
				If hitInfo.HitTest = EditHitTest.MaskBox Then
					Dim val As Object = checkedComboBoxEdit1.EditValue
					If val Is Nothing OrElse val Is DBNull.Value OrElse val.Equals(String.Empty) Then
						Return
					End If
					Dim checkedItems() As String = checkedComboBoxEdit1.Text.Split(separatorChar)
					Dim rect As Rectangle = editInfo.MaskBoxRect
					Dim description As String = GetCheckedItemDescription(checkedItems, editInfo.PaintAppearance.Font, e.ControlMousePosition, rect)
					Dim itemIndex As Integer = GetItemIndex(checkedComboBoxEdit1, description)
					If itemIndex = -1 Then
						Return
					End If
                    Dim tInfo As ToolTipControlInfo = New ToolTipControlInfo()
					tInfo.Object = itemIndex
					tInfo.SuperTip = GetSuperToolTip(checkedComboBoxEdit1, carsDBDataSet.Cars(itemIndex))
					e.Info = tInfo
				End If
			End If
		End Sub

		Private Function GetItemIndex(ByVal edit As CheckedComboBoxEdit, ByVal description As String) As Integer
			Dim collection As CheckedListBoxItemCollection = edit.Properties.GetItems()
			For i As Integer = 0 To collection.Count - 1
				If collection(i).Description.Equals(description) Then
					Return i
				End If
			Next i
			Return -1
		End Function

		Private Function GetSuperToolTip(ByVal edit As CheckedComboBoxEdit, ByVal row As DataRow) As SuperToolTip
			Dim superToolTip As New SuperToolTip()
			Dim itemDecsr As New ToolTipItem()
			itemDecsr.Text = row(edit.Properties.DisplayMember).ToString()
			Dim itemValue As New ToolTipItem()
			itemValue.Text = row(edit.Properties.ValueMember).ToString()
			Dim itemImage As New ToolTipItem()
			itemImage.Image = GetToolTipImage(row, itemImage)
			superToolTip.Items.Add(itemDecsr)
			superToolTip.Items.Add(itemValue)
			superToolTip.Items.Add(itemImage)
			Return superToolTip
		End Function

		Private Function GetToolTipImage(ByVal row As DataRow, ByVal itemImage As ToolTipItem) As Image
			Using stream As New MemoryStream(CType(row("Picture"), Byte()))
				Return Image.FromStream(stream)
			End Using
		End Function

		Private Function GetCheckedItemDescription(ByVal checkedItems() As String, ByVal font As Font, ByVal point As Point, ByVal rect As Rectangle) As String
			Dim widths() As Integer = GetValuesWidths(font, checkedItems)
			Dim valRect As New Rectangle(rect.X, rect.Y, widths(0), rect.Height)
			Dim n As Integer = 0
			Do While (Not valRect.Contains(point)) AndAlso n < widths.Length - 1
				valRect.X += widths(n)
				n += 1
				valRect.Width = widths(n)
			Loop
			Return checkedItems(n).Trim()
		End Function

		Private Function GetValuesWidths(ByVal font As Font, ByVal values() As String) As Integer()
			Dim info As New GraphicsInfo()
			info.AddGraphics(Nothing)

			Dim widths(values.Length - 1) As Integer
			Dim separatorWidth As Integer = GetSeparatorWidth(font, separatorChar, info.Graphics)
			For i As Integer = 0 To values.Length - 2
				widths.SetValue(DevExpress.Utils.Text.TextUtils.GetStringSize(info.Graphics, values(i), font).Width + separatorWidth, i)
			Next i
			widths.SetValue(DevExpress.Utils.Text.TextUtils.GetStringSize(info.Graphics, values(values.Length - 1), font).Width, values.Length - 1)

			info.ReleaseGraphics()

			Return widths
		End Function

		Private Function GetSeparatorWidth(ByVal font As Font, ByVal separatorChar As Char, ByVal gr As Graphics) As Integer
			Return DevExpress.Utils.Text.TextUtils.GetStringSize(gr, separatorChar.ToString(), font).Width
		End Function
	End Class

End Namespace