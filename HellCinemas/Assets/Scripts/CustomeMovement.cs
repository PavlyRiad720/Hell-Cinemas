using UnityEngine;

public class CustomeMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float counterY = 25f;
    public float stopBuffer = 6f;

    [Header("Detection")]
    public LayerMask customerLayer;

    // Update is called once per frame
    void Update()
    {
        if (CanMoveForward())
        {
            float targetY = counterY;

            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, targetY), moveSpeed * Time.deltaTime);
        }
    }

    bool CanMoveForward()
    {
        if (transform.position.y >= counterY - 0.01f) return false;

        Vector2 startPos = (Vector2)transform.position + Vector2.up * 1.5f;
        RaycastHit2D hit = Physics2D.Raycast(startPos, Vector2.up, stopBuffer, customerLayer);
        return hit.collider == null;
    }
}
