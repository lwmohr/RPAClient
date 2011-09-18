using RockPaperScissorsPro;
using System.Collections;
using System;

namespace RockPaperAzure
{
    public class MyBot : IRockPaperScissorsBot
    {

        bool firstTime = true;
        BotLogger MyBotLog = new BotLogger();
        public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        {
            
            if (!firstTime)
            {
                int myMove, opponentMove;
                if (you.LastMove.Equals(Moves.WaterBalloon))
                    myMove = 0;
                else if (you.LastMove.Equals(Moves.Dynamite))
                    myMove = 1;
                else 
                    myMove = 3;
                if (opponent.LastMove.Equals(Moves.WaterBalloon))
                    opponentMove = 0;
                else if (opponent.LastMove.Equals(Moves.Dynamite))
                    opponentMove = 1;
                else 
                    opponentMove = 3;
                MyBotLog.addLog(myMove,opponentMove);
                
            }
            else
                firstTime = false;

            int history;
            int dynamite = 0;
            int water = 0;
            int randomNum;
            int tiesRemaining;

            if (you.LastMove.Equals(opponent.LastMove) || (opponent.LastMove.Equals(Moves.Dynamite) || opponent.LastMove.Equals(Moves.WaterBalloon)))
            {
                history = 5;
                MyBotLog.analyzeThrow(history, ref dynamite, ref water);
                if ((dynamite >= 3) && you.HasDynamite)
                {
                        you.Log.AppendLine(String.Format("{0}: Water Balloon : {1}/{2}", you.NumberOfDecisions, dynamite, history));
                        return Moves.WaterBalloon;
                    }
                if (water >= 3)
                {
                    you.Log.AppendLine(String.Format("{0}: Random : {1}/{2}", you.NumberOfDecisions, water, history));
                    return Moves.GetRandomMove();

                }
            }

            if ((you.LastMove.Equals(opponent.LastMove)) && you.HasDynamite)
            {
                randomNum = Moves.GetRandomNumber(3);
                if ((MyBotLog.getTies() > 1) && ( randomNum == 2))
                {
                    you.Log.AppendLine(String.Format("{0}: Multiple ties : {1},{2}", you.NumberOfDecisions, MyBotLog.getTies(),randomNum));
                    return Moves.Dynamite;
                }
                tiesRemaining = ((2 * (rules.PointsToWin - opponent.Points)) / 3);
                randomNum = Moves.GetRandomNumber(tiesRemaining);
                if (randomNum <= you.DynamiteRemaining)
                {
                    you.Log.AppendLine(String.Format("{0}: Single Tie : {1},{2}", you.NumberOfDecisions, MyBotLog.getTies(),tiesRemaining));
                    return Moves.Dynamite;
                }
            }
            return Moves.GetRandomMove();
        }
    }

    public class BotLogger
    {
    //    ArrayList logArray = new ArrayList();
    //List<int> ShootInfo = new List<int>();
	    private int[,] myArray = new int[3000,5];
        int currentTies = 0;
        int pointsWon = 0;
        int throwNum = 0;


        public void addLog(int myThrow, int opponentThrow)
        {
            throwNum++;
            if (myThrow == opponentThrow)
                currentTies++;
            else
            {
                pointsWon = currentTies + 1;
                currentTies = 0;
            }

            myArray[throwNum - 1,0] = myThrow;
            myArray[throwNum - 1,1] = opponentThrow;
            myArray[throwNum - 1,2] = currentTies;
            myArray[throwNum - 1,3] = pointsWon;
            myArray[throwNum - 1,4] = throwNum;
        }

        public int getTies()
        {
            return currentTies;
        }

        public void analyzeThrow(int histInstances, ref int dynamite, ref int water)
        {
            dynamite = 0;
            water = 0;
            int tmpTies = myArray[throwNum - 1,2];
            for (int i = (throwNum - 2); i >= 0; i--)
            {
                if (histInstances == 0)
                    break;
                if ((myArray[i,0] == myArray[throwNum - 1,0]) && (myArray[i,2] == myArray[throwNum - 1,2]))
                {
                    if (myArray[i + 1,1] == 0)
                        water++;
                    else if (myArray[i + 1,1] == 1)
                        dynamite++;
                    histInstances--;
                }
            }
        }

    }
}
