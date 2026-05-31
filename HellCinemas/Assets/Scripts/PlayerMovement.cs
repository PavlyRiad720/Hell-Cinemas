using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isMoving;
  
    public float timeToMove = 0.2f;
    public float moveDistance = 10f;

    [Header("Visual Smoothing")]
    public AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Boundaries")]
    public float minX = -10f;
    public float maxX = 10f;

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                AttemptMove(Vector3.left * moveDistance);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                AttemptMove(Vector3.right * moveDistance);
            }
        }
    }

    private void AttemptMove(Vector3 translation)
    {
        Vector3 potentialTarget = transform.position + translation;

        if (potentialTarget.x >= minX && potentialTarget.x <= maxX)
        {
            StartCoroutine(MovePlayer(potentialTarget));
        }
    }

    private IEnumerator MovePlayer(Vector3 targetPosition)
    {
        isMoving = true;
        float elapsedTime = 0;

        Vector3 originalPosition = transform.position;

        while (elapsedTime < timeToMove)
        {
            float time = elapsedTime / timeToMove;
            float curveValue = movementCurve.Evaluate(time);

            transform.position = Vector3.Lerp(originalPosition, targetPosition, curveValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }
}
