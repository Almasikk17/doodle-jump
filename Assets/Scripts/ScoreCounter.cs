using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public TMP_Text ScoreText;
    public TMP_Text ScoreTextAnim;
    public float ScoreMultiplier;
    public static float ScorePerKill;
    
    private float _scoreNumber;
    private float _startPositionY;
    private Animator _textAnimator;

    private void Start()
    {
        _startPositionY = transform.position.y;
        _textAnimator = ScoreTextAnim.GetComponent<Animator>();
    }

    private void Update()
    {
        _scoreNumber += Mathf.Abs(transform.position.y - _startPositionY) * Random.Range(12,ScoreMultiplier);
        ScoreText.text = _scoreNumber.ToString("F0");
        _startPositionY = transform.position.y;
    }

    public void AddScore(int value)
    {
        Debug.Log("+500");
        _scoreNumber += value;
        ScoreText.text = _scoreNumber.ToString("F0");
    }

    private void AddScoreEnemyKill(Monsters monster)
    {
        UnregisterMonster();
        float killPoints = 250f;
        _scoreNumber += killPoints;
        _textAnimator.SetBool("ActivationTextAnim", true);
        StartCoroutine(StopTextAnim());
    }

    private IEnumerator StopTextAnim()
    {
        //AnimatorStateInfo stateInfo = _textAnimator.GetCurrentAnimatorStateInfo(0);
        /*if(stateInfo.normalizedTime > 1)
        {
            
        }*/

        yield return new WaitForSeconds(0.5f);
        _textAnimator.SetBool("ActivationTextAnim", false);
    }

    public void RegisterMonster(Monsters monster)
    {
        monster.OnEnemyDestroyed += AddScoreEnemyKill;
    }

    private void UnregisterMonster()
    {
        var monsters = FindObjectsOfType<Monsters>();
        foreach(var monster in monsters)
        {
            monster.OnEnemyDestroyed -= AddScoreEnemyKill;
        }
    }

    private void OnDestroy()
    {
        
    }

}

