using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase.Auth;
using System;

public class AuthAPI : MonoBehaviour
{
    FirebaseAuth auth;

    User currentUser;

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SignupUser(string email, string password, Action callback, Action<AggregateException> fallback)
    {

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
                fallback(task.Exception);
            else
                callback();
        });
    }

    public void SigninUser(string email, string password, Action callback, Action<AggregateException> fallback)
    {

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
                fallback(task.Exception);
            else
                callback();
        });
    }

    public void SignOut() => auth.SignOut();

    public string GetUserID => auth.CurrentUser.UserId;

    public User CurrentUser { get => currentUser; set => currentUser = value; }
}
