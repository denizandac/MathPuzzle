using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class Destroyable : MonoBehaviour
{
    public float explosionForce;
    public float explosionRadius;
    public float delay = 2f;
    public float timer;
    public bool shattering = false;
    public static Destroyable instance;
    public TextMeshPro textMesh;
    private List<Rigidbody> _rigidbodies;
    private Collider _collider;
    [SerializeField] private Rigidbody parentRb;

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        instance = this;
    }
    

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _rigidbodies = new List<Rigidbody>();
        for (int i = 0; i < transform.childCount-1; i++) // -1 because of the textmesh
        {
            var child = transform.GetChild(i);
            _rigidbodies.Add(child.GetComponent<Rigidbody>());
        }
        transform.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timer = 0;
            Activate();
        }
        if (shattering)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                transform.gameObject.SetActive(false);
                shattering = false;
            }
        }
    }

    public void InstantiateTheCube(Vector3 position)
    {
        parentRb.transform.position = position;
        transform.position = position;
    }

    public void Activate()
    {
        textMesh.text = "";
        foreach (var rb in _rigidbodies)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddExplosionForce(explosionForce,transform.position,explosionRadius);
            rb.AddTorque(Random.insideUnitSphere*100);
        }
        shattering = true;
        //set delay 2 secs
    }
}