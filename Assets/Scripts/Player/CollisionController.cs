using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class CollisionController : MonoBehaviour
    {
        [Header("Collision Check")] 
        [SerializeField] private List<string> objectTags;
        private void OnCollisionEnter(Collision other)
        {
            if (objectTags.Contains(other.gameObject.tag))
            {
                Debug.Log($"Died by {other.gameObject.tag}");
                GameManager.StopGame();
                //gameObject.SetActive(false);
            }
        }
    }
}
