using Q42.HueApi;
using Q42.HueApi.Models.Groups;
using System.Collections.Generic;
using System.ComponentModel;

namespace senses.Models {
    public class SensesGroup : Group, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SensesGroup(Group group) {
            Id = group.Id;
            Name = group.Name;
            Type = group.Type;
            Class = group.Class;
            ModelId = group.ModelId;
            Lights = group.Lights;
            Action = group.Action;
            State = group.State;
        }

        public string ObservableName {
            get {
                return Name;
            }
            set {
                if (Name != value) {
                    Name = value;
                    NotifyPropertyChanged("ObservableName");
                }
            }
        }

        public List<string> ObservableLights {
            get {
                return Lights;
            }
            set {
                if (Lights != value) {
                    Lights = value;
                    NotifyPropertyChanged("ObservableLights");
                }
            }
        }

        public State ObservableAction {
            get {
                return Action;
            }
            set {
                if (Action != value) {
                    Action = value;
                    NotifyPropertyChanged("ObservableAction");
                }
            }
        }

        public GroupState ObservableState {
            get {
                return State;
            }
            set {
                if (State!=value) {
                    State = value;
                    NotifyPropertyChanged("ObservableState");
                }
            }
        }
    }
}
