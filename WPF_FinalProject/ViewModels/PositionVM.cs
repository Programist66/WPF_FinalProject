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
    public class PositionVM : BindableBase
    {
        private DBWorker dbworker = new();
        public Position currentPosition { get; private set; }

        public PositionVM() { throw new NotImplementedException(); }

        public PositionVM(Position position)
        {
            currentPosition = position;
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
            get { return currentPosition.Name; }
            set
            {
                currentPosition.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public decimal Salary
        {
            get { return currentPosition.Salary; }
            set
            {
                currentPosition.Salary = value;
                RaisePropertyChanged(nameof(Salary));
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
