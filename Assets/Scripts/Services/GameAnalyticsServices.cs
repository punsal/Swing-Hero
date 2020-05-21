using GameAnalyticsSDK;
using UnityEngine;

namespace Services {
    public class GameAnalyticsServices : MonoBehaviour
    {
        private void Start()
        {
            GameAnalytics.Initialize();
        }
    }
}
