using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attract : MonoBehaviour {
    
    [SerializeField]float G = 61888;
    [SerializeField]float initialspeed = 10;
    public static List<Attract> Planets;
    public GameObject route;
    public GameObject Route_parent;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * initialspeed;   //初速
    }

    private void OnEnable()
    {
        if (Planets == null)
        {
            Planets = new List<Attract>();
        }
        Planets.Add(this);
    }

    private void OnDisable()
    {
        Planets.Remove(this);
    }

    private void FixedUpdate()
    {
        Instantiate(route, this.transform.position, Quaternion.identity,Route_parent.transform);
        foreach (Attract planet in Planets)
        {
            if (planet != this)
            {
                Attract_Function(planet);
            }
        }
        
    }

    void Attract_Function(Attract otherPlanet)
    {
        // g = G*(m1*m2)/d^2

        Vector3 vector = otherPlanet.transform.position - this.transform.position;
        float distance = vector.magnitude;
        Vector3 direction = vector.normalized;
        Vector3 Force =G * this.GetComponent<Rigidbody>().mass * otherPlanet.GetComponent<Rigidbody>().mass / Mathf.Pow(distance,2) * direction;
        this.GetComponent<Rigidbody>().AddForce(Force);
    }
}
