using UnityEngine;
using UnityEngine.UI;

public class ProgressImageUI : MonoBehaviour
{    
    [SerializeField] private Canvas _uiCanvas; // Можно оставить пустым (если канвас не WorldSpace)
    [SerializeField] private Image _progressBar;
    private Transform _uiCanvasTransform;
    private Transform _cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        if (_uiCanvas != null)
        {
            _uiCanvasTransform = _uiCanvas.transform;
            _cameraTransform = Camera.main.transform;
        }
        
        _progressBar.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_uiCanvas != null) 
        {
            _uiCanvasTransform.forward = _cameraTransform.forward;
        }
        
    }

    public void FillProgress(float value)
    {
        _progressBar.fillAmount = 1 - value;
    }        

    public void ProgressReset(float value)
    {
        _progressBar.fillAmount = value;
    }
}
