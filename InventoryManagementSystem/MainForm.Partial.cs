using System.ComponentModel;

namespace InventoryManagementSystem
{
    public partial class MainForm
    {
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
            public System.DateTime SavedAtUtc { get; set; }
        }
    }
}
