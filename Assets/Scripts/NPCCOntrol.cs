// NPCController.cs - Asignar al GameObject "NPC"
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float waitTime = 2f;
    
    private int currentPointIndex;
    private float waitTimer;
    private bool isWaiting;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (patrolPoints.Length == 0) return;
    }
    
    void Update()
    {
        Patrol();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            transform.position = new Vector2(7f, -3f);
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
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            }
            return;
        }
        
        Vector2 target = patrolPoints[currentPointIndex].position;
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        
        transform.position = Vector2.MoveTowards(transform.position, target, patrolSpeed * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            isWaiting = true;
            spriteRenderer.flipX = direction.x < 0;
        }
        else
        {
            spriteRenderer.flipX = direction.x < 0;
        }
    }
}
