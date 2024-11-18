using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Glove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        var sequence = DOTween.Sequence();
        sequence.Insert(0f, transform.DOPunchPosition(new Vector3(0, 0, 5f), 0.5f, 1, 0)).OnUpdate(() => { EnableCollider(sequence, true); });
        sequence.OnComplete(() => { gameObject.SetActive(false); });
    }

    public void EnableCollider(DG.Tweening.Sequence sequence, bool enable)
    {
        if (sequence.position > 0.1f)
        {
            transform.GetComponent<SphereCollider>().enabled = enable;
        }
        if(sequence.position > 0.34f)
        {
            transform.GetComponent<SphereCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody != null)
        {
            other.attachedRigidbody.AddForce(transform.forward * 10f, ForceMode.Impulse);
        }
    }
}
