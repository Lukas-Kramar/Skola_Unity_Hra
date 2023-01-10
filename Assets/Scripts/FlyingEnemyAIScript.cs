using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemyAIScript : MonoBehaviour
{
    private FlyingEnemy _flyingEnemy;
    private Transform target;
    public float nextWaypointDIstance = 3f;
    public float AttackRangeAnimation = 15;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D _rigidbody;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        _flyingEnemy = GetComponent<FlyingEnemy>();
        _rigidbody = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        animator = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating("UpdatePath", 0f, .2f);
        seeker.StartPath(_rigidbody.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(_rigidbody.position, target.position, OnPathComplete);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }

        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - _rigidbody.position).normalized;
        Vector2 force = direction * _flyingEnemy.speed * Time.deltaTime;

        var distanceFromPlayer = Vector2.Distance(_rigidbody.position, target.position);
        //Debug.Log(distanceFromPlayer);

        //if (distanceFromPlayer > 0 && distanceFromPlayer < _flyingEnemy.chaseDistance)
        //{

        //}
        //Debug.Log(_flyingEnemy.chaseDistance);
        if(distanceFromPlayer < _flyingEnemy.chaseDistance) _rigidbody.AddForce(force);




        float distance = Vector2.Distance(_rigidbody.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDIstance)
        {
            currentWaypoint++;
        }

        //Sprite turning
        if (_rigidbody.velocity.x <= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (_rigidbody.velocity.x >= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        ////Sprite attacking animation
        //float TargetDistance = Vector2.Distance((Vector2)target.position,  _rigidbody.position);
        //Debug.Log(TargetDistance);
        //if ((TargetDistance <= AttackRangeAnimation && TargetDistance > 0) || (TargetDistance <= 0 && TargetDistance > -AttackRangeAnimation))
        //{
        //    StartCoroutine(Attack());
        //}
    }

    //IEnumerator Attack()
    //{
    //    animator.SetBool("Attack", true);
    //    yield return new WaitForSecondsRealtime(0.6f);
    //    animator.SetBool("Attack", false);
    //}
}
