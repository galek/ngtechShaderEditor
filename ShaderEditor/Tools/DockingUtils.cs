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
using System.IO;

namespace NGEd.Tools
{
    public class DockingUtils
    {
        /**/
        public static string editorPath = "../Editor/Configs/";

        public static void CreateEditorFolderIfNotExist()
        {
            // Determine whether the directory exists.
            if (Directory.Exists(DockingUtils.editorPath))
            {
                Console.WriteLine("Editor path exists already.");
                return;
            }

            // Try to create the directory.
            DirectoryInfo di = Directory.CreateDirectory(DockingUtils.editorPath);
            Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(DockingUtils.editorPath));
        }

        public static void LoadLayoutConfiguration(string _path, DevExpress.XtraBars.Docking.DockManager _dock)
        {
            try
            {
                _dock.RestoreLayoutFromXml(DockingUtils.editorPath + _path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Saves editor settings
        /// </summary>
        public static void SaveLayoutConfiguration(string _path, DevExpress.XtraBars.Docking.DockManager _dock)
        {
            try
            {
                _dock.SaveLayoutToXml(DockingUtils.editorPath + _path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}