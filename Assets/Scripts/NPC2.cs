// Random3PointsPatrol.cs - Asignar al GameObject "NPC_Random3"
// Sigue al Player + Patrulla 3 puntos aleatorios
using UnityEngine;

public class Random3PointsPatrol : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] patrolPoints = new Transform[3]; // 3 puntos en Inspector
    public float patrolSpeed = 1.5f;
    public float waitTime = 1.5f;
    
    [Header("Player Follow")]
    public Transform player;
    public float followRange = 6f;
    public float followSpeed = 2.5f;
    
    private int currentPointIndex;
    private float waitTimer;
    private bool isWaiting;
    private bool isFollowingPlayer;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer < followRange)
        {
            isFollowingPlayer = true;
            FollowPlayer();
        }
        else
        {
            isFollowingPlayer = false;
            Patrol();
        }
    }
    
    void FollowPlayer()
    {
        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
        spriteRenderer.flipX = direction.x < 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            transform.position = new Vector2(20f, 0f);
        }
    }
    
    void Patrol()
    {
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                waitTimer = 0f;
                // Cambia a punto aleatorio de los 3
                currentPointIndex = Random.Range(0, patrolPoints.Length);
            }
            return;
        }
        
        if (patrolPoints.Length == 0) return;
        
        Vector2 target = patrolPoints[currentPointIndex].position;
        Vector2 direction = ((Vector2)target - (Vector2)transform.position).normalized;
        
        transform.position = Vector2.MoveTowards(transform.position, target, patrolSpeed * Time.deltaTime);
        spriteRenderer.flipX = direction.x < 0;
        
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            isWaiting = true;
        }
    }
}
