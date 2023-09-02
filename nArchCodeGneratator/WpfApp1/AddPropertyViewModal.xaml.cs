using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.Services;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AddPropertyViewModal.xaml
    /// </summary>
    public partial class AddPropertyViewModal : Window
    {
        public ObservableCollection<ClassProperty> Properties;
        private Guid ClassId;
        public AddPropertyViewModal(Guid classId)
        {
            InitializeComponent();
            Properties = ClassService.Instance.GetClassProperties(classId);
            ClassId = classId;
            listViewProperty.ItemsSource = Properties;
            DataContext = this;
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

            if (!string.IsNullOrEmpty(accessModifierValue)
            && !string.IsNullOrEmpty(nameValue))
            {

                //ClassModelList.Add(new ClassModel() { Id = Guid.NewGuid(), AccessModifier = accessModifierValue, Name = nameValue });
                Properties = ClassService.Instance.AddClassProperty(ClassId,accessModifierValue, typeValue, nameValue);
            }
            else
            {
                MessageBox.Show("AccessModifier, Type or Name cannot be null");
            }
        }
    }
}
