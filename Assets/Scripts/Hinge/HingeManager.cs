using _Scripts.State_Machine.States;
using Spawners;
using UnityEngine;

namespace Hinge
{
    public class HingeManager : Spawner
    {
        private Joint selectedJoint;

        private int index;
        private float[] distances;

        private int recycleAmount = 0;

        private void OnEnable()
        {
            GameState.OnEnterGameStateEvent += Prepare;
            GameState.OnExecuteGameStateEvent += Recycle;
            GameState.OnExitGameStateEvent += Reset;

            GameManager.OnClicked += GetClosestHinge;
            GameManager.OnRelease += Release;
        }

        private void OnDisable()
        {
            GameState.OnEnterGameStateEvent -= Prepare;
            GameState.OnExecuteGameStateEvent -= Recycle;
            GameState.OnExitGameStateEvent -= Reset;

            GameManager.OnClicked -= GetClosestHinge;
            GameManager.OnRelease -= Release;
        }

        private (Joint, int) GetClosestHinge(Vector3 position)
        {
            distances = new float[Objects.Count];
            var hingesArray = Objects.ToArray();
            for (var i = 0; i < distances.Length; i++)
            {
                distances[i] = Vector3.Distance(hingesArray[i].position, position);
            }

            index = 0;
            var minDistance = distances[0];
            for (var i = 0; i < distances.Length; i++)
            {
                if (!(distances[i] < minDistance)) continue;
                if (!(player.position.x < hingesArray[i].position.x)) continue;
                
                minDistance = distances[i];
                index = i;
            }

            var score = index + recycleAmount;
            recycleAmount = 0;

            selectedJoint = hingesArray[index].GetComponent<Joint>();

            return (selectedJoint, score);
        }

        private void Release()
        {
            selectedJoint.connectedBody = null;
        }

        public override void Recycle()
        {
            if (!(Objects.Peek().position.x + recycleOffset < player.position.x)) return;
            var temp = Objects.Dequeue();
            temp.rotation = Quaternion.identity;
            temp.gameObject.SetActive(false);
            
            Spawn();
            recycleAmount++;
        }

        public override void Reset()
        {
            for (var i = 0; i < Objects.Count; i++)
            {
                var temp = Objects.Dequeue();
                temp.GetComponent<Joint>().connectedBody = null;
                temp.transform.position = Vector3.zero;
                temp.rotation = Quaternion.identity;
            }

            spawnPoint.position = InitialSpawnPoint;
        }
    }
}
