﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour {
    Material m_mat;
    public bool scrollMainTex;
    public bool scrollNormal;
    public float scrollXSpeed;
    public float scrollYSpeed;
    private Vector2 scrollSpeed;

    private void Start()
    {
        m_mat = GetComponent<MeshRenderer>().material;
        SetSpeed();
    }

    public void SetSpeed()
    {
        scrollSpeed = new Vector2(scrollXSpeed, scrollYSpeed);
    }

    void LateUpdate ()
    {
        if (scrollMainTex)
        {
            m_mat.mainTextureOffset = scrollSpeed * Time.time;
        }
        if(scrollNormal)
        {
            m_mat.SetTextureOffset("_BumpMap", m_mat.GetTextureOffset("_BumpMap")+scrollSpeed*Time.deltaTime);
        }
        //m_mat.SetTextureOffset("_DissolveTexture", scrollSpeed * Time.time);
    }

}
