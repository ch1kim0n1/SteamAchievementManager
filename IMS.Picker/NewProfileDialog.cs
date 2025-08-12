using System;
using System.Windows.Forms;

namespace IMS.Picker
{
    public class NewProfileDialog : Form
    {
        private TextBox nameTextBox;
        private Button okButton;
        private Button cancelButton;

        public string ProfileName => nameTextBox.Text.Trim();

        public NewProfileDialog()
        {
            this.Text = "New Profile";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new System.Drawing.Size(320, 120);

            var label = new Label
            {
                Text = "Profile Name:",
                Location = new System.Drawing.Point(12, 15),
                Size = new System.Drawing.Size(80, 23)
            };

            nameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(100, 12),
                Size = new System.Drawing.Size(200, 23)
            };

            okButton = new Button
            {
                Text = "OK",
                Location = new System.Drawing.Point(145, 50),
                Size = new System.Drawing.Size(75, 23),
                DialogResult = DialogResult.OK
            };

            cancelButton = new Button
            {
                Text = "Cancel",
                Location = new System.Drawing.Point(225, 50),
                Size = new System.Drawing.Size(75, 23),
                DialogResult = DialogResult.Cancel
            };

            okButton.Click += (_, __) =>
            {
                if (string.IsNullOrWhiteSpace(ProfileName))
                {
                    MessageBox.Show(this, "Profile name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.DialogResult = DialogResult.OK;
            };

            this.Controls.AddRange(new Control[] { label, nameTextBox, okButton, cancelButton });
            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;
        }
    }
}
