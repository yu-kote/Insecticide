using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// エネミーを管理するクラス
/// </summary>
public class EnemyManager : MonoBehaviour
{
    public int pop_count;
    public int pop_frame;
    public int pop_range;
    public float pop_height;

    [SerializeField]
    PlayerController player;
    [SerializeField]
    GameController gamecontroller;

    void Start()
    {
        pop_count = 0;
        enemys = new List<GameObject>();
        enemy_pop_steps = EnemyPopSteps.ONE_STEP;
    }

    List<GameObject> enemys;
    void Update()
    {
        enemyPopFrameIntervalShortening();
        enemyPop();
        enemyOutOfRangeActiveChange();
        enemyDead();
    }

    void enemyPop()
    {
        pop_count++;
        if (pop_count % pop_frame == 0)
        {
            var enemy = Resources.Load<GameObject>("Prefabs/Enemy");

            enemy.transform.position = Random.insideUnitSphere * pop_range + player.transform.position;
            var pos = enemy.transform.position;
            pos.y = pop_height;
            enemy.transform.position = pos;
            //enemy.GetComponent<MeshRenderer>().material = enemymaterial;

            enemys.Add((GameObject)Instantiate(enemy, transform));
        }
    }

    [SerializeField]
    AudioSource hit_mp3;
    void enemyDead()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i].GetComponent<Enemy>().hp <= 0)
            {
                hit_mp3.Play();
                Destroy(enemys[i]);
                enemys.RemoveAt(i);
                GameController.instance.score++;
                break;
            }
        }
    }

    void enemyOutOfRangeActiveChange()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            if (pointToCirclePlane(player.transform.position, enemys[i].transform.position, pop_range * 2))
                enemys[i].gameObject.SetActive(true);
            else
                enemys[i].gameObject.SetActive(false);
        }
    }

    // 点と円 in true
    bool pointToCircle(Vector2 pointpos, Vector2 circlepos, float radius)
    {
        float x = (pointpos.x - circlepos.x) * (pointpos.x - circlepos.x);
        float y = (pointpos.y - circlepos.y) * (pointpos.y - circlepos.y);

        return x + y <= radius * radius;
    }

    // x,z平面での円の判定(yの値無視)
    bool pointToCirclePlane(Vector3 pointpos, Vector3 circlepos, float radius)
    {
        float x = (pointpos.x - circlepos.x) * (pointpos.x - circlepos.x);
        float z = (pointpos.z - circlepos.z) * (pointpos.z - circlepos.z);

        return x + z <= radius * radius;
    }

    // 三段階に分ける
    public enum EnemyPopSteps
    {
        ONE_STEP,
        TWO_STEP,
        THREE_STEP,
    }
    EnemyPopSteps enemy_pop_steps;
    EnemyPopSteps current_pop_step;

    // 段階を挙げるフレーム
    int step_up_frame;

    // プレイヤーの倒している敵の数と
    // 残り時間を見て、敵の湧く時間を短縮する
    void enemyPopFrameIntervalShortening()
    {
        int battletime = gamecontroller.gameend_count;
        step_up_frame = battletime / 3;
        if (pop_count < step_up_frame * 3)
            enemy_pop_steps = EnemyPopSteps.THREE_STEP;
        if (pop_count < step_up_frame * 2)
            enemy_pop_steps = EnemyPopSteps.TWO_STEP;
        if (pop_count < step_up_frame)
            enemy_pop_steps = EnemyPopSteps.ONE_STEP;

        if (current_pop_step == enemy_pop_steps) return;
        current_pop_step = enemy_pop_steps;

        if (enemy_pop_steps == EnemyPopSteps.ONE_STEP)
        {

        }
        else if (enemy_pop_steps == EnemyPopSteps.TWO_STEP)
        {
            pop_frame = pop_frame / 2;
        }
        else if (enemy_pop_steps == EnemyPopSteps.THREE_STEP)
        {
            pop_frame = pop_frame / 3;
        }
    }





}
