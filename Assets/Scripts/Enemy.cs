using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public float speed;
    public int gold;
    public Route route;
    public int pointIndex;
    public bool hitBlock=false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = route.points[0].transform.position;
        pointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var v=transform.position;
        //Debug.Log(gameObject.layer);
        switch (gameObject.layer)
        {
            case 8:
                v = route.points[pointIndex + 1].transform.position - route.points[pointIndex].transform.position;
                transform.position += v.normalized * speed * Time.deltaTime;
                var pv = transform.position - route.points[pointIndex].transform.position;
                if (pv.magnitude >= v.magnitude)
                {
                    pointIndex++;
                    if (pointIndex >= route.points.Length - 1)
                    {
                        if (gameObject.layer == 10) Debug.Log(gameObject);
                        Destroy(gameObject);
                        //ToDo プレイヤーにダメージ
                        FindObjectOfType<Player>().hp--;
                    }
                }
                break;
            case 10:
                v = SearchEnemy();
                transform.position += v.normalized * speed * Time.deltaTime;
                if (transform.position == route.points[pointIndex+1].transform.position)
                {
                    pointIndex++;
                    if (pointIndex >= route.points.Length - 1)
                    {
                        if (gameObject.layer == 10) Debug.Log(gameObject);
                        Destroy(gameObject);
                        //ToDo プレイヤーにダメージ
                        FindObjectOfType<Player>().hp--;
                    }
                }
                break;
        }
    }

    private Vector3 SearchEnemy() {
        var retVector3 = new Vector3(0,0,0) ;
        var collider = Physics2D.OverlapCircle(transform.position, 4.0f, LayerMask.GetMask("Enemy"));
        if (collider != null&&!hitBlock)
        {
            Enemy targetEnemy = collider.GetComponent<Enemy>();
            retVector3 = targetEnemy.route.points[targetEnemy.pointIndex + 1].transform.position - transform.position;
            
        }
        else
        {
            retVector3 = route.points[pointIndex + 1].transform.position - transform.position;
        }
        return retVector3;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Block")
        {
            hitBlock = true;
        }
    }
}
