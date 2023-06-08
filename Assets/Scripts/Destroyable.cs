using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Destroyable : MonoBehaviour
{
    public float explosionForce;
    public float explosionRadius;
    public static Destroyable instance;
    private List<Rigidbody> _rigidbodies;
    private Collider _collider;
    [SerializeField] private List<Vector3> _positions;
    [SerializeField] private List<Vector3> _rotations;
    [SerializeField] private Rigidbody parentRb;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void Start()
    {
        _collider = GetComponent<Collider>();
        _rigidbodies = new List<Rigidbody>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            _rigidbodies.Add(child.GetComponent<Rigidbody>());
            _positions.Add(_rigidbodies[i].transform.localPosition);
            _rotations.Add(_rigidbodies[i].transform.localRotation.eulerAngles);
        }
        transform.gameObject.SetActive(false);
    }
    
    public void InstantiateTheCube(Vector3 position)
    {
        parentRb.transform.position = position;
        for(int i = 0; i < _rigidbodies.Count; i++)
        {
            _rigidbodies[i].isKinematic = true;
            _rigidbodies[i].transform.localPosition = _positions[i];
            _rigidbodies[i].transform.localRotation = Quaternion.Euler(_rotations[i]);
        }
        Activate();
    }

    public void Activate()
    {
        foreach (var rb in _rigidbodies)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddExplosionForce(explosionForce,transform.position,explosionRadius);
            rb.AddTorque(Random.insideUnitSphere*100);
        }
        //set delay 2 secs
    }
}