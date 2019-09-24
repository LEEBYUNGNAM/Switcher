﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    // 대포에서 생성할 마나스톤
    [HideInInspector]
    public GameObject manastone;

    // 마나스톤이 마법진에 몇개 들어왔는지 카운트
    private int inCount = 0;

    // collisionEnter시 잠깐 활성
    [HideInInspector]
    public bool collisionEnterFl = false;

    public MoveDoor door;

    private AudioSource audio;
    public AudioClip onClip;
    public AudioClip offClip;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 같은 마법진에 닿았는지 확인
        if (collision.gameObject.tag == gameObject.tag)
        {
            inCount++;
            // 들어온 마나스톤이 하나일경우
            if (inCount == 1)
            {
                // 닿은 마나스톤을 저장
                manastone = collision.gameObject;
                collisionEnterFl = true;
                door.MoveOpen();
                audio.clip = onClip;
                audio.Play();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        collisionEnterFl = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        // 마나스톤이 마법진에서 나갈때
        if (collision.gameObject.tag == gameObject.tag)
        {
            inCount--;
            // 마나스톤 다 빠져나가면 발사 멈춤
            if (inCount == 0)
            {
                manastone = null;
                door.MoveClose();
                audio.clip = offClip;
                audio.Play();
            }
        }
    }
}
