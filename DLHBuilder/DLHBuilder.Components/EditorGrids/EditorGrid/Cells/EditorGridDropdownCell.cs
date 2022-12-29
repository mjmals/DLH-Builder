using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace DLHBuilder.Components.EditorGrids
{
    class EditorGridDropdownCell : DataGridViewComboBoxCell, IEditorGridCell
    {
        public object BaseObject { get; set; }

        public string PropertyName { get; set; }

        public object PropertyValue
        {
            get
            {
                return DatasourceDefinition.Values()[(string)Value];
            }
            set
            {
                Value = value;
            }
        }

        public bool AllowEdit => true;

        public Type ColumnType => typeof(DataGridViewComboBoxColumn);

        public EditorGridDatasourceDefinition DatasourceDefinition { get; set; }

        void SetDatasource()
        {
            foreach(var item in DatasourceDefinition.Values())
            {
                if(item.Value is string)
                {
                    Items.Add(item.Value);
                }
                else
                {
                    DisplayMember = DatasourceDefinition.DisplayProperty;
                    Items.Add(item.Value);
                }
            }
        }

        public void SetProperties(object baseObject, string propertyName, object propertyValue, EditorGridDatasourceDefinition dataSource = null)
        {
            BaseObject = baseObject;
            PropertyName = propertyName;

            if(dataSource != null)
            {
                DatasourceDefinition = dataSource;
                SetDatasource();
                Value = propertyValue.ToString();
            }
        }

        public void ProcessCellUpdate()
        {
            EditorGridPropertyUpdater.Run(BaseObject, PropertyName, PropertyValue);
        }
    }
}
