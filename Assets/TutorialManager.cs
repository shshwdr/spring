using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    public bool skipTutorial = true;
    Dictionary<string, string> finishDialogToStartDialog = new Dictionary<string, string>() {
        {Dialogues.Welcome,Dialogues.PlantPond },

    };

    Dictionary<HelperPlantType, string> finishPlantToStartDialog = new Dictionary<HelperPlantType, string>() {
        {HelperPlantType.pond,Dialogues.FinishPlantPond },
        {HelperPlantType.waterlily,Dialogues.FinishPlantWaterLily },
        {HelperPlantType.crimson,Dialogues.FinishPlantCrimson },
        {HelperPlantType.lavender,Dialogues.FinishPlantLavender },
        {HelperPlantType.marigold,Dialogues.FinishPlantMarigold },
        {HelperPlantType.appleTree2,Dialogues.FinishUpgradeTree1 },
        


    };

    Dictionary<HelperPlantType, string> enoughResourceToStartDialog = new Dictionary<HelperPlantType, string>() {
        
        {HelperPlantType.waterlily,Dialogues.FinishCollectForWaterLily },
        {HelperPlantType.crimson,Dialogues.FinishCollectForCrimson },
        {HelperPlantType.lavender,Dialogues.FinishCollectForLavender },

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
