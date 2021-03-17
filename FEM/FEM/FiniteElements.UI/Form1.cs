using FiniteElements.BLL.Meshes;
using FiniteElements.BLL.Meshes.Surfaces;
using FiniteElements.BLL.Nodes;
using FiniteElements.BLL.Nodes.Surfaces;
using FiniteElements.BLL.Parsers;
using FiniteElements.BLL.Parsers.Surfaces;
using FiniteElements.BLL.SolutionTools;
using FiniteElements.BLL.Solvers;
using FiniteElements.BLL.Solvers.Surfaces;
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

namespace FiniteElements.UI
{
    public partial class MainForm : Form
    {
        Mesh2D _mesh;
        double[] _lastNodesMoves;
        public MainForm()
        {
            InitializeComponent();
            SecondaryInitialization();
        }

        private void SecondaryInitialization()
        {
            nodeTypeBox.SelectedItem = "Без условия";
            angleTypeBox.SelectedItem = "град.";
        }

        private bool CheckMesh()
        {
            bool exist = _mesh != null;
            if (!exist)
            {
                MessageBox.Show("Загрузите сетку");
            }
            return exist;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if(_mesh != null) _mesh.Paint(e.Graphics, e.ClipRectangle);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (!CheckMesh())
                return;
            try
            {
                Node2DInfo state = new Node2DInfo();
                switch (nodeTypeBox.Text)
                {
                    case "Сила":
                        state.NodeType = NodeType.Force;
                        state.ForceValue = double.Parse(forceValueBox.Text);
                        if (angleTypeBox.SelectedItem.Equals("град."))
                        {
                            double radAngle = double.Parse(forceAngleBox.Text) * Math.PI / 180;
                            state.ForceAngle = radAngle;
                        }
                        else
                        {
                            state.ForceAngle = double.Parse(forceAngleBox.Text);
                        }
                        break;
                    case "Закрепление":
                        state.NodeType = NodeType.Fixed;
                        break;
                    default:
                        state.NodeType = NodeType.None;
                        break;
                }
                _mesh.ChangeNodeState(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y, meshPanel.ClientRectangle, state);
                meshPanel.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "Сила")
            {
                forcePanel.Visible = true;
            }
            else
            {
                forcePanel.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!CheckMesh())
                return;
            try
            {
                ElasticitySolutionInfoParser parser = new ElasticitySolutionInfoParser(dataPathBox.Text);
                ElasticitySolutionInfo info = parser.ParseTxt(coefsBox.Text);
                Elasticity2DSolver solver = new Elasticity2DSolver();
                double[] solution = solver.Solve(_mesh, info);
                _lastNodesMoves = solution;
                _mesh.MoveMesh(solution);
                meshPanel.Refresh();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!CheckMesh())
                return;
            _mesh.ClearNodesTypes();
            meshPanel.Refresh();
        }

        private void dataPathBox_DragEnter(object sender, DragEventArgs e)
        {
            dataPathBox.BackColor = Color.Blue;

            DragDropEffects effect = DragDropEffects.None;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = ((string[])e.Data.GetData(DataFormats.FileDrop));
                if (files.Length > 1)
                {
                    MessageBox.Show("Выберите одну папку");
                    return;
                }
                var path = files[0];
                if (Directory.Exists(path))
                {
                    effect = DragDropEffects.Copy;
                    dataPathBox.Text = path;
                    string[] folderFiles = Directory.GetFiles(path).Select(filename => filename.Split('\\')[filename.Split('\\').Length - 1]).ToArray();
                    nodesBox.Items.Clear();
                    elementsBox.Items.Clear();
                    coefsBox.Items.Clear();
                    nodesBox.Items.AddRange(folderFiles);
                    elementsBox.Items.AddRange(folderFiles);
                    coefsBox.Items.AddRange(folderFiles);
                }
            }
            e.Effect = effect;
        }

        private void dataPathBox_DragLeave(object sender, EventArgs e)
        {
            dataPathBox.BackColor = SystemColors.Control;
        }

        private void dataPathBox_DragDrop(object sender, DragEventArgs e)
        {
            dataPathBox.BackColor = SystemColors.Control;
        }

        private void loadMeshButton_Click(object sender, EventArgs e)
        {
            try
            {
                Mesh2DLoader parser = new Mesh2DLoader(dataPathBox.Text);
                _mesh = parser.ParseMeshFromCsv(nodesBox.Text, elementsBox.Text, ';');
                _lastNodesMoves = null;
                meshPanel.Refresh();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void showDisplacements_Click(object sender, EventArgs e)
        {
            if (_lastNodesMoves == null)
            {
                MessageBox.Show("Сначала нужно провести вычисления");
            }
            else
            {
                ElasticitySolutionInfoParser parser = new ElasticitySolutionInfoParser(dataPathBox.Text);
                ElasticitySolutionInfo info = parser.ParseTxt(coefsBox.Text);
                _mesh.PaintElementsByCharacteristics(_lastNodesMoves, info, MeshPaintCharacteristicType.Displacements);
                meshPanel.Refresh();
            }
        }

        private void showStressesButton_Click(object sender, EventArgs e)
        {
            if (_lastNodesMoves == null)
            {
                MessageBox.Show("Сначала нужно провести вычисления");
            }
            else
            {
                ElasticitySolutionInfoParser parser = new ElasticitySolutionInfoParser(dataPathBox.Text);
                ElasticitySolutionInfo info = parser.ParseTxt(coefsBox.Text);
                _mesh.PaintElementsByCharacteristics(_lastNodesMoves, info, MeshPaintCharacteristicType.Stresses);
                meshPanel.Refresh();
            }
        }

        private void showXDeformationsButton_Click(object sender, EventArgs e)
        {
            if (_lastNodesMoves == null)
            {
                MessageBox.Show("Сначала нужно провести вычисления");
            }
            else
            {
                ElasticitySolutionInfoParser parser = new ElasticitySolutionInfoParser(dataPathBox.Text);
                ElasticitySolutionInfo info = parser.ParseTxt(coefsBox.Text);
                _mesh.PaintElementsByCharacteristics(_lastNodesMoves, info, MeshPaintCharacteristicType.XDeformations);
                meshPanel.Refresh();
            }
        }

        private void showYDeformationsButton_Click(object sender, EventArgs e)
        {
            if (_lastNodesMoves == null)
            {
                MessageBox.Show("Сначала нужно провести вычисления");
            }
            else
            {
                ElasticitySolutionInfoParser parser = new ElasticitySolutionInfoParser(dataPathBox.Text);
                ElasticitySolutionInfo info = parser.ParseTxt(coefsBox.Text);
                _mesh.PaintElementsByCharacteristics(_lastNodesMoves, info, MeshPaintCharacteristicType.YDeformations);
                meshPanel.Refresh();
            }
        }
    }
}
