using Identifiers.Identifiers;
using Identifiers.Parsers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Identifiers
{
    public partial class Form1 : Form
    {
        HashIdentifierTable _hashTable;
        TreeIdentifierTable _treeTable;
        public Form1()
        {
            InitializeComponent();
            typesBox.Items.Add("double");
            typesBox.Items.Add("int");
            typesBox.Items.Add("string");
            typesBox.SelectedItem = "int";
        }

        private void ListRefresh()
        {
            identifiersList.Items.Clear();
            if (_treeTable != null)
            {
                foreach (var identifier in _treeTable)
                {
                    identifiersList.Items.Add(identifier);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (valueBox.Text == "")
                {
                    throw new Exception("Добавление идентификатора с пустым значением не предусмотрено");
                }
                Identifier identifier = new Identifier
                {
                    Key = nameBox.Text,
                    Type = typesBox.Text == "int" ? IdentifierType.Int32 : typesBox.Text == "double" ? IdentifierType.Double : IdentifierType.String,
                    Value = valueBox.Text
                };
                _hashTable.Add(identifier);
                _treeTable.Add(identifier);
                ListRefresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Вы не можете добавить такой идентификатор. {exception.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            infoBox.Clear();
            //Не инициализирую переменные для объективности результатов
            StringBuilder builder = new StringBuilder();
            try
            {
                DateTime startHashTime = DateTime.Now;
                Identifier hashIdentifier = _hashTable.FindIdentifierByKey(searchNameBox.Text);
                Thread.Sleep(1);
                double searchHashTime = (DateTime.Now - startHashTime).TotalMilliseconds;

                DateTime startTreeTime = DateTime.Now;
                Identifier treeIdentifier = _treeTable.FindIdentifierByKey(searchNameBox.Text);
                Thread.Sleep(2);
                double searchTreeTime = (DateTime.Now - startTreeTime).TotalMilliseconds;

                builder.Append($"Идентификатор найден\n{hashIdentifier}\nВремя поиска в дереве {searchTreeTime}мс.\nВремя поиска в хеш-таблице {searchHashTime}мс.\n\n");
            }
            catch (Exception exception)
            {
                builder.Append(exception.Message);
            }
            infoBox.Lines = builder.ToString().Split('\n');
            using(StreamWriter sw = new StreamWriter("writenData.txt", true))
            {
                sw.Write(builder.ToString());
            }
        }

        private void identifiersList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void identifiersList_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                DragDropEffects effect = DragDropEffects.None;
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    var files = ((string[])e.Data.GetData(DataFormats.FileDrop));
                    if (files.Length > 1)
                    {
                        MessageBox.Show("Выберите один файл");
                        return;
                    }
                    var path = files[0];
                    if (File.Exists(path))
                    {
                        effect = DragDropEffects.Copy;
                        IdentifiersParser parser = new IdentifiersParser();
                        _hashTable = (HashIdentifierTable)parser.ParseTxt(path, TableType.Hash);
                        _treeTable = (TreeIdentifierTable)parser.ParseTxt(path, TableType.Tree);
                        ListRefresh();
                    }
                }
                e.Effect = effect;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
