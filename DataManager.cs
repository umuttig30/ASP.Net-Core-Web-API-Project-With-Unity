using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;       //For data communication

namespace aspnetharjoitus
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField outputArea;

        

        public void GetData()
        {
            outputArea.text = null;
            string uri = "https://localhost:7037/superadventure"; 
            Stat stat = new Stat();
            StartCoroutine(stat.LoadStaDataFromDatabase(uri, outputArea));
        }

        public void PutData()
        {
            outputArea.text = "Loading...";
            string uri = "https://localhost:7037/superadventure/1";


            //Create a Stat object
            Stat stat = new Stat(Player.instance.Id, Player.instance.CurrentHitPoints,
                Player.instance.MaxHitPoints, Player.instance.Gold, Player.instance.Exp, Player.instance.CurrentLoactionID);

            //Performs an update
            StartCoroutine(stat.SaveStatDataToDatabase(uri, outputArea));
        }
    }
}
