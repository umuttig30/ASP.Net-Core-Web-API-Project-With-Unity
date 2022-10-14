using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace aspnetharjoitus
{
    public class Player : MonoBehaviour
    {
        public static Player instance;

        //Game character status information (Stats)
        [SerializeField] private int id;
        [SerializeField] private int currentHitPoints;
        [SerializeField] private int maxHitPoints;
        [SerializeField] private int gold;
        [SerializeField] private int exp;
        [SerializeField] private int currentLoactionID;
        public int Id { get => id; set => id = value; }
        public int CurrentHitPoints { get => currentHitPoints; set => currentHitPoints = value; }
        public int MaxHitPoints { get => maxHitPoints; set => maxHitPoints = value; }
        public int Gold { get => gold; set => gold = value; }
        public int Exp { get => exp; set => exp = value; }
        public int CurrentLoactionID { get => currentLoactionID; set => currentLoactionID = value; }

        private void Awake()
        {
            instance = this;   
        }
    }
}
