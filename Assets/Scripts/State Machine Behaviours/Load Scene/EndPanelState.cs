using UnityEngine;

public class EndPanelState : StateMachineBehaviour
{
    public int sceneIndex;
    public SceneLoader sceneLoader;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sceneLoader.SetSceneToLoad(sceneIndex);
        sceneLoader.StartLoad();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sceneLoader.FinishLoad();            
    }
}
