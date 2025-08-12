using System;
using System.Windows.Forms;

namespace IMS.Manager
{
    public class EditCurrencyDialog : Form
    {
        private TextBox code;
        private TextBox name;
        private NumericUpDown amount;
        private Button ok;
        private Button cancel;

        public string Code => code.Text.Trim();
        public string NameText => name.Text.Trim();
        public long Amount => (long)amount.Value;

        public EditCurrencyDialog()
        {
            this.Text = "Add Currency";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new System.Drawing.Size(360, 180);

            code = new TextBox { Left = 100, Top = 16, Width = 230 };
            name = new TextBox { Left = 100, Top = 48, Width = 230 };
            amount = new NumericUpDown { Left = 100, Top = 80, Width = 100, Minimum = 0, Maximum = 1000000000, Value = 100 };
            ok = new Button { Left = 180, Top = 120, Width = 70, Text = "OK" };
            cancel = new Button { Left = 260, Top = 120, Width = 70, Text = "Cancel" };

            ok.Click += (_, __) =>
            {
                if (string.IsNullOrWhiteSpace(Code))
                {
                    MessageBox.Show(this, "Code is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.DialogResult = DialogResult.OK;
            };
            cancel.Click += (_, __) => this.DialogResult = DialogResult.Cancel;

            Controls.AddRange(new Control[]
            {
                new Label{ Left = 16, Top = 18, Text = "Code:" }, code,
                new Label{ Left = 16, Top = 50, Text = "Name:" }, name,
                new Label{ Left = 16, Top = 82, Text = "Amount:" }, amount,
                ok, cancel
            });
        }
    }
}
