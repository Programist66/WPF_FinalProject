using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_FinalProject.HelperClasses;
using WPF_FinalProject.Windows;

namespace WPF_FinalProject.ViewModels
{
    public class MainUsersVM
    {
        public ObservableCollection<UsersVM> Users {  get; set; } = [];
        public int SelectedIndex { get; set; }
        public UsersVM SelectedItem { get; set; } = null;
        private DBWorker dbWorker = new();

        public MainUsersVM(IEnumerable<User> users) 
        {
            foreach (var item in users)
            {
                Users.Add(new UsersVM(item));
            }
            Users.CollectionChanged += Users_CollectionChanget;
            AddingNewItemCommand = new RelayCommand(AddingNewItem);
            SaveCommand = new DelegateCommand(Save);
            AddCommand = new DelegateCommand(Add);
            ChangeCommand = new DelegateCommand(Change);
        }

        public ICommand AddingNewItemCommand { get; }
        private void AddingNewItem(object? param)
        {
            AddingNewItemEventArgs e = (AddingNewItemEventArgs)param!;
            UsersChangeWindow usersChangeWindow = new(new UsersVM(new User(Users[^1].currentUser.Id + 1)));
            if (usersChangeWindow.ShowDialog() == true)
            {
                e.NewItem = (UsersVM)usersChangeWindow.DataContext;
            }
        }

        public ICommand SaveCommand { get; }
        private void Save() 
        {
            foreach (var user in Users) 
            {
                if (CheckValidateUser(user.currentUser))
                {
                    if (dbWorker.GetUserById(user.currentUser.Id) is null)
                    {
                        dbWorker.AddUser(user.currentUser);
                    }
                    else 
                    {
                        dbWorker.UpdateUser(user.currentUser);
                    }                    
                }
            }
            MessageBox.Show("Данные сохранены");
        }

        public ICommand AddCommand { get; }
        private void Add() 
        {
            UsersChangeWindow usersChangeWindow = new(new UsersVM(new User(Users[^1].currentUser.Id + 1)));
            if (usersChangeWindow.ShowDialog() == true)
            {
                Users.Add((UsersVM)usersChangeWindow.DataContext);
            }
        }
            
        public ICommand ChangeCommand { get; }
        private void Change() 
        {
            if (SelectedItem is null)
            {
                return;
            }
            int index = SelectedIndex;
            UsersChangeWindow usersChangeWindow = new(Users[index]);
            
            if (usersChangeWindow.ShowDialog() == true)
            {

                Users[index] = (UsersVM)usersChangeWindow.DataContext;
            }
        }

        private bool CheckValidateUser(User user) 
        {
            if (user.Surname == "")
            {
                return false;
            }
            if (user.Name == "")
            {
                return false;
            }
            if (user.Patronymic == "")
            {
                return false;
            }
            if (user.Department is null)
            {
                return false;
            }
            if (user.Position is null)
            {
                return false;
            }
            if (user.Office is null)
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
                    foreach (UsersVM user in e.OldItems!)
                    {
                        if (dbWorker.GetUserById(user.currentUser.Id) is null)
                        {
                            continue;
                        }
                        else
                        {
                            dbWorker.DeleteUserById(user.currentUser.Id);
                        }
                    }
                    break;
            }
        }
    }
}
