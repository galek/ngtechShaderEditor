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

namespace NGEd
{
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Repository;
    using DevExpress.XtraVerticalGrid.Events;
    using System;
    using System.Windows.Forms;

    // TODO: Необходимо при изменении свойства, обновлять propgrid в реальном времени
    public partial class PropertiesTab : DevExpress.XtraEditors.PanelControl
    {
        public PropertiesTab()
        {
            InitializeComponent();
            AutoSize = true;
            Dock = System.Windows.Forms.DockStyle.Fill;
            Text = "Properties";

            customize = new PropertiesTabCustomize(propertyGrid1);
        }

        private PropertiesTabCustomize customize;

        public object SelectedObject { get { return customize.SelectedObject; } set { customize.SelectedObject = value; } }

        public void RetrieveFields()
        {
            customize.PropertyGridControl.RetrieveFields();
        }

        public void UpdateRows()
        {
            customize.PropertyGridControl.UpdateRows();
        }

        private void propertyGrid1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            log.DebugPrintf(System.Reflection.MethodBase.GetCurrentMethod().Name);
            if (this.propertyGrid1 != null)
                this.propertyGrid1.Refresh();
        }

        private void propertyGrid1_StateChanged(object sender, EventArgs e)
        {
            log.Warning(System.Reflection.MethodBase.GetCurrentMethod().Name);
            _UpdatePropGrid(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        private void _UpdatePropGrid(string _name)
        {
            //log.DebugPrintf(_name);
            //api.EDITOR_CheckWhatWasModificated();
        }

        private void propertyGrid1_FocusedRecordCellChanged(object sender, IndexChangedEventArgs e)
        {
            if (this.propertyGrid1 != null)
                this.propertyGrid1.Refresh();
        }
    }

    /*Used for customization property grid*/

    public class PropertiesTabCustomize
    {
        private RepositoryItemButtonEdit _browserFolderDialogEdit = new RepositoryItemButtonEdit();
        private RepositoryItemSpinEdit _repositoryItemSpinEdit1 = new RepositoryItemSpinEdit();
        private RepositoryItemTextEdit _repositoryItemSpinEditFloat = new RepositoryItemTextEdit();
        private RepositoryItemSpinEdit _repositoryItemSpinEditUINT = new RepositoryItemSpinEdit();
        private RepositoryItemSpinEdit _repositoryItemSpinEditINT = new RepositoryItemSpinEdit();
        private RepositoryItemTextEdit _repositoryStringEdit = new RepositoryItemTextEdit();

        private RepositoryItemTextEdit _repositoryItemSpinEditVectors = new RepositoryItemTextEdit();

        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGrid1 = null;

        public object SelectedObject { get { return propertyGrid1.SelectedObject; } set { propertyGrid1.SelectedObject = value; } }
        public DevExpress.XtraVerticalGrid.PropertyGridControl PropertyGridControl { get { return propertyGrid1; } }

        /**/

        public PropertiesTabCustomize(DevExpress.XtraVerticalGrid.PropertyGridControl _propertyGrid1)
        {
            propertyGrid1 = _propertyGrid1;

            _browserFolderDialogEdit.ButtonClick += openFileDialogButtonClick;

            /*See docs on: https://documentation.devexpress.com/#WindowsForms/clsDevExpressXtraEditorsRepositoryRepositoryItemtopic*/
            propertyGrid1.DefaultEditors.Add(typeof(MFileNameEditor), _browserFolderDialogEdit);
            propertyGrid1.DefaultEditors.Add(typeof(TextureNameEditor), _browserFolderDialogEdit);
            propertyGrid1.DefaultEditors.Add(typeof(MaterialNameEditor), _browserFolderDialogEdit);
            propertyGrid1.DefaultEditors.Add(typeof(ShaderNameEditor), _browserFolderDialogEdit);
            propertyGrid1.DefaultEditors.Add(typeof(ScriptNameEditor), _browserFolderDialogEdit);

            /*Внезапно, но приходится и стандартные методы перегружать*/
            propertyGrid1.DefaultEditors.Add(typeof(float), _repositoryItemSpinEditFloat);
            propertyGrid1.DefaultEditors.Add(typeof(double), _repositoryItemSpinEditFloat);

            propertyGrid1.DefaultEditors.Add(typeof(long), _repositoryItemSpinEditINT);
            propertyGrid1.DefaultEditors.Add(typeof(int), _repositoryItemSpinEditINT);

            propertyGrid1.DefaultEditors.Add(typeof(uint), _repositoryItemSpinEditUINT);
            propertyGrid1.DefaultEditors.Add(typeof(UInt16), _repositoryItemSpinEditUINT);
            propertyGrid1.DefaultEditors.Add(typeof(UInt32), _repositoryItemSpinEditUINT);
            propertyGrid1.DefaultEditors.Add(typeof(UInt64), _repositoryItemSpinEditUINT);
            // TODO:!HACK Не убирай, тут каст к INT
            propertyGrid1.DefaultEditors.Add(typeof(Decimal), _repositoryItemSpinEditINT);

            propertyGrid1.DefaultEditors.Add(typeof(MVec3), _repositoryStringEdit);
            propertyGrid1.DefaultEditors.Add(typeof(MVec4), _repositoryStringEdit);

            propertyGrid1.CustomDrawRowValueCell += propertyGrid_CustomDrawRowValuCell;
            propertyGrid1.CustomRecordCellEdit += PropertyGrid1_CustomRecordCellEdit;
            propertyGrid1.CustomRecordCellEditForEditing += PropertyGrid1_CustomRecordCellEdit;

            _repositoryStringEdit.ParseEditValue += __Edit_ParseEditValue;

            _repositoryItemSpinEditVectors.ParseEditValue += _repositoryItemSpinEditVectors_ParseEditValue; ;

            // Only Float types
            _repositoryItemSpinEditFloat.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            _repositoryItemSpinEditFloat.Mask.EditMask = "f";
            _repositoryItemSpinEditFloat.ParseEditValue += __Edit_ParseEditValueFloat;

            // Only Uint types
            _repositoryItemSpinEditUINT.Mask.EditMask = "\\d+";
            _repositoryItemSpinEditUINT.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            _repositoryItemSpinEditUINT.ParseEditValue += __Edit_ParseEditValueUINT;
            _repositoryItemSpinEditUINT.Mask.UseMaskAsDisplayFormat = true;

            // Only int types
            _repositoryItemSpinEditINT.Mask.EditMask = "\\d";
            _repositoryItemSpinEditINT.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            _repositoryItemSpinEditINT.ParseEditValue += __Edit_ParseEditValueINT;
            _repositoryItemSpinEditINT.Mask.UseMaskAsDisplayFormat = true;

            // Only Vectors types
            //_repositoryItemSpinEditVectors.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            //_repositoryItemSpinEditVectors.ParseEditValue += __Edit_ParseEditValueVectors;
            //_repositoryItemSpinEditVectors.EditValueChanged += _repositoryItemSpinEditVectors_EditValueChanged;
        }

        private void _repositoryItemSpinEditVectors_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            //e.Value = e.Value.ToString();

            throw new NotImplementedException("CUSTOM VECTORS NOT WORKING");
        }

        // Принудительно преобразовывает UINT типы в UINT. Ибо, по умолчанию, значение = Decimal
        private void __Edit_ParseEditValueDecimalToINT(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = Convert.ToInt32(e.Value);
        }

        // Принудительно преобразовывает UINT типы в UINT. Ибо, по умолчанию, значение = Decimal
        private void __Edit_ParseEditValueUINT(object sender, ConvertEditValueEventArgs e)
        {
            if (e.Value is UInt16)
            {
                //throw new NotImplementedException("UInt16");
                e.Value = Convert.ToUInt16(e.Value);
            }
            else if (e.Value is UInt64)
            {
                //throw new NotImplementedException("UInt64");
                e.Value = Convert.ToUInt64(e.Value);
            }
            else // (e.Value is UInt32)
            {
                //throw new NotImplementedException("UInt32");
                e.Value = Convert.ToUInt32(e.Value);
            }
        }

        // Принудительно преобразовывает UINT типы в UINT. Ибо, по умолчанию, значение = Decimal
        private void __Edit_ParseEditValueINT(object sender, ConvertEditValueEventArgs e)
        {
            e.Value = Convert.ToInt32(e.Value);
        }

        private void __Edit_ParseEditValueFloat(object sender, ConvertEditValueEventArgs e)
        {
            if (e.Value is Double)
            {
                //throw new NotImplementedException("Double");
                e.Value = Convert.ToDouble(e.Value);
            }
            else
            {
                //throw new NotImplementedException("float");
                e.Value = Convert.ToDouble(e.Value);
            }
        }

        private void __Edit_ParseEditValueVectors(object sender, ConvertEditValueEventArgs e)
        {
            //if (e.Value is EngineCLR.MVec3)
            //{
            //    EngineCLR.MVec3 t = e.Value as EngineCLR.MVec3;
            //    {
            //        e.Value = t.ToSingle();
            //    }

            //}
            //else if (e.Value is EngineCLR.MVec4)
            //{
            //    throw new Exception("2");
            //}
            //else
            //    throw new Exception("3");
        }

        private void _repositoryItemSpinEditVectors_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void __Edit_ParseEditValue(object sender, ConvertEditValueEventArgs e)
        {
            if (e.Value is Double)
            {
                //throw new NotImplementedException("Double");
                e.Value = Convert.ToDouble(e.Value);
            }
            else if (e.Value is float)
            {
                //throw new NotImplementedException("float");
                e.Value = Convert.ToDouble(e.Value);
            }
            else if (e.Value is Boolean)
            {
                //throw new NotImplementedException("Boolean");
                e.Value = Convert.ToBoolean(e.Value);
            }
            else if (e.Value is UInt16)
            {
                //throw new NotImplementedException("UInt16");
                e.Value = Convert.ToUInt16(e.Value);
            }
            else if (e.Value is UInt32)
            {
                //throw new NotImplementedException("UInt32");
                e.Value = Convert.ToUInt32(e.Value);
            }
            else if (e.Value is UInt64)
            {
                //throw new NotImplementedException("UInt64");
                e.Value = Convert.ToUInt64(e.Value);
            }
            else if (e.Value is Decimal)
            {
                //throw new NotImplementedException("Decimal");
                e.Value = Convert.ToDecimal(e.Value);
            }
            else if (e.Value is Int16)
            {
                //throw new NotImplementedException("Int16");
                e.Value = Convert.ToInt16(e.Value);
            }
            else if (e.Value is Int32)
            {
                //throw new NotImplementedException("Int32");
                e.Value = Convert.ToInt32(e.Value);
            }
            else if (e.Value is Int64)
            {
                //throw new NotImplementedException("Int64");
                e.Value = Convert.ToInt64(e.Value);
            }
            else if (e.Value is String)
            {
                e.Value = e.Value.ToString();
            }
            else
            {
                e.Value = e.Value.GetType().ToString();
            }
        }

        private void PropertyGrid1_CustomRecordCellEdit(object sender, GetCustomRowCellEditEventArgs e)
        {
            //throw new NotImplementedException(e.Row.Name.ToString());
        }

        /**/

        private void propertyGrid_CustomDrawRowValuCell(object sender, CustomDrawRowValueCellEventArgs e)
        {
            if (propertyGrid1.SelectedObject == null || e.Row.Properties.RowEdit != null)
                return;

            System.Reflection.MemberInfo[] mi = (propertyGrid1.SelectedObject.GetType()).GetMember(e.Row.Properties.FieldName);
            if (mi.Length == 1)
            {
                EditorAliasAttribute attr = (EditorAliasAttribute)Attribute.GetCustomAttribute(mi[0], typeof(EditorAliasAttribute));
                if (attr != null)
                {
                    // TODO: Project Editor implementation of types
                    //f(attr.EditorType == CustomEditorType.FolderBrowserEditor)
                    e.Row.Properties.RowEdit = _browserFolderDialogEdit;
                }
            }
        }

        /**/

        private void openFileDialogButtonClick(object sender, ButtonPressedEventArgs e)
        {
            using (var frm = new OpenFileDialog())
            {
                // Set start folder
                var path = propertyGrid1.EditingValue.ToString();

                // Get new path
                if ((frm.ShowDialog() == DialogResult.OK) && (frm.SafeFileName != ""))
                {
                    propertyGrid1.EditingValue = new MFileNameEditor(frm.SafeFileName);
                }
            }
        }
    }
}