using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Util.ObjectPooling
{
    public class ObjectPoolContainer : MonoBehaviour
    {
        void OnDestroy()
        {
            List<GameObject> childrenGameObjectList = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                childrenGameObjectList.Add(child.gameObject);
            }

            foreach (GameObject childGameObject in childrenGameObjectList)
            {
                Destroy(childGameObject);
            }
        }
    }
}