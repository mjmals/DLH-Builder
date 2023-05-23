using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLHApp.Model.DataStructs;

namespace DLHWin.Grids.DataStructs
{
    internal class DataStructEditorGridKeyTypeCell : EditorGridTextCell
    {
        public override void SetValue(object value)
        {
            if (value is DataStructFieldKeyTypeCollection)
            {
                DataStructFieldKeyTypeCollection keyTypes = (DataStructFieldKeyTypeCollection)value;

                if (keyTypes != null && keyTypes.Count > 0)
                {
                    Value = string.Join(",", keyTypes.Select(x => x.ToString()).ToArray());
                    return;
                }
            }

            if (value is string)
            {
                Value = value.ToString();
                return;
            }

            Value = string.Empty;
        }

        public override void UpdateBaseProperty(object baseObject)
        {
            string[] keyTypeStrings = Value.ToString().Split(",");
            List<DataStructFieldKeyType> keyTypes = new List<DataStructFieldKeyType>();

            foreach(string keyTypeString in keyTypeStrings.Where(x => !string.IsNullOrEmpty(x)))
            {
                try
                {
                    DataStructFieldKeyType keyType = Enum.Parse<DataStructFieldKeyType>(keyTypeString.Trim());
                    keyTypes.Add(keyType);
                }
                catch
                {
                    MessageBox.Show(string.Format("\"{0}\" is not a valid Key Type.", keyTypeString.Trim()));
                    return;
                }
            }

            DataStructField field = (DataStructField)baseObject;
            if (field.KeyTypes == null) field.KeyTypes = new DataStructFieldKeyTypeCollection();
            field.KeyTypes.Clear();
            field.KeyTypes.AddRange(keyTypes);
        }
    }
}
