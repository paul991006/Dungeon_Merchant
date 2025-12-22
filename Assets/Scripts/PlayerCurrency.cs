using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using System.Collections.Generic;

public class PlayerCurrency : MonoBehaviour
{
    public int essence;

    private DatabaseReference db;
    private string uid;

    private void Awake()
    {
        uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        db = FirebaseDatabase.DefaultInstance.RootReference;
        
        Load();
    }

    public void AddEssence(int amount)
    {
        essence += amount;
        Save();
    }

    public bool UseEssence(int amount)
    {
        if (essence < amount) return false;
        essence -= amount;
        Save();
        return true;
    }

    void Load()
    {
        db.Child("users").Child(uid).Child("currency").Child("essence").GetValueAsync().ContinueWith(task =>
          {
              if (task.IsCompleted && task.Result != null) essence = int.Parse(task.Result.Value.ToString());
          });
    }

    void Save()
    {
        //기존 currency 외 다른 데이터 덮어쓰기 방지
        var updates = new Dictionary<string, object>
        {
            { "essence", essence }
        };

        db.Child("users").Child(uid).Child("currency").UpdateChildrenAsync(updates);
    }
}
