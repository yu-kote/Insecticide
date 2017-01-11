using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーを操作して、ステータスも管理するクラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    public bool is_attack = false;
    public bool is_attack_animation = false;
    int attack_count;
    int attack_delay;
    int attack_start_frame;
    int attack_end_frame;

    public float attack_power;

    private Animator anim;

    GameObject wepon;

    [SerializeField]
    GameController gamecontroller;

    void Start()
    {
        is_attack_animation = false;
        anim = GetComponent<Animator>();
        attack_delay = 45;
        attack_count = 0;
        equipWepon("StandardSword");
        attack_start_frame = 20;
        attack_end_frame = 40;
    }

    void Update()
    {
        if (gamecontroller.gamestep != GameController.GameStep.BATTLE) return;
        anim.SetBool("IsAttack", is_attack_animation);
        if (is_attack_animation == false)
            if (Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("Fire1"))
            {
                is_attack_animation = true;
            }
        if (is_attack_animation)
        {
            attack_count++;
            if (attack_count > attack_start_frame && attack_count < attack_end_frame)
                is_attack = true;
            else
                is_attack = false;

            if (attack_count >= attack_delay)
            {
                attack_count = 0;
                is_attack_animation = false;
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
