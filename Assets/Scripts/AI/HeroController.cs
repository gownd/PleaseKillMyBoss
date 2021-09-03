using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss.Combat;
using Boss.Movement;
using Boss.Object;
using Boss.Core;

namespace Boss.AI
{
    public class HeroController : MonoBehaviour
    {
        [SerializeField] float sightRange = 1f;

        Fighter fighter;
        Health health;
        Mover mover;
        State state;

        bool isInteracting = false;
        ActionObject currentTarget = null;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
        }

        private void Start()
        {
            state = State.Walk;
        }

        private void OnDrawGizmos() 
        {
            Gizmos.DrawWireSphere(transform.position, sightRange);    
        }

        private void Update()
        {
            if (!health.IsAlive()) return;

            if (state == State.Walk)
            {
                isInteracting = false;
                mover.WalkRight();

                ActionObject closeObject = FindCloseObjectInRange();
                if(closeObject != null)
                {
                    StartAction(closeObject);
                }
            }
            else if (state == State.Interact)
            {
                if (currentTarget == null || currentTarget.type == ObjectType.Enemy) return;

                if (!isInteracting)
                {
                    MoveToInteract();
                }
                // Fight와 다른 Action은 별도의 class에서에서 실행   
            }
        }

        ActionObject FindCloseObjectInRange()
        {
            ActionObject[] actionObjects = FindObjectsOfType<ActionObject>();

            if (actionObjects.Length <= 0) return null;

            ActionObject closeTarget = null;
            float closeTargetDistance = 0f;
            int count = 0;

            foreach (ActionObject actionObject in actionObjects)
            {
                float distanceToObject = Mathf.Abs(transform.position.x - actionObject.transform.position.x);

                if(!actionObject.CanInteract() || distanceToObject > sightRange) continue;

                if (count == 0 || closeTargetDistance >= distanceToObject)
                {
                    closeTarget = actionObject;
                    closeTargetDistance = distanceToObject;
                    count++;
                }
            }

            return closeTarget;
        }

        void StartAction(ActionObject actionObject)
        {
            currentTarget = actionObject;

            ObjectType targetType = currentTarget.type;
            if (targetType == ObjectType.Enemy)
            {
                fighter.Attack(currentTarget.gameObject);
            }

            state = State.Interact;
        }

        void MoveToInteract()
        {
            if (!currentTarget.IsInRange(transform))
            {
                mover.RunTo(currentTarget.transform);
            }
            else
            {
                mover.Cancel();
                currentTarget.StartActionWithThisObject();
                isInteracting = true;
            }
        }

        public void GoToWalkState() // Event Listner에서 실행
        {
            StartCoroutine(HandleGoToWalkState());
        }

        IEnumerator HandleGoToWalkState()
        {
            currentTarget = null;
            yield return new WaitForSeconds(0.75f);
            state = State.Walk;
        }
    }

    enum State
    {
        Walk, Interact
    }
}

