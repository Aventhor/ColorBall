using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

    [Header("Цвета шарика")]
    public string[] colors;

    public int Score
    {
        get { return score; }
        set { value = 0; }
    }
    private int score;
    private string currentColor;

    public delegate void OnLoseGame();
    public OnLoseGame callback;

	// Use this for initialization
	void Start () {
        ChangeColor();
    }
	
	// Update is called once per frame
	void Update () {
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.CompareTag("Platform"))
        {
            if (coll.gameObject.GetComponent<Platform>().color == currentColor)
            {
                ChangeColor();
                ChangeGravity();
                score++;
            }
            else
            {
                LoseGame();
            }
        }
    }
    private void ChangeColor()
    {
        Color color = new Color();
        int randomNumber = Random.Range(0, 3);
        ColorUtility.TryParseHtmlString(colors[randomNumber].ToString(), out color);
        GetComponent<SpriteRenderer>().color = color;
        currentColor = colors[randomNumber];
    }
    private void LoseGame()
    {
        callback();
        GetComponent<Rigidbody2D>().simulated = false;
    }
    private void ChangeGravity()
    {
        float gravity = GetComponent<Rigidbody2D>().gravityScale;
        if(gravity < 5.3)
            gravity += 0.05f;
        GetComponent<Rigidbody2D>().gravityScale = gravity;
        print(gravity);
    }
}
