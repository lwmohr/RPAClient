using RockPaperScissorsPro;
using System;

namespace RockPaperAzure
{
    public class MyBot : IRockPaperScissorsBot
    {

        bool firstTime = true;
        BotLogger MyBotLog = new BotLogger();
        int waterCounter = 0;
        int dynaCounter = 0;
        public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        {
            if (!firstTime)
            {
                int myMove, opponentMove;
                myMove = 3;
                opponentMove = 3;
                if (you.LastMove == Moves.WaterBalloon)
                    myMove = 0;
                if (you.LastMove == Moves.Dynamite)
                    myMove = 1;
                if (you.LastMove == Moves.Rock)
                    myMove = 3;
                if (you.LastMove == Moves.Scissors)
                    myMove = 4;
                if (you.LastMove == Moves.Paper)
                    myMove = 5;
                if (opponent.LastMove == Moves.WaterBalloon)
                    opponentMove = 0;
                if (opponent.LastMove == Moves.Dynamite)
                    opponentMove = 1;
                if (opponent.LastMove == Moves.Rock)
                    opponentMove = 3;
                if (opponent.LastMove == Moves.Scissors)
                    opponentMove = 4;
                if (opponent.LastMove == Moves.Paper)
                    opponentMove = 5;
                MyBotLog.addLog(myMove,opponentMove);
            }
            else
            {
                firstTime = false;
            return Moves.GetRandomMove();
        }

            int history;
            int dynamite = 0;
            int water = 0;
            int randomNum;
            int tiesRemaining;

            if ((opponent.LastMove == Moves.Dynamite) && (you.LastMove != Moves.Dynamite))
                dynaCounter++;
            else
                dynaCounter = 0;
            if (dynaCounter == 3)
                return Moves.WaterBalloon;

            //if (you.NumberOfDecisions < 5)
            //    you.Log.AppendLine(String.Format("{0}: Move : {1} ms {2}", you.NumberOfDecisions, you.LastMove,you.TotalTimeDeciding.Milliseconds));
             history = 5;
             //if ((you.NumberOfDecisions >= history) && (MyBotLog.getTies() > 1))
                 if (you.NumberOfDecisions >= history)
             {
                 MyBotLog.analyzeThrow(history, ref dynamite, ref water);
                 you.Log.AppendLine(String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", you.NumberOfDecisions, MyBotLog.getThrowNum(), MyBotLog.getTies(), MyBotLog.getPointsWon(), you.LastMove, opponent.LastMove, water, dynamite));
             }
            if (you.LastMove.Equals(opponent.LastMove) || (opponent.LastMove.Equals(Moves.Dynamite) || opponent.LastMove.Equals(Moves.WaterBalloon)))
            {
                history = 5;
                if (you.NumberOfDecisions >= history)
                {
                    MyBotLog.analyzeThrow(history, ref dynamite, ref water);
                    if ((dynamite >= 3) && you.HasDynamite)
                    {
                        you.Log.AppendLine(String.Format("{0}: Water Balloon : {1}/{2}", you.NumberOfDecisions, dynamite, history));
                        if (waterCounter > 0)
                        {
                            you.Log.AppendLine(String.Format("{0}: oops decrement water counter : {1}/{2}", you.NumberOfDecisions, waterCounter, history));
                            waterCounter--;
                        }
                        else
                        {
                            waterCounter = 2;
                            return Moves.WaterBalloon;
                        }
                    }
                    if (water >= 3)
                    {
                        you.Log.AppendLine(String.Format("{0}: Random : {1}/{2}", you.NumberOfDecisions, water, history));
                        return Moves.GetRandomMove();
                    }
                }
            }

            //if ((you.LastMove != Moves.Dynamite) && ((you.LastMove.Equals(opponent.LastMove)) && you.HasDynamite))
            if ((you.LastMove.Equals(opponent.LastMove)) && you.HasDynamite)
            {
                randomNum = Moves.GetRandomNumber(100);
                if ((MyBotLog.getTies() > 1 ) && ( randomNum < 30))
                //if (((MyBotLog.getTies() == 2) && (randomNum < 25)) && ((Moves.GetRandomNumber(50) + you.NumberOfDecisions) > 40))
                {
                    //you.Log.AppendLine(String.Format("{0}: Multiple ties : {1},{2}", you.NumberOfDecisions, MyBotLog.getTies(),randomNum));
                    return Moves.Dynamite;
                }
                //randomNum = Moves.GetRandomNumber(3);
                //if (((MyBotLog.getTies() > 2) && (randomNum == 2)) && ((Moves.GetRandomNumber(50) + you.NumberOfDecisions) > 40))
                //{
                //    //you.Log.AppendLine(String.Format("{0}: Multiple ties : {1},{2}", you.NumberOfDecisions, MyBotLog.getTies(),randomNum));
                //    return Moves.Dynamite;
                //}
                tiesRemaining = ((2 * (rules.PointsToWin - opponent.Points)) / 3);
                randomNum = Moves.GetRandomNumber(tiesRemaining);
                //if ((randomNum <= you.DynamiteRemaining) && ((Moves.GetRandomNumber(rules.PointsToWin / 2) + you.NumberOfDecisions) > ((rules.PointsToWin * 3) / 2)))
                if (randomNum <= you.DynamiteRemaining)
                {
                    //you.Log.AppendLine(String.Format("{0}: Single Tie : {1},{2}", you.NumberOfDecisions, MyBotLog.getTies(),tiesRemaining));
                    return Moves.Dynamite;
                }
            }
            return Moves.GetRandomMove();
        }
    }

    public class BotLogger
    {
        private int[,] myArray = new int[5000, 5];
        int currentTies = 0;
        int pointsWon = 0;
        int throwNum = 0;


        public void addLog(int myThrow, int opponentThrow)
        {
            throwNum++;
            if (myThrow == opponentThrow)
            {
                currentTies++;
            }
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
            pointsWon = 0;
        }

        public int getTies()
        {
            return myArray[throwNum - 1, 2];
        }
        public int getPointsWon()
        {
            return myArray[throwNum - 1, 3];
        }
        public int getThrowNum()
        {
            return myArray[throwNum - 1, 4];
        }

        public void analyzeThrow(int histInstances, ref int dynamite, ref int water)
        {
            dynamite = 0;
            water = 0;
            int tmpTies = myArray[throwNum - 1,2];
            for (int i = (throwNum - 2); i >= 0; i--)
            {
                //if ((histInstances != 0) && ((myArray[i,0] == myArray[throwNum - 1,0]) && (myArray[i,2] == myArray[throwNum - 1,2])))
                if ((histInstances != 0) && (myArray[i, 2] == myArray[throwNum - 1, 2]))
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
