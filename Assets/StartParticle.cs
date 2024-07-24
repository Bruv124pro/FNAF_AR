using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class StartParticle : MonoBehaviour
{
    [SerializeField]private VisualEffect[] particle;
    void Start()
    {
        foreach (var effect in particle)
        {
            effect.transform.GetComponent<VisualEffect>();
        }
    }

    private void OnEnable()
    {
        if (particle != null)
        {
            foreach (VisualEffect p in particle)    
            {
                //p.SetBool("ElectricArkOnOff", true);
            }
        }
            
    }

    private void OnDisable()
    {
        if (particle != null)
        {
            foreach (VisualEffect p in particle)
            {
                //p.SetBool("ElectricArkOnOff", false);
            }
        }
    }

}
