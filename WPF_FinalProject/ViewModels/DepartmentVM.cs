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
    public class DepartmentVM : BindableBase
    {
        private DBWorker dbworker = new();
        public Department currentDepartment { get; private set; }

        public DepartmentVM() { throw new NotImplementedException(); }

        public DepartmentVM(Department department)
        {
            currentDepartment = department;
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
            get { return currentDepartment.Name; }
            set
            {
                currentDepartment.Name = value;
                RaisePropertyChanged(nameof(Name));
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
