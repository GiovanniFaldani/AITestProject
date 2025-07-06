using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] public Transform[] pointHomes;
    [SerializeField] public Transform[] coverHomes;
    [SerializeField] public Transform[] healthPacks;

    [HideInInspector] public CapturePoint point;

    public static AIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        point = FindAnyObjectByType<CapturePoint>().GetComponent<CapturePoint>();
    }


    public Transform ChooseFreeDestination(Transform[] destinations)
    {
        // random destination selection
        int randomIndex = Random.Range(0, destinations.Length);

        for(int i = 0; i < destinations.Length; i++)
        {
            // loop the array from a random starting position
            int index = (randomIndex + i) % destinations.Length;

            // Pick the first free destination
            if (destinations[index].childCount > 0) continue;
            return destinations[index];
        }

        // If nothing is free, pick a random destination
        return destinations[randomIndex];
    }
}
