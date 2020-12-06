using UnityEngine;

public class GroundedDetection : MonoBehaviour
{
    // Start is called before the first frame update
    [System.NonSerialized] public bool m_Grounded = false;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = 0.5f;

    private void FixedUpdate()
    {
        bool foundGround = false;
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                foundGround = true;
                m_Grounded = true;
            }
        }
        if (!foundGround){
            m_Grounded = false;
        }
    }
}
