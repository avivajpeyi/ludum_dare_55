using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{ 
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;
    public string publicKey = "7c387bb7073facc2a946b6cfd5c1e2681ac3c4abc37b0e23629c7a450a68284f";
    // Secret key: 8b88d8b3c9a3d820c32c6c8850d31016f6a7e82aa8f2d0fc43e786f63338ed15e68b1136076d640ccc1127a0af81ef3921ab72668b7da44a2cc911298e0fdecb0f53da02e39ec69bd57c8e15fe99ebf31fdb8ed8bf484def308236436faf134750d75a2f72de97f9417ab2e218876f1b656e11216c9ca3ba12195e8d3c9054de

    private void Start()
    {
        GetLeaderboard();
    }
    
    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicKey, ((msg) => {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; ++i) {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboard(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicKey, username, score, ((msg) => {
            GetLeaderboard();
        }));
    }
}

