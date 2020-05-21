using UnityEngine;

namespace UI.Controllers
{
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    // ReSharper disable once InconsistentNaming
    public class UIValueController : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TextMeshProUGUI value;

        private void Awake()
        {
            value = GetComponent<TMPro.TextMeshProUGUI>();
        }

        /// <summary>
        /// Sets given integer value to "text".
        /// </summary>
        /// <param name="value">int-only</param>
        // ReSharper disable once ParameterHidesMember
        public void SetValue(int value)
        {
            this.value.text = value == -1 
                ? "Tutorial" 
                : value.ToString();
        }

        /// <summary>
        /// Sets given integer values to "text" in a given debug conditions.
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="selectedValue"></param>
        public void SetValue(int currentValue, int selectedValue)
        {
            this.value.text = selectedValue == -1
                ? $"{currentValue} / Tutorial"
                : $"{currentValue} / {selectedValue}";
        }

        /// <summary>
        /// Gets current "text" value as integer.
        /// </summary>
        /// <returns>int-only</returns>
        public int GetValue() { return System.Convert.ToInt32(value.text); }

        /// <summary>
        /// Increment "text" value by given integer.
        /// </summary>
        /// <param name="by">Default 1.</param>
        public void Increment(int by = 1) { value.text = (GetValue() + by).ToString(); }

        /// <summary>
        /// Decrement "text" value by given integer.
        /// </summary>
        /// <param name="by">Default 1.</param>
        public void Decrement(int by = 1)
        {
            value.text = GetValue() + (-1 * by) >= 0 
                ? (GetValue() + (-1 * by)).ToString() : 
                0.ToString();
        }
    }
}
