using CodeGeneratorExt.Generator;
using CodeGeneratorExt.Generator.Factories;
using CodeGeneratorExt.Services;
using System.Linq;
using System.Windows.Forms;

namespace CodeGeneratorExt
{
    public partial class PropertyForm : Form
    {
        private string ClassId { get; set; }

        public PropertyForm()
        {
            InitializeComponent();
        }

        public PropertyForm(string classId)
        {
            InitializeComponent();
            ClassId = classId;
            var @class = ClassService.Instance.GetClassProperties(classId);


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void PropertyForm_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string accessModifier = metroComboBox1.SelectedItem.ToString();
            string  type = metroComboBox2.SelectedItem.ToString();
            string nameValue = metroTextBox1.Text;

            if (!string.IsNullOrEmpty(accessModifier) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(nameValue))
            {
                metroGrid1.Columns.Add("Id", "Id");
                metroGrid1.Columns.Add("AccessModifier", "Access Modifier");
                metroGrid1.Columns.Add("Type", "Type");
                metroGrid1.Columns.Add("Name", "Name");

                var XClassModelList = ClassService.Instance.AddClassProperty(ClassId, accessModifier, type, nameValue);
                metroGrid1.Rows.Add(Guid.NewGuid(), accessModifier, type, nameValue);

            }
            else
            {
                System.Windows.MessageBox.Show("AccessModifier, Type or Name cannot be null");
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            var classModelList = ClassService.Instance.ClassModelList.ToList();
            //ICodeGeneratorFactory DomainFactory = new DomainFactory();
            //Generator.Generator DomainGenerator = DomainFactory.Generate(classModelList);
            //DomainGenerator.Generate();
            ICodeGeneratorFactory PersistenceFactory = new PersistenceFactory();
            Generator.Generator PersistenceGenerator = PersistenceFactory.Generate(classModelList,"");
            PersistenceGenerator.Generate();
        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
