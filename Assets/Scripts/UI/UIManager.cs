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

    // Lives Left UI
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;

    private GameManager _gameManager;



    // Start is called before the first frame update
    void Start()
    {
        // Assign text component to the handle
        _scoreText.text = "";
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL.");
        }

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
            _scoreText.fontSize = 30;
            _scoreText.color = Color.green;
            yield return new WaitForSeconds(0.2f);
            _playerScored = false;
            _scoreText.color = Color.yellow;
            _scoreText.fontSize = 25;
        }
    }

    public void UpdateLives(int currentLives) 
    {
        _LivesImg.sprite = _livesSprites[currentLives];
        if(currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);

    }
}
