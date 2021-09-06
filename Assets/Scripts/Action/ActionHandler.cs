using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss.Object;
using Boss.Movement;

namespace Boss.Action
{
    public class ActionHandler : MonoBehaviour
    {
        [SerializeField] Action action;

        public void Interact() // Event Listner에서 실행
        {
            ActionObject myObject = GetComponent<ActionObject>();

            if (myObject.IsTarget())
            {
                GameObject hero = GameObject.FindWithTag("Hero");
                if(hero == null) return;
                
                action.Handle(myObject, hero);
            }
        }
    }
}

