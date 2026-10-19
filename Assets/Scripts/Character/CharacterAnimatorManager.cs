using UnityEngine;

namespace SG{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character;

        float horizontal_value;
        float vertical_value;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public void UpdateAnimatorMovementParameters(float horizontal_value, float vertical_value)
        {
            character.animator.SetFloat("Horizontal", horizontal_value, 0.1f, Time.deltaTime);
            character.animator.SetFloat("Vertical", vertical_value, 0.1f, Time.deltaTime);
        }
    }
}