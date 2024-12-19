using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Slider HPBar;
    private float enemyMaxHP = 10;
    public float enemyCurrentHP = 0;

    private NavMeshAgent agent;
    private Animator animator;

    private GameObject targetPlayer;
    private float TargetDelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {   
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        targetPlayer = GameObject.FindWithTag("Player");
        InitEnemyHP();
    }

    // Update is called once per frame
    void Update()
    {
        HPBar.value = enemyCurrentHP / enemyMaxHP;

        if(enemyCurrentHP <= 0)
        {
             StartCoroutine(EnemyDie());
            return;
        }

        if(targetPlayer != null)
        {
            float maxDelay = 0.5f;
            TargetDelay += Time.deltaTime;

            if (TargetDelay < maxDelay)
            {
                return;
            }

            agent.destination = targetPlayer.transform.position;
            transform.LookAt(targetPlayer.transform.position);

            
            bool isRange = Vector3.Distance(transform.position, targetPlayer.transform.position) <=
                agent.stoppingDistance;

            if (isRange)
            {
                animator.SetTrigger("Attack");
            }
            else
            {
                animator.SetFloat("MoveSpeed", agent.velocity.magnitude);

            }
            TargetDelay = 0;
        }
    }

    private void InitEnemyHP()
    {
        enemyCurrentHP = enemyMaxHP;
    }

    IEnumerator EnemyDie()
    {
        agent.speed = 0;
        animator.SetTrigger("Dead");

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
