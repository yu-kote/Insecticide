using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public int hp;
    public int invincible_time;
    public bool is_invincible;
    public int invincible_count;

    public int damage;

    void Start()
    {
        invincible_count = 0;
    }

    void Update()
    {
        if (is_invincible)
        {
            invincible_count++;
            if (invincible_count > invincible_time)
            {
                invincible_count = 0;
                is_invincible = false;
            }
        }
    }

    public void hit(int damage_)
    {
        if (damage_ <= 0) return;
        if (is_invincible) return;
        hp -= damage_;
        damage = damage_;
        is_invincible = true;
        if (hp <= 0)
            hp = 0;
    }
}
