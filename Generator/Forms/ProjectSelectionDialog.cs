using System;
using System.Windows.Forms;

namespace Generator.Forms
{
    public class ProjectSelectionDialog : Form
    {
        private ComboBox comboBox1;
        private Button button1;

        public ProjectSelectionDialog(string[] projectNames)
        {
            comboBox1 = new ComboBox();
            button1 = new Button();

            this.Text = "Select a Project";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(400, 150);

            comboBox1.Location = new System.Drawing.Point(30, 30);
            comboBox1.Size = new System.Drawing.Size(300, 20);
            comboBox1.Items.AddRange(projectNames);

            button1.Text = "OK";
            button1.Location = new System.Drawing.Point(150, 70);
            button1.Click += new EventHandler(button1_Click);

            this.Controls.Add(comboBox1);
            this.Controls.Add(button1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string GetSelectedProject()
        {
            return comboBox1.SelectedItem.ToString();
        }
    }
}
