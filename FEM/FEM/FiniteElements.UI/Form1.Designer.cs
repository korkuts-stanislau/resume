
namespace FiniteElements.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.meshPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.nodeTypeBox = new System.Windows.Forms.ComboBox();
            this.forcePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.forceValueBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.forceAngleBox = new System.Windows.Forms.TextBox();
            this.angleTypeBox = new System.Windows.Forms.ComboBox();
            this.computeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.clearNodesButton = new System.Windows.Forms.Button();
            this.dataPathBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nodesBox = new System.Windows.Forms.ComboBox();
            this.elementsBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.loadMeshButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.coefsBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.showDisplacements = new System.Windows.Forms.Button();
            this.showStressesButton = new System.Windows.Forms.Button();
            this.showYDeformationsButton = new System.Windows.Forms.Button();
            this.showXDeformationsButton = new System.Windows.Forms.Button();
            this.mainControl = new System.Windows.Forms.TabControl();
            this.fem2D = new System.Windows.Forms.TabPage();
            this.fem3D = new System.Windows.Forms.TabPage();
            this.forcePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.mainControl.SuspendLayout();
            this.fem2D.SuspendLayout();
            this.SuspendLayout();
            // 
            // meshPanel
            // 
            this.meshPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.meshPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.meshPanel.Location = new System.Drawing.Point(13, 14);
            this.meshPanel.Name = "meshPanel";
            this.meshPanel.Size = new System.Drawing.Size(618, 599);
            this.meshPanel.TabIndex = 0;
            this.meshPanel.Click += new System.EventHandler(this.panel1_Click);
            this.meshPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(57, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Задать значение узлу";
            // 
            // nodeTypeBox
            // 
            this.nodeTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nodeTypeBox.FormattingEnabled = true;
            this.nodeTypeBox.Items.AddRange(new object[] {
            "Закрепление",
            "Сила",
            "Без условия"});
            this.nodeTypeBox.Location = new System.Drawing.Point(100, 51);
            this.nodeTypeBox.Name = "nodeTypeBox";
            this.nodeTypeBox.Size = new System.Drawing.Size(161, 23);
            this.nodeTypeBox.TabIndex = 4;
            this.nodeTypeBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // forcePanel
            // 
            this.forcePanel.Controls.Add(this.label3);
            this.forcePanel.Controls.Add(this.forceValueBox);
            this.forcePanel.Controls.Add(this.label4);
            this.forcePanel.Controls.Add(this.forceAngleBox);
            this.forcePanel.Controls.Add(this.angleTypeBox);
            this.forcePanel.Location = new System.Drawing.Point(5, 90);
            this.forcePanel.Name = "forcePanel";
            this.forcePanel.Size = new System.Drawing.Size(268, 70);
            this.forcePanel.TabIndex = 6;
            this.forcePanel.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Значение силы";
            // 
            // forceValueBox
            // 
            this.forceValueBox.Location = new System.Drawing.Point(127, 3);
            this.forceValueBox.Name = "forceValueBox";
            this.forceValueBox.Size = new System.Drawing.Size(128, 23);
            this.forceValueBox.TabIndex = 1;
            this.forceValueBox.Text = "5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(3, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "Угол силы";
            // 
            // forceAngleBox
            // 
            this.forceAngleBox.Location = new System.Drawing.Point(90, 32);
            this.forceAngleBox.Name = "forceAngleBox";
            this.forceAngleBox.Size = new System.Drawing.Size(83, 23);
            this.forceAngleBox.TabIndex = 3;
            this.forceAngleBox.Text = "90";
            // 
            // angleTypeBox
            // 
            this.angleTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.angleTypeBox.FormattingEnabled = true;
            this.angleTypeBox.Items.AddRange(new object[] {
            "рад.",
            "град."});
            this.angleTypeBox.Location = new System.Drawing.Point(179, 32);
            this.angleTypeBox.Name = "angleTypeBox";
            this.angleTypeBox.Size = new System.Drawing.Size(77, 23);
            this.angleTypeBox.TabIndex = 4;
            // 
            // computeButton
            // 
            this.computeButton.BackColor = System.Drawing.Color.AntiqueWhite;
            this.computeButton.Location = new System.Drawing.Point(13, 628);
            this.computeButton.Name = "computeButton";
            this.computeButton.Size = new System.Drawing.Size(618, 41);
            this.computeButton.TabIndex = 3;
            this.computeButton.Text = "Вычислить";
            this.computeButton.UseVisualStyleBackColor = false;
            this.computeButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(8, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "Тип узла";
            // 
            // clearNodesButton
            // 
            this.clearNodesButton.BackColor = System.Drawing.SystemColors.Info;
            this.clearNodesButton.Location = new System.Drawing.Point(666, 525);
            this.clearNodesButton.Name = "clearNodesButton";
            this.clearNodesButton.Size = new System.Drawing.Size(269, 32);
            this.clearNodesButton.TabIndex = 8;
            this.clearNodesButton.Text = "Обнулить значения узлов";
            this.clearNodesButton.UseVisualStyleBackColor = false;
            this.clearNodesButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataPathBox
            // 
            this.dataPathBox.AllowDrop = true;
            this.dataPathBox.Location = new System.Drawing.Point(22, 26);
            this.dataPathBox.Name = "dataPathBox";
            this.dataPathBox.ReadOnly = true;
            this.dataPathBox.Size = new System.Drawing.Size(231, 23);
            this.dataPathBox.TabIndex = 9;
            this.dataPathBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataPathBox_DragDrop);
            this.dataPathBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataPathBox_DragEnter);
            this.dataPathBox.DragLeave += new System.EventHandler(this.dataPathBox_DragLeave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(204, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Перетащите сюда папку с данными";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "Выберите файл с узлами";
            // 
            // nodesBox
            // 
            this.nodesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nodesBox.FormattingEnabled = true;
            this.nodesBox.Location = new System.Drawing.Point(19, 88);
            this.nodesBox.Name = "nodesBox";
            this.nodesBox.Size = new System.Drawing.Size(234, 23);
            this.nodesBox.TabIndex = 15;
            // 
            // elementsBox
            // 
            this.elementsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.elementsBox.FormattingEnabled = true;
            this.elementsBox.Location = new System.Drawing.Point(19, 148);
            this.elementsBox.Name = "elementsBox";
            this.elementsBox.Size = new System.Drawing.Size(234, 23);
            this.elementsBox.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(173, 15);
            this.label8.TabIndex = 16;
            this.label8.Text = "Выберите файл с элементами";
            // 
            // loadMeshButton
            // 
            this.loadMeshButton.Location = new System.Drawing.Point(19, 256);
            this.loadMeshButton.Name = "loadMeshButton";
            this.loadMeshButton.Size = new System.Drawing.Size(234, 35);
            this.loadMeshButton.TabIndex = 18;
            this.loadMeshButton.Text = "Загрузить сетку";
            this.loadMeshButton.UseVisualStyleBackColor = true;
            this.loadMeshButton.Click += new System.EventHandler(this.loadMeshButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.Controls.Add(this.coefsBox);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.dataPathBox);
            this.panel1.Controls.Add(this.loadMeshButton);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.elementsBox);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.nodesBox);
            this.panel1.Location = new System.Drawing.Point(666, 204);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(269, 304);
            this.panel1.TabIndex = 19;
            // 
            // coefsBox
            // 
            this.coefsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coefsBox.FormattingEnabled = true;
            this.coefsBox.Location = new System.Drawing.Point(19, 209);
            this.coefsBox.Name = "coefsBox";
            this.coefsBox.Size = new System.Drawing.Size(234, 23);
            this.coefsBox.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(203, 15);
            this.label6.TabIndex = 19;
            this.label6.Text = "Выберите файл с коэффициентами";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.nodeTypeBox);
            this.panel2.Controls.Add(this.forcePanel);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(666, 14);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(269, 167);
            this.panel2.TabIndex = 20;
            // 
            // showDisplacements
            // 
            this.showDisplacements.BackColor = System.Drawing.SystemColors.Info;
            this.showDisplacements.Location = new System.Drawing.Point(666, 572);
            this.showDisplacements.Name = "showDisplacements";
            this.showDisplacements.Size = new System.Drawing.Size(126, 41);
            this.showDisplacements.TabIndex = 21;
            this.showDisplacements.Text = "Показать перемещения";
            this.showDisplacements.UseVisualStyleBackColor = false;
            this.showDisplacements.Click += new System.EventHandler(this.showDisplacements_Click);
            // 
            // showStressesButton
            // 
            this.showStressesButton.BackColor = System.Drawing.SystemColors.Info;
            this.showStressesButton.Location = new System.Drawing.Point(809, 572);
            this.showStressesButton.Name = "showStressesButton";
            this.showStressesButton.Size = new System.Drawing.Size(126, 41);
            this.showStressesButton.TabIndex = 22;
            this.showStressesButton.Text = "Показать напряжения";
            this.showStressesButton.UseVisualStyleBackColor = false;
            this.showStressesButton.Click += new System.EventHandler(this.showStressesButton_Click);
            // 
            // showYDeformationsButton
            // 
            this.showYDeformationsButton.BackColor = System.Drawing.SystemColors.Info;
            this.showYDeformationsButton.Location = new System.Drawing.Point(809, 628);
            this.showYDeformationsButton.Name = "showYDeformationsButton";
            this.showYDeformationsButton.Size = new System.Drawing.Size(126, 41);
            this.showYDeformationsButton.TabIndex = 24;
            this.showYDeformationsButton.Text = "Показать деформации по Y";
            this.showYDeformationsButton.UseVisualStyleBackColor = false;
            this.showYDeformationsButton.Click += new System.EventHandler(this.showYDeformationsButton_Click);
            // 
            // showXDeformationsButton
            // 
            this.showXDeformationsButton.BackColor = System.Drawing.SystemColors.Info;
            this.showXDeformationsButton.Location = new System.Drawing.Point(666, 628);
            this.showXDeformationsButton.Name = "showXDeformationsButton";
            this.showXDeformationsButton.Size = new System.Drawing.Size(126, 41);
            this.showXDeformationsButton.TabIndex = 23;
            this.showXDeformationsButton.Text = "Показать деформации по X";
            this.showXDeformationsButton.UseVisualStyleBackColor = false;
            this.showXDeformationsButton.Click += new System.EventHandler(this.showXDeformationsButton_Click);
            // 
            // mainControl
            // 
            this.mainControl.Controls.Add(this.fem2D);
            this.mainControl.Controls.Add(this.fem3D);
            this.mainControl.Location = new System.Drawing.Point(12, 12);
            this.mainControl.Name = "mainControl";
            this.mainControl.SelectedIndex = 0;
            this.mainControl.Size = new System.Drawing.Size(960, 729);
            this.mainControl.TabIndex = 25;
            // 
            // fem2D
            // 
            this.fem2D.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.fem2D.Controls.Add(this.meshPanel);
            this.fem2D.Controls.Add(this.showYDeformationsButton);
            this.fem2D.Controls.Add(this.computeButton);
            this.fem2D.Controls.Add(this.showXDeformationsButton);
            this.fem2D.Controls.Add(this.clearNodesButton);
            this.fem2D.Controls.Add(this.showStressesButton);
            this.fem2D.Controls.Add(this.panel1);
            this.fem2D.Controls.Add(this.showDisplacements);
            this.fem2D.Controls.Add(this.panel2);
            this.fem2D.Location = new System.Drawing.Point(4, 24);
            this.fem2D.Name = "fem2D";
            this.fem2D.Padding = new System.Windows.Forms.Padding(3);
            this.fem2D.Size = new System.Drawing.Size(952, 701);
            this.fem2D.TabIndex = 0;
            this.fem2D.Text = "2D";
            // 
            // fem3D
            // 
            this.fem3D.Location = new System.Drawing.Point(4, 24);
            this.fem3D.Name = "fem3D";
            this.fem3D.Padding = new System.Windows.Forms.Padding(3);
            this.fem3D.Size = new System.Drawing.Size(952, 701);
            this.fem3D.TabIndex = 1;
            this.fem3D.Text = "3D";
            this.fem3D.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 753);
            this.Controls.Add(this.mainControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "МКЭ";
            this.forcePanel.ResumeLayout(false);
            this.forcePanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.mainControl.ResumeLayout(false);
            this.fem2D.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel meshPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox nodeTypeBox;
        private System.Windows.Forms.FlowLayoutPanel forcePanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox forceValueBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox forceAngleBox;
        private System.Windows.Forms.Button computeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clearNodesButton;
        private System.Windows.Forms.ComboBox angleTypeBox;
        private System.Windows.Forms.TextBox dataPathBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox nodesBox;
        private System.Windows.Forms.ComboBox elementsBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button loadMeshButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox coefsBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button showDisplacements;
        private System.Windows.Forms.Button showStressesButton;
        private System.Windows.Forms.Button showYDeformationsButton;
        private System.Windows.Forms.Button showXDeformationsButton;
        private System.Windows.Forms.TabControl mainControl;
        private System.Windows.Forms.TabPage fem2D;
        private System.Windows.Forms.TabPage fem3D;
    }
}

