using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructs;
using DLHApp.Model.DataTypes;
using DLHApp.Model.DataTypes.Converters;

namespace DLHWin.Grids.DataStructs
{
    internal class DataStructEditorGridDataTypeCell : EditorGridTextCell
    {
        public override void UpdateBaseProperty(object baseObject)
        {
            DataStructField field = (DataStructField)baseObject;

            try
            {
                field.DataType = new DataTypeParser(Value.ToString()).Parse();
            }
            catch
            {
                MessageBox.Show(string.Format("Could not parse \"{0}\" to a recognised Data Type.", Value.ToString()));
                return;
            }
        }
    }
    }
