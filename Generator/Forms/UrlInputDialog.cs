using System;
using System.Windows.Forms;

namespace Generator.Forms
{
    public class UrlInputDialog : Form
    {
        private TextBox textBox1;
        private Button button1;

        public UrlInputDialog()
        {
            textBox1 = new TextBox();
            button1 = new Button();

            this.Text = "Enter URL";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(400, 150);

            textBox1.Location = new System.Drawing.Point(30, 30);
            textBox1.Size = new System.Drawing.Size(300, 20);
            textBox1.Text = "http://";

            button1.Text = "OK";
            button1.Location = new System.Drawing.Point(150, 70);
            button1.Click += new EventHandler(button1_Click);

            this.Controls.Add(textBox1);
            this.Controls.Add(button1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string GetUrl()
        {
            return textBox1.Text;
        }
    }
}
