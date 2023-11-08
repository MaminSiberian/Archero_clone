using UnityEngine.SceneManagement;

namespace UI
{
    public class PlayButton : ButtonBase
    {
        protected override void OnButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
