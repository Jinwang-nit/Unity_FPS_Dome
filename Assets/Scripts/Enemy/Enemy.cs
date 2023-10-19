using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private GameObject player;
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }
    public Path path;

    [Header("Base Values")]
    public int health = 100;

    [Header("Sight Values")]
    public float sightDistance = 4f; // 视距
    public float fieldOfView = 85f; // 视角
    public float eyeHeight;
    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate;

    [SerializeField]
    private string currentState;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
        isLife();
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward); // 返回角度
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitinfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitinfo, sightDistance))
                    {
                        if (hitinfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
             
                }
            }
        }
        return false;
    }

    public void isLife()
    {
        if (health <= 0)
        {

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // 解锁
            Destroy(gameObject);
        }
    }

    public void TakeDamage()
    {
        health -= 10;
    }
}
