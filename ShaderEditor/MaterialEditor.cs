/* Copyright (C) 2009-2017, NG Games Ltd. All rights reserved.
*
* This file is part of the NGTech (http://nggames.com/).
*
* Your use and or redistribution of this software in source and / or
* binary form, with or without modification, is subject to: (i) your
* ongoing acceptance of and compliance with the terms and conditions of
* the NGTech License Agreement; and (ii) your inclusion of this notice
* in any version of this software that you use or redistribute.
* A copy of the NGTech License Agreement is available by contacting
* NG Games Ltd. at http://nggames.com/
*/

using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Graph;
using NGEd.Tools;
using ShaderEditor.Nodes;
using ShaderEditor.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace NGEd
{
    public partial class MaterialEditor : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MaterialEditor()
        {
            InitializeComponent();
            _InitEditor();
        }

        private void _InitEditor()
        {
            AutoSize = true;
            graphControl = new NGEd.FormComponents.NodeComponent();
            graphControl.Parent = dockPanel4;
            graphControl.Show();
            /**/
            propWindow = new PropertiesTab();
            propWindow.Parent = dockPanel3;
            propWindow.Show();
            /**/
            _InitMaterialNode();

            graphControl.GraphControl.ShowElementMenu += new EventHandler<Graph.AcceptElementLocationEventArgs>(OnShowElementMenu);
            graphControl.GraphControl.FocusChanged += new EventHandler<Graph.ElementEventArgs>(GraphControl_Click);
            graphControl.GraphControl.ConnectionAdded += GraphControl_ConnectionAdded;

            DockingUtils.CreateEditorFolderIfNotExist();
            DockingUtils.LoadLayoutConfiguration(_LayoutName, dockManager1);
        }

        private void GraphControl_ConnectionAdded(object sender, AcceptNodeConnectionEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void GraphControl_Click(object sender, ElementEventArgs e)
        {
            var obj = e.Element;
            if (obj == null)
                return;

            // TODO: сделать возможность убрать объект
            SelectObjProp(obj);
        }

        private void _InitMaterialNode()
        {
            var someNode = new Graph.Node("Material Inputs");
            someNode.Location = new Point(508, 305);
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Base Color", true, false) { Tag = -1 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Metallic", true, false) { Tag = -2 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Specular", true, false) { Tag = -3 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Roughness", true, false) { Tag = -4 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Emissive Color", true, false) { Tag = -5 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Opacity", true, false) { Tag = -6 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Opacity Mask", true, false) { Tag = -7 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Normal", true, false) { Tag = -8 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("World Position Offset", true, false) { Tag = -9 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("World Displacement", true, false) { Tag = -10 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Tessellation Multiplier", true, false) { Tag = -11 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("SubSurface Color", true, false) { Tag = -12 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Clear Coat", true, false) { Tag = -13 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Clear Coat Roughness", true, false) { Tag = -14 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Ambient Occlusion", true, false) { Tag = -15 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Refraction", true, false) { Tag = -16 });
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Pixel Depth Offset", true, false) { Tag = -17 });

            graphControl.GraphControl.AddNode(someNode);

            _RegisterNodes();
        }

        private void _RegisterNodes()
        {
            // TODO: create list and generate this list
            _RegisterNode<ColorNode>("ColorNode", "ColorNode");
            _RegisterNode<ColorNode>("ColorNode2", "ColorNode23");
        }

        private void _RegisterNode<T>(string _caption, string _name)
        {
            BarButtonItem item = new BarButtonItem() { Caption = _caption, Id = 0, ImageIndex = 0, Name = _name };
            nodeMenu.ItemLinks.Add(item);
            item.ItemClick +=
               new ItemClickEventHandler(delegate (Object o, ItemClickEventArgs a)
               {
                   Activator.CreateInstance(typeof(T),
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding, null, new object[] { this, LastClickPositionHelper.XPos, LastClickPositionHelper.YPos }, CultureInfo.CurrentCulture);
               });
        }

        public GraphControl GraphControlFormComp { get { return graphControl.GraphControl; } }

        private void OnShowElementMenu(object sender, Graph.AcceptElementLocationEventArgs e)
        {
            // On empty space
            if (e.Element == null)
            {
                // similiar as MousePosition.X
                //MessageBox.Show(string.Format("EPos X: {0} Y: {1}", e.Position.X, e.Position.Y));


                LastClickPositionHelper.XPos = graphControl.PointToClient(Cursor.Position).X;
                LastClickPositionHelper.YPos = graphControl.PointToClient(Cursor.Position).Y;

                // Show a test menu for when you click on nothing
                nodeMenu.ShowPopup(e.Position);
                e.Cancel = false;
            }
            else
            if (e.Element is Graph.Node)
            {
                // Show a test menu for a node
                nodeMenu.ShowPopup(e.Position);
                e.Cancel = false;
            }
            else
            if (e.Element is Graph.NodeItem)
            {
                // Show a test menu for a nodeItem
                nodeMenu.ShowPopup(e.Position);
                e.Cancel = false;
            }
            else
            {
                // if you don't want to show a menu for this item (but perhaps show a menu for something more higher up)
                // then you can cancel the event
                e.Cancel = true;
            }
        }

        /// <summary>
        /// TODO: TEST ONLY. Тестовая функция
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SomeNode_TestMouseDown(object sender, EventArgs e)
        {
            var node = new Graph.Node("Some node");
            node.AddItem(new Graph.Items.NodeLabelItem("Entry 1", true, false));
            node.AddItem(new Graph.Items.NodeLabelItem("Entry 2", true, false));
            node.AddItem(new Graph.Items.NodeLabelItem("Entry 3", false, true));
            node.AddItem(new Graph.Items.NodeTextBoxItem("TEXTTEXT", false, true));
            node.AddItem(new Graph.Items.NodeDropDownItem(new string[] { "1", "2", "3", "4" }, 0, false, false));
            // Устанавливает позицию, относительно graphControl
            node.Location = new Point(graphControl.PointToClient(Cursor.Position).X, graphControl.PointToClient(Cursor.Position).Y);
            XtraMessageBox.Show(graphControl.PointToClient(Cursor.Position).X.ToString(), graphControl.PointToClient(Cursor.Position).Y.ToString());
            DoDragDrop(node, DragDropEffects.Copy);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "../";
            openFileDialog1.Filter = "Material files (*.mat)|*.mat|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if ((openFileDialog1.ShowDialog() == DialogResult.OK) && (openFileDialog1.SafeFileName != ""))
            {
                _SelectMaterial(openFileDialog1.SafeFileName);
            }
            openFileDialog1.Dispose();
        }

        private void _SelectMaterial(string _materialName)
        {
            // Open the file containing the data that you want to deserialize.
            FileStream fs = new FileStream(_materialName, FileMode.Open);

            object res = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                res = formatter.Deserialize(fs);
            }
            catch (SerializationException er)
            {
                MessageBox.Show("Failed to deserialize. Reason: " + er.Message, "Error", MessageBoxButtons.OK);
                throw;
            }
            finally
            {
                fs.Close();
            }

            graphControl.GraphControl.SetGraphNodes((List<Node>)res);
        }

        private void SelectObjProp(Object _node)
        {
            propWindow.propertyGrid1.SelectedObject = _node;

            propWindow.propertyGrid1.RetrieveFields();
            propWindow.propertyGrid1.UpdateRows();
            propWindow.propertyGrid1.Refresh();

            propWindow.HideReadOnlyValues = barCheckItem1.Checked;
        }

        //private EditorState editorState = null;
        private PropertiesTab propWindow = null;

        private NGEd.FormComponents.NodeComponent graphControl = null;

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DockingUtils.SaveLayoutConfiguration(_LayoutName, dockManager1);
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DockingUtils.LoadLayoutConfiguration(_LayoutName, dockManager1);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "../";
            openFileDialog1.Filter = "Material file (*.mat)|*.mat|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = false;
            if ((openFileDialog1.ShowDialog() == DialogResult.OK) && (openFileDialog1.SafeFileName != ""))
            {
                _SelectMaterial(openFileDialog1.SafeFileName);
            }
            openFileDialog1.Dispose();
        }

        private static string _LayoutName = "MaterialEditor.xml";

        private void MaterialEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            DockingUtils.SaveLayoutConfiguration(_LayoutName, dockManager1);
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog openFileDialog1 = new SaveFileDialog();
            openFileDialog1.InitialDirectory = "../";
            openFileDialog1.Filter = "Material file (*.mat)|*.mat|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = false;
            if ((openFileDialog1.ShowDialog() == DialogResult.OK) && (openFileDialog1.FileName != ""))
            {
                FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(fs, graphControl.GraphControl.Nodes);
                }
                catch (SerializationException er)
                {
                    MessageBox.Show("Failed to serialize. Reason: " + er.Message, "Error", MessageBoxButtons.OK);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
            openFileDialog1.Dispose();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var obj = new { graphControl.GraphControl.Nodes };
            //var json = JsonConvert.SerializeObject(graphControl);
            //MessageBox.Show(json, "1", MessageBoxButtons.OK);

            //JsonConvert.DeserializeObject<MaterialBaseNode>(json);
            /*
                // Tests
            someNode = new Graph.Node("My Title");
            someNode.Location = new Point(500, 100);
            var check1Item = new Graph.Items.NodeCheckboxItem("Check 1", true, true) { Tag = 31337 };
            someNode.AddItem(check1Item);
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Check 2", true, true) { Tag = 42f });

            graphControl.GraphControl.AddNode(someNode);
             */
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            propWindow.HideReadOnlyValues = barCheckItem1.Checked;
        }

        private void barCheckItem2_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.graphControl.GraphControl.ShowGrid = barCheckItem2.Checked;
        }

        public LastClickPositionHelperC LastClickPositionHelper { get { return mLastClickPositionHelper; } }
        private LastClickPositionHelperC mLastClickPositionHelper = new LastClickPositionHelperC();
    }
}