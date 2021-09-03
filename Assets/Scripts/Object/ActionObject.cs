using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss.Core;

namespace Boss.Object
{
    public class ActionObject : MonoBehaviour
    {
        [SerializeField] float interactRange;
        [SerializeField] BEvent actionEnded;
        public ObjectType type;

        bool canInteract = true;
        bool isTarget = false;

        private void Start() 
        {
            canInteract = true;
            isTarget = false;       
        }

        public bool CanInteract()
        {
            return canInteract;
        }

        public void Enable()
        {
            canInteract = true;
        }

        public void Disable()
        {
            canInteract = false;
        }

        public void SetTarget(bool onoff)
        {
            isTarget = onoff;
        }

        public bool IsTarget()
        {
            return isTarget;
        }

        public void StartActionWithThisObject()
        {
            SetTarget(true);
            // actionStarted.Occurred(this.gameObject);
        }

        public void EndActionWithThisObject()
        {
            Disable();
            SetTarget(false);
            actionEnded.Occurred(this.gameObject);
        }

        public bool IsInRange(Transform actor)
        {
            return Mathf.Abs(actor.position.x - transform.position.x) <= interactRange;
        }
    }

    public enum ObjectType
    {
        Enemy, Others
    }
}
