using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    protected Animator animator;
    protected static int state = Animator.StringToHash("state");
    public Transform respawnTransform;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Respawn()
    {
        var transform1 = transform;
        transform1.position = respawnTransform.position;
        transform1.rotation = respawnTransform.rotation;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}