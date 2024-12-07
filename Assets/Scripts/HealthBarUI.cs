using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private Camera mainCam;

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private Image stunBar;

    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(mainCam.transform);
        transform.Rotate(0, 180, 0);
    }

    public void SetHealth(float health,  float maxHealth, float stunAmount, float stunMax)
    {
        healthBar.fillAmount = health / maxHealth;

        stunBar.fillAmount = stunAmount / stunMax;
    }
}
