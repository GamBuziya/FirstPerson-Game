using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.DialogSystem
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private GameObject _dialogueParent;
        [SerializeField] private TMP_Text _dialogueText;
        [SerializeField] private Button _option1Button;
        [SerializeField] private Button _option2Button;

        [SerializeField] private float _typingSpeed = 0.05f;
        [SerializeField] private float _turnSpeed = 2f;

        private List<DialogString> _dialogList;
        
        private Transform _playerCamera;


        private CharacterController _characterController;

        private int _currentDialogIndex = 0;

        public bool IsDialog = false;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _dialogueParent.SetActive(false);
            _playerCamera = Camera.main.transform;
        }
        
        public void DialogStart(List<DialogString> textToPrint, Transform NPC)
        {
            IsDialog = true;
            _dialogueParent.SetActive(true);
            _characterController.enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            StartCoroutine(TurnCameraTowardNPC(NPC));

            _dialogList = textToPrint;
            _currentDialogIndex = 0;

            DisableButtons();

            StartCoroutine(PrintDialogue());
        }

        private void DisableButtons()
        {
            _option1Button.interactable = false;
            _option2Button.interactable = false;

            _option1Button.GetComponentInChildren<TMP_Text>().text = "No Option";
            _option2Button.GetComponentInChildren<TMP_Text>().text = "No Option";
        }

        private IEnumerator TurnCameraTowardNPC(Transform NPC)
        {
            Quaternion startRotation = _playerCamera.rotation;
            Vector3 targetDirection = NPC.position - _playerCamera.position;
            targetDirection.y += 1.0f;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            float elapsedTime = 0;

            while (elapsedTime < 1f)
            {
                _playerCamera.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
                elapsedTime += Time.deltaTime * _turnSpeed;
                yield return null;
            }

            _playerCamera.rotation = targetRotation;
        }


        private bool _optionSelected = false;
        private IEnumerator PrintDialogue()
        {
            while (_currentDialogIndex < _dialogList.Count)
            {
                DialogString line = _dialogList[_currentDialogIndex];
                

                if (line.IsQuestion)
                {
                    yield return StartCoroutine(TypeText(line));
                    _option1Button.interactable = true;
                    _option2Button.interactable = true;

                    _option1Button.GetComponentInChildren<TMP_Text>().text = line.AnswerOption1;
                    _option2Button.GetComponentInChildren<TMP_Text>().text = line.AnswerOption2;
                    
                    _option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1Index));
                    _option2Button.onClick.AddListener(() => HandleOptionSelected(line.option2Index));

                    yield return new WaitUntil(() => _optionSelected);
                }
                else
                {
                    yield return StartCoroutine(TypeText(line));
                }
                
                _optionSelected = false;
            }
            
            DialogueEnded();
        }

        private void HandleOptionSelected(int index)
        {
            _optionSelected = true;
            DisableButtons();

            _currentDialogIndex = index;
        }

        private IEnumerator TypeText(DialogString text)
        {
            _dialogueText.text = "";
            foreach (var letter in text.Text.ToCharArray())
            {
                _dialogueText.text += letter;
                yield return new WaitForSeconds(_typingSpeed);
            }

            if (!_dialogList[_currentDialogIndex].IsQuestion)
            {
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            }
            
            
            if (_dialogList[_currentDialogIndex].IsEnd)
                DialogueEnded();
            
            _currentDialogIndex++;
        }

        private void DialogueEnded()
        {
            StopAllCoroutines();
            _dialogueText.text = "";
            _dialogueParent.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            _characterController.enabled = true;

            IsDialog = false;
            
            //GameEventManager.Instance.InputEvents.SubmitPressed();
        }
    }
}