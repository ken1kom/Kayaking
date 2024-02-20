using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoyancyObject : MonoBehaviour
{
    public Transform[] floaters;

    public float underWaterDrag = 3;
    public float underWaterAngularDrag = 1;

    public float airDrag = 0;
    public float airAngularDrag = 0.05f;

    public float floatingPower = 15f;

    Rigidbody m_Rigidbody;

    int floatersUnderwater;

    bool underwater;

    OceanManager oceanManager;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        oceanManager = FindObjectOfType<OceanManager>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        floatersUnderwater = 0;
        for (int i = 0; i < floaters.Length; i++)
        {
            float difference = floaters[i].position.y - oceanManager.WaterHeightAtPosition(floaters[i].position);

            if (difference < 0)
            {
                m_Rigidbody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), floaters[i].position, ForceMode.Force);
                floatersUnderwater++;

                if (!underwater)
                {
                    underwater = true;
                    SwitchState(true);
                }
            }
        }

        
        if(underwater && floatersUnderwater == 0)
        {
            underwater = false;
            SwitchState(false);
        }

    }

    void SwitchState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            m_Rigidbody.drag = underWaterDrag;
            m_Rigidbody.angularDrag = underWaterAngularDrag;
        }
        else
        {
            m_Rigidbody.drag = airDrag;
            m_Rigidbody.angularDrag = airAngularDrag;
        }
    }
}
