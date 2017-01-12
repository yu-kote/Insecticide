using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    int nowscore;
    int highscore;

    [SerializeField]
    Text nowscore_text;
    [SerializeField]
    Text highscore_text;

    void Start()
    {
        nowscore = GameController.instance.score;
        nowscore_text.text = nowscore.ToString();

    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("Title");
            highScoreUpdate();
        }
    }

    public void sceneChangeTitle()
    {
        SceneManager.LoadScene("Title");
        highScoreUpdate();
    }

    void highScoreUpdate()
    {
        if (highscore < nowscore)
        {
            highscore = nowscore;
        }
    }
}
