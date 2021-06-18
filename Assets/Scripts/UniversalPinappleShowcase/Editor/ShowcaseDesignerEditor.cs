using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[CustomEditor(typeof(ShowcaseDesigner)), CanEditMultipleObjects]
public class ShowcaseDesignerEditor : Editor
{




    public override void OnInspectorGUI()
    {
        ShowcaseDesigner targetObject = (ShowcaseDesigner)target;

        base.OnInspectorGUI();
        if (GUILayout.Button("Generate File"))
        {

            WriteFile(targetObject);






 
        }


        serializedObject.ApplyModifiedProperties();
    }



    public void WriteFile(ShowcaseDesigner target)
    {


        ShowcaseSaveData save = new ShowcaseSaveData();

        //compile list data 
        for (int i = 0; i < target.ShowcaseItems.Count; i++)
        {
            save.showcaseItems.Add(new ShowcaseItem(target.ShowcaseItems[i]));
        }



        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;


        // write new file
         
        if (!File.Exists(Application.persistentDataPath + "/ShowcaseData" + ".PSS"))
        {
            //creates save file if cant be found;

            file = File.Create(Application.persistentDataPath + "/ShowcaseData" + ".PSS");

        }

        else

        // write over old file
        {

            file = File.Open(Application.persistentDataPath + "/ShowcaseData" + ".PSS", FileMode.Open);


        }
       
        Debug.Log("Saved showcase file");


        bf.Serialize(file, save);
        file.Close();




    }
}

