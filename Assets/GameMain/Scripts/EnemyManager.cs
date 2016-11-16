using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{

    int count;
    public int pop_frame;
    public int pop_range;
    public float pop_height;

    void Start()
    {
        count = 0;
        enemys = new List<GameObject>();
    }

    List<GameObject> enemys;
    void Update()
    {
        count++;
        if (count % pop_frame == 0)
        {
            var enemy = Resources.Load<GameObject>("Prefabs/Enemy");

            enemy.transform.position = Random.insideUnitSphere * pop_range;
            var pos = enemy.transform.position;
            pos.y = pop_height;
            enemy.transform.position = pos;


            enemys.Add((GameObject)Instantiate(enemy, transform));
        }

        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i].GetComponent<Enemy>().hp <= 0)
            {
                Destroy(enemys[i]);
                enemys.RemoveAt(i);
                break;
            }
        }
    }
}
