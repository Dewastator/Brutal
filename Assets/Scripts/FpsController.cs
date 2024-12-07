using UnityEngine;

public class FpsController : MonoBehaviour
{

    [SerializeField]
    private TMPro.TMP_Text _fpsText;
    private float _fps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QualitySettings.vSyncCount = 0; 
        Application.targetFrameRate = 60;
        InvokeRepeating(nameof(GetFPS), 1, 1);
    }

    private void GetFPS()
    {
        _fps = (int)(1 / Time.unscaledDeltaTime);
        _fpsText.text = _fps.ToString();
    }

    

}
