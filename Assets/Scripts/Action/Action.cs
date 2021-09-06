using UnityEngine;
using Boss.Object;

namespace Boss.Action
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Handle(ActionObject target, GameObject hero);
    }
}
