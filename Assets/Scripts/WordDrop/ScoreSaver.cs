
//Score saivng static class for the WordDrop (should remember score between levels/scenes etc)

public static class ScoreSaver
{

    static public int score = 0;
    static public int level = 1;
    static public int maxScore = 5; //(4+level)*level;

    static public string longestWord = "";
    static public int totalScore = 0;


}
