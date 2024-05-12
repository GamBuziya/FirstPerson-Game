using DefaultNamespace.Abstract_classes;
using UnityEngine;

namespace DefaultNamespace.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Magic Attacks/Venom Attack")]
    public class VenomAttackSO : MagicAttackSO
    {
        public override void DebafFunc(GameCharacter gameCharacter)
        {
            //gameCharacter.StaminaDamage(30);
        
            var temp = gameCharacter is IMagic;
            if (temp)
            {
                IMagic magicCharacter = (IMagic)gameCharacter;
                magicCharacter.MagicManager.MagicDamage();
            }
        }
    }
}