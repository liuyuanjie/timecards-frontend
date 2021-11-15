using System.Linq;
using System.Windows.Forms;

namespace TimecardsControl.Extensions
{
    public static class FormControlsExtensions
    {
        public static Control FindControl(this Control.ControlCollection controls, string controlName)
        {
            var foundControl = controls.Find(controlName, true);
            return foundControl.FirstOrDefault();
        }
    }
}