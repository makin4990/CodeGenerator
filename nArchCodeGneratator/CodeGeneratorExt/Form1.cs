using CodeGeneratorExt.Generator;
using CodeGeneratorExt.Generator.Factories;
using CodeGeneratorExt.Models;
using CodeGeneratorExt.Services;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace CodeGeneratorExt
{
    public partial class Form1 : Form
    {
        //public List<ClassModel> ClassModelList;
        public List<string> NameSpaces = new List<string>();
        public Form1()
        {
            InitializeComponent();
            //ClassModelList = new();
            //metroGrid1.DataSource = ClassModelList;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string accessModifier = metroComboBox1.SelectedItem.ToString();
            string nameValue = metroTextBox1.Text;

            if (!string.IsNullOrEmpty(accessModifier) && !string.IsNullOrEmpty(nameValue))
            {
                metroGrid1.Columns.Add("Id", "Id");
                metroGrid1.Columns.Add("AccessModifier", "Access Modifier");
                metroGrid1.Columns.Add("Name", "Name");

                var  XClassModelList = ClassService.Instance.AddClass(accessModifier, nameValue);
                metroGrid1.Rows.Add(XClassModelList.Id, accessModifier,nameValue);

            }
            else
            {
                System.Windows.MessageBox.Show("AccessModifier, Type or Name cannot be null");
            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the clicked row's ID (assuming it's in the first column)
                string rowId = metroGrid1.Rows[e.RowIndex].Cells[0].Value.ToString();

                // Open the new form and pass the row ID
                var newForm = new PropertyForm(rowId);
                newForm.ShowDialog(); // Show the form as a dialog
            }

        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            var basePath = VS.Solutions.GetCurrentSolution().FullPath.Split('.').First();

            var projects = VS.Solutions.GetAllProjectsAsync().Result.Where(i=> !i.Text.StartsWith("Core"));

            var domainPath = projects.First(i => i.Text.EndsWith("Domain")).FullPath.Split('.').First();
            var applicaitonPath = projects.First(i => i.Text.EndsWith("Application")).FullPath.Split('.').First();
            var persistencePath = projects.First(i => i.Text.EndsWith("Persistence")).FullPath.Split('.').First();
            var webApiPath = projects.First(i => i.Text.EndsWith("WebAPI")).FullPath.Split('.').First();

            var classModelList = ClassService.Instance.ClassModelList.ToList();


            ICodeGeneratorFactory ApiFactory = new ApiFactory();
            Generator.Generator ApiGenerator = ApiFactory.Generate(classModelList, domainPath);
            ApiGenerator.Generate();

            ICodeGeneratorFactory ApplicationFactory = new ApplicationFactory();
            Generator.Generator ApplicationGenerator = ApplicationFactory.Generate(classModelList, applicaitonPath);
            ApplicationGenerator.Generate();

            ICodeGeneratorFactory DomainFactory = new DomainFactory();
            Generator.Generator DomainGenerator = DomainFactory.Generate(classModelList, basePath);
            DomainGenerator.Generate();

            ICodeGeneratorFactory PersistenceFactory = new PersistenceFactory();
            Generator.Generator PersistenceGenerator = PersistenceFactory.Generate(classModelList, persistencePath);
            PersistenceGenerator.Generate();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
