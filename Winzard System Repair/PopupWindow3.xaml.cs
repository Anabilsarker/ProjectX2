﻿using System;
using System.Windows;
using System.Windows.Media;
using WPFUI;

namespace Winzard_System_Repair
{
    /// <summary>
    /// Interaction logic for PopupWindow3.xaml
    /// </summary>
    public partial class PopupWindow3 : Window
    {
        public BrowserWindow bwobj;
        bool result = false;
        public PopupWindow3()
        {
            InitializeComponent();
            RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
            problemnum();
        }

        private void no_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void yes_Click(object sender, RoutedEventArgs e)
        {
            BrowserWindow bwObj = new BrowserWindow();
            result = bwObj.ShowDialog().Value;
            Close();
        }

        private void popupclose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = result;
        }

        private void problemnum()
        {
            Random rnd = new Random();
            int issuesnum = rnd.Next(70, 200);
            int itemsnum = rnd.Next(100, 3000);
            int freespacenum = rnd.Next(1, 20);
            issues.Text = issuesnum.ToString();
            items.Text = itemsnum.ToString();
            freespace.Text = freespacenum.ToString();
        }
    }
}
