using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class SignInHandler : MonoBehaviour
{
    public TMP_InputField playernameIF;
    string uuid;

    bool signInComplete;

    public void SignUp(string email, string password)
    {
        APIHandler.Instance.authAPI.SignupUser(email, password, callback: () =>
        {
            var user = new User { nickname = playernameIF.text };
            APIHandler.Instance.databaseAPI.PostUser(user, 
                callback: () =>
                {
                    APIHandler.Instance.authAPI.CurrentUser = user;
                    signInComplete = true;
                }, Debug.Log);
        }, Debug.Log);
    }

    public void SignIn(string email, string password)
    {
        APIHandler.Instance.authAPI.SigninUser(email, password, 
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

    string get_email_name()
    {
        return playernameIF.text + "?#" + uuid + "@dummy.foo";
    }

    public void StartGame()
    {
        string path = Application.persistentDataPath + "/userdata.id";
        //Debug.Log(Application.persistentDataPath);
        // load userdata file
        if (System.IO.File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);
            uuid = reader.ReadLine();

            // 
            if(uuid.Length > 0)
            {
                SignIn(get_email_name(), uuid);
            }
        }
        else
        {
            // create a unique userID
            StreamWriter writer = new StreamWriter(path);
            System.Guid guid = System.Guid.NewGuid();
            uuid = guid.ToString();

            // save to file for next sign in
            writer.WriteLine(uuid);
            writer.Close();

            // auto signup to firebase with playername + uuid
            Debug.Log(get_email_name());
            SignUp(get_email_name(), uuid);
        }
    }
}
