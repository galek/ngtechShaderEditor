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

using DevExpress.XtraEditors;
using NGEd.Tools;
using ShaderEditor.MaterialNodes;
using System;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Graph;
using System.Collections.Generic;

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
            //var viewport = new ViewPort();
            //propWindow.Parent = dockPanel1;
            //viewport.Text = "Material Preview";
            //viewport.InitEngine();

            _InitMaterialNode();
            _RegisterAllNodes();

            graphControl.GraphControl.ShowElementMenu += new EventHandler<Graph.AcceptElementLocationEventArgs>(OnShowElementMenu);
            graphControl.GraphControl.FocusChanged += new EventHandler<Graph.ElementEventArgs>(GraphControl_Click);


            DockingUtils.CreateEditorFolderIfNotExist();
            DockingUtils.LoadLayoutConfiguration(_LayoutName, dockManager1);
        }

        private void GraphControl_Click(object sender, ElementEventArgs e)
        {
            var obj = e.Element;
            if (obj == null)
                return;

            SelectObjProp(obj);
        }

        //public void SetState(EditorState _state)
        //{
        //    editorState = _state;
        //}

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

            // Tests
            someNode = new Graph.Node("My Title");
            someNode.Location = new Point(500, 100);
            var check1Item = new Graph.Items.NodeCheckboxItem("Check 1", true, true) { Tag = 31337 };
            someNode.AddItem(check1Item);
            someNode.AddItem(new Graph.Items.NodeCheckboxItem("Check 2", true, true) { Tag = 42f });

            graphControl.GraphControl.AddNode(someNode);
        }

        private void OnShowElementMenu(object sender, Graph.AcceptElementLocationEventArgs e)
        {
            XtraMessageBox.Show("!");
            if (e.Element == null)
            {
                // Show a test menu for when you click on nothing
                //testMenuItem.Text = "(clicked on nothing)";
                //nodeMenu.Show(e.Position);
                e.Cancel = false;
            }
            else
            if (e.Element is Graph.Node)
            {
                // Show a test menu for a node
                //testMenuItem.Text = ((Graph.Node)e.Element).Title;
                //nodeMenu.Show(e.Position);
                e.Cancel = false;
            }
            else
            if (e.Element is Graph.NodeItem)
            {
                // Show a test menu for a nodeItem
                //testMenuItem.Text = e.Element.GetType().Name;
                //nodeMenu.Show(e.Position);
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
        /// TODO: REWRITE
        /// </summary>
        private void _RegisterAllNodes()
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Click += new EventHandler(SomeNode_TestMouseDown);
            item.Text = "Sub-item 1";
            //nodeMenu.Items.Add(item);
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
            //if (editorState.GetEngine == null)
            //    return;

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
            //if (_materialName == string.Empty ||
            //        editorState.GetEngine == null)
            //    return;

            //EngineCLR.log.DebugPrintf("Loaded material " + _materialName + " to editor");

            //propWindow.propertyGrid1.SelectedObject = EngineCLR.MaterialsHelper.UpdateMaterialVars(_materialName);
            //propWindow.propertyGrid1.RetrieveFields();
            //propWindow.propertyGrid1.UpdateRows();
            //propWindow.propertyGrid1.Refresh();
        }

        private void SelectObjProp(Object _node)
        {
            propWindow.propertyGrid1.SelectedObject = _node;
            propWindow.propertyGrid1.RetrieveFields();
            propWindow.propertyGrid1.UpdateRows();
            propWindow.propertyGrid1.Refresh();
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
            //SaveFileDialog openFileDialog1 = new SaveFileDialog();
            //openFileDialog1.InitialDirectory = "../";
            //openFileDialog1.Filter = "Material file (*.mat)|*.mat|All files (*.*)|*.*";
            //openFileDialog1.FilterIndex = 2;
            //openFileDialog1.RestoreDirectory = false;
            //if ((openFileDialog1.ShowDialog() == DialogResult.OK) && (openFileDialog1.FileName != ""))
            //{
            //    SaveLoadHelper.SaveEngineFormat(openFileDialog1.FileName, SaveLoadHelper.Type.MATERIAL);
            //}
            //openFileDialog1.Dispose();
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

            FileStream fs = new FileStream("DataFile.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, graphControl.GraphControl.Nodes);
            }
            catch (SerializationException er)
            {
                Console.WriteLine("Failed to serialize. Reason: " + er.Message);
                MessageBox.Show(er.Message, "1", MessageBoxButtons.OK);
                throw;
            }
            finally
            {
                fs.Close();
            }

        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Open the file containing the data that you want to deserialize.
            FileStream fs = new FileStream("DataFile.dat", FileMode.Open);

            object res = null;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                // Deserialize the hashtable from the file and 
                // assign the reference to the local variable.
                res = formatter.Deserialize(fs);
            }
            catch (SerializationException er)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + er.Message);
                MessageBox.Show(er.Message, "1", MessageBoxButtons.OK);
                throw;
            }
            finally
            {
                fs.Close();
            }

            graphControl.GraphControl.SetGraphNodes((List<Node>)res);

        }
    }
}