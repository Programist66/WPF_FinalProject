using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WPF_FinalProject.HelperClasses;
using WPF_FinalProject.model;

namespace WPF_FinalProject.ViewModels
{
    public class OfficeVM : BindableBase
    {
        private DBWorker dbworker = new();
        public Office currentOffice { get; private set; }

        public OfficeVM() { throw new NotImplementedException(); }

        public OfficeVM(Office office)
        {
            currentOffice = office;
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
        public string Name
        {
            get { return currentOffice.Name; }
            set
            {
                currentOffice.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public Department Department
        {
            get { return currentOffice.Department; }
            set
            {
                currentOffice.Department = value;
                RaisePropertyChanged(nameof(Department));
            }
        }

        public IReadOnlyList<Department> AllDepartments
        {
            get
            {
                return dbworker.GetDepartments()!;
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
