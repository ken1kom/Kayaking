using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAnimationController : MonoBehaviour
{
    [SerializeField] private Animator paddleAnimator;
    [SerializeField] private Animator boatAnimator;
    [SerializeField] private PlayerMovement player;

    private Stroke currentStroke;
    private StrokeState currentStrokeState;

    private float normalStrokeAxis;
    private float wideStrokeAxis;
    private float sideStrokeAxis;
    private float backStrokeAxis;
    private float rudderAxis;
    private float edgeAxis;

    private bool isRuddering;

    private int StrokeStateHash;
    private int EdgeAxisHash;
    private int RudderAxisHash;

    void Start()
    {
        player = GetComponent<PlayerMovement>();
        StrokeStateHash = Animator.StringToHash("StrokeState");
        EdgeAxisHash = Animator.StringToHash("EdgeAxis");
        RudderAxisHash = Animator.StringToHash("RudderBlending");
    }

    // Update is called once per frame
    void Update()
    {
        currentStroke = player.CurrentStroke;
        currentStrokeState = player.CurrentStrokeState;
        normalStrokeAxis = player.NormalStrokeAxis;
        wideStrokeAxis = player.WideStrokeAxis;
        sideStrokeAxis = player.SideStrokeAxis;
        backStrokeAxis = player.BackStrokeAxis;
        rudderAxis = player.RudderAxis;
        edgeAxis = player.EdgeAxis;

        isRuddering = currentStrokeState == StrokeState.Ruddering ? true : false;

        paddleAnimator.SetInteger(StrokeStateHash, AssignStrokeInt());
        paddleAnimator.SetFloat(RudderAxisHash, AssignRudderFloat());

        boatAnimator.SetInteger(EdgeAxisHash, AssignEdgeInt());
        
    }

    private float AssignRudderFloat()
    {
        return Mathf.InverseLerp(-1, 1, rudderAxis);
    }

    private int AssignStrokeInt()
    {
        /*
         * 0 = Idle
         * 1 = NORMAL stroke on RIGHT
         * 2 = NORMAL stroke on LEFT
         * 3 = WIDE stroke on RIGHT
         * 4 = WIDE stroke on LEFT
         * 5 = SIDE stroke on RIGHT
         * 6 = SIDE stroke on LEFT
         * 7 = RUDDERING on RIGHT
         * 8 = RUDDERING on LEFT
         * 9 = BACK stroke
         */


        if (currentStrokeState == StrokeState.Withdrawing || (currentStrokeState == StrokeState.Ruddering && normalStrokeAxis == 0)) // If paddle is leaving water, go back to IDLE
        {
            return 0;
        }

        /*if (normalStrokeAxis != 0 && (wideStrokeAxis != 0 || sideStrokeAxis != 0 || backStrokeAxis != 0))
        {
            return 0;
        }
        else if (wideStrokeAxis != 0 && (normalStrokeAxis != 0 || sideStrokeAxis != 0 || backStrokeAxis != 0))
        {
            return 0;
        }
        else if (sideStrokeAxis != 0 && (normalStrokeAxis != 0 || wideStrokeAxis != 0 || backStrokeAxis != 0))
        {
            return 0;
        }
        else if (backStrokeAxis != 0 && (normalStrokeAxis != 0 || wideStrokeAxis != 0 || sideStrokeAxis != 0))
        {
            return 0;
        }*/

        if (currentStroke == Stroke.normal && normalStrokeAxis > 0.1 && currentStrokeState == StrokeState.MidStroke) // Play NORMAL Stroke Animation on RIGHT
        {
            return 1;
        }
        else if (currentStroke == Stroke.normal && normalStrokeAxis < -0.1 && currentStrokeState == StrokeState.MidStroke) // Play NORMAL Stroke Animation on LEFT
        {
            return 2;
        }
        else if (currentStroke == Stroke.wide && wideStrokeAxis > 0.1 && currentStrokeState == StrokeState.MidStroke) // Play WIDE Stroke Animation on RIGHT
        {
            return 3;
        }
        else if (currentStroke == Stroke.wide && wideStrokeAxis < -0.1 && currentStrokeState == StrokeState.MidStroke) // Play WIDE Stroke Animation on LEFT
        {
            return 4;
        }
        else if (currentStroke == Stroke.side && sideStrokeAxis > 0.1 && currentStrokeState == StrokeState.MidStroke) // Play SIDE Stroke Animation on RIGHT
        {
            return 5;
        }
        else if (currentStroke == Stroke.side && sideStrokeAxis < -0.1 && currentStrokeState == StrokeState.MidStroke) // Play SIDE Stroke Animation on RIGHT
        {
            return 6;
        }
        else if (currentStroke == Stroke.back && backStrokeAxis > 0.1 && currentStrokeState == StrokeState.MidStroke)
        {
            return 9;
        }
        else // IF ANYTHING ELSE, GO BACK TO IDLE
        {
            return 0;
        }
    }

    private int AssignEdgeInt()
    {
        if (edgeAxis > 0.1)
        {
            return 1;
        }
        else if (edgeAxis < -0.1)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
}
