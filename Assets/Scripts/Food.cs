using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    ParticleSystem food;
    [SerializeField]
    LayerMask mask;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    private void Start()
    {
        food = GetComponent<ParticleSystem>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.layer == 6 || other.layer == 8)
        {
            if(food.trigger.GetCollider(0) != other.transform)
            {
                food.trigger.AddCollider(other.transform);
            }
        }
    }

    private void OnParticleTrigger()
    {
        int numEnter = food.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter, out var enterData);

        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            var go = enterData.GetCollider(i, 0).gameObject;
            if(go.GetComponent<Player>() != null)
            {
                go.GetComponent<Player>().CurrentFoodAmount++;
            }
            enter[i] = p;
        }

        food.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }
}
