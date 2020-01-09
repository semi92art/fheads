using UnityEngine;
using System.Collections;

public class Markers : MonoBehaviour
{
    [Header("Tilt Edges:")]
    public Transform leftTiltEdgeTr;
    public Transform rightTiltEdgeTr;
    [Header("Top Bar Position:")]
	public Transform topBarTr;
    [Header("Middle of Stadium:")]
	public Transform midTr;
    [Header("Goalkeeper State Position:")]
	public Transform gkStateTr;
    [Header("Defend Position:")]
	public Transform defPosTr;
    [Header("Stadium Edge Positions:")]
    public Transform leftDownBonusTr;
    public Transform rightUpBonusTr;
    [Header("Start positions:")]
    public Transform plStartTr;
    public Transform enStartTr;
    [Header("Practice Markers:")]
    public Transform plGatesPracticeTr;
    public Transform enGatesPracticeTr;
}
