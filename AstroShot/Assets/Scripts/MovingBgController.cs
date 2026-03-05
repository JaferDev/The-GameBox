using UnityEngine;

public class MovingBgController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] GameObject firstMove;
    [SerializeField] GameObject secondMove;

    private GameObject backgroundToCheck;
    private int spawnedAmt = 0;
    private void Start()
    {
        transform.position = new Vector3(0, 5f, 0);
        backgroundToCheck = Instantiate(firstMove, Vector3.zero, Quaternion.identity);
        spawnedAmt++;
    }
    void Update()
    {
        if (backgroundToCheck.transform.position.y <= 0)
        {
            if (spawnedAmt % 2 ==0) backgroundToCheck = Instantiate(firstMove, new Vector3(0, 10, 0), Quaternion.identity);
            else backgroundToCheck = Instantiate(secondMove, new Vector3(0, 10, 0), Quaternion.identity);
            spawnedAmt++;
        }
    }
}
