using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace IMS.Picker
{
    public partial class ProfilePicker : Form
    {
        private readonly BindingList<ProfileInfo> _profiles = new BindingList<ProfileInfo>();
        private static string ProfilesPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "InventoryManagementSystem", "profiles");

        public ProfilePicker()
        {
            InitializeComponent();
            profileListView.View = View.Details;
            profileListView.FullRowSelect = true;
            profileListView.GridLines = true;
            profileListView.Columns.Add("Profile Name", 200);
            profileListView.Columns.Add("Last Modified", 150);
            profileListView.Columns.Add("Items", 80);
            profileListView.Columns.Add("Currencies", 80);

            RefreshProfiles();
        }

        private void RefreshProfiles()
        {
            _profiles.Clear();
            profileListView.Items.Clear();

            if (!Directory.Exists(ProfilesPath))
                Directory.CreateDirectory(ProfilesPath);

            var files = Directory.GetFiles(ProfilesPath, "*.json");
            foreach (var file in files)
            {
                try
                {
                    var info = new FileInfo(file);
                    var profileName = Path.GetFileNameWithoutExtension(file);
                    var json = File.ReadAllText(file);
                    var data = JsonConvert.DeserializeObject<SaveState>(json);
                    
                    var profile = new ProfileInfo
                    {
                        Name = profileName,
                        LastModified = info.LastWriteTime,
                        ItemCount = data?.Items?.Count ?? 0,
                        CurrencyCount = data?.Currencies?.Count ?? 0,
                        FilePath = file
                    };

                    _profiles.Add(profile);

                    var item = new ListViewItem(new[] {
                        profile.Name,
                        profile.LastModified.ToString("yyyy-MM-dd HH:mm"),
                        profile.ItemCount.ToString(),
                        profile.CurrencyCount.ToString()
                    });
                    item.Tag = profile;
                    profileListView.Items.Add(item);
                }
                catch
                {
                    // Skip invalid files
                }
            }

            // Add default profile if none exist
            if (_profiles.Count == 0)
            {
                CreateDefaultProfile();
            }
        }

        private void CreateDefaultProfile()
        {
            var defaultData = new SaveState
            {
                Items = new System.Collections.Generic.List<Item>
                {
                    new Item { Id = "health_potion", Name = "Health Potion", Quantity = 5, Tags = "consumable" },
                    new Item { Id = "iron_sword", Name = "Iron Sword", Quantity = 1, Tags = "weapon" }
                },
                Currencies = new System.Collections.Generic.List<Currency>
                {
                    new Currency { Code = "GOLD", Name = "Gold", Amount = 1000 },
                    new Currency { Code = "GEMS", Name = "Gems", Amount = 50 }
                },
                SavedAtUtc = DateTime.UtcNow
            };

            var defaultPath = Path.Combine(ProfilesPath, "Default.json");
            var json = JsonConvert.SerializeObject(defaultData, Formatting.Indented);
            File.WriteAllText(defaultPath, json);
            RefreshProfiles();
        }

        private void OnManage(object sender, EventArgs e)
        {
            if (profileListView.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Please select a profile to manage.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var profile = (ProfileInfo)profileListView.SelectedItems[0].Tag;
            Process.Start("IMS.Manager.exe", $"\"{profile.Name}\"");
        }

        private void OnNewProfile(object sender, EventArgs e)
        {
            using var dlg = new NewProfileDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                var newPath = Path.Combine(ProfilesPath, dlg.ProfileName + ".json");
                if (File.Exists(newPath))
                {
                    MessageBox.Show(this, "A profile with that name already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var newData = new SaveState
                {
                    Items = new System.Collections.Generic.List<Item>(),
                    Currencies = new System.Collections.Generic.List<Currency>(),
                    SavedAtUtc = DateTime.UtcNow
                };

                var json = JsonConvert.SerializeObject(newData, Formatting.Indented);
                File.WriteAllText(newPath, json);
                RefreshProfiles();
            }
        }

        private void OnDeleteProfile(object sender, EventArgs e)
        {
            if (profileListView.SelectedItems.Count == 0) return;

            var profile = (ProfileInfo)profileListView.SelectedItems[0].Tag;
            if (MessageBox.Show(this, $"Delete profile '{profile.Name}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                File.Delete(profile.FilePath);
                RefreshProfiles();
            }
        }

        private void OnRefresh(object sender, EventArgs e)
        {
            RefreshProfiles();
        }

        private void OnDoubleClick(object sender, EventArgs e)
        {
            OnManage(sender, e);
        }

        public class ProfileInfo
        {
            public string Name { get; set; } = "";
            public DateTime LastModified { get; set; }
            public int ItemCount { get; set; }
            public int CurrencyCount { get; set; }
            public string FilePath { get; set; } = "";
        }

        public class Item
        {
            public string Id { get; set; } = "";
            public string Name { get; set; } = "";
            public int Quantity { get; set; }
            public string Tags { get; set; } = "";
        }

        public class Currency
        {
            public string Code { get; set; } = "";
            public string Name { get; set; } = "";
            public long Amount { get; set; }
        }

        public class SaveState
        {
            public System.Collections.Generic.List<Item> Items { get; set; } = new();
            public System.Collections.Generic.List<Currency> Currencies { get; set; } = new();
            public DateTime SavedAtUtc { get; set; }
        }
    }
}
