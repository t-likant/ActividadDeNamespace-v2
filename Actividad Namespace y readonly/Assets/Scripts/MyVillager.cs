namespace NPC
{
    namespace Ally
    {
        using UnityEngine;
        public struct VillagerStruct
        {
            // Datos del aldeano
            public enum nombresAldeano
            {
                Ruby, Yang, Blake, Weiss, Fernando, Jaune, Pyrrha, Nora, Lie, Sun,
                Neptune, Penny, Velvet, Glynda, Qrow, Winter, Neopolitan, Salem, Raven, Ozpin
            };
            public nombresAldeano nombreAldeano;
            public int edadAldeano;
        }
        public class MyVillager : MonoBehaviour
        {
            public VillagerStruct datosAldeano;
            void Awake()
            {
                datosAldeano.nombreAldeano = (VillagerStruct.nombresAldeano)Random.Range(0, 20); // SELECTOR DE NOMBRES
                datosAldeano.edadAldeano = Random.Range(15, 101); // SELECTOR DE EDAD
            }
        }
    }
}