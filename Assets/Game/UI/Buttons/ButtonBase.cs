using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class ButtonBase : MonoBehaviour
    {
        protected Button button;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
        }
        protected virtual void OnEnable()
        {
            button.onClick.AddListener(OnButtonClick);
        }
        protected virtual void OnDisable()
        {
            button?.onClick.RemoveListener(OnButtonClick);
        }
        protected abstract void OnButtonClick();
    }
}
