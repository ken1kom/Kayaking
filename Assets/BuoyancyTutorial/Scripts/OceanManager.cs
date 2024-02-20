using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    public float wavesHeight = 15f;
    public float wavesFrequency = 1f;
    public float wavesSpeed = 1f;

    public Transform ocean;
    public Transform player;

    Material oceanMat;
    Texture2D wavesDisplacement;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        ocean.position = new Vector3(player.position.x, transform.position.y, player.position.z);
        SetVariables();
    }

    void SetVariables()
    {
        oceanMat = ocean.GetComponent<Renderer>().sharedMaterial;
        wavesDisplacement = (Texture2D)oceanMat.GetTexture("_WavesDisplacement");
    }
    
    public float WaterHeightAtPosition(Vector3 position)
    {
        return ocean.position.y + wavesDisplacement.GetPixelBilinear(position.x * wavesFrequency, position.z * wavesFrequency + Time.time * wavesSpeed).g * wavesHeight * ocean.localScale.x;
    }

    private void Update()
    {
        ocean.position = new Vector3(player.position.x, ocean.position.y, player.position.z);
    }

    private void OnValidate()
    {
        if (!oceanMat)
        {
            SetVariables();
        }

        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        oceanMat.SetFloat("_WavesHeight", wavesHeight);
        oceanMat.SetFloat("_WavesFrequency", wavesFrequency);
        oceanMat.SetFloat("_WavesSpeed", wavesSpeed);
    }
}
