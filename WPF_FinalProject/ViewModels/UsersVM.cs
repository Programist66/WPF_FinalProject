using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_FinalProject.HelperClasses;
using WPF_FinalProject.model;

namespace WPF_FinalProject.ViewModels
{
    public class UsersVM : BindableBase
    {
        private DBWorker dbworker = new();
        public User currentUser { get; private set; }
        
        public UsersVM() { throw new NotImplementedException(); }

        public UsersVM(User user) 
        {
            currentUser = user;
            MinimazeWindowCommand = new DelegateCommand(MinimazeWindow);
        }

        #region "Binding Propertys"

        private WindowState windowState = WindowState.Normal;
        public WindowState WindowState 
        {
            get => windowState;
            set 
            {
                SetProperty(ref windowState, value);
            }
        }

        public string Surname 
        {
            get { return currentUser.Surname; }
            set 
            {
                currentUser.Surname = value;
                RaisePropertyChanged(nameof(Surname));
            }
        }
        public string Name
        {
            get { return currentUser.Name; }
            set
            {
                currentUser.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public string Patronymic
        {
            get { return currentUser.Patronymic; }
            set
            {
                currentUser.Patronymic = value;
                RaisePropertyChanged(nameof(Patronymic));
            }
        }

        public Department Department 
        {
            get { return currentUser.Department; }
            set 
            {
                currentUser.Department = value;
                RaisePropertyChanged(nameof(Department));
            }
        }

        public Position Position 
        {
            get { return currentUser.Position; }
            set 
            {
                currentUser.Position = value;
                RaisePropertyChanged(nameof(Position));
            }
        }

        public Office Office 
        {
            get { return currentUser.Office; }
            set 
            {
                currentUser.Office = value;
                RaisePropertyChanged(nameof(Office));
            }
        }

        public IReadOnlyList<Department> AllDepartments 
        {
            get 
            {
                return dbworker.GetDepartments()!;
            }
        }
        public IReadOnlyList<Position> AllPositions
        {
            get
            {
                return dbworker.GetPositions()!;
            }
        }
        public IReadOnlyList<Office> AllOffices
        {
            get
            {
                return dbworker.GetOffices()!;
            }
        }
        #endregion

        #region "Windows Command"
        public ICommand MinimazeWindowCommand { get; }
        private void MinimazeWindow()
        {
            WindowState = WindowState.Minimized;
        }
        #endregion
    }
}
