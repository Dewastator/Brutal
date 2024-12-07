using UnityEngine;

public class BotSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject botPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(botPrefab, new Vector3(Random.Range(-100f,100f), botPrefab.transform.position.y, Random.Range(-100f, 100f)),Quaternion.identity, transform);
        }
    }

    
}
