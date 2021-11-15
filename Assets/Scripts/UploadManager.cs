using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Firestore;
using Firebase.Extensions;
using Query = Firebase.Firestore.Query;

[System.Serializable]
public class JsonPlay
{
    public string book_number;
    public string question_text;
    public string outs;
    public bool runner_on_first;
    public bool runner_on_second;
    public bool runner_on_third;
    public string question_perspective;
    public string answer_base;
    public string ball_hit_to;
    public string movement;
}

[System.Serializable]
public class JsonPlayList
{
    public JsonPlay[] questions;
}

public class UploadManager : MonoBehaviour
{
    public TextAsset jsonFile;

    FirebaseFirestore db;

    // Start is called before the first frame update
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        playList = LoadQuestionsFromFile();

        foreach (JsonPlay play in playList.questions)
        {
            UploadData(play);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal JsonPlayList playList;


    internal JsonPlayList LoadQuestionsFromFile()
    {

        JsonPlayList plays = JsonUtility.FromJson<JsonPlayList>(jsonFile.text);

        //Debug.Log(plays.questions[0].book_number);

       

        return plays;
    }

    private void UploadData(JsonPlay play) {

        DocumentReference docRef = db.Collection("questions").Document();
        Dictionary<string, object> docData = new Dictionary<string, object>
{
        { "book_number", play.book_number },
        { "question_text", play.question_text },
        { "outs", play.outs },
        { "runner_on_first", play.runner_on_first },
        { "runner_on_second", play.runner_on_second },
        { "runner_on_third", play.runner_on_third },
        { "question_perspective", play.question_perspective },
        { "answer_base", play.answer_base },
        { "ball_hit_to", play.ball_hit_to },
        { "movement", play.movement },


};

        docRef.SetAsync(docData);
    }
}