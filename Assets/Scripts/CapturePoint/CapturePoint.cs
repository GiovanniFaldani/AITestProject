using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    [SerializeField, Range(-100.0f, 100.0f)] public float capturePercentRange = 0.0f;

    // this happens with FixedUpdate ticks
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            capturePercentRange += other.GetComponent<Player>().GetCaptureSpeed();
        }
        if (other.CompareTag("Enemy"))
        {
            capturePercentRange -= other.GetComponent<AIController>().GetCaptureSpeed();
        }
        capturePercentRange = Mathf.Clamp(capturePercentRange, -100.0f, 100.0f);
    }
}
