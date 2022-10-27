using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KissaAnimationEvents : MonoBehaviour
{
	[SerializeField] SC_RigidbodyWalker KissaMovement;
	void StepDust()
	{
		KissaMovement.StepDustEmit(); //What are you doing step-dust?
	}

}
