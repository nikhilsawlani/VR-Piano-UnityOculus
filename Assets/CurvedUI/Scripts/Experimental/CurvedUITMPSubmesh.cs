using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if CURVEDUI_TMP || TMP_PRESENT
using TMPro;
#endif

namespace CurvedUI
{
    [ExecuteInEditMode]
    public class CurvedUITMPSubmesh : MonoBehaviour
    {
#if CURVEDUI_TMP || TMP_PRESENT

        //saved references
        VertexHelper vh;
        Mesh straightMesh;
        Mesh curvedMesh;
        CurvedUIVertexEffect crvdVE;
        TMP_SubMeshUI TMPsub;

        public void UpdateSubmesh(bool tesselate, bool curve)
        {
            //find required components
            if (TMPsub == null)
                TMPsub = gameObject.GetComponent<TMP_SubMeshUI>();

            if (TMPsub == null) return;

            if (crvdVE == null)
                crvdVE = gameObject.AddComponentIfMissing<CurvedUIVertexEffect>();


            //perform tesselatio and curving
            if (tesselate || straightMesh == null || vh == null || (!Application.isPlaying))
            {
                vh = new VertexHelper(TMPsub.mesh);

                //save straight mesh - it will be curved then every time the object moves on the canvas.
                straightMesh = new Mesh();
                vh.FillMesh(straightMesh);

                curve = true;
            }


            if (curve)
            {
                //Debug.Log("Submesh: Curve", this.gameObject);
                vh = new VertexHelper(straightMesh);
                crvdVE.ModifyMesh(vh);
                curvedMesh = new Mesh();
                vh.FillMesh(curvedMesh);
                crvdVE.CurvingRequired = true;
            }

            //upload mesh to TMP object's renderer
            TMPsub.canvasRenderer.SetMesh(curvedMesh);
        }

#endif
    }

}


