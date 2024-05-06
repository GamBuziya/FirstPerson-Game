using System;
using System.Collections;
using System.Collections.Generic;
using Abstract_classes;
using DefaultNamespace.Abstract_classes;
using DefaultNamespace.Enums;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerMagicManager : BasicMagicManager
{
    
    private Camera _camera;
    
    private new void Start()
    {
        base.Start();
        _camera = GetComponentInChildren<Camera>();
    }
    
    
    public new void ShootProjectile()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            _destination = hit.point;
        }
        else
        {
            _destination = ray.GetPoint(500);
        }

        base.ShootProjectile();
    }
}
