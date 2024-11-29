using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_FinalProject.HelperClasses;

namespace WPF_FinalProject.ViewModels
{
    public class MainVM : BindableBase
    {
        private object currentView;
        private DBWorker dbWorker = new DBWorker();
        private User model;
        public object CurrentView 
        {
            get => currentView; 
            set 
            {
                SetProperty(ref currentView, value);
            } 
        }

        public MainUsersVM MainUsersVM { get; set; }
        public MainDepartmentVM MainDepartmentVM { get; set; }
        public MainPositionVM MainPositionVM { get; set; }
        public MainOfficeVM MainOfficeVM { get; set; }

        private MainWindow mainWindow;

        public MainVM(MainWindow mainWindow, User model) 
        {
            this.mainWindow = mainWindow;
            CloseAppCommand = new DelegateCommand(CloseApp);
            MaximazeWindowCommand = new RelayCommand(MaximazeWindow);
            MinimazeWindowCommand = new DelegateCommand(MinimazeWindow);
            DragMoveCommand = new DelegateCommand(DragMove);
            OpenUsersViewCommand = new DelegateCommand(OpenUsersView);
            OpenDepartmentViewCommand = new DelegateCommand(OpenDepartmentView);
            OpenPositionViewCommand = new DelegateCommand(OpenPositionView);
            OpenOfficeViewCommand = new DelegateCommand(OpenOfficeView);
            this.model = model;
            MainUsersVM = new MainUsersVM(dbWorker!.GetUsers()!);
            MainDepartmentVM = new MainDepartmentVM(dbWorker!.GetDepartments()!);
            MainPositionVM = new MainPositionVM(dbWorker!.GetPositions()!);
            MainOfficeVM = new MainOfficeVM(dbWorker!.GetOffices()!);
            currentView = MainUsersVM;            
        }

        public ICommand CloseAppCommand { get; set; }
        private void CloseApp() 
        {
            Application.Current.Shutdown();
        }

        public ICommand MaximazeWindowCommand { get; set; }
        private void MaximazeWindow(object? param = null) 
        {
            if (param is null)
            {
                mainWindow.WindowState = mainWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
                return;
            }
            MouseButtonEventArgs mouse = (MouseButtonEventArgs)param!;
            if (mouse.ClickCount == 2)
            {
                mainWindow.WindowState = mainWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            }
        }

        public ICommand MinimazeWindowCommand { get; set; }
        private void MinimazeWindow() 
        {
            mainWindow.WindowState = WindowState.Minimized;
        }
        public ICommand DragMoveCommand { get; set; }
        private void DragMove() 
        {
            mainWindow.DragMove();
        }

        public ICommand OpenUsersViewCommand { get; set; }
        private void OpenUsersView ()
        {
            CurrentView = MainUsersVM;
        }
        public ICommand OpenDepartmentViewCommand { get; set; }
        private void OpenDepartmentView()
        {
            CurrentView = MainDepartmentVM;
        }
        public ICommand OpenPositionViewCommand { get; set; }
        private void OpenPositionView()
        {
            CurrentView = MainPositionVM;
        }
        public ICommand OpenOfficeViewCommand { get; set; }
        private void OpenOfficeView()
        {
            CurrentView = MainOfficeVM;
        }
    }
}
