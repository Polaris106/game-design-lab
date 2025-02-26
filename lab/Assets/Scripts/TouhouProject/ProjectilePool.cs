using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{

    [SerializeField]
    private GameObject pooledProj;
    [SerializeField]
    private int initialPoolSize = 20; // Optionally prefill the pool with a certain number of projectiles

    private List<GameObject> projectiles;

    // Start is called before the first frame update
    void Start()
    {
        projectiles = new List<GameObject>();

        // Optionally prefill the pool
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject proj = Instantiate(pooledProj);
            proj.SetActive(false); // Start with inactive objects
            projectiles.Add(proj);
        }
    }

    public GameObject GetProjectile()
    {
        // Look for an inactive projectile in the pool
        foreach (GameObject proj in projectiles)
        {
            if (!proj.activeInHierarchy)
            {
                proj.SetActive(true); // Activate the object before returning it
                return proj;
            }
        }

        // If no inactive projectile is found, create a new one
        GameObject newProj = Instantiate(pooledProj);
        newProj.SetActive(true);
        projectiles.Add(newProj);
        return newProj;
    }
}
