﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
    public Camera _camera;
    public Shader _drawShader;
    [Range(1, 500)]
    public float _brushSize;
    [Range(0f, 1f)]
    public float _brushStrength;
    [Header("Debug")]
    public bool DebugGUI = true;

    private RenderTexture _splatMap;
    private Material _snowMaterial, _drawMaterial;

    private RaycastHit _hit;

    // Start is called before the first frame update
    void Start()
    {
        _drawMaterial = new Material(_drawShader);
        _drawMaterial.SetVector("_Color", Color.red);

        _snowMaterial = GetComponent<MeshRenderer>().material;
        _splatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        _snowMaterial.SetTexture("_Splat", _splatMap);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out _hit))
            {
                _drawMaterial.SetVector("_Coordinate", new Vector4(_hit.textureCoord.x, _hit.textureCoord.y, 0, 0));
                _drawMaterial.SetFloat("_Strength", _brushStrength);
                _drawMaterial.SetFloat("_Size", _brushSize);
                RenderTexture temp = RenderTexture.GetTemporary(_splatMap.width, _splatMap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_splatMap, temp);
                Graphics.Blit(temp, _splatMap, _drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
    }

    private void OnGUI()
    {
        if(DebugGUI)
            GUI.DrawTexture(new Rect(0, 0, 256, 256), _splatMap, ScaleMode.ScaleToFit, false, 1);
    }
}
