using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーを操作して、ステータスも管理するクラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    public bool is_attack = false;
    int attack_count;
    int attack_delay;

    public float attack_power;

    private Animator anim;

    GameObject wepon;

    void Start()
    {
        is_attack = false;
        anim = GetComponent<Animator>();
        attack_delay = 40;
        attack_count = 0;
        equipWepon("StandardSword");
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

    // プレハブから武器を装備する関数
    public void equipWepon(string weponname)
    {
        wepon = Resources.Load<GameObject>("Prefabs/" + weponname);
    }

    // プレイヤーの攻撃力と武器の攻撃力の合計値
    public float attackPower()
    {
        return attack_power + wepon.GetComponent<WeponController>().power;
    }

}
