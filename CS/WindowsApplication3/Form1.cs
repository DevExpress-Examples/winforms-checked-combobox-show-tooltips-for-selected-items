using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Data;
using System.Drawing;
using System.IO;


namespace WindowsApplication3
{
    public partial class Form1 : XtraForm
    {
        char separatorChar;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            carsBindingSource.DataSource = GetCarSchedulingDataTable();
            separatorChar = checkedComboBoxEdit1.Properties.SeparatorChar;
        }

        DataTable GetCarSchedulingDataTable()
        {
            DataTable table = new DataTable();
            table.TableName = "CarScheduling";
            table.Columns.Add(new DataColumn("ID", typeof(int)));
            table.Columns.Add(new DataColumn("Model", typeof(string)));
            table.Columns.Add(new DataColumn("Picture", typeof(byte[])));
            Random random = new Random();
            for(int i = 0; i < 20; i++)
            {
                int index = i + 1;
                Image image = WindowsApplication3.Properties.Resources.about_32x32;
                if(random.Next(0, 2) == 0)
                    image = WindowsApplication3.Properties.Resources.convert_32x32;
                using(var ms = new MemoryStream())
                {
                    image.Save(ms, image.RawFormat);
                    table.Rows.Add(index, "Car " + random.Next(0, 100), ms.ToArray());

                }
            }
            return table;
        }

        private string GetCheckedItemDescription(string[] checkedItems, Font font, Point point, Rectangle rect)
        {
            int[] widths = GetValuesWidths(font, checkedItems);
            Rectangle valRect = new Rectangle(rect.X, rect.Y, widths[0], rect.Height);
            int n = 0;
            while(!valRect.Contains(point) && n < widths.Length - 1)
            {
                valRect.X += widths[n++];
                valRect.Width = widths[n];
            }
            return checkedItems[n].Trim();
        }

        private int GetItemIndex(CheckedComboBoxEdit edit, string description)
        {
            CheckedListBoxItemCollection collection = edit.Properties.GetItems();
            for(int i = 0; i < collection.Count; i++)
            {
                if(collection[i].Description.Equals(description))
                    return i;
            }
            return -1;
        }

        private int GetSeparatorWidth(Font font, char separatorChar, Graphics gr)
        {
            return DevExpress.Utils.Text.TextUtils.GetStringSize(gr,
                separatorChar.ToString(), font).Width;
        }

        private SuperToolTip GetSuperToolTip(CheckedComboBoxEdit edit, DataRow row)
        {
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

        private Image GetToolTipImage(DataRow row, ToolTipItem itemImage)
        {
            using(MemoryStream stream = new MemoryStream((byte[])row["Picture"]))
                return Image.FromStream(stream);
        }

        private int[] GetValuesWidths(Font font, string[] values)
        {
            GraphicsInfo info = new GraphicsInfo();
            info.AddGraphics(null);

            int[] widths = new int[values.Length];
            int separatorWidth = GetSeparatorWidth(font, separatorChar, info.Graphics);
            for(int i = 0; i < values.Length - 1; i++)
                widths.SetValue(DevExpress.Utils.Text.TextUtils.GetStringSize(info.Graphics,
                    values[i], font).Width + separatorWidth, i);
            widths.SetValue(DevExpress.Utils.Text.TextUtils.GetStringSize(info.Graphics,
                    values[values.Length - 1], font).Width, values.Length - 1);

            info.ReleaseGraphics();

            return widths;
        }

        private void OnGetActiveObject(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if(e.SelectedControl == checkedComboBoxEdit1)
            {
                PopupContainerEditViewInfo editInfo = checkedComboBoxEdit1.GetViewInfo() as PopupContainerEditViewInfo;
                EditHitInfo hitInfo = editInfo.CalcHitInfo(e.ControlMousePosition);
                if(hitInfo.HitTest == EditHitTest.MaskBox)
                {
                    object val = checkedComboBoxEdit1.EditValue;
                    if(val == null || val == DBNull.Value || val.Equals(string.Empty)) return;
                    string[] checkedItems = checkedComboBoxEdit1.Text.Split(separatorChar);
                    Rectangle rect = editInfo.MaskBoxRect;
                    string description = GetCheckedItemDescription(checkedItems, editInfo.PaintAppearance.Font, e.ControlMousePosition, rect);
                    int itemIndex = GetItemIndex(checkedComboBoxEdit1, description);
                    if(itemIndex == -1) return;
                    ToolTipControlInfo tInfo = tInfo = new ToolTipControlInfo();
                    tInfo.Object = itemIndex;
                    tInfo.SuperTip = GetSuperToolTip(checkedComboBoxEdit1, (carsBindingSource.DataSource as DataTable).Rows[itemIndex]);
                    e.Info = tInfo;
                }
            }
        }
    }

}