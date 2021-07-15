using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [Header("Scene object references")]
    FlexibleGridLayout gridLayout;
    Text text;

    public List<int> selectedTiles = new List<int>();

    [SerializeField]
    GameObject letterPrefab;

    [Header("Variables for checking if a word is real")]
    public TextAsset dictionaryFile;
    public Dictionary<string, string> dictionaryOfWords;

    WeightedRandomiser<char> weightedRandomiser;

    Text scoreText;
    Text levelText;
    Text longestWordText;

    bool levelFinished;


    private void Awake()
    {
        //Load the dictionary in
        dictionaryFile = dictionaryFile ?? Resources.Load("words_alpha.txt", typeof(TextAsset)) as TextAsset;
        dictionaryOfWords = dictionaryFile.text.Split("\n"[0]).ToDictionary(x => x.Trim(), x => x.Trim());
    }


    // Start is called before the first frame update
    IEnumerator Start()
    {
        gridLayout = GetComponent<FlexibleGridLayout>();
        text = GameObject.Find("Text").GetComponent<Text>();

        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
        levelText = GameObject.Find("Level Text").GetComponent<Text>();
        longestWordText = GameObject.Find("LongestWord").GetComponent<Text>();

        scoreText.text = ScoreSaver.score + "/" + ScoreSaver.maxScore;
        levelText.text = "Level: " + ScoreSaver.level;
        longestWordText.text = text.text + " | " + ScoreSaver.longestWord;

        weightedRandomiser = new WeightedRandomiser<char>(letterWeights, 15+ScoreSaver.level);

        gridLayout.CalculateLayoutInputHorizontal();
        //gridLayout.cellSize = new Vector2((transform.GetComponent<RectTransform>().rect.width / 4)-15, (transform.GetComponent<RectTransform>().rect.height / 6)-15);
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

        //Initialise each of the cells in the grid
        foreach (Transform child in transform)
        {
            //Let the program start putting the kids in whenever it's ready, shouldn't be too long but I don't want a hang or freeze
            yield return null;
            NewLetter(child);
        }

        transform.SetAsLastSibling();

    }

    /// <summary>
    /// Weights for how often each letter should appear
    /// </summary>
    Dictionary<char, int> letterWeights = new Dictionary<char, int>()
    {
        {'E', 56},
        {'A', 43},
        {'R', 38},
        {'I', 38},
        {'O', 36},
        {'T', 35},
        {'N', 33},
        {'S', 29},
        {'L', 27},
        {'C', 23},
        {'U', 18},
        {'D', 17},
        {'P', 16},
        {'M', 15},
        {'H', 15},
        {'G', 12},
        {'B', 10},
        {'F', 9},
        {'Y', 9},
        {'W', 6},
        {'K', 5},
        {'V', 5},
        {'X', 1},
        {'Z', 1},
        {'J', 1},
        {'Q', 1},
    };

    /// <summary>
    /// Change the letter at , both in the grid and visually
    /// </summary>
    /// <param name="targetCell"></param>
    void NewLetter(Transform targetCell)
    {
        //Set the letter randomly (weighted random for more common letters)
        targetCell.GetComponent<LetterGridCell>().letter = weightedRandomiser.TakeOne();


        //If letter already exists, delete it
        if (targetCell.GetComponent<LetterGridCell>().letterObject)
        {
            targetCell.GetComponent<LetterGridCell>().KillLetterObject();
        }

        //Make new letter (letter is just for visuals, animating in etc
        GameObject newLetter = Instantiate(letterPrefab, transform.parent);
        newLetter.GetComponent<UITweener>().from = GameObject.Find("From").transform;
        newLetter.GetComponent<UITweener>().to = targetCell;
        newLetter.GetComponent<UITweener>().ReadyToStart();

        //Tell the cell about the letter
        targetCell.GetComponent<LetterGridCell>().letterObject = newLetter;

        //Set the text to be right
        newLetter.GetComponentInChildren<Text>().text = targetCell.GetComponent<LetterGridCell>().letter.ToString();

        //Make sure the grid (for clicking on and highlighting letters) is on top
        transform.SetAsLastSibling();

        //Send the object in
        newLetter.GetComponent<UITweener>().In();
    }

    /// <summary>
    /// Move a cell down into null spaces until it runs out of null spaces (hits the bottom i.e. how gravity works)
    /// </summary>
    /// <param name="targetCell"></param>
    void Drop(Transform targetCell)
    {

        //If cell is empty, go up a cell to search for something to fill it 
        if (targetCell.GetComponent<LetterGridCell>().letter == (char)0)
        {
            if (targetCell.GetSiblingIndex() < 4)
            {
                //If at the top, drop a new letter in (as if from above the top)
                NewLetter(targetCell);
            }
            else
            {
                //If this isn't the top, drop the one above it
                Drop(transform.GetChild(targetCell.GetSiblingIndex() - 4));
            }
        }

        //Check if this cell is full and cell below is empty
        if (targetCell.GetComponent<LetterGridCell>().letter != (char)0) {
            //Check if there is anything underneath this transform (can't fall if already at the bottom)
            if (targetCell.GetSiblingIndex() + 4 < transform.childCount)
            {
                //Get the cell below
                Transform belowCell = transform.GetChild(targetCell.GetSiblingIndex() + 4);

                if (belowCell.GetComponent<LetterGridCell>().letter == (char)0)
                {
                    //It's empty, move this letter to it
                    belowCell.GetComponent<LetterGridCell>().letter = targetCell.GetComponent<LetterGridCell>().letter;
                    //Cell moved from should now be blank
                    targetCell.GetComponent<LetterGridCell>().letter = (char)0;


                    //Move the visual from this cell to the empty one below
                    GameObject visualCell = targetCell.GetComponent<LetterGridCell>().letterObject;
                        targetCell.GetComponent<LetterGridCell>().letterObject = null;
                        belowCell.GetComponent<LetterGridCell>().letterObject = visualCell;
                        //visualCell.GetComponent<UITweener>().from = targetCell;
                        visualCell.GetComponent<UITweener>().to = belowCell;
                        //visualCell.GetComponent<UITweener>().ReadyToStart();
                        visualCell.GetComponent<UITweener>().In(true);
                        visualCell.GetComponentInChildren<Text>().text = belowCell.GetComponent<LetterGridCell>().letter.ToString();


                    Drop(belowCell);
                } else
                {
                    if (targetCell.GetSiblingIndex() >= 4)
                    {
                        Drop(transform.GetChild(targetCell.GetSiblingIndex() - 4));
                    }
                }

            }


        }

        

    }

    
    //Vector-like struct for index only vector2: for grid locations
    Vector2i GetRowColumn(int index){

        int column = index % 4;
        int row = index / 4;

        return new Vector2i(column, row);
    }

    //Returns whether one cell is next to another
    bool GetAdjacent(int index1, int index2)
    {
        //If either one step away horizontally or vertically and also zero away the other way (no diagonals) 
        if (
            ((Mathf.Abs(GetRowColumn(index1).x - GetRowColumn(index2).x) == 1) && (Mathf.Abs(GetRowColumn(index1).y - GetRowColumn(index2).y) == 0)) ||
            ((Mathf.Abs(GetRowColumn(index1).x - GetRowColumn(index2).x) == 0) && (Mathf.Abs(GetRowColumn(index1).y - GetRowColumn(index2).y) == 1))
        ) {
            return true;
        }

        return false;
    }

    //Check whether said cell should become highlighted/selected
    public bool CheckCellClick(Transform incomingCellTransform)
    {
        //Get the index of the cell from the inputted transform
        int ictIndex = incomingCellTransform.GetSiblingIndex(); 

        //If there are no selected tiles already, this is the new first selected tile
        if (selectedTiles.Count == 0) {
            AddLetter(ictIndex);
            return true;
        }

        //Check to see if the tile is already in the list (returns true, but doesn't re-add to list)
        foreach (int tile in selectedTiles)
        {
            //If the tile literally already is this tile (already one of the selected tiles)
            if (ictIndex == tile)
            {
                return true;
            }
        }
        //Check to see if the tile is next to an already selected tile
        foreach (int tile in selectedTiles)
        {
            //If is adjacent, return true
            if (GetAdjacent(ictIndex, tile))
            {
                AddLetter(ictIndex);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Add letter to the list of currently selected letters (displayed at the top of the screen)
    /// </summary>
    /// <param name="cellIndex"></param>
    void AddLetter(int cellIndex)
    {
        selectedTiles.Add(cellIndex);
        //Update the text that says the word
        if (selectedTiles.Count == 1)
        {
            text.text = "";
        }
        text.text += transform.GetChild(cellIndex).GetComponent<LetterGridCell>().letter;
    }

    private void Update()
    {
        //When lifting the mouse up, run the function for that. Should check if the currently selected letters are a word
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            MouseUp();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }

    }

    void MouseUp()
    {
        if (levelFinished)
        {
            //return;
        }

        //If word is longer than 3 letters: Check if text is a word
        if (selectedTiles.Count >= 3)
        {
                //Unnecessary as long as "text.text" is reliable, otherwise use this code
                /*
                string s = "";
                foreach (int tile in selectedTiles)
                {
                    s += transform.GetChild(tile).GetComponent<LetterGridCell>().letter;
                }
                CheckWordIsInDictionary(s);
                */

            //Check the word against the dictionary
            if (CheckWordIsInDictionary(text.text)) {

                //Remove completed letters
                foreach (int tile in selectedTiles)
                {
                    Transform targetCell = transform.GetChild(tile);

                    //Set the letter to null (zero char)
                    targetCell.GetComponent<LetterGridCell>().letter = (char)0;

                    //Destroy the visual object that will be moved over
                    targetCell.GetComponent<LetterGridCell>().KillLetterObject();

                }

                //Update score based on length of word
                ScoreSaver.score += (selectedTiles.Count-2)* ScoreSaver.level;
                scoreText.text = ScoreSaver.score +"/"+ ScoreSaver.maxScore;

                //Update longest word if current word is longer
                if (ScoreSaver.longestWord.Length < selectedTiles.Count)
                {
                    ScoreSaver.longestWord = text.text;
                }

                longestWordText.text = text.text + " | " + ScoreSaver.longestWord;


                //If score high enough, level is finished (go to next one)
                if (ScoreSaver.score >= ScoreSaver.maxScore)
                {
                    StartCoroutine(NextLevel());
                    return;
                }


                //Check for falling spaces, make 'em fall if they should. It's in reverse cos you wanna check from the bottom upward
                foreach (int i in selectedTiles) 
                {
                    Drop(transform.GetChild(i));
                }

            }
        }


        //Re-set each tile to default/blank
        foreach (int tile in selectedTiles)
        {
            transform.GetChild(tile).GetComponent<MaskableGraphic>().color = Color.white * new Color(1, 1, 1, 0.0f);
            transform.GetChild(tile).GetComponent<LetterGridCell>().selected = false;
        }
        text.text = "Make a word, please.";

        selectedTiles.Clear();

    }

    public void StartNextLevelCoroutine()
    {
        StartCoroutine(NextLevel());
    }

    public IEnumerator NextLevel()
    {
        levelFinished = true;

        //Re-set each tile to default/blank
        foreach (int tile in selectedTiles)
        {
            transform.GetChild(tile).GetComponent<MaskableGraphic>().color = Color.white * new Color(1, 1, 1, 0.0f);
        }

        selectedTiles.Clear();

        ScoreSaver.totalScore += ScoreSaver.score;

        ScoreSaver.score = 0;
        ScoreSaver.level += 1;
        ScoreSaver.maxScore = (4 + ScoreSaver.level) * ScoreSaver.level;
        DOTween.KillAll();
        foreach (Transform child in transform)
        {
            child.GetComponent<LetterGridCell>().KillLetterObject();
        }

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("WordDrop");

    }


    bool CheckWordIsInDictionary(string word)
    {
        word = word.ToLower();

        return dictionaryOfWords.ContainsKey(word);
    }


    //This should be when the window is resized/when the phone is turned around
    void OnRectTransformDimensionsChange()
    {
        if (gridLayout)
        {
            gridLayout.CalculateLayoutInputHorizontal();
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());

        foreach (Transform child in transform)
        {
            if (child.GetComponent<LetterGridCell>().letterObject)
            {
                child.GetComponent<LetterGridCell>().letterObject.GetComponent<UITweener>().In(true);
            }
        }

    }


}


struct Vector2i
{
    public int x, y;

    public Vector2i(int x, int y) { this.x = x; this.y = y; }
}