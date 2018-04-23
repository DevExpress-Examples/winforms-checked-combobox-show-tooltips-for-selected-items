Imports Microsoft.VisualBasic
Imports System
Namespace WindowsApplication3
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Me.defaultLookAndFeel1 = New DevExpress.LookAndFeel.DefaultLookAndFeel(Me.components)
			Me.checkedComboBoxEdit1 = New DevExpress.XtraEditors.CheckedComboBoxEdit()
			Me.toolTipController1 = New DevExpress.Utils.ToolTipController(Me.components)
			Me.carsDBDataSet = New WindowsApplication3.CarsDBDataSet()
			Me.carsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
			Me.carsTableAdapter = New WindowsApplication3.CarsDBDataSetTableAdapters.CarsTableAdapter()
			CType(Me.checkedComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.carsDBDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.carsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' defaultLookAndFeel1
			' 
			Me.defaultLookAndFeel1.LookAndFeel.SkinName = "Black"
			' 
			' checkedComboBoxEdit1
			' 
			Me.checkedComboBoxEdit1.Location = New System.Drawing.Point(30, 72)
			Me.checkedComboBoxEdit1.Name = "checkedComboBoxEdit1"
			Me.checkedComboBoxEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() { New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
			Me.checkedComboBoxEdit1.Properties.DataSource = Me.carsBindingSource
			Me.checkedComboBoxEdit1.Properties.DisplayMember = "Model"
			Me.checkedComboBoxEdit1.Properties.ValueMember = "ID"
			Me.checkedComboBoxEdit1.Size = New System.Drawing.Size(620, 22)
			Me.checkedComboBoxEdit1.TabIndex = 0
			Me.checkedComboBoxEdit1.ToolTipController = Me.toolTipController1
			' 
			' toolTipController1
			' 
'			Me.toolTipController1.GetActiveObjectInfo += New DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(Me.OnGetActiveObject);
			' 
			' carsDBDataSet
			' 
			Me.carsDBDataSet.DataSetName = "CarsDBDataSet"
			Me.carsDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
			' 
			' carsBindingSource
			' 
			Me.carsBindingSource.DataMember = "Cars"
			Me.carsBindingSource.DataSource = Me.carsDBDataSet
			' 
			' carsTableAdapter
			' 
			Me.carsTableAdapter.ClearBeforeFill = True
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(7F, 16F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(713, 359)
			Me.Controls.Add(Me.checkedComboBoxEdit1)
			Me.Name = "Form1"
			Me.Text = "Show ToolTip For Individual Value"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.checkedComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.carsDBDataSet, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.carsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private defaultLookAndFeel1 As DevExpress.LookAndFeel.DefaultLookAndFeel
		Private checkedComboBoxEdit1 As DevExpress.XtraEditors.CheckedComboBoxEdit
		Private WithEvents toolTipController1 As DevExpress.Utils.ToolTipController
		Private carsDBDataSet As CarsDBDataSet
		Private carsBindingSource As System.Windows.Forms.BindingSource
		Private carsTableAdapter As WindowsApplication3.CarsDBDataSetTableAdapters.CarsTableAdapter
	End Class
End Namespace

