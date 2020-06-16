using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private MessageManager messageManager;
    [SerializeField]
    private GameObject cena2Trigger;
    private int tutorialLevel = 1;
    private bool corroutine = false;
    private bool completed = false;
    private bool canStartTuto = false;
    private string checkpoint;
    private GameObject freezeAim;
    private DialogueManager dialogueManager;
    private PostProcessVolume postProcessing;
    private AutoExposure autoExposure;

    private void Start()
    {
        freezeAim = GameObject.FindGameObjectWithTag("freezeAim");
        freezeAim.SetActive(false);

        PowersManager.dashCanvas.alpha = 0;
        PowersManager.strengthCanvas.alpha = 0;
        PowersManager.doubleJumpCanvas.alpha = 0;
        PowersManager.freezeCanvas.alpha = 0;

        dialogueManager = FindObjectOfType<DialogueManager>();

        postProcessing = GameObject.Find("Main PostProcessingVolume").GetComponent<PostProcessVolume>();
        postProcessing.profile.TryGetSettings(out autoExposure);
    }

    void Update()
    {
        if (dialogueManager.endedScenes.Contains("Cena1"))
        {
            cena2Trigger.SetActive(true);
        }
        else
        {
            autoExposure.minLuminance.value = 9f;
        }

        if (PlayerEntity.getIsOnDialogue())
        {
            return;
        }

        if (dialogueManager.endedScenes.Contains("Cena2"))
        {
            canStartTuto = true;
        }

        #region Dialogue Routine

        if (!corroutine && canStartTuto)
        {
            switch (tutorialLevel)
            {
                case 1:
                    messageManager.DisplayMessage("Acesse o menu/pause apertando ESC");
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        StartCoroutine(changeLevel());
                    }
                    break;

                case 2:
                    if (!completed)
                    {
                        messageManager.DisplayMessage("Siga o caminho!", "Ande com as teclas W, A, S, D.", 0.1f);
                    }
                    else
                    {
                        messageManager.DisplayMessage("Ande com as teclas W, A, S, D.");
                    }
                    if (PlayerEntity.checkInputHorizontal() != 0 || PlayerEntity.checkInputVertical() != 0)
                    {
                        StartCoroutine(changeLevel());
                    }
                    break;

                case 3:
                    messageManager.DisplayMessage("Use o SHIFT ESQUERDO para dar um dash na direção da câmera", "Você pode movimentar a câmera com o mouse!", 0.1f);
                    if (PlayerEntity.getKeyLeftShift())
                    {
                        PowersManager.dashCanvas.alpha = 0.3f;
                        StartCoroutine(changeLevel());
                    }
                    break;

                case 4:
                    messageManager.DisplayMessage("Pule usando ESPAÇO.");
                    if (PlayerEntity.getButtonJump())
                    {
                        StartCoroutine(changeLevel());
                    }
                    break;

                case 5:
                    messageManager.DisplayMessage("O dash pode ser muito útil para prolongar pulos ou simplesmente chegar mais rápido onde você quer.", "Ele possui um tempo de recarga de 2 segundos.", 0.1f);
                    if (!PlayerEntity.getGrounded() && PlayerEntity.getKeyLeftShift())
                    {
                        StartCoroutine(changeLevel());
                    }
                    break;


                case 6:
                    if (checkpoint == "doubleJump" || completed)
                    {
                        messageManager.DisplayMessage("Aperte espaço duas vezes para pulo duplo.");
                        if (!PlayerEntity.getGrounded() && PlayerEntity.getButtonJump())
                        {
                            PowersManager.doubleJumpCanvas.alpha = 0.3f;
                            StartCoroutine(changeLevel());
                        }
                    }
                    break;

                case 7:
                    if (checkpoint == "strengthAndFreeze" || completed)
                    {
                        messageManager.DisplayMessage("Você pode pegar certas caixas apertando E próximo delas.", "Também pode se mover enquanto segura", 0.1f);
                        if (PlayerEntity.getLocked())
                        {
                            PowersManager.strengthCanvas.alpha = 0.3f;
                            StartCoroutine(changeLevel());
                        }
                    }
                    break;

                case 8:
                    messageManager.DisplayMessage("Você pode soltá-las apertando E novamente.", "Ou jogá-las longe apertando Q", 0.1f);
                    if (PlayerEntity.getKeyQ())
                    {
                        StartCoroutine(changeLevel());
                    }
                    break;

                case 9:
                    freezeAim.SetActive(true);
                    messageManager.DisplayMessage("Você também pode congelar certas caixas, em uma determinada distância, apertando Q.", "Mire com o indicador no centro da tela", 0.1f);
                    if (PlayerEntity.getKeyQ() && !PlayerEntity.getLocked())
                    {
                        PowersManager.freezeCanvas.alpha = 0.3f;
                        StartCoroutine(changeLevel());
                    }
                    break;

                case 10:
                    messageManager.DisplayMessage("Descongele apertando Q novamente");
                    if (PlayerEntity.getKeyQ() && !PlayerEntity.getLocked())
                    {
                        StartCoroutine(changeLevel());
                    }
                    break;

                case 11:
                    messageManager.DisplayMessage("Isso é tudo, pessoal! Use o que aprendeu para chegar ao outro lado!");
                    StartCoroutine(dismissLastMessage());
                    if (/*Input.GetKeyDown(KeyCode.R)*/ false)
                    {
                        StartCoroutine(changeLevel());
                    }
                    break;

                default:
                    //completed = true;
                    //tutorialLevel = 1;
                    break;
            }
        }

        #endregion
    }

    IEnumerator changeLevel()
    {
        corroutine = true;
        messageManager.DismissMessage();
        yield return new WaitForSeconds(0.6f);
        tutorialLevel++;
        corroutine = false;
    }

    IEnumerator dismissLastMessage()
    {
        yield return new WaitForSeconds(5f);
        messageManager.DismissMessage();
        tutorialLevel++;
    }

    public void setCheckpoint(string checkpoint)
    {
        this.checkpoint = checkpoint;
    }
}
