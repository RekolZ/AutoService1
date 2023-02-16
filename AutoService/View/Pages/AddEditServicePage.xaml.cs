using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
using AutoService.Models;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;

namespace AutoService.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditServicePage.xaml
    /// </summary>
    public partial class AddEditServicePage : Page
    {
        int imageId;
        private Service _currentService = null;
        private byte[] _mainImageData;
        OpenFileDialog ofd;
        Core db = new Core();
        public AddEditServicePage(Service currentService)
        {
            InitializeComponent();
            //TBoxTitle.Text = _currentService.Title;
            //TBoxCost.Text = _currentService.Cost.ToString();
            //_currentService.DurationInSeconds = TBoxDuration.Text + " мин.";
        }

        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            ofd = new OpenFileDialog();
            ofd.Filter = "Image | *.png; *.jpg; *.jpeg; ";
            if (ofd.ShowDialog() == true)
            {
                _mainImageData = File.ReadAllBytes(ofd.FileName);
                ImageService.Source = (ImageSource)new ImageSourceConverter()
                    .ConvertFrom(_mainImageData);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
           
            if (_currentService == null)
            {
                //отсюда
                
                string name = System.IO.Path.GetFileName(ofd.FileName);

                if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\..\\..\\Assets\\img\\SERVICES\\{System.IO.Path.GetFileName(ofd.FileName)}"))
                {
                    
                    ServicePhoto selectedImage = db.context.ServicePhoto.FirstOrDefault(x => x.PhotoPath.Contains(name));
                    if (selectedImage != null)
                    {
                        imageId = selectedImage.ID;
                    }
                }
                else
                {
                    string newfilename = "\\Assets\\img\\SERVICES\\";
                    string appFolderPath = Directory.GetCurrentDirectory();
                    appFolderPath = appFolderPath.Replace("\\bin\\Debug", "");//обрезанный путь
                    string imageName = System.IO.Path.GetFileName(ofd.FileName);//имя картинки с расширением
                    newfilename = appFolderPath + newfilename + imageName;
                    File.Copy(ofd.FileName, newfilename);
                    ServicePhoto photo = new ServicePhoto
                    {
                        PhotoPath = $"SERVICES/{System.IO.Path.GetFileName(ofd.FileName)}"
                    };
                    db.context.ServicePhoto.Add(photo);
                    // MessageBox.Show("Жесть");
                    db.context.SaveChanges();
                    imageId = photo.ID;
                }
                //досюда
                var service = new Service
                {
                    Title = TBoxTitle.Text,
                    Cost = decimal.Parse(TBoxCost.Text),
                    DurationInSeconds = TBoxDuration.Text + " мин.",
                    Description = TBoxDescription.Text,
                    Discount = string.IsNullOrWhiteSpace(TBoxDiscount.Text)
                    ? 0 : double.Parse(TBoxDiscount.Text) / 100,
                    MainImagePath = imageId
                };
                db.context.Service.Add(service);
                db.context.SaveChanges();
            }

            //редактирование
            else
            {
                _currentService.Title = TBoxTitle.Text;
                _currentService.Cost = decimal.Parse(TBoxCost.Text);
                _currentService.DurationInSeconds = TBoxDuration.Text + " мин.";
                _currentService.Description = TBoxDescription.Text;
                _currentService.Discount = string.IsNullOrWhiteSpace(TBoxDiscount.Text)
                ? 0 : double.Parse(TBoxDiscount.Text) / 100;
                if (_mainImageData != null)
                    _currentService.MainImagePath = Convert.ToInt32(_mainImageData);
                db.context.SaveChanges();
            }
            NavigationService.GoBack();
            
        }
    }
}
