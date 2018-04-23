using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using System.IO;


namespace WindowsApplication3 {
    public partial class Form1: XtraForm {
        public Form1() {
            InitializeComponent();
        }

        char separatorChar;

        private void Form1_Load(object sender, EventArgs e) {
            // TODO: This line of code loads data into the 'carsDBDataSet.Cars' table. You can move, or remove it, as needed.
            this.carsTableAdapter.Fill(this.carsDBDataSet.Cars);
            separatorChar = checkedComboBoxEdit1.Properties.SeparatorChar;
        }

        private void OnGetActiveObject(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e) {
            if(e.SelectedControl == checkedComboBoxEdit1) {
                PopupContainerEditViewInfo editInfo = checkedComboBoxEdit1.GetViewInfo() as PopupContainerEditViewInfo;
                EditHitInfo hitInfo = editInfo.CalcHitInfo(e.ControlMousePosition);
                if(hitInfo.HitTest == EditHitTest.MaskBox) {
                    object val = checkedComboBoxEdit1.EditValue;
                    if(val == null || val == DBNull.Value || val.Equals(string.Empty)) return;
                    string[] checkedItems = checkedComboBoxEdit1.Text.Split(separatorChar);
                    Rectangle rect = editInfo.MaskBoxRect;
                    string description = GetCheckedItemDescription(checkedItems, editInfo.PaintAppearance.Font, e.ControlMousePosition, rect);
                    int itemIndex = GetItemIndex(checkedComboBoxEdit1, description);
                    if(itemIndex == -1) return;
                    ToolTipControlInfo tInfo = tInfo = new ToolTipControlInfo();
                    tInfo.Object = itemIndex;
                    tInfo.SuperTip = GetSuperToolTip(checkedComboBoxEdit1, carsDBDataSet.Cars[itemIndex]);
                    e.Info = tInfo;
                }
            }
        }

        private int GetItemIndex(CheckedComboBoxEdit edit, string description) {
            CheckedListBoxItemCollection collection = edit.Properties.GetItems();
            for(int i = 0; i < collection.Count; i++){
                if(collection[i].Description.Equals(description))
                    return i;
            }
            return -1;
        }

        private SuperToolTip GetSuperToolTip(CheckedComboBoxEdit edit, DataRow row)  {
            SuperToolTip superToolTip = new SuperToolTip();
            ToolTipItem itemDecsr = new ToolTipItem();
            itemDecsr.Text = row[edit.Properties.DisplayMember].ToString();
            ToolTipItem itemValue = new ToolTipItem();
            itemValue.Text = row[edit.Properties.ValueMember].ToString();
            ToolTipItem itemImage = new ToolTipItem();
            itemImage.Image = GetToolTipImage(row, itemImage);
            superToolTip.Items.Add(itemDecsr);
            superToolTip.Items.Add(itemValue);
            superToolTip.Items.Add(itemImage);
            return superToolTip;
        }

        private Image GetToolTipImage(DataRow row, ToolTipItem itemImage) {
            using(MemoryStream stream = new MemoryStream((byte[])row["Picture"]))
                return Image.FromStream(stream);
        }

        private string GetCheckedItemDescription(string[] checkedItems, Font font, Point point, Rectangle rect) {
            int[] widths = GetValuesWidths(font, checkedItems);
            Rectangle valRect = new Rectangle(rect.X, rect.Y, widths[0], rect.Height);
            int n = 0;
            while(!valRect.Contains(point) && n < widths.Length - 1) {
                valRect.X += widths[n++];
                valRect.Width = widths[n];
            }
            return checkedItems[n].Trim();
        }

        private int[] GetValuesWidths(Font font, string[] values) {
            GraphicsInfo info = new GraphicsInfo();
            info.AddGraphics(null);

            int[] widths = new int[values.Length];
            int separatorWidth = GetSeparatorWidth(font, separatorChar, info.Graphics);
            for(int i = 0;i < values.Length - 1;i++)
                widths.SetValue(DevExpress.Utils.Text.TextUtils.GetStringSize(info.Graphics,
                    values[i], font).Width + separatorWidth, i);
            widths.SetValue(DevExpress.Utils.Text.TextUtils.GetStringSize(info.Graphics,
                    values[values.Length - 1], font).Width, values.Length - 1);

            info.ReleaseGraphics();

            return widths;
        }

        private int GetSeparatorWidth(Font font, char separatorChar, Graphics gr) {
            return DevExpress.Utils.Text.TextUtils.GetStringSize(gr,
                separatorChar.ToString(), font).Width;
        }
    }

}