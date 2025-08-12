using System.Windows.Forms;

namespace IMS.Manager
{
    partial class InventoryManager
    {
        private TabControl mainTabControl;
        private TabPage inventoryTabPage;
        private TabPage currencyTabPage;
        private ListView inventoryListView;
        private ListView currencyListView;
        private ToolStrip inventoryToolStrip;
        private ToolStrip currencyToolStrip;
        private ToolStripButton addItemButton;
        private ToolStripButton removeItemButton;
        private ToolStripButton reloadButton;
        private ToolStripButton storeButton;
        private ToolStripButton addCurrencyButton;
        private ToolStripButton removeCurrencyButton;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        private void InitializeComponent()
        {
            this.mainTabControl = new TabControl();
            this.inventoryTabPage = new TabPage();
            this.currencyTabPage = new TabPage();
            this.inventoryListView = new ListView();
            this.currencyListView = new ListView();
            this.inventoryToolStrip = new ToolStrip();
            this.currencyToolStrip = new ToolStrip();
            this.addItemButton = new ToolStripButton();
            this.removeItemButton = new ToolStripButton();
            this.reloadButton = new ToolStripButton();
            this.storeButton = new ToolStripButton();
            this.addCurrencyButton = new ToolStripButton();
            this.removeCurrencyButton = new ToolStripButton();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();
            this.SuspendLayout();

            // mainTabControl
            this.mainTabControl.Controls.Add(this.inventoryTabPage);
            this.mainTabControl.Controls.Add(this.currencyTabPage);
            this.mainTabControl.Dock = DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.SelectedIndex = 0;

            // inventoryTabPage
            this.inventoryTabPage.Controls.Add(this.inventoryListView);
            this.inventoryTabPage.Controls.Add(this.inventoryToolStrip);
            this.inventoryTabPage.Location = new System.Drawing.Point(4, 22);
            this.inventoryTabPage.Padding = new Padding(3);
            this.inventoryTabPage.Size = new System.Drawing.Size(776, 396);
            this.inventoryTabPage.TabIndex = 0;
            this.inventoryTabPage.Text = "Items";
            this.inventoryTabPage.UseVisualStyleBackColor = true;

            // currencyTabPage
            this.currencyTabPage.Controls.Add(this.currencyListView);
            this.currencyTabPage.Controls.Add(this.currencyToolStrip);
            this.currencyTabPage.Location = new System.Drawing.Point(4, 22);
            this.currencyTabPage.Padding = new Padding(3);
            this.currencyTabPage.Size = new System.Drawing.Size(776, 396);
            this.currencyTabPage.TabIndex = 1;
            this.currencyTabPage.Text = "Currencies";
            this.currencyTabPage.UseVisualStyleBackColor = true;

            // inventoryListView
            this.inventoryListView.Dock = DockStyle.Fill;
            this.inventoryListView.Location = new System.Drawing.Point(3, 28);
            this.inventoryListView.UseCompatibleStateImageBehavior = false;
            this.inventoryListView.View = View.Details;

            // currencyListView
            this.currencyListView.Dock = DockStyle.Fill;
            this.currencyListView.Location = new System.Drawing.Point(3, 28);
            this.currencyListView.UseCompatibleStateImageBehavior = false;
            this.currencyListView.View = View.Details;

            // inventoryToolStrip
            this.inventoryToolStrip.Items.AddRange(new ToolStripItem[] {
                this.addItemButton,
                this.removeItemButton,
                new ToolStripSeparator(),
                this.reloadButton,
                this.storeButton
            });
            this.inventoryToolStrip.Location = new System.Drawing.Point(3, 3);
            this.inventoryToolStrip.Size = new System.Drawing.Size(770, 25);

            // currencyToolStrip
            this.currencyToolStrip.Items.AddRange(new ToolStripItem[] {
                this.addCurrencyButton,
                this.removeCurrencyButton,
                new ToolStripSeparator(),
                this.reloadButton,
                this.storeButton
            });
            this.currencyToolStrip.Location = new System.Drawing.Point(3, 3);
            this.currencyToolStrip.Size = new System.Drawing.Size(770, 25);

            // addItemButton
            this.addItemButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.addItemButton.Size = new System.Drawing.Size(23, 22);
            this.addItemButton.Text = "Add Item";
            this.addItemButton.Click += OnAddItem;

            // removeItemButton
            this.removeItemButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.removeItemButton.Size = new System.Drawing.Size(23, 22);
            this.removeItemButton.Text = "Remove Item";
            this.removeItemButton.Click += OnRemoveItem;

            // addCurrencyButton
            this.addCurrencyButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.addCurrencyButton.Size = new System.Drawing.Size(23, 22);
            this.addCurrencyButton.Text = "Add Currency";
            this.addCurrencyButton.Click += OnAddCurrency;

            // removeCurrencyButton
            this.removeCurrencyButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.removeCurrencyButton.Size = new System.Drawing.Size(23, 22);
            this.removeCurrencyButton.Text = "Remove Currency";
            this.removeCurrencyButton.Click += OnRemoveCurrency;

            // reloadButton
            this.reloadButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.reloadButton.Size = new System.Drawing.Size(23, 22);
            this.reloadButton.Text = "Reload";
            this.reloadButton.Click += OnReload;

            // storeButton
            this.storeButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.storeButton.Size = new System.Drawing.Size(23, 22);
            this.storeButton.Text = "Store";
            this.storeButton.Click += OnStore;

            // statusStrip
            this.statusStrip.Items.AddRange(new ToolStripItem[] { this.statusLabel });
            this.statusStrip.Location = new System.Drawing.Point(0, 428);
            this.statusStrip.Size = new System.Drawing.Size(784, 22);

            // statusLabel
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Ready";

            // InventoryManager
            this.ClientSize = new System.Drawing.Size(784, 450);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.statusStrip);
            this.Text = "Inventory Management System";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
