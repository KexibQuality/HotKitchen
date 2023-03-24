using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
   private Animator _animator;
   private const string IS_WALKING = "IsWalking";

   [SerializeField] private Player player;
   private void Awake()
   {
      _animator = GetComponent<Animator>();
   }

   private void Update()
   { 
      _animator.SetBool(IS_WALKING,player.IsWalking());
   }
}
