using TheLegendsOfNart.Components.ColliderBased;
using TheLegendsOfNart.Components.GoBased;
using UnityEngine;

namespace TheLegendsOfNart.Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private bool _invertScale;
        [SerializeField] private float _speed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] private float _damageJumpSpd;
        [SerializeField] private int _damage;

        [Header("Checkers")]
        [SerializeField] protected LayerMask _groundLayer;
        [SerializeField] private ColliderCheck _groundCheck;
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;
        
        protected Vector2 Direction;
        protected Rigidbody2D Body;
        protected Animator Animator;
        //protected PlaySoundsComponent Sounds;
        protected bool IsGrounded;
        private bool _isJumping;
        private bool _isCanMove;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            UnlockMovement();
            Body = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            //Sounds = GetComponent<PlaySoundsComponent>();
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        protected virtual void Update()
        {
            IsGrounded = _groundCheck.IsTouchingLayer;
        }

        private void FixedUpdate()
        {
            var xVelocity = Direction.x * _speed;
            xVelocity *= _isCanMove ? 1 : 0;
            //var yVelocity = CalculateYVelosity();
            Body.velocity = new Vector2(xVelocity, Body.velocity.y);

            Animator.SetFloat(VerticalVelocityKey, Body.velocity.y);
            Animator.SetBool(IsRunningKey, Direction.x != 0);
            Animator.SetBool(IsGroundKey, IsGrounded);

            UpdateSpriteDirection(Direction);
        }

        public void LockMovement()
        {
            _isCanMove = false;
        }

        public void UnlockMovement()
        {
            _isCanMove = true;
        }

        //protected virtual float CalculateYVelosity()
        //{
        //    var yVelocity = Body.velocity.y;
        //    var isJumpPressing = Direction.y > 0;

        //    if (IsGrounded)
        //    {
        //        _isJumping = false;
        //    }            

        //    if (isJumpPressing)
        //    {
        //        _isJumping = true;
        //        var isFalling = Body.velocity.y <= 0.001f;
        //        yVelocity = isFalling ? CalculateJumpVelosity(yVelocity) : yVelocity;
        //    }
        //    else if (Body.velocity.y > 0 && _isJumping)
        //    {
        //        yVelocity *= 0.5f;
        //    }
        //    return yVelocity;
        //}

        protected virtual float DoJump(float yVelocity)
        {
            if (IsGrounded)
            {
                Body.AddForce(Vector2.up * yVelocity, ForceMode2D.Impulse);
                //Debug.Log("Jump");
                DoJumpVfx();
            }

            return yVelocity;
        }

        protected void DoJumpVfx()
        {
            _particles.Spawn("Jump");
            //Sounds.PlayClip("Jump");
        }

        public void UpdateSpriteDirection(Vector2 direction)
        {
            var scale = _invertScale ? -1 : 1; 
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(scale, 1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-scale, 1, 1);
            }
        }

        public virtual void TakeDamage()
        {
            _isJumping = false;
            Animator.SetTrigger(Hit);
            Body.velocity = new Vector2(Body.velocity.x, _damageJumpSpd);            
        }

        public virtual void Attack()
        {
            Animator.SetTrigger(AttackKey);
            //Sounds.PlayClip("Melee");
        }

        public virtual void MakeAttack()
        {
            Debug.Log("OnAttack");
            _attackRange.Check();
            _particles.Spawn("Attack");
            
        }
    }
}
