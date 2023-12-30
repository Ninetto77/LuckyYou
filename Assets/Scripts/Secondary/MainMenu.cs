using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float speed = 1.0f;
    private float startTime;
    private bool isChanging = false;
    public Color startColor1 = Color.clear;
    public Color endColor1 = Color.white;
    public Color startColor2 = Color.clear;
    public Color endColor2 = Color.white;
    private int state = 1;

    // Start is called before the first frame update
    void Start()
    {
        text.color = startColor1;
        startTime = Time.time;        
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
        StartGame();
    }
    private void SmoothChange(Color startColor, Color endColor)
    {
        float t = (Time.time - startTime) * speed;
        text.color = Color.Lerp(startColor, endColor, t);
    }

    private void StateMachine()
    {
        switch (state)
        {
            case 1:
                if (isChanging)
                {
                    SmoothChange(startColor1, endColor1);
                    if (text.color == endColor1) isChanging = false;
                }
                else
                {
                    state = 2;
                    startTime = Time.time;
                    isChanging = true;
                }
                break;
            case 2:
                if (isChanging)
                {
                    SmoothChange(startColor2, endColor2);
                    if (text.color == endColor2) isChanging = false;
                }
                else
                {
                    state = 1;
                    startTime = Time.time;
                    isChanging = true;
                }
                break;
            
            default:
                break;
        }
    }

    private void StartGame()
    {
        if (Input.touchCount > 0) SceneManager.LoadScene(1);
    }
}
