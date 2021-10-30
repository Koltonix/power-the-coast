using UnityEngine;

namespace power.utilities
{
    // Source for most of the logic: https://catlikecoding.com/unity/tutorials/procedural-grid/
    public class GenerateWaterPlane : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int size = new Vector2Int(25, 25);
        private float padding = 5.0f;

        [SerializeField]
        private MeshRenderer rend = null;
        [SerializeField]
        private MeshFilter filter = null;
        [SerializeField]
        private Mesh mesh = null;

        [SerializeField]
        private Material material = null;

        private void Start()
        {
            CreateMesh();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
                ChangeSize(size + Vector2Int.one);
        }

        public void ChangeSize(Vector2Int size)
        {
            this.size = size;
            
            Destroy(mesh);
            CreateMesh();
        }

        private void CreateMesh()
        {
            if (!rend)
                rend = this.gameObject.AddComponent<MeshRenderer>();

            if (!filter)
                filter = this.gameObject.AddComponent<MeshFilter>();

            mesh = new Mesh();
            filter.mesh = mesh;
            rend.material = material;

            mesh.vertices = ConstructVertices();
            mesh.triangles = ConstructTriangles();
            mesh.uv = ConstructUV();

            mesh.RecalculateNormals();
        }

        private Vector3[] ConstructVertices()
        {
            int i = 0;
            Vector3[] vertices = new Vector3[(size.x + 1) * (size.y + 1)];
            Vector3 totalSize = new Vector3((size.x + 1) * padding, 0.0f, (size.y + 1) * padding);

            for (int y = 0; y <= size.y; y++)
            {
                for (int x = 0; x <= size.x; x++)
                {
                    vertices[i] = (new Vector3(x, 0.0f, y) * padding) - totalSize * .5f;
                    i++;
                }
            }

            return vertices;
        }

        private int[] ConstructTriangles()
        {
            int[] triangles = new int[size.x * size.y * 6];
            for (int ti = 0, vi = 0, y = 0; y < size.y; y++, vi++) 
            {
                for (int x = 0; x < size.x; x++, ti += 6, vi++) 
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + size.x + 1;
                    triangles[ti + 5] = vi + size.x + 2;
                }
            }

            return triangles;
        }

        private Vector2[] ConstructUV()
        {
            Vector2[] uv = new Vector2[(size.x + 1) * (size.y + 1)];
            for (int i = 0, y = 0; y <= size.y; y++) 
            {
                for (int x = 0; x <= size.x; x++, i++) 
                {
                    	uv[i] = new Vector2((float)x / size.x, (float)y / size.y);
                }
            }

            return uv;
        }
    }
}