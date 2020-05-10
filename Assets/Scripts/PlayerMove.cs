using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera), typeof(PlayerInput))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private float movementSpeed = 10.0f;

    private Coroutine currentCoroutine = null;

    private PlayerInput input;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    public void StartMovingTowardsPosition(Vector3 positionToMove)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        currentCoroutine = StartCoroutine(MoveTowardsTargetPosition(transform.position, positionToMove, transform.rotation, targetRotation));
    }

    private IEnumerator MoveTowardsTargetPosition(Vector3 startMovingPosition, Vector3 targetMovingPosition, Quaternion startRotation, Quaternion targetRotation)
    {
        for (float moveAlpha = 0.0f; moveAlpha < 1.0f; moveAlpha += movementSpeed * Time.deltaTime)
        {
            transform.position = Vector3.Lerp(startMovingPosition, targetMovingPosition, moveAlpha);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, moveAlpha);

            gameController.SetBlendOnAllSpheres(moveAlpha);

            yield return null;
        }

        currentCoroutine = null;
        input.ResetTurning();

        gameController.MovementToSphereEnded();
    }
}
