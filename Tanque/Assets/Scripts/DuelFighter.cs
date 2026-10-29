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

        if (HasStateAuthority == true)
        {
            body.bodyType = RigidbodyType2D.Dynamic;

            if (Runner.IsSharedModeMasterClient == true)
            {
                PlayerIndex = 0;
            }
            else
            {
                PlayerIndex = 1;
            }
        }
        else
        {
            body.bodyType = RigidbodyType2D.Kinematic;
        }

        UpdateColor();
    }

    void UpdateColor()
    {
        if (PlayerIndex == 0)
        {
            bodyRenderer.color = player1Color;
        }
        else
        {
            bodyRenderer.color = player2Color;
        }
    }

    void Update()
    {
        if (HasStateAuthority == true && DuelGame.CanFight == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Runner.Spawn(bulletPrefab, firePoint.position, firePoint.rotation);
            }
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority == false)
        {
            return;
        }

        float walk = 0;
        float turn = 0;

        if (DuelGame.CanFight == true)
        {
            walk = Input.GetAxisRaw("Vertical");
            turn = Input.GetAxisRaw("Horizontal");
        }

        body.linearVelocity = transform.right * walk * moveSpeed;
        body.angularVelocity = -turn * turnSpeed;
    }
}