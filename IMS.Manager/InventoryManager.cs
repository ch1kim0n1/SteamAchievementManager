using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace IMS.Manager
{
    internal partial class InventoryManager : Form
    {
        private readonly BindingList<Item> _items = new BindingList<Item>();
        private readonly BindingList<Currency> _currencies = new BindingList<Currency>();
        private readonly string _profileName;
        private readonly Timer _saveTimer = new Timer();

        private string ProfilePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "InventoryManagementSystem", "profiles", _profileName + ".json");

        public InventoryManager(string profileName)
        {
            _profileName = profileName;
            InitializeComponent();
            
            // Configure grids similar to SAM's achievement view
            inventoryListView.View = View.Details;
            inventoryListView.FullRowSelect = true;
            inventoryListView.GridLines = true;
            inventoryListView.CheckBoxes = false;
            inventoryListView.Columns.Add("Item ID", 140);
            inventoryListView.Columns.Add("Name", 200);
            inventoryListView.Columns.Add("Quantity", 80);
            inventoryListView.Columns.Add("Tags", 120);

            currencyListView.View = View.Details;
            currencyListView.FullRowSelect = true;
            currencyListView.GridLines = true;
            currencyListView.CheckBoxes = false;
            currencyListView.Columns.Add("Code", 120);
            currencyListView.Columns.Add("Name", 200);
            currencyListView.Columns.Add("Amount", 100);

            this.Text = $"Inventory Management System - {_profileName}";

            LoadProfile();

            _saveTimer.Interval = 5000; // Auto-save every 5 seconds
            _saveTimer.Tick += (_, __) => SaveProfile();
            _saveTimer.Start();
        }

        private void LoadProfile()
        {
            try
            {
                if (File.Exists(ProfilePath))
                {
                    var json = File.ReadAllText(ProfilePath);
                    var data = JsonConvert.DeserializeObject<SaveState>(json);
                    if (data != null)
                    {
                        _items.Clear();
                        foreach (var item in data.Items) _items.Add(item);
                        _currencies.Clear();
                        foreach (var currency in data.Currencies) _currencies.Add(currency);
                    }
                }
                RefreshViews();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to load profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveProfile()
        {
            try
            {
                var dir = Path.GetDirectoryName(ProfilePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var data = new SaveState
                {
                    Items = _items.ToList(),
                    Currencies = _currencies.ToList(),
                    SavedAtUtc = DateTime.UtcNow
                };

                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(ProfilePath, json);
                
                statusLabel.Text = $"Profile saved at {DateTime.Now:HH:mm:ss}";
            }
            catch
            {
                // Ignore save errors
            }
        }

        private void RefreshViews()
        {
            inventoryListView.Items.Clear();
            foreach (var item in _items)
            {
                var listItem = new ListViewItem(new[] { item.Id, item.Name, item.Quantity.ToString(), item.Tags });
                listItem.Tag = item;
                inventoryListView.Items.Add(listItem);
            }

            currencyListView.Items.Clear();
            foreach (var currency in _currencies)
            {
                var listItem = new ListViewItem(new[] { currency.Code, currency.Name, currency.Amount.ToString() });
                listItem.Tag = currency;
                currencyListView.Items.Add(listItem);
            }

            // Update status similar to SAM
            statusLabel.Text = $"Loaded {_items.Count} items and {_currencies.Count} currencies.";
        }

        private void OnAddItem(object sender, EventArgs e)
        {
            using var dlg = new EditItemDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                var existing = _items.FirstOrDefault(i => i.Id == dlg.ItemId);
                if (existing != null)
                {
                    existing.Quantity += dlg.Quantity;
                }
                else
                {
                    _items.Add(new Item
                    {
                        Id = dlg.ItemId,
                        Name = dlg.ItemName,
                        Quantity = dlg.Quantity,
                        Tags = dlg.Tags
                    });
                }
                RefreshViews();
            }
        }

        private void OnRemoveItem(object sender, EventArgs e)
        {
            if (inventoryListView.SelectedItems.Count > 0)
            {
                var item = (Item)inventoryListView.SelectedItems[0].Tag;
                _items.Remove(item);
                RefreshViews();
            }
        }

        private void OnAddCurrency(object sender, EventArgs e)
        {
            using var dlg = new EditCurrencyDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                var existing = _currencies.FirstOrDefault(c => c.Code == dlg.Code);
                if (existing != null)
                {
                    existing.Amount += dlg.Amount;
                }
                else
                {
                    _currencies.Add(new Currency
                    {
                        Code = dlg.Code,
                        Name = dlg.NameText,
                        Amount = dlg.Amount
                    });
                }
                RefreshViews();
            }
        }

        private void OnRemoveCurrency(object sender, EventArgs e)
        {
            if (currencyListView.SelectedItems.Count > 0)
            {
                var currency = (Currency)currencyListView.SelectedItems[0].Tag;
                _currencies.Remove(currency);
                RefreshViews();
            }
        }

        private void OnReload(object sender, EventArgs e)
        {
            LoadProfile();
        }

        private void OnStore(object sender, EventArgs e)
        {
            SaveProfile();
            MessageBox.Show(this, "Profile saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _saveTimer.Stop();
            SaveProfile();
            base.OnFormClosing(e);
        }

        public class Item : INotifyPropertyChanged
        {
            private string _id = "";
            private string _name = "";
            private int _quantity;
            private string _tags = "";

            public string Id { get => _id; set { _id = value; OnChanged(nameof(Id)); } }
            public string Name { get => _name; set { _name = value; OnChanged(nameof(Name)); } }
            public int Quantity { get => _quantity; set { _quantity = value; OnChanged(nameof(Quantity)); } }
            public string Tags { get => _tags; set { _tags = value; OnChanged(nameof(Tags)); } }

            public event PropertyChangedEventHandler PropertyChanged;
            private void OnChanged(string n) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
        }

        public class Currency : INotifyPropertyChanged
        {
            private string _code = "";
            private string _name = "";
            private long _amount;

            public string Code { get => _code; set { _code = value; OnChanged(nameof(Code)); } }
            public string Name { get => _name; set { _name = value; OnChanged(nameof(Name)); } }
            public long Amount { get => _amount; set { _amount = value; OnChanged(nameof(Amount)); } }

            public event PropertyChangedEventHandler PropertyChanged;
            private void OnChanged(string n) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
        }

        public class SaveState
        {
            public System.Collections.Generic.List<Item> Items { get; set; } = new();
            public System.Collections.Generic.List<Currency> Currencies { get; set; } = new();
            public DateTime SavedAtUtc { get; set; }
        }
    }
}
