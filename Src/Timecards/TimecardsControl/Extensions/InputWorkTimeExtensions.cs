using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TimecardsControl.Extensions
{
    public static class InputWorkTimeExtensions
    {
        private static int _controlHigh = 105;
        private static int _defaultPosition = 3;

        public static InputWorkTimeControl AddInputWorkTimeControl(this Control.ControlCollection controls,
            InputWorkTime inputWorkTime)
        {
            var count = FindAllInputWorkTimeControls(controls).Count();

            var inputWorkTimeControl = new InputWorkTimeControl(inputWorkTime)
            {
                Location = new Point(_defaultPosition, _defaultPosition + count * _controlHigh)
            };
            controls.Add(inputWorkTimeControl);

            return inputWorkTimeControl;
        }

        public static void RemoveInputWorkTimeControl(this Control.ControlCollection controls,
            InputWorkTimeControl control)
        {
            controls.Remove(control);

            var index = 0;
            FindAllInputWorkTimeControls(controls)
                .ToList()
                .ForEach(x =>
                {
                    var controlPositionHigh = index * _controlHigh;
                    x.Location = new Point(_defaultPosition, _defaultPosition + controlPositionHigh);
                    index++;
                });
        }

        public static void RemoveAllInputWorkTimes(this Control.ControlCollection controls)
        {
            var items = FindAllInputWorkTimeControls(controls)
                .Cast<InputWorkTimeControl>()
                .ToList();
            items.ForEach(x => controls.Remove(x));
        }

        private static IEnumerable<Control> FindAllInputWorkTimeControls(Control.ControlCollection controls)
        {
            return controls
                .Cast<Control>()
                .Where(x => x.GetType() == typeof(InputWorkTimeControl));
        }
    }
}