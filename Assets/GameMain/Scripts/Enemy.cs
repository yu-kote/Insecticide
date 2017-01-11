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
        materialReset();
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
                materialReset();
            }
        }
        damageStaging();

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
        hitPlay();
        hp -= damage_;
        damage = damage_;
        is_invincible = true;
        if (hp <= 0)
            hp = 0;
    }

    [SerializeField]
    AudioSource hit_mp3;

    public void hitPlay()
    {
        hit_mp3.Play();
    }

    public Material[] material = new Material[2];
    bool is_damagestaging;

    void materialReset()
    {
        gameObject.GetComponent<MeshRenderer>().material = material[0];
    }

    void damageStaging()
    {
        if (!is_invincible) return;
        if (invincible_count % 5 == 0)
        {
            is_damagestaging = !is_damagestaging;
        }

        if (is_damagestaging)
            gameObject.GetComponent<MeshRenderer>().material = material[0];
        else
            gameObject.GetComponent<MeshRenderer>().material = material[1];
    }
}
