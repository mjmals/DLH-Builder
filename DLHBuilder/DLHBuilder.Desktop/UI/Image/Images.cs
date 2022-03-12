﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLHBuilder.Desktop.UI
{
    static class Images
    {
        public static ImageList Items
        {
            get
            {
                ImageList output = new ImageList();
                output.Images.Add("Column", Properties.Resources.Column_16x);
                output.Images.Add("Connection", Properties.Resources.Plugged_16x);
                output.Images.Add("Data Application", Properties.Resources.DatabaseApplication_16x);
                output.Images.Add("Data Artifact", Properties.Resources.DatabaseStoredProcedures_16x);
                output.Images.Add("Data Source", Properties.Resources.DataSourceReference_16x);
                output.Images.Add("Data Stage", Properties.Resources.RouteService_16x);
                output.Images.Add("Folder Closed", Properties.Resources.FolderClosed_16x);
                output.Images.Add("Folder Open", Properties.Resources.FolderOpened_16x);
                output.Images.Add("Open", Properties.Resources.OpenFile_16x);
                output.Images.Add("Project", Properties.Resources.ProjectFilterFile_16x);
                output.Images.Add("Run", Properties.Resources.Run_16x);
                output.Images.Add("Save", Properties.Resources.Save_16x);
                output.Images.Add("Schema Item", Properties.Resources.PropertySnippet_16x);
                output.Images.Add("SQL", Properties.Resources.SQL_16x);
                output.Images.Add("Table", Properties.Resources.Table_16x);
                return output;
            }
        }
    }
}
