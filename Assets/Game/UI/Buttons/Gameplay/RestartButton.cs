using UnityEngine.SceneManagement;

namespace UI
{
    public class RestartButton : ButtonBase
    {
        protected override void OnButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
