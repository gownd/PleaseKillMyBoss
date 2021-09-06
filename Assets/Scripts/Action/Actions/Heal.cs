using UnityEngine;
using Boss.Object;

namespace Boss.Action
{
    [CreateAssetMenu(fileName = "Heal", menuName = "Boss/Action/Heal", order = 0)]
    public class Heal : Action
    {
        [SerializeField] float healAmount = 50f;

        public override void Handle(ActionObject myObject, GameObject hero)
        {
            hero.GetComponent<Health>().Heal(healAmount);

            myObject.EndActionWithThisObject();
        }
    }
}
