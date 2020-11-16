using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SignInHandler : MonoBehaviour
{
    public TMP_InputField emailIF;
    public TMP_InputField nicknameIF;
    public TMP_InputField passwordIF;

    bool signInComplete;

    public void SignUp()
    {
        APIHandler.Instance.authAPI.SignupUser(emailIF.text, passwordIF.text, callback: () =>
        {
            var user = new User { nickname = nicknameIF.text };
            APIHandler.Instance.databaseAPI.PostUser(user, 
                callback: () =>
                {
                    APIHandler.Instance.authAPI.CurrentUser = user;
                    signInComplete = true;
                }, Debug.Log);
        }, Debug.Log);
    }

    public void SignIn()
    {
        APIHandler.Instance.authAPI.SigninUser(emailIF.text, passwordIF.text, 
            callback: () => APIHandler.Instance.databaseAPI.GetUser( user =>
            {
                APIHandler.Instance.authAPI.CurrentUser = user;
                signInComplete = true;
            }, Debug.Log),
            Debug.Log);
    }

    void Update()
    {
        if(signInComplete)
        {
            SceneManager.LoadScene(2);
        }
    }
}
