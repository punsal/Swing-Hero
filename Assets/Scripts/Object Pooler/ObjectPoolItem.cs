using UnityEngine;

namespace Assets.Scripts.Object_Pooler
{
    [System.Serializable]
    public struct ObjectPoolItem
    {
        public int amountToPool;
        public GameObject objectToPool;
        public PoolingType poolingType;
    }
}
