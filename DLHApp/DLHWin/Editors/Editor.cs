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
            this.Resize += OnResize;
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

            for(int i = 0; i < controls.Count(); i++)
            {
                Control control = controls[i];
                control.Width = this.Width;
                control.Height = Convert.ToInt32(this.Height / controls.Count());
                control.Location = new Point(0, i == 0 ? 0 : Controls[i-1].Bottom);
                Controls.Add(control);
            }
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
