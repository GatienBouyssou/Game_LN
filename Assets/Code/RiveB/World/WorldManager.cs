using System.Collections;
using UnityEngine;

namespace Assets.Code.RiveB.World
{
    using UnityEngine;
    using System.Collections.Generic;

    public class WorldManager : MonoBehaviour
    {
        private struct ObjectState
        {
            public GameObject prefab;
            public Vector3 position;
            public Quaternion rotation;
        }

        private List<ObjectState> initialObjects = new List<ObjectState>();

        void Start()
        {
            // SaveInitialSceneState(); Foncionne po
        }

        private void SaveInitialSceneState()
        {
            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
            {
                if (obj.CompareTag("Player Spawn")) continue;

                ObjectState state = new ObjectState
                {
                    prefab = obj,
                    position = obj.transform.position,
                    rotation = obj.transform.rotation
                };
                initialObjects.Add(state);
            }
        }

        public void ResetScene()
        {
            foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
            {
                if (obj != this.gameObject && !obj.CompareTag("Player Spawn"))
                {
                    Destroy(obj);
                }
            }

            foreach (ObjectState state in initialObjects)
            {
                Instantiate(state.prefab, state.position, state.rotation);
            }
        }
    }

}