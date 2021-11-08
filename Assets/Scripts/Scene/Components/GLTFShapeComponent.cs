using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLTFShapeComponent : EntityComponent
{
    class SpecificGltfShapeJson
    {
        public SpecificGltfShapeJson(GLTFShapeComponent sc)
        {
            glbPath = sc.glbPath;
        }

        public string glbPath;
    }

    public override string SpecificJson => JsonUtility.ToJson(new SpecificGltfShapeJson(this));
    public override void ApplySpecificJson(string jsonString)
    {
        var json = JsonUtility.FromJson<SpecificGltfShapeJson>(jsonString);
        glbPath = json.glbPath;
    }

    public string glbPath;

    public override string ComponentName => "GLTFShape";
    public override Ts GetTypeScript()
    {
        return new Ts( InternalComponentSymbol, $"const {InternalComponentSymbol} = new GLTFShape(\"{glbPath}\")\n" +
                                                     $"{InternalComponentSymbol}.withCollisions = true\n" +
                                                     $"{InternalComponentSymbol}.isPointerBlocker = true\n" +
                                                     $"{InternalComponentSymbol}.visible = true\n");
    }

    public override void Start()
    {
        base.Start();
        componentRepresentation = Instantiate(ComponentRepresentationList.GltfShapeComponentInScene, entity.componentsParent.transform);
        componentRepresentation.GetComponent<GltfComponentRepresentation>().UpdateVisuals(this);
    }

    public override GameObject UiItemTemplate => ComponentRepresentationList.GltfShapeComponentUI;
}
