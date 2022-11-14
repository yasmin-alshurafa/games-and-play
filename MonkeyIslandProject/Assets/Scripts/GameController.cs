using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
public class GameController : MonoBehaviour
{
    public GameObject question_text_go;
    public GameObject option1_text_go;
    public GameObject option2_text_go;
    public GameObject option1_indicator_go;
    public GameObject option2_indicator_go;
    public int current_question_index;
    public QuestionAnswer[] question_answer_array;
    public int current_index;
    public int player_scores;
    public int devil_scores;
    public bool follow_enemy;

    public GameObject audio_controller;
    #region Unity Methods
    private void Awake()
    {
        audio_controller = GameObject.FindGameObjectWithTag("AudioController");
        player_scores = 0;
        devil_scores = 0;
        current_index = 0;
        question_answer_array = new QuestionAnswer[]
        {
            new QuestionAnswer{question="This is the END for you, you gutter-crawling cur!",option1="First you'd better stop waving it like a feather-duster.",option2="And I've got a little TIP for you, get the POINT?",answer=false},
            new QuestionAnswer{question="Soon you'll be wearing my sword like a shish kebab!",option1="First you'd better stop waving it like a feather-duster.",option2="Even BEFORE they smell your breath?",answer=true},
            new QuestionAnswer{question="My handkerchief will wipe up your blood!",option1="I hope now you've learned to stop picking your nose.",option2="So you got that job as janitor, after all.",answer=false},
            new QuestionAnswer{question="People fall at my feet when they see me coming.",option1="Even BEFORE they smell your breath?",option2="Too bad no one's ever heard of YOU at all.",answer=true},
            new QuestionAnswer{question="I once owned a dog that was smarter then you.",option1="You run THAT fast?",option2="He must have taught you everything you know.",answer=false},
            new QuestionAnswer{question="You make me want to puke.",option1="You make me think somebody already did.",option2="I'd be in real trouble if you ever used them.",answer=true},
            new QuestionAnswer{question="Nobody's ever drawn blood from me and nobody ever will.",option1="Even BEFORE they smell your breath?",option2="You run THAT fast?",answer=false},
            new QuestionAnswer{question="You fight like a dairy farmer.",option1="How appropriate. You fight like a cow.",option2="I wanted to make sure you'd feel comfortable with me.",answer=true},
            new QuestionAnswer{question="I got this scar on my face during a mighty struggle!",option1="Yes, there are. You just never learned them.",option2="I hope now you've learned to stop picking your nose.",answer=false},
            new QuestionAnswer{question="Have you stopped wearing diapers yet?",option1="Why, did you want to borrow one?",option2="You make me think somebody already did.",answer=true},
            new QuestionAnswer{question="I've heard you were a contemptible sneak.",option1="He must have taught you everything you know.",option2="Too bad no one's ever heard of YOU at all.",answer=false},
            new QuestionAnswer{question="You're no match for my brains, you poor fool.",option1="I'd be in real trouble if you ever used them.",option2="How appropriate. You fight like a cow.",answer=true},
            new QuestionAnswer{question="You have the manners of a beggar.",option1="He must have taught you everything you know.",option2="I wanted to make sure you'd feel comfortable with me.",answer=false},
            new QuestionAnswer{question="I'm not going to take your insolence sitting down!",option1="Your hemorrhoids are flaring up again, eh?",option2="How appropriate. You fight like a cow.",answer=true},
            new QuestionAnswer{question="There are no words for how disgusting you are.",option1="Your hemorrhoids are flaring up again, eh?",option2="Yes, there are. You just never learned them.",answer=false},
            new QuestionAnswer{question="I've spoken with apes more polite then you.",option1="I'm glad to hear you attended your family reunion.",option2="I'd be in real trouble if you ever used them.",answer=true},
        };
    }
    private void Start()
    {
        follow_enemy = true;
        GetQuestion();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
    #endregion
    #region UI Callback Methods
    public void OnResponseClicked(bool resp)
    {
        option1_indicator_go.GetComponent<Button>().enabled = false;
        option2_indicator_go.GetComponent<Button>().enabled = false;
        EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.SetActive(true);
       
        StartCoroutine(WaitForSomeTime(resp));
    }
    #endregion
    #region Private Methods
    private void GetQuestion()
    {
        if (current_index < question_answer_array.Length - 1)
        {
            question_text_go.GetComponent<TextMeshProUGUI>().text = question_answer_array[current_index].question;
            option1_text_go.GetComponent<TextMeshProUGUI>().text = question_answer_array[current_index].option1;
            option2_text_go.GetComponent<TextMeshProUGUI>().text = question_answer_array[current_index].option2;
        }
        else
        {
            if (player_scores > devil_scores)
            {
                PlayerPrefs.SetString("WinnerName", "Player Won");
            }
            else if (devil_scores > player_scores)
            {
                PlayerPrefs.SetString("WinnerName", "Computer Won");
            }
            else if(player_scores==devil_scores)
            {
                PlayerPrefs.SetString("WinnerName","It's A Tie");
            }
            SceneManager.LoadScene("GameOver");
        }
    }
    #endregion
    private IEnumerator WaitForSomeTime(bool resp)
    {
        yield return new WaitForSeconds(1.5f);
        if (question_answer_array[current_index].answer == resp)
        {
            

            player_scores++;
        }
        else
        {
            

            devil_scores++;
        }
        yield return new WaitForSeconds(2.0f);
        option1_indicator_go.transform.GetChild(0).gameObject.SetActive(false);
        option2_indicator_go.transform.GetChild(0).gameObject.SetActive(false);
        current_index++;
        GetQuestion();
        option1_indicator_go.GetComponent<Button>().enabled = true;
        option2_indicator_go.GetComponent<Button>().enabled = true;

    }

}
