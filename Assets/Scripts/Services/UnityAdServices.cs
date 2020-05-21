using System.Collections;
using _Scripts.State_Machine.States;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Services {
    public class UnityAdServices : MonoBehaviour
    {
        [SerializeField] private string gameId = "3393315";
        [SerializeField] private string bannerId = "InPlay";
        [SerializeField] private bool testMode = false;

        private void OnEnable()
        {
            GameState.OnEnterGameStateEvent += TryToShowBanner;
            
            GameState.OnExitGameStateEvent += TryToHideBanner;
            GameState.OnExitGameStateEvent += Advertisement.Show;
        }

        private void Start()
        {
            Advertisement.Initialize(gameId, testMode);
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        }

        private void OnDisable()
        {
            GameState.OnEnterGameStateEvent += TryToShowBanner;
            
            GameState.OnExitGameStateEvent += TryToHideBanner;
            GameState.OnExitGameStateEvent -= Advertisement.Show;
        }

        private void TryToShowBanner()
        {
            StartCoroutine (ShowBannerWhenReady ());
        }

        private void TryToHideBanner()
        {
            Advertisement.Banner.Hide();
        }
        
        private IEnumerator ShowBannerWhenReady () {
            while (!Advertisement.IsReady (bannerId)) {
                yield return new WaitForSeconds (0.5f);
            }
            Advertisement.Banner.Show (bannerId);
        }
    }
}
