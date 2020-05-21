using _Scripts.State_Machine.States;
using Object_Pooler;
using UnityEngine;

namespace Spawners
{
    public class GroundSpawner : Spawner
    {
        [Header("Skyline Properties")]
        [SerializeField] private float scaleFactor = 10f;

        private void OnEnable()
        {
            GameState.OnEnterGameStateEvent += Prepare;
            GameState.OnExecuteGameStateEvent += Recycle;
            GameState.OnExitGameStateEvent += Reset;

        }

        private void OnDisable()
        {
            GameState.OnEnterGameStateEvent -= Prepare;
            GameState.OnExecuteGameStateEvent -= Recycle;
            GameState.OnExitGameStateEvent -= Reset;

        }

        public override void Spawn(int length)
        {
            var objectPooler = ObjectPooler.SharedInstance;
            for (var i = 0; i < length; i++)
            {
                var spawnPosition = spawnPoint.position;

                var randomVerticalFactor = Random.Range(1f, verticalGap) * scaleFactor;
                var randomHorizontalFactor = Random.Range(1f, horizontalGap) * scaleFactor;

                var objectScale =
                    Vector3.right * randomHorizontalFactor +
                    Vector3.up * randomVerticalFactor +
                    Vector3.forward * scaleFactor;

                spawnPosition.x += objectScale.x * 0.5f;
                spawnPoint.position = spawnPosition;

                var temp = objectPooler.GetPooledObject(objectTag);
                temp.SetActive(true);
                temp.transform.position = spawnPosition + 0.5f * objectScale.y * Vector3.up;
                temp.transform.localScale = objectScale;

                Objects.Enqueue(temp.transform);
            }
        }
    }
}
