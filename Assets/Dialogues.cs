using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : MonoBehaviour
{
    //plants
    static public string PlantFlowerInst = "Drag Plant To Ground";
    static public string InsufficientResource = "Insufficient Resource";
    static public string RemovePlantResource = "Right Click To Remove";
    static public string RemovePlantConfirm = "Do you want to remove this plant?";

    //tree
    static public string UpgradeTreeInst = "Click To Upgrade";
    static public string SpawnTreeFlowerInst = "Click To Spawn Flowers";
    static public string AttactBeeInst = "Attract Bees And Wait For Fruit.";
    static public string StartTreeConfirm = "Do you want to start this tree?";


    //tutorial

    static public string Welcome = "Welcome To The Garden!";
    static public string PlantPond = "First drag the pond around your tree.";
    static public string FinishPlantPond = "Great! You can collect water from pond after a while.";

    static public string FinishCollectForWaterLily = "Good Job! Now you have enough resources, you can plant the new flower - Water Lily! Drag it in the pond!";
    static public string FinishPlantWaterLily = "Water Lily will attract some frogs for you, they will be useful soon! Now build more pond and keep collecting resources!";
    static public string FinishCollectForCrimson = "Good Job! Now you have enough resources, you can plant the new flower - Crimson Clover! Drag it around the tree!";
    static public string FinishPlantCrimson = "Crimson Clover will provide N for you, which is important for plant grow. You can use it grow new plants!";
    static public string FinishCollectForLavender = "It could take a while for plant to generate resource, remember to plant more pond and flowers to get resource faster! But now Let's plant Lavender!";
    static public string FinishPlantLavender = "Lavender will provide P. The main target for the garden is to upgrade your tree. Check what resource you need for upgrading tree";

    static public string FinishCollectForTree1 = "Now you can upgrade your tree.";
    static public string FinishUpgradeTree1 = "Awesome! Keep an eye on what resource you will need for next level. Keep upgrade the tree!";
    static public string FinishPlantMarigold = "Marigold will attract bees. It will be useful later when you have flowers on the tree.";

    static public string GetPestValue = "See Snail Value? Take care of it, the higher this value means more snails will shows up in your garden and eat your plants!";
    static public string GetBeeValue = "See Bee Value? The higher this value means more bees will shows up in your garden and help your pollinate your tree!";


    static public string SeeBird = "Sometimes the bird will fly to your place. click them and they will surprise you!";
    static public string SeePest = "Snail shows up! click them and the frog will go to eat them.";
    static public string SeeBee = "Bee shows up! You can drag from the flower to the bee to attact bees to your flowers.";


    static public string GetFlowerTree = "Your tree is at its max size, you should keep upgrade to get flowers on it, and plant Marigold to attact bees.";
    static public string FinishFirstTree = "Congratulations! You finish your first tree! Stay around and enjoy or move your tree to the garden.";
    // static public string FinishPlantMarigold = "Marigold will attract bees.";
}
