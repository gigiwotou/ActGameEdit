using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace seesharp
{
    public partial class Form1 : Form
    {
        bool mShowLines = false;
        Pen p = new Pen(Color.Black);
        Image image;
        float width { set; get; }
        float height { set; get; }
        MyImage myImage = new MyImage();

        public Form1()
        {
            InitializeComponent();
            InitMyForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    image = Image.FromFile(openFileDialog1.FileName);
                    textBox1.Text = openFileDialog1.FileName;
                    //string n = openFileDialog1.SafeFileName;
                    string name = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.SafeFileName);
                    listBox1.Items.Add(name);
                    MyImage.ImageInfo info = new MyImage.ImageInfo();
                    info.name = name;
                    info.filename = name;

                    myImage.add(info);
                    System.Drawing.Imaging.ImageFormat type = image.RawFormat;
                    imageShow1.BackgroundImage = image;
                }
                catch
                {
                    MessageBox.Show("打开的不是图片文件或不支持的格式", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                }
            }
        }

        private void InitMyForm()
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            if (File.Exists(Application.StartupPath + "\\TEST.xml"))
                LoadFromFile(Application.StartupPath + "\\TEST.xml");

            foreach (MyImage.ImageInfo prime in myImage.ObjectTypes) // Loop through List with foreach
            {
                listBox1.Items.Add(prime.name);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mShowLines = !mShowLines;
            MyImage.ImageInfo info = myImage.find(listBox1.SelectedIndex);
            if (info == null)
                return;
            info.row = (int)numericUpDown2.Value;
            info.col = (int)numericUpDown1.Value;
            imageShow1.Invalidate();
        }

        private void imageShow1_Paint(object sender, PaintEventArgs e)
        {
            float row = (float)numericUpDown2.Value;
            float column = (float)numericUpDown1.Value;
            if (row == 0 || column == 0 || image == null)
                return;

            width = image.Width;
            height = image.Height;
            imageShow1.Width = (int)width + 5;
            imageShow1.Height = (int)height + 5;

            for (int i = 0; i < column + 1; i++)
            {
                int x1 = i * (int)(width / column);
                int y1 = 0;
                int x2 = x1;
                int y2 = (int)height;
                e.Graphics.DrawLine(p, new Point(x1, y1), new Point(x2, y2));
            }
            for (int j = 0; j < row + 1; j++)
            {
                int x1 = 0;
                int y1 = j * (int)(height / row);
                int x2 = (int)width;
                int y2 = y1;
                e.Graphics.DrawLine(p, new Point(x1, y1), new Point(x2, y2));
            }
        }

        private void SaveToFile(string fileName)
        {
            try
            {
                FileStream stream = File.Open(fileName, FileMode.Create, FileAccess.Write);
                XmlSerializer serializer = new XmlSerializer(typeof(MyImage));
                XmlTextWriter writer = new XmlTextWriter(stream, Encoding.GetEncoding("gb2312"));
                writer.Formatting = Formatting.Indented;

                serializer.Serialize(writer, myImage);
                writer.Close();
                stream.Close();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        
        }

        private void LoadFromFile(string fileName)
        {
            FileStream stream = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(MyImage));
                stream = File.OpenRead(fileName);
                XmlReader reader = XmlReader.Create(stream);
                myImage = (MyImage)serializer.Deserialize(reader);
                stream.Close();
            }
            catch (InvalidOperationException ex)
            {
                if (stream != null)
                    stream.Close();

                //MessageBox.Show("错误：" + ex.Message + "\n无法加载数据，尝试转换操作！！！", Settings.Default.EditorTitle);
                //myImage = GfxHelper.ReloadFromOldData(fileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveToFile(Application.StartupPath + "\\TEST.xml");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0 && listBox1.SelectedIndex > -1)
            {
                string n = listBox1.SelectedItem.ToString();
                myImage.del(listBox1.SelectedIndex);
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0 && listBox1.SelectedIndex > -1)
            {
                string n = listBox1.SelectedItem.ToString();
                Image img = (System.Drawing.Bitmap)seesharp.Properties.Resources.ResourceManager.GetObject(n);
                if (img == null)
                    return;
                width = img.Width;
                height = img.Height;
                imageShow1.Width = (int)width + 5;
                imageShow1.Height = (int)height + 5;
                image = img;
                imageShow1.BackgroundImage = image;

                MyImage.ImageInfo info = myImage.find(listBox1.SelectedIndex);
                if (info != null)
                {
                    numericUpDown2.Value = info.row;
                    numericUpDown1.Value = info.col;
                }
            }
        }
    }
}
