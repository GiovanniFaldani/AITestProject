using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }

    private void Update()
    {
        float x = player.transform.position.x;
        float z = player.transform.position.z;
        transform.position = new Vector3 (x, transform.position.y, z);
    }
}
