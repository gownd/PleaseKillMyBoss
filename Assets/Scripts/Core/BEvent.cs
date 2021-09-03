using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss.Core
{
    [CreateAssetMenu(fileName = "BEvent", menuName = "Boss/BEvent", order = 0)]
    public class BEvent : ScriptableObject
    {
        List<BEventListener> elisteners = new List<BEventListener>();

        public void Register(BEventListener listener)
        {
            elisteners.Add(listener);
        }

        public void Unregister(BEventListener listener)
        {
            elisteners.Remove(listener);
        }

        public void Occurred(GameObject go)
        {
            for (int i = 0; i < elisteners.Count; i++)
            {
                elisteners[i].OnEventOccurs(go);
            }
        }
    }
}
