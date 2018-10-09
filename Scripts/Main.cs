using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {
    [Header("Menu UI")]
    public GameObject menuUI;
    public Button playBtn;
    public Text record;

    [Header("Game UI")]
    public GameObject gameUI;
    public Canvas platfCanv;
    public Text scoreText;
    public GameObject resultPanel;
    public Text scoreResult;

    [Header("Префабы")]
    public GameObject ball;
    public GameObject platforms;

    private Ball ballInstance;
    private GameObject platformInstance;

    // Use this for initialization

    void Awake()
    {
        if(!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", 0);
        }
    }
    void Start () {
        ShowRecord();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameUI.activeSelf)
        {
            try
            {
                if (ballInstance != null)
                {
                    scoreText.text = ballInstance.Score.ToString();
                }
            }
            catch
            {
                print("Waiting to creating ball");
            }
        }
    }
    public void ShowRecord()
    {
        record.text = PlayerPrefs.GetInt("Score").ToString();
    }
    public void Play()
    {
        playBtn.GetComponent<Animation>().Play();
        StartCoroutine(OnExitMenu());
    }
    IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(0.5f);
        menuUI.SetActive(true);

    }
    IEnumerator OnExitMenu()
    {
        yield return new WaitForSeconds(0.5f);
        menuUI.SetActive(false);
        StartCoroutine(OnStartGame());
    }
    IEnumerator OnStartGame()
    {
        if (!gameUI.activeSelf)
        {
            gameUI.SetActive(true);
        }
        if(resultPanel.activeSelf)
        {
            resultPanel.SetActive(false);
        }
        scoreText.text = "0";
        GameObject p = Instantiate(platforms, new Vector2(0, -400), Quaternion.identity);
        p.transform.SetParent(platfCanv.transform);
        p.transform.localScale = new Vector2(88, 88);
        p.transform.localPosition = new Vector3(0,-500,0);
        platformInstance = p;
        yield return new WaitForSeconds(1.5f);
        GameObject o = Instantiate(ball, new Vector2(0, 3), Quaternion.identity);
        ballInstance = o.GetComponent<Ball>();
        ballInstance.callback += LoseGame;
    }
    public void LoseGame()
    {
        resultPanel.SetActive(true);
        int score = ballInstance.Score;
        scoreResult.text = score.ToString();
        if(score > PlayerPrefs.GetInt("Score"))
        {
            PlayerPrefs.SetInt("Score", score);
        }
        Destroy(ballInstance.gameObject);
        Destroy(platformInstance);
    }
    public void Retry()
    {
        StartCoroutine(OnStartGame());
    }
    public void ExitMenu()
    {
        gameUI.SetActive(false);
        StartCoroutine(LoadMenu());
        ShowRecord();
    }
}
