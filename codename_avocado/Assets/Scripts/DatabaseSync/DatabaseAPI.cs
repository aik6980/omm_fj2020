using Firebase;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseAPI : MonoSingleton<DatabaseAPI>
{
    /// <summary>
    /// 
    /// WorldIds [
    ///     { id: "World19727437291", done: "true" },
    ///     { id: "World28749749123", done: "false" }
    /// ]
    /// 
    /// Worlds [
    ///     World19727437291
    ///     {
    ///         Pieces: [
    ///             Piece1 { x: 1, y: 1, shape: "L", UserId: "jfhshfks1" },
    ///             Piece2 { x: 1, y: 3, shape: "L", UserId: "jfhshfks1" },
    ///             Piece3 { x: 54, y: 2, shape: "L", UserId: "maadhfks2" },
    ///             Piece4 { x: 11, y: 2, shape: "L", UserId: "uiosyuts4" }
    ///         ]
    ///     },
    ///     World28749749123
    ///     {
    ///         Pieces: [
    ///             Piece1 { x: 1, y: 6, shape: "L" },
    ///             Piece2 { x: 4, y: 1, shape: "L" }
    ///         ]
    ///     }
    /// ]
    ///     
    /// Contributers [
    ///     World19727437291
    ///     {
    ///         Users:  [
    ///             { name: "Aik", UserId: "jfhshfks1" },
    ///             { name: "Phil" UserId: "maadhfks2" },
    ///             { name: "Matt", UserId: "uiosyuts4" }
    ///         ]
    ///     }
    /// ]
    /// 
    /// </summary>


    private DatabaseReference reference;

    EventHandler<ChildChangedEventArgs> newMessageListener;
    EventHandler<ChildChangedEventArgs> editedMessageListener;
    EventHandler<ChildChangedEventArgs> deletedMessageListener;

    void Awake()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public struct WorldData
    { }

    [Serializable]
    public class UnfoldData
    {
        public int          x;
        public int          y;
        public string       piece_shape;
        public string       creator;
    }


    public WorldData GetWorld()
    {
        return new WorldData();
    }

    public void SyncUnfold(UnfoldData data, Action<UnfoldData> callback, Action<AggregateException> fallback)
    {
        var json = StringSerializationAPI.Serialize(typeof(UnfoldData), data);
        reference.Child("worlds/world_1234/pieces").Push().SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                fallback(task.Exception);
            }
            else
            {
                callback(data);
            }
        });
    }



    //public void PostMessage(Message message, Action callback, Action<AggregateException> fallback)
    //{
    //    var messageJSON = StringSerializationAPI.Serialize(typeof(Message), message);
    //    reference.Child("messages").Push().SetRawJsonValueAsync(messageJSON).ContinueWith(task =>
    //    {
    //        if (task.IsCanceled || task.IsFaulted)
    //            fallback(task.Exception);
    //        else
    //            callback();
    //    });
    //}

    //public void EditMessage(string messageId, string newText, Action callback, Action<AggregateException> fallback)
    //{
    //    var messageJSON = StringSerializationAPI.Serialize(typeof(string), newText);
    //    reference.Child($"messages/{messageId}/text").SetRawJsonValueAsync(messageJSON).ContinueWith(task =>
    //    {
    //        if (task.IsCanceled || task.IsFaulted)
    //            fallback(task.Exception);
    //        else
    //            callback();
    //    });
    //}

    //public void DeleteMessage(string messageId, Action callback, Action<AggregateException> fallback)
    //{
    //    reference.Child($"messages/{messageId}").SetRawJsonValueAsync(null).ContinueWith(task =>
    //    {
    //        if (task.IsCanceled || task.IsFaulted)
    //            fallback(task.Exception);
    //        else
    //            callback();
    //    });
    //}

    public void ListenForUnfold(Action<UnfoldData> callback, Action<AggregateException> fallback)
    {
        void CurrentListener(object o, ChildChangedEventArgs args)
        {
            if (args.DatabaseError != null)
                fallback(new AggregateException(new Exception(args.DatabaseError.Message)));
            else
                callback(StringSerializationAPI.Deserialize(typeof(UnfoldData), args.Snapshot.GetRawJsonValue()) as UnfoldData);
        }

        newMessageListener = CurrentListener;
        reference.Child("worlds/world_1234/pieces").ChildAdded += newMessageListener;
    }

    //public void ListenForEditedMessages(Action<string, string> callback, Action<AggregateException> fallback)
    //{
    //    void CurrentListener(object o, ChildChangedEventArgs args)
    //    {
    //        if (args.DatabaseError != null)
    //            fallback(new AggregateException(new Exception(args.DatabaseError.Message)));
    //        else
    //            callback(args.Snapshot.Key,
    //                args.Snapshot.Child("text").GetRawJsonValue());
    //    }
    //
    //    editedMessageListener = CurrentListener;
    //    reference.Child("messages").ChildChanged += editedMessageListener;
    //}

    //public void ListenForDeletedMessages(Action<string> callback, Action<AggregateException> fallback)
    //{
    //    void CurrentListener(object o, ChildChangedEventArgs args)
    //    {
    //        if (args.DatabaseError != null)
    //            fallback(new AggregateException(new Exception(args.DatabaseError.Message)));
    //        else
    //            callback(args.Snapshot.Key);
    //    }
    //
    //    deletedMessageListener = CurrentListener;
    //    reference.Child("messages").ChildRemoved += deletedMessageListener;
    //}

    public void StopListenForMessage()
    {
        reference.Child("worlds/world_1234/pieces").ChildAdded -= newMessageListener;
        //reference.Child("messages").ChildChanged -= editedMessageListener;
        //reference.Child("messages").ChildRemoved -= deletedMessageListener;
    }

    //public void PostUser(User user, Action callback, Action<AggregateException> fallback)
    //{
    //
    //    var messageJSON = StringSerializationAPI.Serialize(typeof(User), user);
    //    reference.Child($"users/{APIHandler.Instance.authAPI.GetUserID}").SetRawJsonValueAsync(messageJSON).ContinueWith(task =>
    //    {
    //        if (task.IsCanceled || task.IsFaulted)
    //            fallback(task.Exception);
    //        else
    //            callback();
    //    });
    //}
    //
    //public void GetUser(Action<User> callback, Action<AggregateException> fallback)
    //{
    //    reference.Child($"users/{APIHandler.Instance.authAPI.GetUserID}").GetValueAsync().ContinueWith(task =>
    //    {
    //        if (task.IsCanceled || task.IsFaulted)
    //            fallback(task.Exception);
    //        else
    //            callback(StringSerializationAPI.Deserialize(typeof(User), task.Result.GetRawJsonValue()) as User);
    //    });
    //}
}
