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
    public partial class MainProperties : DevExpress.XtraEditors.PanelControl
    {
        public MainProperties()
        {
            InitializeComponent();
            AutoSize = true;
            Dock = System.Windows.Forms.DockStyle.Fill;
            Text = "Properties";
        }
    }
}