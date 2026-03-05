using UnityEngine;

public class MoveBg : MonoBehaviour
{
    public float speed = 0.7f;

    private void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        if (transform.position.y < -10f) Destroy(gameObject);
    }
}
