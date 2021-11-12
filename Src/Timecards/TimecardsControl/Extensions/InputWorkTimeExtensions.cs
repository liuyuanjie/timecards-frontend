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
            var count = controls
                .Cast<Control>()
                .Count(x => x.GetType() == typeof(InputWorkTimeControl));

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
            controls
                .Cast<Control>()
                .Where(x => x.GetType() == typeof(InputWorkTimeControl))
                .ToList()
                .ForEach(x =>
                {
                    x.Location = new Point(_defaultPosition, _defaultPosition + index * _controlHigh);
                    index++;
                });
        }

        public static void RemoveAllInputWorkTimes(this Control.ControlCollection controls)
        {
            var items = controls
                .Cast<Control>()
                .Where(x => x.GetType() == typeof(InputWorkTimeControl))
                .Cast<InputWorkTimeControl>()
                .ToList();
            items.ForEach(x => controls.Remove(x));
        }
    }
}