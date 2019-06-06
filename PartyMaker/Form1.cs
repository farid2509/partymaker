using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PartyMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var items = new AddWorker() { Workers = new Workers() };
            if (items.ShowDialog(this) == DialogResult.OK)
            {
                listBox1.Items.Add(items.Workers);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Workers)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog() { Filter = "Праздник|*.party" };

            if (sfd.ShowDialog(this) != DialogResult.OK)
                return;

            var storage = new PartyModel()
            {
                TypeofEvent = textBox1.Text,
                Distination = textBox2.Text,
                Services = textBox3.Text,
                Workers = listBox1.Items.OfType<Workers>().ToList(),
            };


            var xs = new XmlSerializer(typeof(PartyModel));
            var file = File.Create(sfd.FileName);
            xs.Serialize(file, storage);
            file.Close();
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog() { Filter = "Праздник|*.party" };

            if (ofd.ShowDialog(this) != DialogResult.OK)
                return;
            var xs = new XmlSerializer(typeof(PartyModel));
            var file = File.OpenRead(ofd.FileName);
            var party = (PartyModel)xs.Deserialize(file);
            file.Close();

            textBox1.Text = party.TypeofEvent;
            textBox2.Text = party.Distination;
            textBox3.Text = party.Services;

            listBox1.Items.Clear();
            
            foreach (var items in party.Workers)
            {
                listBox1.Items.Add(items);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = listBox1.SelectedItem is Workers;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                var worker = (Workers)listBox1.Items[index];
                var ff = new AddWorker() { Workers = worker };
                if (ff.ShowDialog(this) == DialogResult.OK)
                {
                    listBox1.Items.Remove(worker);
                    listBox1.Items.Insert(index, worker);
                }
            }
        }
    }
}
