using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHWin.Editors
{
    internal abstract class Editor : TabPage
    {
        public Editor()
        {
            SetControls();
        }

        protected virtual Control[] EditorControls()
        {
            return new Control[0];
        }

        protected virtual void SetControls()
        {
            Controls.Clear();
            Control[] controls = EditorControls();

            if(controls.Count() == 1)
            {
                Controls.Add(controls[0]);
                Controls[0].Dock = DockStyle.Fill;
                return;
            }

            SplitContainer bodyPanel = new SplitContainer() { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal };
            
            bodyPanel.Panel1.Controls.Add(controls[0]);
            bodyPanel.Panel1.Controls[0].Dock = DockStyle.Fill;

            bodyPanel.Panel2.Controls.Add(controls[1]);
            bodyPanel.Panel2.Controls[0].Dock = DockStyle.Fill;

            Controls.Add(bodyPanel);
        }

        void OnResize(object sender, EventArgs e)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                Control control = Controls[i];
                control.Width = this.Width;
                control.Height = Convert.ToInt32(this.Height / Controls.Count);
                control.Location = new Point(0, i == 0 ? 0 : Controls[i - 1].Bottom);
            }
        }
    }
}
