using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using WPF_FinalProject.HelperClasses;
using WPF_FinalProject.Windows;
using WPF_FinalProject.model;

namespace WPF_FinalProject.ViewModels
{
    public class MainDepartmentVM
    {
        public ObservableCollection<DepartmentVM> Departments { get; set; } = [];
        public int SelectedIndex { get; set; }
        private DBWorker dbWorker = new();

        public MainDepartmentVM(IEnumerable<Department> departments)
        {
            foreach (var item in departments)
            {
                Departments.Add(new DepartmentVM(item));
            }
            Departments.CollectionChanged += Users_CollectionChanget;
            AddingNewItemCommand = new RelayCommand(AddingNewItem);
            SaveCommand = new DelegateCommand(Save);
            AddCommand = new DelegateCommand(Add);
            ChangeCommand = new DelegateCommand(Change);
        }

        public ICommand AddingNewItemCommand { get; }
        private void AddingNewItem(object? param)
        {
            AddingNewItemEventArgs e = (AddingNewItemEventArgs)param!;
            DepartmentChangeWindow departmentChangeWindow = new(new DepartmentVM(new Department(Departments[^1].currentDepartment.Id + 1)));
            if (departmentChangeWindow.ShowDialog() == true)
            {
                e.NewItem = (DepartmentVM)departmentChangeWindow.DataContext;
            }
        }

        public ICommand SaveCommand { get; }
        private void Save()
        {
            foreach (var department in Departments)
            {
                if (CheckValidateDepartments(department.currentDepartment))
                {
                    if (dbWorker.GetDepartmentById(department.currentDepartment.Id) is null)
                    {
                        dbWorker.AddDepartment(department.currentDepartment);
                    }
                    else
                    {
                        dbWorker.UpdateDepartment(department.currentDepartment);
                    }                    
                }
            }
            MessageBox.Show("Данные сохранены");
        }

        public ICommand AddCommand { get; }
        private void Add()
        {
            DepartmentChangeWindow departmentChangeWindow = new(new DepartmentVM(new Department(Departments[^1].currentDepartment.Id + 1)));
            if (departmentChangeWindow.ShowDialog() == true)
            {
                Departments.Add((DepartmentVM)departmentChangeWindow.DataContext);
            }
        }

        public ICommand ChangeCommand { get; }
        private void Change()
        {
            if (SelectedIndex == -1)
            {
                return;
            }
            int index = SelectedIndex;
            DepartmentChangeWindow departmentChangeWindow = new(Departments[index]);

            if (departmentChangeWindow.ShowDialog() == true)
            {
                Departments[index] = (DepartmentVM)departmentChangeWindow.DataContext;
            }
        }

        private bool CheckValidateDepartments(Department department)
        {
            if (department.Name == "")
            {
                return false;
            }
            return true;
        }

        private void Users_CollectionChanget(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    foreach (DepartmentVM department in e.OldItems!)
                    {
                        if (dbWorker.GetDepartmentById(department.currentDepartment.Id) is null)
                        {
                            continue;
                        }
                        else
                        {
                            dbWorker.DeleteDepartmentById(department.currentDepartment.Id);
                        }
                    }
                    break;
            }
        }
    }
}
