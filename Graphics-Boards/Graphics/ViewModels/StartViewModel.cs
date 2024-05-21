
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Graphics.UC;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MenuItem = Graphics.Helpers.MenuItem;


namespace Graphics.ViewModels
{
    public partial class StartViewModel : ObservableObject
    {

        [ObservableProperty]
        private bool _isOpen;

        [ObservableProperty]
        private ObservableCollection<MenuItem> _menuOptions;
        [ObservableProperty]
        private string _title;

        private MenuItem _selectedItem;
        private UserControl _selectedUserControl;

        public MenuItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                SelectedUserControl = (UserControl)Activator.CreateInstance(_selectedItem.UserControlType);
                Title = _selectedItem.Title;
                IsOpen = false;
            }
        }

        public UserControl SelectedUserControl
        {
            get { return _selectedUserControl; }
            set { SetProperty(ref _selectedUserControl, value); }
        }


        public StartViewModel()
        {
            IsOpen = false;
            Title = "ANÁLISIS DE REPORTES";
            MenuLinks();

        }
         //METHODS
        private void MenuLinks()
        {
            MenuOptions = new ObservableCollection<MenuItem>()
            {
               new MenuItem("QUEUES","ViewDashboard","ANÁLISIS DE REPORTES EN QUEUES",typeof(QueuesControl)),
               new MenuItem("USUARIOS","AccountGroup","ANÁLISIS DE REPORTES EN USUARIOS",typeof(UsersControl)),
            };
            SelectedItem = MenuOptions[0];
        }

        //COMMANDS
        [RelayCommand]
        private void ToggleMenu()
        {
            IsOpen = IsOpen ? true : false;
        }
    }
}
