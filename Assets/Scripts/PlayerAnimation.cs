using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlatformer
{
    public class PlayerAnimation : BaseCharacterAnimation
    {
        [SerializeField] Movement _movement;
        [SerializeField] CharacterCollision _characterCollision;
        [SerializeField] BaseCombatAbility _combatAbility;
        [SerializeField] string idleName = "Idle";
        [SerializeField] string runName = "Run";
        [SerializeField] string airName = "Air";

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimations(Time.deltaTime);
        }

        void UpdateAnimations(float dt)
        {
            if (_combatAbility.IsAttacking)
            {
                return;
            }

            if (_characterCollision.OnGround)
            {
                if (_movement.IsMovingHorizontally)
                {
                    PlayAnimIfNotAlreadyPlaying(runName);
                }
                else
                {
                    PlayAnimIfNotAlreadyPlaying(idleName);
                }
            }
            else
            {
                PlayAnimIfNotAlreadyPlaying(airName);
            }
        }
    }
}
