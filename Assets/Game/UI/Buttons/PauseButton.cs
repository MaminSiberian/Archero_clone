
namespace UI
{
    public class PauseButton : ButtonBase
    {
        private UIManager manager;

        private void Start()
        {
            manager = FindObjectOfType<UIManager>();
        }
        protected override void OnButtonClick()
        {
            manager.PauseGame();
        }
    }
}
