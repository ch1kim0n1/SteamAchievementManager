using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace InventoryManagementSystem
{
    public partial class MainForm : Form
    {
        private readonly BindingList<Item> _items = new BindingList<Item>();
        private readonly BindingList<Currency> _currencies = new BindingList<Currency>();
    private bool _autoSave = false;
    private string AutoSavePath => System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "InventoryManagementSystem", "autosave.json");

        public MainForm()
        {
            InitializeComponent();
            inventoryGrid.AutoGenerateColumns = false;
            currencyGrid.AutoGenerateColumns = false;

            inventoryGrid.DataSource = _items;
            currencyGrid.DataSource = _currencies;

            // Seed example data
            _items.Add(new Item { Id = "health_potion", Name = "Health Potion", Quantity = 3, Tags = "consumable"});
            _items.Add(new Item { Id = "iron_sword", Name = "Iron Sword", Quantity = 1, Tags = "weapon"});
            _currencies.Add(new Currency { Code = "GOLD", Name = "Gold", Amount = 1500 });
            _currencies.Add(new Currency { Code = "GEMS", Name = "Gems", Amount = 12 });

            inventoryGrid.CellDoubleClick += (_, __) => OnAddItem(this, EventArgs.Empty);
            currencyGrid.CellDoubleClick += (_, __) => OnAddCurrency(this, EventArgs.Empty);

            UpdateStatus();

            TryAutoLoad();
        }

    private void OnAddItem(object sender, EventArgs e)
        {
            using var dlg = new EditItemDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
        AddOrUpdate(
            _items,
            match: i => i.Id == dlg.ItemId,
            createNew: () => new Item { Id = dlg.ItemId, Name = dlg.ItemName, Quantity = dlg.Quantity, Tags = dlg.Tags },
            updateExisting: i => i.Quantity += dlg.Quantity,
            refreshGrid: () => inventoryGrid.Refresh());
                UpdateStatus();
                AutoSaveIfEnabled();
            }
        }

        private void OnRemoveItem(object sender, EventArgs e)
        {
            if (inventoryGrid.CurrentRow?.DataBoundItem is Item item)
            {
                _items.Remove(item);
                UpdateStatus();
                AutoSaveIfEnabled();
            }
        }

    private void OnAddCurrency(object sender, EventArgs e)
        {
            using var dlg = new EditCurrencyDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
        AddOrUpdate(
            _currencies,
            match: c => c.Code == dlg.Code,
            createNew: () => new Currency { Code = dlg.Code, Name = dlg.NameText, Amount = dlg.Amount },
            updateExisting: c => c.Amount += dlg.Amount,
            refreshGrid: () => currencyGrid.Refresh());
                UpdateStatus();
                AutoSaveIfEnabled();
            }
        }

        private void OnRemoveCurrency(object sender, EventArgs e)
        {
            if (currencyGrid.CurrentRow?.DataBoundItem is Currency c)
            {
                _currencies.Remove(c);
                UpdateStatus();
                AutoSaveIfEnabled();
            }
        }

    private void OnExport(object sender, EventArgs e)
        {
            using var dlg = new SaveFileDialog
            {
                Filter = "JSON Files (*.json)|*.json",
                FileName = "inventory.json"
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                var state = new SaveState
                {
                    Items = _items.ToList(),
                    Currencies = _currencies.ToList(),
                    SavedAtUtc = DateTime.UtcNow
                };
        var json = JsonConvert.SerializeObject(state, Formatting.Indented);
        System.IO.File.WriteAllText(dlg.FileName, json);
                MessageBox.Show(this, "Exported.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    private void OnImport(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog { Filter = "JSON Files (*.json)|*.json" };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    var json = System.IO.File.ReadAllText(dlg.FileName);
            var state = JsonConvert.DeserializeObject<SaveState>(json);
                    if (state != null)
                    {
                        _items.Clear();
                        foreach (var it in state.Items) _items.Add(it);
                        _currencies.Clear();
                        foreach (var cu in state.Currencies) _currencies.Add(cu);
                        UpdateStatus();
                        AutoSaveIfEnabled();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Failed to import: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OnNew(object sender, EventArgs e)
        {
            _items.Clear();
            _currencies.Clear();
            UpdateStatus();
            AutoSaveIfEnabled();
        }

        private void OnAutoSaveToggled(object sender, EventArgs e)
        {
            _autoSave = optionsAutoSaveMenuItem.Checked;
            if (_autoSave)
            {
                AutoSaveIfEnabled();
            }
        }

        private void UpdateStatus()
        {
            totalItemsStatus.Text = $"Items: {_items.Sum(i => i.Quantity)}";
            totalCurrenciesStatus.Text = $"Currencies: {_currencies.Count}";
        }

        private void AutoSaveIfEnabled()
        {
            if (!_autoSave) return;
            try
            {
                var dir = System.IO.Path.GetDirectoryName(AutoSavePath);
                if (!string.IsNullOrEmpty(dir) && !System.IO.Directory.Exists(dir)) System.IO.Directory.CreateDirectory(dir);
                var state = new SaveState { Items = _items.ToList(), Currencies = _currencies.ToList(), SavedAtUtc = DateTime.UtcNow };
                var json = JsonConvert.SerializeObject(state, Formatting.Indented);
                System.IO.File.WriteAllText(AutoSavePath, json);
            }
            catch { /* ignore autosave errors */ }
        }

        private void TryAutoLoad()
        {
            try
            {
                if (System.IO.File.Exists(AutoSavePath))
                {
                    var json = System.IO.File.ReadAllText(AutoSavePath);
                    var state = JsonConvert.DeserializeObject<SaveState>(json);
                    if (state != null)
                    {
                        _items.Clear();
                        foreach (var it in state.Items) _items.Add(it);
                        _currencies.Clear();
                        foreach (var cu in state.Currencies) _currencies.Add(cu);
                        UpdateStatus();
                    }
                }
            }
            catch { /* ignore autoload errors */ }
        }

        private static void AddOrUpdate<T>(BindingList<T> list, Func<T, bool> match, Func<T> createNew, Action<T> updateExisting, Action refreshGrid)
        {
            var existing = list.FirstOrDefault(match);
            if (existing != null)
            {
                updateExisting(existing);
                refreshGrid();
            }
            else
            {
                list.Add(createNew());
            }
        }
    }
}
