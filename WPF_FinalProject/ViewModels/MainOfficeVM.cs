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
    public class MainOfficeVM
    {
        public ObservableCollection<OfficeVM> Offices { get; set; } = [];
        public int SelectedIndex { get; set; }
        private DBWorker dbWorker = new();

        public MainOfficeVM(IEnumerable<Office> offices)
        {
            foreach (var item in offices)
            {
                Offices.Add(new OfficeVM(item));
            }
            Offices.CollectionChanged += Users_CollectionChanget;
            AddingNewItemCommand = new RelayCommand(AddingNewItem);
            SaveCommand = new DelegateCommand(Save);
            AddCommand = new DelegateCommand(Add);
            ChangeCommand = new DelegateCommand(Change);
        }

        public ICommand AddingNewItemCommand { get; }
        private void AddingNewItem(object? param)
        {
            AddingNewItemEventArgs e = (AddingNewItemEventArgs)param!;
            OfficeChangeWindow officesChangeWindow = new(new OfficeVM(new Office(Offices[^1].currentOffice.Id + 1)));
            if (officesChangeWindow.ShowDialog() == true)
            {
                e.NewItem = (OfficeVM)officesChangeWindow.DataContext;
            }
        }

        public ICommand SaveCommand { get; }
        private void Save()
        {
            foreach (var office in Offices)
            {
                if (CheckValidateUser(office.currentOffice))
                {
                    if (dbWorker.GetOfficeById(office.currentOffice.Id) is null)
                    {
                        dbWorker.AddOffice(office.currentOffice);
                    }
                    else
                    {
                        dbWorker.UpdateOffice(office.currentOffice);
                    }
                }
            }
            MessageBox.Show("Данные сохранены");
        }

        public ICommand AddCommand { get; }
        private void Add()
        {
            OfficeChangeWindow officesChangeWindow = new(new OfficeVM(new Office(Offices[^1].currentOffice.Id + 1)));
            if (officesChangeWindow.ShowDialog() == true)
            {
                Offices.Add((OfficeVM)officesChangeWindow.DataContext);
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
            OfficeChangeWindow officesChangeWindow = new(Offices[index]);

            if (officesChangeWindow.ShowDialog() == true)
            {
                Offices[index] = (OfficeVM)officesChangeWindow.DataContext;
            }
        }

        private bool CheckValidateUser(Office office)
        {
            if (office.Name == "")
            {
                return false;
            }
            if (office.Department is null)
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
                    foreach (OfficeVM office in e.OldItems!)
                    {
                        if (dbWorker.GetOfficeById(office.currentOffice.Id) is null)
                        {
                            continue;
                        }
                        else
                        {
                            dbWorker.DeleteOfficeById(office.currentOffice.Id);
                        }
                    }
                    break;
            }
        }
    }
}
