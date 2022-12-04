using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMeeleChase : MonoBehaviour
{
    //[Header("Enviroment + Player")]
    //public Player player;
    //[SerializeField] private float _maxChaseDistance = 50;

    ////[Header("Enviroment + Player")]
    //private BasicEnemy _basicEnemy;    
    //// Start is called before the first frame update
    //void Start()
    //{
    //    _basicEnemy = GetComponent<BasicEnemy>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    var distance = Vector2.Distance(transform.position, player.transform.position);
    //    if(distance < _maxChaseDistance)
    //    {
    //        Vector2 direction = player.transform.position - transform.position;
    //        direction.Normalize();
    //        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    //        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, _basicEnemy.speed * Time.deltaTime);
    //       // transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    //    }
    //}
}
