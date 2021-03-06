﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudObjectUtile : MonoBehaviour {

    [SerializeField]
    private GameObject arcSprite;
    [SerializeField]
    private GameObject spearSprite;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changement(objectUtile obj)
    {
        if(obj == objectUtile.arc)
        {
            changementInterface(arcSprite); 
        }
        if (obj == objectUtile.spear)
        {
            changementInterface(spearSprite);
        }
        if (obj == objectUtile.none)
        {
            detruireEnfant(); 
        }
    }

    void changementInterface(GameObject obj)
    {
        detruireEnfant(); 
        GameObject gameobjTemp = Instantiate(obj);
        gameobjTemp.transform.parent = this.transform;
        gameobjTemp.transform.position = this.transform.position;
    }

    void detruireEnfant()
    {
        for(int i = 0; i < transform.childCount; i++)
        
            Destroy(transform.GetChild(i).gameObject);

    }
}
