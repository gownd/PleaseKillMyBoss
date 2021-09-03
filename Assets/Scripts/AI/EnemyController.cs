using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss.Movement;
using Boss.Combat;
using Boss.Object;

namespace DD.AI
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 1f;

        Animator animator;
        AnimatorOverrideController overrideController;
        Fighter fighter;
        ActionObject actionObject;

        GameObject hero;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            fighter = GetComponent<Fighter>();
            actionObject = GetComponent<ActionObject>();
        }

        private void Start()
        {
            hero = GameObject.FindGameObjectWithTag("Hero");
        }

        private void Update()
        {
            if(!actionObject.CanInteract()) return;

            if(InAttackRangeOfPlayer() && fighter.CanAttack(hero))
            {
                fighter.Attack(hero);
            }
            else
            {
                fighter.Cancel();
            }
        }   

        private void OnDrawGizmos() 
        {
            Gizmos.DrawWireSphere(transform.position, chaseDistance);    
        }

        bool InAttackRangeOfPlayer()
        {
            if(!hero.GetComponent<Health>().IsAlive()) return false;
            float distanceToHero = Mathf.Abs(transform.position.x - hero.transform.position.x);

            return distanceToHero <= chaseDistance;
        }
    }
}

