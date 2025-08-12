using System;
using System.Windows.Forms;

namespace InventoryManagementSystem
{
    public class EditItemDialog : Form
    {
        private TextBox id;
        private TextBox name;
        private NumericUpDown qty;
        private TextBox tags;
        private Button ok;
        private Button cancel;

        public string ItemId => id.Text.Trim();
        public string ItemName => name.Text.Trim();
        public int Quantity => (int)qty.Value;
        public string Tags => tags.Text.Trim();

        public EditItemDialog()
        {
            this.Text = "Add Item";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new System.Drawing.Size(360, 200);

            id = new TextBox { Left = 100, Top = 16, Width = 230 };
            name = new TextBox { Left = 100, Top = 48, Width = 230 };
            qty = new NumericUpDown { Left = 100, Top = 80, Width = 100, Minimum = 0, Maximum = 1000000, Value = 1 };
            tags = new TextBox { Left = 100, Top = 112, Width = 230 };
            ok = new Button { Left = 180, Top = 150, Width = 70, Text = "OK" };
            cancel = new Button { Left = 260, Top = 150, Width = 70, Text = "Cancel" };

            ok.Click += (_, __) =>
            {
                if (string.IsNullOrWhiteSpace(ItemId))
                {
                    MessageBox.Show(this, "Id is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.DialogResult = DialogResult.OK;
            };
            cancel.Click += (_, __) => this.DialogResult = DialogResult.Cancel;

            Controls.AddRange(new Control[]
            {
                new Label{ Left = 16, Top = 18, Text = "Id:" }, id,
                new Label{ Left = 16, Top = 50, Text = "Name:" }, name,
                new Label{ Left = 16, Top = 82, Text = "Quantity:" }, qty,
                new Label{ Left = 16, Top = 114, Text = "Tags:" }, tags,
                ok, cancel
            });
        }
    }
}
