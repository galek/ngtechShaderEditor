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

using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Graph
{
    sealed partial class SelectionForm : Form
    {
        public SelectionForm()
        {
            InitializeComponent();
        }

        public int SelectedIndex { get { return TextComboBox.SelectedIndex; } set { TextComboBox.SelectedIndex = value; } }

        public string[] Items
        {
            get
            {
                return (from item in TextComboBox.Items.OfType<string>() select item).ToArray();
            }
            set
            {
                TextComboBox.Items.Clear();
                if (value == null ||
                    value.Length == 0)
                    return;
                foreach (var item in value)
                    TextComboBox.Items.Add(item);
            }
        }

        private void OnSelectionFormLoad(object sender, EventArgs e)
        {
            TextComboBox.Focus();
        }
    }
}