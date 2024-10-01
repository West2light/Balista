using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Balista balista;
    public Transform balistaSpawn;
    public Arrow arrow;

    void Start()
    {
        CreateBalista();
    }

    void CreateBalista()
    {
        Balista objBalista = Instantiate(balista);
        objBalista.transform.position = balistaSpawn.position;
        objBalista.transform.localEulerAngles = Vector3.zero;

    }
}
