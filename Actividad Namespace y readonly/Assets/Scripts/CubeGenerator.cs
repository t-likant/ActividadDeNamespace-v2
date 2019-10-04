﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPC.Enemy;
using NPC.Ally;
using TMPro;

public class CubeGenerator : MonoBehaviour
{
    static System.Random r = new System.Random();
    public readonly int limiteMinimo = r.Next(5,15);
    const int limiteMaximo = 25;

    int nAlly = 0, nEnemy = 0, limiteGenerado,generadorRandom;
    // HEROE VARIABLES Y FUNCION GENERADORA
    public GameObject cuboHeroe;
    GameObject heroe;
    public GameObject camaraHeroe;
    GameObject camara;
    Vector3 posHero;
    Vector3 camPos;
    GameObject enemys;
    GameObject allys;

    public TextMeshProUGUI nEnemigos; 
    public TextMeshProUGUI nAliados;

    public void CreacionHeroe()
    {
        // CREACION DEL HEROE
        posHero = new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f));
        heroe = GameObject.Instantiate(cuboHeroe, posHero, Quaternion.identity);
        heroe.name = "Heroe";
        heroe.AddComponent<MyHero>();
        heroe.AddComponent<HeroMove>();


        // CREACION DE LA CAMARA QUE SIGUE AL HEROE
        camPos = new Vector3(heroe.transform.position.x, heroe.transform.position.y + 0.8f, heroe.transform.position.z);
        camara = Instantiate(camaraHeroe, camPos, Quaternion.identity);
        camara.AddComponent<HeroCam>();
        camara.name = "Camara Heroe";
        camara.transform.SetParent(heroe.transform);
    }
    // ZOMBIE VARIABLES Y FUNCION GENERADORA
    int colorZombie;
    public GameObject zombie;
    public void CreacionZombie(GameObject enemigos)
    {
        zombie = GameObject.CreatePrimitive(PrimitiveType.Cube);
        zombie.name = "Zombie";
        zombie.AddComponent<MyZombie>();
        Vector3 posZombi = new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f));
        zombie.transform.position = posZombi;

        switch (zombie.GetComponent<MyZombie>().datosZombie.colorZombi)
        {
            case 0:
                zombie.GetComponent<Renderer>().material.color = Color.cyan;
                break;
            case 1:
                zombie.GetComponent<Renderer>().material.color = Color.green;
                break;
            case 2:
                zombie.GetComponent<Renderer>().material.color = Color.magenta;
                break;
        }
        zombie.AddComponent<Rigidbody>();
        zombie.GetComponent<Rigidbody>().freezeRotation = true;
        zombie.transform.SetParent(enemigos.transform);
    }
    // ALDEANO VARIABLES Y FUNCION GENERADORA
    public GameObject aldeano;
    public void CreacionAldeano(GameObject aliados)
    {
        aldeano = GameObject.CreatePrimitive(PrimitiveType.Cube); // CREA LA FIGURA SOLICITADA
        Vector3 posAldeano = new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f)); // ELIGE UNA POSICION ALEATORIA
        aldeano.transform.position = posAldeano; // ASIGNA LA POSICION A UNA VARIABLE
        aldeano.GetComponent<Renderer>().material.color = Color.black; // ASIGNACION DE UN COLOR AL ALDEANO
        aldeano.GetComponent<Transform>().localScale = new Vector3(1.0f, 2.0f, 1.0f); // ASIGNA UN COLOR PARA IDENTIFICAR A LOS ALDEANOS
        aldeano.AddComponent<Rigidbody>().freezeRotation = true; // AÑADE CUERPO SOLIDO AL ZOMBIE Y CONGELA LA ROTACION 
        aldeano.name = "Aldeano"; // NOMBRE DEL ALDEANO EN LA JERARQUIA
        aldeano.AddComponent<MyVillager>();
        aldeano.transform.SetParent(aliados.transform);
    }

    void Start()
    {
        limiteGenerado = Random.Range(limiteMinimo, limiteMaximo+1);
        // CREACION DEL CONJUNTO DE LOS ALDEANOS
        allys = new GameObject();
        allys.name = "Allys";
        // CREACION DEL CONJUNTO DE LOS ZOMBIS
        enemys = new GameObject();
        enemys.name = "Enemys";
        for (int i = 0; i < limiteGenerado; i++)
        {
            generadorRandom = Random.Range(0, 2);
            if (generadorRandom == 0)
                CreacionZombie(enemys);

            if (generadorRandom == 1)
                CreacionAldeano(allys);
        }
        // CREACION DEL HEROE
        CreacionHeroe();

        var zombieList = FindObjectsOfType<MyZombie>();
        foreach (var item in zombieList)
        {
            nEnemigos.text = zombieList.Length.ToString();
        }

        var villagerList = FindObjectsOfType<MyVillager>();
        foreach (var item in villagerList)
        {
            nAliados.text = villagerList.Length.ToString();
        }
    }
}