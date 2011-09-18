using RockPaperScissorsPro;
using System.Collections;

namespace RockPaperAzure
{
    public class MyBot : IRockPaperScissorsBot
    {
        int tiesCounter = 0;

        bool firstTime = true;
        bool twoTieWater = false;
        bool waterAfterTie = false;
        bool firstTieDyna = false;
        bool secondTieDyna = false;
        int dynaTwice, dynaSingleWater, dynaTwiceWater, waterSingle, transitionPoint;
        BotLogger MyBotLog = new BotLogger();
        public Move MakeMove(IPlayer you, IPlayer opponent, GameRules rules)
        {
            MyBotLog.addLog("asdfxasdf","asdfasdf",4,6);
            if (firstTime)
            {
                dynaTwice = Moves.GetRandomNumber(500);
                dynaSingleWater = Moves.GetRandomNumber(500);
                dynaTwiceWater = Moves.GetRandomNumber(500);
                //dynaTwiceWater = 5;
                waterSingle = Moves.GetRandomNumber(500);
                firstTime = false;
            }

            //float highPoint = you.Points;
            //float totalThrows = you.NumberOfDecisions;
            //float remainingDynamite = you.DynamiteRemaining;
            //if (you.Points < opponent.Points)
            //    highPoint = opponent.Points;
            //float winPercent = highPoint / totalThrows;
            //float predictedThrows = (float)1000 / winPercent;
            //float remainingThrows = predictedThrows - totalThrows;
            //float oddsForDynamite = remainingDynamite / remainingThrows;

            //if ((dynaTwice == you.NumberOfDecisions) || ((dynaTwice + 1) == you.NumberOfDecisions))
            //    return Moves.Dynamite;
            //if (dynaSingleWater == you.NumberOfDecisions)
            //    return Moves.Dynamite;
            //if ((dynaSingleWater + 1) == you.NumberOfDecisions)
            //    return Moves.WaterBalloon;
            //if ((dynaTwiceWater == you.NumberOfDecisions) || ((dynaTwiceWater + 1) == you.NumberOfDecisions))
            //    return Moves.Dynamite;
            //if ((dynaTwiceWater + 2) == you.NumberOfDecisions)
            //    return Moves.WaterBalloon;
            //if (waterSingle == you.NumberOfDecisions)
            //    return Moves.WaterBalloon;

            if (tiesCounter >= 2 && opponent.LastMove == Moves.WaterBalloon)
                twoTieWater = true;
            if ((tiesCounter == 1) && (opponent.LastMove == Moves.Dynamite))
                firstTieDyna = true;
            if ((tiesCounter == 2) && (opponent.LastMove == Moves.Dynamite))
                secondTieDyna = true;
            if ((you.LastMove == opponent.LastMove) && (opponent.LastMove == Moves.WaterBalloon))
                waterAfterTie = true;

            if (you.LastMove == opponent.LastMove)
                tiesCounter++;
            else
                tiesCounter = 0;

            if (you.HasDynamite)
            {
                if (tiesCounter > 0)
                {
                    int myRand = Moves.GetRandomNumber(1000);
                    switch (tiesCounter){

                        case 1:
                            //if ((transitionPoint > you.NumberOfDecisions) && opponent.HasDynamite)
                            //{
                                if ((myRand < 500) && (you.DynamiteRemaining >= Moves.GetRandomNumber((int)(rules.PointsToWin - opponent.Points) / 2) +1))
                                    if ((rules.PointsToWin - opponent.Points) < 100)
                                        return Moves.Dynamite;
                                //if ((firstTieDyna) && ((myRand > 950) && (opponent.DynamiteRemaining > 1)))
                                //    return Moves.WaterBalloon;

                            //}
                            break;
                        case 2:
                            if ((you.LastMove == Moves.Dynamite) && (opponent.DynamiteRemaining > 1))
                            {
                                if ((myRand < 450) && (you.DynamiteRemaining >= Moves.GetRandomNumber((int)((rules.PointsToWin - opponent.Points) / 5) +1)))
                                    return Moves.Dynamite;
                                if (myRand > 950)
                                    return Moves.WaterBalloon;

                            }
                            else if (you.DynamiteRemaining >= Moves.GetRandomNumber((int)((rules.PointsToWin - opponent.Points) / 5) +1))
                            {
                                if (myRand < 500)
                                    return Moves.Dynamite;
                                //if ((secondTieDyna) && ((myRand > 950) && (opponent.DynamiteRemaining > 1)))
                                //    return Moves.WaterBalloon;
                            }
                            break;
                        default:
                            if ((!waterAfterTie) && (opponent.DynamiteRemaining < 2))
                            {
                                //if (myRand < 750)
                                    return Moves.Dynamite;
                                //else if ((myRand > 500) && (opponent.DynamiteRemaining > 1))
                                //    return Moves.WaterBalloon;

                            }
                            else if (!waterAfterTie)
                            {
                                if (myRand < 500)
                                    return Moves.Dynamite;
                                else if ((myRand > 500) && (opponent.DynamiteRemaining > 1))
                                    return Moves.WaterBalloon;

                            }

                            else if ((you.LastMove == Moves.Dynamite) && (opponent.DynamiteRemaining > 1))
                            {
                                if ((myRand < 450) && (you.DynamiteRemaining >= Moves.GetRandomNumber((int)((rules.PointsToWin - opponent.Points) / 11) +1)))
                                    return Moves.Dynamite;
                                if (myRand > 950) 
                                    return Moves.WaterBalloon;

                            }
                            else
                            {
                                if ((myRand < 500) && (you.DynamiteRemaining >= Moves.GetRandomNumber((int)((rules.PointsToWin - opponent.Points) / 11) +1)))
                                    return Moves.Dynamite;
                                if ((secondTieDyna) && ((myRand > 950) && (opponent.DynamiteRemaining > 1)))
                                    return Moves.WaterBalloon;
                            }
                            break;
                    }
                }
                if ((you.DynamiteRemaining >= Moves.GetRandomNumber(((rules.PointsToWin - opponent.Points) * 16) - 14)) && ((rules.PointsToWin - opponent.Points) < 20))
                    return Moves.Dynamite;
            }
            return Moves.GetRandomMove();
        }
    }

