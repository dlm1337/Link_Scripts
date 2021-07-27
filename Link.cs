using UnityEngine;

namespace Assets.Scripts
{
    
    public class Link : MonoBehaviour
    {
        [SerializeField] public Animator animator;
        float moveSpeed = 3.5f;
        private bool attackMode = false;
        [SerializeField] float padding = 1f;

        float xMin;
        float xMax;
        float yMin;
        float yMax;

        void Start()
        {
           
            SetUpMoveBoundaries();
        }
        void Update()
        {
            SwordSwing();
            Move();
            Facing();
            DoubleButtons();
        }

        public void SwordSwing()
        {
            if (Input.GetButtonDown("Fire1") && attackMode != true)
            {
                AnimTrigger("Slash");
            }
        }
        void AnimTrigger(string triggerName)
        {
            foreach (AnimatorControllerParameter p in animator.parameters)
                if (p.type == AnimatorControllerParameterType.Trigger)
                    animator.ResetTrigger(p.name);
            animator.SetTrigger(triggerName);
        }
        private void AttackMode()
        {
            attackMode = true;
            moveSpeed = 0f;
        }
        private void AttackModeFalse()
        {
            attackMode = false;
            moveSpeed = 3.5f;
        }

        private void Idle()
        {
            if (attackMode == false)
            {
                AnimTrigger("Idle");
                moveSpeed = 0f;
            }


        }

        public void Move()
        {
            var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
            var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

            var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
            var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

            transform.position = new Vector2(newXPos, newYPos);
        }

        public void SetUpMoveBoundaries()
        {
            Camera gameCamera = Camera.main;
            xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
            xMax = gameCamera.ViewportToWorldPoint(new Vector3(20, 0, 0)).x - padding;

            yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
            yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 20, 0)).y - padding;
        }

        public void Facing()
        {
            if (Input.GetKey("s") && attackMode == false)
            {
                animator.Play("Link Move Forward");
                moveSpeed = 3.5f;
            }
            else if (Input.GetKey("w") && attackMode == false)
            {
                animator.Play("Link Move Back");
                moveSpeed = 3.5f;
            }
            else if (Input.GetKey("a") && attackMode == false)
            {
                animator.Play("Link Move Left");
                moveSpeed = 3.5f;
            }
            else if (Input.GetKey("d") && attackMode == false)
            {
                animator.Play("Link Move Right");
                moveSpeed = 3.5f;
            }
            else if (!Input.anyKey)
            {
                Idle();
            }
        }
        public void DoubleButtons()
        {
            if (Input.GetKey("s") && attackMode == false)
            {
                if (Input.GetKey("w"))
                {
                    AnimTrigger("Idle");
                    moveSpeed = 0f;
                }
            }
            else if (Input.GetKey("w") && attackMode == false)
            {
                if (Input.GetKey("s"))
                {
                    AnimTrigger("Idle");
                    moveSpeed = 0f;
                }
            }
            else if (Input.GetKey("d") && attackMode == false)
            {
                if (Input.GetKey("a"))
                {
                    AnimTrigger("Idle");
                    moveSpeed = 0f;
                }
            }
            else if (Input.GetKey("a") && attackMode == false)
            {
                if (Input.GetKey("d"))
                {
                    AnimTrigger("Idle");
                    moveSpeed = 0f;
                }
            }
        }
    }

}
