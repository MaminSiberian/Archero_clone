using UnityEngine;

namespace UI
{
    public class ExitButton : ButtonBase
    {
        protected override void OnButtonClick()
        {
            Application.Quit();
        }
    }
}
