using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool hitBlock = false;
    public int hp;
    public float speed;
    public int gold;
    public Route route;
    public int pointIndex;

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
                break;
            case 10:
                v = SearchEnemy();
                transform.position += v.normalized * speed * Time.deltaTime;
                break;
        }


        //var pv = transform.position- route.points[pointIndex].transform.position;
        //if (pv.magnitude>=v.magnitude) {
        if (gameObject.layer == 10)
        {
            if (transform.position == route.points[pointIndex + 1].transform.position)
            {
                if (gameObject.layer == 10) Debug.Log("Next");
                pointIndex++;
                if (pointIndex >= route.points.Length - 1)
                {
                    if (gameObject.layer == 10) Debug.Log(gameObject);
                    Destroy(gameObject);
                    //ToDo プレイヤーにダメージ
                    FindObjectOfType<Player>().hp--;
                }
            }
        }
        else {
            var pv = transform.position- route.points[pointIndex].transform.position;
            if (pv.magnitude>=v.magnitude) {
            pointIndex++;
            if (pointIndex >= route.points.Length - 1)
            {
                if (gameObject.layer == 10) Debug.Log(gameObject);
                Destroy(gameObject);
                //ToDo プレイヤーにダメージ
                FindObjectOfType<Player>().hp--;
            }
        }
    }
    }

    private Vector3 SearchEnemy() {
        var retVector3 = new Vector3(0,0,0) ;
        var collider = Physics2D.OverlapCircle(transform.position, 4.0f, LayerMask.GetMask("Enemy"));
        if (collider != null)
        {
            Enemy targetEnemy = collider.GetComponent<Enemy>();
            
            if (!hitBlock) {
                retVector3 = targetEnemy.route.points[targetEnemy.pointIndex + 1].transform.position - transform.position;
            }
            /*var collider_wall = Physics2D.OverlapBox(transform.position, new Vector2(0.0f,0.0f), LayerMask.GetMask("Block"));
            if (collider_wall != null)
            {

                Debug.Log(collider_wall.GetComponent<Block>().transform.position);
                //if (gameObject.layer == 10) Debug.Log(gameObject);
                if (collider_wall.transform.position.x > transform.position.x && collider_wall.transform.position.x < transform.position.x + retVector3.normalized.x * speed * Time.deltaTime)
                {
                    retVector3.x = 0;
                }
                if (collider_wall.transform.position.x < transform.position.x && collider_wall.transform.position.x > transform.position.x + retVector3.normalized.x * speed * Time.deltaTime)
                {
                    retVector3.x = 0;
                }
                if (collider_wall.transform.position.y > transform.position.y && collider_wall.transform.position.y < transform.position.y + retVector3.normalized.y * speed * Time.deltaTime)
                {
                    retVector3.y = 0;
                }
                if (collider_wall.transform.position.y < transform.position.y && collider_wall.transform.position.y > transform.position.y + retVector3.normalized.y * speed * Time.deltaTime)
                {
                    retVector3.y = 0;
                }
            }*/
            
            /*
            RaycastHit[] _raycastHits = new RaycastHit[10];
            // 自身とプレイヤーの座標差分を計算
            var positionDiff = collider.transform.position - transform.position;

            // プレイヤーとの距離を計算
            var distance = positionDiff.magnitude;

            // プレイヤーへの方向（長さだけ１にする処理。方向は変わらない）
            var direction = positionDiff.normalized;

            // _raycastHitsに、ヒットしたColliderや座標情報などが格納される
            // RaycastAllとRaycastNonAllocは同等の機能だが、RaycastNonAllocだとメモリにゴミが残らないのでこちらを推奨
            var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance);
            if (hitCount > 0)
            {
                var diff = _raycastHits[0].transform.position - transform.position;
                if (diff.magnitude<0.7f){
                    if (diff.x<0.4f) {
                        retVector3.x = 0;
                    }
                    if (diff.y < 0.4f)
                    {
                        retVector3.y= 0;
                    }
                }
            }*/
        }
        else
        {
            
            retVector3 = route.points[pointIndex + 1].transform.position - transform.position;
            
        }
        return retVector3;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Block") {
            Debug.Log("hit Block!");
            hitBlock = true;
        }
    }

}
