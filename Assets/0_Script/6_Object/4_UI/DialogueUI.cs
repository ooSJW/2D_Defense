using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class DialogueUI : MonoBehaviour, IPointerClickHandler // Data Field
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    private bool isTyping = false;
    private string[] messages =
    {
        "환영합니다! 이 게임은 2D 디펜스 게임입니다\n게임 진행 방법을 간략히 알려드리겠습니다.",
        "처음 스테이지에 입장 시 지급되는 재화로 포탑을 배치한 후\n오른쪽 하단의 '배치완료'버튼을 클릭해 게임을 시작할 수 있습니다.",
        "적 처리 보상으로 획득한 경험치와 재화를 사용해 로비에 위치한 상점에서 더 강력한 포탑을 구매할 수 있습니다!",
        "적은 항상 길 끝에 돌 바닥이 위치한 곳 에서 출발합니다\n적의 공격을 성공적으로 방어하세요!",
    };

    private int currentDialogueIndex = 0;
    private Coroutine typingCoroutine;
}

public partial class DialogueUI : MonoBehaviour, IPointerClickHandler // Main
{
    private void OnEnable()
    {
        typingCoroutine = StartCoroutine(StartDialogue());
    }
}
public partial class DialogueUI : MonoBehaviour, IPointerClickHandler // Coroutine
{

    public IEnumerator StartDialogue()
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in messages[currentDialogueIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        isTyping = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isTyping)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            dialogueText.text = messages[currentDialogueIndex];
            isTyping = false;
        }
        else
            OnNextDialogue();
    }

    public void OnNextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex < messages.Length)
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            typingCoroutine = StartCoroutine(StartDialogue());
        }
        else
        {
            PlayerPrefs.SetInt("IsFirstPlay", 0);
            PlayerPrefs.Save();
            gameObject.SetActive(false);
        }
    }

}