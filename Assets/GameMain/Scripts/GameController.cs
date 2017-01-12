using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance = new GameController();
    public int score = 0;

    public enum GameStep
    {
        NONE,
        MISSION_INFO,
        START,
        BATTLE,
        END,
        CAN_RESULT_MOVE,
    }

    public GameStep gamestep;
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
    public int calc_msec;// ミリ秒
    int msec;  // 描画用のミリ秒

    string draw_sec;
    string draw_msec;

    public int enemykill_count;

    public AudioSource end_whistle;
    public AudioSource bgm;

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
            mission_text.text = "ブロックを切りまくれ!!!";
            if (game_count > missiondraw_count)
            {
                mission_text.gameObject.SetActive(false);

                gamestep = GameStep.START;
                startcount_text.gameObject.SetActive(true);
                game_count = 0;
                secInit(gamestart_count);
            }
        }
        if (gamestep == GameStep.START)
        {
            secUpdate();
            if (sec > 0)
            {
                startcount_text.text = sec.ToString();
            }
            else
            {
                startcount_text.text = "スタート!";
            }
            if (game_count > gamestart_count)
            {
                startcount_text.gameObject.SetActive(false);
                gamestep = GameStep.BATTLE;
                gamecount_text.gameObject.SetActive(true);
                game_count = 0;
                secInit(gameend_count);
            }
        }
        if (gamestep == GameStep.BATTLE)
        {
            secUpdate();
            gamecount_text.text = draw_sec + " : " + draw_msec;
            if (sec < 10)
                gamecount_text.color = Color.red;
            if (game_count > gameend_count)
            {
                gamestep = GameStep.END;
                //gamecount_text.gameObject.SetActive(false);
                can_result_move_text.gameObject.SetActive(true);
                game_count = 0;
                end_whistle.Play();
            }
        }
        if (gamestep == GameStep.END)
        {
            can_result_move_text.text = "終わり！";
            bgm.volume -= 0.0005f;
            if (game_count > can_result_move_count)
            {

                if (Input.GetKeyDown(KeyCode.Return) ||
                    Input.GetButtonDown("Jump") ||
                    game_count > can_result_move_count + 300)
                {
                    SceneManager.LoadScene("Result");
                }
            }
        }
    }

    void secUpdate()
    {
        calc_msec--;
        sec = calc_msec / 60;

        msec--;
        if (msec < 0)
        {
            msec = 59;
            if (calc_msec < 0)
                msec = 0;
        }


        // 一の位のときに十の位のところに0を入れる処理
        if (sec >= 10)
            draw_sec = sec.ToString();
        else
            draw_sec = "0" + sec.ToString();
        if (msec >= 10)
            draw_msec = msec.ToString();
        else
            draw_msec = "0" + msec.ToString();
    }


    void secInit(int nextmsec)
    {
        calc_msec = nextmsec;
        msec = nextmsec % 60;
        sec = calc_msec / 60;
    }

}
