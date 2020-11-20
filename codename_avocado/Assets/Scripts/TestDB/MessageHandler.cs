using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class MessageHandler : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject editButton;
    public GameObject deleteButton;

    // for edited text
    public InputField textIF;

    public Message message;
    public string messageId; 
    public bool isOwner;

    void Start()
    {
        text.text = $"{message.senderNickname} : {message.text}";

        if (isOwner)
        {
            editButton.SetActive(true);
            deleteButton.SetActive(true);
        }
    }

    public void EditMessage()
    {
        APIHandler.Instance.databaseAPI.EditMessage(messageId, textIF.text, callback: () =>
        {
            Debug.Log("Message edited");
        }, Debug.Log);
    }

    public void DeleteMessage()
    {
        APIHandler.Instance.databaseAPI.DeleteMessage(messageId, callback: () =>
        {
            Debug.Log("Message deleted");
        }, Debug.Log);
    }
}
