using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Arrow arrowPrefab;

    public int lv = 1;
    public float ShotRange => 1 + lv * 0.5f;
    public float ShotInterval => 1.0f * Mathf.Pow(0.9f,lv);

    public int Cost => (int)(100 * Mathf.Pow(1.5f, lv));
    public int Price => Cost / 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SearchAndShot());   
    }

    private IEnumerator SearchAndShot() {
        while (true) {
            yield return new WaitForSeconds(ShotInterval);
            var collider = Physics2D.OverlapCircle(transform.position, ShotRange, LayerMask.GetMask("Enemy", "Enemy_Healer")) ;
            if (collider != null )
            {
                transform.rotation = Quaternion.FromToRotation(Vector3.right,collider.transform.position-transform.position);
                var arrow=Instantiate(arrowPrefab,transform.position,transform.rotation) ;
                arrow.targetEnemy=collider.GetComponent<Enemy>();
            }
        }
    }
}
