using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    // 말 스피드
    public float horseSpeed;

    // 목적지
    public Transform target;

    // 다음 목적지
    int nextTarget;

    // Start is called before the first frame update
    public void StartAI()
    {
        target = GameManager.instance.target[nextTarget];
        GetComponent<NavMeshAgent>().speed = horseSpeed;
        StartCoroutine("AI_Move");
        
    }

    //AI 움직임
    IEnumerator AI_Move()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.position);

        while (true)
        {
            float dis = (target.position - transform.position).magnitude;

            if(dis <= 10)
            {
                nextTarget += 1;
                target = GameManager.instance.target[nextTarget];
                GetComponent<NavMeshAgent>().SetDestination(target.position);
            }

            yield return null;
        }
    }
}
