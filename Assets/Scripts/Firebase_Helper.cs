using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;
using Query = Firebase.Firestore.Query;

public class Firebase_Helper
{
    
    /*
     * Firebase asynchronous method that returns a local copy of
     * the questions pulled from our database.
     *
     * Ideally this should be called only once in the start method
     * and only if it has not been called earlier.
     *
     * Callers of this method should be aware this method can throw an
     * error and "should" protect by using a try block.
     */
    public static async Task<List<Dictionary<string, object>>> updateQuestions()
    {
        List<Dictionary<string, object>> local_questions = new List<Dictionary<string, object>>();
        try
        {
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            
            Query questions = db.Collection("questions").WhereEqualTo("question_perspective", utils.question_perspective);
            await questions.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                foreach (DocumentSnapshot documentSnapshot in task.Result.Documents)
                {
                    Dictionary<string, object> question = documentSnapshot.ToDictionary();
                    local_questions.Add(question);
                }
            });
            return local_questions;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    /*
     * signInExistingUser() is a method that will attempt to sign in a user that already has
     * an account. This method will return a bool to the caller. If this method returns true then the user
     * exists and is now logged in. If this method returns false then the user was not found.
     *
     * This method also operates on the assumption that the email & password have already been
     * verified. To be more concise, do not send the raw user input to this function if it has not
     * been verified.
     */
    public static async Task<bool> signInExistingUser(String email, String password)
    {
        Debug.LogFormat("email: {0}\n password: {1}", email, password);
        var authInstance = FirebaseAuth.DefaultInstance;
        
        bool answer = await authInstance.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return false;
            }

            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return false;
            }
            
            return true;
        });
        return answer;
    }


    /*
     * createNewUser() takes in an email and password, and attempts to create a user in the system. 
     * This method returns true if the user was created successfully, and false if the creation
     * encountered an error.
     *
     * The caller should only pass in "cleaned" data. This method assumes the email and password are
     * formatted correctly.
     */
    public static async Task<bool> createNewUser(String email, String password, String username)
    {
        Debug.LogFormat("email: {0}\n password: {1}", email, password);
        var authInstance = FirebaseAuth.DefaultInstance;
        bool created = await authInstance.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return false;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return false;
            }
            return true;
        });

        return created;
    }

    public static async Task<bool> addScoreToLeaderBoard()
    {
        if (utils.score == 0)
        {
            return true;
        }
        var db = FirebaseFirestore.DefaultInstance;
        DocumentReference newDoc = db.Collection("leaderboard").Document();
        Dictionary<string, object> score = new Dictionary<string, object>
        {
            { "email", FirebaseAuth.DefaultInstance.CurrentUser.Email },
            { "score", utils.score }
        };
       bool added = await newDoc.SetAsync(score).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                return false;
            }

            if (task.IsCanceled)
            {
                return false;
            }
            return true;
        });
        return added;
    }

    public static bool signOutUser()
    {
        var authInstance = FirebaseAuth.DefaultInstance;
        var user = authInstance.CurrentUser;
        if (user != null)
        {
            Debug.Log(user.Email);
            Debug.Log("Is now signing out");
            authInstance.SignOut();
            return true;
        }
        Debug.Log("User is null, nothing to sign out");
        return false;
    }

    public static bool isUserSignedIn()
    {
        var authInstance = FirebaseAuth.DefaultInstance;
        var user = authInstance.CurrentUser;
        return user != null;
    }

    public static async Task<Dictionary<string, object>> getHighScore()
    {
        Dictionary<string, object> answer = new Dictionary<string, object>();
        try
        {
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            CollectionReference leaderboard = db.Collection("leaderboard");
            Query query = leaderboard.WhereEqualTo("email", FirebaseAuth.DefaultInstance.CurrentUser.Email).OrderByDescending("score").Limit(1);
            
            await query.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                foreach (DocumentSnapshot documentSnapshot in task.Result.Documents)
                {
                    answer = documentSnapshot.ToDictionary();
                }

            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return answer;
    }


    public static async Task<List<Dictionary<string, object>>> getScoreFromLeaderboard()
    {
        List<Dictionary<string, object>> entries = new List<Dictionary<string, object>>();
        try
        {
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            CollectionReference leaderboard = db.Collection("leaderboard");
            Query query = leaderboard.OrderByDescending("score").Limit(10);
            await query.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                foreach (DocumentSnapshot documentSnapshot in task.Result.Documents)
                {
                    Dictionary<string, object> entry = documentSnapshot.ToDictionary();
                    entries.Add(entry);
                    foreach (KeyValuePair<string, object> pair in entry)
                    {
                        Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                    }
                }
            });
            return entries;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }

}