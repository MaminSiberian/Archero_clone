using UnityEngine.SceneManagement;

namespace UI
{
    public class QuitLevelButton : ButtonBase
    {
        protected override void OnButtonClick()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
