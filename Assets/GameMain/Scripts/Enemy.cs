using UnityEngine;
using System.Collections;

/// <summary>
/// エネミー一体一体が持っているクラス（ステータス管理）
/// </summary>
public class Enemy : MonoBehaviour
{
    public float hp;
    public float hp_max;
    public int invincible_time;
    public bool is_invincible;
    public int invincible_count;

    public float damage;

    void Awake()
    {
        hpSetup();
    }
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

    // HPを最大値にもどす関数
    public void hpSetup()
    {
        hp = hp_max;
    }

    // 攻撃が当たった時にエネミーのHPを減らす処理
    public void hit(float damage_)
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
