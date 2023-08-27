using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ClassModel> ClassModelList;
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            EditCommand = new RelayCommand(EditClass);
            DeleteCommand = new RelayCommand(DeleteClass);
            ClassModelList = ClassService.Instance.ClassModelList;
            listView.ItemsSource = ClassService.Instance.ClassModelList; 
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void eye_button_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem accessModifier = accessModifiers.SelectedItem as ComboBoxItem;
            string accessModifierValue = accessModifier?.Content.ToString();

            ComboBoxItem type = types.SelectedItem as ComboBoxItem;
            string typeValue = type?.Content.ToString();

            string nameValue = propertyName.Text;

            if(!string.IsNullOrEmpty(accessModifierValue) 
            && !string.IsNullOrEmpty(nameValue))
            {
                
                //ClassModelList.Add(new ClassModel() { Id = Guid.NewGuid(), AccessModifier = accessModifierValue, Name = nameValue });
                ClassModelList = ClassService.Instance.AddClass(accessModifierValue, nameValue);
            }
            else
            {
                MessageBox.Show("AccessModifier, Type or Name cannot be null");
            }
        }
        private void EditClass(object parameter)
        {
            if (parameter is Guid classId)
            {
                AddPropertyViewModal editModal = new AddPropertyViewModal(classId);
                editModal.ShowDialog();
            }
        }
        private void DeleteClass(object parameter)
        {
            if (parameter is Guid productId)
            {
                ClassService.Instance.RemoveClass(productId);
            }
        }
    
    }
}


