#pragma strict
var lineColor : Color;

var backgroundColor : Color;

var ZWrite = true;

var AWrite = true;

var blend = true;

 

private var lines : Vector3[];

private var linesArray : Array;

private var lineMaterial : Material;

private var meshRenderer : SkinnedMeshRenderer;

public var meshMat: Material;
public var lineMat : Material;
 
public function disableYourself() {
	this.enabled = false;
}


function Start ()

{

    meshRenderer = GetComponent(SkinnedMeshRenderer);

    //if(!meshRenderer) meshRenderer = gameObject.AddComponent(MeshRenderer);

    //meshRenderer.material = new Material("Shader \"Lines/Background\" { Properties { _Color (\"Main Color\", Color) = (1,1,1,1) } SubShader { Pass {" + (ZWrite ? " ZWrite on " : " ZWrite off ") + (blend ? " Blend SrcAlpha OneMinusSrcAlpha" : " ") + (AWrite ? " Colormask RGBA " : " ") + "Lighting Off Offset 1, 1 Color[_Color] }}}");

    meshRenderer.material = meshMat;
 	lineMaterial = lineMat;
    //lineMaterial = new Material("Shader \"Lines/Colored Blended\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha ZWrite Off Cull Front Fog { Mode Off } } } }");

    

    /*lineMaterial.hideFlags = HideFlags.HideAndDontSave;

    lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;*/

   

    linesArray = new Array();

    var filter : SkinnedMeshRenderer = GetComponent(SkinnedMeshRenderer);

    var mesh = filter.sharedMesh;

    var vertices = mesh.vertices;

    var triangles = mesh.triangles;

    

    for (var i = 0; i < triangles.length / 3; i++)

    {

        linesArray.Add(vertices[triangles[i * 3]]);

        linesArray.Add(vertices[triangles[i * 3 + 1]]);

        linesArray.Add(vertices[triangles[i * 3 + 2]]);

    }

    

    lines = linesArray.ToBuiltin(Vector3);

}

 

 

function OnRenderObject()

{   


    lineMaterial.SetPass(0);

   

    GL.PushMatrix();

    GL.MultMatrix(transform.localToWorldMatrix);

    GL.Begin(GL.LINES);

    GL.Color(lineColor);

   

    for (var i = 0; i < lines.length / 3; i++)

    {

        GL.Vertex(lines[i * 3]);

        GL.Vertex(lines[i * 3 + 1]);

        

        GL.Vertex(lines[i * 3 + 1]);

        GL.Vertex(lines[i * 3 + 2]);

        

        GL.Vertex(lines[i * 3 + 2]);

        GL.Vertex(lines[i * 3]);

    }

         

    GL.End();

    GL.PopMatrix();

}