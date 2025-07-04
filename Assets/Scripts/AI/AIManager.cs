using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] public Transform[] pointHomes;
    [SerializeField] public Transform[] coverHomes;
    [SerializeField] public Transform[] healthPacks;

    public static AIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }


    public Transform ChooseFreeDestination(Transform[] destinations)
    {
        foreach(Transform d in destinations)
        {
            if (d.childCount > 0) continue;
            return d;
        }
        return null;
    }
}
