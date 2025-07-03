using System.ComponentModel;
using UnityEngine;

public class CapturePoint : MonoBehaviour
{
    [SerializeField, Range(-100.0f, 100.0f)] public float capturePercentRange = 0.0f;

    //private void LateUpdate()
    //{
    //    if(capturePercentRange >= 100.0f)
    //    {
    //        GameManager.Instance.Victory();
    //    }
    //    else if(capturePercentRange <= -100.0f)
    //    {
    //        GameManager.Instance.Lose();
    //    }
    //}

    // this happens with FixedUpdate ticks
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            capturePercentRange += other.GetComponent<Player>().GetCaptureSpeed();
        }
        else if (other.CompareTag("Enemy"))
        {
            capturePercentRange -= other.GetComponent<AIController>().GetCaptureSpeed();
        }
    }
}
