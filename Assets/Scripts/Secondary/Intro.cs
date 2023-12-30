using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject[] frames;
    private AudioManagerIntro audioManager;
    public int frameCount = 0;
    public float frameTime = 0;
    private float startTimer;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GetComponent<AudioManagerIntro>();
        ShowImage(frameCount);
        frameTime = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(frameTime > 0) frameTime -= Time.deltaTime;
        TapOrLongTouch();
    }

    private void ShowImage(int numberOfImage)
    {
        for (int i = 0; i < frames.Length; i++) 
        {
            if (i != numberOfImage) frames[i].SetActive(false);
            else
            {
                frames[i].SetActive(true);
                AudioPlay(i);
                frameCount++;
            }
            if(numberOfImage == frames.Length - 1) StartGame();
        }
    }        

    public void TapOrLongTouch()
    {
        
        if (Input.touchCount > 0 && frameTime <= 0)
        {
            
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTimer = Time.time;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                frameTime = 0.5f;
                float duration = Time.time - startTimer;
                if (duration >= 2f)
                {
                    StartGame();
                }
                else
                {
                    ShowImage(frameCount);
                }                
            }
        }
    }

    private void AudioPlay(int frame)
    {
        switch (frame)
        {
            case 0:
                audioManager.PlaySound("0");
                break;
            case 1:
                break;
            case 2:
                audioManager.StopSound("0");
                audioManager.PlaySound("1");
                break;
            case 3:
                audioManager.PlaySound("2");
                break;
            case 4:
                audioManager.StopSound("2");
                break;
            case 5:
                audioManager.PlaySound("3");
                break;
            case 6:
                audioManager.PlaySound("4");
                break;
            case 7:
                audioManager.StopSound("4");
                break;
            case 8:                
                audioManager.PlaySound("5");
                StartCoroutine(Delay());
                break;
            default:
                break;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        audioManager.PlaySound("6");
    }

    private void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("StartGame");
    }
}