    public class BotLogger
    {
        ArrayList logArray = new ArrayList();
	List<int> ShootInfo = new List<int>();
	private int[,] myArray = new int[3000,5];
        private list ShootInfo
        {
            private string myThrow { get; set; }
            private string opponentThrow { get; set; }
            private int currentTies { public get; set; }
            private int pointsWon { get; set; }

            public ShootInfo(string myThrow, string opponentThrow, int currentTies, int pointsWon)
            {
                this.myThrow = myThrow;
                this.opponentThrow = opponentThrow;
                this.currentTies = currentTies;
                this.pointsWon = pointsWon;
            }
        }

        public void addLog(int myThrow, int opponentThrow, int currentTies, int pointsWon, int throwNum)
        {
            myArray[throwNum - 1][0] = myThrow;
            myArray[throwNum - 1][1] = opponentThrow;
            myArray[throwNum - 1][2] = currentTies;
            myArray[throwNum - 1][3] = pointsWon;
            myArray[throwNum - 1][4] = throwNum;
        }

        public void addLog(string myThrow, string opponentThrow, int currentTies, int pointsWon)
        {
            logArray.Add(new ShootInfo(myThrow, opponentThrow, currentTies, pointsWon));
        }

        //int i = (ShootInfo)logArray[j].get.currentTies;
        public void getShoot()
        {
            int j = 5;
            int num;
            ShootInfo[] MyLogArray = logArray.ToArray(typeof(ShootInfo)) as ShootInfo[];
            foreach (ShootInfo MyLog in MyLogArray)
            {
                num = MyLog(typeof)ShootInfo(j);
                
            }
        }

    }
}
