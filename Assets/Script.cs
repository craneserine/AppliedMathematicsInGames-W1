using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = .1f;
    private Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            direction = (target.position - transform.position).normalized;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            this.transform.position = direction * speed * Time.deltaTime;
        }
    }
}
