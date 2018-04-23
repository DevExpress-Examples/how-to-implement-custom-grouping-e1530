﻿Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Collections

Namespace DXGrid_CustomGrouping
	''' <summary>
	''' Interaction logic for Window1.xaml
	''' </summary>
	Partial Public Class Window1
		Inherits Window
		Public Sub New()
			InitializeComponent()
			grid.ItemsSource = New productsDataSetTableAdapters.ProductsTableAdapter().GetData()
			grid.Columns("UnitPrice").GroupIndex = 0
		End Sub

		Private Sub grid_CustomColumnGroup(ByVal sender As Object, _
				ByVal e As DevExpress.Xpf.Grid.CustomColumnSortEventArgs)
			If e.Column.FieldName = "UnitPrice" Then
				Dim x As Double = Math.Floor(Convert.ToDouble(e.Value1) / 10)
				Dim y As Double = Math.Floor(Convert.ToDouble(e.Value2) / 10)
				Dim res As Integer = Comparer.Default.Compare(x, y)
				If x > 9 AndAlso y > 9 Then
					res = 0
				End If
				e.Result = res
				e.Handled = True
			End If
		End Sub
		Private Sub grid_CustomGroupDisplayText(ByVal sender As Object, _
				ByVal e As DevExpress.Xpf.Grid.CustomGroupDisplayTextEventArgs)
			If e.Column.FieldName <> "UnitPrice" Then
				Return
			End If
			Dim groupLevel As Integer = grid.GetRowLevelByRowHandle(e.RowHandle)
			If groupLevel <> e.Column.GroupIndex Then
				Return
			End If
			Dim interval As String = IntervalByValue(e.Value)
			e.DisplayText = interval
		End Sub
		' Gets the text that represents the interval which contains the specified value.
		Private Function IntervalByValue(ByVal val As Object) As String
			Dim d As Double = Math.Floor(Convert.ToDouble(val) / 10)
			Dim ret As String = String.Format("{0:c} - {1:c} ", d * 10, (d + 1) * 10)
			If d > 9 Then
				ret = String.Format(">= {0:c} ", 100)
			End If
			Return ret
		End Function

	End Class
End Namespace
