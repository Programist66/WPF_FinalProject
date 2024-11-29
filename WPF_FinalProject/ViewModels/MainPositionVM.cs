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
    public class MainPositionVM
    {
        public ObservableCollection<PositionVM> Positions { get; set; } = [];
        public int SelectedIndex { get; set; }
        private DBWorker dbWorker = new();

        public MainPositionVM(IEnumerable<Position> positions)
        {
            foreach (var item in positions)
            {
                Positions.Add(new PositionVM(item));
            }
            Positions.CollectionChanged += Users_CollectionChanget;
            AddingNewItemCommand = new RelayCommand(AddingNewItem);
            SaveCommand = new DelegateCommand(Save);
            AddCommand = new DelegateCommand(Add);
            ChangeCommand = new DelegateCommand(Change);
        }

        public ICommand AddingNewItemCommand { get; }
        private void AddingNewItem(object? param)
        {
            AddingNewItemEventArgs e = (AddingNewItemEventArgs)param!;
            PositionChangeWindow positionsChangeWindow = new(new PositionVM(new Position(Positions[^1].currentPosition.Id + 1)));
            if (positionsChangeWindow.ShowDialog() == true)
            {
                e.NewItem = (PositionVM)positionsChangeWindow.DataContext;
            }
        }

        public ICommand SaveCommand { get; }
        private void Save()
        {
            foreach (var position in Positions)
            {
                if (CheckValidateUser(position.currentPosition))
                {
                    if (dbWorker.GetPositionById(position.currentPosition.Id) is null)
                    {
                        dbWorker.AddPosition(position.currentPosition);
                    }
                    else
                    {
                        dbWorker.UpdatePosition(position.currentPosition);
                    }
                    MessageBox.Show("Данные сохранены");
                }
            }
        }

        public ICommand AddCommand { get; }
        private void Add()
        {
            PositionChangeWindow positionsChangeWindow = new(new PositionVM(new Position(Positions[^1].currentPosition.Id + 1)));
            if (positionsChangeWindow.ShowDialog() == true)
            {
                Positions.Add((PositionVM)positionsChangeWindow.DataContext);
            }
        }

        public ICommand ChangeCommand { get; }
        private void Change()
        {
            if (SelectedIndex == -1)
            {
                MessageBox.Show("Test");
                return;
            }
            int index = SelectedIndex;
            PositionChangeWindow positionsChangeWindow = new(Positions[index]);

            if (positionsChangeWindow.ShowDialog() == true)
            {
                Positions[index] = (PositionVM)positionsChangeWindow.DataContext;
            }
        }

        private bool CheckValidateUser(Position position)
        {
            if (position.Name == "")
            {
                return false;
            }
            if (position.Salary == 0)
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
                    foreach (PositionVM position in e.OldItems!)
                    {
                        if (dbWorker.GetPositionById(position.currentPosition.Id) is null)
                        {
                            continue;
                        }
                        else
                        {
                            dbWorker.DeletePositionById(position.currentPosition.Id);
                        }
                    }
                    break;
            }
        }
    }
}
