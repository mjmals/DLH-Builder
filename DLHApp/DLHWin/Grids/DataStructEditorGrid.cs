using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructs;

namespace DLHWin.Grids
{
    internal class DataStructEditorGrid : EditorGrid
    {
        public DataStructEditorGrid(DataStruct dataStruct)
        {
            DataStruct = dataStruct;
        }

        public DataStruct DataStruct { get; set; }
    }
}
