using System;
using System.Collections.Generic;
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

namespace Klijent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var prozor = new MainWindowViewModel();
            this.DataContext = prozor;
            this.btnInsert.Click += prozor.OnInsert;
            this.btnUpdate.Click += prozor.OnUpdate;
            this.btnDelete.Click += prozor.OnDelete;
            this.InsertPredmet.Click += prozor.OnInsertPredmet;
            this.updatePrdmet.Click += prozor.OnUpdatePredmet;
            this.deletePredmet.Click += prozor.OnDeletePredmet;
        }

       
    }
}
