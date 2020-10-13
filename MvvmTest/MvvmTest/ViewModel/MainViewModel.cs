using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmTest.Db;
using MvvmTest.Models;
using MvvmTest.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace MvvmTest.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            fakedb = new FakeDb();
            QueryCommand = new RelayCommand(Query);
            ResetCommand = new RelayCommand(() =>
            {
                Search = string.Empty;
                this.Query();
            });
            EditCommand = new RelayCommand<int>(t => Edit(t));
            DelCommand = new RelayCommand<int>(t => Del(t));
            AddCommand = new RelayCommand(Add);
            ExitCommand = new RelayCommand(Exit);
        }

        FakeDb fakedb;
        private string search = string.Empty;
        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<Student> gridModelList;

        public ObservableCollection<Student> GridModelList
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }

        #region Command
        public RelayCommand QueryCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }
        public RelayCommand<int> EditCommand { get; set; }
        public RelayCommand<int> DelCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand ExitCommand { get; set; }
        #endregion

        public void Query()
        {
            var models = fakedb.GetStudentByName(Search);
            GridModelList = new ObservableCollection<Student>();
            if (models != null)
            {
                models.ForEach(arg =>
                {
                    GridModelList.Add(arg);
                });
            }
        }
        
        public void Edit(int id)
        {
            var model = fakedb.GetStudentById(id);
            if(model != null)
            {
                UserView view = new UserView(model);
                var r = view.ShowDialog();
                if (r.Value)
                {
                    var newModel = GridModelList.FirstOrDefault(t => t.Id == model.Id);
                    if(newModel != null)
                    {
                        newModel.Name = model.Name;
                    }
                }

            }
        }

        public void Del(int id)
        {
            var model = fakedb.GetStudentById(id);
            if (model != null)
            {
                var r = MessageBox.Show($"确认删除当前用户:{model.Name}?", "操作提示", MessageBoxButton.OK, MessageBoxImage.Question);
                if(r == MessageBoxResult.OK)
                {
                    fakedb.DelStudent(model.Id);
                    this.Query();
                }
            }
        }

        public void Add()
        {
            Student student = new Student();
            UserView view = new UserView(student);
            var r = view.ShowDialog();
            if (r.Value)
            {
                student.Id = GridModelList.Max(t => t.Id) + 1;
                fakedb.AddStudent(student);
                this.Query();
            }
        }

        public void Exit()
        {
            MessageBox.Show("咋关啊", "操作提示", MessageBoxButton.OK, MessageBoxImage.Question);
        }
    }
}