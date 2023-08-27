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

namespace nArchCodeGneratatorExt
{
    /// <summary>
    /// Interaction logic for GeneratorConfigurator.xaml
    /// </summary>
    public partial class GeneratorConfigurator : UserControl
    {
        public GeneratorConfigurator()
        {
            InitializeComponent();
            List<ClassModel> items = new List<ClassModel>();
            items.Add(new ClassModel() { AccessModifier = "public", Type="string",Name="Name"  });
            items.Add(new ClassModel() { AccessModifier = "public", Type="string",Name="LastName"  });
            items.Add(new ClassModel() { AccessModifier = "public", Type="string",Name="FullName"  });
           // lvUsers.ItemsSource = items;
        }
    }
    public class ClassModel
    {
        public string AccessModifier { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
