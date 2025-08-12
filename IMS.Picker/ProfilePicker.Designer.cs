using System.Windows.Forms;

namespace IMS.Picker
{
    partial class ProfilePicker
    {
        private ListView profileListView;
        private Button manageButton;
        private Button newProfileButton;
        private Button deleteProfileButton;
        private Button refreshButton;

        private void InitializeComponent()
        {
            this.profileListView = new ListView();
            this.manageButton = new Button();
            this.newProfileButton = new Button();
            this.deleteProfileButton = new Button();
            this.refreshButton = new Button();
            this.SuspendLayout();

            // profileListView
            this.profileListView.Location = new System.Drawing.Point(12, 12);
            this.profileListView.Size = new System.Drawing.Size(560, 300);
            this.profileListView.UseCompatibleStateImageBehavior = false;
            this.profileListView.View = View.Details;
            this.profileListView.FullRowSelect = true;
            this.profileListView.GridLines = true;
            this.profileListView.MultiSelect = false;
            this.profileListView.DoubleClick += OnDoubleClick;

            // manageButton
            this.manageButton.Location = new System.Drawing.Point(590, 12);
            this.manageButton.Size = new System.Drawing.Size(100, 30);
            this.manageButton.Text = "Manage";
            this.manageButton.UseVisualStyleBackColor = true;
            this.manageButton.Click += OnManage;

            // newProfileButton
            this.newProfileButton.Location = new System.Drawing.Point(590, 50);
            this.newProfileButton.Size = new System.Drawing.Size(100, 30);
            this.newProfileButton.Text = "New Profile";
            this.newProfileButton.UseVisualStyleBackColor = true;
            this.newProfileButton.Click += OnNewProfile;

            // deleteProfileButton
            this.deleteProfileButton.Location = new System.Drawing.Point(590, 88);
            this.deleteProfileButton.Size = new System.Drawing.Size(100, 30);
            this.deleteProfileButton.Text = "Delete";
            this.deleteProfileButton.UseVisualStyleBackColor = true;
            this.deleteProfileButton.Click += OnDeleteProfile;

            // refreshButton
            this.refreshButton.Location = new System.Drawing.Point(590, 126);
            this.refreshButton.Size = new System.Drawing.Size(100, 30);
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += OnRefresh;

            // ProfilePicker
            this.ClientSize = new System.Drawing.Size(710, 330);
            this.Text = "Inventory Management System - Profile Picker";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(this.profileListView);
            this.Controls.Add(this.manageButton);
            this.Controls.Add(this.newProfileButton);
            this.Controls.Add(this.deleteProfileButton);
            this.Controls.Add(this.refreshButton);
            this.ResumeLayout(false);
        }
    }
}
