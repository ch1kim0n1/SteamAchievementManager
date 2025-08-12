using System.Windows.Forms;

namespace InventoryManagementSystem
{
    partial class MainForm
    {
    private DataGridView inventoryGrid;
    private DataGridView currencyGrid;
    private Button addItemButton;
    private Button removeItemButton;
    private Button addCurrencyButton;
    private Button removeCurrencyButton;
    private Button exportButton;
    private Button importButton;
    private MenuStrip menuStrip;
    private ToolStripMenuItem fileMenu;
    private ToolStripMenuItem fileNewMenuItem;
    private ToolStripMenuItem fileImportMenuItem;
    private ToolStripMenuItem fileExportMenuItem;
    private ToolStripMenuItem fileExitMenuItem;
    private ToolStripMenuItem editMenu;
    private ToolStripMenuItem editAddItemMenuItem;
    private ToolStripMenuItem editRemoveItemMenuItem;
    private ToolStripMenuItem editAddCurrencyMenuItem;
    private ToolStripMenuItem editRemoveCurrencyMenuItem;
    private ToolStripMenuItem optionsMenu;
    private ToolStripMenuItem optionsAutoSaveMenuItem;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel totalItemsStatus;
    private ToolStripStatusLabel totalCurrenciesStatus;

        private void InitializeComponent()
        {
            this.inventoryGrid = new DataGridView();
            this.currencyGrid = new DataGridView();
            this.addItemButton = new Button();
            this.removeItemButton = new Button();
            this.addCurrencyButton = new Button();
            this.removeCurrencyButton = new Button();
            this.exportButton = new Button();
            this.importButton = new Button();
            this.menuStrip = new MenuStrip();
            this.fileMenu = new ToolStripMenuItem("File");
            this.fileNewMenuItem = new ToolStripMenuItem("New");
            this.fileImportMenuItem = new ToolStripMenuItem("Import...");
            this.fileExportMenuItem = new ToolStripMenuItem("Export...");
            this.fileExitMenuItem = new ToolStripMenuItem("Exit");
            this.editMenu = new ToolStripMenuItem("Edit");
            this.editAddItemMenuItem = new ToolStripMenuItem("Add Item");
            this.editRemoveItemMenuItem = new ToolStripMenuItem("Remove Item");
            this.editAddCurrencyMenuItem = new ToolStripMenuItem("Add Currency");
            this.editRemoveCurrencyMenuItem = new ToolStripMenuItem("Remove Currency");
            this.optionsMenu = new ToolStripMenuItem("Options");
            this.optionsAutoSaveMenuItem = new ToolStripMenuItem("Auto-save") { CheckOnClick = true };
            this.statusStrip = new StatusStrip();
            this.totalItemsStatus = new ToolStripStatusLabel();
            this.totalCurrenciesStatus = new ToolStripStatusLabel();
            this.SuspendLayout();
            // inventoryGrid
            this.inventoryGrid.Location = new System.Drawing.Point(12, 12);
            this.inventoryGrid.Size = new System.Drawing.Size(560, 220);
            this.inventoryGrid.AllowUserToAddRows = false;
            this.inventoryGrid.AllowUserToDeleteRows = false;
            this.inventoryGrid.ReadOnly = false;
            this.inventoryGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Id", DataPropertyName = "Id", Width = 140 });
            this.inventoryGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "Name", Width = 200 });
            this.inventoryGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Quantity", DataPropertyName = "Quantity", Width = 80 });
            this.inventoryGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tags", DataPropertyName = "Tags", Width = 120 });
            // currencyGrid
            this.currencyGrid.Location = new System.Drawing.Point(12, 260);
            this.currencyGrid.Size = new System.Drawing.Size(560, 160);
            this.currencyGrid.AllowUserToAddRows = false;
            this.currencyGrid.AllowUserToDeleteRows = false;
            this.currencyGrid.ReadOnly = false;
            this.currencyGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Code", DataPropertyName = "Code", Width = 120 });
            this.currencyGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "Name", Width = 200 });
            this.currencyGrid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Amount", DataPropertyName = "Amount", Width = 100 });
            // buttons
            this.addItemButton.Text = "+ Item";
            this.addItemButton.Location = new System.Drawing.Point(590, 12);
            this.addItemButton.Click += OnAddItem;

            this.removeItemButton.Text = "- Item";
            this.removeItemButton.Location = new System.Drawing.Point(590, 44);
            this.removeItemButton.Click += OnRemoveItem;

            this.addCurrencyButton.Text = "+ Currency";
            this.addCurrencyButton.Location = new System.Drawing.Point(590, 260);
            this.addCurrencyButton.Click += OnAddCurrency;

            this.removeCurrencyButton.Text = "- Currency";
            this.removeCurrencyButton.Location = new System.Drawing.Point(590, 292);
            this.removeCurrencyButton.Click += OnRemoveCurrency;

            this.exportButton.Text = "Export";
            this.exportButton.Location = new System.Drawing.Point(590, 340);
            this.exportButton.Click += OnExport;

            this.importButton.Text = "Import";
            this.importButton.Location = new System.Drawing.Point(590, 372);
            this.importButton.Click += OnImport;

            // menuStrip
            this.menuStrip.Items.AddRange(new ToolStripItem[] { this.fileMenu, this.editMenu, this.optionsMenu });
            // file menu
            this.fileMenu.DropDownItems.AddRange(new ToolStripItem[] { this.fileNewMenuItem, new ToolStripSeparator(), this.fileImportMenuItem, this.fileExportMenuItem, new ToolStripSeparator(), this.fileExitMenuItem });
            this.fileNewMenuItem.Click += OnNew;
            this.fileImportMenuItem.Click += OnImport;
            this.fileExportMenuItem.Click += OnExport;
            this.fileExitMenuItem.Click += (_, __) => this.Close();
            // edit menu
            this.editMenu.DropDownItems.AddRange(new ToolStripItem[] { this.editAddItemMenuItem, this.editRemoveItemMenuItem, new ToolStripSeparator(), this.editAddCurrencyMenuItem, this.editRemoveCurrencyMenuItem });
            this.editAddItemMenuItem.Click += OnAddItem;
            this.editRemoveItemMenuItem.Click += OnRemoveItem;
            this.editAddCurrencyMenuItem.Click += OnAddCurrency;
            this.editRemoveCurrencyMenuItem.Click += OnRemoveCurrency;
            // options menu
            this.optionsMenu.DropDownItems.Add(this.optionsAutoSaveMenuItem);
            this.optionsAutoSaveMenuItem.CheckedChanged += OnAutoSaveToggled;

            // statusStrip
            this.statusStrip.Items.AddRange(new ToolStripItem[] { this.totalItemsStatus, this.totalCurrenciesStatus });
            this.statusStrip.SizingGrip = false;

            // MainForm
            this.ClientSize = new System.Drawing.Size(700, 464);
            this.Text = "Inventory Management System (Local Only)";
            this.MainMenuStrip = this.menuStrip;
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.inventoryGrid);
            this.Controls.Add(this.currencyGrid);
            this.Controls.Add(this.addItemButton);
            this.Controls.Add(this.removeItemButton);
            this.Controls.Add(this.addCurrencyButton);
            this.Controls.Add(this.removeCurrencyButton);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.statusStrip);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
