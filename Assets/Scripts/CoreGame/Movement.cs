using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCore.SystemControls;

namespace GameCore
{
    namespace SystemMovements
    {
        public class Movement 
        {
            public static void MoveForward(Rigidbody rb, float speed, Transform t)
            {
                rb.velocity = t.forward * speed * Controllers.AxisDeltaTime.y;
            }
            
        
            public static void JumpUp(Rigidbody rb, float jumpForce)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }


            public static float rotation = 0f;

            public static void RotateY(Transform t, float rotationSpeed)
            {
                rotation += Controllers.Axis.x * rotationSpeed;

                t.rotation = Quaternion.Euler(0f, rotation, 0f);
            }

            public static void MoveTopDown(Transform t, float speed)
            {
                t.Translate(Vector3.forward * Controllers.Axis.magnitude * Time.deltaTime * speed);

                if(Controllers.Axis != Vector2.zero)
                {
                    t.rotation = Quaternion.LookRotation(new Vector3(Controllers.Axis.x, 0f, Controllers.Axis.y));
                }
            }
            
        }
    }
}