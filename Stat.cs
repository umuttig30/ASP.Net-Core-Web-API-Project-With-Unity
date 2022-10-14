using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace aspnetharjoitus
{
    [System.Serializable]
    public class Stat
    {
        public int id;
        public int currentHitPoints;
        public int maxHitPoints;
        public int gold;
        public int exp;
        public int currentLocationID;

        public Stat() { }

        public Stat(int id, int currentHitPoints, int maxHitPoints, int gold, int exp, int currentLocationID)
        {
            this.id = id;
            this.currentHitPoints = currentHitPoints;
            this.maxHitPoints = maxHitPoints;
            this.gold = gold;
            this.exp = exp;
            this.currentLocationID = currentLocationID;
        }

        //Retrieves the game character's state information from the database (JSON)
        public IEnumerator LoadStaDataFromDatabase(string uri, TMP_InputField outputArea)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(uri))
            {
                //Wait for response
                yield return request.SendWebRequest();
                //checks for errors
                if (request.error != null)
                {
                    outputArea.text = $"Nettivirhe: {request.error}";
                }
                else
                {
                    outputArea.text = request.downloadHandler.text;
                }

                //takes the JSON from the database and removes the [ and ] characters
                string newOutputArea = outputArea.text.Remove(0, 1);
                string newOutputArea2 = newOutputArea.Remove
                (newOutputArea.Length - 1, 1);

                 
                Stat stat = JsonUtility.FromJson<Stat>(newOutputArea2);

                //update the game character's status information
                Player.instance.Id = stat.id;
                Player.instance.CurrentHitPoints = stat.currentHitPoints;
                Player.instance.MaxHitPoints = stat.maxHitPoints;
                Player.instance.Gold = stat.gold;
                Player.instance.Exp = stat.exp;
                Player.instance.CurrentLoactionID = stat.currentLocationID;
            }

        }

        //Save the status information of the game character to the database (JSON)
        public IEnumerator SaveStatDataToDatabase(string uri, TMP_InputField outputArea)
        {
            //Create a storage JSON structure
            string id = $"\"id\":{this.id},";
            string currentHitpoints = $"\"currentHitPoints\":{this.currentHitPoints},";
            string maxHitpoints = $"\"maxHitpoints\":{this.maxHitPoints},";
            string gold = $"\"gold\":{this.gold},";
            string exp = $"\"exp\":{this.exp},";
            string currentLoactionID = $"\"currentLocationID\":{this.currentLocationID}";
            string bodyData = "{" + id + currentHitpoints + maxHitpoints + gold + exp + currentLoactionID + "}";


            //Asks the server to update (PUT) status information
            using (UnityWebRequest request = UnityWebRequest.Put(uri, bodyData))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                yield return request.SendWebRequest();
                if (request.error != null)
                {
                    outputArea.text = $"Nettivirhe : {request.error}";
                }
                else
                {
                    outputArea.text = request.downloadHandler.text;
                }
            }
        }
    }
}