﻿using Pmer.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace Pmer.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // 只有当isWindowShow为True时，才进行数据绑定
        public MainWindow(bool isWindowShow, string key)
        {
            InitializeComponent();
            this.MouseDown += (sender, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
            if (isWindowShow)
            {
                MainWindowModel mainWindowModel = new MainWindowModel(key);
                this.DataContext = mainWindowModel;                                
            }
        }

    }
}
