using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    // Default forces and torques values
    [SerializeField] private float minForce = 12f,
        maxForce = 16f,
        minTorque = -10f,
        maxTorque = 10f,
        xRange = 4,
        ySpawnPos = -4;

    private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the component RigidBody
        _rigidbody = GetComponent<Rigidbody>();
        
        // Give it an impulse in order to make it jump in the screen
        _rigidbody.AddForce(
            RandomForce(), 
            ForceMode.Impulse
        );
        
        // Make it rotate due to a torque
        _rigidbody.AddTorque(
            RandomTorque(), 
            RandomTorque(), 
            RandomTorque(), 
            ForceMode.Impulse
        );

        // Generate it in a random X but a fixed Y
        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Generates a random Vector3
    /// </summary>
    /// <returns>
    /// Random force in the up direction
    /// </returns>
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    /// <summary>
    /// Generates a random float
    /// </summary>
    /// <returns>
    /// Random value between minTorque and maxTorque
    /// </returns>
    float RandomTorque()
    {
        return Random.Range(minTorque, maxTorque);
    }

    /// <summary>
    /// Generates a random position
    /// </summary>
    /// <returns>
    /// 3D random position with coordinate z equals to 0
    /// </returns>
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
