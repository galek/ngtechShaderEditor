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
using System;
using System.Windows.Forms;

namespace NGEd
{
    public partial class ViewPort : DevExpress.XtraEditors.PanelControl
    {
        /// <summary>
        ///
        /// </summary>
        public ViewPort()
        {
            InitializeComponent();
            AutoSize = true;
            Dock = System.Windows.Forms.DockStyle.Fill;
        }

        /// <summary>
        ///
        /// </summary>
        public void InitEngine()
        {
            //if (engine == null)
            //{
            //    engine = new EngineCLR.EngineCLR(Handle.ToInt32());
            //    engine.EngineInit();
            //    ResizeEngine(Width, Height);
            //}
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_Width"></param>
        /// <param name="_Height"></param>
        public void ResizeEngine(int _Width, int _Height)
        {
            //if (engine == null)
            //    return;

            //engine.Resize(_Width, _Height);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewPort_AutoSizeChanged(object sender, EventArgs e)
        {
            ResizeEngine(Width, Height);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewPort_Resize(object sender, EventArgs e)
        {
            //if (engine == null)
            //    return;

            //ResizeEngine(Width, Height);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewPort_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                if (e.Button == MouseButtons.Right)
                    MouseRightBActions(sender, e);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseRightBActions(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //if (engine == null)
            //    return;

            //if (isMouseDown)
            //    CameraActions(1, e.X - Width / 2, e.Y - Height / 2);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewPort_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_action"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void CameraActions(int _action, int x, int y)
        {
            //switch (_action)
            //{
            //    case 1:
            //        engine.CameraSetDirection(x, y);
            //        break;

            //    case 2:
            //        //Nick:TODO("Actions")
            //        break;

            //    default:
            //        //Nick:TODO("Actions")
            //        break;
            //}
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected override bool ProcessKeyPreview(ref Message msg)
        {
            //if (engine == null)
            //    return false;

            //const int WM_KEYUP = 0x0101;
            //const int WM_KEYDOWN = 0x0100;

            //if (msg.Msg == WM_KEYDOWN)
            //{
            //    isMouseDown = true;
            //    engine.KeyDown((int)(Keys)msg.WParam);
            //}
            //else if (msg.Msg == WM_KEYUP)
            //{
            //    isMouseDown = false;
            //    engine.KeyUp((int)(Keys)msg.WParam);
            //}

            return base.ProcessKeyPreview(ref msg);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewPort_MouseDown(object sender, MouseEventArgs e)
        {
            if (popupMenu1 != null)
                popupMenu1.HidePopup();

            isMouseDown = true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewPort_DoubleClick(object sender, MouseEventArgs e)
        {
            if (engine == null)
                return;

            // Если захвачен, то выполняем функции редактора
            EDITORActionsDoubleClick(e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        private void EDITORActionsDoubleClick(MouseEventArgs e)
        {
            if (popupMenu1 == null)
                return;

            if (e.Button == MouseButtons.Middle)
                popupMenu1.ShowPopup(barManager1, Control.MousePosition);
        }

        /// <summary>
        ///
        /// </summary>
        //public EngineCLR.EngineCLR Engine
        //{
        //    get { return engine; }
        //    set { engine = value; }
        //}

        public PopupControl PopUpMenu
        {
            get { return popupMenu1; }
            set { popupMenu1 = value; }
        }

        /// <summary>
        ///
        /// </summary>
        private bool isMouseDown = false;//флаг нажатия кнопки мыши

        /// <summary>
        ///
        /// </summary>
        //private EngineCLR.EngineCLR engine = null;

        private PopupControl popupMenu1 = null;
    }
}