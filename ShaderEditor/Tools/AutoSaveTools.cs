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

//using EngineCLR;
using System;
using System.Windows.Forms;

namespace NGEd.Tools
{
    internal class AutoSaveTools
    {
        //public
        //EngineCLR.EngineCLR GetEngine
        //{
        //    get
        //    {
        //        return m_engine;
        //    }
        //    set
        //    {
        //        m_engine = value;
        //    }
        //}

        //public
        //EditorState GetState
        //{
        //    get
        //    {
        //        return m_state;
        //    }
        //    set
        //    {
        //        m_state = value;
        //    }
        //}

        public
        bool TimerEnabled
        {
            get { return m_TimerEnabled; }
            set { m_TimerEnabled = value; }
        }

        public void SaveSnapshot(bool auto_save)
        {
            // Save the snapshot.
            //UndoList.Push(GetSnapshot());

            // Empty the redo list.
            //if (RedoList.Count > 0) RedoList = new Stack();

            // Enable or disable the Undo and Redo menu items.
            //EnableUndo();

            // Auto-save.
            if (auto_save) AutoSaveScene();
        }

        /// <summary>
        ///
        /// </summary>
        public void AskAboutSaveScene()
        {
            //if (GetEngine.CountofObjectsOnScene() == 0 || GetState.IsSceneModificated == false)
            //    return;

            if (XtraMessageBox.Show("Scene was modificated. Save at?", "Save at?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Save();
            }
        }

        public void AskAboutLoadAutoSaveScene()
        {
            // See if the file exists.
            //if (File.Exists(m_state.CurrentSceneName + m_bakName))
            //{
            //    // Ask the user if we should load this file.
            //    if (XtraMessageBox.Show(
            //        "An auto-save file exists. Do you want to load it?",
            //        "Restore?", MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Question)
            //        == DialogResult.Yes)
            //    {
            //        // Load the file.
            //        LoadFromFile(m_state.CurrentSceneName + m_bakName);
            //    }
            //}
        }

        private void LoadFromFile(string _file)
        { }

        private void Save()
        {
            //if (GetEngine == null || GetState == null)
            //{
            //    return;
            //}

            //// останавливаем симуляцию
            //m_state.IsSimPaused = true;
            //_SaveScene(String.Empty, String.Empty);

            //// Deletion of existing bak file
            //if (File.Exists(m_state.CurrentSceneName + m_bakName))
            //    File.Delete(m_state.CurrentSceneName + m_bakName);
        }

        // Auto-save.
        public void AutoSaveScene()
        {
            if (TimerEnabled)
            {
                _SaveScene(String.Empty, m_bakName);
            }
        }

        private void _SaveScene(string _prefix, string _postExt)
        {
            //if (m_engine == null || m_state == null || String.IsNullOrEmpty(m_state.CurrentSceneName) ||
            //    m_state.CurrentSceneName == "EMPTY"
            //    || m_state.IsSimPaused == false
            //    )
            //{
            //    return;
            //}

            //SaveLoadHelper.SaveEngineFormat(_prefix + m_state.CurrentSceneName + _postExt, SaveLoadHelper.Type.SCENE);
        }

        //private
        //EngineCLR.EngineCLR m_engine;

        //private
        //EditorState m_state;

        private
        string m_bakName = ".bak";

        private
        bool m_TimerEnabled = true;
    }
}