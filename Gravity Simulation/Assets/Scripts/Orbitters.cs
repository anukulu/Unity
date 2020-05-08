using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Orbitters : MonoBehaviour
{
    public InputField iPlanetSpeed;
    public InputField iSunSize;
    public InputField iDistanceFromSun;
    public InputField iPlanetNumber;

    private int planetCount = 5;
    public int maxRadius = 50;
    private float sunSize = 1;
    public GameObject[] planets;    //planets == spheres
    public Material[] mats;
    public Material trailMaterial;
    private float forwardMovementSpeed;
    private bool isSetNumbers = false;

    public void InitSettings()
    {
        forwardMovementSpeed = float.Parse(iPlanetSpeed.text);
        sunSize = float.Parse(iSunSize.text);
        planetCount = int.Parse(iPlanetNumber.text);
        //distanceFromSun = PlayerPrefs.GetInt("planetSpeed");
        PlayerPrefs.SetInt("hasSetNumbers", 1);
        PlayerPrefs.Save();
        GameObject.Find("Canvas").SetActive(false);
        GameObject.Find("UIBG").SetActive(false);
        GameObject.Find("EventSystem").SetActive(false);
        isSetNumbers = true;
        planets = new GameObject[planetCount];
        CreatePlanets();
    }


    public void CreatePlanets()
    {
        print("here");
        transform.localScale *= sunSize; 
        var planetToCopy = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Rigidbody rb = planetToCopy.AddComponent<Rigidbody>();
        rb.useGravity = false;
        print(planetCount);
        for(int i = 0; i < planetCount; i++)
        {
            GameObject pl = Instantiate(planetToCopy);
            pl.transform.position = new Vector3(Random.Range(-maxRadius, maxRadius), Random.Range(-10, 10), Random.Range(-maxRadius, maxRadius));
            pl.transform.localScale *= Random.Range(0.5f, 1);
            pl.GetComponent<Renderer>().material = mats[Random.Range(0, mats.Length)];
            TrailRenderer tr = pl.AddComponent<TrailRenderer>();
            tr.time = 1.0f;
            tr.startWidth = 0.1f;
            tr.endWidth = 0f;
            tr.material = trailMaterial;
            tr.startColor = new Color(1, 1, 0, 0.1f);
            tr.endColor = new Color(0, 0, 0, 0);
            Rigidbody rbTemp = GetComponent<Rigidbody>();
            rbTemp.velocity = new Vector3(rbTemp.velocity.x, rbTemp.velocity.y, forwardMovementSpeed);
            planets[i] = pl;
        }        
        Destroy(planetToCopy);
    }

    void Update()
    {
        if(isSetNumbers)
        {
            foreach (GameObject p in planets)
            {
                Vector3 difference = this.transform.position - p.transform.position;
                float distance = difference.magnitude;
                Vector3 gravityDirection = difference.normalized;
                float gravity = 6.7f * ((this.transform.localScale.x * p.transform.localScale.x * 80) / Mathf.Pow(distance, 2));
                Vector3 gravityVector = gravityDirection * gravity;
                p.transform.GetComponent<Rigidbody>().AddForce(p.transform.forward, ForceMode.Acceleration);
                p.transform.GetComponent<Rigidbody>().AddForce(gravityVector, ForceMode.Acceleration);
            }
        }
    }

}
