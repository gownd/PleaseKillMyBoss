using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss.Core;
using Boss.Movement;

namespace Boss.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float damage = 10f;
        [SerializeField] float attackRange = 1f;

        public Health target;

        Animator animator;
        Mover mover;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (!GetComponent<Health>().IsAlive() || target == null) return;

            if (!IsInRange())
            {
                mover.RunTo(target.transform);
            }
            else
            {
                animator.SetBool("isAttacking", true);
                mover.Cancel();
                // AttackBehaviour();
            }
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            target = null;

            animator.SetBool("isAttacking", false);

            GetComponent<Mover>().Cancel();
        }

        public bool IsInRange()
        {
            return Mathf.Abs(transform.position.x - target.transform.position.x) <= attackRange;
        }

        public void Hit() // attack 애니메이션에서 실행됨
        {
            if (!target.IsAlive()) return;

            target.TakeDamage(damage);
            if (!target.IsAlive()) Cancel();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;

            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && targetToTest.IsAlive();
        }
    }

}
