using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnScreenMessage
{
    public GameObject go;
    public float timeToLive;

    public OnScreenMessage(GameObject go)
    {
        this.go = go;
    }
}

public class OnScreeMessageSystems : MonoBehaviour
{
    [SerializeField] GameObject textPrefab;

    List<OnScreenMessage> onScreenMessagesList;
    List<OnScreenMessage> openList;

    void Awake()
    {
        onScreenMessagesList = new List<OnScreenMessage>();
        openList = new List<OnScreenMessage>();
    }

    void Update()
    {
        for(int i = onScreenMessagesList.Count - 1; i >= 0; i--)
        {
            onScreenMessagesList[i].timeToLive -= Time.deltaTime;
            if(onScreenMessagesList[i].timeToLive < 0)
            {
                onScreenMessagesList[i].go.SetActive(false);
                openList.Add(onScreenMessagesList[i]);
                onScreenMessagesList.RemoveAt(i);
            }
        }
    }

    public void PostMessage(Vector3 worldPosition, string message)
    {
        worldPosition.z = -1f;

        if (openList.Count > 0)
        {
            ReuseObjectFromOpenList(worldPosition, message);
        }
        else
        {
            CreateNewOnScreenMessageObject(worldPosition, message);
        }
    }

    private void ReuseObjectFromOpenList(Vector3 worldPosition, string message)
    {
        OnScreenMessage osm = openList[0];
        osm.go.SetActive(true);
        osm.timeToLive = 6f;
        osm.go.GetComponent<TextMeshPro>().text = message;
        osm.go.transform.position = worldPosition;
        openList.RemoveAt(0);
        onScreenMessagesList.Add(osm);
    }

    private void CreateNewOnScreenMessageObject(Vector3 worldPosition, string message)
    {
        GameObject textGo = Instantiate(textPrefab, transform);
        textGo.transform.position = worldPosition;

        TextMeshPro tmp = textGo.GetComponent<TextMeshPro>();
        tmp.text = message;

        OnScreenMessage onScreenMessage = new OnScreenMessage(textGo);
        onScreenMessage.timeToLive = 6f;
        onScreenMessagesList.Add(onScreenMessage);
    }
}
