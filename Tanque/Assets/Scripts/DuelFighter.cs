using Fusion;
using UnityEngine;

public class DuelFighter : NetworkBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Color player1Color;
    [SerializeField] private Color player2Color;
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private NetworkObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [Networked, OnChangedRender(nameof(UpdateColor))]
    public byte PlayerIndex { get; set; }

    private Rigidbody2D body;

    public override void Spawned()
    {
        body = GetComponent<Rigidbody2D>();

        body.bodyType = HasStateAuthority ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;

        if (HasStateAuthority)
            PlayerIndex = (byte)(Runner.IsSharedModeMasterClient ? 0 : 1);

        UpdateColor();
    }

    private void UpdateColor()
    {
        bodyRenderer.color = PlayerIndex == 0 ? player1Color : player2Color;
    }

    private void Update()
    {
        if (HasStateAuthority && DuelGame.CanFight && Input.GetButtonDown("Fire1"))
            Runner.Spawn(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority)
            return;

        float walk = 0;
        float turn = 0;

        if (DuelGame.CanFight)
        {
            walk = Input.GetAxisRaw("Vertical");
            turn = Input.GetAxisRaw("Horizontal");
        }

        body.linearVelocity = transform.right * walk * moveSpeed;
        body.angularVelocity = -turn * turnSpeed;
    }
}
