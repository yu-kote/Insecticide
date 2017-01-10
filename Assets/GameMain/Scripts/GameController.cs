using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public enum GameStep
    {
        NONE,
        MISSION_INFO,
        START,
        BATTLE,
        END,
        CAN_RESULT_MOVE,
    }

    GameStep gamestep;
    public int game_count;

    [SerializeField]
    Text mission_text;
    public int missiondraw_count;

    [SerializeField]
    Text startcount_text;
    public int gamestart_count;

    [SerializeField]
    Text gamecount_text;

    [SerializeField]
    Text end_text;
    public int gameend_count;

    [SerializeField]
    Text can_result_move_text;
    public int can_result_move_count;

    public int sec; // 秒
    public int msec;// ミリ秒

    public int enemykill_count;


    void Start()
    {
        gamestep = GameStep.MISSION_INFO;
        sec = 0;
        enemykill_count = 0;
        mission_text.gameObject.SetActive(false);
        startcount_text.gameObject.SetActive(false);
        gamecount_text.gameObject.SetActive(false);
        can_result_move_text.gameObject.SetActive(false);

    }


    void Update()
    {
        game_count++;

        if (gamestep == GameStep.MISSION_INFO)
        {
            mission_text.gameObject.SetActive(true);
            mission_text.text = "敵を倒しまくれ!!!";
            if (game_count > missiondraw_count)
            {
                mission_text.gameObject.SetActive(false);
                gamestep = GameStep.START;
                game_count = 0;
                minus_sec = gamestart_count;
            }
        }
        if (gamestep == GameStep.START)
        {
            minusSec();
            startcount_text.gameObject.SetActive(true);
            startcount_text.text = sec.ToString();
            if (game_count > gamestart_count)
            {
                gamestep = GameStep.BATTLE;
                game_count = 0;
                startcount_text.gameObject.SetActive(false);
            }
        }
        if (gamestep == GameStep.BATTLE)
        {
            if (game_count > gameend_count)
            {
                gamestep = GameStep.END;
                game_count = 0;
            }
        }
        if (gamestep == GameStep.END)
        {
            if (game_count > can_result_move_count)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Result");
                }
            }
        }
    }

    void secUpdate()
    {
        sec = game_count % 60;
        msec++;
        if (game_count % 60 == 0)
        {
            msec = 0;
        }
    }


    int minus_sec;
    void minusSec()
    {
        minus_sec--;
        sec = minus_sec % 60;
    }

}
