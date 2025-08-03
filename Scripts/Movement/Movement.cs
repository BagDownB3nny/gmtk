using UnityEngine;

public abstract class Movement : MonoBehaviour
{
	protected readonly float moveSpeed = 5f;

	public Rigidbody2D rb;

	protected Vector2 movement;
	protected Vector2 mousePos;

	protected virtual void FixedUpdate()
	{
		// Movement
		Vector2 moveAction = rb.position + moveSpeed * Time.fixedDeltaTime * movement.normalized;
		rb.MovePosition(moveAction);

		Vector2 lookDir = mousePos - rb.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
		rb.rotation = angle;
	}
}
