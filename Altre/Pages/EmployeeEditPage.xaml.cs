﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Altre.AppData;
using Microsoft.Win32;

namespace Altre.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditPage.xaml
    /// </summary>
    public partial class EmployeeEditPage : Page
    {
        bool checkNew;
        string pathImage;
        static Employee employee;
        public EmployeeEditPage(Employee empl)
        {
            InitializeComponent();
            if (empl == null)
            {
                empl = new Employee();
                checkNew = true;
            }
            else
                checkNew = false;
            DataContext = employee = empl;
        }

        private void ClearImageBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                employee.photo = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
        }

        private void ImagePathBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
                if (dialog.ShowDialog().GetValueOrDefault(false))
                {
                    pathImage = dialog.FileName;
                }
                ImageBox.Source = new BitmapImage(new Uri(pathImage));
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pathImage != null && pathImage.Trim() != "")
                {
                    employee.photo = File.ReadAllBytes(pathImage);
                }
            }
            catch (Exception ex) 
            {
                employee.photo = null;
                MessageBox.Show(ex.Message);
            }

            if (employee.employee_id == 0) 
            {
                ConnectionDB.GetCont().Employee.Add(employee);
            }
            try
            {
                ConnectionDB.GetCont().SaveChanges();
                if (Nav.MFrame.CanGoBack)
                    Nav.MFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
