using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    [SerializeField] LayerMask groundLayer;
    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public float collisionRadius;
    [SerializeField] Vector2 groundOffset;
    [SerializeField] Vector2 rightOffset;
    [SerializeField] Vector2 leftOffset;

    [SerializeField] Color gizmoColor = Color.red;

    private void Update() {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + groundOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
    }

    //private void OnDrawGizmos() {
    //    Gizmos.color = gizmoColor;
    //    Gizmos.DrawWireSphere((Vector2)transform.position + groundOffset, collisionRadius);
    //    Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
    //    Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);

    //}

}
