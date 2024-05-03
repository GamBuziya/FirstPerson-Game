using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MagicFire : MonoBehaviour
{
    public Camera Cam;
    public GameObject Projectile;
    public Transform Firepoint;
    public float Speed = 30f;
    public float ArcRange = 1f;
    
    
    private Vector3 _destination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShootProjectile();
        }
        
    }

    private void ShootProjectile()
    {
        Ray ray = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _destination = hit.point;
        }
        else
        {
            _destination = ray.GetPoint(1000);
        }

        InstantiateProjectile();
    }

    private void InstantiateProjectile()
    {
        var projectileObj = Instantiate(Projectile, Firepoint.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().velocity = (_destination - Firepoint.position).normalized * Speed;
        
        iTween.PunchPosition(projectileObj, new Vector3(
            Random.Range(-ArcRange, ArcRange), 
            Random.Range(-ArcRange, ArcRange), 0), 
            Random.Range(0.5f, 2));
    }
}
