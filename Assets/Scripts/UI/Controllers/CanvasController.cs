using UI.Types;
using UnityEngine;

namespace UI.Controllers
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private UIType type;
        // ReSharper disable once ConvertToAutoProperty
        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        public UIType Type => type = UIType.Idle;

        // ReSharper disable once ParameterHidesMember
        public void Show(UIType type)
        {
            gameObject.SetActive(this.type == type);
        }
    }
}
