using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityGameObjectEvent : UnityEvent<GameObject> { }

namespace Boss.Core
{
    public class BEventListener : MonoBehaviour
    {
        public BEvent gEvent;
        public UnityGameObjectEvent response = new UnityGameObjectEvent();

        private void OnEnable()
        {
            gEvent.Register(this);
        }

        private void OnDisable()
        {
            gEvent.Unregister(this);
        }

        public void OnEventOccurs(GameObject go)
        {
            response.Invoke(go);
        }
    }

}
