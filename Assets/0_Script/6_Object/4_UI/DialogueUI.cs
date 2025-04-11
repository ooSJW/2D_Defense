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
        "ȯ���մϴ�! �� ������ 2D ���潺 �����Դϴ�\n���� ���� ����� ������ �˷��帮�ڽ��ϴ�.",
        "ó�� ���������� ���� �� ���޵Ǵ� ��ȭ�� ��ž�� ��ġ�� ��\n������ �ϴ��� '��ġ�Ϸ�'��ư�� Ŭ���� ������ ������ �� �ֽ��ϴ�.",
        "�� ó�� �������� ȹ���� ����ġ�� ��ȭ�� ����� �κ� ��ġ�� �������� �� ������ ��ž�� ������ �� �ֽ��ϴ�!",
        "���� �׻� �� ���� �� �ٴ��� ��ġ�� �� ���� ����մϴ�\n���� ������ ���������� ����ϼ���!",
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