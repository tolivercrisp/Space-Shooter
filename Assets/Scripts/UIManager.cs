using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // new package to include to access unity Text object;

public class UIManager : MonoBehaviour
{
    // Handle to text
    [SerializeField]
    private Text _scoreText;
    private bool _playerScored = false;




    // Start is called before the first frame update
    void Start()
    {
        // Assign text component to the handle
        _scoreText.text = "";
        
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = playerScore.ToString();
        _playerScored = true;
        StartCoroutine(UpdateScoreVisualRoutine());

    }

    public IEnumerator UpdateScoreVisualRoutine()
    {
        while(_playerScored == true)
        {
            _scoreText.color = Color.green;
            _scoreText.fontSize = 25;
            yield return new WaitForSeconds(0.2f);
            _playerScored = false;
            _scoreText.color = Color.yellow;
            _scoreText.fontSize = 20;
        }
    }
}
