using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public Heal healPrefab;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.gameObject;
        StartCoroutine(SearchAndMagic());   
    }
    private IEnumerator SearchAndMagic() {
        while (true)
        {
            transform.position = enemy.transform.position;
            yield return new WaitForSeconds(1);
            var collider = Physics2D.OverlapCircle(transform.position, 2.0f, LayerMask.GetMask("Enemy"));
            if (collider!=null) {
                var heal_bool = Instantiate(healPrefab,transform.position,transform.rotation);
                heal_bool.targetEnemy = collider.GetComponent<Enemy>();
            }
        }
    }
}
