using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public bool is_attack = false;
    int attack_count;
    int attack_delay;

    public int attack_power;

    private Animator anim;

    void Start()
    {
        is_attack = false;
        anim = GetComponent<Animator>();
        attack_delay = 30;
        attack_count = 0;
    }

    void Update()
    {
        anim.SetBool("IsAttack", is_attack);
        if (is_attack == false)
            if (Input.GetKeyDown(KeyCode.K))
            {
                is_attack = true;
            }
        if (is_attack)
        {
            attack_count++;
            if (attack_count >= attack_delay)
            {
                attack_count = 0;
                is_attack = false;
            }
        }

    }
}
