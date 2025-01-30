using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Warrior_ult_bullet : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 7;

    void Start()
    {
        StartCoroutine(Die());
    }

    // Update is called once per frame
    private void Update() => transform.Translate(direction * speed * Time.deltaTime, Space.World);

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
