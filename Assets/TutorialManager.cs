using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    public bool skipTutorial = true;
    Dictionary<string, string> finishDialogToStartDialog = new Dictionary<string, string>() {
        {Dialogues.Welcome,Dialogues.PlantPond },

    };

    Dictionary<string, string> firstSeeSomethingToStartDialog = new Dictionary<string, string>() {
        {"bird",Dialogues.SeeBird },
        {"pest",Dialogues.SeePest },
        {"bee",Dialogues.SeeBee },

    };

    Dictionary<HelperPlantType, string> finishPlantToStartDialog = new Dictionary<HelperPlantType, string>() {
        {HelperPlantType.pond,Dialogues.FinishPlantPond },
        {HelperPlantType.waterlily,Dialogues.FinishPlantWaterLily },
        {HelperPlantType.crimson,Dialogues.FinishPlantCrimson },
        {HelperPlantType.lavender,Dialogues.FinishPlantLavender },
        {HelperPlantType.marigold,Dialogues.FinishPlantMarigold },
        {HelperPlantType.appleTree2,Dialogues.FinishUpgradeTree1 },
        {HelperPlantType.appleTree4,Dialogues.GetFlowerTree },



    };

    Dictionary<HelperPlantType, string> enoughResourceToStartDialog = new Dictionary<HelperPlantType, string>() {
        
        {HelperPlantType.waterlily,Dialogues.FinishCollectForWaterLily },
        {HelperPlantType.crimson,Dialogues.FinishCollectForCrimson },
        {HelperPlantType.lavender,Dialogues.FinishCollectForLavender },

    };

    Dictionary<PlantProperty, string> collectResourceToStartDialog = new Dictionary<PlantProperty, string>() {

        {PlantProperty.bee,Dialogues.GetBeeValue },
        {PlantProperty.pest,Dialogues.GetPestValue },

    };

    Dictionary<string, bool> hadDialog = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        //if (!skipTutorial)
        {

            TutorialPanel.Instance.Init(Dialogues.Welcome);
        }

    }
    public void finishPopup(string key)
    {
        if (finishDialogToStartDialog.ContainsKey(key))
        {
            TutorialPanel.Instance.Init(finishDialogToStartDialog[key]);
        }

        if(key == Dialogues.FinishUpgradeTree1)
        {
            BirdManager.Instance.autoResrouceStart = true;
        }
    }

    public void finishTree(HelperPlantType treeType)
    {
        var value = Dialogues.FinishFirstTree;
        if (!hadDialog.ContainsKey(value))
        {
            hadDialog[value] = true;
            TutorialPanel.Instance.Init(value);
        }
    }

    public void firstSeeSomething(string something)
    {
        if (firstSeeSomethingToStartDialog.ContainsKey(something))
        {
            var value = firstSeeSomethingToStartDialog[something];
            if (!hadDialog.ContainsKey(value))
            {
                hadDialog[value] = true;
                TutorialPanel.Instance.Init(value);
            }
        }
    }

    public void collectResource(PlantProperty something)
    {
        if (collectResourceToStartDialog.ContainsKey(something))
        {
            var value = collectResourceToStartDialog[something];
            if (!hadDialog.ContainsKey(value))
            {
                hadDialog[value] = true;
                TutorialPanel.Instance.Init(value);
            }
        }
    }

    public void finishPlant(HelperPlantType type)
    {
        if (finishPlantToStartDialog.ContainsKey(type))
        {
            var value = finishPlantToStartDialog[type];
            if (!hadDialog.ContainsKey(value))
            {
                hadDialog[value] = true;
                TutorialPanel.Instance.Init(value);
            }
        }
    }

    public void canPurchasePlant(HelperPlantType type)
    {
        if (TutorialPanel.Instance.canGeneratePanel() &&enoughResourceToStartDialog.ContainsKey(type))
        {
            var value = enoughResourceToStartDialog[type];
            if (!hadDialog.ContainsKey(value))
            {
                hadDialog[value] = true;
                TutorialPanel.Instance.Init(value);
            }
        }
    }

    public void resourceGetTo(PlantProperty key)
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
