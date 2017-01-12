using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{

    [SerializeField]
    GameObject canvas;

    [SerializeField]
    Image fadeblack;

    void Start()
    {
        canvas.gameObject.SetActive(true);
        bgmcount = 0; // ゴミコ
    }

    int bgmcount;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene("GameMain");
        }

        bgmcount++;
        if (bgmcount < 180) return;
        var color = fadeblack.color;
        color.a -= 0.01f;
        fadeblack.color = color;
        if (color.a <= 0.0f)
        {
            fadeblack.gameObject.SetActive(false);
        }
    }

    public void sceneChangeGameMain()
    {
        SceneManager.LoadScene("GameMain");
    }
}
