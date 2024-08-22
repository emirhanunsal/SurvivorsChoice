using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    public List<CardData> cardDataList;
    [SerializeField] private Image healthImage;
    [SerializeField] private Image hungerImage;
    [SerializeField] private Image moodImage;
    [SerializeField] private Image energyImage;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text leftOptionText;
    [SerializeField] private TMP_Text rightOptionText;

    private List<CardData> shuffledCardDataList;
    private int currentCardIndex = 0;

    private void Start()
    {
            // Kart verilerini başlat
            cardDataList = new List<CardData>
            {
                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 0f, energyRight = 10f,
                    healthLeft = 0f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a stream of water. It looks clean but you're not sure if it's safe.",
                    rightAction = "Drink the water", leftAction = "Ignore it",
                    rightConclusion = "You drink the water and become ill from the contaminants.",
                    leftConclusion = "You avoid the water, remaining cautious but thirsty."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = -20f, energyRight = 0f,
                    healthLeft = 0f, hungerLeft = 0f, moodLeft = 0f, energyLeft = -10f,
                    description = "A snake slithers across your path. It seems agitated.",
                    rightAction = "Run away", leftAction = "Attempt to scare it",
                    rightConclusion = "You escape the snake, but the experience leaves you shaken and fatigued.",
                    leftConclusion = "You scare off the snake but sustain a minor injury from its bite."
                },

                new CardData
                {
                    healthRight = 10f, hungerRight = 0f, moodRight = 0f, energyRight = -10f,
                    healthLeft = 0f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You spot a berry bush with ripe berries.",
                    rightAction = "Pick and eat some berries", leftAction = "Move on",
                    rightConclusion =
                        "The berries provide a boost to your health but leave you feeling slightly drained.",
                    leftConclusion = "You move on, conserving your energy but remaining hungry."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 0f, hungerLeft = 0f, moodLeft = -10f, energyLeft = 10f,
                    description =
                        "A stranger offers you a meal. It smells delicious but you don't know the stranger's intentions.",
                    rightAction = "Accept the meal", leftAction = "Politely decline",
                    rightConclusion =
                        "The meal fills you up and improves your mood, but you remain wary of the stranger.",
                    leftConclusion = "You decline the meal, keeping your guard up but feeling hungry."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 0f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 0f, moodLeft = 10f, energyLeft = 0f,
                    description = "You stumble upon a hidden cave.",
                    rightAction = "Explore the cave", leftAction = "Avoid the cave",
                    rightConclusion = "Exploring the cave reveals useful supplies but leaves you exhausted.",
                    leftConclusion = "You avoid the cave, missing out on potential resources but staying safe."
                },

                new CardData
                {
                    healthRight = -20f, hungerRight = 10f, moodRight = 0f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 10f, energyLeft = 0f,
                    description = "A storm is approaching. You're caught in the open.",
                    rightAction = "Find shelter quickly", leftAction = "Stay and endure the storm",
                    rightConclusion = "You find shelter and stay safe but are left feeling drained and hungry.",
                    leftConclusion =
                        "You endure the storm, which impacts your health and mood but strengthens your resilience."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 0f, moodLeft = -10f, energyLeft = 0f,
                    description = "You hear a distant howl in the night.",
                    rightAction = "Build a fire for safety", leftAction = "Ignore it and sleep",
                    rightConclusion = "Building a fire keeps you safe and boosts your mood but consumes your energy.",
                    leftConclusion = "Ignoring the howl, you manage to sleep but remain uneasy and cold."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = -10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = 0f, moodLeft = 10f, energyLeft = -10f,
                    description = "You find a mysterious potion lying on the ground.",
                    rightAction = "Drink the potion", leftAction = "Leave it alone",
                    rightConclusion = "Drinking the potion has unforeseen effects that damage your health.",
                    leftConclusion = "Leaving the potion alone avoids potential risks but leaves you curious."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 0f, moodRight = 0f, energyRight = -10f,
                    healthLeft = 0f, hungerLeft = 0f, moodLeft = -10f, energyLeft = 10f,
                    description = "A wild animal is blocking your path.",
                    rightAction = "Try to sneak around it", leftAction = "Attempt to confront it",
                    rightConclusion = "Sneaking around avoids confrontation but costs you energy.",
                    leftConclusion = "Confronting the animal is risky but boosts your confidence and mood."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 10f, energyLeft = 0f,
                    description = "You come across a hunter's trap.",
                    rightAction = "Carefully disarm it", leftAction = "Avoid the trap",
                    rightConclusion = "Disarming the trap yields useful items but leaves you vulnerable.",
                    leftConclusion = "Avoiding the trap keeps you safe but means missing out on potential rewards."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 0f, moodRight = 0f, energyRight = 10f,
                    healthLeft = 0f, hungerLeft = 0f, moodLeft = -10f, energyLeft = 0f,
                    description = "You find a hidden stash of supplies.",
                    rightAction = "Take the supplies", leftAction = "Leave them",
                    rightConclusion = "Taking the supplies boosts your resources but slightly decreases your energy.",
                    leftConclusion = "Leaving the supplies keeps your energy intact but you miss out on useful items."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 0f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = -10f, energyLeft = 0f,
                    description = "A rival survivor challenges you to a duel.",
                    rightAction = "Accept the challenge", leftAction = "Refuse and walk away",
                    rightConclusion = "Accepting the challenge may result in injury but improves your standing.",
                    leftConclusion = "Refusing avoids potential harm but leaves you feeling defeated."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = -10f, hungerLeft = 0f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden spring.",
                    rightAction = "Drink the water", leftAction = "Mark the location and move on",
                    rightConclusion = "Drinking from the spring replenishes your thirst but doesn't solve other needs.",
                    leftConclusion = "Marking the location preserves your energy but doesn't quench your thirst."
                },
                new CardData
                {
                    healthRight = 0f, hungerRight = 10f, moodRight = 10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find an abandoned cabin.",
                    rightAction = "Search the cabin",
                    rightConclusion = "Inside, you find some useful supplies but are exhausted from the search.",
                    leftAction = "Continue exploring the forest",
                    leftConclusion = "You stay on your path and find a new source of food."
                },

                new CardData
                {
                    healthRight = 10f, hungerRight = -10f, moodRight = -10f, energyRight = 0f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You encounter a friendly traveler offering food.",
                    rightAction = "Accept and share food",
                    rightConclusion = "You gain some health and mood, but lose some energy.",
                    leftAction = "Politely refuse and move on",
                    leftConclusion = "You avoid any potential risk but remain hungry and tired."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = -10f, energyRight = 10f,
                    healthLeft = 0f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a strange, glowing stone.",
                    rightAction = "Investigate the stone",
                    rightConclusion = "The stone has strange properties; you lose health but gain knowledge.",
                    leftAction = "Ignore it and continue",
                    leftConclusion = "You avoid potential danger but miss out on possible benefits."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = -10f, hungerLeft = 0f, moodLeft = -10f, energyLeft = 10f,
                    description = "A dense fog rolls in, making it hard to see.",
                    rightAction = "Find shelter until it clears",
                    rightConclusion = "You stay safe but lose time and energy.",
                    leftAction = "Press on through the fog",
                    leftConclusion = "You continue but risk running into hazards."
                },

                new CardData
                {
                    healthRight = 10f, hungerRight = 0f, moodRight = -10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You stumble upon a patch of medicinal herbs.",
                    rightAction = "Collect and use them",
                    rightConclusion = "You restore some health but lose energy.",
                    leftAction = "Keep moving",
                    leftConclusion = "You avoid potential healing but continue with your journey."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You hear a rustling sound nearby.",
                    rightAction = "Investigate the noise",
                    rightConclusion = "You discover a hidden stash, but it costs you some health.",
                    leftAction = "Avoid it and move on",
                    leftConclusion = "You avoid potential danger but miss out on a possible benefit."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 10f, moodRight = 0f, energyRight = 0f,
                    healthLeft = -10f, hungerLeft = 0f, moodLeft = 10f, energyLeft = 10f,
                    description = "You come across a peaceful meadow.",
                    rightAction = "Rest and enjoy the view",
                    rightConclusion = "You regain energy and mood but lose time.",
                    leftAction = "Quickly pass through",
                    leftConclusion = "You maintain your pace but miss out on relaxation."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 0f, moodLeft = -10f, energyLeft = 0f,
                    description = "You encounter a family of wild deer.",
                    rightAction = "Observe quietly",
                    rightConclusion = "You enjoy a peaceful moment but gain nothing else.",
                    leftAction = "Attempt to approach them",
                    leftConclusion = "You get closer to the deer but risk startling them and losing mood."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = -10f, energyLeft = 0f,
                    description = "A sudden downpour starts.",
                    rightAction = "Find cover quickly",
                    rightConclusion = "You stay dry but expend some energy searching for cover.",
                    leftAction = "Continue despite the rain",
                    leftConclusion = "You endure the rain but become wet and tired."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You see a faint light in the distance.",
                    rightAction = "Investigate the light",
                    rightConclusion = "You discover a small campfire and gain warmth but lose energy.",
                    leftAction = "Avoid the light",
                    leftConclusion = "You continue your journey without disturbance but remain cold."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 0f, moodRight = 0f, energyRight = -10f,
                    healthLeft = 0f, hungerLeft = 0f, moodLeft = -10f, energyLeft = 10f,
                    description = "A loud roar echoes through the forest.",
                    rightAction = "Seek safety",
                    rightConclusion = "You find shelter but expend energy in your rush.",
                    leftAction = "Find the source of the roar",
                    leftConclusion = "You uncover the source of the roar but risk encountering danger."
                },

                new CardData
                {
                    healthRight = 10f, hungerRight = 0f, moodRight = -10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover an old, abandoned campsite.",
                    rightAction = "Search for useful items",
                    rightConclusion = "You find some supplies but lose energy searching.",
                    leftAction = "Move on",
                    leftConclusion = "You continue without interruption but miss out on potential items."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 10f, moodRight = 10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a large, ripe fruit hanging from a tree.",
                    rightAction = "Pick and eat it",
                    rightConclusion = "You gain hunger and mood but lose some energy.",
                    leftAction = "Leave it and continue",
                    leftConclusion = "You avoid potential risk but remain hungry and low on energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 0f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 10f, energyLeft = -10f,
                    description = "A wild boar charges at you.",
                    rightAction = "Dodge and escape",
                    rightConclusion = "You evade the boar but suffer a minor injury.",
                    leftAction = "Fight it off",
                    leftConclusion = "You successfully defend yourself but sustain some injuries."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = 0f, moodLeft = -10f, energyLeft = 10f,
                    description = "You find a mysterious, glowing mushroom.",
                    rightAction = "Eat the mushroom",
                    rightConclusion = "The mushroom has strange effects; you gain mood but lose health.",
                    leftAction = "Leave it alone",
                    leftConclusion = "You avoid potential risks but miss out on possible benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 10f, moodRight = 0f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = 0f, moodLeft = -10f, energyLeft = 0f,
                    description = "You find an old book partially buried in the ground.",
                    rightAction = "Read the book",
                    rightConclusion = "You gain knowledge but lose time and energy.",
                    leftAction = "Ignore it and continue",
                    leftConclusion = "You avoid potential distractions but miss out on possible insights."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = -10f, energyRight = 0f,
                    healthLeft = 0f, hungerLeft = -10f, moodLeft = 10f, energyLeft = 10f,
                    description = "You hear footsteps following you.",
                    rightAction = "Turn and confront them",
                    rightConclusion = "You confront the follower and gain some insights but risk further danger.",
                    leftAction = "Quickly move away",
                    leftConclusion = "You escape potential confrontation but remain unsettled."
                },

                new CardData
                {
                    healthRight = 10f, hungerRight = 0f, moodRight = -10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a hidden path leading to a small pond.",
                    rightAction = "Rest by the pond",
                    rightConclusion = "You regain some energy but feel a bit isolated.",
                    leftAction = "Follow the path further",
                    leftConclusion = "You continue your journey but might be missing a chance to rest."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 10f, moodRight = 10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 10f,
                    description = "A dense fog makes it hard to see the trail ahead.",
                    rightAction = "Wait for the fog to clear",
                    rightConclusion = "You stay safe but lose valuable time.",
                    leftAction = "Push through the fog",
                    leftConclusion = "You make progress but risk stumbling into danger."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = -10f, energyLeft = 0f,
                    description = "You find a secluded, sunlit clearing.",
                    rightAction = "Rest and gather your thoughts",
                    rightConclusion = "You regain some mood and energy but waste time.",
                    leftAction = "Continue exploring",
                    leftConclusion = "You keep moving but miss a chance for a much-needed rest."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 0f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a pile of bones and old equipment.",
                    rightAction = "Search the pile",
                    rightConclusion = "You find some useful items but feel uneasy.",
                    leftAction = "Avoid the area",
                    leftConclusion = "You avoid any potential danger but miss out on possible resources."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 0f, moodLeft = -10f, energyLeft = 10f,
                    description = "You encounter a group of friendly travelers.",
                    rightAction = "Join them for a meal",
                    rightConclusion = "You gain health and mood but lose some energy.",
                    leftAction = "Politely decline and move on",
                    leftConclusion = "You maintain your pace but miss out on a potential boost."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a stash of tools left behind.",
                    rightAction = "Take the tools",
                    rightConclusion = "You gain useful tools but lose some time.",
                    leftAction = "Ignore them",
                    leftConclusion = "You keep moving but miss out on potential benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = -10f,
                    description = "You stumble upon an old, locked chest.",
                    rightAction = "Try to open it",
                    rightConclusion = "You manage to open the chest and find valuable items but suffer some injuries.",
                    leftAction = "Leave it alone",
                    leftConclusion = "You avoid potential risks but miss out on valuable loot."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 0f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "A strong wind picks up, making it difficult to walk.",
                    rightAction = "Find shelter from the wind",
                    rightConclusion = "You avoid the worst of the wind but spend some time finding cover.",
                    leftAction = "Push through",
                    leftConclusion = "You continue your journey but become exhausted from fighting the wind."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a hidden cache of supplies.",
                    rightAction = "Use the supplies",
                    rightConclusion = "You benefit from the supplies but expend some energy.",
                    leftAction = "Leave them",
                    leftConclusion = "You avoid potential risk but miss out on useful resources."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = -10f, hungerLeft = 0f, moodLeft = -10f, energyLeft = 10f,
                    description = "You come across an old, abandoned campsite.",
                    rightAction = "Search for useful items",
                    rightConclusion = "You find some useful items but feel uneasy about the campsite.",
                    leftAction = "Avoid the campsite",
                    leftConclusion = "You continue on but miss out on potential supplies."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = -10f,
                    description = "You encounter a wounded animal.",
                    rightAction = "Try to help it",
                    rightConclusion = "You help the animal and gain some mood but suffer injuries.",
                    leftAction = "Avoid the animal",
                    leftConclusion = "You stay safe but miss out on a potential connection with the animal."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 10f, moodRight = -10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = 0f, moodLeft = 10f, energyLeft = 10f,
                    description = "You discover a small, hidden garden.",
                    rightAction = "Harvest some vegetables",
                    rightConclusion = "You gain some food and mood but spend time harvesting.",
                    leftAction = "Move on",
                    leftConclusion = "You continue without interruption but remain hungry."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 0f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find an old, rusted key on the ground.",
                    rightAction = "Pick it up and investigate",
                    rightConclusion = "The key opens a nearby lock, revealing valuable items.",
                    leftAction = "Leave it alone",
                    leftConclusion = "You avoid potential risks but miss out on unlocking a mystery."
                },

                new CardData
                {
                    healthRight = 10f, hungerRight = -10f, moodRight = -10f, energyRight = 0f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 10f, energyLeft = 0f,
                    description = "You come across a tranquil pond.",
                    rightAction = "Relax by the pond",
                    rightConclusion = "You regain some mood and energy but might waste time.",
                    leftAction = "Continue exploring",
                    leftConclusion = "You keep moving but miss a chance to relax and restore."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = -10f, energyLeft = 10f,
                    description = "You find a collection of herbs and plants.",
                    rightAction = "Gather and use them",
                    rightConclusion = "You benefit from the herbs but lose some energy.",
                    leftAction = "Ignore them",
                    leftConclusion = "You keep moving but miss out on potential healing."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You encounter a family of deer.",
                    rightAction = "Observe from a distance",
                    rightConclusion = "You enjoy a peaceful moment but do not gain any immediate benefits.",
                    leftAction = "Try to approach them",
                    leftConclusion = "You risk startling the deer but might gain some insight or rewards."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 0f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 0f, moodLeft = -10f, energyLeft = 0f,
                    description = "You find an old, abandoned cabin.",
                    rightAction = "Search for supplies inside",
                    rightConclusion = "You find useful supplies but might encounter some danger.",
                    leftAction = "Keep moving",
                    leftConclusion = "You avoid potential risks but miss out on valuable resources."
                },

                new CardData
                {
                    healthRight = 10f, hungerRight = 0f, moodRight = -10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 10f, energyLeft = 0f,
                    description = "You come across a small waterfall.",
                    rightAction = "Enjoy the view and relax",
                    rightConclusion = "You gain mood and enjoy a serene moment but lose some energy.",
                    leftAction = "Proceed without stopping",
                    leftConclusion = "You continue your journey but miss a chance to rest and enjoy the view."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 10f, moodRight = -10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = 0f, moodLeft = 10f, energyLeft = 10f,
                    description = "You find a small, makeshift campfire.",
                    rightAction = "Use the fire to cook food",
                    rightConclusion = "You regain some hunger and mood but use up resources.",
                    leftAction = "Leave it and continue",
                    leftConclusion = "You avoid potential risks but stay hungry and cold."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = -10f, energyLeft = 0f,
                    description = "You encounter a group of other survivors.",
                    rightAction = "Join them for warmth and food",
                    rightConclusion = "You gain warmth and food but lose some energy.",
                    leftAction = "Avoid them and stay hidden",
                    leftConclusion = "You stay safe but miss out on a chance to socialize and rest."
                },
                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a hidden cave.",
                    rightAction = "Explore the cave",
                    rightConclusion =
                        "You discover some interesting artifacts and gain mood and energy but risk potential dangers.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid any potential risks but miss out on possible rewards."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = -10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 10f, energyLeft = 0f,
                    description = "You hear an ominous noise in the distance.",
                    rightAction = "Investigate the sound",
                    rightConclusion =
                        "You uncover a hidden threat but gain valuable information about potential dangers.",
                    leftAction = "Ignore it and keep moving",
                    leftConclusion = "You avoid potential danger but might miss out on a clue about nearby threats."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 0f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You come across an old, rusty bridge.",
                    rightAction = "Cross the bridge",
                    rightConclusion =
                        "You successfully cross but with some risk, which affects your mood. You find a shortcut.",
                    leftAction = "Find another route",
                    leftConclusion =
                        "You avoid the risk but spend extra time finding a new path, which might affect your energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 10f, moodRight = 0f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You spot a few wild mushrooms on the ground.",
                    rightAction = "Collect and examine them",
                    rightConclusion =
                        "You find some edible mushrooms that help with hunger but could have been poisonous.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid any risk but also miss out on potentially useful resources."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 0f, moodRight = 10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = -10f, energyLeft = 10f,
                    description = "You find a sunny spot perfect for resting.",
                    rightAction = "Rest and rejuvenate",
                    rightConclusion = "You regain mood and some energy but might spend too much time resting.",
                    leftAction = "Keep going",
                    leftConclusion =
                        "You continue your journey but miss out on a chance to restore your mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 0f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = 0f, moodLeft = 10f, energyLeft = 0f,
                    description = "You discover an old, abandoned shelter.",
                    rightAction = "Search for useful items inside",
                    rightConclusion = "You find some useful supplies but might encounter remnants of past dangers.",
                    leftAction = "Ignore it",
                    leftConclusion = "You avoid potential risks but miss out on useful items that could help you."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You come across a group of friendly animals.",
                    rightAction = "Observe from a distance",
                    rightConclusion = "You gain a boost in mood and enjoy a peaceful moment without risk.",
                    leftAction = "Try to approach them",
                    leftConclusion = "You risk startling the animals but might gain some unique rewards or information."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You spot a clear stream with fresh water.",
                    rightAction = "Drink and refill your supplies",
                    rightConclusion =
                        "You quench your thirst and gain some energy but might need to find more food soon.",
                    leftAction = "Continue without stopping",
                    leftConclusion = "You stay hydrated but miss the opportunity to refill your water supplies."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 0f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find an old, dusty backpack.",
                    rightAction = "Check the backpack for items",
                    rightConclusion = "You discover useful items or clues but might be exposed to some hazards.",
                    leftAction = "Leave it alone",
                    leftConclusion = "You avoid potential hazards but miss out on potentially helpful items."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a patch of edible plants.",
                    rightAction = "Gather and eat the plants",
                    rightConclusion =
                        "You satisfy your hunger and boost your mood but could be at risk of eating something bad.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid the risk of potential poisoning but miss out on valuable nourishment."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You stumble upon an old, crumbling statue.",
                    rightAction = "Investigate the statue",
                    rightConclusion = "You find historical artifacts or clues but expend some energy.",
                    leftAction = "Ignore it and continue",
                    leftConclusion = "You avoid the effort and risk but miss out on potentially interesting finds."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = -10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "A sudden fog rolls in, obscuring your vision.",
                    rightAction = "Find a safe place to wait",
                    rightConclusion = "You stay safe from potential threats but lose some time and energy.",
                    leftAction = "Keep moving cautiously",
                    leftConclusion =
                        "You continue on your path but might face unforeseen dangers due to limited visibility."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 10f, moodRight = -10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = -10f, moodLeft = 10f, energyLeft = 10f,
                    description = "You find a cozy, sunlit glade.",
                    rightAction = "Rest and enjoy the sun",
                    rightConclusion = "You restore some mood and energy but could lose valuable time.",
                    leftAction = "Continue your journey",
                    leftConclusion =
                        "You keep moving but miss out on a relaxing break that could have improved your mood."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 0f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You encounter a curious squirrel.",
                    rightAction = "Try to feed it",
                    rightConclusion = "You might gain a small morale boost but could waste some resources.",
                    leftAction = "Observe from a distance",
                    leftConclusion = "You avoid wasting resources but miss out on a potential morale boost."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find an old, worn-out map.",
                    rightAction = "Study the map",
                    rightConclusion = "You uncover useful information about the area but use up some energy.",
                    leftAction = "Discard it and keep moving",
                    leftConclusion = "You avoid potential distractions but miss out on helpful information."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a patch of wild berries.",
                    rightAction = "Eat some berries",
                    rightConclusion = "You satisfy your hunger and gain some energy but might face some health risks.",
                    leftAction = "Avoid the berries",
                    leftConclusion = "You avoid potential health risks but also miss out on valuable food."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You encounter an overgrown, tangled path.",
                    rightAction = "Navigate through the path",
                    rightConclusion = "You manage to clear the path but expend some energy and time.",
                    leftAction = "Find a new route",
                    leftConclusion = "You avoid immediate obstacles but may face more challenges later."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You come across an old, abandoned campsite.",
                    rightAction = "Search the campsite for supplies",
                    rightConclusion =
                        "You find useful supplies or clues but might encounter remnants of previous campers.",
                    leftAction = "Skip the campsite",
                    leftConclusion = "You avoid potential risks but miss out on valuable supplies."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = -10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "A dense fog starts to roll in.",
                    rightAction = "Wait for the fog to clear",
                    rightConclusion = "You stay safe but lose some time and energy.",
                    leftAction = "Continue through the fog",
                    leftConclusion = "You keep moving but might face increased risk due to reduced visibility."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = 10f, moodRight = -10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = -10f, moodLeft = 10f, energyLeft = 10f,
                    description = "You find a cool, shady spot under a large tree.",
                    rightAction = "Rest and recover",
                    rightConclusion = "You regain some mood and energy but lose some time.",
                    leftAction = "Move on quickly",
                    leftConclusion = "You avoid wasting time but miss out on a chance to relax and recover."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden spring with fresh water.",
                    rightAction = "Drink and refill your supplies",
                    rightConclusion = "You quench your thirst and gain some mood but might need more food.",
                    leftAction = "Continue without stopping",
                    leftConclusion = "You stay hydrated but miss the chance to refill your water supplies."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small grove of fruit trees.",
                    rightAction = "Gather and eat some fruit",
                    rightConclusion = "You satisfy your hunger and boost your mood but may consume too much energy.",
                    leftAction = "Keep moving",
                    leftConclusion = "You avoid possible risks of overconsumption but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You encounter a peaceful, reflective pond.",
                    rightAction = "Rest and reflect",
                    rightConclusion = "You gain some mood improvement but spend energy and time.",
                    leftAction = "Pass by without stopping",
                    leftConclusion =
                        "You keep moving and avoid losing time but miss out on a chance to boost your mood."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a stash of old, usable camping gear.",
                    rightAction = "Inspect and take the gear",
                    rightConclusion =
                        "You gain useful equipment that can assist you in future tasks but might have hidden problems.",
                    leftAction = "Leave it behind",
                    leftConclusion = "You avoid any potential complications but miss out on valuable gear."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 0f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You come across an old, weathered journal.",
                    rightAction = "Read through the journal",
                    rightConclusion = "You gain insights or clues about the area but might expend some energy.",
                    leftAction = "Ignore it and keep moving",
                    leftConclusion = "You avoid spending energy but miss out on potentially useful information."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a well-preserved campsite with useful supplies.",
                    rightAction = "Search and take the supplies",
                    rightConclusion = "You gain valuable items or resources but could encounter potential dangers.",
                    leftAction = "Skip the campsite",
                    leftConclusion = "You avoid risks but miss out on valuable supplies."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a rare medicinal herb.",
                    rightAction = "Collect and use the herb",
                    rightConclusion = "You gain health benefits but might face side effects or require extra energy.",
                    leftAction = "Leave the herb behind",
                    leftConclusion = "You avoid potential side effects but miss out on possible health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You stumble upon an abandoned mine entrance.",
                    rightAction = "Explore the mine",
                    rightConclusion = "You uncover hidden resources or threats but expend some health and energy.",
                    leftAction = "Avoid the mine",
                    leftConclusion = "You skip potential resources but avoid possible dangers or health risks."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You encounter a mysterious, ancient relic.",
                    rightAction = "Examine the relic",
                    rightConclusion = "You might discover something valuable or gain mood but risk some energy.",
                    leftAction = "Leave the relic alone",
                    leftConclusion = "You avoid potential risks but miss out on possible discoveries."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = -10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a secluded meadow with fresh grass.",
                    rightAction = "Rest and graze",
                    rightConclusion = "You restore some mood and energy but could face some health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You continue your journey but miss a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a hidden cave.",
                    rightAction = "Explore the cave",
                    rightConclusion = "You might find valuable resources or shelter but could face hidden dangers.",
                    leftAction = "Continue your path",
                    leftConclusion = "You avoid potential dangers but miss out on potential resources or shelter."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 0f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a series of strange markings on a tree.",
                    rightAction = "Investigate the markings",
                    rightConclusion = "You gain some knowledge or clues but might use up some energy.",
                    leftAction = "Ignore the markings",
                    leftConclusion = "You avoid expending energy but miss out on potentially useful information."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You encounter a friendly forest animal.",
                    rightAction = "Interact with the animal",
                    rightConclusion =
                        "You gain some mood and possibly some useful information but might lose some energy.",
                    leftAction = "Avoid interaction",
                    leftConclusion = "You conserve energy but miss out on possible mood improvement or information."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small pile of dried herbs.",
                    rightAction = "Collect and use the herbs",
                    rightConclusion = "You gain some health benefits or mood improvement but might use some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You conserve energy but miss out on potential health or mood benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a secluded pond with fish.",
                    rightAction = "Try fishing",
                    rightConclusion = "You might catch some food and improve mood but expend some energy.",
                    leftAction = "Continue without fishing",
                    leftConclusion = "You avoid expending energy but miss out on a potential food source."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You encounter a sudden downpour.",
                    rightAction = "Take shelter and wait it out",
                    rightConclusion = "You stay dry but lose time and energy.",
                    leftAction = "Brave the rain and keep moving",
                    leftConclusion = "You continue your journey but might face increased risks and discomfort."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden path leading to a waterfall.",
                    rightAction = "Explore the waterfall",
                    rightConclusion = "You might discover something refreshing or beautiful but expend some energy.",
                    leftAction = "Skip the path",
                    leftConclusion = "You avoid potential risks but miss out on a chance to explore."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You come across an abandoned hunter's cabin.",
                    rightAction = "Search the cabin for supplies",
                    rightConclusion = "You might find useful resources or clues but face potential dangers.",
                    leftAction = "Move on quickly",
                    leftConclusion = "You avoid possible risks but miss out on useful supplies."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small berry bush with ripe fruit.",
                    rightAction = "Pick and eat the berries",
                    rightConclusion = "You satisfy your hunger and gain some energy but might face health risks.",
                    leftAction = "Ignore the bush",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You encounter a small, quiet clearing.",
                    rightAction = "Rest and enjoy the peace",
                    rightConclusion = "You improve your mood and energy but spend some time.",
                    leftAction = "Continue on",
                    leftConclusion = "You keep moving and avoid losing time but miss out on a mood boost."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = 10f, energyRight = -10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover an old, rusty toolbox.",
                    rightAction = "Inspect the toolbox",
                    rightConclusion = "You might find useful tools or materials but expend some energy.",
                    leftAction = "Leave it behind",
                    leftConclusion = "You avoid potential risks but miss out on possibly valuable tools."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You encounter a mysterious, abandoned shrine.",
                    rightAction = "Investigate the shrine",
                    rightConclusion = "You might uncover secrets or gain mood but could use some energy.",
                    leftAction = "Move past the shrine",
                    leftConclusion = "You avoid potential risks but miss out on possible discoveries."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 0f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You stumble upon a small, bubbling brook.",
                    rightAction = "Rest and refresh",
                    rightConclusion = "You gain some mood and energy but might spend extra time.",
                    leftAction = "Continue without stopping",
                    leftConclusion = "You avoid losing time but miss out on a chance to refresh."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a hidden stash of emergency rations.",
                    rightAction = "Take and consume the rations",
                    rightConclusion = "You satisfy your hunger and gain energy but might face some health risks.",
                    leftAction = "Leave the rations",
                    leftConclusion = "You avoid potential risks but miss out on valuable food."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, well-hidden cave.",
                    rightAction = "Explore the cave",
                    rightConclusion = "You might discover shelter or resources but expend some energy.",
                    leftAction = "Keep moving",
                    leftConclusion = "You avoid potential risks but miss out on possible shelter or resources."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You encounter a small, enchanted grove.",
                    rightAction = "Explore the grove",
                    rightConclusion = "You might find something magical or beneficial but expend some energy.",
                    leftAction = "Skip the grove",
                    leftConclusion = "You avoid potential energy loss but miss out on possible magical benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 0f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a hidden treasure chest.",
                    rightAction = "Open the chest",
                    rightConclusion = "You might find valuable items or clues but face potential risks.",
                    leftAction = "Leave the chest alone",
                    leftConclusion = "You avoid potential dangers but miss out on valuable items."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a small, old cabin with smoke coming out of the chimney.",
                    rightAction = "Investigate the cabin",
                    rightConclusion = "You might find helpful resources or shelter but face potential risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential dangers but miss out on possible resources or shelter."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You encounter a gentle rain shower.",
                    rightAction = "Seek shelter until it passes",
                    rightConclusion = "You stay dry but lose some time and energy.",
                    leftAction = "Walk through the rain",
                    leftConclusion = "You continue moving but might experience discomfort and increased risk."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a hidden garden with rare plants.",
                    rightAction = "Explore the garden",
                    rightConclusion = "You might find valuable plants or herbs but expend some energy.",
                    leftAction = "Continue on",
                    leftConclusion = "You avoid potential risks but miss out on valuable resources."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a small cave with ancient drawings.",
                    rightAction = "Examine the drawings",
                    rightConclusion = "You gain insights or clues but might expend some energy.",
                    leftAction = "Skip the cave",
                    leftConclusion = "You avoid expending energy but miss out on potentially useful information."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 0f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, serene pond with fish.",
                    rightAction = "Try to catch some fish",
                    rightConclusion = "You might gain food and improve mood but use some energy.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid expending energy but miss out on potential food and mood improvement."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a hidden cache of supplies.",
                    rightAction = "Take and use the supplies",
                    rightConclusion = "You gain useful items but might face some health risks.",
                    leftAction = "Ignore the cache",
                    leftConclusion = "You avoid potential risks but miss out on valuable supplies."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, quiet glade.",
                    rightAction = "Rest and enjoy the quiet",
                    rightConclusion = "You improve your mood and regain some energy but lose time.",
                    leftAction = "Continue on",
                    leftConclusion = "You keep moving and avoid wasting time but miss a mood boost and recovery."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You encounter an old, forgotten burial site.",
                    rightAction = "Investigate the site",
                    rightConclusion = "You might find clues or items but face potential energy loss.",
                    leftAction = "Avoid the site",
                    leftConclusion = "You conserve energy but miss out on possible discoveries."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a hidden stash of dried meat.",
                    rightAction = "Eat the dried meat",
                    rightConclusion = "You satisfy your hunger and gain some energy but might face health risks.",
                    leftAction = "Leave the stash",
                    leftConclusion = "You avoid potential health issues but miss out on valuable food."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a small, hidden cabin with supplies.",
                    rightAction = "Search the cabin",
                    rightConclusion = "You find useful resources but might face some health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential dangers but miss out on valuable supplies."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, peaceful meadow.",
                    rightAction = "Rest and enjoy the meadow",
                    rightConclusion = "You regain some mood and energy but lose time.",
                    leftAction = "Move on quickly",
                    leftConclusion = "You avoid losing time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You encounter a small, overgrown trail.",
                    rightAction = "Navigate through the trail",
                    rightConclusion = "You clear the path but expend some energy.",
                    leftAction = "Find a different route",
                    leftConclusion = "You avoid immediate obstacles but might face more challenges later."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden grove with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain some energy but might face health risks.",
                    leftAction = "Ignore the fruit",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a hidden, tranquil pond.",
                    rightAction = "Rest by the pond",
                    rightConclusion = "You improve your mood and regain energy but might spend extra time.",
                    leftAction = "Move on",
                    leftConclusion = "You continue on and avoid wasting time but miss out on a mood boost."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, ancient altar.",
                    rightAction = "Examine the altar",
                    rightConclusion = "You might uncover something valuable or insightful but expend some energy.",
                    leftAction = "Move past the altar",
                    leftConclusion = "You avoid potential energy loss but miss out on possible discoveries."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 0f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You stumble upon a small, abandoned campsite.",
                    rightAction = "Search the campsite",
                    rightConclusion = "You might find useful supplies but face potential risks.",
                    leftAction = "Avoid the campsite",
                    leftConclusion = "You avoid potential dangers but miss out on valuable resources."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a small, secluded cave.",
                    rightAction = "Explore the cave",
                    rightConclusion = "You might find shelter or resources but expend some energy.",
                    leftAction = "Continue on",
                    leftConclusion = "You avoid potential risks but miss out on possible shelter or resources."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You encounter a small, serene pond.",
                    rightAction = "Relax by the pond",
                    rightConclusion = "You regain mood and energy but spend some time.",
                    leftAction = "Move along",
                    leftConclusion = "You avoid losing time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, abandoned shack with supplies.",
                    rightAction = "Search the shack",
                    rightConclusion = "You might find useful supplies but face potential risks.",
                    leftAction = "Continue your journey",
                    leftConclusion = "You avoid potential dangers but miss out on valuable resources."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You come across a hidden grove with a small pond.",
                    rightAction = "Rest by the pond",
                    rightConclusion = "You gain mood and energy but spend some time.",
                    leftAction = "Move on quickly",
                    leftConclusion = "You avoid losing time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a hidden cache of firewood.",
                    rightAction = "Collect and use the firewood",
                    rightConclusion = "You gain warmth and comfort but might expend some energy.",
                    leftAction = "Ignore the firewood",
                    leftConclusion = "You avoid using energy but miss out on potential warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a small, hidden cave with fresh berries.",
                    rightAction = "Pick and eat the berries",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Leave the berries",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, secluded glade with wild herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health or mood benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on possible health or mood benefits."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a hidden stash of clean water.",
                    rightAction = "Drink the water",
                    rightConclusion = "You replenish your energy but might face some hunger or mood risks.",
                    leftAction = "Leave the water",
                    leftConclusion = "You avoid potential risks but miss out on energy replenishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden cave with a cozy nook.",
                    rightAction = "Rest in the cozy nook",
                    rightConclusion = "You regain some mood and energy but spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid losing time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden, well-stocked pantry.",
                    rightAction = "Take and use the supplies",
                    rightConclusion = "You gain valuable food and supplies but might face some health risks.",
                    leftAction = "Leave the pantry",
                    leftConclusion = "You avoid potential risks but miss out on valuable resources."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden cave with a relaxing atmosphere.",
                    rightAction = "Rest in the cave",
                    rightConclusion = "You gain mood and energy but spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 0f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You encounter a small, abandoned hut.",
                    rightAction = "Search the hut",
                    rightConclusion = "You might find useful supplies but face potential risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential dangers but miss out on valuable resources."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a hidden grove with a small, tranquil stream.",
                    rightAction = "Relax by the stream",
                    rightConclusion = "You gain mood and energy but might lose some time.",
                    leftAction = "Continue your journey",
                    leftConclusion = "You avoid losing time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden, well-preserved campsite.",
                    rightAction = "Rest and use the supplies",
                    rightConclusion = "You regain mood and energy but might face some health risks.",
                    leftAction = "Continue on",
                    leftConclusion = "You avoid potential health risks but miss out on valuable resources."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden glade with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You gain health benefits but might expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on possible health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, overgrown glade with wild fruits.",
                    rightAction = "Pick and eat the fruits",
                    rightConclusion = "You satisfy your hunger and gain some energy but might face health risks.",
                    leftAction = "Leave the fruits",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a hidden, serene pool.",
                    rightAction = "Rest by the pool",
                    rightConclusion = "You regain mood and energy but spend some time.",
                    leftAction = "Continue on",
                    leftConclusion = "You avoid losing time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, abandoned hut with a cozy fire.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Continue your journey",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a hidden cache of survival gear.",
                    rightAction = "Take and use the gear",
                    rightConclusion = "You gain useful items but might face some health risks.",
                    leftAction = "Ignore the gear",
                    leftConclusion = "You avoid potential risks but miss out on valuable gear."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden grove with fresh water.",
                    rightAction = "Drink the water",
                    rightConclusion = "You replenish your energy but might face some hunger or mood risks.",
                    leftAction = "Ignore the water",
                    leftConclusion = "You avoid potential risks but miss out on energy replenishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, secluded nook with wild berries.",
                    rightAction = "Pick and eat the berries",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a small, tranquil grove.",
                    rightAction = "Rest in the grove",
                    rightConclusion = "You regain mood and energy but might lose some time.",
                    leftAction = "Continue on",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, secluded glade with medicinal plants.",
                    rightAction = "Gather and use the plants",
                    rightConclusion = "You might gain health or mood benefits but expend some energy.",
                    leftAction = "Ignore the plants",
                    leftConclusion = "You avoid expending energy but miss out on possible health or mood benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a hidden grove with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden grove with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You gain health benefits but might expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on possible health benefits."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden glade with wild herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health or mood benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health or mood benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a small, hidden stream.",
                    rightAction = "Rest by the stream",
                    rightConclusion = "You regain mood and energy but spend some time.",
                    leftAction = "Continue your journey",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden cave with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain some energy but might face health risks.",
                    leftAction = "Leave the fruit",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a small, hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid expending energy but miss out on possible health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You come across a hidden glade with wild mushrooms.",
                    rightAction = "Pick and eat the mushrooms",
                    rightConclusion = "You might gain some energy but face potential health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on a chance to gain energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with a warm spring.",
                    rightAction = "Relax by the spring",
                    rightConclusion = "You gain mood and energy but might face some health risks.",
                    leftAction = "Move on",
                    leftConclusion =
                        "You avoid potential health risks but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 0f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden grove with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, secluded glade with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden cave with a warm spring.",
                    rightAction = "Relax by the spring",
                    rightConclusion = "You regain mood and energy but might lose some time.",
                    leftAction = "Continue on",
                    leftConclusion = "You avoid losing time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a hidden grove with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on possible health benefits."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a small, hidden cave with a cozy nook.",
                    rightAction = "Rest in the nook",
                    rightConclusion = "You regain mood and energy but spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid losing time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a hidden grove with wild herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden glade with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain some energy but might face health risks.",
                    leftAction = "Leave the fruit",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a small, hidden stream with fresh berries.",
                    rightAction = "Pick and eat the berries",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Continue your journey",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with wild herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You gain health benefits but might expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on possible health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden cave with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health or mood benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on possible health or mood benefits."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden stream.",
                    rightAction = "Rest by the stream",
                    rightConclusion = "You regain mood and energy but spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden cave with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain some energy but might face health risks.",
                    leftAction = "Ignore the fruit",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with a warm spring.",
                    rightAction = "Relax by the spring",
                    rightConclusion = "You regain mood and energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion =
                        "You avoid potential health risks but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health or mood benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health or mood benefits."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden glade with wild berries.",
                    rightAction = "Pick and eat the berries",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden cave with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain some energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with a warm spring.",
                    rightAction = "Relax by the spring",
                    rightConclusion = "You regain mood and energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion =
                        "You avoid potential health risks but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on possible health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden glade with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain some energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health or mood benefits but expend some energy.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid expending energy but miss out on possible health or mood benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a small, hidden glade with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health or mood benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on possible health or mood benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden glade with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain some energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health or mood benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health or mood benefits."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a hidden glade with a cozy nook.",
                    rightAction = "Rest in the nook",
                    rightConclusion = "You regain mood and energy but might spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with a warm spring.",
                    rightAction = "Relax by the spring",
                    rightConclusion = "You regain mood and energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion =
                        "You avoid potential health risks but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with wild mushrooms.",
                    rightAction = "Pick and eat the mushrooms",
                    rightConclusion = "You might gain some energy but face potential health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on a chance to gain energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You find a small, hidden cave with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain some energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a small, hidden glade with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health or mood benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on possible health or mood benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden cave with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = 0f, moodRight = 10f, energyRight = 10f,
                    healthLeft = 10f, hungerLeft = -10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a small, hidden glade with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid expending energy but miss out on possible health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = 0f, hungerRight = -10f, moodRight = -10f, energyRight = 10f,
                    healthLeft = -10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 0f,
                    description = "You discover a small, hidden glade with a cozy nook.",
                    rightAction = "Rest in the nook",
                    rightConclusion = "You regain mood and energy but might spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with a warm spring.",
                    rightAction = "Relax by the spring",
                    rightConclusion = "You regain mood and energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion =
                        "You avoid potential health risks but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden cave with wild mushrooms.",
                    rightAction = "Pick and eat the mushrooms",
                    rightConclusion = "You might gain some energy but face potential health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on a chance to gain energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden glade with a cozy nook.",
                    rightAction = "Rest in the nook",
                    rightConclusion = "You regain mood and energy but might spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with a warm spring.",
                    rightAction = "Relax by the spring",
                    rightConclusion = "You regain mood and energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion =
                        "You avoid potential health risks but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with wild mushrooms.",
                    rightAction = "Pick and eat the mushrooms",
                    rightConclusion = "You might gain some energy but face potential health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on a chance to gain energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden glade with a cozy nook.",
                    rightAction = "Rest in the nook",
                    rightConclusion = "You regain mood and energy but might spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with a cozy nook.",
                    rightAction = "Rest in the nook",
                    rightConclusion = "You regain mood and energy but might spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden glade with a cozy nook.",
                    rightAction = "Rest in the nook",
                    rightConclusion = "You regain mood and energy but might spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden glade with a warm spring.",
                    rightAction = "Relax by the spring",
                    rightConclusion = "You regain mood and energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion =
                        "You avoid potential health risks but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden cave with wild mushrooms.",
                    rightAction = "Pick and eat the mushrooms",
                    rightConclusion = "You might gain some energy but face potential health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on a chance to gain energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden glade with a cozy nook.",
                    rightAction = "Rest in the nook",
                    rightConclusion = "You regain mood and energy but might spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with wild mushrooms.",
                    rightAction = "Pick and eat the mushrooms",
                    rightConclusion = "You might gain some energy but face potential health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on a chance to gain energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden glade with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with a cozy nook.",
                    rightAction = "Rest in the nook",
                    rightConclusion = "You regain mood and energy but might spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden cave with wild mushrooms.",
                    rightAction = "Pick and eat the mushrooms",
                    rightConclusion = "You might gain some energy but face potential health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on a chance to gain energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden glade with a warm spring.",
                    rightAction = "Relax by the spring",
                    rightConclusion = "You regain mood and energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion =
                        "You avoid potential health risks but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with wild mushrooms.",
                    rightAction = "Pick and eat the mushrooms",
                    rightConclusion = "You might gain some energy but face potential health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on a chance to gain energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with a cozy nook.",
                    rightAction = "Rest in the nook",
                    rightConclusion = "You regain mood and energy but might spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with wild mushrooms.",
                    rightAction = "Pick and eat the mushrooms",
                    rightConclusion = "You might gain some energy but face potential health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on a chance to gain energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with a cozy nook.",
                    rightAction = "Rest in the nook",
                    rightConclusion = "You regain mood and energy but might spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with wild mushrooms.",
                    rightAction = "Pick and eat the mushrooms",
                    rightConclusion = "You might gain some energy but face potential health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on a chance to gain energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a hidden glade with a warm spring.",
                    rightAction = "Relax by the spring",
                    rightConclusion = "You regain mood and energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion =
                        "You avoid potential health risks but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with wild mushrooms.",
                    rightAction = "Pick and eat the mushrooms",
                    rightConclusion = "You might gain some energy but face potential health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on a chance to gain energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with a cozy nook.",
                    rightAction = "Rest in the nook",
                    rightConclusion = "You regain mood and energy but might spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with wild mushrooms.",
                    rightAction = "Pick and eat the mushrooms",
                    rightConclusion = "You might gain some energy but face potential health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on a chance to gain energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with medicinal herbs.",
                    rightAction = "Gather and use the herbs",
                    rightConclusion = "You might gain health benefits but expend some energy.",
                    leftAction = "Ignore the herbs",
                    leftConclusion = "You avoid expending energy but miss out on potential health benefits."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden glade with a cozy nook.",
                    rightAction = "Rest in the nook",
                    rightConclusion = "You regain mood and energy but might spend some time.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid wasting time but miss out on a chance to recover mood and energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with fresh fruit.",
                    rightAction = "Pick and eat the fruit",
                    rightConclusion = "You satisfy your hunger and gain energy but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on nourishment."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You discover a small, hidden cave with wild mushrooms.",
                    rightAction = "Pick and eat the mushrooms",
                    rightConclusion = "You might gain some energy but face potential health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health issues but miss out on a chance to gain energy."
                },

                new CardData
                {
                    healthRight = -10f, hungerRight = -10f, moodRight = 10f, energyRight = 0f,
                    healthLeft = 10f, hungerLeft = 10f, moodLeft = 0f, energyLeft = 10f,
                    description = "You find a small, hidden glade with a cozy fire pit.",
                    rightAction = "Warm yourself by the fire",
                    rightConclusion = "You gain warmth and comfort but might face health risks.",
                    leftAction = "Move on",
                    leftConclusion = "You avoid potential health risks but miss out on warmth and comfort."
                }
            };

            ShuffleCardDataList();

            if (shuffledCardDataList.Count > 0)
            {
                UpdateCardData(shuffledCardDataList[currentCardIndex]);
            }
    }

    private void ShuffleCardDataList()
    {
        shuffledCardDataList = new List<CardData>(cardDataList);
        for (int i = shuffledCardDataList.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            CardData temp = shuffledCardDataList[i];
            shuffledCardDataList[i] = shuffledCardDataList[j];
            shuffledCardDataList[j] = temp;
        }
    }

    public void UpdateCardData(CardData cardData)
    {
        descriptionText.text = cardData.description;
        leftOptionText.text = cardData.leftAction;
        rightOptionText.text = cardData.rightAction;

        UpdateStats(cardData);
    }

    private void UpdateStats(CardData cardData)
    {
        healthImage.fillAmount = Mathf.Clamp01(healthImage.fillAmount + cardData.healthRight / 100f);
        hungerImage.fillAmount = Mathf.Clamp01(hungerImage.fillAmount + cardData.hungerRight / 100f);
        moodImage.fillAmount = Mathf.Clamp01(moodImage.fillAmount + cardData.moodRight / 100f);
        energyImage.fillAmount = Mathf.Clamp01(energyImage.fillAmount + cardData.energyRight / 100f);
    }

    public void ShowNextCard(bool leftSwipe)
    {
        if (leftSwipe)
        {
            shuffledCardDataList[currentCardIndex].leftConclusionShown = true;
        }
        else
        {
            shuffledCardDataList[currentCardIndex].rightConclusionShown = true;
        }

        currentCardIndex = (currentCardIndex + 1) % shuffledCardDataList.Count;
        UpdateCardData(shuffledCardDataList[currentCardIndex]);
    }

    public CardData GetCurrentCardData()
    {
        return shuffledCardDataList[currentCardIndex];
    }
}