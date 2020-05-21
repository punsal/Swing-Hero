using System.Collections.Generic;
using Interfaces;
using Object_Pooler;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public abstract class Spawner : MonoBehaviour, ISpawn, IReset, IRecycle
    {
        [Header("Object To Pool")]
        [SerializeField] protected string objectTag;

        [Header("Object Follow")] 
        [SerializeField] protected Transform player;
        [SerializeField] protected float recycleOffset = 20f;

        [Header("Spawn Conditions")]
        [SerializeField] protected Transform spawnPoint;
        [SerializeField] protected float verticalTravel = 5f;
        [SerializeField] protected float verticalGap = 2.5f;
        [SerializeField] protected float horizontalGap = 1f;

        [Header("Hinge Queue")]
        [SerializeField] protected int queueLength = 10;
        protected Queue<Transform> Objects;

        #region Reset Information

        protected Vector3 InitialSpawnPoint;

        #endregion

        private void Awake()
        {
            InitialSpawnPoint = spawnPoint.position;
        }

        public virtual void Prepare()
        {
            Objects = new Queue<Transform>();
            Spawn(queueLength);
        }

        public virtual void Spawn(int length = 1)
        {
            var objectPooler = ObjectPooler.SharedInstance;
            for (var i = 0; i < length; i++)
            {
                var spawnPosition = spawnPoint.position;
                spawnPosition += Vector3.right * Random.Range(-1 * verticalGap, verticalGap);
                spawnPosition.x += verticalTravel;
                spawnPosition += Vector3.up * Random.Range(-1 * horizontalGap, horizontalGap);
                spawnPoint.position = spawnPosition;

                var temp = objectPooler.GetPooledObject(objectTag);
                temp.SetActive(true);
                temp.transform.position = spawnPosition;

                Objects.Enqueue(temp.transform);
            }
        }

        public virtual void Reset()
        {
            Debug.Log($"{name} Reset");
            for (var i = 0; i < Objects.Count; i++)
            {
                Objects.Dequeue().gameObject.SetActive(false);
            }

            spawnPoint.position = InitialSpawnPoint;
        }

        public virtual void Recycle()
        {
            if (!(Objects.Peek().position.x + recycleOffset < player.position.x)) return;
            var temp = Objects.Dequeue();
            temp.rotation = Quaternion.identity;
            temp.gameObject.SetActive(false);

            Spawn();
        }
    }
}
