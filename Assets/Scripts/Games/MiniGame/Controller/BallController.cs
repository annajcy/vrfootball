using UnityEngine;

public class BallController : MonoBehaviour
{
    public Transform ballRespawnTransform;

    public void Respawn()
    {
        transform.position = ballRespawnTransform.position;
        transform.rotation = ballRespawnTransform.rotation;
    }
}