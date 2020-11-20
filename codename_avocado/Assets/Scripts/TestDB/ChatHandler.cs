using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChatHandler : MonoBehaviour
{
    public FirebaseMgr database;

    public InputField textIF;

    public GameObject messagePrefab;
    public Transform messageContainer;

    bool signOutComplete;

    public Dictionary<string, MessageHandler> messageHandlerList = new Dictionary<string, MessageHandler>();

    // Start is called before the first frame update
    void Start()
    {
        APIHandler.Instance.databaseAPI.ListenForNewMessages(InstantiateMessage, Debug.Log);
        APIHandler.Instance.databaseAPI.ListenForEditedMessages(EditMessage, Debug.Log);
        APIHandler.Instance.databaseAPI.ListenForDeletedMessages(DeleteMessage, Debug.Log);
    }

    // Update is called once per frame
    void Update()
    {
        if(signOutComplete)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void SendMessage()
    {
        APIHandler.Instance.databaseAPI.PostMessage(
            new Message(APIHandler.Instance.authAPI.CurrentUser.nickname, 
                APIHandler.Instance.authAPI.GetUserID, textIF.text),
            callback: () => { Debug.Log("Message was sent"); },
            fallback: exception => { Debug.Log(exception); });
    }

    void InstantiateMessage(Message message, string messageId)
    {
        var messageGO = Instantiate(messagePrefab, transform.position, Quaternion.identity);
        messageGO.transform.SetParent(messageContainer, worldPositionStays: false);

        var messageHandler = messageGO.GetComponent<MessageHandler>();

        messageHandler.textIF    = textIF;
        messageHandler.messageId = messageId;
        messageHandler.message = message;
        messageHandler.isOwner = (APIHandler.Instance.authAPI.GetUserID == message.senderUserId);

        messageHandlerList.Add(messageId, messageHandler);
    }

    void EditMessage(string messageId, string newText)
    {
        var msg = messageHandlerList[messageId].GetComponent<MessageHandler>();
        messageHandlerList[messageId].text.text = $"{msg.message.senderNickname} : {newText}";
    }

    void DeleteMessage(string messageId)
    {
        Destroy(messageHandlerList[messageId].gameObject);
        messageHandlerList.Remove(messageId);
    }

    public void SignOut()
    {
        APIHandler.Instance.databaseAPI.StopListenForMessage();
        APIHandler.Instance.authAPI.SignOut();
        signOutComplete = true;
    }
}
