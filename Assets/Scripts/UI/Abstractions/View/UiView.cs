using UI.Abstractions.View;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Abstractions.Views
{
    public class UiView : UIBehaviour, IUiView
    {
        [field: SerializeField] public bool AutoShow { get; private set; }

        public virtual void Show()
        {
            gameObject?.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject?.SetActive(false);
        }

    }
}
