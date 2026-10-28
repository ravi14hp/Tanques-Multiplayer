using Fusion;
using UnityEngine;

public class TesteBullet : NetworkBehaviour
{
    [SerializeField] private float flySpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private int maxBounces;

    private float timeLeft;
    private int bounces;
    private Rigidbody2D body;

    public override void Spawned()
    {
        timeLeft = lifeTime;
        body = GetComponent<Rigidbody2D>();

        body.bodyType = HasStateAuthority ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;

        if (HasStateAuthority)
            body.linearVelocity = transform.right * flySpeed;
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority)
            return;

        timeLeft -= Runner.DeltaTime;
        if (timeLeft <= 0)
        {
            Runner.Despawn(Object);
            return;
        }

        Vector2 velocity = body.linearVelocity;
        if (velocity.sqrMagnitude > 0.01f)
            body.linearVelocity = velocity.normalized * flySpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!HasStateAuthority)
            return;

        DuelFighter target = collision.collider.GetComponentInParent<DuelFighter>();

        if (target != null)
        {
            if (target.HasStateAuthority)
                return;

            DuelGame.Current.RPC_PlayerDefeated(target.Object.StateAuthority);
            Runner.Despawn(Object);
            return;
        }

        bounces++;
        if (bounces > maxBounces)
            Runner.Despawn(Object);
    }
}