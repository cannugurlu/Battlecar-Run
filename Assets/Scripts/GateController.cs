using UnityEngine;
using TMPro;

public class GateController : MonoBehaviour
{
    void Start()
    {
        GameObject[] gates = GameObject.FindGameObjectsWithTag("Gate");

        foreach (GameObject gate in gates)
        {
            if (gate.GetComponent<Gate>() != null)
            {
                if (gate.GetComponent<MeshRenderer>().material.name == "GatePosIn (Instance)")
                {
                    gate.GetComponent<Gate>().gateNumber = Random.Range(2, 15);
                    gate.GetComponentInChildren<TextMeshPro>().text += (gate.GetComponent<Gate>().gateNumber).ToString();
                }else
                {
                    gate.GetComponent<Gate>().gateNumber = Random.Range(-1, -5);
                    gate.GetComponentInChildren<TextMeshPro>().text = (gate.GetComponent<Gate>().gateNumber).ToString();
                }
            }
        }
    }
}
