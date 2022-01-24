using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogScript : MonoBehaviour
{
    [SerializeField] private BreakManager bm;
    [SerializeField] private Color bad, broken;
    public int BrokenStage { get; set; }

    public void Deteriorate()
    {
        GetComponent<SpriteRenderer>().color = bad;
        Debug.Log(gameObject.name + " is breaking");
        StartCoroutine(Break());
    }

    private IEnumerator Break()
    {
        float time = Random.Range(TimeManager.MinTimeDet, TimeManager.MaxTimeDet);
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().color = broken;
        BreakManager.numOfBrokenCogs++;
        if(BreakManager.numOfBrokenCogs == 4)
        {
            GameManager.loseState = true;
        }
        BrokenStage++;
    }

    public void FixCog()
    {
        if(BrokenStage > 0)
        {
            StopAllCoroutines();
            if (BrokenStage == 2)
            {
                BreakManager.numOfBrokenCogs--;
            }
            BrokenStage = 0;
            GetComponent<SpriteRenderer>().color = GameManager.OffColor;
            bm.AddCog(gameObject);
            ScoreManager.UpdateScore(2);
        }
    }
}
