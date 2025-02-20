using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public GameObject pooledProjectile;

    [SerializeField]
    private bool notEnoughProjectileInPool = true;

    private List<GameObject> projectiles;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        projectiles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetProjectile()
    {
        if (projectiles.Count > 0)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                if (!projectiles[i].activeInHierarchy)
                {
                    return projectiles[i];
                }
            }
        }

        if (notEnoughProjectileInPool)
        {
            GameObject proj = Instantiate(pooledProjectile);
            proj.SetActive(false);
            projectiles.Add(proj);
            return proj;
        }

        return null;
    }
}
