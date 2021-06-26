using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace kursMinin
{
    public partial class Userman
    {
        public string FullName
        {
            get
            {
                return FirstName+LastName+Patronomyc;
            }
        }
        public string FilterRole
        {
            get
            {
                if (Roleid == 4)
                {
                    return "Visible";
                }
                return "Collapsed";
               
                                
            }
        }
    }
}


namespace kursMinin.window
{
    /// <summary>
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window, INotifyPropertyChanged
    {

        public List<Userman> UsermanList { get; set; }

        public ServiceWindow(Zakazy zakazy)
        {
            InitializeComponent();
            this.DataContext = this;
            CurrentOrder = zakazy;
            UsermanList = Core.DB.Userman.ToList();

        }
        public Zakazy CurrentOrder { get; set; }
        public string WindowName
        {
            get
            {
                return CurrentOrder.id == 0 ? "Новая услуга" : "Редоктирование улсгуи";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void GetImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog GetImageDialog = new OpenFileDialog();
            // задаем фильтр для выбираемых файлов
            // до символа "|" идет произвольный текст, а после него шаблоны файлов раздеренные точкой с запятой
            GetImageDialog.Filter = "Файлы изображений: (*.png, *.jpg)|*.png;*.jpg";
            // чтобы не искать по всему диску задаем начальный каталог
            GetImageDialog.InitialDirectory = Environment.CurrentDirectory;
            if (GetImageDialog.ShowDialog() == true)
            {
                // перед присвоением пути к картинке обрезаем начало строки, т.к. диалог возвращает полный путь
                // (тут конечно еще надо проверить есть ли в начале Environment.CurrentDirectory)
                CurrentOrder.Phot = GetImageDialog.FileName.Substring(Environment.CurrentDirectory.Length + 1);
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentService"));
                }
            }
        }

            private void SaveButton_Click(object sender, RoutedEventArgs e)
        {



            {
                if (CurrentOrder.id == 0)
                    Core.DB.Zakazy.Add(CurrentOrder);

                
                try
                {
                    Core.DB.SaveChanges();
                }
                catch
                {
                }
                DialogResult = true;
            }
        
           
           
        }
       
        public string NewProduct
        {
            get
            {
                if (CurrentOrder.id == 0) return "collapsed";
                return "visible";



            }
        }
    }
}