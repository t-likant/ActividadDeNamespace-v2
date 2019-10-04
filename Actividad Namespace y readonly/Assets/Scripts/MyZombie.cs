namespace NPC
{
    namespace Enemy
    {
        using UnityEngine;
        using System.Collections;

        public struct ZombieStruct
        {
            // Datos del zombi
            public int colorZombi;
            public enum gustosZombi { cerebro, corazon, higado, nariz, lengua };
            public gustosZombi gustoZombi;
            public enum estadosZombi { Idle, Moving, Rotating };
            public estadosZombi estadoZombi;
        }
        public class MyZombie : MonoBehaviour
        {
            public ZombieStruct datosZombie;
            int seMueve, selectorDireccional;
            public void Awake()
            {
                datosZombie.gustoZombi = (ZombieStruct.gustosZombi)Random.Range(0, 5);
                datosZombie.colorZombi = Random.Range(0, 3);
            }
            void Start()
            {
                StartCoroutine(ComportamientoZombie(datosZombie));
            }
            IEnumerator ComportamientoZombie(ZombieStruct gameStruct)
            {
                while (true)
                {
                    gameStruct.estadoZombi = (ZombieStruct.estadosZombi)Random.Range(0, 3);
                    switch (gameStruct.estadoZombi)
                    {
                        case ZombieStruct.estadosZombi.Idle: // Para que se quede quieto
                            seMueve = 0;
                            break;
                        case ZombieStruct.estadosZombi.Moving: // Para que se mueva hacia el frente
                            seMueve = 1;
                            break;
                        case ZombieStruct.estadosZombi.Rotating: // Para que rote
                            seMueve = 2;
                            selectorDireccional = Random.Range(0, 2);
                            break;
                    }
                    yield return new WaitForSeconds(3.0f); // Espera 3 segundos y cambia de comportamiento
                }
            }

            void Update()
            {
                if (seMueve == 0) { } // Idle

                if (seMueve == 1) // Moving
                {
                    transform.position += transform.forward * 0.6f * Time.deltaTime;
                }

                if (seMueve == 2) // Rotating
                {
                    if (selectorDireccional == 0) // Rotacion Positiva
                    {
                        transform.eulerAngles += new Vector3(0, Random.Range(10f, 150f) * Time.deltaTime, 0);
                    }
                    if (selectorDireccional == 1) // Rotacion Negativa
                    {
                        transform.eulerAngles += new Vector3(0, Random.Range(-10f, -150f) * Time.deltaTime, 0);
                    }
                }
            }
        }
    }
}