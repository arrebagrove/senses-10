using Q42.HueApi;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace senses.Models {
    public class SensesLight : Light, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SensesLight(Light light) {
            Id                  = light.Id;
            State               = light.State;
            Type                = light.Type;
            Name                = light.Name;
            ModelId             = light.ModelId;
            ProductId           = light.ProductId;
            SwConfigId          = light.SwConfigId;
            UniqueId            = light.UniqueId;
            LuminaireUniqueId   = light.LuminaireUniqueId;
            ManufacturerName    = light.ManufacturerName;
            SoftwareVersion     = SoftwareVersion;
        }

        public bool IsON {
            get {
                return State.On;
            }
            set {
                if (State.On != value) {
                    State.On = value;
                    NotifyPropertyChanged("IsON");
                }
            }
        }

        public State ObservableState {
            get {
                return State;
            }
            set {
                if (State != value) {
                    State = value;
                    NotifyPropertyChanged("ObservableState");
                }
            }
        }

        public string ObservableName {
            get {
                return Name;
            }
            set {
                if (Name!=value) {
                    Name = value;
                    NotifyPropertyChanged("ObservableName");
                }
            }
        }
    }
}
