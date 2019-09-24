﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMode : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    public LineRenderer laser;
    GameMgr gameMgr;

    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        gameMgr = FindObjectOfType<GameMgr>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(laser.transform.position, laser.transform.forward);
        if (Physics.Raycast(ray, out hit, 16.0f))
        {
            float dist = hit.distance;
            laser.SetPosition(1, new Vector3(0, 0, dist));
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("START")))
            {
                audio.Play();
                gameMgr.StartCoroutine(gameMgr.TestLoad());
            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("CONTINUE")))
            {
                audio.Play();
                //SceneManager.LoadScene("Demo");
            }
        }
    }
}
