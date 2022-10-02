using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PuzzleManager : MonoBehaviour, ISoundable {
    // public static PuzzleManager instance;

    public TextMeshProUGUI n1;
    public TextMeshProUGUI n2;
    public TextMeshProUGUI signBetween;

    public TMP_InputField playersAnswer;

    public delegate void PuzzleEvent();
    public static event PuzzleEvent OnPuzzleSolved;
    // I can fill this out.
    public static event PuzzleEvent OnPuzzleFailed;

    // Puzzle sounds:
    [SerializeField] AudioClip soundPuzzlePanelShowUp;
    [SerializeField] AudioClip soundPuzzlePanelHideUp;
    // Spike sounds:
    [SerializeField] AudioClip soundSpikeDestroy;

    // Puzzle animator:
    [SerializeField] Animator animatorPuzzlePanel;

    // Spike animator:
    [SerializeField] Animator animatorSpike;
    int solution;

    //private void Awake() {
    //    if (PuzzleManager.instance != null) {
    //        return;
    //    }
    //    instance = this;
    //}

    public void CheckSolution() {

        if (playersAnswer.text.ToString() == solution.ToString()) {
            PuzzlePanelHideUp();
            SpikeDestroy();
        } else {
            playersAnswer.text = "";
        }

    }
    private void Start() {
        CreateMathQuestion();
        LogValues();
    }


    private void CreateMathQuestion() {

        int n1Value = Random.Range(10, 20);
        int n2Value = Random.Range(10, 20);

        n1.text = n1Value.ToString();
        n2.text = n2Value.ToString();

        Debug.Log($"n1Value: {n1Value}");
        Debug.Log($"n2Value: {n2Value}");

        int valueRandom = Random.Range(1, 4);
        Debug.Log(valueRandom);

        switch (valueRandom) {
            case 1: //  +
                solution = n1Value + n2Value;
                signBetween.text = "+";
                break;

            case 2:
                solution = n1Value - n2Value;
                signBetween.text = "-";
                break;

            case 3:
                solution = n1Value * n2Value;
                signBetween.text = "*";
                break;

            case 4:
                solution = n1Value / n2Value;
                signBetween.text = "/";
                break;
            
            default:
                break;
        }
        
        
        
        //solution = Random.Range(0, 3) switch {

        //    0 => solution = n1Value + n2Value,
        //    1 => solution = n1Value - n2Value,
        //    2 => solution = n1Value * n2Value,
        //    3 => solution = n1Value / n2Value,
        //    _ => solution = n1Value + n2Value
        //};

        //Debug.Log(solution.ToString());
        //signBetween.text = solution switch {
        //    0 => "+",
        //    1 => "-",
        //    2 => "*",
        //    3 => "/",
        //    _ => "+"
        //};



    }

    private void LogValues() {
        Debug.Log($"{n1.text} {signBetween.text} {n2.text} ");
        Debug.Log($"S: {solution} | A: {playersAnswer.text}");
    }

    //xOffset = player.GetComponent<PlayerController>().BirdsAttached switch {
    //    0 => 5,
    //    1 => 8,
    //    2 => 1,
    //    _ => 0
    //};

    public void EventAssignation() {
        //OnPuzzleSolved += PuzzlePanelHideUp;
        //OnPuzzleSolved += SpikeDestroy;
        OnPuzzleSolved += CheckSolution;
    }

    public void SolvePuzzle() {
        OnPuzzleSolved();
    }

    public void PuzzlePanelShowUp() {
        animatorPuzzlePanel.SetTrigger("ShowUp");
        PlaySound(soundPuzzlePanelShowUp);
    }

    private void PuzzlePanelHideUp() {
        animatorPuzzlePanel.SetTrigger("HideUp");
        PlaySound(soundPuzzlePanelHideUp);
    }

    private void SpikeDestroy() {
        animatorSpike.SetTrigger("Destroy");
        float waitingSeconds = 2.4f;
        StartCoroutine(SpikeDestruction(waitingSeconds));
    }

    IEnumerator SpikeDestruction(float waitingSeconds) {
        yield return new WaitForSeconds(waitingSeconds);
        animatorSpike.gameObject.SetActive(false);
    }

    public void PlaySound(AudioClip clip) {
        SoundManager.instance.audioSource.PlayOneShot(clip);
    }

}
