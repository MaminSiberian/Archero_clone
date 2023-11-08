
namespace UI
{
    public class UnpauseButton : ButtonBase
    {
        private UIManager manager;

        private void Start()
        {
            manager = FindObjectOfType<UIManager>();
        }
        protected override void OnButtonClick()
        {
            manager.UnpauseGame();
        }
    }
}
