
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System.Xml;


[System.Serializable]
public class SerializeKillTracker : MonoBehaviour
{
   [SerializeField] private int serializeKills;
    private string path;
    private void Update()
    {
        serializeKills = KillTracker.killsCounted;
        Invoke("XMLSave", 4);

        Invoke("LoadXML", 6);
    }

    private void XMLSave()
    {
        //SerializeKillTracker tracker = new SerializeKillTracker();
        XmlDocument document = new XmlDocument();

        XmlElement root = document.CreateElement("Save");
        root.SetAttribute("FileName", "File_01");

        XmlElement killed = document.CreateElement("KillCount");
        killed.InnerText = serializeKills.ToString();
        Debug.Log("This is the value you are saving: " + killed.InnerText);
        root.AppendChild(killed);

        document.AppendChild(root);


        document.Save(Application.dataPath + "/KillTracker.text");
        if(File.Exists(Application.dataPath + "/KillTracker.text"))
        {
            Debug.Log("XML Created!");
        }
    }

    private void LoadXML()
    {
        if(File.Exists(Application.dataPath + "/KillTracker.text"))
        {
            //load the data
            XmlDocument loaddocument = new XmlDocument();
            loaddocument.Load(Application.dataPath + "/KillTracker.text");

            XmlNodeList kill = loaddocument.GetElementsByTagName("KillCount");
            int loadkill = int.Parse(kill[0].InnerText);
            serializeKills = loadkill;
            Debug.Log("Your file loaded, the value of serializeKills is: " + serializeKills);
        }
        else
        {
            Debug.Log("Data not found!");
        }
    }
    
}

